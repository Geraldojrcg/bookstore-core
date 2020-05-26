using System.Collections.Generic;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Models;
using bookstore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Author>>> Get([FromServices] IAuthorRepository repository)
        {
            var data = await repository.Get();
            return data;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Author>> GetById([FromServices] IAuthorRepository repository, int id)
        {
            var data = await repository.GetById(id);
            return data;
        }

        [HttpGet]
        [Route("{id:int}/books")]
        public async Task<ActionResult<List<Book>>> GetBooksByAuthorId([FromServices] IAuthorRepository repository, int id)
        {
            var data = await repository.GetBooksByAuthorId(id);
            return data;
        }

        [HttpPost]
        public ActionResult<Author> Post(
            [FromServices] IAuthorRepository repository,
            [FromServices] IUnitOfWork uow,
            [FromBody] Author model)
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
                uow.Rollback();
                throw;
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<Author> Put(
            [FromServices] IAuthorRepository repository,
            [FromServices] IUnitOfWork uow,
            [FromBody] Author model,
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
        public ActionResult<Author> Delete(
            [FromServices] IAuthorRepository repository,
            [FromServices] IUnitOfWork uow,
            int id)
        {
            repository.Delete(id);
            uow.Commit();
            return Ok();
        }
    }
}