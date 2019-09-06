using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public virtual TeamDTO Team { get; set; }
    }
}
