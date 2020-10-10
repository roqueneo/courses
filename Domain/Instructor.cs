using System.Collections.Generic;

namespace Domain
{
    public class Instructor : BaseDomainObject
    {
        public Instructor()
        { }
            
        public Instructor(string name, string lastName, string grade)
        {
            Name = name;
            LastName = lastName;
            Grade = grade;
        }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Grade { get; set; }

        public byte[] Avatar { get; set; }

        public ICollection<CourseInstructor> CourseLinks { get; set; }
    }
}