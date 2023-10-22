using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BasicService
{
	public static class OHS
	{
		public static IServiceProvider ServiceProvider = new ServiceCollection().BuildServiceProvider();
	 
	}
}
