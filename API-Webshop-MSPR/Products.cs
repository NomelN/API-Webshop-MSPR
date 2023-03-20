namespace API_Webshop_MSPR
{
    public class Products
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DetailsProducts Details { get; set; }
        public int Stock { get; set; }
    }
}
