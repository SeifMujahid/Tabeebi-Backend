# Tabeebi Clinic Management System

## 🏗️ Clean Architecture Setup

This project demonstrates a clean architecture implementation for a clinic management system following senior development best practices.

### 📁 Project Structure

```
Tabeebi.Core/
├── Entities/           # Domain entities
│   ├── Patient.cs
│   ├── Doctor.cs (User.cs)
│   ├── Appointment.cs
│   ├── MedicalRecord.cs
│   ├── Tenant.cs
│   ├── Service.cs
│   ├── ClinicProfile.cs
│   └── InsuranceProvider.cs
├── Enums/             # Domain enums
│   ├── Gender.cs
│   ├── UserRole.cs
│   └── TenantStatus.cs
└── Common/            # Base classes
    ├── BaseEntity.cs
    └── BaseTenantEntity.cs

Tabeebi.Domain/
└── Placeholders.cs    # Future: Business logic, services, DTOs

Tabeebi.Infrastructure/
└── Placeholders.cs    # Future: EF Core, repositories, data access

Tabeebi.API/
├── Program.cs         # Application entry point
├── appsettings.json   # Configuration
└── Tabeebi.API.csproj # Project file

Tabeebi.UnitTests/     # Future: Unit tests
```

### 🎯 Architecture Layers

#### **Core Layer** (`Tabeebi.Core`)
- **Responsibility**: Domain entities and enums only
- **Dependencies**: None
- **Contains**: Entity classes, base classes, enums

#### **Domain Layer** (`Tabeebi.Domain`)
- **Responsibility**: Business logic, services, DTOs (future)
- **Dependencies**: Tabeebi.Core
- **Future**: Service interfaces, implementations, validation

#### **Infrastructure Layer** (`Tabeebi.Infrastructure`)
- **Responsibility**: Data access, external services (future)
- **Dependencies**: Tabeebi.Core
- **Future**: EF Core DbContext, repositories

#### **API Layer** (`Tabeebi.API`)
- **Responsibility**: Web API, controllers, endpoints
- **Dependencies**: All other layers
- **Contains**: Minimal API setup with test endpoint

### 🚀 Getting Started

1. **Build the project:**
   ```bash
   dotnet build Tabeebi.API/Tabeebi.API.csproj
   ```

2. **Run the application:**
   ```bash
   dotnet run --project Tabeebi.API
   ```

3. **Access Swagger UI:**
   - Navigate to `https://localhost:7xxx` (development)
   - Or access `/swagger` directly

### 🧪 Test Endpoint

The application includes a simple test endpoint:

- **URL**: `/api/test`
- **Method**: GET
- **Response**: Architecture information and status

**Sample Response:**
```json
{
  "message": "Tabeebi Clinic Management API - Clean Architecture Test",
  "version": "1.0.0",
  "architecture": "Clean Architecture",
  "layers": [
    "Core (Entities)",
    "Domain (Business Logic)",
    "Infrastructure (Data Access)",
    "API (Controllers & Endpoints)"
  ],
  "timestamp": "2025-01-01T12:00:00.000Z"
}
```

### 📋 Development Guidelines

#### Naming Conventions
- **PascalCase** for classes, interfaces, methods, properties
- **camelCase** for variables, parameters, private fields
- **UPPER_CASE** for constants
- Interface names start with **I** prefix
- Async methods end with **Async** suffix

#### Clean Architecture Rules
- **Core**: Only domain entities, no business logic
- **Domain**: Business logic, services, DTOs
- **Infrastructure**: Data access, external services
- **API**: Controllers, endpoints, middleware

#### Dependencies
- **Core**: No dependencies
- **Domain**: References Core only
- **Infrastructure**: References Core only
- **API**: References all layers

### 🔧 Future Implementation

This setup provides the foundation for implementing:

- ✅ **Entity Framework Core** with proper repository pattern
- ✅ **AutoMapper** for DTO mapping
- ✅ **FluentValidation** for input validation
- ✅ **JWT Authentication** with role-based authorization
- ✅ **Multi-tenant support** with proper data isolation
- ✅ **Comprehensive API endpoints** for all CRUD operations
- ✅ **Unit and integration tests**
- ✅ **Database migrations**

### 📦 Package References

#### **Tabeebi.Core**
- Minimal dependencies (no external packages)

#### **Tabeebi.Domain**
- Tabeebi.Core reference only

#### **Tabeebi.Infrastructure**
- Microsoft.EntityFrameworkCore.SqlServer

#### **Tabeebi.API**
- Microsoft.AspNetCore.OpenApi
- Swashbuckle.AspNetCore

---

**Status**: ✅ Clean architecture foundation established and tested
**Next Steps**: Implement services, repositories, and API endpoints as needed