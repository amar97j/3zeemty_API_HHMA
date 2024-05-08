namespace _3zeemtyApp.Models.Responses
{
    public class BookingResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }

        public string CatererService { get; set; }
    }

}
