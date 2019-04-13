using System;

namespace Domain.Model
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
