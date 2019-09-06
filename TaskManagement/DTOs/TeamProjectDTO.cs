using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Mappers;
using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class TeamProjectDTO
    {
        public TeamDTO Team { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
