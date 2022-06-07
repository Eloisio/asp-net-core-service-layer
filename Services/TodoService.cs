using TWTodoList.Contexts;

namespace TWTodoList.Services;

public class TodoService
{
    private readonly AppDbContex _context;

    public TodoService(AppDbContex context)
    {
        _context = context;
    }
}