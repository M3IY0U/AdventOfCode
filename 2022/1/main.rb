# frozen_string_literal: true

lines = File.read('input.txt').split("\n\n").map { |s| s.gsub("\n", ' ').split.map(&:to_i).sum }

# Part 1
puts lines.max # => 71506

# Part 2
puts lines.max(3).sum # => 209603
