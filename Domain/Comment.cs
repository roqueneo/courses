using System;

namespace Domain
{
    public class Comment : BaseDomainObject
    {
        public string Student { get; set; }
        
        public int Score { get; set; }
        
        public string Text { get; set; }
        
        public Guid CourseId { get; set; }
        
        public Course Course { get; set; }

    }
}