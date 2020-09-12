namespace Domain
{
    public class Price : BaseDomainObject
    {
        public decimal CurrentPrice { get; set; }

        public decimal PromotionPrice { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}