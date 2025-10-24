# Toy Robot Game

## Overview
The **Toy Robot Game** is a console-based C# application that simulates a robot moving on a 5x5 grid.  The board uses a coordinate system where positions are represented as (row, column). The bottom-left corner is (1,1) and the top-right corner is (5,5).
- The robot can be placed, moved and rotated.
- The board can contain walls that block the robot’s movement.  
- Movement beyond the edge of the board wraps the robot to the opposite side.
- A real-time tracking of the robot's position and orientation can be retrieved on demand.

---

## Implementation Details

### Architecture
The project follows an object-oriented design:

- **`Position`**: Represent a specific cell on the game board with Row and Column properties.
- **`Board`**: Manages the game's logic including robot, walls and movement.  
- **`Robot`**: Maintains position and facing direction; handles the rotation.  
- **`ICommand` & Commands**: Each command (`PlaceRobotCommand`, `MoveCommand`, `TurnLeftCommand`, `TurnRightCommand`, `ReportCommand`, `PlaceWallCommand`) encapsulates a single action.  
- **`CommandParser`**: Parses user input and converts it into `ICommand` objects. Handles invalid input gracefully.  
- **`GameEngine`**: Orchestrates the game flow: reads input and executes commands.

### Design Decisions
- Commands are separate classes to allow easy extension with new commands in the future.  
- The board, not the robot, enforces rules like **occupied positions** and **walls**, keeping the robot reusable with other boards.
- Robot is board-agnostic, only handles its own rotation.
- All commands are validated for correct parameters and positions. The system ignores letter case and extra spaces in input.
- Invalid inputs are ignored without crashing the game.
- The game stops only when the board is completely filled with walls and no more commands can be executed.

---

## How It Works

1. **User Input** → e.g., `PLACE_ROBOT 2,3,NORTH`  
2. **CommandParser** converts input to an `ICommand` object.  
3. **GameEngine** executes the command on the **Board**.  
4. **Board** updates the robot’s position/facing or adds a wall.  
5. **REPORT** prints the current robot position and facing direction.  

### Wrap-Around Movement
- Moving off the top edge: wraps to the bottom  
- Moving off the bottom edge: wraps to the top  
- Moving off the left edge: wraps to the right  
- Moving off the right edge: wraps to the left  

---

## Commands

### PLACE_ROBOT
**Structure**: `PLACE_ROBOT row,col,facing`  
**Function**: Places the robot on the board at the specified position and direction if possible  
**Example**: `PLACE_ROBOT 1,1,NORTH`

### PLACE_WALL  
**Structure**: `PLACE_WALL row,col`  
**Function**: Places a wall at the specified position if empty  
**Example**: `PLACE_WALL 3,2`

### MOVE
**Structure**: `MOVE`  
**Function**: Moves the robot forward one space in the current direction  
**Example**: `MOVE`

### LEFT
**Structure**: `LEFT`  
**Function**: Turns the robot 90 degrees left  
**Example**: `LEFT`

### RIGHT  
**Structure**: `RIGHT`  
**Function**: Turns the robot 90 degrees right  
**Example**: `RIGHT`

### REPORT
**Structure**: `REPORT`  
**Function**: Shows the robot's current position and facing  
**Example**: `REPORT` → `2,3,NORTH`

---

## Command Validations

### PLACE_ROBOT
- Row must be between 1-5
- Column must be between 1-5  
- Facing must be: NORTH, SOUTH, EAST, or WEST
- Command ignored if the position is occupied by a wall
- Command ignored if any parameter is invalid

### PLACE_WALL
- Row must be between 1-5
- Column must be between 1-5
- Command ignored if the position is occupied by a robot or another wall

### MOVE
- Command ignored if no robot is placed
- Command ignored if the target position contains a wall
- Robot warps to the opposite side when moving beyond the board edges

### LEFT / RIGHT
- Command ignored if no robot is placed

### REPORT
- Command ignored if no robot is placed
---
## Test Data & Results

### Test Case 1: Basic Movement
**Input:**

`PLACE_ROBOT 3,3,NORTH`   
`MOVE`  
`MOVE`  
`RIGHT`  
`MOVE`  
`MOVE`  
`MOVE`  
`REPORT`  
**Expected Output:** `5,1,EAST`  
**Reasoning:** Robot moves north twice, turns east, moves east three times with wrapping from (5,5) to (5,1)

### Test Case 2: Placement and Movement with Obstacles
**Input:**

`PLACE_ROBOT 2,2,NORTH`   
`PLACE_WALL 3,2`  
`MOVE`  
`PLACE_ROBOT 3,2,EAST`   
`REPORT`  

**Expected Output:** `2,2,NORTH`  
**Reasoning:**
- Robot placed at (2,2) facing NORTH
- Wall placed at (3,2) - directly above the robot
- MOVE command ignored - wall blocks movement north to (3,2)
- PLACE_ROBOT at (3,2) ignored - position occupied by wall
- REPORT shows original position unchanged

---

## Testing Approach

### Testing Strategy
- **Unit Tests**: Each component tested in isolation (Board, Robot, Position, CommandParser)
- **Integration Tests**: End-to-end command sequences and game flows
- **Edge Cases**: Invalid inputs, boundary conditions, and error scenarios

### Test Coverage
- Command validation and parsing
- Movement with edge wrapping behaviour
- Wall placement and collision detection
- Robot placement, rotation and position updates
- Board state management

---
## Code Quality
- Follows SOLID principles and clean architecture
- Comprehensive unit test coverage
- Consistent coding standards and naming conventions

---

## Future Enhancements

### Code Refactoring
1. **Direction Class**: Extract rotation logic into a dedicated `Direction` class if facing becomes more complex, making the robot fully direction-agnostic
2. **Wall Class**: Enhance walls with properties (type, durability) by creating a dedicated `Wall` class instead of using basic positions
3. **Board Interface**: Implement `IBoard` interface to support multiple board types (different sizes, hexagonal grids, etc.)
4. **Command Structure**: Reorganise the commands folder into feature-based groups or add command handlers if the number of commands grows significantly

---

## Technical Stack

- **Framework**: .NET 8.0
- **Language**: C#
- **Testing**: xUnit

---

## Installation & Setup

### Clone the repository
git clone https://github.com/pkyriakou23/RobotGameSimulation

### Navigate to project directory
cd RobotGameSimulation

### Build the project
dotnet build

### Run the application
dotnet run
