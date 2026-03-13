namespace RealEstate.WebUILayer.DTOs.ProductDetailDTOs
{
    public class CreateProductDetailDTO
    {
        public int ProductId { get; set; }
        public int ProductSize { get; set; }
        public byte ProductBedRoomCount { get; set; }
        public byte ProductBathCount { get; set; }
        public byte ProductRoomCount { get; set; }
        public byte ProductGarageSize { get; set; }
        public string ProductBuildYear { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductLocation { get; set; }
    }
}
