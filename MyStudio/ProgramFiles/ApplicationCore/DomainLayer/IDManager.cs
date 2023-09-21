///using ApplicationLayer;
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
		

		protected string? _vaultPath;
		protected IVaultDatabase _vaultDatabase;
		public IVaultDatabase VaultDatabase
		{
			set { _vaultDatabase = value; }
			get { return _vaultDatabase; }
		}
		protected IdBook? _idBoook;
		
		
		#region Necessary Implementation
		public void BindVault(string vaultPath)
		{
			_vaultPath = vaultPath;
		}
		public string AcquireId()
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
		#endregion

		public void LoadVaultIdBook()
		{
			_idBoook = _vaultDatabase.LoadVaultIdBook(_vaultPath);
		}
		public void SaveVaultIdBook()
		{
			_vaultDatabase.SaveVaultIdBook(_vaultPath, _idBoook);
		}
	}
	public class IdBook //  value object for port transfer
	{
		// ulong: unsigned int64
		public List<ulong> releasedIds = new List<ulong>();
		public ulong currentMaxId;
	}
	public partial interface IVaultDatabase
	{
		public IdBook LoadVaultIdBook(string valutPath);
		public void SaveVaultIdBook(string vaultPath, IdBook idBook);
	}
}
