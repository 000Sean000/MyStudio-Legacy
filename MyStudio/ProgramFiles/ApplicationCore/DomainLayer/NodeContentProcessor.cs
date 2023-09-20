using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DomainModel;
using PKG;
namespace DomainLayer
{
	public class NodeContentProcessor:INodeContentProcessor
	{
		#region Reference parsing
		public static Regex regexNodeId = new Regex(@"\d{20}");
		public static Regex regexCitedNode = new Regex(@"\(Node\d{20}\)");
		public static Regex regexCitation = new Regex(@"\[\[\(Node\d{20}\)([\s\S]*?)\]\]");
		public static Regex regexCiteForm = new Regex(@"^\[\[\(Node\d{20}\)\]\]$");
		// regular expression of citations, it matches "[[citation]]", where citation can be any character or newline or no character.
		#endregion
		public List<string> PlaintextextToTextSet(string text)
		{
			return new List<string>();
		}
		public string TextSetToPlaintext(string visitingNodeId, List<string> visitedNodeId)
		{
			string plaintext = "";
			if (visitedNodeId.Contains(visitingNodeId)) { plaintext = "< a recursive visit! >"; }
			else
			{
				visitedNodeId.Add(visitingNodeId);
				Node visitingNode = new Node(visitingNodeId);
				List<string> texts = visitingNode.Get<List<string>>(Node.CONTENT, Node.TEXT);
				
				foreach (string text in texts)
				{

				}
			}
			return plaintext;
		}
		public void WriteContentToNode(Node baseNode, string text)
		{

		}
		public string ReadContentFromNode(Node baseNode)
		{
			return "";
		}
		public void InsertReference(Node baseNode, Node referedNode)
		{

		}
	}
}
