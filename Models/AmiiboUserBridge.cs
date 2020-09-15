namespace AmiiboTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AmiiboUserBridge")]
    public partial class AmiiboUserBridge
    {
        [Key]
        public int PK { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        public int? AmiiboID { get; set; }

        public bool IsWishList { get; set; }

        public virtual Amiibo Amiibo { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
