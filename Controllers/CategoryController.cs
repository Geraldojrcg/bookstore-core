using System.Collections.Generic;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Models;
using bookstore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace bookstore.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get([FromServices] ICategoryRepository repository)
        {
            var data = await repository.Get();
            return data;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById([FromServices] ICategoryRepository repository, int id)
        {
            var data = await repository.GetById(id);
            return data;
        }

        [HttpGet]
        [Route("{id:int}/books")]
        public async Task<ActionResult<List<Book>>> GetBooksByCategoryId([FromServices] ICategoryRepository repository, int id)
        {
            var data = await repository.GetBooksByCategoryId(id);
            return data;
        }

        [HttpPost]
        public ActionResult<Category> Post(
            [FromServices] ICategoryRepository repository,
            [FromServices] IUnitOfWork uow,
            [FromBody] Category model
            )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Save(model);
                    uow.Commit();
                    return model;
                }
                else
                {
                    return BadRequest(model);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<Category> Put(
           [FromServices] ICategoryRepository repository,
           [FromServices] IUnitOfWork uow,
           [FromBody] Category model,
           int id)
        {
            try
            {
                repository.Update(id, model);
                uow.Commit();
                return model;
            }
            catch (System.Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<Category> Delete(
            [FromServices] ICategoryRepository repository,
            [FromServices] IUnitOfWork uow,
            int id)
        {
            repository.Delete(id);
            uow.Commit();
            return Ok();
        }
    }
}