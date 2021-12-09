# frozen_string_literal: true

# Basin
class Basin
  attr_reader :size, :x, :y

  def initialize(pos_x, pos_y, arr)
    @x = pos_x
    @y = pos_y
    @size = 0
    @field = arr
    @markers = {}
    fill(pos_x, pos_y)
  end

  def out_of_bounds?(pos_x, pos_y)
    pos_x >= @field.size || pos_x.negative? || pos_y >= @field.first.size || pos_y.negative?
  end

  def fill(pos_x, pos_y)
    return if out_of_bounds?(pos_x, pos_y) # out of bounds
    return if @field[pos_x][pos_y] == 9 # border reached
    return if @markers[[pos_x, pos_y]] == 'X' # been here before

    @size += 1
    @markers[[pos_x, pos_y]] = 'X'

    fill(pos_x + 1, pos_y)
    fill(pos_x - 1, pos_y)
    fill(pos_x, pos_y + 1)
    fill(pos_x, pos_y - 1)
  end
end

def low_point?(i, j, arr)
  r = true
  r &&= arr[i - 1][j] > arr[i][j] if i - 1 >= 0
  r &&= arr[i][j - 1] > arr[i][j] if j - 1 >= 0
  r &&= arr[i + 1][j] > arr[i][j] if i + 1 < arr.size
  r &&= arr[i][j + 1] > arr[i][j] if j + 1 < arr.first.size
  r
end

input = File.readlines('input.txt').map(&:chomp).map { |l| l.chars.map(&:to_i) }
basins = []

input.each_with_index do |row, i|
  row.each_index do |j|
    basins << Basin.new(i, j, input) if low_point?(i, j, input)
  end
end

# Part 1
p basins.map { |b| input[b.x][b.y] + 1 }.sum # => 631

# Part 2
p basins.sort_by(&:size).reverse.take(3).map(&:size).inject(&:*) # => 821560
