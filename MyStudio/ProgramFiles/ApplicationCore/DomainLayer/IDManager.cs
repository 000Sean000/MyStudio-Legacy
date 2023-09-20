using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomainLayer
{
	public static class IDManager
	{
		#region Reference parsing
		public static Regex regexNodeId = new Regex(@"\d{20}");
		public static Regex regexCitedNode = new Regex(@"\(Node\d{20}\)");
		public static Regex regexCitation = new Regex(@"\[\[\(Node\d{20}\)([\s\S]*?)\]\]");
		public static Regex regexCiteForm = new Regex(@"^\[\[\(Node\d{20}\)\]\]$");
		// regular expression of citations, it matches "[[citation]]", where citation can be any character or newline or no character.
		#endregion


	}
}
