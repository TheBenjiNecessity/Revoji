using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("review")]
    public class Review : Table
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("code")]
        public string CodeType { get; set; }

        [NotMapped]//TODO: map this later
        public dynamic Content { get; set; }

        [Column("app_user_id")]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }

        [Column("reviewable_event_id")]
        public int ReviewableEventId { get; set; }

        [ForeignKey("ReviewableEventId")]
        public virtual ReviewableEvent ReviewableEvent { get; set; }

        [Column("reviewable_product_id")]
        public int ReviewableProductId { get; set; }

        [ForeignKey("ReviewableProductId")]
        public virtual ReviewableProduct ReviewableProduct { get; set; }

        [Column("reviewable_service_id")]
        public int ReviewableServiceId { get; set; }

        [ForeignKey("ReviewableServiceId")]
        public virtual ReviewableService ReviewableService { get; set; }

        [Column("reviewable_business_id")]
        public int ReviewableBusinessId { get; set; }

        [ForeignKey("ReviewableBusinessId")]
        public virtual ReviewableBusiness ReviewableBusiness { get; set; }
    }
}