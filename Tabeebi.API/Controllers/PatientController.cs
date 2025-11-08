using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tabeebi.Domain.DTOs.Patient;
using Tabeebi.Domain.Interfaces;

namespace Tabeebi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPut("{patientId:guid}")]
        public async Task<ActionResult<MedicalHistoryResponseDto>> UpdateMedicalHistory(Guid patientId, [FromBody] UpdateMedicalHistoryDto dto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            try
            {
                var result = await _patientService.UpdateMedicalHistoryAsync(patientId, dto, currentUserId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only doctors can update medical history");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}