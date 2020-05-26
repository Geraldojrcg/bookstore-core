using System.Collections.Generic;
using System.Threading.Tasks;
using bookstore.Data;
using bookstore.Models;
using bookstore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get([FromServices] IBookRepository repository)
        {
            var data = await repository.Get();
            return data;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Book>> GetById([FromServices] IBookRepository repository, int id)
        {
            var data = await repository.GetById(id);
            return data;
        }

        [HttpPost]
        public ActionResult<Book> Post(
            [FromServices] IBookRepository repository,
            [FromServices] IUnitOfWork uow,
            [FromBody] Book model)
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
        public ActionResult<Book> Put(
           [FromServices] IBookRepository repository,
           [FromServices] IUnitOfWork uow,
           [FromBody] Book model,
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
        public ActionResult<Book> Delete(
            [FromServices] IBookRepository repository,
            [FromServices] IUnitOfWork uow,
             int id)
        {
            repository.Delete(id);
            uow.Commit();
            return Ok();
        }
    }
}