# frozen_string_literal: true

$lines = File.readlines('input.txt', chomp: true).map { |line| line.split('') }

# Part 1
def has_symbol(x, y)
  result = false

  (-1..1).each do |i|
    (-1..1).each do |j|
      next if (x + i).negative? || (y + j).negative? || x + i >= $lines.size || y + j >= $lines[x].size

      result ||= $lines[x + i][y + j].match?(/[^\d.\n]/)
    end
  end

  result
end

$checkedIndices = []

def find_num_at_index(x, y)
  result = []
  line = $lines[x]
  result << line[y]

  return 0 if $checkedIndices.include?([x, y])

  $checkedIndices << [x, y]

  # check left
  i = y - 1
  while i >= 0 && line[i].match?(/\d/)
    result.unshift(line[i])
    $checkedIndices << [x, i]
    i -= 1
  end

  # check right
  i = y + 1
  while i < line.size && line[i].match?(/\d/)
    result << line[i]
    $checkedIndices << [x, i]
    i += 1
  end

  result.join.to_i
end

indices = []

$lines.each_with_index do |line, x|
  line.each_with_index do |char, y|
    next unless char.match?(/\d/)

    indices << [x, y] if has_symbol(x, y)
  end
end

puts indices.map { |x, y| find_num_at_index(x, y) }.sum # => 535235

# Part 2
$checkedIndices.clear

def gear_vibe_check(x, y)
  count = 0
  numbers = []

  (-1..1).each do |i|
    (-1..1).each do |j|
      next if (x + i).negative? || (y + j).negative? || x + i >= $lines.size || y + j >= $lines[x].size

      if $lines[x + i][y + j].match?(/\d/)
        numbers << find_num_at_index(x + i, y + j)
        count += 1
      end
    end
  end

  numbers.reject(&:zero?)
end

gear_indices = []

$lines.each_with_index do |line, x|
  line.each_with_index do |char, y|
    next unless char == '*'

    gear_indices << [x, y]
  end
end

gear_indices.map! { |x, y| gear_vibe_check(x, y) }

p gear_indices.select { |arr| arr.size == 2 }.map { |a| a.inject(&:*) }.sum # => 79844424
