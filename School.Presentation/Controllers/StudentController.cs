using Microsoft.AspNetCore.Mvc;
using School.BLL;

namespace School.Presentation.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService = new StudentService();

        //Student/Index
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();

            return View("Index" , students); 
        }


        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            return View("Details", student);
        }
    }
}
