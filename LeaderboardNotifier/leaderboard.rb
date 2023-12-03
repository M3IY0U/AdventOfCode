# frozen_string_literal: true

require 'bundler/inline'
require 'json'

gemfile do
  source 'https://rubygems.org'
  gem 'discord-notifier'
  gem 'json', '~> 2.6.1'
  gem 'httparty'
end

abort('Please provide a config.json in the same directory this is run in') unless File.exist?('config.json')

@config = JSON.parse(File.read('config.json'))
@url = "https://adventofcode.com/#{@config['year']}/leaderboard/private/view/#{@config['leaderboard_id']}.json"
@cookie = "session=#{@config['session_cookie']}"
@local = JSON.parse(File.read('local.json')) if File.exist?('local.json')
@local = {} if @local.nil?

Discord::Notifier.setup do |config|
  config.url = @config['webhook_url']
  config.username = @config['webhook_username']
  config.avatar_url = @config['webhook_avatar_url']
end

def make_request
  puts 'Making request to AoC'
  url = @url
  headers = {
    'Content-Type' => 'application/json',
    'Cookie' => @cookie
  }

  res = HTTParty.get(url, headers: headers)
  res.body
end

def notify(user, content)
  leaderboard_url = @url.chomp('.json')
  embed = Discord::Embed.new do
    author name: "#{user} achieved new star(s)!",
           url: leaderboard_url
    description content
    color 0x009000
  end

  puts 'Sending message via webhook'
  Discord::Notifier.message(embed)
end

def check(member)
  puts "Checking member #{member['name']}"
  old = @local.find { |m| m['id'] == member['id'] } || {}
  return if old['stars'] == member['stars']

  new_stars = Hash[*(member['completion_day_level'].to_a - old['completion_day_level'].to_a).flatten]
  content = ''
  new_stars.each do |star|
    parts = star.last
                .transform_keys { |k| "Part #{k}" }
                .transform_values { |v| ", achieved <t:#{v['get_star_ts']}:R>" }
    parts.each do |part|
      if old.empty? || !(old['completion_day_level'][star.first]&.has_key? part.first[-1])
        content += "Day #{star.first} #{part.join}\n"
      end
    end
  end
  notify(member['name'], content)
end

loop do
  begin
    web = JSON.parse(make_request)['members'].map(&:last)
    web.each { |member| check(member) }
    last_sent = Time.now
    @local = web
    File.write('local.json', JSON.pretty_generate(web))
  rescue StandardError => e
    puts "Error checking leaderboard: #{e}"
  end

  sleep 60 and puts "Sleeping 900 seconds until #{last_sent + 900}" until Time.now > last_sent + 900
end
