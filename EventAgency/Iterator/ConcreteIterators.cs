using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.Iterator
{
    class StandartIterator : EventIterator
    {
        private EventCollection _events;
        private int _position = -1;

        public StandartIterator(EventCollection events)
        {
            this._events = new EventCollection(events.getItems().OrderBy(e => e.eventId).ToList());
        }

        public override object Current()
        {
            return this._events.getItems()[_position];
        }

        public override int Key()
        {
            return this._position;
        }

        public override bool MoveNext()
        {
            int newPos = this._position + 1;

            if (newPos >= 0 && newPos < this._events.getItems().Count)
            {
                this._position = newPos;
                return true;
            }
            else
                return false;
        }

        public override void Reset()
        {
            this._position = 0;
        }
    }

    class DateIterator : EventIterator
    {
        private EventCollection _events;
        private int _position = -1;
        private bool _reverse = false;

        public DateIterator(EventCollection events, bool reverse = false)
        {
            this._events = new EventCollection(events.getItems().OrderBy(e => e.eventTime).ToList());
            this._reverse = reverse;

            if (reverse)
            {
                this._position = events.getItems().Count;
            }
        }

        public override object Current()
        {
            return this._events.getItems()[_position];
        }

        public override int Key()
        {
            return this._position;
        }

        public override bool MoveNext()
        {
            int newPos = this._position + (this._reverse ? -1 : 1);

            if (newPos >= 0 && newPos < this._events.getItems().Count)
            {
                this._position = newPos;
                return true;
            }
            else
                return false;
        }

        public override void Reset()
        {
            this._position = this._reverse ? this._events.getItems().Count - 1 : 0;
        }
    }
}
