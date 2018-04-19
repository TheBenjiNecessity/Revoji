using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("reviewable_business")]
    public class ReviewableBusiness : Reviewable
    {
        [Column("city")]
        public string City { get; set; }

        [Column("administrative_area")]
        public string AdministrativeArea { get; set; }

        [Column("country")]
        public string Country { get; set; }
    }
}
