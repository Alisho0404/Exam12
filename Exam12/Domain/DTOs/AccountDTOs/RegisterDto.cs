﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.AccountDTOs
{
    public class RegisterDto
    {
        public required string Name { get; set; }
        [DataType(DataType.EmailAddress)] public required string Email { get; set; }
        [DataType(DataType.Password)] public required string Password { get; set; }
        [Compare("Password"), DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
    }
}
