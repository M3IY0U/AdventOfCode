# frozen_string_literal: true

# Helper class for this entire thing
class Board
  def initialize(arr)
    @board = Array.new(arr.map { |row| row.split.map(&:to_i) })
  end

  def rows
    @board
  end

  def columns
    @board.transpose
  end

  def mark(val)
    @board.collect! do |i|
      if i.include?(val)
        i.collect! { |x| x == val ? -1 : x }
      else
        i
      end
    end
  end

  def sum_unmarked
    @board.flatten.reject { |v| v == -1 }.sum
  end

  def check
    rows.any? { |r| r.all?(-1) } || columns.any? { |c| c.all?(-1) }
  end
end

input = File.read('input.txt').split("\n\n")
calls = input.shift.split(',').map(&:to_i)
boards = input.map { |b| Board.new(b.split("\n")) }

# Part 1
result = 0
calls.each do |call|
  boards.each do |board|
    board.mark(call)
    break result = board.sum_unmarked * call if board.check
  end
  break if result != 0
end

puts result # => 51034

# Part 2
calls.each do |call|
  if boards.size == 1
    boards.first.mark(call)
    break result = boards.first.sum_unmarked * call if boards.first.check

    boards.first.mark(call)
    next
  end

  boards.each { |board| board.mark(call) }
  boards.delete_if(&:check)
end

puts result # => 5434
