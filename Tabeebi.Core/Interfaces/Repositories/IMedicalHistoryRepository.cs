using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Core.Entities;

namespace Tabeebi.Core.Interfaces.Repositories
{
    public interface IMedicalHistoryRepository
    {
        Task<MedicalHistory> AddMedicalHistoryAsync(MedicalHistoryDto history);
        Task<MedicalHistory?> GetByPatientIdAsync(Guid patientId);
        Task UpdateMedicalHistoryAsync(MedicalHistory history);
    }
}