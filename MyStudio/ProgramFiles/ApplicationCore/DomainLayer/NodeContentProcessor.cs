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
	// deal with reference
	public class NodeContentProcessor:INodeContentProcessor
	{
		
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
