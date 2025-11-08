using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Core.Entities;
using Tabeebi.Core.Enums;
using Tabeebi.Core.Interfaces.Repositories;
using Tabeebi.Domain.DTOs.Patient;
using Tabeebi.Domain.Services;

namespace Tabeebi.Tests.ServicesTests
{
    public class PatientServiceTests
    {
        private readonly Mock<IMedicalHistoryRepository> _mockMedicalHistoryRepo;
        private readonly Mock<IUserProfileRepository> _mockUserProfileRepo;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _mockMedicalHistoryRepo = new Mock<IMedicalHistoryRepository>();
            _mockUserProfileRepo = new Mock<IUserProfileRepository>();
            _patientService = new PatientService(_mockMedicalHistoryRepo.Object, _mockUserProfileRepo.Object);
        }

        [Fact]
        public async Task UpdateMedicalHistoryAsync_WhenDoctorNotFound_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();
            var dto = new UpdateMedicalHistoryDto { BloodType = "A+" };

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(doctorId, default))
                .ReturnsAsync((UserProfile?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _patientService.UpdateMedicalHistoryAsync(patientId, dto, doctorId.ToString()));

            Assert.Equal("Only doctors can update medical history", exception.Message);
        }

        [Fact]
        public async Task UpdateMedicalHistoryAsync_WhenUserIsNotDoctor_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var dto = new UpdateMedicalHistoryDto { BloodType = "A+" };

            var nonDoctorProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = userId,
                ProfileType = UserProfileType.Patient,
                FirstName = "Seif",
                LastName = "Allithy"
            };

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(userId, default))
                .ReturnsAsync(nonDoctorProfile);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _patientService.UpdateMedicalHistoryAsync(patientId, dto, userId.ToString()));

            Assert.Equal("Only doctors can update medical history", exception.Message);
        }

        [Fact]
        public async Task UpdateMedicalHistoryAsync_WhenPatientNotFound_ThrowsArgumentException()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();
            var dto = new UpdateMedicalHistoryDto { BloodType = "A+" };

            var doctorProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = doctorId,
                ProfileType = UserProfileType.Doctor,
                FirstName = "Dr. Karim",
                LastName = "Essam"
            };

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(doctorId, default))
                .ReturnsAsync(doctorProfile);

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(patientId, default))
                .ReturnsAsync((UserProfile?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _patientService.UpdateMedicalHistoryAsync(patientId, dto, doctorId.ToString()));

            Assert.Equal("Invalid patient ID", exception.Message);
        }

        [Fact]
        public async Task UpdateMedicalHistoryAsync_WhenPatientProfileIsNotPatientType_ThrowsArgumentException()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();
            var dto = new UpdateMedicalHistoryDto { BloodType = "A+" };

            var doctorProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = doctorId,
                ProfileType = UserProfileType.Doctor,
                FirstName = "Dr. Karim",
                LastName = "Essam"
            };

            var nonPatientProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = patientId,
                ProfileType = UserProfileType.Receptionist,
                FirstName = "Seif",
                LastName = "Allithy"
            };

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(doctorId, default))
                .ReturnsAsync(doctorProfile);

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(patientId, default))
                .ReturnsAsync(nonPatientProfile);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _patientService.UpdateMedicalHistoryAsync(patientId, dto, doctorId.ToString()));

            Assert.Equal("Invalid patient ID", exception.Message);
        }

        [Fact]
        public async Task UpdateMedicalHistoryAsync_WhenCreatingNewHistory_CallsAddMedicalHistoryAsync()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();
            var dto = new UpdateMedicalHistoryDto
            {
                BloodType = "O+",
                Allergies = "Penicillin",
                Medications = "Aspirin",
                FamilyMedicalHistory = "Diabetes",
                ChronicConditions = "Hypertension",
                PreviousSurgeries = "Appendectomy 2020"
            };

            var doctorProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = doctorId,
                ProfileType = UserProfileType.Doctor,
                FirstName = "Dr. Karim",
                LastName = "Essam"
            };

            var patientProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = patientId,
                ProfileType = UserProfileType.Patient,
                FirstName = "Seif",
                LastName = "Allithy"
            };

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(doctorId, default))
                .ReturnsAsync(doctorProfile);

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(patientId, default))
                .ReturnsAsync(patientProfile);

            _mockMedicalHistoryRepo.Setup(x => x.GetByPatientIdAsync(patientId))
                .ReturnsAsync((MedicalHistory?)null);

            var addedHistory = new MedicalHistory
            {
                PatientId = patientId,
                DoctorId = doctorId,
                BloodType = dto.BloodType,
                Allergies = dto.Allergies,
                Medications = dto.Medications,
                FamilyMedicalHistory = dto.FamilyMedicalHistory,
                ChronicConditions = dto.ChronicConditions,
                PreviousSurgeries = dto.PreviousSurgeries,
                CreatedAt = DateTime.UtcNow
            };

            _mockMedicalHistoryRepo.Setup(x => x.AddMedicalHistoryAsync(It.IsAny<MedicalHistoryDto>()))
                .ReturnsAsync(addedHistory);

            // Act
            var result = await _patientService.UpdateMedicalHistoryAsync(patientId, dto, doctorId.ToString());

            // Assert
            _mockMedicalHistoryRepo.Verify(x => x.AddMedicalHistoryAsync(It.Is<MedicalHistoryDto>(h =>
                h.PatientId == patientId &&
                h.DoctorId == doctorId &&
                h.BloodType == dto.BloodType &&
                h.Allergies == dto.Allergies &&
                h.Medications == dto.Medications &&
                h.FamilyMedicalHistory == dto.FamilyMedicalHistory &&
                h.ChronicConditions == dto.ChronicConditions &&
                h.PreviousSurgeries == dto.PreviousSurgeries
            )), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(patientId, result.PatientId);
            Assert.Equal(doctorId, result.DoctorId);
            Assert.Equal("Seif Allithy", result.PatientName);
            Assert.Equal("Dr. Karim Essam", result.DoctorName);
            Assert.Equal(dto.BloodType, result.BloodType);
            Assert.Equal(dto.Allergies, result.Allergies);
        }

        [Fact]
        public async Task UpdateMedicalHistoryAsync_WhenUpdatingExistingHistory_CallsUpdateMedicalHistoryAsync()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();
            var oldDoctorId = Guid.NewGuid();
            var dto = new UpdateMedicalHistoryDto
            {
                BloodType = "AB+",
                Allergies = "Updated allergies"
            };

            var doctorProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = doctorId,
                ProfileType = UserProfileType.Doctor,
                FirstName = "Dr. Karim",
                LastName = "Essam"
            };

            var patientProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = patientId,
                ProfileType = UserProfileType.Patient,
                FirstName = "Seif",
                LastName = "Allithy"
            };

            var existingHistory = new MedicalHistory
            {
                PatientId = patientId,
                DoctorId = oldDoctorId,
                BloodType = "O+",
                Allergies = "Old allergies",
                Medications = "Old medications",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            };

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(doctorId, default))
                .ReturnsAsync(doctorProfile);

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(patientId, default))
                .ReturnsAsync(patientProfile);

            _mockMedicalHistoryRepo.Setup(x => x.GetByPatientIdAsync(patientId))
                .ReturnsAsync(existingHistory);

            _mockMedicalHistoryRepo.Setup(x => x.UpdateMedicalHistoryAsync(It.IsAny<MedicalHistory>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _patientService.UpdateMedicalHistoryAsync(patientId, dto, doctorId.ToString());

            // Assert
            _mockMedicalHistoryRepo.Verify(x => x.UpdateMedicalHistoryAsync(It.Is<MedicalHistory>(h =>
                h.PatientId == patientId &&
                h.DoctorId == doctorId &&
                h.BloodType == dto.BloodType &&
                h.Allergies == dto.Allergies &&
                h.Medications == "Old medications" && // Should retain old value
                h.UpdatedAt != null
            )), Times.Once);

            _mockMedicalHistoryRepo.Verify(x => x.AddMedicalHistoryAsync(It.IsAny<MedicalHistoryDto>()), Times.Never);

            Assert.NotNull(result);
            Assert.Equal(patientId, result.PatientId);
            Assert.Equal(doctorId, result.DoctorId);
            Assert.Equal(dto.BloodType, result.BloodType);
            Assert.Equal(dto.Allergies, result.Allergies);
        }

        [Fact]
        public async Task UpdateMedicalHistoryAsync_WhenUpdatingWithNullValues_OnlyUpdatesNonNullFields()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();
            var dto = new UpdateMedicalHistoryDto
            {
                BloodType = "B+",
                Allergies = null, // Should not update
                Medications = null // Should not update
            };

            var doctorProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = doctorId,
                ProfileType = UserProfileType.Doctor,
                FirstName = "Dr. Karim",
                LastName = "Essam"
            };

            var patientProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = patientId,
                ProfileType = UserProfileType.Patient,
                FirstName = "Seif",
                LastName = "Allithy"
            };

            var existingHistory = new MedicalHistory
            {
                PatientId = patientId,
                DoctorId = Guid.NewGuid(),
                BloodType = "O+",
                Allergies = "Existing allergies",
                Medications = "Existing medications",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            };

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(doctorId, default))
                .ReturnsAsync(doctorProfile);

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(patientId, default))
                .ReturnsAsync(patientProfile);

            _mockMedicalHistoryRepo.Setup(x => x.GetByPatientIdAsync(patientId))
                .ReturnsAsync(existingHistory);

            _mockMedicalHistoryRepo.Setup(x => x.UpdateMedicalHistoryAsync(It.IsAny<MedicalHistory>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _patientService.UpdateMedicalHistoryAsync(patientId, dto, doctorId.ToString());

            // Assert
            Assert.Equal("B+", result.BloodType);
            Assert.Equal("Existing allergies", result.Allergies); // Should retain old value
            Assert.Equal("Existing medications", result.Medications); // Should retain old value
        }

        [Fact]
        public async Task UpdateMedicalHistoryAsync_ReturnsCorrectResponseDto()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();
            var dto = new UpdateMedicalHistoryDto
            {
                BloodType = "A+",
                Allergies = "Peanuts",
                Medications = "Ibuprofen",
                FamilyMedicalHistory = "Heart disease",
                ChronicConditions = "Asthma",
                PreviousSurgeries = "None"
            };

            var doctorProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = doctorId,
                ProfileType = UserProfileType.Doctor,
                FirstName = "Dr. Karim",
                LastName = "Essam"
            };

            var patientProfile = new UserProfile
            {
                Id = Guid.NewGuid(),
                IdentityUserId = patientId,
                ProfileType = UserProfileType.Patient,
                FirstName = "Seif",
                LastName = "Allithy"
            };

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(doctorId, default))
                .ReturnsAsync(doctorProfile);

            _mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(patientId, default))
                .ReturnsAsync(patientProfile);

            _mockMedicalHistoryRepo.Setup(x => x.GetByPatientIdAsync(patientId))
                .ReturnsAsync((MedicalHistory?)null);

            var addedHistory = new MedicalHistory
            {
                PatientId = patientId,
                DoctorId = doctorId,
                BloodType = dto.BloodType,
                Allergies = dto.Allergies,
                Medications = dto.Medications,
                FamilyMedicalHistory = dto.FamilyMedicalHistory,
                ChronicConditions = dto.ChronicConditions,
                PreviousSurgeries = dto.PreviousSurgeries,
                CreatedAt = DateTime.UtcNow
            };

            _mockMedicalHistoryRepo.Setup(x => x.AddMedicalHistoryAsync(It.IsAny<MedicalHistoryDto>()))
                .ReturnsAsync(addedHistory);

            // Act
            var result = await _patientService.UpdateMedicalHistoryAsync(patientId, dto, doctorId.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patientId, result.PatientId);
            Assert.Equal("Seif Allithy", result.PatientName);
            Assert.Equal(doctorId, result.DoctorId);
            Assert.Equal("Dr. Karim Essam", result.DoctorName);
            Assert.Equal("A+", result.BloodType);
            Assert.Equal("Peanuts", result.Allergies);
            Assert.Equal("Ibuprofen", result.Medications);
            Assert.Equal("Heart disease", result.FamilyMedicalHistory);
            Assert.Equal("Asthma", result.ChronicConditions);
            Assert.Equal("None", result.PreviousSurgeries);
        }
    }
}
