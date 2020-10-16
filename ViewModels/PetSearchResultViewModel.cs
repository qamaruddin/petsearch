using System.Collections.Generic;

namespace PetSearch.ViewModels
{
    public class PetSearchResultViewModel
    {
        public string OwnerGender { get; set; }

        public IEnumerable<PetViewModel> Pets { get; set; }
    }
}