<h1 align="center">.NET MVC PROJECT</h1>

## **Introduction**

In this personal project, I embarked on a comprehensive journey to develop a web application using the Model-View-Controller (MVC) architectural pattern in C#. The MVC pattern divides the application into three core components: Model, View, and Controller, facilitating modular and organized development. Here's a breakdown of the project's key components and what I learned:

## **Part 1 - Entity Framework:**

I delved into Entity Framework, a powerful ORM framework provided by Microsoft. I embraced the code-first approach, allowing me to create a database using domain-specific classes, abstracting away the complexities of the underlying database structure. Through tutorials and hands-on practice, I mastered CRUD operations, database table creation, and data manipulation using LINQ operators.

## **Part 2 - Identity Framework:**
ASP.NET Identity became my go-to for implementing user authentication and registration. Instead of building a custom authentication system from scratch, I harnessed the capabilities of Identity Framework. I learned to manage user data, roles, claims, and security features, ensuring a robust authentication system for the application.

## **Part 3 - Database Design:**
In the absence of an Entity-Relationship Diagram (ERD), I employed database design principles to structure the database for the car rental application. I classified vehicles by type and car body by type, ensuring minimal but essential vehicle information storage. I also prepared for rental data storage, including date, car details, customer information, and status.

## **Part 4 - Querying the Database:**
To populate the UI with data, I needed to retrieve information from the database, allowing users to view and search for cars based on various criteria. I honed my skills in LINQ (Language Integrated Query), a query language integrated into C# and the .NET Framework. This enabled me to define queries efficiently and retrieve the desired data from the database.

## **Part 5 - Security:**
Security was a paramount consideration. I rigorously secured the controllers and actions, preventing unauthorized access or data modification. By using the Authorize attribute and role-based access control, I ensured that only authorized users could interact with specific parts of the application.

## **Part 6 - Implementing Vertical Slice and Mediator:**
To enhance the project's architecture and maintain a clean and organized codebase, I implemented the Vertical Slice Architecture pattern and incorporated the Mediator design pattern. 