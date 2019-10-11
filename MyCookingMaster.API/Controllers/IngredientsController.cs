using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCookingMaster.BL.Interfaces;
using MyCookingMaster.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCookingMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Ingredients
        [HttpGet]
        public ActionResult<IEnumerable<Ingredient>> GetIngredients()
        {
            return _unitOfWork.Repository<Ingredient>().Find().ToList();
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public ActionResult<Ingredient> GetIngredient(int id)
        {
            var ingredient = _unitOfWork.Repository<Ingredient>().FindById(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }

        // PUT: api/Ingredients/5
        [HttpPut("{id}")]
        public IActionResult PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Repository<Ingredient>().Update(ingredient);

            try
            {
                _unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.Repository<Ingredient>().Contains(x => x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ingredients
        [HttpPost]
        public ActionResult<Ingredient> PostIngredient(Ingredient ingredient)
        {
            _unitOfWork.Repository<Ingredient>().Add(ingredient);
            _unitOfWork.Complete();

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public ActionResult<Ingredient> DeleteIngredient(int id)
        {
            var ingredient = _unitOfWork.Repository<Ingredient>().FindById(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Ingredient>().Remove(ingredient);
            _unitOfWork.Complete();

            return ingredient;
        }
    }
}
