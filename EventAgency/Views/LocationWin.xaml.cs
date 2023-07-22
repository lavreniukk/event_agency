using EventAgency.Command;
using EventAgency.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventAgency.Views
{
    /// <summary>
    /// Логика взаимодействия для LocationWin.xaml
    /// </summary>
    public partial class LocationWin : Window
    {
        public Location chosenLocation;

        public LocationWinViewModel locationWinViewModel;
        public Invoker invoker = new Invoker();

        public LocationWin()
        {
            InitializeComponent();
            locationWinViewModel = new LocationWinViewModel();
            this.DataContext = locationWinViewModel;
        }

        private void AddLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Location newLocation = new Location(addressField.Text, cityField.Text, countryField.Text, locationTypeField.Text, Int32.Parse(maxCapacityField.Text));
            invoker.AddCommand(new AddLocationCommand(newLocation, locationWinViewModel.locationRepository));
            invoker.Run();

            UpdateLocationTable();
        }

        private void UpdateLocationButton_Click(object sender, RoutedEventArgs e)
        {
            int id = chosenLocation.locationId;
            chosenLocation = new Location(id, addressField.Text, cityField.Text, countryField.Text, locationTypeField.Text, Int32.Parse(maxCapacityField.Text));
            invoker.AddCommand(new UpdateLocationCommand(chosenLocation, locationWinViewModel.locationRepository));
            invoker.Run();

            UpdateLocationTable();
        }

        private void DeleteLocationButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.AddCommand(new DeleteLocationCommand(chosenLocation.locationId, locationWinViewModel.locationRepository));
            invoker.Run();

            UpdateLocationTable();
        }

        private void UpdateLocationTable()
        {
            locationWinViewModel.Locations = new ObservableCollection<Location>();

            foreach (var location in locationWinViewModel.locationRepository.GetAll().ToList())
            {
                locationWinViewModel.Locations.Add(location);
            }

            LocationData.ItemsSource = locationWinViewModel.Locations;
        }

        private void LocationData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LocationData.SelectedItem != null)
            {
                chosenLocation = (Location)LocationData.SelectedItem;

                addressField.Text = chosenLocation.address;
                cityField.Text = chosenLocation.city;
                countryField.Text = chosenLocation.country;
                locationTypeField.Text = chosenLocation.locationType;
                maxCapacityField.Text = chosenLocation.maxCapacity.ToString();
            }
        }
    }
}
