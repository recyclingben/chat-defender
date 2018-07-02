using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using ChatDefenders.Attributes;
using Newtonsoft.Json;
using ChatDefenders.Extensions;

namespace ChatDefenders.Data
{
    public abstract class DbObject
    {
		/* Returns collection of all PropertyInfos marked with
		 * the 'DbObjectData' attribute. */
		[NotMapped]
		public ICollection<PropertyInfo> DTOProperties =>
			GetType()
				.GetProperties()
				.Where(_ => Attribute.IsDefined(_, typeof(DTOIncludeAttribute)))
				.ToList();

		/* Only return properties decorated with the 'DTOInclude' attribute. 
		 * This is to avoid including the auto-implemented lazy loading methods
		 * and/or unneeded properties when sending to client. */
		public Dictionary<string, object> AsDTO
		{
			get
			{
				var result = new Dictionary<string, object>();

				foreach(var prop in DTOProperties)
					result[prop.Name.ToCamelCase()] = prop.GetValue(this);
				return result;
			}
		}

		// Returns JSON-formatted data transfer object.
		public string AsDTOJSON =>
			JsonConvert.SerializeObject(AsDTO);
	}
}