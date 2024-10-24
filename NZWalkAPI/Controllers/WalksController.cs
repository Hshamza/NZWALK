using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;
using NZWalkAPI.Repositories;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepositoy walkRepo;
        public WalksController(IMapper mapper, IWalkRepositoy walkRepositoy) {
            this._mapper = mapper;
            this.walkRepo = walkRepositoy;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
             var walkDomainModel =  _mapper.Map<Walk>(addWalkRequestDTO);
             await walkRepo.CreateAsync(walkDomainModel);

            var walkDTO = _mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
             
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool isAscending,
            [FromQuery] int pageNumber =1, [FromQuery] int pageSize = 1000)
        {
           var walkDomainModel = await walkRepo.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
           return Ok(_mapper.Map<List<WalkDTO>>(walkDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepo.GetAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] UpdateWalkDTO updateWalkDTO)
        {
            var walkDomainModel = _mapper.Map<Walk>(updateWalkDTO);

            walkDomainModel = await walkRepo.UpdateAsync(id, walkDomainModel);
            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepo.DeleteAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(walkDomainModel)); 
        }
    }
}
