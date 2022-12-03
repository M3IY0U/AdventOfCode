# frozen_string_literal: true

lines = File.readlines('input.txt').map(&:chomp)

# 🦧
class String
  def prio
    /[[:upper:]]/.match(self) ? ord - 38 : ord - 96
  end
end

# Part 1
p lines.map { |s| s.partition(/.{#{s.size / 2}}/)[1, 2] }.map { |a| a.first.chars & a.last.chars }.flatten.map(&:prio).sum # => 8493

# Part 2
p lines.map(&:chars).each_slice(3).map { |a| a[0] & a[1] & a[2] }.flatten.map(&:prio).sum # => 2552
