# Exercise .NET and React JS 🎯

In this project we will create a CRUD of Users using the folowing indications:

## Technologies

- **Frontend:** React, organized using **Feature-Based Architecture**.
- **Backend:** .NET, implemented with **Hexagonal Architecture** for maintainability and scalability.
- **Database:** SQL Server with its docker file on repository use of Entity **Framework DataFirst**.
- **IaC:** Terraform for infrastructure deployment.


## Arquitecture back end  

Here, we're implementing the Hexagonal Architecture, keeping all the business logic encapsulated within our Domain layer. We also have the interfaces (ports) that the SQL Repository will use, where we implement the exposed interfaces (Adapters).


<img width="411" height="321" alt="Diagrama sin título-HexagonalUserCrud drawio" src="https://github.com/user-attachments/assets/00f48421-2aac-4299-860f-715fdbcc8570" />

"In this example, we can see all the SOLID principles at work:

**S** Single Responsibility: Each module has its specific function.

**O** Open/Closed Principle: Open to extension, closed to modification. This is applied through the ports and adapters that use interfaces and interface implementations. We can easily replace the current database with another, like Redis, Mongo, etc., in this exercise.

**L** Liskov Substitution Principle: The interface implementations focus on performing their specific function and avoiding errors not expected by the Domain.

**I** Interface Segregation Principle: We can see that the repository has two additional interfaces—one for creating and one for updating—avoiding the concentration of logic solely in a single 'Save' method.

**D** Dependency Inversion Principle: High-level modules (Domain) should not depend on low-level modules; in this case, abstractions are used—a clear example of ports and adapters."

# 🛠️ Migration Strategy: Data First

We follow a **Data First** approach for database migrations. The initial migration is executed via command line, which automatically creates the necessary tables based on the defined data models.

---

## 📋 Key Implementations

### 🔍 AuditableEntity

We use an `AuditableEntity` base model to enable auditing. This allows us to track whether a record has been **updated** or **deleted**, providing traceability and accountability across the system.

### 🔐 Permission Authorization

User access to specific pages is controlled through a permission-based system. The frontend integrates with the backend by calling the `ValidatePermission` API, which checks whether the **logged-in user** has access to the requested resource.

This ensures that only authorized users can view or interact with protected routes or components.

## Infrestructure as Code 

## ⚙️ Infrastructure Scalability with Terraform

An example was created to demonstrate how automatic scalability can be managed in a production environment using **Terraform**.

This setup enables **horizontal scalability**, allowing the infrastructure to automatically adjust and provision additional resources when system load increases.

Print from lens
<img width="1571" height="420" alt="image" src="https://github.com/user-attachments/assets/8f43633b-f987-45eb-a16f-7847053bf737" />




## Front end 
 React, organized using **Feature-Based Architecture** 
* Client-side form validation.
<br><b>Response:</b> added validations with React-hook-form and materialize.
* Use of React Hooks and functional components.
<br><b>Response:</b> Created custom hooks. and use other hooks
* Update and delete employee functionality.
<br><b>Response:</b> Done.
* Standarize API calls.

Structure Model

```plaintext

src/
├── assets/                # Static assets like images, icons, fonts
├── features/             # Domain-specific features
│   ├── login/            # Login-related logic and components
│   └── userManagement/   # User management functionality
│       ├── components/   # UI components specific to this feature
│       ├── queries/      # React Query hooks (fetching and mutations)
│       └── types.tsx     # Type definitions for this feature
├── shared/               # Reusable code across features
│   ├── components/       # Generic UI components
│   └── context/          # Global contexts (e.g., AuthContext)
|   └── types.tsx         # Global type definitions
├── services/             # API calls and external logic
├── App.tsx               # Root component
├── main.tsx              # Application entry point
├── index.css             # Global styles
      

