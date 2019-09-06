using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Mappers;
using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Task, TaskDTO>()
                .ForMember(m => m.Project, d => d.MapFrom(s => s.Project.ToDto()));
            CreateMap<TaskDTO, Task>();
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(m => m.Team, d => d.MapFrom(s => s.Team.ToDto()));
            CreateMap<UserDTO, ApplicationUser>();
        }
    }
}
