using Nancy;
using Simple.Data;

namespace AngluarTutorialWithNancy.Modules
{
    public class TodoModule : NancyModule
    {
        private dynamic _db = Database.Open();

        public TodoModule() : base("/api/todo")
        {
            Get["/"] = _ => { return _db.TodoItems.All(); };
        }
    }
}