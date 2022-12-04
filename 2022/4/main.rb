# frozen_string_literal: true

lines = File.readlines('input.txt').map(&:chomp).map { |l| l.gsub('-', '..').split(',') }

p lines.map { |r| eval("(#{r.first}).cover?(#{r.last}) || (#{r.last}).cover?(#{r.first})") }.count(true)

p lines.map { |r| eval("(#{r.first}).cover?((#{r.last}).first) || (#{r.last}).cover?((#{r.first}).first)") }.count(true)
