using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInterface
{
	public enum ENoteComposition
	{
		OnlyText, OnlySingleRef, Mixed
	}
	public enum ENoteImportance
	{
		ContextualLabel, EssentialData
	}
	public interface INote: INoteDTO
	{

	}
}
