using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.CustomActionFilter;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;
using NZWalkAPI.Repositories;
using System.Text.Json;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionController : ControllerBase
    {

        private readonly NZWalksDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRegionRepository regionRepository;
        private readonly ILogger<RegionController> logger;
        public RegionController(NZWalksDbContext dbContext,IRegionRepository regionRepository, IMapper mapper, ILogger<RegionController> logger) {
            this._context = dbContext;
            this.regionRepository = regionRepository;
            this._mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
 
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var regions = await regionRepository.GetAllSync();
                logger.LogInformation("Get All Action method was invoked");
                var regionsDto = _mapper.Map<List<RegionDTO>>(regions);
                logger.LogInformation($"Finised Request:{JsonSerializer.Serialize(regionsDto)}");
                return Ok(regionsDto);

            }
            catch(Exception ex)
            {
               logger.LogError(ex, ex.Message);
                throw;
            }

           
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegion([FromRoute] Guid id) {

            var res = await regionRepository.GetById(id);

            if (res == null)
            {
                return NotFound();
            }

            var regionsDto = _mapper.Map<RegionDTO>(res);


           
            return Ok(regionsDto);
        }
        [HttpPost]
        [ValidateModel]
        public async  Task<IActionResult> Create([FromBody] AddRegionDTO addRegionDTO)
        {                
            var regionDomainModel = _mapper.Map<Region>(addRegionDTO);
            

             regionDomainModel = await regionRepository.Create(regionDomainModel);

            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);
       
            return CreatedAtAction(nameof(GetRegion), new { id = regionDomainModel.Id}, regionDto);

        
            
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            var updateRegionModel = _mapper.Map<Region>(updateRegionDTO);
         

            var res = await regionRepository.Update(id, updateRegionModel);

            if(res == null)
            {
                return NotFound();
            }

            var regionDTo = _mapper.Map<RegionDTO>(res);

            return Ok(regionDTo);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var res = await regionRepository.Delete(id);
            if(res == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<RegionDTO>(res);
      
            return Ok(regionDTO);
        }

    }
} 
