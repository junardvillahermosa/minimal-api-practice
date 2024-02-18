//using Microsoft.AspNetCore.Components;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SixMinApi.Data;
using SixMinApi.Dtos;
using SixMinApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace SixMinApi.Controllers
{
    [ServiceFilter(typeof(TestAsyncActionFilter))]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repo, IMapper mapper){
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("binder/{id}")]
        public IActionResult GetPlatformById(
            [ModelBinder(Name = "id")] Platform platform)
            {
                if(platform == null)
                {
                    return NotFound();
                }

                return Ok(platform);

            }
        
        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
        {
            var platformModel = await _repo.GetPlatformById(id);
            if (platformModel != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformModel));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformCreateDto>> CreatePlatform(PlatformCreateDto pmCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(pmCreateDto);
            await _repo.CreatePlatform(platformModel);
            await _repo.SaveChanges();

            var pmReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            Console.WriteLine($"Model State is: {ModelState.IsValid}");

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = pmReadDto}, pmReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlatform(int id, PlatformUpdateDto pmUpdateDto)
        {
            var platformModelFromRepo = await _repo.GetPlatformById(id);

            if (platformModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(pmUpdateDto, platformModelFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        //PATCH api/v1/commands/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialPlatformUpdate(int id, JsonPatchDocument<PlatformUpdateDto> patchDoc)
        {
            var platformModelFromRepo = await _repo.GetPlatformById(id);

            if(platformModelFromRepo == null)
            {
                return NotFound();
            }

            var platformToPatch = _mapper.Map<PlatformUpdateDto>(platformModelFromRepo);
            patchDoc.ApplyTo(platformToPatch, ModelState);

            if(!TryValidateModel(platformToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(platformToPatch, platformModelFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }



    }

}