using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models.WriteModels;
using TodoApp.Models.ResponseModels;
using Persistence;
using Persistence.Models.ReadModels;
using TodoApp.Models.RequestModels;

namespace TodoApp.Controllers
{
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodosRepository _todosRepository;
        public TodosController(ITodosRepository todosRepository)
        {
            _todosRepository = todosRepository;
        }

        [HttpGet]
        [Route("todos")]
        public async Task<IEnumerable<TodoItemReadModel>> GetTodos()
        {
            return await _todosRepository.GetAll();
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<ActionResult<TodoItemResponse>> GetTodo(Guid id)
        {
            var todoItem = await _todosRepository.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPost]
        [Route("todos")]
        public async Task<ActionResult<TodoItemResponse>> AddTodo([FromBody] SaveTodoItemRequest request)
        {
            var todoItem = new TodoItemWriteModel
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Difficulty = request.Difficulty,
                DateCreated = DateTime.Now,
                IsComplete = false
            };
            await _todosRepository.SaveOrUpdate(todoItem);

            return CreatedAtAction("GetTodo", new { todoItem.Id }, todoItem.MapToTodoItemResponse());
        }

        [HttpPut]
        [Route("todos/{id}")]
        public async Task<ActionResult<TodoItemResponse>> UpdateTodo([FromBody] UpdateTodoItemRequest request, Guid id)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var alreadyExiststodoItem = await _todosRepository.Get(id);
            if (alreadyExiststodoItem is null)
            {
                return NotFound();
            }

            var todoItem = new TodoItemWriteModel
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Difficulty = request.Difficulty,
                DateCreated = DateTime.Now,
                IsComplete = request.IsCompleted
            };
            await _todosRepository.SaveOrUpdate(todoItem);

            return CreatedAtAction("GetTodo", new { todoItem.Id }, todoItem.MapToTodoItemResponse());
        }

        [HttpDelete]
        [Route("todos/{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todoToDelete = await _todosRepository.Get(id);

            if (todoToDelete is null)
            {
                return NotFound();
            }
            await _todosRepository.Delete(id);

            return NoContent();
        }
    }
}