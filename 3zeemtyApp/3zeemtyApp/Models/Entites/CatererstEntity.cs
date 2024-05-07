namespace ProductApi.Models.Entites
{
    public class CatererstEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }

        public List<Services> serviceProvided { get; set; }
        
    }
}
