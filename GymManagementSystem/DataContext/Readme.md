# Gym Management System

The Gym Management System is a C# application designed to help gyms manage client memberships, trainer schedules, and session assignments. This system allows trainers to coach clients in various sessions, with each client restricted to attending one sports session per month. The project utilizes several classes, including `Client`, `Trainer`, `Membership`, and `Schedule`, to facilitate easy management of gym resources and scheduling.

## Table of Contents
- [Features](#features)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Usage](#usage)
- [Classes Overview](#classes-overview)
- [Client](#client)
- [Membership](#membership)
- [Schedule](#schedule)
- [Trainer](#trainer)
- [Future Improvements](#future-improvements)
- [Contributing](#contributing)
- [License](#license)

## Features
- **Client Management**: Track and manage gym clients, including their memberships and session restrictions.
- **Trainer Scheduling**: Manage trainer schedules, with each trainer assigned up to two shifts.
- **Session Restriction**: Each client is allowed to participate in only one session per month.
- **Membership Management**: Link clients to their membership plans and manage their membership details.

## Project Structure

```plaintext
GymManagementSystem/
├── Models/
│   ├── Client.cs
│   ├── Trainer.cs
│   ├── Membership.cs
│   └── Schedule.cs
├── Data/
│   └── DataContext.cs
├── Services/
│   ├── ClientService.cs
│   ├── TrainerService.cs
│   ├── ScheduleService.cs
│   └── MembershipService.cs
└── Program.cs
```


## Classes Overview

### Client
Represents a gym client with the following properties:
- **FullName**: The client's full name.
- **PhoneNumber**: Contact number for the client.
- **Age**: The client's age.
- **MembershipId**: Foreign key linking the client to a `Membership`.
- **Membership**: Navigation property to the `Membership` the client is associated with.
- **ScheduleId**: Foreign key linking the client to a `Schedule`.
- **Schedule**: Navigation property to the `Schedule` for the client's assigned session.

### Membership
Represents a membership plan that can be associated with multiple clients. Properties include:
- **Type**: Type of membership, specified by the `MembershipType`.
- **Price**: Cost of the membership plan.
- **MaxCapacity**: Maximum number of clients allowed under this membership type.
- **StartDate**: Date when the membership begins.
- **EndDate**: Date when the membership expires.
- **Clients**: Collection of clients associated with this membership.

### Schedule
Represents a schedule for trainers and sessions, with properties such as:
- **StartTime**: The beginning time of the session.
- **EndTime**: The ending time of the session.
- **TrainerId**: Foreign key linking the schedule to a `Trainer`.
- **Trainer**: Navigation property for the `Trainer` assigned to this schedule.
- **Clients**: Collection of clients attending this session, with the restriction that each client can only have one session per month.

### Trainer
Represents a gym trainer with properties such as:
- **FullName**: The trainer's full name.
- **Specialization**: The area of expertise or specialty of the trainer.
- **DateOfBirth**: The trainer's birth date.
- **PhoneNumber**: Contact number for the trainer.
- **Schedules**: Collection of schedules linked to this trainer, allowing up to two shifts per trainer.
