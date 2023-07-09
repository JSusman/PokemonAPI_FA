using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : Controller
    {
        private readonly ICatagoryRepository _catagoryRepository;
        private readonly IMapper _mapper;

        public CatagoryController(ICatagoryRepository catagoryRepository, IMapper mapper)
        {
            _catagoryRepository = catagoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Catagory>))]

        public IActionResult GetCatagories()
        {
            var catagories = _mapper.Map<List<CatagoryDto>>(_catagoryRepository.GetCatagories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(catagories);

        }

        [HttpGet("{catagoryId}")]
        [ProducesResponseType(200, Type = typeof(Catagory))]
        [ProducesResponseType(400)]
        public IActionResult GetCatagory(int catagoryId)
        {
            if (!_catagoryRepository.CatagoryExists(catagoryId))
                return NotFound();

            var catagory = _mapper.Map<CatagoryDto>(_catagoryRepository.GetCatagory(catagoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(catagory);
        }

        [HttpGet("pokemon/{catagoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByCatagoryId(int catagoryId)
        {

            var pokemons = _mapper.Map<List<PokemonDto>>(_catagoryRepository.GetPokemonByCatagory(catagoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemons);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCatagory([FromBody] CatagoryDto catagoryCreate)
        {

            if (catagoryCreate == null)
                return BadRequest();

            var catagory = _catagoryRepository.GetCatagories()
                .Where(c => c.Name.Trim().ToUpper() == catagoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (catagory != null)
            {
                ModelState.AddModelError("", "Category Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var catagoryMap = _mapper.Map<Catagory>(catagoryCreate);

            if (!_catagoryRepository.CreateCatagory(catagoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully Created");

        }

        [HttpPut("{catagoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCatagory(int catagoryId, [FromBody] CatagoryDto updateCatagory)
        { 
        
            if(updateCatagory == null)
                return BadRequest(ModelState);

            if(catagoryId != updateCatagory.Id)
                return BadRequest(ModelState);

            if (!_catagoryRepository.CatagoryExists(catagoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var catagoryMap = _mapper.Map<Catagory>(updateCatagory);

            if (!_catagoryRepository.UpdateCatagory(catagoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating catagory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
