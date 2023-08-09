using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace Todo.Controllers
{
/*We create a Public class */
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var todoListViewModel = GetAllTodos();
            return View(todoListViewModel);
        }

        [HttpGet]

        public JsonResult PopulateForm(int id)
        {
            var todo = GetById(id);
            return Json(todo);
        }

        internal TodoViewModel GetAllTodos()
        {
            list<TodoItem> todolist = new();

            using (SqliteConnection con =
                new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand()) 
                {
                    con.Open();
                    tableCmd.CommandText = "SELECT * FROM todo";

                    using (var render = tableCmd.ExecuteReader()) 
                    {
                        if (render.HasRows)
                        {
                            while(render.read())
                            {
                                todolist.Add(
                                    new TodoItem
                                    {
                                        Id = render.GetInt32(0),
                                        Name = render.GetString(1)
                                    });
                            }
                        }
                        else
                        {
                            return new TodoViewModel
                            {
                                Todolist = todolist
                            };
                        }
                    };
                }
            }
            return new TodoViewModel
            {
                Todolist = todolist
            };
        }

        internal TodoItem GetById()
        {
            TodoItem todo= new();

            using (var Connection =
                new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = connection .CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM todo Where Id = '{id}'";

                    using (var render = tableCmd.ExecuteReader())
                    {
                        if (render.HasRows)
                        {
                            reader.Read()
                           todoId = render.GetInt32(0),
                           todo.Name = render.GetString(1)
                        }
                        else
                        {
                            return todo;
                        }
                    };
                }
            }
            return todo;
        }
    /*We can create a insert button*/
        public RedirectResult Insert(TodoItem todo)
        {
            using (SqliteConnection con =
                new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd= con.CreateCommand()) 
                {
                    con.Open();
                    tableCmd.CommandText = $"INSERT INTO todo (name) VALUES ('{todo.name}') ";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            [HttpPost]
        /*we create a Delete button*/
            public JsonResult Delete(int id)
            {
                using (SqliteConnection con =
                    new SqliteConnection("Data Source=db.sqlite"))
                {
                    using (var tableCmd = con.CreateCommand())
                    {
                        con.Open();
                        tableCmd.CommandText = $"DELETE from todo WHERE Id = '{id}'";
                        tableCmd.ExecuteNonQuery();
                    }
                }
                return Json(new { });
            }
    /*we create a upadte button also*/
            public RedirectResult Update(TodoItem todo)
            {
                using (SqliteConnection con =
                    new SqliteConnection("Data Source=db.sqlite"))
                {
                    using (var tableCmd = con.CreateCommand())
                    {
                        con.Open();
                        tableCmd.CommandText = $"UPDATE todo SET name = '{todo.name}' WHERE Id = '{todo.Id}'";
                        try
                        {
                            tableCmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                return Redirect("https://localhost:5001/");
        }
    }
}


    

