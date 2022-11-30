# frozen_string_literal: true

# Part 1
lines = File.readlines('input.txt').map(&:to_i)

count = 0
last = lines.first
lines.each do |i|
  count += 1 if last < i
  last = i
end

puts count # => 1583

# Part 2
count = 0
last = lines.take(3).sum
lines.each_index do |i|
  sum = lines[i..(i + 2)].sum
  count += 1 if last < sum
  last = sum
end

puts count # => 1627
