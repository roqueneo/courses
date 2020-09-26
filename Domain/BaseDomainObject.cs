using System;

namespace Domain
{
    public abstract class BaseDomainObject
    {
        public Guid Id { get; set; }
    }
}