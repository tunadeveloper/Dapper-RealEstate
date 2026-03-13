namespace RealEstate.WebUILayer.DTOs.MessageDTOs
{
    public class UpdateMessageDTO
    {
        public int MessageId { get; set; }
        public string MessageNameSurname { get; set; }
        public string MessageEmail { get; set; }
        public string MessageSubject { get; set; }
        public string MessageDetail { get; set; }
    }
}
