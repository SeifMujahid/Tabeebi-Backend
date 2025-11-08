# Medical History Management System - Complete Documentation

---

## System Overview

This system manages patient medical histories in a healthcare application called **Tabeebi**.
It follows a clean architecture pattern with clear separation of concerns across multiple layers:

- **API Layer**: Handles HTTP requests and responses
- **Domain/Service Layer**: Contains business logic and validation
- **Infrastructure Layer**: Manages data access and persistence
- **Core Layer**: Defines entities and interfaces

---

## Architecture

### Layered Architecture

```
┌─────────────────────────────────────────┐
│         API Layer (Controllers)          │
│     - PatientController.cs               │
└────────────────┬────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────┐
│      Domain/Service Layer                │
│     - PatientService.cs                  │
│     - IPatientService.cs                 │
│     - DTOs (Request/Response objects)    │
└────────────────┬────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────┐
│      Infrastructure Layer                │
│     - MedicalHistoryRepository.cs        │
│     - AppDbContext.cs (EF Core)          │
└────────────────┬────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────┐
│         Core Layer                       │
│     - MedicalHistory.cs (Entity)         │
│     - Repository Interfaces              │
└─────────────────────────────────────────┘
```

---

## Entity Models

### MedicalHistory.cs

**Location**: `Tabeebi.Core.Entities`

This is the core entity representing a patient's complete medical history.

#### Properties

| Property               | Type        | Description                                                         |
| ---------------------- | ----------- | ------------------------------------------------------------------- |
| `PatientId`            | `Guid`      | **Primary Key** - References UserProfile with ProfileType = Patient |
| `DoctorId`             | `Guid`      | ID of the doctor who last updated the record                        |
| `BloodType`            | `string?`   | Patient's blood type (e.g., "A+", "O-")                             |
| `Allergies`            | `string?`   | Known allergies with severity levels                                |
| `Medications`          | `string?`   | Current medications the patient is taking                           |
| `FamilyMedicalHistory` | `string?`   | Family medical history and genetic conditions                       |
| `ChronicConditions`    | `string?`   | Long-term health conditions requiring monitoring                    |
| `PreviousSurgeries`    | `string?`   | Past surgical procedures with dates                                 |
| `CreatedAt`            | `DateTime`  | Timestamp when the record was first created                         |
| `UpdatedAt`            | `DateTime?` | Timestamp of the last update (nullable)                             |

#### Key Design Decisions

1. **PatientId as Primary Key**: Each patient has exactly one medical history record
2. **Nullable Fields**: Medical information may not be complete initially
3. **Audit Trail**: `CreatedAt` and `UpdatedAt` track when records are modified
4. **String Storage**: Medical details stored as strings for flexibility (could be normalized in future versions)

---

## Data Transfer Objects (DTOs)

DTOs are used to transfer data between layers and provide a clean API contract.

### MedicalHistoryDto.cs

**Purpose**: Used when creating a new medical history record

```csharp
public class MedicalHistoryDto
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public string? BloodType { get; set; }
    public string? Allergies { get; set; }
    public string? Medications { get; set; }
    public string? FamilyMedicalHistory { get; set; }
    public string? ChronicConditions { get; set; }
    public string? PreviousSurgeries { get; set; }
}
```

### UpdateMedicalHistoryDto.cs

**Purpose**: Used for partial updates (all fields optional)

```csharp
public class UpdateMedicalHistoryDto
{
    public string? BloodType { get; set; }
    public string? Allergies { get; set; }
    public string? Medications { get; set; }
    public string? FamilyMedicalHistory { get; set; }
    public string? ChronicConditions { get; set; }
    public string? PreviousSurgeries { get; set; }
}
```

**Key Feature**: Only includes fields that can be updated. PatientId and DoctorId are handled separately.

### MedicalHistoryResponseDto.cs

**Purpose**: Enriched response returned to API clients

```csharp
public class MedicalHistoryResponseDto
{
    public Guid PatientId { get; set; }
    public string PatientName { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
    public string? BloodType { get; set; }
    public string? Allergies { get; set; }
    public string? Medications { get; set; }
    public string? FamilyMedicalHistory { get; set; }
    public string? ChronicConditions { get; set; }
    public string? PreviousSurgeries { get; set; }
}
```

**Enhancement**: Includes human-readable names for patient and doctor (not stored in database).

---

## Repository Pattern

### IMedicalHistoryRepository.cs

**Location**: `Tabeebi.Core.Interfaces.Repositories`

Defines the contract for data access operations.

