namespace AmiiboTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Amiibo")]
    public partial class Amiibo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Amiibo()
        {
            AmiiboUserBridges = new HashSet<AmiiboUserBridge>();
        }

        [Key]
        public int PK { get; set; }

        [Required]
        [StringLength(128)]
        public string AmiiboSeries { get; set; }

        [Required]
        [StringLength(128)]
        public string Character { get; set; }

        [Required]
        [StringLength(128)]
        public string GameSeries { get; set; }

        [Required]
        [StringLength(128)]
        public string Head { get; set; }

        [Required]
        [StringLength(128)]
        public string Image { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }


        [StringLength(128)]
        public string ReleaseAU { get; set; }


        [StringLength(128)]
        public string ReleaseEU { get; set; }

        [StringLength(128)]
        public string ReleaseJP { get; set; }


        [StringLength(128)]
        public string ReleaseNA { get; set; }

        [Required]
        [StringLength(128)]
        public string Tail { get; set; }

        [Required]
        [StringLength(128)]
        public string Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AmiiboUserBridge> AmiiboUserBridges { get; set; }
    }
}
