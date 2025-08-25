using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public class EnrolmentRepo : IEnrolments                                                    // Implements IEnrolments interface to manage enrolment data
    {
        private readonly StudentEnrolmentContext _context;                                      // Private readonly context to access the database

        public EnrolmentRepo() { }                                                              // Parameterless constructor
        public EnrolmentRepo(StudentEnrolmentContext context)                                   // Constructor with dependency injection to access DB context
        {
            _context = context;
        }

        public int Count { get; private set; }                                                  // Tracks total number of enrolments (manual count)

        // Method - 1
        public async Task<Enrolment> AddEnrolment(Enrolment enrolment)                          // Adds a new enrolment to the database
        {
            await _context.Enrolments.AddAsync(enrolment);                                      // Add the enrolment to DbSet
            await _context.SaveChangesAsync();                                                  // Save changes to the database
            return enrolment;                                                                   // Return the added enrolment
        }

        // Method - 2
        public async Task<Enrolment> GetEnrolmentById(int enrolmentId)                          // Retrieves a specific enrolment by its ID
        {
            return await _context.Enrolments
                            .Include(e => e.Student)
                            .Include(e => e.Course)
                            .FirstOrDefaultAsync(e => e.Id == enrolmentId);                     // Include related Student and Course entities for complete data

        }

        // Method - 3
        public async Task<IEnumerable<Enrolment>> GetAllEnrolments()                            // Retrieves all enrolments
        {
            // Include related Student and Course entities
            return await _context.Enrolments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .ToListAsync();                                                             // Return list of enrolments
        }

        // Method - 4
        public async Task<Enrolment> UpdateEnrolment(int enrolmentId, Enrolment newenrol)       // Updates an existing enrolment
        {
            var existingEnrolment = await _context.Enrolments.FindAsync(enrolmentId);           // Find existing enrolment
            if (existingEnrolment == null)
            {
                return existingEnrolment;                                                       // Return null if not found
            }

            existingEnrolment.EnrolmentDate = newenrol.EnrolmentDate;                           // Update enrolment date
            existingEnrolment.StudentId = newenrol.StudentId;                                   // Update student ID
            existingEnrolment.CourseId = newenrol.CourseId;                                     // Update course ID

            await _context.SaveChangesAsync();                                                  // Save changes to the database
            return existingEnrolment;                                                           // Return updated enrolment
        }

        // Method - 5
        public async Task<Enrolment> DeleteEnrolment(int enrolmentId)                           // Deletes an enrolment by its ID
        {
            var enrolment = await _context.Enrolments.FindAsync(enrolmentId);                   // Find the enrolment
            if (enrolment == null)
            {
                return enrolment;                                                               // Return null if not found
            }

            _context.Enrolments.Remove(enrolment);                                              // Remove enrolment from DbSet
            await _context.SaveChangesAsync();                                                  // Commit deletion to database
            return enrolment;                                                                   // Return deleted enrolment
        }
    }
}

/*
 | Status Code                | Meaning          | When it's used                               |
| --------------------------- | -----------------| -------------------------------------------- |
| `200 OK`                    | Success          | Data fetched, saved, or updated successfully |
| `404 Not Found`             | Resource missing | Item not found in the database               |
| `500 Internal Server Error` | Server crash     | Unexpected error on the server               |
| `400 Bad Request`           | Invalid input    | Client sent wrong or incomplete data         |
 */