using System;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Core.Abstractions.Services
{
    public interface IHashUniqueness
    {
        Task<bool> IsUnique(Guid UserId, string hash);
    }
}
