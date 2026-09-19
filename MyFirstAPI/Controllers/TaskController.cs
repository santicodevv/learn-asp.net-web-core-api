using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers
{
    //Estre atributo nos activa el comportamiento automatico de API, para evitarnos hacer validacion de modelo,
    //    respuesta atuomatica 400 en erroes de validacion

    [ApiController]

    //Esto es la ruta de cual sera el controller que va a recibir las peticiones http y la va a devolver, osea el manejador,
    //    el controller lo que hace en este caso es que tenemos el controller TaskController, pero ya en el endposins seria GET /api/task/, 
    //    osea ya nos crea la ruta
    [Route("/api/[controller]")]
    public class TaskController : Controller
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Id = 1,
                Name = "Learn ASP.Net",
                Done = false,
            }
        };

        //Este es un atributto que solo se usara en peticione http GET
        [HttpGet]
        public ActionResult<List<TaskItem>> GetAll()
        {
            return Ok(_tasks);
        }
    }
}

public class TaskItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
        public bool Done { get; set; }
}
