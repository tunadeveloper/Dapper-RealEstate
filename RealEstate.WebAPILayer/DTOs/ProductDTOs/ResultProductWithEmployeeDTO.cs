namespace RealEstate.WebAPILayer.DTOs.ProductDTOs
{
    public class ResultProductWithEmployeeDTO
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductCoverImage { get; set; }
        public string ProductCity { get; set; }
        public string ProductDistrict { get; set; }
        public string ProductAddress { get; set; }
        public string ProductDescription { get; set; }
        public bool ProductIsPopular { get; set; }
        public string EmployeeNameSurname { get; set; }
    }
}
