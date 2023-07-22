using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.Iterator
{
    abstract class EventIterator : IEnumerator
    {
        object IEnumerator.Current => Current();
        public abstract int Key();
        public abstract bool MoveNext();
        public abstract object Current();
        public abstract void Reset();
    }
}
