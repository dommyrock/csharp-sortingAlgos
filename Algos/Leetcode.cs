using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgos_csharp.Algos
{
	public static class Leetcode
	{
		#region SingleNumber
		public static int SingleNumber(int[] nums)
		{
			if (nums.Length == 1) return nums[0];
			var dict = new Dictionary<int, int>();
			foreach (int i in nums)
			{
				if (!dict.ContainsKey(i)) dict.Add(i, 1);
				else
					dict[i]++;
			}
			return dict.FirstOrDefault(x => x.Value == 1).Key;

		}
		public static int SingleNumberXOR(int[] nums)
		{
			var result = 0;
			foreach (int i in nums)
			{
				result ^= i;
			}
			return result;
		}
		#endregion
		#region GetConcatenation
		public static int[] GetConcatenation(int[] nums)
		{
			var result = new int[nums.Length * 2];
			for (int i = 0; i < nums.Length; i++)
			{
				result[i] = nums[i];
				result[nums.Length + i] = nums[i];
			}
			return result;
		}

		public static int[] GetConcatenationOneLine(int[] nums)
		{
			int[] result = new int[nums.Length * 2];
			for (int i = 0; i < nums.Length; i++)
				result[i + nums.Length] = result[i] = nums[i];
			return result;
		}
		#endregion
		public static int[] BuildArray(int[] nums)
		{
			//[0,2,1,5,3,4] ->[0,1,2,4,5,3]
			//[5,0,1,2,3,4] ->[4,5,0,1,2,3]
			var result = new int[nums.Length];
			for (int i = 0; i < nums.Length; i++)
			{
				result[i] = nums[nums[i]];
			}
			return result;
		}

		public static int[] RunningSum(int[] nums)
		{
			//[1,2,4] -> [1,3,7] (1,1+2,1+2+4)
			int i = 1;//skip 1st [0]
			while (nums.Length > i)
			{
				nums[i] += nums[i - 1];
				i++;
			}
			return nums;
		}

		public static int MaximumWealth(int[][] accounts)
		{//[[1,5],[7,3],[3,5]] --> 6,10,8 ()
			int max = int.MinValue;
			for (int i = 0; i < accounts.Length; i++)
			{
				int sum = 0;
				for (int j = 0; j < accounts[0].Length; j++)//2x iter per array (for example above) 
				{
					sum += accounts[i][j];//iter 1 accounts[0][0]= 1, iter 2 accounts[0][1]= 5 ,.....[1][0]=7,[1][1]=3,  [2][0]=3,[2][1]=5
				}
				max = Math.Max(sum, max);
			}
			return max;
		}

		public static int FinalValueAfterOperations(string[] operations)
		{
			var dict = new Dictionary<string, int>{
						{"X++",1},
						{"X--",-1},
						{"++X",1},
						{"--X",-1},
				};
			var sum = 0;
			for (int i = 0; i < operations.Length; i++)
			{
				if (dict.ContainsKey(operations[i])) sum += dict[operations[i]];

			}
			return sum;

		}
		/// <summary>
		/// Check if 1st index in each str is + and determines if ++ or --
		/// </summary>
		public static int FinalValueAfterOperationsTrick(string[] operations)
		{
			int val = 0;
			for (int i = 0; i < operations.Length; i++)
			{
				if (operations[i][1] == '+') val++;
				else val--;
			}
			return val;
		}

		public static int MostWordsFound(string[] sentences)
		{// ["vfvvvvvvv fg gfg fg fg"].split(' ').L =5
			var max = int.MinValue;
			for (int i = 0; i < sentences.Length; i++)
			{
				var words = sentences[i].Split(' ').Length;
				max = Math.Max(max, words);
			}
			return max;
		}

		public static string DefangIPaddr(string address)
		{//129.0.0.1 ->129[.]0[.]0[.]1
			return address.Replace(".", "[.]");
		}

		/// <summary>
		/// 2 in one loop
		/// </summary>
		public static int[] Shuffle(int[] nums, int n)
		{
			int[] res = new int[2 * n];
			for (int i = 0, j = n, idx = 0; idx < res.Length; i++, j++)
			{
				res[idx++] = nums[i];//increment after assign once
				res[idx++] = nums[j];//increment 2nd time
			}
			return res;
		}

		public static int NumIdenticalPairs(int[] nums)
		{//Input: nums = [1,2,3,1,1,3] -> Out 4 (pairs) ->(0,3)(0,4)(3,4)(2,5)
		 //[1,1,1,1] -> result 6
			var dict = new Dictionary<int, List<int>>();

			for (int i = 0; i < nums.Length; i++)
			{
				if (!dict.ContainsKey(nums[i]))
					dict.Add(nums[i], new List<int> { i });
				else dict[nums[i]].Add(i);//add index to list
			}
			//proces hashmap
			int result = 0;
			foreach (var number in dict)
			{
				for (int i = 1; i < number.Value.Count; i++)//O(n-1) (when ther is all same numbers)
					result += number.Value.Count - i;
			}
			return result;
		}

		public static IList<bool> KidsWithCandies(int[] candies, int extraCandies)
		{//[2,3,5,1,3] -> [5,6,8,4,6] -> [true,true,true,false,true],[12.1,12] -> true,false,true
			var result = new List<bool>();
			var after = new int[candies.Length];
			for (int i = 0; i < candies.Length; i++)
				after[i] = candies[i] + extraCandies;

			//Sort so we can O(1) check 
			Array.Sort(candies);//O(n log n) Also total TC
			for (int i = 0; i < candies.Length; i++)//O(n)
			{
				//if current > Max && max != itself
				for (int j = candies.Length - 1; j >= 0; j--)
				{
					if (after[i] >= candies[j])//O(1) //because we check only against max val
					{
						result.Add(true);
						break;
					}
					else
					{
						result.Add(false);
						break;
					}
				}
			}
			return result;
		}

		public static int MinPartitions(string n)
		{//
			var result = 0;
			for (var i = 0; i < n.Length; i++)
			{
				result = Math.Max(result, (int)n[i] - '0');//This works because each character is internally represented by a number.
			}
			return result;
		}
		//Take n = 135 as an example,
		//we initilize 5 deci-binary number with lengh = 3,
		//So we have
		//a1 = 111
		//a2 = 011
		//a3 = 011
		//a4 = 001
		//a5 = 001
		// -->n =135, -> 111+11+11+1+1=135- >5 (just return max number)

		//Time O(L)
		//Space O(1)

		#region BitShifting
		/// <summary>
		/// Adds input to N powers,[Right number = how many times left side is x2]
		/// </summary>
		public static int[] ShiftMeLeftDaddy(int input, int[] rightShifts)
		{// 2 <<1 =4, 2<<3=16 (2^2^2^2) 
			for (int i = 0; i < rightShifts.Length; i++)
			{
				rightShifts[i] = input << rightShifts[i];
			}
			return rightShifts;
		}
		/// <summary>
		/// Divides input on left by 2 N times on the right
		/// </summary>
		public static int[] ShiftMeRightDaddy(int input, int[] nTimes)
		{// 8>>3 =1 (8/2/2/2)
			for (int i = 0; i < nTimes.Length; i++)
			{
				nTimes[i] = input >> nTimes[i];
			}
			return nTimes;
		}

		public static int[] PutMeToNPowers(int input, int[] powers)
		{
			var inputTooPowers = new int[powers.Length];
			for (int i = 0; i < powers.Length; i++)
			{
				if (powers[i] == 0 || i < 1)
					inputTooPowers[i] = powers[i];
				else
					inputTooPowers[i] = input << powers[i - 1];
			}
			return inputTooPowers;
		}
		#endregion

		public static int TrappingRainwater(int[] heights)
		{
			//stat is to move from left to center and right to center and keep track of highest points
			int total = 0;
			int current = 0;
			int previouis = heights.Length - 1;

			int leftmax = 0, rightmax = 0;
			while (current <= previouis)
			{
				leftmax = Math.Max(leftmax, heights[current]);
				rightmax = Math.Max(rightmax, heights[previouis]);

				Console.WriteLine($"LeftMax {leftmax}, RightMax {rightmax}");
				if (leftmax < rightmax)
				{
					// leftmax is smaller than rightmax, so the (leftmax-heights[current]) water can be stored
					total += (leftmax - heights[current]);
					current++;
				}
				else
				{
					total += (rightmax - heights[previouis]);
					previouis--;
				}
				Console.WriteLine($"Current {current},Previous {previouis}");
			}
			return total;
		}
		//O N runtime 
		//c++ >https://leetcode.com/problems/trapping-rain-water/discuss/17391/Share-my-short-solution.
		//java https://leetcode.com/problems/trapping-rain-water/discuss/153992/Java-O(n)-time-and-O(1)-space-(with-explanations).
	}

	#region Subrectangle Queries
	public class SubrectangleQueries
	{
		public int[][] result;
		public SubrectangleQueries(int[][] rectangle)
		{
			result = rectangle;
		}

		public void UpdateSubrectangle(int row1, int col1, int row2, int col2, int newValue)
		{
			for (int i = row1; i <= row2; ++i)
			{
				for (int j = col1; j <= col2; ++j)
				{
					result[i][j] = newValue;
				}
			}
		}

		public int GetValue(int row, int col)
		{
			return result[row][col];
		}
		//For each call,

		//Time:
		//updateSubrectangle: O(m* n)
		//getValue: O(1)

		//Space:
		//updateSubrectangle: O(1)
		//getValue: O(1)
		#endregion


	}
}

