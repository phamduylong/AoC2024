public class Day3Solution
{
	public string input = "";
	public List<List<int>> reportRows = new List<List<int>>();

	// Link: https://adventofcode.com/2024/day/3

	public Day3Solution()
	{
		Setup();
	}

	public void Setup()
	{
		input = File.ReadAllText("Day3/input.txt");
	}

	public int Task1()
	{
		int res = 0;
		var parts = input.Split("mul(");
		foreach (var part in parts)
		{
			var nums = part.Split(")");
			if (nums.Length > 1)
			{
				var n = nums[0].Split(",");
				if (Int32.TryParse(n[0], out int a) && Int32.TryParse(n[1], out int b))
				{
					res += a * b;
				}
			}
		}
		return res;
	}

	public int Task2()
	{
		int res = 0;

		var doParts = input.Split("do()");

		foreach (var doPart in doParts)
		{
			int cutoff = doPart.IndexOf("don't()");
			string relevantPart;
			if (cutoff > 0)
			{
				relevantPart = doPart.Substring(0, cutoff);
			}
			else
			{
				relevantPart = doPart;
			}
			var parts = relevantPart.Split("mul(");
			foreach (var part in parts)
			{
				var nums = part.Split(")");
				if (nums.Length > 1)
				{
					var n = nums[0].Split(",");
					if (Int32.TryParse(n[0], out int a) && Int32.TryParse(n[1], out int b))
					{
						res += a * b;
					}
				}
			}
		}

		return res;
	}
}