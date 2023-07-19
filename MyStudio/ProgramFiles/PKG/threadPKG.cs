using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKG
{
	public static class threadPKG
	{
		public delegate void Actions();

	}

	public class Resource
	{
		private ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
		private int value;

		public int ReadValue()
		{
			lockSlim.EnterReadLock();
			try
			{
				return value;
			}
			finally
			{
				lockSlim.ExitReadLock();
			}
		}

		public void WriteValue(int newValue)
		{
			lockSlim.EnterWriteLock();
			try
			{
				value = newValue;
			}
			finally
			{
				lockSlim.ExitWriteLock();
			}
		}
	}

}
