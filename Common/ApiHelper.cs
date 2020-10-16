using System.Threading.Tasks;

namespace PetSearch.Common
{
    public interface IApiHelper
    {
        Task<T> GetResult<T>(string uri);
    }
}
