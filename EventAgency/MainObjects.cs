using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAgency.State;
using EventAgency.Observer;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
using System.Windows;

namespace EventAgency
{
    public class Event : IObservable
    {
        public int eventId { get; set; }
        public string eventName { get; set; }
        public string eventType { get; set; }
        public DateTime eventTime { get; set; }
        public Location location { get; set; }
        public EventScenario eventScenario { get; set; }
        public decimal price { get; set; }
        public int capacity { get; set; }
        public List<IObserver> guestList { get; set; }
        public List<Participant> participants { get; set; }
        private IEventState _state;

        public Event(int eventId, string eventName, string eventType, DateTime eventTime, Location location, EventScenario eventScenario, decimal price, int capacity, List<IObserver> guestList, List<Participant> participants)
        {
            this.eventId = eventId;
            this.eventName = eventName;
            this.eventType = eventType;
            this.eventTime = eventTime;
            this.location = location;
            this.eventScenario = eventScenario;
            this.price = price;
            this.capacity = capacity;
            this.guestList = guestList;
            this.participants = participants;
        }

        public Event()
        {
        }

        public Event(string eventType)
        {
            this.eventType = eventType;
        }

        public IEventState State
        {
            get { return _state; }
        }

        public void SetEventState(IEventState state)
        {
            _state = state;
            
        }

        public void Notify()
        {
            foreach(IObserver guest in guestList)
            {
                guest.UpdateObserver(this);
            }
        }
    }

    public class WeddingEvent : Event
    {
        public WeddingEvent() : base(eventType: "Wedding")
        {

        }
    }

    public class BirthdayEvent : Event
    {
        public BirthdayEvent() : base(eventType: "Birthday")
        {

        }
    }

    public class Location
    {
        public int locationId { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string locationType { get; set; }
        public int maxCapacity { get; set; }
        
        public Location(int locationId, string address, string city, string country, string locationType, int maxCapacity)
        {
            this.locationId = locationId;
            this.address = address;
            this.city = city;
            this.country = country;
            this.locationType = locationType;
            this.maxCapacity = maxCapacity;
        }

        public Location(string address, string city, string country, string locationType, int maxCapacity)
        {
            this.address = address;
            this.city = city;
            this.country = country;
            this.locationType = locationType;
            this.maxCapacity = maxCapacity;
        }

        public string GetFullAddress()
        {
            return $"{address}, {city}, {country}";
        }
    }

    public class EventScenario
    {
        public int eventScenarioId { get; set; }
        public string locationPreparation { get; set; }
        public string eventDescription { get; set; }
        public string eventPlan { get; set; }

        public EventScenario(int eventScenarioId, string locationPreparation, string eventDescription, string eventPlan)
        {
            this.eventScenarioId = eventScenarioId;
            this.locationPreparation = locationPreparation;
            this.eventDescription = eventDescription;
            this.eventPlan = eventPlan;
        }

        public EventScenario(string locationPreparation, string eventDescription, string eventPlan)
        {
            this.locationPreparation = locationPreparation;
            this.eventDescription = eventDescription;
            this.eventPlan = eventPlan;
        }
    }

    public class Guest : IObserver
    {
        public int guestId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string phoneNum { get; set; }
        public string email { get; set; }

        public Guest(int guestID, string firstName, string lastName, int age, string phoneNum, string email)
        {
            this.guestId = guestID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.phoneNum = phoneNum;
            this.email = email;
        }

        public Guest(string firstName, string lastName, int age, string phoneNum, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.phoneNum = phoneNum;
            this.email = email;
        }

        public string GetFullGuestInfo()
        {
            return $"{guestId}\n{firstName} {lastName}\n Age: {age}\n Phone: {phoneNum}";
        }

        public void UpdateObserver(Event subscribedEvent)
        {
            using (var client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("lavreniuk.andrey@gmail.com", "qzjxbfbxtdmnmtuo");
                using (var message = new MailMessage(
                    from: new MailAddress("lavreniuk.andrey@gmail.com", "EventCompany"),
                    to: new MailAddress($"{this.email}", $"{this.firstName} {this.lastName}")
                    ))
                {

                    message.Subject = "The event you are registered for has been updated";
                    message.Body = $"{subscribedEvent.eventName} was updated! \n " +
                        $"Event Type: {subscribedEvent.eventType} \n " +
                        $"Date: {subscribedEvent.eventTime} \n " +
                        $"Location: {subscribedEvent.location.GetFullAddress()} \n ";

                    client.Send(message);
                }
            }
        }

        private static void MessageSent(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show(string.Format($"{e.UserState} send canceled"), "Message");
            if (e.Error != null)
                MessageBox.Show(string.Format($"{e.UserState} {e.Error}"), "Message");
            else
                MessageBox.Show("Message has been sent.", "Message");
        }
    }

    public class Participant
    {
        public int participantId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string speciality { get; set; }
        public string phoneNum { get; set; }
        public string email { get; set; }
        public decimal perHourPrice { get; set; }

        public Participant(int participantID, string firstName, string lastName, string speciality, string phoneNum, string email, decimal perHourPrice)
        {
            this.participantId = participantID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.speciality = speciality;
            this.phoneNum = phoneNum;
            this.email = email;
            this.perHourPrice = perHourPrice;
        }

        public Participant(string firstName, string lastName, string speciality, string phoneNum, string email, decimal perHourPrice)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.speciality = speciality;
            this.phoneNum = phoneNum;
            this.email = email;
            this.perHourPrice = perHourPrice;
        }

        public string GetFullParticipantInfo()
        {
            return $"{participantId}\n {firstName} {lastName}\n Speciality: {speciality} \n Phone: {phoneNum}";
        }
    }
}
