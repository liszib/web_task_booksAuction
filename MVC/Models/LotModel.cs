namespace MVC.Models
{
    public class LotModel
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public UserModel? Seller { get; set; }

        public UserModel? Customer { get; set; }
    }
}
