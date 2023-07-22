using EventAgency.Command;
using EventAgency.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventAgency.ViewModel
{
    public class MainMenuViewModel
    {
        public ICommand ShowViewEventsWindowCommand { get; set; }
        public ICommand ShowLocationWindowCommand { get; set; }
        public ICommand ShowParticipantWindowCommand { get; set; }
        public ICommand ShowGuestWindowCommand { get; set; }



        public MainMenuViewModel()
        {
            ShowViewEventsWindowCommand = new RelayCommand(ShowViewEventsWindow, CanShowViewEventsWindowCommand);
            ShowLocationWindowCommand = new RelayCommand(ShowLocationWindow, CanShowLocationWindowCommand);
            ShowParticipantWindowCommand = new RelayCommand(ShowParticipantWindow, CanShowParticipantWindow);
            ShowGuestWindowCommand = new RelayCommand(ShowGuestWindow, CanShowGuestWindow);
        }

        private bool CanShowParticipantWindow(object obj)
        {
            return true;
        }

        private bool CanShowLocationWindowCommand(object obj)
        {
            return true;
        }

        private bool CanShowViewEventsWindowCommand(object obj)
        {
            return true;
        }

        private bool CanShowGuestWindow(object obj)
        {
            return true;
        }

        private void ShowGuestWindow(object obj)
        {
            GuestWin guestWin = new GuestWin();
            guestWin.Show();
        }

        private void ShowParticipantWindow(object obj)
        {
            ParticipantWin participantWin = new ParticipantWin();
            participantWin.Show();
        }

        private void ShowLocationWindow(object obj)
        {
            LocationWin locationWin = new LocationWin();
            locationWin.Show();
        }

        private void ShowViewEventsWindow(object obj)
        {
            ViewEvents viewEvents = new ViewEvents();
            viewEvents.Show();
        }
    }
}
