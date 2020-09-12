using System.Collections.Generic;

namespace Domain
{
    public class Instructor : BaseDomainObject
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Grade { get; set; }

        public byte[] Avatar { get; set; }

        public ICollection<CourseInstructor> CourseLinks { get; set; }
    }
}