using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.ViewModel
{
    public class Countries
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ISO3 { get; set; }
        public List<Populations> Populations { get; set; }
    }
}
