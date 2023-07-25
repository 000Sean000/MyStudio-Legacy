using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKG
{
	public class LogicPKG
	{
	}

	public class SafeAccessor // To prevent recursive calling between accessors
	{
		protected bool _isSettingValue; // backing field that keeps track of whether the setter is currently being executed.
		protected bool _value;

		public bool Value
		{
			get { return _value; }
			set
			{
				if (!_isSettingValue)
				{
					_isSettingValue = true;
					_value = value;
					// Perform any logic you need to do when the property is set
					_isSettingValue = false;
				}
				// Optionally, you can add an else block to handle what happens if the setter is already being executed.
				// This can be useful for handling potential recursive calls or raising an exception.
			}
		}
	}

}
