namespace RealEstate.WebUILayer.DTOs.ClientDTOs
{
    public class UpdateClientDTO
    {
        public int ClientId { get; set; }
        public string ClientNameSurname { get; set; }
        public string ClientTitle { get; set; }
        public string ClientComment { get; set; }
        public string ClientImageUrl { get; set; }
    }
}
