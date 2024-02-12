using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentsController : ControllerBase
    {
       private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<StudentPageModel> Get([FromQuery]string firstName = null, [FromQuery] int page = 0, [FromQuery] int pageSize = 10, [FromQuery] string sort = null)
        {
            return Ok(_studentService.Get(firstName, page, pageSize, sort));

        }
    }
}
