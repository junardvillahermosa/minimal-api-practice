using AutoMapper;
using Azure;
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
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repo, IMapper mapper){
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllCommands([FromHeader] bool flipSwitch)
        {
            var commands = await _repo.GetAllCommands();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"--> The flip switch is: {flipSwitch}");
            Console.ResetColor();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
        {
            var commandModel = await _repo.GetCommandById(id);
            if (commandModel != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandModel));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CommandCreateDto>> CreateCommand(CommandCreateDto cmdCreateDto)
        {
            var commandModel = _mapper.Map<Command>(cmdCreateDto);
            await _repo.CreateCommand(commandModel);
            await _repo.SaveChanges();

            var cmdReadDto = _mapper.Map<CommandReadDto>(commandModel);

            Console.WriteLine($"Model State is: {ModelState.IsValid}");

            return CreatedAtRoute(nameof(GetCommandById), new { Id = cmdReadDto}, cmdReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, CommandUpdateDto cmdUpdateDto)
        {
            var commandModelFromRepo = await _repo.GetCommandById(id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(cmdUpdateDto, commandModelFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        //PATCH api/v1/commands/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = await _repo.GetCommandById(id);

            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandModelFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var commandModelFromRepo = await _repo.GetCommandById(id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _repo.DeleteCommand(commandModelFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }
    }
}