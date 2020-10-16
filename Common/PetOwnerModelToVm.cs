using System.Collections.Generic;
using System.Linq;
using PetSearch.Models;
using PetSearch.ViewModels;

namespace PetSearch.Common
{
    public static class PetOwnerModelToVm
    {
        public static PetSearchResultViewModel ToVm(this PetOwner petOwner, string location)
        {
            return new PetSearchResultViewModel
            {
                OwnerGender = petOwner.Gender,
                Pets = petOwner.Pets != null && petOwner.Pets.Any()
                    ? petOwner.Pets.Select(p => new PetViewModel
                    {
                        Type = p.Type,
                        Name = p.Name,
                        Location = location
                    })
                    : new List<PetViewModel>()
            };
        }
    }
}
