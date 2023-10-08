using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace SharedKernel
{
	public class SharedKernel
	{
	}
	/*
	 * INode
	 * An Instance to do Node-oriented operation
	 *	1. Data accessing
	 *	2. Inter-Node operation
	 */
	public interface INode
	{
		public string ReadNote();
		public string WriteNote(string note);
		public void AddLink(INode node);
	}
	/* I
	 * 
	 */
	public interface ITry<T>
	{
		public T a { get; set; }
		public ITry<T> b { get; set; }
	}
	public class Try<T>:ITry<ITry<T>>
	{
		public ITry<T> a { get; set; }
		public ITry<ITry<T>> b { get; set; }
	}
	public delegate int a(int x);

}
