using CommonReadModel;
using System.Threading.Tasks;

namespace MetalApi.Providers
{
    public interface IMetalProvider
    {
        Task<string> Get(Metal metalType);
    }
}
