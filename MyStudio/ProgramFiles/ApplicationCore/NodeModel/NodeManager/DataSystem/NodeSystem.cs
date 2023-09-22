using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DomainModel
{
	/*
	 * Using Json Object saving Node data
	 */
	public partial class Node
	{

		#region Json keys
		public const string METADATA = "Metadata";
		public const string ID = "id";
		////public const string SUB_PATH = "Relative path to nodesDir";
		public const string NODE_TYPE = "Node Type";
		public const string NODE_CLASS = "Node Class"; 
		public const string CONTENT = "Content";
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

		public const string CONTENT_TYPE = "Content Type";
		public enum EnumContentType
		{
			Data, Label
		}
		public enum EnumNodeType
		{
			Normal, Citer
		}
		public enum EnumNodeClass
		{
			Template, Module, Group, Database, View, Tag, Layout, Appearance,
		}
		public enum EnumLinkType
		{
			Parent, Component, SubPart, Next, Related, // normal node

			Member, // Group
			DB_Item, DB_Property, // database group

		}

		#endregion
		#region Accessor
		public string Id
		{
			get
			{
				return Get<string>(METADATA, ID);
			}
			set
			{
				Set<string>(value, METADATA, ID);
			}
		}
		public EnumContentType ContentType
		{
			set { Set<string>(value.ToString(), METADATA, CONTENT_TYPE); }
			get { return Get<EnumContentType>(METADATA, CONTENT_TYPE); }
		}
		public string Content
		{
			set { WriteContent(value); }
			get { return ReadContent(); }
		}
		public List<string> ContentTextSet
		{
			set { Set<List<string>>(value, CONTENT, TEXT_SET); }
			get { return Get<List<string>>(CONTENT, TEXT_SET); }
		}

		public JObject Data { get { return _data; } }
		#endregion

	}
	
}