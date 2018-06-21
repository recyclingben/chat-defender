using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using ChatDefenders.Attributes;

namespace ChatDefenders.Data
{
    public abstract class DbObject
    {
		// Returns array of all PropertyInfos marked with
		// the 'DbObjectData' attribute.
		[NotMapped]
		public ICollection<PropertyInfo> DTOProperties =>
			GetType()
				.GetProperties()
				.Where(_ => Attribute.IsDefined(_, typeof(DTOIncludeAttribute)))
				.ToList();
	}
}