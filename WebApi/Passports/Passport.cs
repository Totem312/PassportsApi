using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Passports
{
    [Table("passports")]
    
    public class Passport
    {
        [Key]
        public string Id { get; set; } = "";
        [Column("series")]
        public int Series { get; set; }
        [Column("number")]
        public int Number { get; set; }
        [Column("status")]
        public bool Status { get; set; }=false;
        public ICollection<History> Histories { get; set; }
    }
}
