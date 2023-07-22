using EventAgency.Builder;
using EventAgency.Command;
using EventAgency.Repository;
using EventAgency.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Window
    {
        public EventBuilder eventBuilder = new EventBuilder();
        public LocationRepository locationRepository = new LocationRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");
        public AddEventViewModel addEventViewModel;
        public Invoker invoker = new Invoker();

        public AddEvent()
        {
            InitializeComponent();
            addEventViewModel = new AddEventViewModel();
            this.DataContext = addEventViewModel;
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewEventScenario();
            invoker.AddCommand(new AddEventCommand(eventBuilder.BuildEvent(), addEventViewModel.eventRepository));
            invoker.Run();
        }

        private void EventNameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            eventBuilder.SetEventName(EventNameField.Text);
        }

        private void EventTypeField_TextChanged(object sender, TextChangedEventArgs e)
        {
            eventBuilder.SetEventType(EventTypeField.Text);
        }

        private void EventTimeField_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            eventBuilder.SetEventTime(DateTime.Parse(EventTimeField.Text));
        }

        private void EventLocationIdField_TextChanged(object sender, TextChangedEventArgs e)
        {
            eventBuilder.SetLocation(locationRepository.GetById(Int32.Parse(EventLocationIdField.Text)));
        }

        private void AddNewEventScenario()
        {
            EventScenario eventScenario = new EventScenario(EventLocationPrepField.Text, EventDescrpitionField.Text, EventPlanField.Text);
            invoker.AddCommand(new AddScenarioCommand(eventScenario, addEventViewModel.eventRepository));
            invoker.Run();
            eventScenario = addEventViewModel.eventRepository.GetLastScenrio();
            eventBuilder.SetEventScenario(eventScenario);
        }

        private void EventPriceField_TextChanged(object sender, TextChangedEventArgs e)
        {
            eventBuilder.SetPrice(Decimal.Parse(EventPriceField.Text));
        }

        private void EventCapacityField_TextChanged(object sender, TextChangedEventArgs e)
        {
            eventBuilder.SetCapacity(Int32.Parse(EventCapacityField.Text));
        }
    }
}
