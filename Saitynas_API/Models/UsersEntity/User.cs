using System;

namespace Saitynas_API.Models.UsersEntity
{
    public class User
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
