using System;

namespace Domain
{
    public class CourseInstructor
    {
        public CourseInstructor()
        { }

        public CourseInstructor(Course course, Instructor instructor)
        {
            if (course != null)
            {
                Course = course;
                CourseId = course.Id;
            }
            if (instructor != null)
            {
                Instructor = instructor;
                InstructorId = instructor.Id;
            }
        }

        public Guid CourseId { get; set; }
        
        public Course Course { get; set; }

        public Guid InstructorId { get; set; }

        public Instructor Instructor { get; set; }
    }
}