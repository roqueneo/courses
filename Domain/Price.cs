using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Price : BaseDomainObject
    {
        [Column(TypeName = "decimal(18,4)")]
        public decimal CurrentPrice { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal PromotionPrice { get; set; }

        public Guid CourseId { get; set; }

        public Course Course { get; set; }
    }
}