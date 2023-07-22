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
    /// Логика взаимодействия для ParticipantWin.xaml
    /// </summary>
    public partial class ParticipantWin : Window
    {
        public Participant chosenParticipant;

        public ParticipantWinViewModel participantWinViewModel;
        public Invoker invoker = new Invoker();

        public ParticipantWin()
        {
            InitializeComponent();
            participantWinViewModel = new ParticipantWinViewModel();
            this.DataContext = participantWinViewModel;
        }

        private void ParticipantData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParticipantData.SelectedItem != null)
            {
                chosenParticipant = (Participant)ParticipantData.SelectedItem;

                firstNameField.Text = chosenParticipant.firstName;
                lastNameField.Text = chosenParticipant.lastName;
                ageField.Text = chosenParticipant.speciality;
                phoneNumField.Text = chosenParticipant.phoneNum;
                emailField.Text = chosenParticipant.email;
                priceField.Text = chosenParticipant.perHourPrice.ToString();
            }
        }

        private void UpdateParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            int id = chosenParticipant.participantId;
            chosenParticipant = new Participant(id, firstNameField.Text, lastNameField.Text, ageField.Text, phoneNumField.Text, emailField.Text, Decimal.Parse(priceField.Text));
            invoker.AddCommand(new UpdateParticipantCommand(chosenParticipant, participantWinViewModel.participantRepository));
            invoker.Run();

            UpdateParticipantTable();
        }

        private void DeleteParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.AddCommand(new DeleteParticipantCommand(chosenParticipant.participantId, participantWinViewModel.participantRepository));
            invoker.Run();

            UpdateParticipantTable();
        }

        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            Participant newParticipant = new Participant(firstNameField.Text, lastNameField.Text, ageField.Text, phoneNumField.Text, emailField.Text, Decimal.Parse(priceField.Text));
            invoker.AddCommand(new AddParticipantCommand(newParticipant, participantWinViewModel.participantRepository));
            invoker.Run();

            UpdateParticipantTable();
        }

        private void UpdateParticipantTable()
        {
            participantWinViewModel.Participants = new ObservableCollection<Participant>();

            foreach (var participant in participantWinViewModel.participantRepository.GetAll().ToList())
            {
                participantWinViewModel.Participants.Add(participant);
            }

            ParticipantData.ItemsSource = participantWinViewModel.Participants;
        }
    }
}
