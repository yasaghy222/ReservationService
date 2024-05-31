using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ReservationService.Data;
using ReservationService.DTOs;
using ReservationService.Enums;
using ReservationService.Models;
using ReservationService.Shared;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController(ReservationServiceContext context,
                                                                IValidator<AddReservationDto> addValidator) : ControllerBase
    {
        private readonly ReservationService _service = new(context, addValidator);

        [HttpPost]
        public async Task<IActionResult> Post(AddReservationDto model)
        {
            Result result = await _service.Add(model);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet]
        [Route("[controller]/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Result result = await _service.Get(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet]
        [Route("[controller]/Get")]
        public async Task<IActionResult> Get(ReservationFilterDto model)
        {
            Result result = await _service.GetAll(model);
            return StatusCode(result.StatusCode, result.Data);
        }


        [HttpPatch]
        [Route("[controller]/Patch/{id}/{status}")]
        public async Task<IActionResult> Patch(Guid id, ReservationStatus status)
        {
            Result result = await _service.ChangeStatus(id, status);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}