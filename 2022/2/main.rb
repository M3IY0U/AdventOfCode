# frozen_string_literal: true

lines = File.readlines('input.txt').map(&:split)

score = 0
lines.each do |l|
  score += 3 if (l.first.ord + 23).chr == l.last
  score += 6 if [%w[A Y], %w[B Z], %w[C X]].include? l
  score += l.last.ord - 87
end

p score # => 13924

score = 0
lines.map { |x| [x.first.to_sym, x.last] }.each do |l|
  case l.last
  when 'Z'
    score += 6
    score += { 'A': 2, 'B': 3, 'C': 1 }[l.first]
  when 'Y'
    score += 3
    score += { 'A': 1, 'B': 2, 'C': 3 }[l.first]
  else
    score += { 'A': 3, 'B': 1, 'C': 2 }[l.first]
  end
end

p score # => 13448
