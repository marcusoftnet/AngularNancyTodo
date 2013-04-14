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
        public dynamic Limit { get { return Request.Query.limit; } }
        public dynamic Offset { get { return Request.Query.offset; } }

        private List<TodoItem> GetTodos()
        {
            return _db.TodoItems
                        .All()
                        .Where(GetWhere())
                        .Take(Limit)
                        .Skip(Offset);
        }

        private SimpleExpression GetWhere()
        {
            // This is just a default expression that matches everything
            // not sure I like this but it works
            var where = new SimpleExpression("1", "1", SimpleExpressionType.Equal);

            if(Q.HasValue && Q != "undefined")
            {
                // If we have a query we'll overwrite the WHERE-part
                // with the one doing the search
                where = _db.TodoItems.Todo.Like("%" + Q + "%");
            }
            return where;
        }
    }
}