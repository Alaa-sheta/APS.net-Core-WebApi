using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.ViewModel
{
    public class Populations
    {

        public int Id { get; set; }
        [ForeignKey("CountryId")]
        public Countries Country { get; set; }
        public int CountryId { get; set; }
        public int year { get; set; }
        public string value { get; set; }
    }
}
