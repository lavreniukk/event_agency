using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.Observer
{
    public interface IObservable
    {
        void Notify();
    }
}
