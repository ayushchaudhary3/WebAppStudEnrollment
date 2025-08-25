using System.ComponentModel.DataAnnotations;                                        // Provides validation attributes like [Required], [Key], etc.

namespace WebApStudentEnrolment.Models
{
    public class Enrolment
    {
        [Key]                                                                       // Primary Key
        public int Id { get; set; }

        public int StudentId { get; set; }                                          // Foreign Key to Student Table

        public int CourseId { get; set; }                                           // Foreign Key to Course Table

        public DateTime EnrolmentDate { get; set; }                                 // Date & Time when enrolment occurred

        public virtual Student? Student { get; set; }                               // Navigation property to Student (lazy loading possible)

        public virtual Course? Course { get; set; }                                 // Navigation property to Course

        public Enrolment() { }

        public Enrolment(int id, int studentId, int courseId, DateTime enrolmentDate)
        {
            Id = id;
            StudentId = studentId;
            CourseId = courseId;
            EnrolmentDate = enrolmentDate;
        }

        // Returns string version of the enrolment with navigation data
        /*
        public override string ToString()
        {
            return $"Id: {Id}, StudentId: {StudentId}, CourseId: {CourseId}, EnrolmentDate: {EnrolmentDate}" +
                $"Student Nme :{ Student.Name} Course Title: {Course.Name}" ;
        }
        */
    }
}