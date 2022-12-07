# frozen_string_literal: true

# 🌳
class Tree
  attr_accessor :children, :name, :value, :parent

  def initialize(name, parent)
    @name = name
    @value = 0
    @children = []
    @parent = parent
  end

  def collect_values(threshold = nil)
    result = []
    if threshold.nil?
      result << value
    elsif value < threshold
      result << value
    end
    @children.each do |child|
      result += child.collect_values(threshold)
    end
    result
  end
end

master_node = Tree.new('/', nil)
current_node = master_node

File.readlines('input.txt').drop(1).each do |line|
  next if line.start_with?('$ ls')

  if line.start_with?('$ cd')
    if line.start_with?('$ cd ..')
      current_node = current_node.parent
      next
    end

    current_node = current_node.children.find { |child| child.name == line.split[2] }
    next
  end

  if line.start_with?('dir')
    current_node.children << Tree.new(line.split[1],
                                      current_node)
  else
    current_node.value += line.split[0].to_i
  end
end

def also_sum_children(node)
  node.children.each do |child|
    also_sum_children(child)
    node.value += child.value
  end
end

also_sum_children(master_node)

# Part 1
p master_node.collect_values(100_000).sum # => 1077191

# Part 2
p master_node.collect_values.select { |v| v > 30_000_000 - (70_000_000 - master_node.value) }.min # => 5649896
