using EventAgency.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.ViewModel
{
    public class ParticipantWinViewModel
    {
        public ObservableCollection<Participant> Participants { get; set; }

        public ParticipantRepository participantRepository = new ParticipantRepository("Server=localhost;Port=5432;Database=myDataBase;User Id=postgres;Password=daedrapr;Database=eventagency;");

        public ParticipantWinViewModel()
        {
            Participants = new ObservableCollection<Participant>();

            foreach (var participant in participantRepository.GetAll().ToList())
            {
                Participants.Add(participant);
            }
        }
    }
}
