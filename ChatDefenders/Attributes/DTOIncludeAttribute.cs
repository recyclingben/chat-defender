using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDefenders.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
    public class DTOIncludeAttribute : Attribute { }
}