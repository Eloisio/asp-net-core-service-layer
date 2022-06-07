using TWTodoList.Contexts;
using TWTodoList.Exceptions;
using TWTodoList.Models;
using TWTodoList.ViewModels;

namespace TWTodoList.Services;

public class TodoService
{
    private readonly AppDbContex _context;

    public TodoService(AppDbContex context)
    {
        _context = context;
    }

    public ListTodoViewModel FindAll()
    {
        var todos = _context.Todos.OrderBy(x => x.Date).ToList();
        return new ListTodoViewModel { Todos = todos };
    }

    public void Create(FormTodoViewModel data)
    {
        var todo = new Todo(data.Title, data.Date);
        _context.Add(todo);
        _context.SaveChanges();
    }

    public FormTodoViewModel FindById(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            throw new TodoNotFoundException();
        }
        return new FormTodoViewModel { Title = todo.Title, Date = todo.Date };
    }

    public void UpdateById(int id, FormTodoViewModel data)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            throw new TodoNotFoundException();
        }
        todo.Title = data.Title;
        todo.Date = data.Date;
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            throw new TodoNotFoundException();
        }
        _context.Remove(todo);
        _context.SaveChanges();
    }
}