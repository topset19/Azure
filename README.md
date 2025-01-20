# Peikko Precast Wall Designer Backend

## *Overview*

The **Peikko Precast Wall Designer Backend** is a monolithic controller-based backend built using Clean Architecture principles. This project ensures scalability, maintainability, and testability by separating concerns into distinct layers, enabling independent development and testing.

## ✨*Key Features*

- **Monolithic Design**: Centralized logic with well-defined responsibilities.

- **ASP.NET Core + C#**: Built with modern technologies for performance and reliability.

- **Dependency Injection**: Ensures flexible and testable code.

- **Azure Functions**: for complex computations.

- **Azure Service Bus**: for message-based communication

- **CosmosDB**: data persistence

## 📁 *Project Structure*

The solution is divided into several layers, each with a specific responsibility:

```
src/
└── PeikkoPrecastWallDesigner.Api
└── PeikkoPrecastWallDesigner.Application
└── PeikkoPrecastWallDesigner.Background
└── PeikkoPrecastWallDesigner.Domain
└── PeikkoPrecastWallDesigner.Infrastructure
└── PeikkoPrecastWallDesigner.UnitTests
```

## 🛠*Layers Description*

**1. Api Layer**

- **Location**: `src/PeikkoPrecastWallDesigner.Api`

- **Purpose**: Hosts the API endpoints and serves as the entry point for external clients.

- **Key Components**:

  - **Controllers**: Define HTTP endpoints for user interaction.

  - **Program.cs**: Configures services, middleware, and dependency injection.

  - **appsettings.json**: configurations for all layers.

**2. Application Layer**

- **Location**: `src/PeikkoPrecastWallDesigner.Application`

- **Purpose**: Contains core business logic and application services.

- **Key Components**:

  - **DependencyInjection.cs**: Registers application-level services.

  - **Common**: Shared utilities and helpers.

  - **Computations**: Business logic for complex calculations.

**3. Background Layer**

- **Location**: `src/PeikkoPrecastWallDesigner.Background`

- **Purpose**: Handles background services and long-running tasks.

- **Key Components**:

  - Not implemented. This one is intended for future usage.

**4. Domain Layer**

- **Location**: `src/PeikkoPrecastWallDesigner.Domain`

- **Purpose**: Defines the core entities, enums, value objects, and domain services.

- **Key Components**:

  - **Entities**: Domain models representing real-world concepts. Usually inheritted from class `Entity`  

  - **Services**: `Domain` services used in `Application` services like business logics or computations.

  - **Exceptions**: Custom exceptions for domain errors.
  
  - **Infrastructure**: Define interfaces or base classes shared between the `Infrastructure` and `Application` layers, which will be implemented by the `Infrastructure` layer.

  - **DependencyInjection.cs**: Registers domain-level services.
  

**5. Infrastructure Layer**

- **Location**: `src/PeikkoPrecastWallDesigner.Infrastructure`

- **Purpose**: Implements data access and external services. Every configuration must be performed in here

- **Key Components**:

  - **Confuguration Functions**: Functions that start with `Add...()` located in `DependencyInejction` folders. Use those functions to add new services after adding the correct config in `appsettings.json` file in the `Api` layer.

  - **AppSettings**: A class mapped to `appsettings.json`. 

  - **KeyVaults**: Securely handles sensitive configurations.

  - **Logging**: Configures application logging.

  - **External**: Implement services like `Azure Service Bus`.
 
  - **Persistence**: Implements database interactions.


**6. Unit Tests**

- **Location**: `src/PeikkoPrecastWallDesigner.UnitTests`

- **Purpose**: Ensures code correctness.

- **Key Components**:

  - **Computations**: Unit tests for the business logic regarding computations and validations.

  - **Testing Framework**: Uses xUnit for test cases.

## 🧩Clean Architecture Principles

This project adheres to the **Clean Architecture** principles, ensuring:

- **Separation of Concerns**: Layers are decoupled to reduce dependencies.

- **Testability**: Each layer can be tested independently.

- **Scalability**: Support future migration to modular monolith.

### 🔗Layered Dependencies

- API Layer → Application Layer

- Application Layer → Domain Layer

- Infrastructure Layer → Application & Domain Layers

🚫 **DON'T** change this dependency rule. Those projects are referencing like this to adhere the above priciples.