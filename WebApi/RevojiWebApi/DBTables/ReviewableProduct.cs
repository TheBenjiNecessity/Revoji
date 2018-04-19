using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("reviewable_product")]
    public class ReviewableProduct : Reviewable
    {
        [Column("code")]
        public string Code { get; set; }

        [Column("code")]
        public string CodeType { get; set; }
    }
}