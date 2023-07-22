using EventAgency.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventAgency.ViewModel
{
    public class AddEventViewModel
    {
        public ObservableCollection<Location> Locations { get; set; }

        public static LocationRepository locationRepository = new LocationRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");
        public EventRepository eventRepository = new EventRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;", locationRepository);

        public AddEventViewModel()
        {
            Locations = new ObservableCollection<Location>();

            foreach (var location in locationRepository.GetAll().ToList())
            {
                Locations.Add(location);
            }
        }
    }
}
