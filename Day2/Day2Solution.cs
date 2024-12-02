public class Day2Solution
{
	public string input = "";
	public List<List<int>> reportRows = new List<List<int>>();

	// Link: https://adventofcode.com/2024/day/2

	public Day2Solution()
	{
		Setup();
	}

	public void Setup()
	{
		input = File.ReadAllText("Day2/input.txt");
		var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
		foreach (var line in lines)
		{
			reportRows.Add(line.Split(' ').Select(x => Convert.ToInt32(x)).ToList());
		}
	}

	public int Task1()
	{
		var safeCount = 0;
		foreach (var report in reportRows)
		{
			if (IsSafe(report))
			{
				safeCount++;
			}
		}

		return safeCount;
	}

	public int Task2()
	{
		var safeCount = 0;
		foreach (var report in reportRows)
		{

			for (int i = 0; i < report.Count; i++)
			{
				var temp = report[i];
				report.RemoveAt(i);
				if (IsSafe(report))
				{
					safeCount++;
					break;
				}
				report.Insert(i, temp);
			}
		}

		return safeCount;
	}

	private static bool IsSafe(List<int> values)
	{
		bool increasing = values[0] < values[1];
		bool safe = true;
		for (int i = 0; i < values.Count - 1; i++)
		{
			if (increasing)
			{
				if (!(values[i] < values[i + 1] && values[i + 1] - values[i] <= 3))
				{
					safe = false;
					break;
				}
			}
			else
			{
				if (!(values[i] > values[i + 1] && values[i] - values[i + 1] <= 3))
				{
					safe = false;
					break;
				}
			}
		}
		return safe;
	}
}