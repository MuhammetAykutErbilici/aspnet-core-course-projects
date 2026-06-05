namespace MeetingApp.Models
{
    public class MeetingInfo
    {
        public int Id { get; set; }
        public string? Location { get; set; }
        public DateTime Date  { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime EndTime { get; set; }
        public List<string> Participants { get; set; }

    }
}  


