
### Technical Specification: Meeting Diary System

## Overview

This technical specification outlines the design and implementation of a backend service for a meeting diary system that allows users to record and manage meetings using .NET Core and Entity Framework Core. The objective is to develop a set of APIs that manage user registration, meeting creation and management, and provide notifications for upcoming meetings. This project will offer practical experience in building a robust backend system using best practices in .NET Core development.

## Goals

- Implement CRUD operations for the main entities in the diary system.
- Understand and apply best practices in Entity Framework Core for data modeling and database management.
- Develop a clear understanding of service-based architecture using .NET Core.
- Gain experience in handling data relationships and business logic in the context of meeting management.
- Implement authentication and authorization using JWT and Permissions.
- Implement notification functionality for upcoming meetings.
- Integrate and use Serilog for logging within the system.
- Adhere to SOLID principles in designing and developing the system.

## Tools and Technologies

- .NET Core 8
- Entity Framework Core
- PostgreSQL or any compatible database
- Visual Studio or VS Code or Rider
- JWT
- Permissions
- Serilog

## Entities

### User

- Id (int)
- Name (string)
- Email (string)
- Password (string)
- RegistrationDate (DateTime)

### Role

- Id (int)
- Name (string) (Administrator, User)

### Meeting

- Id (int)
- Title (string)
- Description (string)
- StartDateTime (DateTime)
- EndDateTime (DateTime)
- UserId (int) [Foreign Key]

### Notification

- Id (int)
- MeetingId (int) [Foreign Key]
- UserId (int) [Foreign Key]
- Message (string)
- SentDateTime (DateTime)

### Services

Each service layer should contain business logic for managing its corresponding entity.

#### AccountService

- Register
- Login
- ForgotPassword
- ResetPassword
- ChangePassword
- DeleteAccount

#### UserService

- UpdateUser: Method to update user information.
- GetUser: Method to retrieve user information.
- GetUserById: Method to retrieve user information by their Id.

#### MeetingService

- AddMeeting: Method to add a new meeting.
- UpdateMeeting: Method to update meeting information.
- DeleteMeeting: Method to delete a meeting.
- GetMeeting: Method to retrieve meeting information.
- GetMeetingById: Method to retrieve meeting information by its Id.
- GetUpcomingMeetings: Method to retrieve upcoming meetings for the user.

#### NotificationService

- AddNotification: Method to add a new notification.
- GetNotification: Method to retrieve notification information.
- GetNotificationById: Method to retrieve notification information by its Id.
- SendNotification: Method to send notifications about upcoming meetings.

#### RoleService

- AddRole: Method to add a new role.
- UpdateRole: Method to update role information.
- DeleteRole: Method to delete a role.
- GetRole: Method to retrieve role information.
- GetRoleById: Method to retrieve role information by its Id.

### Authentication and Authorization

- Implement JWT-based authentication.
- Ensure only authenticated users have access to the API.
- Implement role-based authorization to restrict access to certain endpoints (e.g., only administrators can manage roles).

### Testing and Documentation

- Test the API endpoints using tools like Postman or Swagger UI.
- Document the API endpoints using Swagger or another documentation tool.

### Validation

- Implement data validation to ensure the integrity of incoming data.

## Database Setup

- Use EF Core migrations to set up the database schema.
- Seed the database with initial data for testing.

## API Development

- Implement CRUD operations for each entity.
- Implement API endpoints for handling authentication and authorization.
- Implement API endpoints for sending notifications.

## Submission

- Upload the project to GitHub, containing all source code.

## Evaluation Criteria

- Code organization and quality.
- Proper implementation of CRUD operations and service layers.
- Effective use of Entity Framework Core for data management.
- Handling of edge cases and potential errors.
- Implementation of authentication and authorization using JWT and Permissions.

## Tools Utilized

- AutoMapper
- Try-catch blocks
- Response handling
- PaginatedResponse
- Filtering (Filter)
- Serilog (upload in file, console, database)
- Notifications
- Data seeding
- Permissions