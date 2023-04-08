namespace OnlineAuction.Entities
{
    public class Lot
    {
        private List<string> _bookGenres = new List<string>{ "Детектив", "Фантастика", "Приключения", "Роман", "Научная книга", "Фольклор(эпос, легенды, сказки)",
        "Юмор(анекдоты, союз)","Справочная книга", "Поэзия", "Детская книга", "Документальная литература", "Деловая литература",
        "Религиозная литература", "Учебная книга", "Книга о психологии", "Хобби, досуг(отдых, туризм, домашние животные, домоводство)",
        "Техника(авто, бытовая техника, компьютер)"};

        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public User Seller { get; set; }

        public User Customer { get; set; }

    }
}
