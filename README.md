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

## Project Structure
MeetingRoomBookingSystem
│
├── Data
├── Models
├── ViewModels
├── Services
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
