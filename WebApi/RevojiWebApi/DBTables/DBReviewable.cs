using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RevojiWebApi.DBTables.JSONObjects;

namespace RevojiWebApi.DBTables
{
    public class DBReviewable : DBTable
    {
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("content")]
        private string ReviewableContentJSON { get; set; }

        [Column("info")]
        private string ReviewableInfoJSON { get; set; }

        private ReviewableContent _content;
        private ReviewableInfo _info;

        [NotMapped]
        public ReviewableContent Content
        {
            get
            {
                if (_content == null)
                {
                    _content = new ReviewableContent(ReviewableContentJSON);
                }

                return _content;
            }
            set
            {
                _content = value;
                ReviewableContentJSON = value.ToString();
            }
        }

        [NotMapped]
        public ReviewableInfo Info
        {
            get
            {
                if (_info == null)
                {
                    _info = new ReviewableInfo(ReviewableInfoJSON);
                }

                return _info;
            }
            set
            {
                _info = value;
                ReviewableInfoJSON = value.ToString();
            }
        }
    }
}
