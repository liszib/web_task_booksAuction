namespace MVC.Models
{
    public class UserModel
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public string Email { get; set; }

        public string? Password { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<LotModel>? BoughtLots { get; set; }

        public List<LotModel>? SellLots { get; set; }
    }
}
