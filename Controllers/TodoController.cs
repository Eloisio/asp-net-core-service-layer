using Microsoft.AspNetCore.Mvc;
using TWTodoList.Contexts;
using TWTodoList.Exceptions;
using TWTodoList.Models;
using TWTodoList.Services;
using TWTodoList.ViewModels;

namespace TWTodoList.Controllers;

public class TodoController : Controller
{
    private readonly AppDbContex _context;
    private readonly TodoService _service;

    public TodoController(AppDbContex context, TodoService service)
    {
        _context = context;
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
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            return NotFound();
        }
        _context.Remove(todo);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
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
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            return NotFound();
        }
        todo.IsCompleted = true;
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}