using Microsoft.AspNetCore.Mvc;
using School.BLL;
using School.Domain;
using School.Presentation.ViewModels.Student;

namespace School.Presentation.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService = new StudentService();
        private readonly IDepartmentService _departmentService = new DepartmentService();


        #region Read

        //Student/Index
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync();

            return View("Index", students);
        }


        //Student/Index/1
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            return View("Details", student);
        } 
        #endregion

        #region Create
        //Student/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // refill
            var departments = await _departmentService.GetAllAsync();

            var vm = new CreateStudentVM
            {
                Departments = departments.ToList(),
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentVM createStudentVM)
        {
            //refill
            var departments = await _departmentService.GetAllAsync();
            createStudentVM.Departments = departments.ToList();


            //Not Valid 
            if (!ModelState.IsValid)
            {
                return View("Create", createStudentVM);
            }


            //Valid 

            //Map VM => Entity 
            Student? student = new Student()
            {
                Name = createStudentVM.Name,
                Age = createStudentVM.Age,
                Address = createStudentVM.Address,
                Email = createStudentVM.Email,
                ImageURL = createStudentVM.ImageURL,
                DepartmentId = createStudentVM.DepartmentId
            };

            //Add
            await _studentService.AddAsync(student);

            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            var departments = await _departmentService.GetAllAsync();

            var viewModel = new UpdateStudentVM
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Address = student.Address,
                ImageURL = student.ImageURL,
                DepartmentId = student.DepartmentId,
                Departments = departments.ToList()
            };

            return View(viewModel);
        }

       
        [HttpPost]
        public async Task<IActionResult> Update(UpdateStudentVM model)
        {
            //refill
            var departments = await _departmentService.GetAllAsync();
            model.Departments = departments.ToList();


            //Not Valid 
            if (!ModelState.IsValid)
            {
                return View("Update", model);
            }


            var student = await _studentService.GetByIdAsync(model.Id);
            if (student == null)
                return NotFound();

            // Update entity
            student.Name = model.Name;
            student.Email = model.Email;
            student.Age = model.Age;
            student.Address = model.Address;
            student.ImageURL = model.ImageURL;
            student.DepartmentId = model.DepartmentId;

            await _studentService.UpdateAsync(student);

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        //  Student/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            await _studentService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        #endregion


    }
}
