using System;
using TaskManagement.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Mappers
{
    public static class ProjectDtoMapper
    {
        public static ProjectDTO ToDto(this Project project)
        {
            ProjectDTO projectDTO = new ProjectDTO
            {
                Title = project.Title,
                Content = project.Content,
                StartDate = project.StartDate,
                Deadline = project.Deadline
            };
            return projectDTO;
        }
    }
}