```csharp
public interface IMedicalHistoryRepository
{
    Task<MedicalHistory> AddMedicalHistoryAsync(MedicalHistoryDto history);
    Task<MedicalHistory?> GetByPatientIdAsync(Guid patientId);
    Task UpdateMedicalHistoryAsync(MedicalHistory history);
}
```

### MedicalHistoryRepository.cs

**Location**: `Tabeebi.Infrastructure.Repositories`

Implements data access using Entity Framework Core.

#### Methods Explained

##### 1. AddMedicalHistoryAsync

```csharp
public async Task<MedicalHistory> AddMedicalHistoryAsync(MedicalHistoryDto history)
```

**What it does**:

- Converts DTO to entity
- Sets `CreatedAt` timestamp
- Adds to database context
- Saves changes
- Returns the created entity

**Usage**: When a patient's first medical history record is created.

##### 2. GetByPatientIdAsync

```csharp
public async Task<MedicalHistory?> GetByPatientIdAsync(Guid patientId)
```

**What it does**:

- Queries database for medical history by patient ID
- Returns null if not found
- Uses `FirstOrDefaultAsync` for async operation

**Usage**: Check if a patient already has a medical history before creating/updating.

##### 3. UpdateMedicalHistoryAsync

```csharp
public async Task UpdateMedicalHistoryAsync(MedicalHistory history)
```

**What it does**:

- Marks entity as modified in EF Core
- Saves changes to database

**Usage**: When updating an existing medical history record.

### AppDbContext.cs

**Location**: `Tabeebi.Infrastructure.Data`

Entity Framework Core database context.

```csharp
public class AppDbContext : DbContext
{
    public DbSet<MedicalHistory> MedicalHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedicalHistory>()
            .HasKey(mh => mh.PatientId);
    }
}
```

**Configuration**:

- Sets `PatientId` as the primary key
- Creates `MedicalHistories` table in database

---

## Service Layer

### IPatientService.cs

**Location**: `Tabeebi.Domain.Interfaces`

Service contract defining business operations.

```csharp
public interface IPatientService
{
    Task<MedicalHistoryResponseDto> UpdateMedicalHistoryAsync(
        Guid patientId,
        UpdateMedicalHistoryDto dto,
        string currentUserId
    );
}
```

### PatientService.cs

**Location**: `Tabeebi.Domain.Services`

Core business logic for medical history management.

#### UpdateMedicalHistoryAsync - Detailed Flow

```csharp
public async Task<MedicalHistoryResponseDto> UpdateMedicalHistoryAsync(
    Guid patientId,
    UpdateMedicalHistoryDto dto,
    string currentUserId
)
```

**Step-by-Step Process**:

##### Step 1: Authentication & Authorization

```csharp
var doctorId = Guid.Parse(currentUserId);
var doctor = await _userProfileRepository.GetByIdentityUserIdAsync(doctorId);

if (doctor == null || doctor.ProfileType != UserProfileType.Doctor)
{
    throw new UnauthorizedAccessException("Only doctors can update medical history");
}
```

- Converts user ID from string to Guid
- Retrieves doctor's profile from database
- **Validates**: User exists AND has Doctor role
- **Throws exception** if validation fails

##### Step 2: Patient Validation

```csharp
var patient = await _userProfileRepository.GetByIdentityUserIdAsync(patientId);

if (patient == null || patient.ProfileType != UserProfileType.Patient)
{
    throw new ArgumentException("Invalid patient ID");
}
```

- Retrieves patient profile
- **Validates**: Patient exists AND has Patient role
- **Throws exception** if invalid

##### Step 3: Check Existing History

```csharp
var history = await _medicalHistoryRepository.GetByPatientIdAsync(patientId);
bool isNew = history == null;
```

- Queries for existing medical history
- Determines if this is a create or update operation

##### Step 4: Create or Update Entity

```csharp
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
```

- **If new**: Initialize new entity with required fields
- **If existing**: Update doctor ID and timestamp

##### Step 5: Apply Partial Updates

```csharp
if (dto.BloodType != null) history.BloodType = dto.BloodType;
if (dto.Allergies != null) history.Allergies = dto.Allergies;
if (dto.Medications != null) history.Medications = dto.Medications;
// ... etc
```

- **Only updates non-null fields** from DTO
- Preserves existing values if not provided in update
- Enables partial updates without overwriting data

##### Step 6: Persist to Database

