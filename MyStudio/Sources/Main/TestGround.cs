using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGround
{
	public class TestObj
	{
		public TestObj(int attr_) { this.attr = attr_; }
		protected int attr;
		public int Attr
		{
			set { attr = value; }
			get { return attr; }
		}
		public TestObj ReadOnlyClone() { return new TestObj(attr); }
		public static void TestReadOnlyClone()
		{
			TestObj obj = new TestObj(1) { Attr = 2};
			TestObj testObj = obj.ReadOnlyClone();

		}
	}
	public interface INestedClass
	{
		public interface IInnerClass
		{
			public string Name { get; set; }
		}
	}
	public class NestedClass : INestedClass
	{
		
	}
}
