using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ChatDefenders.Helpers;
using static System.Diagnostics.Debug;

namespace ChatDefenders.Extensions
{
    public static class GenericExtensions
    {
		// Takes two objects and evaluates if all properties are
		// equal, ignoring all properties in the 'excluded' array.
		public static bool PropertiesEqualExclude<T>(this T obj, T comparing, params string[] excluded)
		{
			var propertyTypesNotExcluded = GenericHelpers.GetAllPropertiesExcept<T>(excluded);
			foreach(var propertyType in propertyTypesNotExcluded)
			{
				if (!propertyType.GetValue(obj, null).Equals(propertyType.GetValue(comparing, null)))
					return false;
			}
			return true;
		}
	}
}