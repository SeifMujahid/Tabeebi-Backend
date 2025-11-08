using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Domain.DTOs.Patient;

namespace Tabeebi.Domain.Interfaces
{
    public interface IPatientService
    {
        Task<MedicalHistoryResponseDto> UpdateMedicalHistoryAsync(Guid patientId, UpdateMedicalHistoryDto dto, string currentUserId);
    }
}