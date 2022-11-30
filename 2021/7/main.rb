# frozen_string_literal: true

input = File.read('input.txt').split(',').map(&:to_i)

def median(arr)
  sorted = arr.sort
  len = sorted.length
  (sorted[(len - 1) / 2] + sorted[len / 2]) / 2
end

def price(x)
  x * (x + 1) / 2
end

# Part 1
fuel = 0
m = median(input)
input.each { |c| fuel += (c - m).abs }

p fuel # => 349357

# Part 2
fuel = 0
mean = input.sum / input.size
input.sort!
fuel = [input.map { |i| price((i - mean).abs) }.sum, input.map { |i| price((i - mean - 1).abs) }.sum].min

p fuel # => 96708205
