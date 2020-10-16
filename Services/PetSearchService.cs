using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PetSearch.Common;
using PetSearch.Models;
using PetSearch.Repositories;
using PetSearch.ViewModels;

namespace PetSearch.Services
{
    public class PetSearchService : IPetSearchService
    {
        private readonly IPetSearchRepository _petSearchRepository;
        private readonly IOptions<ApiUrlConfig> _options;

        public PetSearchService(IPetSearchRepository petSearchRepository, IOptions<ApiUrlConfig> options)
        {
            _petSearchRepository = petSearchRepository;
            _options = options;
        }

        public async Task<IEnumerable<PetSearchResultViewModel>> SearchPetsAsync(PetSearchRequest filterRequest = null)
        {

            if (string.IsNullOrEmpty(filterRequest.Location) && string.IsNullOrEmpty(filterRequest.Name) && string.IsNullOrEmpty(filterRequest.Type))
            {
                return await GetPetsFromAllLocation();
            }
            
            List<PetSearchResultViewModel> petDetails;

            if (!string.IsNullOrEmpty(filterRequest.Location))
            {
                petDetails = await GetPetsByLocation(filterRequest.Location);
            }
            else
            {
                petDetails = await GetPetsFromAllLocation();
            }

            if (!string.IsNullOrEmpty(filterRequest.Name))
            {
                petDetails = petDetails.Select(x => new PetSearchResultViewModel
                {
                    OwnerGender = x.OwnerGender,
                    Pets = x.Pets.Where(pn => pn.Name.ToLower() == filterRequest.Name.ToLower())
                }).ToList();
            }

            if (!string.IsNullOrEmpty(filterRequest.Type))
            {
                petDetails = petDetails.Select(x => new PetSearchResultViewModel
                {
                    OwnerGender = x.OwnerGender,
                    Pets = x.Pets.Where(pt => pt.Type.ToLower() == filterRequest.Type.ToLower())
                }).ToList();
            }

            return petDetails.Any() ? petDetails : Enumerable.Empty<PetSearchResultViewModel>();
        }

        private async Task<List<PetSearchResultViewModel>> GetPetsFromAllLocation()
        {
            var sydneyPets = await GetPetsByLocation(Location.Sydney.ToString());
            var melbournePets = await GetPetsByLocation(Location.Melbourne.ToString());
            sydneyPets.AddRange(melbournePets);
            var result = sydneyPets.GroupBy(x => x.OwnerGender)
                .Select(group => new PetSearchResultViewModel
                {
                    OwnerGender = @group.Key,
                    Pets = @group.SelectMany(p => p.Pets).OrderBy(o => o.Name).AsEnumerable()
                }).ToList();
            return result;
        }


        private async Task<List<PetSearchResultViewModel>> GetPetsByLocation(string location)
        {
            var pets = new List<PetOwner>();
            if (location.ToLower() == Location.Melbourne.ToString().ToLower())
            {
                pets = await _petSearchRepository.SearchPetsAsync(_options.Value.MelbourneApiBaseUrl);
            }

            if (location.ToLower() == Location.Sydney.ToString().ToLower())
            {
                pets = await _petSearchRepository.SearchPetsAsync(_options.Value.SydneyApiBaseUrl);
            }

            return pets.Select(x => x.ToVm(location)).ToList();
        }
    }
}