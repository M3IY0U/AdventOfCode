require "benchmark"
s = File.read("input.txt")

Benchmark.ips do |x|
  x.report("Part 1") do
    (s.chars.each_cons(4).to_a.index { |a| a.uniq.size == 4 } || 0) + 4
  end

  x.report("Part 2") do
    (s.chars.each_cons(14).to_a.index { |a| a.uniq.size == 14 } || 0) + 14
  end
end
