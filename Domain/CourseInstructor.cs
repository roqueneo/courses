using System;

namespace Domain
{
    public class CourseInstructor
    {
        public Guid CourseId { get; set; }
        
        public Course Course { get; set; }

        public Guid InstructorId { get; set; }

        public Instructor Instructor { get; set; }
    }
}