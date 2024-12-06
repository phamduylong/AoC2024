using System.Text.RegularExpressions;
using System.Text;

public class Day5Solution
{
	private List<List<int>> rules = [];
	private List<List<int>> orders = [];
	public Day5Solution()
	{
		Setup();
	}
	public void Setup()
	{
		var lines = File.ReadAllText("Day5/rules.txt").Split("\n").ToList();
		foreach(var line in lines)
		{
			
			var numsFromLine = line.Split(',');
			var nums = new List<int>();
			foreach(var num in numsFromLine)
			{
				nums.Add(Int32.Parse(num));
			}
			rules.Add(nums);
		}

		lines = File.ReadAllText("Day5/order.txt").Split('\n').ToList();
		foreach(var line in lines)
		{
			var tmp = line.Split("|");
			orders.Add([Int32.Parse(tmp[0]), Int32.Parse(tmp[1])]);
		}

	}

	public int Task1()
	{
		int sumOfMiddlePages = 0;
		foreach(var rule in rules)
		{
			if (IsInCorrectOrder(rule))
			{
				sumOfMiddlePages += rule[(rule.Count / 2)];
			}
		}
		return sumOfMiddlePages;
	}

	public int Task2()
	{
		int sumOfMiddlePages = 0;
		foreach (var rule in rules)
		{
			if (!IsInCorrectOrder(rule))
			{
				var correctedRule = FixRuleOrder(rule);
				sumOfMiddlePages += correctedRule[correctedRule.Count / 2];
			}
		}
		return sumOfMiddlePages;
	}

	private bool IsInCorrectOrder(List<int> rule)
	{
		for(int i = 0; i < rule.Count; ++i)
		{
			// only get orders concerning the current number and another one
			var relatedOrders = orders.Where(x => x.Contains(rule[i]) && x.Count(x => rule.IndexOf(x) != -1) == 2);
			
			foreach(var relatedOrder in relatedOrders)
			{
				// If ordered for current number to be first, we check if this rule is correctly applied
				if (rule[i] == relatedOrder[0])
				{
					var tmp = relatedOrder[1];
					if(rule.IndexOf(tmp) < i) { 
						return false; 
					}
				} else if (rule[i] == relatedOrder[1])
				{
					var tmp = relatedOrder[0];
					if(rule.IndexOf(tmp) > i)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	// Iterate back and forth and continuously adding values
	private List<int> FixRuleOrder(List<int> rule)
	{
		var orderedRule = new List<int>();

		var relatedOrders = orders.Where(x => rule.IndexOf(x[0]) != -1 && rule.IndexOf(x[1]) != -1);

		foreach (var r in rule)
		{
			if(orderedRule.Count == 0)
			{
				orderedRule.Add(r);
				continue;
			}
			for (var i = 0; i < orderedRule.Count; i++)
			{
				var relatedOrder = orders.Where(x => x.Contains(r) && x.Contains(orderedRule[i])).ToList().First();
				if (relatedOrder[0] == r)
				{
					orderedRule.Insert(i, r);
					break;
				}
			}
			if(!orderedRule.Contains(r))
			{
				for(var i = orderedRule.Count - 1;  i >= 0; i--)
				{
					var relatedOrder = orders.Where(x => x.Contains(r) && x.Contains(orderedRule[i])).First();
					if (relatedOrder[1] == r)
					{
						orderedRule.Insert(i + 1, r);
						break;
					}
				}
			}
		}

		return orderedRule;
	}
}

