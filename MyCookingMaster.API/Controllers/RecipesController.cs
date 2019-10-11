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
    public class RecipeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Recipees
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetRecipees()
        {
            //return _unitOfWork.Recipes.GetAllIncludingIngredients().ToList();
            return _unitOfWork.Repository<Recipe>().Find().ToList();
        }

        // GET: api/Recipees/5
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipee(int id)
        {
            var recipee = _unitOfWork.Repository<Recipe>().FindById(id);

            if (recipee == null)
            {
                return NotFound();
            }

            return recipee;
        }

        //// PUT: api/Recipees/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, Recipe recipee)
        {
            if (id != recipee.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Repository<Recipe>().Update(recipee);

            try
            {
                _unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.Repository<Recipe>().Contains(x => x.Id == id))
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

        // POST: api/Recipees
        [HttpPost]
        public ActionResult<Recipe> PostRecipee(Recipe recipee)
        {
            _unitOfWork.Repository<Recipe>().Add(recipee);
            _unitOfWork.Complete();

            return CreatedAtAction("GetRecipee", new { id = recipee.Id }, recipee);
        }

        // DELETE: api/Recipees/5
        [HttpDelete("{id}")]
        public ActionResult<Recipe> DeleteRecipee(int id)
        {
            var recipee = _unitOfWork.Repository<Recipe>().FindById(id);
            if (recipee == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Recipe>().Remove(recipee);
            _unitOfWork.Complete();

            return recipee;
        }
    }
}
