using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("reviewable_event")]
    public class ReviewableEvent : Reviewable
    {
        [Column("city")]
        public string City { get; set; }

        [Column("administrative_area")]
        public string AdministrativeArea { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("start_time")]
        public DateTime StartTime { get; set; }

        [Column("end_time")]
        public DateTime EndTime { get; set; }
    }
}