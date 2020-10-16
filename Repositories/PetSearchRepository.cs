using System.Collections.Generic;
using System.Threading.Tasks;
using PetSearch.Common;
using PetSearch.Models;

namespace PetSearch.Repositories
{
    public class PetSearchRepository : IPetSearchRepository
    {
        private readonly IApiHelper _apiHelper;

        public PetSearchRepository(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<PetOwner>> SearchPetsAsync(string apiUrl)
        {
            return await _apiHelper.GetResult<List<PetOwner>>(apiUrl);
        }
    }
}