using System.Collections.Generic;
using System.Threading.Tasks;
using PetSearch.ViewModels;

namespace PetSearch.Services
{
    public interface IPetSearchService
    {
        Task<IEnumerable<PetSearchResultViewModel>> SearchPetsAsync(PetSearchRequest filterRequest = null);
    }
}