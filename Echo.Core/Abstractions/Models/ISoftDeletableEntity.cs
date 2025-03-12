using System;

namespace Echo.Core.Abstractions.Models
{
    public interface ISoftDeletableEntity
    {
        DateTime? DeletedOnUtc { get; }
        bool Deleted { get; }
    }
}