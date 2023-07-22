using EventAgency.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.ViewModel
{
    public class LocationWinViewModel
    {
        public ObservableCollection<Location> Locations { get; set; }

        public LocationRepository locationRepository = new LocationRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");

        public LocationWinViewModel()
        {
            Locations = new ObservableCollection<Location>();

            foreach (var location in locationRepository.GetAll().ToList())
            {
                Locations.Add(location);
            }
        }
    }
}
