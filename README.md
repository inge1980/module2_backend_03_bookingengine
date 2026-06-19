### B.E.A.M. - Booking Engine for Asynchronous Management

 ![Flytdiagram](img/flowchart.png)

## Description

A C# asynchronous booking simulation system designed to demonstrate concurrency, task orchestration, and external service integration patterns.

The system processes booking requests (room type, start date, end date) through an asynchronous pipeline that simulates concurrent execution, timing variability, external dependencies, and request conflicts.

The goal is to explore and compare different approaches to asynchronous programming in .NET.

---

## Features

- Async Booking Pipeline  
  End-to-end asynchronous processing using async/await and task-based orchestration.

- Concurrent Request Simulation  
  Multiple booking requests are processed in parallel to demonstrate scheduling behavior and race conditions.

- Coordination & Task Handling  
  Uses task-based patterns to coordinate booking completion and failure states.

- External Service Simulation (HTTP-based)  
  Simulated external calls for:
  - Pricing
  - Availability
  - Weather influence on booking behavior

- Failure Simulation  
  Simulated random failures in external services (e.g. availability checks) to test basic error handling in async workflows.

- Execution Logging  
  Tracks async flow, task execution, and concurrency behavior for debugging and analysis.

---

## How to Use

- The system accepts booking requests in the following format:
  - roomType
  - startDate
  - endDate

- Requests can be:
  - Simulated internally in the backend
  - Conceptually submitted from an external client (e.g. frontend or API consumer)

- Each request is processed asynchronously through the booking pipeline

---

## How to Run

1. **Build the project**:
   ```bash
   dotnet build
   ```

2. **Run the simulation**:
   ```bash
   dotnet run --project Console/Console.csproj
   ```

## System Behavior

- Multiple booking requests are processed in parallel
- Concurrency is simulated using Task/Thread-based execution
- External service calls influence booking outcomes (simulated HTTP calls)
- Logs show execution flow across asynchronous operations

---

## Program Logic

- Accepts booking requests containing room type and date range
- Dispatches each request into an asynchronous processing pipeline
- Performs parallel calls to simulated external services (pricing, availability, etc.)
- Simulates concurrency scenarios such as overlapping requests and race conditions
- Simulates failure scenarios and retry behavior in dependent services
- Aggregates results into a final booking confirmation or rejection
- Logs execution flow across tasks and threads for observability

---

## Architecture Notes

This system simulates concurrent backend behavior in a single-process environment.

Key concepts:
- Thread-based simulation of concurrent execution (Part A)
- Task-based orchestration using async/await (Part C)
- TaskCompletionSource-style coordination patterns (Part B)
- Fan-out / fan-in processing model
- Failure scenarios and timing variability to emulate real async systems

---

## Assignment Mapping (A-D)

This project is structured to explicitly demonstrate the required learning goals:

### A - Thread + Join
- Thread-based simulation of concurrent booking requests
- Demonstrates execution order differences with and without Join
- Highlights race conditions and non-deterministic output

### B - Task + TaskCompletionSource
- Task-based orchestration of booking workflows using TaskCompletionSource for manual task completion
- Explicit completion signaling for booking success/failure
- Coordination of multiple asynchronous operations

### C - async/await Orchestration
- Booking pipeline implemented with async/await and Task.WhenAll orchestration
- Uses await Task.Delay and simulated external calls (pricing, availability, weather)
- Improved readability compared to manual task coordination

---

## Input Contract (Conceptual)

The system processes booking requests using the following structure:

- roomType
- startDate
- endDate

This represents the expected input format from any client (frontend, API consumer, or simulation layer).

A frontend is optional and not required for execution.

All validation, orchestration, and processing occur in the backend simulation pipeline.

---

## Language

All logging and internal system messages use English to ensure consistent async execution tracing and debugging across concurrent operations.