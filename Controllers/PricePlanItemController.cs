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
    public class PricePlanItemController(ReservationServiceContext context,
                                                        IValidator<AddPricePlanItemDto> addValidator,
                                                        IValidator<EditPricePlanItemDto> editValidator) : ControllerBase
    {
        private readonly PricePlanItemService _service = new(context, addValidator, editValidator);

        [HttpPost]
        public async Task<IActionResult> Post(AddPricePlanItemDto model)
        {
            Result result = await _service.Add(model);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Put(EditPricePlanItemDto model)
        {
            Result result = await _service.Edit(model);
            return StatusCode(result.StatusCode, result.Data);
        }


        [HttpGet]
        [Route("[controller]/Get/{id}/{type}")]
        public async Task<IActionResult> Get(Guid id, GetPricePlanType type)
        {
            Result result = type switch
            {
                GetPricePlanType.Item => await _service.Get(id),
                GetPricePlanType.List => await _service.GetAll(id),
                _ => await _service.Get(id),
            };
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch]
        [Route("[controller]/Patch/{id}/{status}")]
        public async Task<IActionResult> Patch(Guid id, PricePlanItemStatus status)
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