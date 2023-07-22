using EventAgency.Command;
using EventAgency.Repository;
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
    /// Логика взаимодействия для GuestWin.xaml
    /// </summary>
    public partial class GuestWin : Window
    {
        public Guest chosenGuest;

        public GuestWinViewModel guestWinViewModel;
        public Invoker invoker = new Invoker();

        public GuestWin()
        {
            InitializeComponent();
            guestWinViewModel = new GuestWinViewModel();
            this.DataContext = guestWinViewModel;
        }

        private void DeleteGuestButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.AddCommand(new DeleteGuestCommand(chosenGuest.guestId, guestWinViewModel.guestRepository));
            invoker.Run();

            UpdateGuestTable();
            /*guestWinViewModel.Guests = new ObservableCollection<Guest>();

            foreach (var guest in guestWinViewModel.guestRepository.GetAll().ToList())
            {
                guestWinViewModel.Guests.Add(guest);
            }

            GuestData.ItemsSource = guestWinViewModel.Guests;*/
        }

        private void UpdateGuestButton_Click(object sender, RoutedEventArgs e)
        {
            int id = chosenGuest.guestId;
            chosenGuest = new Guest(id, firstNameField.Text, lastNameField.Text, Int32.Parse(ageField.Text), phoneNumField.Text, emailField.Text);
            invoker.AddCommand(new UpdateGuestCommand(chosenGuest, guestWinViewModel.guestRepository));
            invoker.Run();

            UpdateGuestTable();
            /*guestWinViewModel.Guests = new ObservableCollection<Guest>();

            foreach (var guest in guestWinViewModel.guestRepository.GetAll().ToList())
            {
                guestWinViewModel.Guests.Add(guest);
            }

            GuestData.ItemsSource = guestWinViewModel.Guests;*/
        }

        private void AddGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest newGuest = new Guest(firstNameField.Text, lastNameField.Text, Int32.Parse(ageField.Text), phoneNumField.Text, emailField.Text);
            invoker.AddCommand(new AddGuestCommand(newGuest, guestWinViewModel.guestRepository));
            invoker.Run();

            UpdateGuestTable();
            /*guestWinViewModel.Guests = new ObservableCollection<Guest>();

            foreach(var guest in guestWinViewModel.guestRepository.GetAll().ToList())
            {
                guestWinViewModel.Guests.Add(guest);
            }

            GuestData.ItemsSource = guestWinViewModel.Guests;*/
        }

        private void UpdateGuestTable()
        {
            guestWinViewModel.Guests = new ObservableCollection<Guest>();

            foreach (var guest in guestWinViewModel.guestRepository.GetAll().ToList())
            {
                guestWinViewModel.Guests.Add(guest);
            }

            GuestData.ItemsSource = guestWinViewModel.Guests;
        }

        private void GuestData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GuestData.SelectedItem != null)
            {
                chosenGuest = (Guest)GuestData.SelectedItem;

                firstNameField.Text = chosenGuest.firstName;
                lastNameField.Text = chosenGuest.lastName;
                ageField.Text = chosenGuest.age.ToString();
                phoneNumField.Text = chosenGuest.phoneNum;
                emailField.Text = chosenGuest.email;
            }
        }
    }
}
