# FSM - Context-Based State Machine Engine

A lightweight, generic Finite State Machine (FSM) engine built with context-aware transitions. This engine allows defining states and conditional transitions based on a shared context object.

## 🧠 Features

- Generic FSM engine supporting any `TContext` class
- Fluent builder pattern for easy configuration
- Conditional transitions based on context data
- Entry action execution on state change
- Clean, modular design for extension

## 🔧 Usage

### 1. Define Your Context

```csharp
public class Player
{
    public Vector2 Velocity { get; set; }
}
```

### 2. Define States
```csharp
public class IdleState : State<Player>
{
    public override void OnEnter(Player player)
    {
        Console.WriteLine("Entered Idle state.");
    }

    public override void OnExit(Player player)
    {
        Console.WriteLine("Exiting Idle state.");
    }
}

public class MoveState : State<Player>
{
    public override void OnEnter(Player player)
    {
        Console.WriteLine("Player starts moving.");
    }

    public override void OnExit(Player player)
    {
        Console.WriteLine("Player stops moving.");
    }
}
```

### 3. Build the State Machine
```csharp
var player = new Player();

var idle = new IdleState();
var move = new MoveState();

var fsm = new StateMachineBuilder<Player>()
    .AddState(idle)
    .AddState(move)
    .AddTransition(idle, move, p => p.Velocity > 0)
    .AddTransition(move, idle, p => p.Velocity == 0)
    .SetInitialState(idle)
    .Build(player);
```

### 4. Update the FSM
Call fsm.Update(context) in your game loop or logic tick to evaluate conditions and perform transitions:

```csharp
fsm.Update(player); // Still idle

player.Velocity = new Vector2(1, 0);
fsm.Update(player); // Transitions to MoveState

player.Velocity = Vector2.Zero;
fsm.Update(player); // Transitions to IdleState
```
