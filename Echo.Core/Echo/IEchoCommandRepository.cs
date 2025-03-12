using Echo.Core.Models;
using System.Threading.Tasks;

namespace Echo.Core
{
    public interface IEchoCommandRepository
    {
        Task Add(EchoEntity Echo);
        Task Update(EchoEntity Echo);
        Task Delete(EchoEntity Echo);
    }

}