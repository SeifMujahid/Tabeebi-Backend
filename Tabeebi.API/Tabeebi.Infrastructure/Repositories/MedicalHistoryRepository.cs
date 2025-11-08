using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tabeebi.Core.Entities;
using Tabeebi.Core.Interfaces.Repositories;
using Tabeebi.Domain.DTOs.Patient;
using Tabeebi.Infrastructure.Data;

namespace Tabeebi.Infrastructure.Repositories
{
    public class MedicalHistoryRepository : IMedicalHistoryRepository
    {
        private readonly AppDbContext _context;

        public MedicalHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalHistory> AddMedicalHistoryAsync(MedicalHistoryDto history)
        {
            var entity = new MedicalHistory
            {
                PatientId = history.PatientId,
                DoctorId = history.DoctorId,
                BloodType = history.BloodType,
                Allergies = history.Allergies,
                Medications = history.Medications,
                FamilyMedicalHistory = history.FamilyMedicalHistory,
                ChronicConditions = history.ChronicConditions,
                PreviousSurgeries = history.PreviousSurgeries,
                CreatedAt = DateTime.UtcNow
            };
            _context.MedicalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MedicalHistory?> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.MedicalHistories.FirstOrDefaultAsync(h => h.PatientId == patientId);
        }

        public async Task UpdateMedicalHistoryAsync(MedicalHistory history)
        {
            _context.MedicalHistories.Update(history);
            await _context.SaveChangesAsync();
        }
    }
}