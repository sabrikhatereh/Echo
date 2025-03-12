# Echo
A global echo of voices and ideas.

## **Overview**

This solution is a modern, scalable, and modular application designed with extensibility and maintainability in mind. 
It incorporates industry best practices and patterns such as **Onion Architecture**, **CQRS**, and **FluentValidation**, while leveraging powerful tools and libraries like **Serilog**, **EasyCache**, **MediatR**, **Swagger**, and robust testing frameworks such as **xUnit** and **Moq**.

---

## **Key Features**

### **1. Logging with Serilog**
- **Current Implementation**:
  - Logs application events and errors to the console.
- **Future-Ready**:
  - Configurable to log to centralized observability tools such as **Logstash**, **Sentry**, or **Elastic Stack**.
- **Benefits**:
  - Enables clear and structured logging, facilitating easier debugging and monitoring.

---

### **2. Caching with EasyCache**
- **Current Implementation**:
  - Uses EasyCache for in-memory caching to improve performance.
- **Scalable Design**:
  - Retains flexibility to switch to a **distributed caching system** like **Redis** by modifying the configuration.
  - Supports multi-instance deployments for scaling by centralizing the cache.

---

### **3. Command and Query Handling with MediatR**
- **Implementation**:
  - Utilizes **MediatR** for managing commands, queries, and domain events in a decoupled manner.
- **Advantages**:
  - Simplifies request-response handling.
  - Promotes a clean and maintainable architecture by separating business logic from infrastructure concerns.

---

### **4. Onion Architecture**
- **Layered Design**:
  - Implements **Onion Architecture** for clear separation of concerns:
    - **Core Layer**: Contains domain entities, interfaces, and core logic.
    - **Application Layer**: Handles business rules, command/query processing, and validation.
    - **Infrastructure Layer**: Manages database contexts, repositories, and external dependencies.
    - **Presentation Layer**: Hosts API endpoints for client interactions.

---

### **5. Validation with FluentValidation**
- **Implementation**:
  - Integrated **FluentValidation** for request validation.
  - Added a custom **`RequestValidationBehavior`** preprocessor to the MediatR pipeline for centralized validation logic.
- **Advantages**:
  - Ensures consistent validation across the application while keeping the logic clean and reusable.

---

### **6. Database Management**
- **Separated Command and Query Repositories**:
  - Distinct repositories for **command** (write) and **query** (read) operations, adhering to CQRS principles.
  - `ApplicationReadDb`: Handles read queries.
  - `EchoWriteDbContext`: Manages write operations.
- **Future-Ready**:
  - Configuration can be adjusted to enable reading from a different source (e.g., a separate read database) without significant code changes.
- **Unique Constraints**:
  - Implements a unique constraint on `Hash` and `UserId` to prevent duplication.
  - A `HashUniqueness` service enforces this constraint in the `CreateEchoCommandHandler`.

---

### **7. API Versioning**
- **Implementation**:
  - Added **API versioning** to ensure backward compatibility and smooth future updates.
- **Reason for Versioning**:
  - Enables introducing breaking changes in a controlled manner while continuing to support older versions of the API.
  - Facilitates seamless evolution of the API without disrupting existing clients.
- **Versioning in Practice**:
  - Supports versioning through URL path segments (e.g., `/api/v1/resource`).

---

### **8. API Documentation with Swagger**
- **Implementation**:
  - Integrated **Swagger** to generate comprehensive API documentation.
- **Advantages**:
  - Provides a clear, interactive interface for exploring API endpoints.
  - Simplifies client integration by detailing request/response schemas and available endpoints.

---

### **9. Testing with xUnit and Moq**
- **xUnit**:
  - Used for writing unit tests.
- **Moq**:
  - Utilized for mocking dependencies in unit tests.
  - Ensures that the application logic is tested independently of external services or infrastructure.
---

### **10. Custom Error Handling with ProblemDetails**

The solution introduces custom exceptions (e.g., `ConflictException`, `RateLimitException`, `DuplicatedRequestException`) to encapsulate specific application logic errors. `ProblemDetails` is used in the `AddCustomErrorHandlingMiddleware` to map custom exceptions (e.g., `ConflictException`) to structured error responses with relevant HTTP status codes and details. It dynamically checks the environment to include or exclude exception details for better security in production. Unhandled exceptions are logged based on a customisable condition, ensuring clarity for debugging while avoiding unnecessary logs for known exception types like validation errors.

## **Technologies and Tools**

- **Logging**: Serilog
- **Caching**: EasyCache
- **Request Handling**: MediatR
- **Validation**: FluentValidation
- **Architecture**: Onion Architecture, CQRS
- **API Management**: API Versioning, Swagger
- **Database**:
  - ApplicationReadDb (for reads)
  - EchoWriteDbContext (for writes)
- **Testing**: xUnit, Moq

---
### **Handling Forbidden Words**

The **Forbidden Words** feature is encapsulated in a dedicated singleton service, `ForbidWords`, to centralize the logic for validating and managing restricted terms. It is implemented as a **lazy-loaded** to ensure the forbidden words list is fetched only once (e.g., from a database or configuration) and reused across the application, enhancing performance and consistency. Future updates may include loading from an external source, ensuring flexibility and scalability without altering core logic.
## **How to Run the Solution**

1. **Prerequisites**:
   - Install [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0).

2. **Configuration**:
   - Modify `appsettings.json` to configure logging, caching, API versioning, or database connections as needed.

---

For any questions or issues, feel free to contact me.