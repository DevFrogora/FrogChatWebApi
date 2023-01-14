﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatModel.DTOModel
{
    public class RoleDto
    {
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage ="Role Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}
