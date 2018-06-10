using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RevojiWebApi.DBTables.JSONObjects;

namespace RevojiWebApi.DBTables
{
    [Table("app_user")]
    public class DBAppUser : DBUser
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

        [Column("content")]
        private string AppUserContentJSON { get; set; }

        [Column("settings")]
        private string AppUserSettingsJSON { get; set; }

        private AppUserContent _content;
        private AppUserSettings _settings;

        [NotMapped]
        public AppUserContent Content
        {
            get 
            { 
                if (_content == null)
                {
                    _content = new AppUserContent(AppUserContentJSON);
                }

                return _content;
            }
            set 
            {
                _content = value;
                AppUserContentJSON = value.ToString();
            }
        }

        [NotMapped]
        public AppUserSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new AppUserSettings(AppUserSettingsJSON);
                }

                return _settings;
            }
            set
            {
                _settings = value;
                AppUserSettingsJSON = value.ToString();
            }
        }
    }
}
