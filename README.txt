
# 2D Game Project (Unity)

## Engine + Version
- **Unity 6.1** (6000.1.14f1)
Aspect ratio display recommended: 4x3.

## What the game is
A small **2D click-to-move** game where you explore a scene, **collect loot** (e.g., eyeballs / butterflies), and track progress through **inventory + EXP/level UI**. Loot can **respawn** after being collected.

## My role
- **Solo developer** (graphics + code + setup)

## Controls
- **Left Click**: move the player to the clicked X position (player stays on the same Y “floor”)
- **Left Click** on the door: move the player to the other level
- **Left Click (when near loot)**: pick up loot (pickup is blocked when clicking UI)

## Systems implemented
- **Physics-based click-to-move movement** using Rigidbody2D velocity in `FixedUpdate()` (prevents jitter)
- **Loot pickup system** (trigger range + click to collect) with a global loot event (`Loot.OnItemLooted`)
- **Inventory system + UI slots** (stacking items, empty-slot placement, UI refresh per slot)
- **Gold tracking** (gold is handled separately from inventory slots and updates UI text)
- **EXP + Level system** with slider + level text (supports multiple level-ups in one gain)
- **Loot respawn system** (pickup animation, hide, wait, then restore visuals/collider and enable pickup again)
