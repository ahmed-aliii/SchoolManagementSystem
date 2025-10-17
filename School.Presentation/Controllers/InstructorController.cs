using Microsoft.AspNetCore.Mvc;
using School.BLL;
using School.Domain;
using System.Threading.Tasks;

namespace School.Presentation.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService = new InstructorService();
        private readonly IDepartmentService _departmentService = new DepartmentService();

        #region Read
        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorService.GetAllAsync();

            return View(instructors.ToList());
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);

            return View(instructor);
        }
        #endregion

        #region Create
        // GET: Instructor/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var depatments = await _departmentService.GetAllAsync();
           
            var vm = new CreateInstructorVM()
            {
                Departments = depatments.ToList()
            };
            return View(vm);
        }

        // POST: Instructor/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorVM vm)
        {
            if (!ModelState.IsValid)
            {
                // Reload departments to repopulate dropdown
                var departments = await _departmentService.GetAllAsync();
                vm.Departments = departments.ToList();
                return View(vm);
            }

            var instructor = new Instructor
            {
                Name = vm.Name,
                Address = vm.Address,
                Age = vm.Age,
                Salary = vm.Salary,
                ImageURL = string.IsNullOrWhiteSpace(vm.ImageURL) ? "avatar.png" : vm.ImageURL,
                HireDate = vm.HireDate,
                DepartmentId = vm.DepartmentId
            };

            await _instructorService.AddAsync(instructor);

            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        // GET: Instructor/Update/5
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
                return NotFound();

            var departments = await _departmentService.GetAllAsync();

            var vm = new UpdateInstructorVM
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Address = instructor.Address,
                Age = instructor.Age,
                Salary = instructor.Salary,
                ImageURL = instructor.ImageURL,
                HireDate = instructor.HireDate,
                DepartmentId = instructor.DepartmentId,
                Departments = departments.ToList()
            };

            return View(vm);
        }

        // POST: Instructor/Update
        [HttpPost]
        public async Task<IActionResult> Update(UpdateInstructorVM vm)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.GetAllAsync();
                vm.Departments = departments.ToList() ;
                return View(vm);
            }

            var instructor = await _instructorService.GetByIdAsync(vm.Id);
            if (instructor == null)
                return NotFound();

            // Map VM to Entity
            instructor.Name = vm.Name;
            instructor.Address = vm.Address;
            instructor.Age = vm.Age;
            instructor.Salary = vm.Salary;
            instructor.ImageURL = string.IsNullOrEmpty(vm.ImageURL) ? "avatar.png" : vm.ImageURL;
            instructor.HireDate = vm.HireDate == default ? DateTime.Now : vm.HireDate;
            instructor.DepartmentId = vm.DepartmentId;

                await _instructorService.UpdateAsync(instructor);
                return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        //  Student/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);

            if (instructor == null)
                return NotFound();

            await _instructorService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        #endregion
    }
}
