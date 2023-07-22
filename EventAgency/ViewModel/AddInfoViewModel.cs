using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventAgency.ViewModel
{
    public class AddInfoViewModel
    {
        public ICommand ShowAddWindowCommand { get; set; }
        public ICommand ShowViewWindowCommand { get; set; }


        public AddInfoViewModel()
        {
        }
    }
}
