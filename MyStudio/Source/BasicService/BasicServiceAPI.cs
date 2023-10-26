using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BasicService.API
{

	public static class Host
	{
		public static IServiceCollection ServiceCollection = new ServiceCollection();
		public static IServiceProvider ServiceProvider = ServiceCollection.BuildServiceProvider();
	 
	}
}
