using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WebApi.Passports
{
    [Table("passports")]
    public class Passport
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("series")]
        public int Series { get; set; }
        [Column("number")]
        public int Number { get; set; }
    }
}
