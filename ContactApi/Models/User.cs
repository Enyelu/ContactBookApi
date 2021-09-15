using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User:IdentityUser
    {
        [Key]
        public override string Id    { get; set; }
        public string FirstName      { get; set; }
        public string LastName       { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImageUrl       { get; set; }
       
    }
}
