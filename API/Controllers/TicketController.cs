﻿using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.ServiceClasses;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;
        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("ticket")]
        public async Task<IActionResult> GetAsync([FromQuery] string id)
        {
            var result = await _ticketService.GetAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetAllAsync([FromQuery] string categoryId)
        {
            var result = await _ticketService.GetAllAsync(categoryId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTicketRequestVM model)
        {
            var result = await _ticketService.CreateAsync(model);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
