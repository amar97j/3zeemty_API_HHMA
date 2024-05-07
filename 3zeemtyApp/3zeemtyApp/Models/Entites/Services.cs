namespace ProductApi.Models.Entites
{
    public class Services
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }    
        public CatererstEntity caterer { get; set; }
        public int catererID { get; set; }

    }
}
