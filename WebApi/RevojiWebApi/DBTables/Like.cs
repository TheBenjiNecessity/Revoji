using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("like")]
    public class Like : Table
    {
        [Column("type")]
        public string Type { get; set; }

        [Column("app_user_id")]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }

        [Column("review_id")]
        public int ReviewId { get; set; }

        [ForeignKey("ReviewId")]
        public virtual Review Review { get; set; }
    }
}
