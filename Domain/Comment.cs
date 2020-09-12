namespace Domain
{
    public class Comment : BaseDomainObject
    {
        public string Student { get; set; }
        
        public int Score { get; set; }
        
        public string Text { get; set; }
        
        public int CourseId { get; set; }
        
        public Course Course { get; set; }

    }
}