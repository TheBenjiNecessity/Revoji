using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("app_user")]
    public class AppUser : User
    {
        [Required]
        [Column("firstname")]
        public string FirstName { get; set; }

        [Required]
        [Column("lastname")]
        public string LastName { get; set; }

        [Column("dob")]
        public DateTime? DOB { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("religion")]
        public string Religion { get; set; }

        [Column("politics")]
        public string Politics { get; set; }

        [Column("education")]
        public string Education { get; set; }

        [Column("profession")]
        public string Profession { get; set; }

        [Column("interests")]
        public string Interests { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("administrative_area")]
        public string AdministrativeArea { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [NotMapped]//TODO: map this later
        public dynamic Content { get; set; }

        [NotMapped]//TODO: map this later
        public dynamic Settings { get; set; }
    }
}
