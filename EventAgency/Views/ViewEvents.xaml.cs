using EventAgency.Builder;
using EventAgency.Command;
using EventAgency.Decorator;
using EventAgency.Iterator;
using EventAgency.Repository;
using EventAgency.State;
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
    /// Логика взаимодействия для ViewEvents.xaml
    /// </summary>
    public partial class ViewEvents : Window
    {
        public Event chosenEvent;
        public Guest chosenGuest;
        public Participant chosenParticipant;

        public ViewEventsViewModel viewEventsViewModel;
        public Invoker invoker = new Invoker();

        public ViewEvents()
        {
            InitializeComponent();
            viewEventsViewModel = new ViewEventsViewModel();
            this.DataContext = viewEventsViewModel;
        }

        private void EventData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventData.SelectedItem != null)
            {
                chosenEvent = (Event)EventData.SelectedItem;

                EventNameField.Text = chosenEvent.eventName;
                EventTypeField.Text = chosenEvent.eventType;
                EventTimeField.Text = chosenEvent.eventTime.ToString();
                EventLocationIdField.Text = chosenEvent.location.locationId.ToString();
                EventLocationPrepField.Text = chosenEvent.eventScenario.locationPreparation;
                EventDescrpitionField.Text = chosenEvent.eventScenario.eventDescription;
                EventPlanField.Text = chosenEvent.eventScenario.eventPlan;
                EventPriceField.Text = chosenEvent.price.ToString();
                EventCapacityField.Text = chosenEvent.capacity.ToString();

                switch (chosenEvent.State.GetType().Name)
                {
                    case "PlanningState":
                        EventStateField.Text = "planning";
                        break;
                    case "CancelledState":
                        EventStateField.Text = "cancelled";
                        break;
                    case "FinishedState":
                        EventStateField.Text = "finished";
                        break;
                    case "ApprovedState":
                        EventStateField.Text = "approved";
                        break;
                }

                GuestData.ItemsSource = chosenEvent.guestList;
                ParticipantData.ItemsSource = chosenEvent.participants;
            }
        }

        private void UpdateEventButton_Click(object sender, RoutedEventArgs e)
        {
            EventScenario newEventScenario = new EventScenario(chosenEvent.eventScenario.eventScenarioId, EventLocationIdField.Text, EventLocationPrepField.Text, EventDescrpitionField.Text);

            LocationRepository locationRepository = new LocationRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");

            chosenEvent = new EventBuilder().SetEventID(chosenEvent.eventId)
                                            .SetEventName(EventNameField.Text)
                                            .SetEventType(EventNameField.Text)
                                            .SetEventTime(DateTime.Parse(EventTimeField.Text))
                                            .SetLocation(locationRepository.GetById(Int32.Parse(EventLocationIdField.Text)))
                                            .SetEventScenario(newEventScenario)
                                            .SetPrice(Decimal.Parse(EventPriceField.Text))
                                            .SetCapacity(Int32.Parse(EventCapacityField.Text))
                                            .SetGuestList(chosenEvent.guestList)
                                            .SetParticipants(chosenEvent.participants)
                                            .BuildEvent();

            if (FloristCheck.IsChecked == true)
                chosenEvent = new Florist(chosenEvent);

            if (DeliveryCheck.IsChecked == true)
                chosenEvent = new Delivery(chosenEvent);

            switch (EventStateField.Text)
            {
                case "planning":
                    chosenEvent.SetEventState(new PlanningState());
                    break;
                case "cancelled":
                    chosenEvent.SetEventState(new CancelledState());
                    break;
                case "finished":
                    chosenEvent.SetEventState(new FinishedState());
                    break;
                case "approved":
                    chosenEvent.SetEventState(new ApprovedState());
                    break;
                default:
                    chosenEvent.SetEventState(new ApprovedState());
                    break;
            }
            viewEventsViewModel.eventRepository.ChangeState(chosenEvent);

            invoker.AddCommand(new UpdateEventCommand(chosenEvent, viewEventsViewModel.eventRepository));
            invoker.AddCommand(new UpdateScenarioCommand(newEventScenario, viewEventsViewModel.eventRepository));
            invoker.Run();

            UpdateEventDataTable();
            /*viewEventsViewModel.Events = new ObservableCollection<Event>();

            EventCollection eventCollection = new EventCollection(viewEventsViewModel.eventRepository.GetAll().ToList());
            EventIterator eventIterator = new StandartIterator(eventCollection);

            while (eventIterator.MoveNext())
            {
                viewEventsViewModel.Events.Add((Event)eventIterator.Current());
            }

            EventData.ItemsSource = viewEventsViewModel.Events;*/
        }

        private void GuestData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllGuestData.SelectedItem != null)
            {
                chosenGuest = (Guest)AllGuestData.SelectedItem;
                GuestIdField.Text = chosenGuest.guestId.ToString();
                GuestFNField.Text = chosenGuest.firstName;
                GuestLNField.Text = chosenGuest.lastName;
            }
        }

        private void ParticipantData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllParticipantData.SelectedItem != null)
            {
                chosenParticipant = (Participant)AllParticipantData.SelectedItem;
                ParticipantIdField.Text = chosenParticipant.participantId.ToString();
                ParticipantFNField.Text = chosenParticipant.firstName;
                ParticipantLNField.Text = chosenParticipant.lastName;
            }
        }

        private void AssignGuest_Click(object sender, RoutedEventArgs e)
        {
            invoker.AddCommand(new AssignGuestToEventCommand(chosenEvent, chosenGuest, GuestStatusField.Text, viewEventsViewModel.guestRepository));
            invoker.Run();

            UpdateEventDataTable();
            /*viewEventsViewModel.Events = new ObservableCollection<Event>();

            EventCollection eventCollection = new EventCollection(viewEventsViewModel.eventRepository.GetAll().ToList());
            EventIterator eventIterator = new StandartIterator(eventCollection);

            while (eventIterator.MoveNext())
            {
                viewEventsViewModel.Events.Add((Event)eventIterator.Current());
            }

            EventData.ItemsSource = viewEventsViewModel.Events;*/
            GuestData.ItemsSource = viewEventsViewModel.eventRepository.GetEventGuests(chosenEvent.eventId);
        }

        private void AssignParticipant_Click(object sender, RoutedEventArgs e)
        {
            invoker.AddCommand(new AssignParticipantToEventCommand(chosenEvent, chosenParticipant, Int32.Parse(WorkHoursField.Text), viewEventsViewModel.participantRepository));
            invoker.Run();

            UpdateEventDataTable();
            /*viewEventsViewModel.Events = new ObservableCollection<Event>();

            EventCollection eventCollection = new EventCollection(viewEventsViewModel.eventRepository.GetAll().ToList());
            EventIterator eventIterator = new StandartIterator(eventCollection);

            while (eventIterator.MoveNext())
            {
                viewEventsViewModel.Events.Add((Event)eventIterator.Current());
            }

            EventData.ItemsSource = viewEventsViewModel.Events;*/
            ParticipantData.ItemsSource = viewEventsViewModel.eventRepository.GetEventParticipants(chosenEvent.eventId);
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.AddCommand(new DeleteEventCommand(chosenEvent.eventId, viewEventsViewModel.eventRepository));
            invoker.Run();

            UpdateEventDataTable();
            /*viewEventsViewModel.Events = new ObservableCollection<Event>();

            EventCollection eventCollection = new EventCollection(viewEventsViewModel.eventRepository.GetAll().ToList());
            EventIterator eventIterator = new StandartIterator(eventCollection);

            while (eventIterator.MoveNext())
            {
                viewEventsViewModel.Events.Add((Event)eventIterator.Current());
            }

            EventData.ItemsSource = viewEventsViewModel.Events;*/
            ParticipantData.ItemsSource = viewEventsViewModel.eventRepository.GetEventParticipants(chosenEvent.eventId);
        }

        private void ChangeEventState_Click(object sender, RoutedEventArgs e)
        {
            switch (EventStateField.Text)
            {
                case "planning":
                    chosenEvent.SetEventState(new PlanningState());
                    break;
                case "cancelled":
                    chosenEvent.SetEventState(new CancelledState());
                    break;
                case "finished":
                    chosenEvent.SetEventState(new FinishedState());
                    break;
                case "approved":
                    chosenEvent.SetEventState(new ApprovedState());
                    break;
                default:
                    chosenEvent.SetEventState(new ApprovedState());
                    break;
            }

            viewEventsViewModel.eventRepository.ChangeState(chosenEvent);
        }

        private void IteratorChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((TextBlock)IteratorChoice.SelectedItem).Text == "Standard")
            {
                viewEventsViewModel.Events = new ObservableCollection<Event>();

                EventCollection eventCollection = new EventCollection(viewEventsViewModel.eventRepository.GetAll().ToList());
                EventIterator eventIterator = new StandartIterator(eventCollection);

                while (eventIterator.MoveNext())
                {
                    viewEventsViewModel.Events.Add((Event)eventIterator.Current());
                }

                EventData.ItemsSource = viewEventsViewModel.Events;
            }
            else 
            {
                viewEventsViewModel.Events = new ObservableCollection<Event>();

                EventCollection eventCollection = new EventCollection(viewEventsViewModel.eventRepository.GetAll().ToList());
                EventIterator eventIterator = new DateIterator(eventCollection);

                while (eventIterator.MoveNext())
                {
                    viewEventsViewModel.Events.Add((Event)eventIterator.Current());
                }

                EventData.ItemsSource = viewEventsViewModel.Events;
            }
        }

        private void UpdateEventDataTable()
        {
            viewEventsViewModel.Events = new ObservableCollection<Event>();

            EventCollection eventCollection = new EventCollection(viewEventsViewModel.eventRepository.GetAll().ToList());
            EventIterator eventIterator = new StandartIterator(eventCollection);

            while (eventIterator.MoveNext())
            {
                viewEventsViewModel.Events.Add((Event)eventIterator.Current());
            }

            EventData.ItemsSource = viewEventsViewModel.Events;
        }
    }
}
