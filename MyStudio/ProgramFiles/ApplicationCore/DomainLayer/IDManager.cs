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
		#region Reference parsing {refer to Node231}
		public static Regex regexNodeId = new Regex(@"\d+");
		public static Regex regexReference = new Regex(@"{Refer to Node\d+}");

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
			// ulong: unsigned int64
			public List<ulong> releasedIds = new List<ulong>();
			public ulong currentMaxId;
		}
		
		public void LoadVaultIdBook()
		{
			_idBoook =  _vaultDatabase.LoadVaultIdBook();
		}
		public void SaveVaultIdBook()
		{
			_vaultDatabase.SaveVaultIdBook(_idBoook);
		}
		public string AquireId()
		{
			string id;
			if (_idBoook.releasedIds.Count > 0)
			{
				ulong minId = _idBoook.releasedIds.Min();
				_idBoook.releasedIds.Remove(minId);
				id = minId.ToString();
			}
			else
			{
				id = (++_idBoook.currentMaxId).ToString();
			}

			return id;
		}
		public void ReleaseId(string id)
		{
			ulong intId = ulong.Parse(id);
			if (intId == _idBoook.currentMaxId)
			{
				_idBoook.currentMaxId--;
			}
			else
			{
				_idBoook.releasedIds.Add(intId);
			}
		}

	}
	public partial interface IVaultDatabase
	{
		public IdManager.IdBook LoadVaultIdBook();
		public void SaveVaultIdBook(IdManager.IdBook idBook);
	}
}
