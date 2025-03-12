using System.Collections.Generic;
using System.Threading.Tasks;

namespace Echo.Core.Abstractions.Services
{
    public interface IForbidWords
    {
        Task<List<string>> LoadForbidWords();
    }
}
