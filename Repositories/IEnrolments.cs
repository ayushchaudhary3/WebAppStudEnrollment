using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface IEnrolments
    {
        int Count { get; }                                                      // Get the count of total number of enrolments

        // Task<IActionResult> AddEnrolment(int enrolmentId, int studentId, int courseId, DateTime enrolmentDate);

        Task<Enrolment> AddEnrolment(Enrolment enrolment);                      // Method to add a new enrolment

        Task<Enrolment> GetEnrolmentById(int enrolmentId);                      // Method to get a specific enrolment by its ID

        Task<IEnumerable<Enrolment>> GetAllEnrolments();                        // Method to retrieve all enrolments

        Task<Enrolment> UpdateEnrolment(int enrolmentId, Enrolment enrolment);  // Method to update an existing enrolment

        Task<Enrolment> DeleteEnrolment(int enrolmentId);                       // Method to delete an enrolment by ID
    }
}