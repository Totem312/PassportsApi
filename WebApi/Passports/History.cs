using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Passports
{
    [Table("history")]
    public class History
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("dateChangeStatus")]
        public DateTime DateChangeStatus { get; set; }

        [Column("passport_id")]
        [ForeignKey("passport_id")]
        public string PassportId { get; set; }
        public virtual Passport Passport { get; set; }
    }
}
