# frozen_string_literal: true

input = File.readlines('input.txt').map(&:chomp)

# Part 1
count_ones = Hash.new(0)
input.first.length.times { |i| input.each { |line| count_ones[i] += 1 if line[i] == '1' } }
gamma = count_ones.transform_values { |v| v > input.length / 2 ? 1 : 0 }.values.join
epsilon = gamma.tr('01', '10')

p gamma.to_i(2) * epsilon.to_i(2) # => 749376

# Part 2
def filter_bits(arr, pos, val)
  val = val ? '1' : '0'
  arr.select { |line| line[pos] == val }
end

def get_common_bit(arr, pos)
  count1 = 0
  count0 = 0
  arr.each { |line| line[pos] == '1' ? count1 += 1 : count0 += 1 }
  return true if count0 == count1

  count1 > count0
end

oxy = input.clone
co2 = input.clone

input.first.length.times do |i|
  oxy = filter_bits(oxy, i, get_common_bit(oxy, i)) unless oxy.size == 1
  co2 = filter_bits(co2, i, !get_common_bit(co2, i)) unless co2.size == 1
end

p oxy.first.to_i(2) * co2.first.to_i(2) # => 2372923
