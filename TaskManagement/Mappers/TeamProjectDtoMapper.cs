using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Mappers
{
    public static class TeamProjectDtoMapper
    {
        public static TeamProjectDTO ToDto(this TeamProject teamProject)
        {
            TeamProjectDTO teamProjectDTO = new TeamProjectDTO
            {
                Team = teamProject.Team.ToDto(),
                Project = teamProject.Project.ToDto()
            };
            return teamProjectDTO;
        }
    }
}
