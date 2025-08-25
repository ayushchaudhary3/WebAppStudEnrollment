using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _studentRepo;

        public StudentController(IStudent studentRepo)
        {
            _studentRepo = studentRepo;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
           var students= await _studentRepo.GetAllStudents();
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentRepo.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        // GET: Students/Create

        public IActionResult Create()
        {
            return View();
        }

        // Replace all instances of 'StudentId' with 'Id' to match the Student class property

        // In Create and Edit actions, update the Bind attribute and property access

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentRepo.AddStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepo.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Address")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _studentRepo.UpdateStudent(id,student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepo.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        // POST: Students/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentRepo.GetStudentById(id);
            if (student != null)
            {
                await _studentRepo.DeleteStudent(id);
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
