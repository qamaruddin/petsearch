using System.Collections.Generic;
using System.Threading.Tasks;
using PetSearch.Models;

namespace PetSearch.Repositories
{
    public interface IPetSearchRepository
    {
        Task<List<PetOwner>> SearchPetsAsync(string apiUrl);
    }
}
