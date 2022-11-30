# frozen_string_literal: true

input = File.readlines('input.txt')

# Part 1
pos, depth = input.partition { |l| l.include?('forward') }
sum_d = depth.map { |i| i.gsub('down ', '+').gsub('up ', '-').chomp }.map(&:to_i).sum
sum_p = pos.map { |i| i.gsub('forward ', '+').chomp }.map(&:to_i).sum

puts sum_d * sum_p # => 1746616

# Part 2
sum_d = 0
sum_p = 0
aim = 0

input.each do |line|
  inst, num = line.split
  case inst
  when 'forward'
    sum_p += num.to_i
    sum_d += aim * num.to_i
  when 'up'
    aim -= num.to_i
  when 'down'
    aim += num.to_i
  end
end

puts sum_d * sum_p # => 1741971043
