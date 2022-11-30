# frozen_string_literal: true

# gulu gulu
class Fish
  attr_reader :timer

  def initialize(timer)
    @timer = timer
  end

  def update
    @timer = @timer.zero? ? 6 : @timer - 1
  end
end

# Holds fishies
class Aquarium
  attr_reader :fishes

  def initialize(fishes)
    @fishes = fishes
  end

  def simulate
    @fishes.select { |f| f.timer.zero? }.each { @fishes << Fish.new(9) }
    @fishes.each(&:update)
  end
end

# Part 1 (the "clueless" approach)
aquarium = Aquarium.new(File.read('input.txt').split(',').map(&:to_i).map { |v| Fish.new(v) })
80.times { aquarium.simulate }

p aquarium.fishes.size # => 350605

# Part 2
counts = Array.new(9) { 0 }
File.read('input.txt').split(',').map { |n| counts[n.to_i] += 1 }
256.times do
  z = counts[0]
  (0...8).each { |n| counts[n] = counts[n + 1] }

  counts[6] += z
  counts[8] = z
end

p counts.sum # => 1592778185024
