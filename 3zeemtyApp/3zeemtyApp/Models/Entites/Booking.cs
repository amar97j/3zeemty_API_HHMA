namespace ProductApi.Models.Entites
{
    public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateOnly DateOnly { get; set; }


        public CatererstEntity CatererService { get; set; }

        public UserAEntity User {  get; set; }

        public bool IsBooked { get; set; }


    }
}
