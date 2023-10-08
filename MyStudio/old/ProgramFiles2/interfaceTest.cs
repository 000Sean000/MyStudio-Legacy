using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyStudio.ProgramFiles
{
	public interface IAnimal
	{
		public void Speak();
	}
	public interface ICat: IAnimal
	{
		public void CatClaw();
	}
	public class Animal: IAnimal
	{
		public void Speak()
		{
			Debug.WriteLine("Animal speak");
		}
	}
	public class Cat:Animal,ICat
	{
		public void CatClaw()
		{
			Debug.WriteLine("Cat Claws something");
		}
	}
	public interface IMagicCloneCat: IAnimal
	{
		public void MagicCloneCatClaw();
	}
	public class MagicCloneCat: Animal, IMagicCloneCat 
	{
		public void MagicCloneCatClaw()
		{
			Debug.WriteLine("Magic Clone Cat Claws something");
		}
	}
	public interface IMagicCat: ICat, IMagicCloneCat
	{
		public void CloneCatByMagic();
	}
	public class MagicCat: Cat,MagicCloneCat, IMagicCat
	{
		public void CloneCatByMagic()
		{
			Debug.WriteLine("Use magic cloning a cat magic shadow");
		}
	}
	
}
