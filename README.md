# Ship Fleet Manager

A modular and testable Ship Fleet Management system written in **C#**.  
This project showcases object-oriented design, type-safe data modeling, and proper API encapsulation — including unit testing with `xUnit`.

## Overview

Each ship has a globally unique IMO number, name, dimensions (length and width), and a position tracking system. Ships fall into two categories: **Tanker Ships** and **Passenger Ships**. Tankers manage fuel tanks, while passenger ships maintain structured passenger data.

## Features

### Core Fleet Management
- **Add ships to the fleet**:
  - **Tanker Ship**: Contains permanently installed fuel tanks.
  - **Passenger Ship**: Stores passenger lists and allows their updates.
- **Remove ships** from the fleet using their IMO number.
- **Retrieve ship details** by IMO number.
- **Get a list of all registered ships**.

### Ship-specific Operations
- **Tanker Ship**:
  - Refuel a selected tank with a specified amount of fuel.
  - Check fuel levels in each tank.
- **Passenger Ship**:
  - View and update passenger lists.

### Position Tracking
- All ships can update their geographical position (latitude/longitude).
- Position history is maintained internally.

## Project Structure
- `ShipFleet.Models`: Contains all model definitions and logic for ships, fuel types.
- `ShipFleet.Tests`: Unit tests written using [xUnit](https://xunit.net/).

## Running tests

Unit tests have been provided to ensure the correct operation of the application. To run the tests execute (from the root directory):

```
dotnet test ShipFleet.Tests\ShipFleet.Tests.csproj
```