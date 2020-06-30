using System;

namespace ChoreCore.Models
{
    public class User
    {
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }  // enum??

        public int Zip { get; set; }
    }
}
