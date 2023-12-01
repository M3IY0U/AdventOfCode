# frozen_string_literal: true

digits = {
  'one' => '1',
  'two' => '2',
  'three' => '3',
  'four' => '4',
  'five' => '5',
  'six' => '6',
  'seven' => '7',
  'eight' => '8',
  'nine' => '9'
}

lines = File.readlines('input.txt').map(&:chomp)

puts lines.map { |i| i.gsub(/[a-z]/, '') }.map { |i| "#{i.chars.first}#{i.chars.last}" }.map(&:to_i).sum # => 55712

lines.each { |i| digits.each { |k, v| i.gsub!(k, "#{k[0]}#{v}#{k[-1]}") } }

puts lines.map { |i| i.gsub(/[a-z]/, '') }.map { |i| "#{i.chars.first}#{i.chars.last}" }.map(&:to_i).sum # => 55413
