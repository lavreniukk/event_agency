using EventAgency.Command;
using EventAgency.Iterator;
using EventAgency.Repository;
using EventAgency.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventAgency.ViewModel
{
    public class ViewEventsViewModel
    {
        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<Guest> Guests { get; set; }
        public ObservableCollection<Participant> Participants { get; set; }

        public static LocationRepository locationRepository = new LocationRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");
        public EventRepository eventRepository = new EventRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;", locationRepository);
        public GuestRepository guestRepository = new GuestRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");
        public ParticipantRepository participantRepository = new ParticipantRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");

        public ICommand ShowGuestWindowCommand { get; set; }
        public ICommand ShowParticipantWindowCommand { get; set; }
        public ICommand ShowAddEventWindowCommand { get; set; }

        public ViewEventsViewModel()
        {
            ShowGuestWindowCommand = new RelayCommand(ShowGuestWindow, CanShowGuestWindow);
            ShowParticipantWindowCommand = new RelayCommand(ShowParticipantWindow, CanShowParticipantWindow);
            ShowAddEventWindowCommand = new RelayCommand(ShowAddEventWindow, CanShowAddEventWindow);

            Events = new ObservableCollection<Event>();
            Guests = new ObservableCollection<Guest>();
            Participants = new ObservableCollection<Participant>();

            EventCollection eventCollection = new EventCollection(eventRepository.GetAll().ToList());
            EventIterator eventIterator = new StandartIterator(eventCollection);

            while (eventIterator.MoveNext())
            {
                Events.Add((Event)eventIterator.Current());
            }

            foreach (var guest in guestRepository.GetAll().ToList())
            {
                Guests.Add(guest);
            }

            foreach (var participant in participantRepository.GetAll().ToList())
            {
                Participants.Add(participant);
            }
        }

        private bool CanShowAddEventWindow(object obj)
        {
            return true;
        }

        private void ShowAddEventWindow(object obj)
        {
            AddEvent addEventWin = new AddEvent();
            addEventWin.Show();
        }

        private bool CanShowParticipantWindow(object obj)
        {
            return true;
        }

        private void ShowParticipantWindow(object obj)
        {
            ParticipantWin participantWin = new ParticipantWin();
            participantWin.Show();
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
    }
}
