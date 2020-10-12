using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Domain
{
    public class Course : BaseDomainObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDate { get; set; } 

        public byte[] CoverPhoto { get; set; } 

        public Price Price { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<CourseInstructor> InstructorLinks { get; set; } = new Collection<CourseInstructor>();

        public void AddInstructor(Instructor instructor)
        {
            if (instructor == null || InstructorLinks.Any(il => il.InstructorId == instructor.Id))
                return;

            CourseInstructor courseInstructor = new CourseInstructor(this, instructor);
            InstructorLinks.Add(courseInstructor);
        }
    }
}