using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatDefenders.Data
{
    public static class DataUtilities
    {
		public static T GetContextInstance<T>() where T : DbContext =>
			ServiceProviderInstance.Instance?.GetService<T>();
    }
}