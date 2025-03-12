using System;

namespace Echo.Core.Abstractions.Models
{
    public interface IAuditableEntity
    {
        String CreatedBy { get; set; }
        String LastModifiedBy { get; set; }
        DateTime CreatedOnUtc { get; set; }
        DateTime? ModifiedOnUtc { get; set; }
    }
}