```csharp
if (isNew)
{
    var historyDto = new MedicalHistoryDto { /* map properties */ };
    await _medicalHistoryRepository.AddMedicalHistoryAsync(historyDto);
}
else
{
    await _medicalHistoryRepository.UpdateMedicalHistoryAsync(history);
}
```

- Chooses appropriate repository method
- Saves changes to database

##### Step 7: Return Response

```csharp
return new MedicalHistoryResponseDto
{
    PatientId = patientId,
    PatientName = $"{patient.FirstName} {patient.LastName}",
    DoctorId = doctorId,
    DoctorName = $"{doctor.FirstName} {doctor.LastName}",
    // ... medical fields
};
```

- Constructs enriched response
- Includes human-readable names
- Returns complete medical history data

---

## API Controller

### PatientController.cs

**Location**: `Tabeebi.API.Controllers`

RESTful API endpoint for medical history operations.

#### Endpoint Details

**Route**: `PUT /api/patient/{patientId:guid}`

**Request Example**:

```http
PUT /api/patient/123e4567-e89b-12d3-a456-426614174000
Authorization: Bearer <token>
Content-Type: application/json

{
  "bloodType": "A+",
  "allergies": "Penicillin - Severe",
  "medications": "Aspirin 100mg daily"
}
```

**Response Example** (Success - 200 OK):

```json
{
  "patientId": "123e4567-e89b-12d3-a456-426614174000",
  "patientName": "John Doe",
  "doctorId": "987e6543-e21b-12d3-a456-426614174000",
  "doctorName": "Dr. Jane Smith",
  "bloodType": "A+",
  "allergies": "Penicillin - Severe",
  "medications": "Aspirin 100mg daily",
  "familyMedicalHistory": null,
  "chronicConditions": null,
  "previousSurgeries": null
}
```

#### Error Responses

| Status Code               | Scenario                | Response                                  |
| ------------------------- | ----------------------- | ----------------------------------------- |
| 401 Unauthorized          | No authentication token | "Unauthorized"                            |
| 401 Unauthorized          | User is not a doctor    | "Only doctors can update medical history" |
| 400 Bad Request           | Invalid patient ID      | Error message from service                |
| 500 Internal Server Error | Unexpected exception    | "An unexpected error occurred."           |

#### Controller Logic

```csharp
[HttpPut("{patientId:guid}")]
public async Task<ActionResult<MedicalHistoryResponseDto>> UpdateMedicalHistory(
    Guid patientId,
    [FromBody] UpdateMedicalHistoryDto dto
)
{
    // 1. Extract current user ID from JWT token
    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (currentUserId == null)
    {
        return Unauthorized();
    }

    try
    {
        // 2. Call service layer
        var result = await _patientService.UpdateMedicalHistoryAsync(
            patientId, dto, currentUserId
        );
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
    catch (Exception)
    {
        return StatusCode(500, "An unexpected error occurred.");
    }
}
```

**Security**:

- Uses `[ApiController]` attribute for automatic model validation
- Extracts user identity from claims (JWT token)
- Delegates authorization to service layer
- Provides appropriate HTTP status codes

---

## Unit Tests

### PatientServiceTests.cs

**Location**: `Tabeebi.Tests.ServicesTests`

Comprehensive test suite using **xUnit** and **Moq** frameworks.

#### Test Structure

Each test follows the **AAA Pattern**:

- **Arrange**: Set up mocks and test data
- **Act**: Execute the method under test
- **Assert**: Verify expected outcomes

#### Test Cases Breakdown

##### 1. UpdateMedicalHistoryAsync_WhenDoctorNotFound_ThrowsUnauthorizedAccessException

**Scenario**: User ID doesn't exist in database

**Setup**:

```csharp
_mockUserProfileRepo.Setup(x => x.GetByIdentityUserIdAsync(doctorId, default))
    .ReturnsAsync((UserProfile?)null);
```

**Expectation**: `UnauthorizedAccessException` with message "Only doctors can update medical history"

**Purpose**: Ensure invalid users cannot access the system

---

##### 2. UpdateMedicalHistoryAsync_WhenUserIsNotDoctor_ThrowsUnauthorizedAccessException

**Scenario**: User exists but is not a doctor (e.g., Patient, Receptionist)

**Setup**:

```csharp
var nonDoctorProfile = new UserProfile
{
    ProfileType = UserProfileType.Patient // Not a doctor!
};
```

**Expectation**: `UnauthorizedAccessException`

**Purpose**: Enforce role-based access control

---

##### 3. UpdateMedicalHistoryAsync_WhenPatientNotFound_ThrowsArgumentException

