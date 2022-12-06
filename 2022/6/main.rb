# frozen_string_literal: true

# Part 1
p File.read('input.txt').chars.each_cons(4).to_a.index { |a| a.uniq.size == 4 } + 4

# Part 2
p File.read('input.txt').chars.each_cons(14).to_a.index { |a| a.uniq.size == 14 } + 14
