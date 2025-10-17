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
        public  IActionResult Create()
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

        #region Update
       
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            // Map entity => VM
            var vm = new UpdateDepartmentVM
            {
                Name = department.Name,
                Manager = department.Manager,
                Location = department.Location
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDepartmentVM model)
        {

            //Not Valid 
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            var existingDepartment = await _departmentService.GetByIdAsync(model.Id);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            // Update fields
            existingDepartment.Name = model.Name;
            existingDepartment.Manager = model.Manager;
            existingDepartment.Location = model.Location;

            // Call service to update
            await _departmentService.UpdateAsync(existingDepartment);

            // Redirect back to the list
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        //  Department/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department == null)
                return NotFound();

            await _departmentService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        #endregion
    }
}
