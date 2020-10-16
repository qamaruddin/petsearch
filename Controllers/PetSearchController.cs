using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetSearch.Services;
using PetSearch.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetSearchController : ControllerBase
    {
        private readonly IPetSearchService _petSearchService;
        private readonly ILogger<PetSearchController> _logger;

        public PetSearchController(IPetSearchService petSearchService, ILogger<PetSearchController> logger)
        {
            _petSearchService = petSearchService;
            _logger = logger;
        }

        public async Task<IEnumerable<PetSearchResultViewModel>> Get([FromQuery] PetSearchRequest filterRequest)
        {
            return await _petSearchService.SearchPetsAsync(filterRequest);
        }
    }
}
