using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    public class Reviewable : Table
    {
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [NotMapped]//TODO: map this later
        public dynamic Content { get; set; }
    }
}
