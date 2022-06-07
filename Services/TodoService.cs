using TWTodoList.Contexts;
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
}