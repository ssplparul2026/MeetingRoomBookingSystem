# MeetingRoomBookingSystem

## Project Overview
This project is a Meeting Room Booking System developed by using ASP.NET Core MVC and EF Core.The aim of this project is to manage meeting room bookings and prevent overlapping reservation.

## Technologies Used
- ASP.Net Core MVC
- EF Core 
- SQL Server
- Razor Views
- Unit Testing(xUnit)
- jQuery UI DatePicker

## Features
- Create meeting rooms
- View all meeting rooms
- View bookings for a selected room and date.
- View all bookings for a selected date.
- Create a new booking
- View my bookings
- Cancel active bookings.
- Prevent overlapping bookings
- jQuery UI DatePicker for date selection.
- Unit tests for booking overlap logic

## Current Progress
The following components have been completed:

- Project setup
- Database configuration
- Entity models
- Entity Framework Core migrations
- Service layer implementation
- Booking overlap validation logic
- Unit testing project setup and tested edge cases
- Controllers implementation
- Room Booking Details page
- Navigation bar with:
  - Home
  - Rooms
  - My Bookings
  - Create Booking
  - Bookings By Date
- Date selection for room bookings using jQuery UI DatePicker
- Booking UI Pages
 


## Project Structure
MeetingRoomBookingSystem
│
├── Data
├── Models
├── ViewModels
├── Services
├── Helpers
├── Migrations
├── Views (Completed)
├── Controllers (Completed)
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

## Validation Rules
- End time must be greater than start time.
- Bookings cannot overlap for the same room.
- Only today's date or future dates can be booked.
- Cancelled bookings are ignored during conflict checking.
- Required fields are validated before creating a booking.

## Business Rules
- Two active bookings for the same room cannot overlap.
- Back-to-back bookings are allowed (e.g., one booking ends at 11:00 AM and the next starts at 11:00 AM).
- Users can create bookings only for today or future dates.
- Past dates cannot be selected for booking.
- End time must be greater than the start time.
- Only active bookings are considered while checking conflicts.
- When a conflict occurs, the system displays which existing booking is causing the conflict.

## Assumptions Made
- A room can not have overlapping bookings on the same date.
- The booking start time must be earlier than the end time.
- Each booking belogs to one room and one room can have multiple bookings.
- The user provides valid booking details.
- Bookings are checked only for the selected booking date.
- Cancelled booking must free up that time slot for new bookings.
- A meeting room can only be booked for the current date or a future date.
- Booking requests for past dates are not allowed.

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

## Challenges Faced

### 1. Booking Overlap Validation
**Challenge:** Implementing the booking overlap logic correctly for all scenarios, such as exact overlap, partial overlap, and back-to-back bookings.

**Solution:** Created a separate `BookingOverlapHelper` class to keep the overlap logic reusable and wrote unit tests to verify different booking scenarios.

---

### 2. Working with DateOnly and TimeOnly
**Challenge:** Handling `DateOnly` and `TimeOnly` values in MVC forms and ensuring they were bound correctly.

**Solution:** Used `DateOnly` for booking dates and `TimeOnly` for booking times, along with HTML date/time inputs and jQuery UI DatePicker for date selection.

---

### 3. Mapping Models and ViewModels
**Challenge:** Passing only the required data to different views instead of exposing the entire entity model.

**Solution:** Created separate ViewModels such as `RoomViewModel`, `BookingViewModel`, `RoomBookingViewModel`, and `MyBookingsViewModel`, and mapped entity data before sending it to the views.

---

### 4. Integrating jQuery UI DatePicker
**Challenge:** Allowing users to select booking dates easily while maintaining the required date format.

**Solution:** Integrated jQuery UI DatePicker and configured it to use the `yyyy-MM-dd` format for compatibility with the application.

---

### 5. Displaying Multiple Booking Conflicts
**Challenge:** Initially, only the first conflicting booking was shown when multiple bookings overlapped.

**Solution:** Updated the conflict detection logic to collect all overlapping bookings and display each conflict as a separate validation message.

---

### 6. Handling Cancelled Bookings
**Challenge:** Ensuring cancelled bookings did not prevent users from creating new bookings for the same time slot.

**Solution:** Filtered overlap validation to check only bookings with `BookingStatus.Active`, allowing cancelled time slots to be booked again.

## Future Improvements
- Edit existing bookings
- Buffer time feature which stops back-to-back schedulling.
- Authentication and authorization
- Email notification after booking
- Calendar view for bookings
- Recurring bookings