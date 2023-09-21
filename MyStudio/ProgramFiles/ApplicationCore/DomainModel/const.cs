using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DomainModel
{
	public partial class Node
	{
		public static Type idType = typeof(int);
		#region Reference parsing
		public static Regex regexNodeId = new Regex(@"\d{20}");
		public static Regex regexCitedNode = new Regex(@"\(Node\d{20}\)");
		public static Regex regexCitation = new Regex(@"\[\[\(Node\d{20}\)([\s\S]*?)\]\]");
		public static Regex regexCiteForm = new Regex(@"^\[\[\(Node\d{20}\)\]\]$");
		// regular expression of citations, it matches "[[citation]]", where citation can be any character or newline or no character.
		#endregion
		#region Json keys
		public const string METADATA = "Metadata";
		public const string ID = "id";
		////public const string SUB_PATH = "Relative path to nodesDir";
		public const string NODE_TYPE = "Node Type";
		public const string NODE_CLASS = "Node Class"; 
		public const string CONTENT = "Content";
		public const string TEXT = "Text";
		public const string TEXT_SET = "TextSet";
		public const string REF_ID = "Refered node ids";
		public const string REFERING = "refering to";
		public const string REFERED = "be refered by";
		public const string PROPERTY = "Property";
		public const string LINKING = "linking to";
		public const string LINKED = "be linked by";
		public const string MEDIA = "Media";
		public const string IMAGE = "Image";
		public const string HYPERLINK = "Hyperlink";
		public const string TAG = "Tag";
		public const string USER_DEF = "User-defined";
		public const string ATTRIBUTE = "Attributes";
		public const string LOGIC = "Logic";
		public const string USAGE = "Usage";
		public const string PHASE = "Phase";
		public const string VISUAL = "Visual Format";
		// ...

		#region NodeType
		public enum NodeType
		{
			Data, Citer
		}
		#endregion
		#region NodeClass
		#endregion
		#region LinkType
		public enum LinkType
		{
			PARENT, COMPONENT, SUB_PART, NEXT, RELATED, // normal node
			DB_ITEM, DB_PROPERTY, // database group
		}

		#endregion
		#endregion
	}
}