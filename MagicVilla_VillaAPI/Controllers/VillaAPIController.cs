﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ApplicationDbContext db)
        {
            _db = db;
        }   
        private readonly ILogger<VillaAPIController> _logger;
        public VillaAPIController(ILogger<VillaAPIController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult <IEnumerable<VillaDTO>> GetVillas() 
        {
            _logger.LogInformation("GetVillas called");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult <VillaDTO> GetVilla(int id)
        {
            if (id == 0) 
            {
                _logger.LogError("GetVilla called with id 0");
                return BadRequest();
            }
            
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO) 
        {
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Customer error", "Villa nema already exists");
                return BadRequest(ModelState);            
            }
            if (villaDTO == null)
            {
                return BadRequest();
            }

            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa model = new ()
            {
                amenities = villaDTO.amenities,
                description = villaDTO.description,
                Id = villaDTO.Id,
                imageUrl = villaDTO.imageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                rate = villaDTO.rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla",new { id = villaDTO.Id}, villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id) 
        {
            if (id == 0) 
            {
                return BadRequest();
            }
            
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null) 
            { 
                return NotFound(); 
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO) 
        {
            if (villaDTO != null || id != villaDTO.Id) 
            {
                return BadRequest();
            }

            // var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            // villa.Name = villaDTO.Name;
            // villa.Sqft = villaDTO.Sqft;
            //villa.Occupancy = villaDTO.Occupancy;   
            Villa model = new()
            {
                amenities = villaDTO.amenities,
                description = villaDTO.description,
                Id = villaDTO.Id,
                imageUrl = villaDTO.imageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                rate = villaDTO.rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id != 0)
            {
                return BadRequest();    
            }
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

            VillaDTO villaDTO = new()
            {
                amenities = villa.amenities,
                description = villa.description,
                Id = villa.Id,
                imageUrl = villa.imageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                rate = villa.rate,
                Sqft = villa.Sqft,
            };
            if (villa == null) 
            {
                return BadRequest();   
            }

            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = new Villa()
            {
                amenities = villaDTO.amenities,
                description = villaDTO.description,
                Id = villaDTO.Id,
                imageUrl = villaDTO.imageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                rate = villaDTO.rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }

            return NoContent();
        }
    }

}
