namespace RealEstate.WebUILayer.DTOs.ProductDTOs
{
    public class ResultPopularProductDTO
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductCoverImage { get; set; }
        public string ProductCity { get; set; }
        public string ProductDistrict { get; set; }
        public string ProductAddress { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryName { get; set; }
        public string EmployeeNameSurname { get; set; }
        public bool ProductIsPopular { get; set; }
        public int ProductSize { get; set; }
        public byte ProductBedRoomCount { get; set; }
        public byte ProductBathCount { get; set; }
        public byte ProductRoomCount { get; set; }
        public byte ProductGarageSize { get; set; }
        public string ProductBuildYear { get; set; }
        public string ProductLocation { get; set; }
    }
}
