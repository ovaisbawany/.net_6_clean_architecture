using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Core.Entities.Base
{
    // Base entity or auditable entity
    public class BaseEntity<TKey> : IAuditModel<TKey>
    {
        public TKey Id { get; set; }

        [Required]
        public long CreatedBy { get; set; }

        [Required]
        [Column(TypeName = "datetime2(3)")]
        public DateTime CreatedOn { get; set; }

        public long? LastModifiedBy { get; set; }

        [Column(TypeName = "datetime2(3)")]
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
