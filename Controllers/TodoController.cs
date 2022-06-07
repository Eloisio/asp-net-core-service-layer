using Microsoft.AspNetCore.Mvc;
using TWTodoList.Exceptions;
using TWTodoList.Services;
using TWTodoList.ViewModels;

namespace TWTodoList.Controllers;

public class TodoController : Controller
{
    private readonly TodoService _service;

    public TodoController(TodoService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var viewModel = _service.FindAll();
        ViewData["Title"] = "Lista de Tarefas";
        return View(viewModel);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            _service.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
        catch (TodoNotFoundException)
        {
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "Cadastrar Tarefa";
        return View("Form");
    }

    [HttpPost]
    public IActionResult Create(FormTodoViewModel data)
    {
        _service.Create(data);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        try
        {
            ViewData["Title"] = "Editar Tarefa";
            var viewModel = _service.FindById(id);
            return View("Form", viewModel);
        }
        catch (TodoNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(int id, FormTodoViewModel data)
    {
        try
        {
            _service.UpdateById(id, data);
            return RedirectToAction(nameof(Index));
        }
        catch (TodoNotFoundException)
        {
            return NotFound();
        }
    }

    public IActionResult ToComplete(int id)
    {
        try
        {
            _service.ToComplete(id);
            return RedirectToAction(nameof(Index));
        }
        catch (TodoNotFoundException)
        {
            return NotFound();
        }
    }
}