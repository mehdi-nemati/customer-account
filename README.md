# Customer account - Implementation of DDD, CQRS and Event Sourcing

This repository shows how to implement Event Sourcing, CQRS and DDD in .NET Core, using a user account as example

## Give a Star! :star:
If you liked the project or helped you, please give a star, thanks ;)

## :floppy_disk: How do I use it?

You're going to need the following tools:

* Docker
* Visual Studio 2022+ or Visual Studio Code
* .Net Core 7

## :dart: Clean Architecture

Here's the basic architecture of this template:

* Respecting policy rules, with dependencies always pointing inward
* Separation of technology details from the rest of the system
* SOLID
* Single responsibility of each layer

## Technologies implemented:

- ASP.NET 7.0
- ASP.NET WebApi Core
- Entity Framework Core 7.0
- .NET Core Native DI
- Kafka [Message Broker]
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI
- MongoDB for storing events
- SQL server for storing data

## Architecture:

- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- Domain Validations
- CQRS (Imediate Consistency)
- Event Sourcing
- Unit of Work


## :envelope: Message Broker

Every successful handled command creates an event, which is published into a Message Broker. A synchronization background process subscribes to those events.


## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Event Bus

This layer contains classes for handling event messages.