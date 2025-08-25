using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly IEnrolments _enrollmentRepo;
        private readonly IStudent _studentRepo;
        private readonly ICourse _courseRepo;

        public EnrolmentController(IEnrolments enrollmentRepo, IStudent studentRepo, ICourse courseRepo)
        {
            _enrollmentRepo = enrollmentRepo;
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentRepo.GetAllEnrolments();
            return View(enrollments);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrolmentById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public async Task<IActionResult> Create()
        {
            // Populate ViewBag with dropdown options
            ViewBag.StudentId = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name");
            ViewBag.CourseId = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Name");
            return View();
        }
        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                // If EnrolmentDate is default, set to current date and time
                if (enrolment.EnrolmentDate == default)
                {
                    enrolment.EnrolmentDate = DateTime.Now;
                }
                try
                {
                    await _enrollmentRepo.AddEnrolment(enrolment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to create record. " + ex.Message);
                }
            }

            ViewBag.StudentId = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name", enrolment.StudentId);
            ViewBag.CourseId = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Name", enrolment.CourseId);
            return View(enrolment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrolmentById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            
            // Populate ViewBag with dropdown options
            ViewBag.StudentId = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name", enrollment.StudentId);
            ViewBag.CourseId = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Name", enrollment.CourseId);
            
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
        {
            if (id != enrolment.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _enrollmentRepo.UpdateEnrolment(id, enrolment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + ex.Message);
                }
            }
            
            // If we get here, repopulate dropdowns and return the view
            ViewBag.StudentId = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name", enrolment.StudentId);
            ViewBag.CourseId = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Name", enrolment.CourseId);
            return View(enrolment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrolmentById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }
        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var enrollment = await _enrollmentRepo.GetEnrolmentById(id);
                if (enrollment != null)
                {
                    await _enrollmentRepo.DeleteEnrolment(id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the error and redirect with error message
                ModelState.AddModelError("", "Unable to delete record. " + ex.Message);
                var enrollment = await _enrollmentRepo.GetEnrolmentById(id);
                return View("Delete", enrollment);
            }
        }
    }
}
