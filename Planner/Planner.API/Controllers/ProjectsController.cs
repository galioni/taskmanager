﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Core.Domain;
using Planner.Infrastructure.DTO;
using Planner.Infrastructure.Helpers;
using Planner.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planner.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private IProjectService _projectService;

		public ProjectsController(IProjectService projectService)
		{
			this._projectService = projectService;
		}

		// GET: api/Projects
		[HttpGet]
		public async Task<IEnumerable<ProjectDTO>> GetProjects()
		{
			return await _projectService.GetAllProjectsAsync();
		}

		// GET: api/Projects/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProject([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var project = await _projectService.GetProjectByIdAsync(id);

			if (project == null)
			{
				return NotFound();
			}

			return Ok(project);
		}

		// PUT: api/Projects/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] ProjectDTO project)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != project.ID)
			{
				return BadRequest();
			}

			try
			{
				await _projectService.UpdateProjectAsync(project);
			}
			catch (DbUpdateConcurrencyException)
			{
				//if (!ProjectExists(id))
				//{
				//	return NotFound();
				//}
				//else
				//{
				//	throw;
				//}
			}

			return NoContent();
		}

		// POST: api/Projects
		[HttpPost]
		public async Task<IActionResult> PostProject([FromBody] ProjectDTO project)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			OperationResult result = await _projectService.AddProjectAsync(project);

            if (!result.Success) return BadRequest(result.Message);

			return CreatedAtAction("GetProject", new { id = project.ID }, project);
		}

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _projectService.DeleteProjectAsync(id);
            //if (project == null)
            //{
            //    return NotFound();
            //}

            //_context.Projects.Remove(project);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool ProjectExists(Guid id)
        //{
        //	return _context.Projects.Any(e => e.ID == id);
        //}
    }
}