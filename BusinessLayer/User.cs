﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Range(10, 80)]
        public int Age { get; set; }

        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(70)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Email { get; set; }

        public List<User> Friends { get; set; }
        public List<Game> Games { get; set; }
        public List<Genre> Genres { get; set; }
        public User()
        {
            Friends = new List<User>();
            Games = new List<Game>();
            Genres = new List<Genre>();
        }
        public User(string firstName, string lastName, int age, string userName, string password, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            UserName = userName;
            Password = password;
            Email = email;
            Friends = new List<User>();
            Games = new List<Game>();
            Genres = new List<Genre>();
        }
    }
}