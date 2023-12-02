# frozen_string_literal: true

lines = File.readlines('input.txt').map(&:chomp)

# Part 1
sum = 0
MAX_RED = 12
MAX_GREEN = 13
MAX_BLUE = 14

lines.each_with_index do |line, idx|
  sets = line.split(':')[1].split(';').map { |i| i.split(',') }
  sets.each { |i| i.map!(&:strip) }

  deadge = false

  sets.each do |set|
    set.each do |r|
      count, color = r.split(' ')
      if color == 'red' && count.to_i > MAX_RED || color == 'green' && count.to_i > MAX_GREEN || color == 'blue' && count.to_i > MAX_BLUE
        deadge = true and break
      end
    end
  end

  next if deadge

  sum += (idx + 1)
end

puts sum # => 2486

# Part 2
sum = 0

lines.each do |line|
  sets = line.split(':')[1].split(';').map { |i| i.split(',') }
  sets.each { |i| i.map!(&:strip) }

  guh = { 'red' => [], 'green' => [], 'blue' => [] }

  sets.each do |set|
    set.each do |r|
      count, color = r.split(' ')
      guh[color] << count.to_i
    end
  end

  sum += (guh['red'].max * guh['green'].max * guh['blue'].max)
end

puts sum # => 87984
