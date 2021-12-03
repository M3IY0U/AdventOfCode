# frozen_string_literal: true

input = File.readlines('input.txt')

# Part 1
count_ones = Hash.new(0)
input.first.length.times { |i| input.each { |line| count_ones[i] += 1 if line[i] == '1' } }
count_ones.transform_values! { |v| v > input.length / 2 ? 1 : 0 }

gamma = count_ones.values.join
epsilon = gamma.tr('01', '10')

p gamma.to_i(2) * epsilon.to_i(2) # => 749376
