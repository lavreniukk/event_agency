using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.Iterator
{
    abstract class EventEnumerable : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
        public abstract IEnumerator GetDateEnumerator();
    }
}
