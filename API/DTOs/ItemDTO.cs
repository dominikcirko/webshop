namespace webshopAPI.DTOs
{
    public class ItemDTO
    {
        public int IDItem { get; set; }
        public int ItemCategoryID { get; set; }
        public int? TagID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
    }
}
