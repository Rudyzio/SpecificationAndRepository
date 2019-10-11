using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCookingMaster.BL.Interfaces;
using MyCookingMaster.BL.Models;
using MyCookingMaster.BL.Specifications;

namespace MyCookingMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _unitOfWork.Repository<User>().Find(new UsersWithRecipesAndIngredientsSpecification()).ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _unitOfWork.Repository<User>().Find(new UsersWithRecipesAndIngredientsSpecification(id)).SingleOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //// PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Repository<User>().Update(user);

            try
            {
                _unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!_unitOfWork.Repository<User>().Contains(x => x.Id == id))
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

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _unitOfWork.Repository<User>().Add(user);
            _unitOfWork.Complete();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = _unitOfWork.Repository<User>().FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<User>().Remove(user);
            _unitOfWork.Complete();

            return user;
        }
    }
}
