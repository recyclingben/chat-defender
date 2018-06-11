using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDefenders.Interfaces
{
    public interface ISetable<T>
    {
		void SetTo(T type);
    }
}
