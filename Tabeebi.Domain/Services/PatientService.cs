using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Core.Entities;
using Tabeebi.Core.Enums;
using Tabeebi.Core.Interfaces.Repositories;
using Tabeebi.Domain.DTOs.Patient;
using Tabeebi.Domain.Interfaces;

namespace Tabeebi.Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly IMedicalHistoryRepository _medicalHistoryRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public PatientService(IMedicalHistoryRepository medicalHistoryRepository, IUserProfileRepository userProfileRepository)
        {
            _medicalHistoryRepository = medicalHistoryRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<MedicalHistoryResponseDto> UpdateMedicalHistoryAsync(Guid patientId, UpdateMedicalHistoryDto dto, string currentUserId)
        {
            var doctorId = Guid.Parse(currentUserId);
            var doctor = await _userProfileRepository.GetByIdentityUserIdAsync(doctorId);
            if (doctor == null || doctor.ProfileType != UserProfileType.Doctor)
            {
                throw new UnauthorizedAccessException("Only doctors can update medical history");
            }

            var patient = await _userProfileRepository.GetByIdentityUserIdAsync(patientId);
            if (patient == null || patient.ProfileType != UserProfileType.Patient)
            {
                throw new ArgumentException("Invalid patient ID");
            }

            var history = await _medicalHistoryRepository.GetByPatientIdAsync(patientId);
            bool isNew = history == null;

            if (isNew)
            {
                history = new MedicalHistory
                {
                    PatientId = patientId,
                    DoctorId = doctorId,
                    CreatedAt = DateTime.UtcNow
                };
            }
            else
            {
                history.DoctorId = doctorId;
                history.UpdatedAt = DateTime.UtcNow;
            }

            if (dto.BloodType != null) history.BloodType = dto.BloodType;
            if (dto.Allergies != null) history.Allergies = dto.Allergies;
            if (dto.Medications != null) history.Medications = dto.Medications;
            if (dto.FamilyMedicalHistory != null) history.FamilyMedicalHistory = dto.FamilyMedicalHistory;
            if (dto.ChronicConditions != null) history.ChronicConditions = dto.ChronicConditions;
            if (dto.PreviousSurgeries != null) history.PreviousSurgeries = dto.PreviousSurgeries;

            if (isNew)
            {
                var historyDto = new MedicalHistoryDto
                {
                    PatientId = history.PatientId,
                    DoctorId = history.DoctorId,
                    BloodType = history.BloodType,
                    Allergies = history.Allergies,
                    Medications = history.Medications,
                    FamilyMedicalHistory = history.FamilyMedicalHistory,
                    ChronicConditions = history.ChronicConditions,
                    PreviousSurgeries = history.PreviousSurgeries
                };
                await _medicalHistoryRepository.AddMedicalHistoryAsync(historyDto);
            }
            else
            {
                await _medicalHistoryRepository.UpdateMedicalHistoryAsync(history);
            }

            return new MedicalHistoryResponseDto
            {
                PatientId = patientId,
                PatientName = $"{patient.FirstName} {patient.LastName}",
                DoctorId = doctorId,
                DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                BloodType = history.BloodType,
                Allergies = history.Allergies,
                Medications = history.Medications,
                FamilyMedicalHistory = history.FamilyMedicalHistory,
                ChronicConditions = history.ChronicConditions,
                PreviousSurgeries = history.PreviousSurgeries
            };
        }
    }
}