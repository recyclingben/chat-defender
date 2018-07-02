using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDefenders.Extensions
{
    public static class StringExtensions
    {
		public static string ToCamelCase(this string str) =>
			Char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}
