using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using ReservationService.Data;
using ReservationService.DTOs;
using ReservationService.Enums;
using ReservationService.Models;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricePlanController(ReservationServiceContext context,
                                                        IValidator<AddPricePlanDto> addValidator,
                                                        IValidator<EditPricePlanDto> editValidator) : ControllerBase
    {
        private readonly PricePlanService _service = new(context, addValidator, editValidator);

        [HttpPost]
        public async Task<IActionResult> Post(AddPricePlanDto model)
        {
            Result result = await _service.Add(model);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Put(EditPricePlanDto model)
        {
            Result result = await _service.Edit(model);
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
        public async Task<IActionResult> Get(PricePlanFilterDto model)
        {
            Result result = await _service.GetAll(model);
            return StatusCode(result.StatusCode, result.Data);
        }


        [HttpPatch]
        [Route("[controller]/Patch/{id}/{status}")]
        public async Task<IActionResult> Patch(Guid id, PricePlanStatus status)
        {
            Result result = await _service.ChangeStatus(id, status);
            return StatusCode(result.StatusCode, result.Data);
        }


        [HttpDelete]
        [Route("[controller]/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Result result = await _service.Delete(id);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}