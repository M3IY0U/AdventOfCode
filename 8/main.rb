# frozen_string_literal: true

# Part 1
p File.readlines('input.txt').map(&:chomp).map { |l| l.split(' | ')[1].split }.flatten.select { |w| [2, 3, 4, 7].include? w.length }.size # => 416

# Part 2 coming when i can wrap my head around whatever the fuck that task is
