﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.AccountDTOs
{
    public class LoginDto
    {
        public required string UserName { get; set; }
        [DataType(DataType.Password)] public required string Password { get; set; }
    }
}
