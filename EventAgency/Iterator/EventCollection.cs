using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.Iterator
{
    class EventCollection : EventEnumerable
    {
        private List<Event> _events = new List<Event>();
        private bool _direction = false;

        public EventCollection(List<Event> events)
        {
            this._events = events;
        }

        public void ReverseDirection()
        {
            _direction = !_direction;
        }

        public List<Event> getItems()
        {
            return _events;
        }

        public override IEnumerator GetEnumerator()
        {
            return new StandartIterator(this);
        }

        public override IEnumerator GetDateEnumerator()
        {
            return new DateIterator(this);
        }
    }
}
