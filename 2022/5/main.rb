# frozen_string_literal: true

x, y = File.read('input.txt').split("\n\n").map { |l| l.split("\n") }
stacks = Array.new(x.pop[-2].to_i + 2) { [] }

# Parsing bs
x.map! { |l| l.sub(' ', '').gsub(']', ' ').gsub('[', ' ').scan(/.{3}/).map!(&:strip) }
x.each { |a| a.each_with_index { |s, i| stacks[i] << s unless s.empty? } }
stacks.reject!(&:empty?)
original_stacks = stacks.map(&:dup)

# Part 1
instructions = y.map do |l|
  i = l.split
  "stacks[#{i.last.to_i - 1}].unshift(stacks[#{i[3].to_i - 1}].shift(#{i[1]}).reverse)
 stacks[#{i.last.to_i - 1}].flatten!"
end

instructions.each { |i| eval(i) }

p stacks.map(&:shift).join # => MQSHJMWNH

# Part 2
stacks = original_stacks

instructions.map { |l| l.gsub('.reverse', '') }.each { |i| eval(i) }

p stacks.map(&:shift).join # => LLWJRBHVZ
