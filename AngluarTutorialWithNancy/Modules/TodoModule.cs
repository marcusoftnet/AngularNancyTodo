using System.Collections.Generic;
using AngluarTutorialWithNancy.Models;
using Nancy;
using Simple.Data;

namespace AngluarTutorialWithNancy.Modules
{
    public class TodoModule : NancyModule
    {
        private dynamic _db = Database.Open();


        /*
         *  public IEnumerable<TodoItem> GetTodoItems(string q = null, string sort = null, bool desc = false,
                                                               int? limit = null, int offset = 0) {
            var list = ((IObjectContextAdapter) db).ObjectContext.CreateObjectSet<TodoItem>();

            IQueryable<TodoItem> items = string.IsNullOrEmpty(sort) ? list.OrderBy(o=>o.Priority)
                : list.OrderBy(String.Format("it.{0} {1}", sort, desc ? "DESC" : "ASC"));

            if (!string.IsNullOrEmpty(q) && q != "undefined") items = items.Where(t => t.Todo.Contains(q));

            if (offset > 0) items = items.Skip(offset);
            if (limit.HasValue) items = items.Take(limit.Value);
            return items;
        }
         * */


        public TodoModule() //: base("/api/todo")
        {
            Get["/api/todo/"] = _ =>
                           {
                               return Negotiate.WithModel(GetTodos());
                           };
        }

        public dynamic Q { get { return Request.Query.q; } }

        private List<TodoItem> GetTodos()
        {
            if(Q.HasValue && Q != "undefined")
            {
                return _db.TodoItems
                            .All()
                            .Where(_db.TodoItems.Todo.Like("%" + Q + "%"));
            }

            return _db.TodoItems.All();
        }
    }
}