**Scenario**: Valid doctor tries to update non-existent patient

**Setup**:

- Doctor profile exists and is valid
- Patient profile returns null

**Expectation**: `ArgumentException` with message "Invalid patient ID"

**Purpose**: Validate patient exists before proceeding

---

##### 4. UpdateMedicalHistoryAsync_WhenPatientProfileIsNotPatientType_ThrowsArgumentException

**Scenario**: Target user is not actually a patient

**Setup**:

```csharp
var nonPatientProfile = new UserProfile
{
    ProfileType = UserProfileType.Receptionist
};
```

**Expectation**: `ArgumentException`

**Purpose**: Ensure medical history is only created for actual patients

---

##### 5. UpdateMedicalHistoryAsync_WhenCreatingNewHistory_CallsAddMedicalHistoryAsync

**Scenario**: First time creating medical history for a patient

**Setup**:

- Valid doctor and patient
- No existing medical history (`GetByPatientIdAsync` returns null)

**Verification**:

```csharp
_mockMedicalHistoryRepo.Verify(x => x.AddMedicalHistoryAsync(
    It.Is<MedicalHistoryDto>(h =>
        h.PatientId == patientId &&
        h.DoctorId == doctorId &&
        h.BloodType == dto.BloodType
        // ... etc
    )
), Times.Once);
```

**Assertions**:

- `AddMedicalHistoryAsync` called exactly once
- All DTO fields mapped correctly
- Response contains patient and doctor names
- All medical fields match input

**Purpose**: Verify create operation works correctly

---

##### 6. UpdateMedicalHistoryAsync_WhenUpdatingExistingHistory_CallsUpdateMedicalHistoryAsync

**Scenario**: Updating existing medical history

**Setup**:

- Valid doctor and patient
- Existing medical history returned from repository
- DTO contains partial updates

**Verification**:

```csharp
_mockMedicalHistoryRepo.Verify(x => x.UpdateMedicalHistoryAsync(
    It.Is<MedicalHistory>(h =>
        h.DoctorId == doctorId && // Doctor ID updated
        h.BloodType == dto.BloodType && // Field updated
        h.Medications == "Old medications" && // Old value retained
        h.UpdatedAt != null // Timestamp set
    )
), Times.Once);
```

**Assertions**:

- `UpdateMedicalHistoryAsync` called (not Add)
- Updated fields have new values
- Non-updated fields retain old values
- Doctor ID updated to current doctor
- `UpdatedAt` timestamp is set

**Purpose**: Verify update operation and field preservation

---

##### 7. UpdateMedicalHistoryAsync_WhenUpdatingWithNullValues_OnlyUpdatesNonNullFields

**Scenario**: Partial update with some null fields

**Setup**:

```csharp
var dto = new UpdateMedicalHistoryDto
{
    BloodType = "B+",    // Update this
    Allergies = null,     // Don't update
    Medications = null    // Don't update
};

var existingHistory = new MedicalHistory
{
    BloodType = "O+",
    Allergies = "Existing allergies",
    Medications = "Existing medications"
};
```

**Assertions**:

```csharp
Assert.Equal("B+", result.BloodType);                      // Updated
Assert.Equal("Existing allergies", result.Allergies);       // Preserved
Assert.Equal("Existing medications", result.Medications);   // Preserved
```

**Purpose**: Ensure null values don't overwrite existing data (critical for partial updates)

---

##### 8. UpdateMedicalHistoryAsync_ReturnsCorrectResponseDto

**Scenario**: Verify complete response structure

**Assertions**:

- `PatientId` matches input
- `PatientName` formatted as "FirstName LastName"
- `DoctorId` matches current user
- `DoctorName` formatted correctly
- All medical fields returned correctly

**Purpose**: Validate API contract and response format

---

#### Mock Setup Patterns

##### Repository Mock Pattern

```csharp
_mockMedicalHistoryRepo.Setup(x => x.GetByPatientIdAsync(patientId))
    .ReturnsAsync(existingHistory);
```

##### Verification Pattern

```csharp
_mockMedicalHistoryRepo.Verify(
    x => x.UpdateMedicalHistoryAsync(It.IsAny<MedicalHistory>()),
    Times.Once
);
```

##### Advanced Matching Pattern

```csharp
_mockMedicalHistoryRepo.Verify(
    x => x.AddMedicalHistoryAsync(
        It.Is<MedicalHistoryDto>(h =>
            h.PatientId == patientId &&
            h.DoctorId == doctorId
        )
    ),
    Times.Once
);
```
