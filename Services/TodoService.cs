using TWTodoList.Contexts;
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
}