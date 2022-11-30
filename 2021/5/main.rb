# frozen_string_literal: true

# Helper class
class Line
  def initialize(arr)
    @x1, @y1 = arr[0].split(',').map(&:to_i)
    @x2, @y2 = arr[1].split(',').map(&:to_i)
  end

  def points
    [@x1, @y1, @x2, @y2]
  end

  def hv?
    @x1 == @x2 || @y1 == @y2
  end
end

def sign(num)
  num <=> 0
end

def draw_line(x1, y1, x2, y2)
  n = [(x2 - x1).abs + 1, (y2 - y1).abs + 1].max
  (0...n).each do |i|
    x = x1 + sign(x2 - x1) * i
    y = y1 + sign(y2 - y1) * i
    $board[y][x] += 1
  end
end

input = File.readlines('input.txt').map(&:chomp).map { |l| Line.new l.split(' -> ') }
SIZE = 1010

# Part 1
$board = Array.new(SIZE) { Array.new(SIZE) { 0 } }
input.each { |l| draw_line(*l.points) if l.hv? }
p $board.flatten.select { |i| i > 1 }.size # => 7269

# Part 2
$board = Array.new(SIZE) { Array.new(SIZE) { 0 } }
input.each { |l| draw_line(*l.points) }
p $board.flatten.select { |i| i > 1 }.size # => 21140
