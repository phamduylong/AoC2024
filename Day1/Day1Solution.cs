public class Day1Solution
{
	public string input = "";

	// Link: https://adventofcode.com/2024/day/1 

	private List<int> input1 = new List<int>();
	private List<int> input2 = new List<int>();

	public Day1Solution()
	{
		Setup();
	}

	public void Setup()
	{
		input = File.ReadAllText("Day1/input.txt");
		var lines = input.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);

		foreach (var line in lines)
		{
			var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
			input1.Add(int.Parse(parts[0]));
			input2.Add(int.Parse(parts[1]));
		}
	}

	public int Task1()
	{
		input1.Sort();
		input2.Sort();

		var diff = 0;
		for (int i = 0; i < input1.Count; i++)
		{
			diff += Math.Abs(input1[i] - input2[i]);
		}

		return diff;
	}

	public int Task2()
	{
		var similarityScore = 0;
		foreach (var i in input1.Distinct())
		{
			similarityScore += input2.Count(x => x == i) * i;
		}

		return similarityScore;
	}
}