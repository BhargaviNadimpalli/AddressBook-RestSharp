using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookRestSharp
{
    public class Person
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Int64 PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmailId { get; set; }
    }
}
