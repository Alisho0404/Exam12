﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
    public class UserFilter:PaginationFilter
    {
        public required string Name { get; set; }

    }
}
