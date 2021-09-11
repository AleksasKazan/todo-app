using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models.ResponseModels;
using Persistence;
using Persistence.Models.ReadModels;
using TodoApp.Models.RequestModels;
using System.Linq;

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
        public async Task<IEnumerable<TodoItemResponse>> GetTodos()
        {
            var todos = await _todosRepository.GetAll();

            return todos.Select(todoItem => todoItem.MapToTodoItemResponse());
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<ActionResult<TodoItemResponse>> GetTodo(Guid id)
        {
            var todoItem = await _todosRepository.Get(id);

            if (todoItem is null)
            {
                return NotFound($"Todo item with id: '{id}' does not exist");
            }

            return todoItem.MapToTodoItemResponse();
        }

        [HttpPost]
        [Route("todos")]
        public async Task<ActionResult<TodoItemResponse>> AddTodo([FromBody] SaveTodoItemRequest request)
        {
            var todoItem = new TodoItemReadModel
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Difficulty = request.Difficulty,
                DateCreated = DateTime.Now,
                IsCompleted = false
            };
            await _todosRepository.SaveOrUpdate(todoItem);

            return CreatedAtAction(nameof(GetTodo), new { todoItem.Id }, todoItem.MapToTodoItemResponse());
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
                return NotFound($"Todo item with id: '{id}' does not exist");
            }
            alreadyExiststodoItem.Title = request.Title;
            alreadyExiststodoItem.Description = request.Description;
            alreadyExiststodoItem.Difficulty = request.Difficulty;

            await _todosRepository.SaveOrUpdate(alreadyExiststodoItem);
            return alreadyExiststodoItem.MapToTodoItemResponse();
        }

        [HttpPut]
        [Route("todos/{id}/status")]
        public async Task<ActionResult<TodoItemResponse>> UpdateTodoStatus([FromBody] UpdateIsCompletedStatus request, Guid id)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var alreadyExiststodoItem = await _todosRepository.Get(id);
            if (alreadyExiststodoItem is null)
            {
                return NotFound($"Todo item with id: '{id}' does not exist");
            }
            if (alreadyExiststodoItem.IsCompleted)
            {
                alreadyExiststodoItem.IsCompleted = false;
            }
            else
            {
                alreadyExiststodoItem.IsCompleted = true;
            }

            await _todosRepository.SaveOrUpdate(alreadyExiststodoItem);
            return alreadyExiststodoItem.MapToTodoItemResponse();
        }

        [HttpDelete]
        [Route("todos/{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todoToDelete = await _todosRepository.Get(id);

            if (todoToDelete is null)
            {
                return NotFound($"Todo item with id: '{id}' does not exist");
            }
            await _todosRepository.Delete(id);

            return NoContent();
        }
    }
}