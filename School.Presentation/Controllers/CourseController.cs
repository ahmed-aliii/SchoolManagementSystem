using Microsoft.AspNetCore.Mvc;
using School.BLL;
using School.Domain;

namespace School.Presentation.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService = new CourseService();
    
        #region Read
        public async Task<IActionResult> Index()
        {
            var cousres = await _courseService.GetAllAsync();

            return View(cousres.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var cousre = await _courseService.GetByIdAsync(id);

            return View(cousre);
        }
        #endregion

        #region Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCourseVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Manual mapping (no AutoMapper)
            var course = new Course
            {
                Name = model.Name,
                Topic = model.Topic,
                Degree = model.Degree,
                MinDegree = model.MinDegree
            };

            await _courseService.AddAsync(course);

            return RedirectToAction(nameof(Index)); 
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            // Manual mapping to UpdateCourseVM
            var vm = new UpdateCourseVM
            {
                Id = course.Id,
                Name = course.Name,
                Topic = course.Topic,
                Degree = course.Degree,
                MinDegree = course.MinDegree
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCourseVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingCourse = await _courseService.GetByIdAsync(model.Id);
            if (existingCourse == null)
            {
                ModelState.AddModelError("", $"Course with ID {model.Id} not found.");
                return View(model);
            }

            // Manual mapping (No AutoMapper)
            existingCourse.Name = model.Name;
            existingCourse.Topic = model.Topic;
            existingCourse.Degree = model.Degree;
            existingCourse.MinDegree = model.MinDegree;

            await _courseService.UpdateAsync(existingCourse);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid course ID.");

            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            await _courseService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
