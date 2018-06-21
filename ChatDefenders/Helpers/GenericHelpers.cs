using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ChatDefenders.Helpers
{
    public static class GenericHelpers
    {
		public static List<PropertyInfo> GetAllPropertiesExcept<T>(params string[] excluded) => 
			typeof(T)
				.GetProperties()
				.Where(_ => !excluded.Contains(_.Name)).ToList();
	}
}
