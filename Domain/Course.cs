using System;
using System.Collections.Generic;

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

        public ICollection<CourseInstructor> InstructorLinks { get; set; }
    }
}