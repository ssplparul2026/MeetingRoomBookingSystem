# MeetingRoomBookingSystem

## Project Overview
This project is a Meeting Room Booking System developed by using ASP.NET Core MVC and EF Core.The aim of this project is to manage meeting room bookings and prevent overlapping reservation.

## Technologies Used
- ASP.Net Core MVC
- EF Core 
- SQL Server
- Unit Testing(xUnit)

## Current Progress
The following components have been completed:

- Project setup
- Database configuration
- Entity models
- Entity Framework Core migrations
- Service layer implementation
- Booking overlap validation logic
- Unit testing project setup and tested edge cases
 


## Project Structure
MeetingRoomBookingSystem
│
├── Data
├── Models
├── ViewModels
├── Services
├── Helpers
├── Migrations
├── Views (In Progress)
├── Controllers (In Progress)
├── wwwroot
├── Program.cs
└── appsettings.json

## Booking Conflict Rule
The booking validation checks whether two bookings overlap using the following logic:

New Start Time < Existing End Time
AND
New End Time > Existing Start Time

Back-to-Back bookings are allowed. For Example:-
- 10:00 AM – 11:00 AM
- 11:00 AM – 12:00 PM

These bookings do not overlap.

## Assumptions Made
- A room can not have overlapping bookings on the same date.
- The booking start time must be earlier than the end time.
- Each booking belogs to one room and one room can have multiple bookings.
- The user provides valid booking details.
- Bookings are checked only for the selected booking date.
- Cancelled booking must free up that time slot for new bookings.

## Unit Test Project Setup
A separate xUnit test project was added to test the booking overlap logic.

Steps:

1. Created a new xUnit Test Project named MeetingRoomBooking.Tests.
2. Added a project reference to the main MeetingRoomBooking project.
3. Installed the required packages: 
    xunit
    xunit.runner.visualstudio
    Microsoft.NET.Test.Sdk 
4. Created unit tests for the booking overlap logic.
5. Excuted the all test via test explorer.

## Test Helper

A helper class was introduced to hold the booking overlap validation logic used by the unit tests.

Initially, the overlap tests required creating an instance of `BookingService`, which in turn required an `AppDbContext` dependency even though the overlap logic itself did not use the database. To remove this unnecessary dependency, the overlap logic was moved to a helper class.

This approach allowed the unit tests to focus only on the overlap validation logic without creating a service instance or providing a database context.

**Benefits:**

- Eliminates unnecessary `AppDbContext` dependency in unit tests.
- Keeps tests focused on business logic.
- Simplifies test setup.
- Improves code reusability.
