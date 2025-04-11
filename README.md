Game Title: Space Marine

Developer: Chris Simonetti

A platform shooter developed using Unity's 2d engine, written in C# using Miscrosoft Visual Studio 2022


## 🧠 Script Overview

Below is a breakdown of the core scripts and their relationships:

---

### 🧱 `BaseObject.cs`
- Serves as the **base class** for all active in-game objects that share common functionality (e.g., movement, health, animations).
- Provides reusable methods and variables to reduce code duplication.

---

### 🎮 `PlayerController.cs`
- Inherits from `BaseObject`.
- Contains player-specific logic such as:
  - Input handling (movement, shooting, jumping)
  - Health and damage response
  - Player-specific animations and interactions

---

### 👾 `EnemyBase.cs`
- Inherits from `BaseObject`.
- Acts as a generic enemy class that holds shared enemy behavior (e.g., pathfinding, taking damage, detecting player).
- Contains virtual methods to be overridden by specific enemy types.

---

### 💀 Unique Enemy Scripts
Each custom enemy inherits from `EnemyBase` and implements its own behavior. Below are examples of unique enemy scripts:

- `EnemyPlatformer.cs`: Moves along the platform using simple AI logic.
- `EnemyPopup.cs`: Stationary turret, that will 'pop-up' when the player is within a detection radius.
- `EnemyHopper.cs`: Unique platformer that will hop in an arch-like motion. It will attempt to avoid suicide by jumping off a platform

---



