using System;

namespace BludataTest.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Active { get; set; } = true;

        public void Deactivate()
        {
            Active = false;
        }

    }
}