using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductApi.Models.Requests
{
    public class AddCaterRequest
    {
        [Required]
        public string Name { get; set; }
      
        public string Type { get; set; }
        public string Description { get; set; }






    }




}
