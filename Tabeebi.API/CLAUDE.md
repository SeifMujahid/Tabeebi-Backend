# AI Assistant Rules - Senior Development Guidelines

## 🎯 Role
Provide senior-level development assistance for the Tabeebi Clinic Management System. Focus on practical, clean solutions without over-engineering.

## 🔧 Core Development Rules

### Code Standards & Naming Conventions
- **PascalCase** for classes, interfaces, methods, properties
- **camelCase** for variables, parameters, private fields
- **UPPER_CASE** for constants
- Use **meaningful, descriptive names** - avoid abbreviations (e.g., `PatientService` not `PatSvc`)
- Interface names start with **I** prefix: `IPatientRepository`
- Private fields use **_** prefix: `_patientRepository`
- Async methods end with **Async** suffix: `GetPatientByIdAsync`
- Keep lines under **120 characters**
- One class/interface per file
- File names match class names

### Clean Architecture Implementation
```
Core Layer: Entities, repository interfaces ONLY
Domain Layer: Service interfaces, DTOs, service implementations, business logic
Infrastructure Layer: EF Core DbContext, repository implementations, external services
API Layer: Controllers, middleware, exception handlers
```

### Multi-Tenancy
- Implement schema-per-tenant separation
- Ensure proper tenant data isolation
- Use tenant identification middleware correctly
- Handle cross-tenant operations securely

### Database & EF Core
- Use Entity Framework Core with proper patterns
- Write efficient queries with proper indexing
- Implement appropriate relationships and constraints
- Use migrations for schema changes

## 🔒 Security Requirements

### Authentication & Authorization
- Implement JWT authentication properly
- Support 5 user roles: Super Admin, Clinic Owner, Doctor, Receptionist, Patient
- Use role-based access control for all endpoints
- Validate permissions at controller and service levels

### Data Protection
- Maintain tenant data isolation at all times
- Use parameterized queries to prevent SQL injection
- Validate input data properly
- Implement proper error handling without information leakage

## 🏗️ Architecture Guidelines

### Layer Responsibilities
- **Core**: Domain entities, repository interfaces (NO business logic, no services)
- **Domain**: Business logic, validation, DTOs, service interfaces and implementations
- **Infrastructure**: Data access, EF Core, repository implementations
- **API**: Controllers, middleware, exception handling, authentication

### Dependency Injection
- Use constructor injection properly
- Register services in correct lifetimes (Singleton, Scoped, Transient)
- Keep dependencies minimal and focused
- Use extension methods for service registration

## 🧪 Testing Requirements
- Write unit tests for business logic in Domain layer
- Test integration points with Infrastructure layer
- Achieve good coverage for critical business operations
- Keep tests simple and focused

## 🔄 Workflow Rules

### Git Flow
- `main`: Production-ready code (protected)
- `develop`: Integration branch
- `feature/*`: Individual feature development
- Create pull requests for review before merging

### Code Review
- Ensure code follows naming conventions and patterns
- Check for proper error handling
- Verify security implementations
- Confirm tenant isolation
- Validate separation of concerns

## 📋 Senior Development Guidelines

### Do's ✅
- Keep solutions simple and focused
- Write self-documenting code with clear naming
- Follow established patterns and conventions
- Handle errors gracefully with proper logging
- Consider performance and scalability
- Use dependency injection correctly
- Implement proper validation
- Write clean, testable code
- Follow SOLID principles
- Use AutoMapper for object mapping
- Implement proper exception handling

### Don'ts ❌
- Don't over-engineer solutions
- Don't add unnecessary complexity
- Don't copy-paste code without understanding
- Don't ignore security requirements
- Don't break tenant isolation
- Don't use hardcoded values
- Don't write business logic in controllers
- Don't expose entities directly to API layer
- Don't mix concerns between layers
- Don't ignore performance implications
- Don't skip validation

## 🚀 Feature Development

### When Building Features
1. Understand the user role requirements
2. Implement proper tenant isolation
3. Add appropriate validation using FluentValidation
4. Include necessary tests
5. Update API documentation

### API Development
- Use RESTful principles
- Return appropriate HTTP status codes
- Include proper error responses
- Document endpoints clearly with Swagger
- Handle pagination for large datasets
- Use DTOs for all API communication
- Implement proper authentication/authorization

## 📦 Package Management

### Core Layer
- Minimal dependencies
- Only domain-related packages

### Domain Layer
- AutoMapper
- FluentValidation
- Microsoft.Extensions.DependencyInjection.Abstractions

### Infrastructure Layer
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- System.IdentityModel.Tokens.Jwt

### API Layer
- Microsoft.AspNetCore.Authentication.JwtBearer
- Swashbuckle.AspNetCore
- Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore

## 📞 Communication Style
- Provide clear, concise explanations
- Focus on practical solutions
- Explain complex concepts simply
- Suggest improvements when appropriate
- Ask for clarification when requirements are unclear

---

**Guiding Principle**: Build reliable, maintainable software that serves healthcare providers effectively. Keep it simple, secure, and scalable.