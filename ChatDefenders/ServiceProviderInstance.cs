using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDefenders
{
	public static class ServiceProviderInstance
	{
		public static IServiceProvider Instance { get; private set; }
		private static readonly object padlock = new Object();

		public static void RegisterInstance(IServiceProvider value)
		{
				if(Instance == null)
				{
					Instance = value;
				}
				else
				{
					throw new InvalidOperationException("Instance has already been set.");
				}
		}
    }
}
