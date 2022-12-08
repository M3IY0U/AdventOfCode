# frozen_string_literal: true

$grid = File.readlines('input.txt').map { |l| l.chomp.split('').map(&:to_i) }

# Part 1
def check_visibility(x, y)
  # check row
  left = (0...x).to_a.all? { |t| $grid[t][y] < $grid[x][y] }
  right = ((x + 1)...$grid.length).to_a.all? { |t| $grid[t][y] < $grid[x][y] }

  # check column
  top = (0...y).to_a.all? { |t| $grid[x][t] < $grid[x][y] }
  bottom = ((y + 1)...$grid[0].length).to_a.all? { |t| $grid[x][t] < $grid[x][y] }

  left || right || top || bottom
end

trees = []
(1...($grid.length - 1)).each do |x|
  (1...($grid[0].length - 1)).each do |y|
    trees << check_visibility(x, y)
  end
end

p trees.count(true) + ($grid.length * 2 + $grid[0].length * 2 - 4) # => 1818

# Part 2
def calc_scenic_score(x, y)
  current = $grid[x][y]
  i = x
  j = y
  scores = [0, 0, 0, 0]
  # check row
  loop do
    scores[0] += 1
    break if i - 1 <= 0 || $grid[i - 1][y] >= current

    i -= 1
  end
  i = x
  loop do
    scores[1] += 1
    break if i + 1 >= $grid.length || $grid[i + 1][y] >= current

    i += 1
  end

  # check column
  loop do
    scores[2] += 1
    break if j - 1 <= 0 || $grid[x][j - 1] >= current

    j -= 1
  end

  j = y + 1 # "why add + 1 here?" 🤓🤓🤓
  loop do
    scores[3] += 1
    break if j + 1 >= $grid[0].length || $grid[x][j + 1] >= current

    j += 1
  end

  scores.inject(:*)
end

scores = []
(1...($grid.length - 1)).each { |x| (1...($grid[0].length - 1)).each { |y| scores << calc_scenic_score(x, y) } }
p scores.max # => 368368
