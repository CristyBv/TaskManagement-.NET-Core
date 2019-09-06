using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Mappers
{
    public static class TeamDtoMapper
    {
        public static TeamDTO ToDto(this Team team)
        {
            TeamDTO teamDTO = new TeamDTO
            {
                Name = team.Name,
            };

            return teamDTO;
        }
    }
}
