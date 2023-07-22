using EventAgency.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.ViewModel
{
    public class GuestWinViewModel
    {
        public ObservableCollection<Guest> Guests { get; set; }

        public GuestRepository guestRepository = new GuestRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");

        public GuestWinViewModel()
        {
            Guests = new ObservableCollection<Guest>();

            foreach (var guest in guestRepository.GetAll().ToList())
            {
                Guests.Add(guest);
            }
        }
    }
}
