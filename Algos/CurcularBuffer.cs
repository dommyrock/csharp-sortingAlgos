using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgos_csharp.Algos
{
	//Blog Explanaition:https://www.baeldung.com/java-ring-buffer
	public class CircularBuffer<E>//AKA Ring BUffer
	{

		private const int DEFAULT_CAPACITY = 8;
		private int capacity;
		private E[] data;

		private volatile int writeSequence, readSequence;

		public CircularBuffer(int capacity)
		{
			this.capacity = (capacity < 1) ? DEFAULT_CAPACITY : capacity;
			this.data = new Object[this.capacity] as E[];
			this.readSequence = 0;
			this.writeSequence = -1;
		}

		public bool Offer(E element)
		{

			if (IsNotFull())
			{

				int nextWriteSeq = writeSequence + 1;
				data[nextWriteSeq % capacity] = element;

				writeSequence++;
				return true;
			}

			return false;
		}

		public E Poll()
		{
			if (IsNotEmpty())
			{

				E nextValue = data[readSequence % capacity];
				readSequence++;
				return nextValue;
			}

			return default;//null
		}
		#region HELPERS
		public int Size()
		{
			return (writeSequence - readSequence) + 1;
		}

		public bool IsEmpty()
		{
			return writeSequence < readSequence;
		}

		public bool IsFull()
		{
			return Size() >= capacity;
		}

		private bool IsNotEmpty()
		{
			return !IsEmpty();
		}

		private bool IsNotFull()
		{
			return !IsFull();
		}
		#endregion
	}
}
