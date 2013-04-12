using System.Collections.Generic;
using AngluarTutorialWithNancy.Models;
using Nancy;
using Simple.Data;

namespace AngluarTutorialWithNancy.Modules
{
    public class TodoModule : NancyModule
    {
        private dynamic _db = Database.Open();

        public TodoModule()
        {
            Get["/"] = _ => { return View["Index", GetAllTodos()]; };
        }

        private List<TodoItem> GetAllTodos()
        {
            return _db.TodoItems.All();
        }
    }

}