using ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomainLayer
{
	public class IdManager:IIdManager
	{
		#region Reference parsing
		public static Regex regexNodeId = new Regex(@"\d{20}");
		public static Regex regexCitedNode = new Regex(@"\(Node\d{20}\)");
		public static Regex regexCitation = new Regex(@"\[\[\(Node\d{20}\)([\s\S]*?)\]\]");
		public static Regex regexCiteForm = new Regex(@"^\[\[\(Node\d{20}\)\]\]$");
		// regular expression of citations, it matches "[[citation]]", where citation can be any character or newline or no character.
		#endregion

		protected IVaultDatabase _vaultDatabase;
		public IVaultDatabase VaultDatabase
		{
			set { _vaultDatabase = value; }
			get { return _vaultDatabase; }
		}
		protected IdBook? _idBoook;
		public class IdBook // a kind of value object
		{
			public List<ulong> releasedIds = new List<ulong>();
			public ulong currentMaxId;
		}
		
		public void LoadVaultIdBook()
		{
			_idBoook =  _vaultDatabase.LoadVaultIdBook();
		}
		public void SaveVaultIdBook()
		{

		}
		public string AquireId()
		{
			string id = "";
			if (_idBoook.releasedIds.Count > 0)
			{

			}

			return id;
		}
		public void ReleaseId(string id)
		{

		}

	}
	public partial interface IVaultDatabase
	{
		public IdManager.IdBook LoadVaultIdBook();
		public void SaveVaultIdBook(IdManager.IdBook idBook);
	}
}
