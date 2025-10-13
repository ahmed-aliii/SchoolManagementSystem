using Microsoft.AspNetCore.Mvc;
using School.BLL;
using School.Domain;
using School.Presentation.ViewModels.Student;

namespace School.Presentation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService = new DepartmentService();
      
        #region Read 
        
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllAsync();

            return View(departments.ToList());
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            return View(department);
        }
        #endregion


        #region Create
        //Student/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateDepartmentVM();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentVM createDepartmentVM)
        {

            //Not Valid 
            if (!ModelState.IsValid)
            {
                return View("Create", createDepartmentVM);
            }


            //Valid 

            //Map VM => Entity 
            Department? department = new Department()
            {
                Name = createDepartmentVM.Name,
                Manager = createDepartmentVM.Manager,
                Location = createDepartmentVM.Location,
            };

            //Add
            await _departmentService.AddAsync(department);

            return RedirectToAction("Index");
        }
        #endregion
    }
}
