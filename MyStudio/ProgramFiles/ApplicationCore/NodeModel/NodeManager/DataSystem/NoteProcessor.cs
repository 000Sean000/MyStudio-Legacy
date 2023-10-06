using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NodeModel;
using NodeSupporter;
using PKG;
namespace NodeDataSystem
{
	// deal with reference
	public class ContentParser : INoteProcessor
	{
		#region Reference parsing {refer to Node231}
		public static Regex regexNodeId = new Regex(@"\d+");
		public static Regex regexFullRef = new Regex(@"{Node\d+}");

		#endregion
		protected NodeManager _nodeManager;
		public void BindNodeManager(NodeManager nodeManager)
		{
			_nodeManager = nodeManager;
		}
		public List<string> GetTextSet(string plaintext)
		{
			List<string> textSet = ParsingPKG.SplitByPattern(plaintext, regexFullRef);
			return textSet;
		}
		public string GetPlaintext(string visitingNodeId, List<string> visitedNodeIdSoFar)
		{
			List<string> visitedNodeId = new List<string>(visitedNodeIdSoFar); // avoid passing by reference
			string plaintext = "";
			if (visitedNodeId.Contains(visitingNodeId)) { plaintext = "{Recursion!}"; }
			else
			{
				visitedNodeId.Add(visitingNodeId);
				Node visitingNode = _nodeManager.FetchNode(visitingNodeId);
				List<string> textSet = visitingNode.ContentTextSet;

				foreach (string text in textSet)
				{
					string subPlaintext;
					if (ParsingPKG.GetFirstMatchedSubstring(text, regexFullRef) != null)
					{
						string idToVisit = ParsingPKG.GetFirstMatchedSubstring(text, regexNodeId);
						subPlaintext = GetPlaintext(idToVisit, visitedNodeId);
						
					}
					else
					{
						subPlaintext = text;
					}
					plaintext += subPlaintext;
				}
			}
			return plaintext;
		}
		#region Necessary Implementation
		public void WriteContentToNode(Node node, string plaintext)
		{
			List<string> textSet;
			textSet = GetTextSet(plaintext);
			node.ContentTextSet = textSet;
		}
		public string ReadContentFromNode(Node node)
		{
			string plaintext;
			plaintext = GetPlaintext(node.Id, new List<string>());
			return plaintext;
		}
		public string GetReferenceForm(Node node)
		{
			string referenceForm = $"{{Node{node.Id}}}";
			if (!regexFullRef.IsMatch(referenceForm))
			{
				throw new Exception("interpretation doesn't match reference form, please update code.");
			}
			return referenceForm;
		}

		#endregion
	}
}