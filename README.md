# Exercise CRUD with C# .NET and React JS 🎯
Before watch details, I created a video to make a presentation of this exercise:

<a href="https://youtu.be/APT2GkYLCtQ" target="_blank">YouTube video</a>


In this project we will create a CRUD of Users using the folowing indications:


## Indications 
## Back end requirements
 * Create a RESTful API for managing employee records with the following fields: id (int, clave primaria), firstName (string), lastName (string), title (string), email (string). (implementation [link](https://github.com/wavila88/CrudReactC-/blob/main/ChallengeBack/Domain/DTO/UserDto.cs))  
 * Implement CRUD operations (controller implementation [link](https://github.com/wavila88/CrudReactC-/blob/main/ChallengeBack/ChallengeBack/Controllers/UserController.cs))
 * Use Entity Framework Core with an in-memory database initially, and then with local SQL Server. 
(implementation [link](https://github.com/wavila88/CrudReactC-/blob/main/ChallengeBack/RepositorySQL/Queries/UserRepository.cs))
*  Validate JSON data (required fields, valid email format). (validation backend line 35 [link](https://github.com/wavila88/CrudReactC-/blob/main/ChallengeBack/Domain/UserService/UserService.cs), Front line 99 [link](https://github.com/wavila88/CrudReactC-/blob/main/front-user/src/components/user/user.tsx) ) 
*  Protect endpoints with token-based authentication (simulating Azure AD with a hardcoded token in the headers). (implemented [link](https://github.com/wavila88/CrudReactC-/blob/main/ChallengeBack/ChallengeBack/Controllers/TokenAuthAttribute.cs))

## Arquitecture back end  

Here, we're implementing the Hexagonal Architecture, keeping all the business logic encapsulated within our Domain layer. We also have the interfaces (ports) that the SQL Repository will use, where we implement the exposed interfaces (Adapters).


<img width="411" height="321" alt="Diagrama sin título-HexagonalUserCrud drawio" src="https://github.com/user-attachments/assets/00f48421-2aac-4299-860f-715fdbcc8570" />

"In this example, we can see all the SOLID principles at work:

**S** Single Responsibility: Each module has its specific function.

**O** Open/Closed Principle: Open to extension, closed to modification. This is applied through the ports and adapters that use interfaces and interface implementations. We can easily replace the current database with another, like Redis, Mongo, etc., in this exercise.

**L** Liskov Substitution Principle: The interface implementations focus on performing their specific function and avoiding errors not expected by the Domain.

**I** Interface Segregation Principle: We can see that the repository has two additional interfaces—one for creating and one for updating—avoiding the concentration of logic solely in a single 'Save' method.

**D** Dependency Inversion Principle: High-level modules (Domain) should not depend on low-level modules; in this case, abstractions are used—a clear example of ports and adapters."

## Front end requirements

* Create a React app that consumes the API. Display a list of employees.
<br><b>Response:</b> created hooks with @tanstack/react-query here encapsulate all Api call [link](https://github.com/wavila88/CrudReactC-/tree/main/front-user/src/components/user/queries)
* Include a form to add new employees.
<br><b>Response:</b> added on the following [link](https://github.com/wavila88/CrudReactC-/blob/main/front-user/src/components/user/user.tsx)
* Use TypeScript for type safety.
<br><b>Response:</b> created ApiResponseDto to standarize api responses [link](https://github.com/wavila88/CrudReactC-/blob/main/front-user/src/types.tsx)
* Perform asynchronous API calls with appropriate loading and error states.
<br><b>Response:</b> Are controlled by the  @tanstack/react-query library it returns a very helpful object 
* Use semantic HTML5 and CSS3 for styling (can be minimal). 
<br><b>Response:</b>
Bonus (Optional):applied on project 

* Client-side form validation.
<br><b>Response:</b> added validations with React-hook-form and materialize.
* Use of React Hooks and functional components.
<br><b>Response:</b> Created custom hooks. and use other hooks
* Update and delete employee functionality.
<br><b>Response:</b> Done.
## Short video for presentation



## 🧱 Project Structure

├── Api/ # Input adapter (controllers, DTOs) \
├── Application/ # Use cases and orchestration \
├── Domain/ # Entities, interfaces (ports), business logic \
├── Infra/ # Output adapters (repositories, messaging) \
