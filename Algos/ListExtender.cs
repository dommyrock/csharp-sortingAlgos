namespace sortingAlgos_csharp.Algos
{
	public static class ListExtender
	{

		/// <summary>
		/// All about picking random pivot and moving less than and greater than items to separate lists,
		/// then concating them back recursively<br/>
		/// 
		/// Steps<br/>
		/// 1. Pick an element, called a Pivot, from the list.<br/>
		/// 2 .Reorder the list so that all elements with values less than the pivot come before the pivot,
		/// while all elements with values greater than the pivot come after it(equal values can go either way).
		/// After this partitioning, the pivot is in its final position.This is called the partition operation.<br/>
		/// 3. Recursively sort the sub-list of lesser elements and the sub-list of greater elements.
		/// </summary>
		public static List<T> QuickSort<T>(this List<T> list) where T : struct
		{
			Random rand = new Random();
			List<T> less = new List<T>();
			List<T> greater = new List<T>();

			//base case
			if (list.Count <= 1) return list;

			//randomly pick pivot index and remove it from list
			int pos = rand.Next(list.Count);
			var pivot = list[pos];
			list.RemoveAt(pos);

			foreach (var x in list)
			{
				if (IsLessThanOrEqual(x, pivot)) less.Add(x);
				else greater.Add(x);
			}

			//recurse
			return Concat(QuickSort(less), pivot, QuickSort(greater));
		}
		/// <summary>
		/// A Merge Sort is an example of divide and conquer paradigm. It is a comparison based sorting algorithm.
		/// It takes a list and divides the list in two lists of almost equal lengths.
		/// It then sorts the list by applying merge sort recursively, which divides the divided lists into
		/// two sublists for each and applying the merge sort to them as well.<br/>
		/// 
		/// Steps<br/>
		/// 1. If the list is of length 0 or 1, then it is already sorted. Otherwise:<br/>
		/// 2. Divide the unsorted list into two sublists of about half the size.<br/>
		/// 3. Sort each sublist recursively by re-applying merge sort.<br/>
		/// 4. Merge the two sublists back into one sorted list.
		/// </summary>
		public static List<T> MergeSort<T>(this List<T> list) where T : struct
		{
			List<T> left = new List<T>();
			List<T> right = new List<T>();

			if (list.Count <= 1)
				return list;

			int middle = list.Count / 2;
			for (int i = 0; i < middle; i++)
				left.Add(list[i]);
			for (int i = middle; i < list.Count; i++)
				right.Add(list[i]);
			left = MergeSort(left);
			right = MergeSort(right);
			if (IsLessThanOrEqual(left[left.Count - 1], right[0]))
				return left.AppendList(right);

			return Merge(left, right); ;
		}
		private static List<T> Merge<T>(List<T> a, List<T> b) where T : struct
		{
			List<T> s = new List<T>();
			while (a.Count > 0 && b.Count > 0)
			{
				if (IsLessThan(a[0], b[0]))
				{
					s.Add(a[0]);
					a.RemoveAt(0);
				}
				else
				{
					s.Add(b[0]);
					b.RemoveAt(0);
				}
			}

			while (a.Count > 0)
			{
				s.Add(a[0]);
				a.RemoveAt(0);
			}
			return s;
		}
		/// <summary>
		/// The heapSort belongs to the selection sort algorithm paradigm and it is a comparison based algorithm.<br/>
		/// The Heapsort works as a recursive fashion.It makes the heap of the given data and then sorts that heaps.
		/// </summary>
		//public static List<T> HeapSort<T>(this List<T> list)where T:struct need to resolve T to T comparisons
		//{
		//	int i, temp;
		//	for (i = (list.Count / 2) - 1; i >= 0; i--)
		//		SiftDown(list, i, list.Count);
		//	for (i = list.Count - 1; i >= 1; i--)
		//	{
		//		temp = list[0];
		//		list[0] = list[i];
		//		list[i] = temp;
		//		SiftDown(list, 0, i - 1);
		//	}
		//	return list;
		//}

		/// <summary>
		///  This function makes the heaps and sort them.<br/>
		///  The shiftDown ensures that the root of the heap contains the largest element 
		///  then its predecessor. If it is, then its ok otherwise it swap the elements to make it sort
		///  and then sends the result to the heapsort function.
		/// </summary>
		//private static void SiftDown<T>(List<T> numbers,T root,T bottom)
		//{
		//	int done, maxChild, temp;
		//	done = 0;
		//	while ((root * 2 <= bottom) && (done == 0))
		//	{
		//		if (root * 2 == bottom)
		//			maxChild = root * 2;
		//		else if (numbers[root * 2] > numbers[root * 2 + 1])
		//			maxChild = root * 2;
		//		else
		//			maxChild = root * 2 + 1;
		//		if (numbers[root] < numbers[maxChild])
		//		{
		//			temp = numbers[root];
		//			numbers[root] = numbers[maxChild];
		//			numbers[maxChild] = temp;
		//			root = maxChild;
		//		}
		//		else
		//			done = 1;
		//	}
		//}

		/// <summary>
		/// In bubble sort, each element of the unsorted list is compared to the next element 
		/// and if the value of first element is greater than the value of the second element, then they are swapped.<br/>
		/// The same is applied again and again until every element gets compared with the whole list<br/>
		/// -><br/>
		/// It traverses each and every element of the unsorted list in every case. That's why it has the worst case and average case complexity as 0(n2).
		/// </summary>
		public static List<T> BubbleSort<T>(this List<T> list) where T : struct
		{
			T temp;
			// foreach(int i in a)
			for (int i = 1; i <= list.Count; i++) //describes the number of passes
				for (int j = 0; j < list.Count - i; j++) //defineds the number of comparisons.
					if (IsGreaterThan(list[j], list[j + 1]))
					{
						temp = list[j];
						list[j] = list[j + 1];
						list[j + 1] = temp;
					}
			return list;
		}

		#region Helpers
		private static List<T> Concat<T>(List<T> less, T pivot, List<T> greater)
		{
			var sorted = new List<T>(less);
			sorted.Add(pivot);

			sorted.AddRange(greater);
			return sorted;
		}
		private static List<T> AppendList<T>(this List<T> left, List<T> right)
		{
			var sorted = new List<T>(left);
			sorted.AddRange(right);
			return sorted;
		}

		/// <summary>
		/// num > num +1
		/// </summary>
		public static bool IsGreaterThan<T>(T num, T numPlusOne) where T : struct
			=> typeof(T) == typeof(short) ? Convert.ToInt16(num) > Convert.ToInt16(numPlusOne) :
				 typeof(T) == typeof(int) ? Convert.ToInt32(num) > Convert.ToInt32(numPlusOne) :
				 typeof(T) == typeof(long) ? Convert.ToInt64(num) > Convert.ToInt64(numPlusOne) :
				 typeof(T) == typeof(double) ? Convert.ToDouble(num) > Convert.ToDouble(numPlusOne) :
				 typeof(T) == typeof(decimal) ? Convert.ToDecimal(num) > Convert.ToDecimal(numPlusOne) :
				 false;

		/// <summary>
		/// x <= pivot
		/// </summary>
		public static bool IsLessThanOrEqual<T>(T x, T pivot) where T : struct
			=> typeof(T) == typeof(short) ? Convert.ToInt16(x) <= Convert.ToInt16(pivot) :
				 typeof(T) == typeof(int) ? Convert.ToInt32(x) <= Convert.ToInt32(pivot) :
				 typeof(T) == typeof(long) ? Convert.ToInt64(x) <= Convert.ToInt64(pivot) :
				 typeof(T) == typeof(double) ? Convert.ToDouble(x) <= Convert.ToDouble(pivot) :
				 typeof(T) == typeof(decimal) ? Convert.ToDecimal(x) <= Convert.ToDecimal(pivot) :
				 false;

		/// <summary>
		///  a[0] < b[0]
		/// </summary>
		public static bool IsLessThan<T>(T a, T b) where T : struct
			=> typeof(T) == typeof(short) ? Convert.ToInt16(a) < Convert.ToInt16(b) :
				 typeof(T) == typeof(int) ? Convert.ToInt32(a) < Convert.ToInt32(b) :
				 typeof(T) == typeof(long) ? Convert.ToInt64(a) < Convert.ToInt64(b) :
				 typeof(T) == typeof(double) ? Convert.ToDouble(a) < Convert.ToDouble(b) :
				 typeof(T) == typeof(decimal) ? Convert.ToDecimal(a) < Convert.ToDecimal(b) :
				 false;

		private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
		{
				typeof(int),  typeof(double), typeof(decimal),
				typeof(long), typeof(short),  typeof(sbyte),
				typeof(byte), typeof(ulong),  typeof(ushort),
				typeof(uint), typeof(float)
		};
		#endregion
	}
}
