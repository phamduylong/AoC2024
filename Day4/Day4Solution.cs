using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Day4Solution
{
	private string? input;
	private readonly string query = "XMAS";
	private readonly string queryReversed = "SAMX";
	public Day4Solution()
	{
		Setup();
	}
	public void Setup()
	{
		input = File.ReadAllText("Day4/input.txt");
	}
	public int Task1()
	{
		var parts = input.Split(Environment.NewLine).ToList();
		var count = 0;
		// Horizontal lines
		foreach(string part in parts)
		{
			count += Regex.Matches(part, query).Count + Regex.Matches(part, queryReversed).Count;
		}

		// Vertical lines
		foreach (int i in Enumerable.Range(0, parts[0].Length))
		{
			var verticalLine = new StringBuilder();
			foreach (string part in parts)
			{
				verticalLine.Append(part[i]);
			}
			count += Regex.Matches(verticalLine.ToString(), query).Count + Regex.Matches(verticalLine.ToString(), queryReversed).Count;
		}

		// All diagonals
		var allDiagonals = new List<string>();
		// Less than parts.Count - 1 to avoid common biggest diagonal being duplicated
		for (int i = 0; i < parts.Count - 1; ++i)
		{
			var line = new StringBuilder();
			var tmp = i;
			for (int j = 0; j < tmp + 1; ++j)
			{
				i = Math.Abs(tmp - j);
				line.Append(parts[i][j]);
			}
			allDiagonals.Add(line.ToString());
			i = tmp;
		}

		for(int i = parts.Count - 1; i >= 0; --i)
		{
			var line = new StringBuilder();
			var tmp = i;
			for(int j = parts.Count - 1; j >= tmp; --j)
			{
				line.Append(parts[i][j]);
				if (i < parts.Count  - 1) i++;
				else break;
			}
			allDiagonals.Add(line.ToString());
			i = tmp;
		}

		// Start at 1 to avoid the biggest common diagonal being duplicated
		for(int i = 1; i < parts.Count; ++i) {
			var line = new StringBuilder();
			var tmp = i;
			for(int j = parts.Count - 1; j >= tmp; --j)
			{
				i = Math.Abs(tmp - j);
				line.Append(parts[i][j]);
				
			}
			i = tmp;
			allDiagonals.Add((line.ToString()));
		}

		for(int i = parts.Count - 1; i >= 0; --i)
		{
			var line = new StringBuilder();
			var tmp = i;	
			for(int j = 0; j < parts.Count - tmp; ++j)
			{
				line.Append(parts[i][j]);
				if (i < parts.Count - 1) i++;
				else break;
			}
			i = tmp;
			allDiagonals.Add(line.ToString());
		}

		foreach (var line in allDiagonals)
		{
			count += Regex.Matches(line.ToString(), query).Count + Regex.Matches(line.ToString(), queryReversed).Count;
		}

		return count;
	}
}

