﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.ViewModel
{
    public class ChangeAvaterViewModel
    {
        public string Name { get; set; }
        [Required]
        public string Picture { get; set; }

    }
}
