using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/autocomplete")]
    public class AutocompleteController : ControllerBase
    {
        private readonly IAutocompleteService _autoCompService;

        public AutocompleteController(IAutocompleteService autoCompService)
        {
            _autoCompService = autoCompService;
        }
        [HttpGet]
        [Route("diseases")]
        [Authorize(Roles = "DOCTOR, SECRETARY")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AutocompleteDiseases()
        {
            try
            {
                var response = await _autoCompService.AutocompleteDiseases();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpGet]
        [Route("products")]
        [Authorize(Roles = "DOCTOR, SECRETARY")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AutocompleteProducts()
        {
            try
            {
                var response = await _autoCompService.AutocompleteProducts();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpGet]
        [Route("meals")]
        [Authorize(Roles = "DOCTOR, SECRETARY")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AutocompleteMeals()
        {
            try
            {
                var response = await _autoCompService.AutocompleteMeals();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpGet]
        [Route("patients")]
        [Authorize(Roles = "DOCTOR, SECRETARY")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AutocompletePatients()
        {
            try
            {
                var response = await _autoCompService.AutocompletePatients();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpGet]
        [Route("doctors")]
        [Authorize(Roles = "DOCTOR, SECRETARY")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AutocompleteDoctors()
        {
            try
            {
                var response = await _autoCompService.AutocompleteDoctors();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpGet]
        [Route("supplements")]
        [Authorize(Roles = "DOCTOR, SECRETARY")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AutocompleteSupplements()
        {
            try
            {
                var response = await _autoCompService.AutocompleteSupplements();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        } 
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "DOCTOR, SECRETARY, ADMIN")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AutocompleteUsers()
        {
            try
            {
                var response = await _autoCompService.AutocompleteUsers();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        } 
    }
}