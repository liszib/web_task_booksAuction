using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OnlineAuction.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Lot> BoughtLots { get; set; }
        public List<Lot> SellLots { get; set; }
        
    }
}
