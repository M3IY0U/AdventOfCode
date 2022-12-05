# frozen_string_literal: true

x, y = File.read('input.txt').split("\n\n").map { |i| i.split("\n") }
stacks = Array.new(x.pop[-2].to_i + 2) { [] }

# Parsing bs
x.map! { |l| l.sub(' ', '').gsub(']', ' ').gsub('[', ' ').scan(/.{3}/).map!(&:strip) }
x.each { |a| a.each_with_index { |s, i| stacks[i] << s unless s.empty? } }
stacks.reject!(&:empty?)
original_stacks = stacks.map(&:dup)

# Part 1
y.each { |i| stacks[i[-1].to_i - 1].unshift(stacks[i[12..13].to_i - 1].shift(i[5..6].to_i).reverse).flatten! }
p stacks.map(&:shift).join # => MQSHJMWNH

stacks = original_stacks
# Part 2
y.each { |i| stacks[i[-1].to_i - 1].unshift(stacks[i[12..13].to_i - 1].shift(i[5..6].to_i)).flatten! }
p stacks.map(&:shift).join # => LLWJRBHVZ
