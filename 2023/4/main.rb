# frozen_string_literal: true

lines = File.readlines('input.txt', chomp: true).map { |line| line.split(':')[1].strip }

# Part 1
games = lines.map { |line| line.split('|') }.each { |a| a.map!(&:split) }

games.each { |a| a.map! { |b| b.map!(&:to_i) } }

puts games.map { |a| (a[1] & a[0]).count }.reject(&:zero?).map { |i| (1...i).reduce(1) { |s, _| s * 2 } }.sum # => 21959

# Part 2
gwc = games.map { |a| [a, (a[1] & a[0]).count] }

card_count = {}
lines.each_index { |i| card_count[i] = 1 }

gwc.each_with_index do |a, i|
  (1..a[1]).each do |j|
    card_count[i + j] += card_count[i]
  end
end

puts card_count.values.sum # => 5132675
