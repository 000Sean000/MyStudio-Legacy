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
		public static IServiceCollection Services = new ServiceCollection();
		public static IServiceProvider ServiceProvider = Services.BuildServiceProvider();
	 
	}
}
