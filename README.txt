# Eye Shop - 2D Game Project (Unity)
PLAYTHROUGH: https://drive.google.com/file/d/14S-Phaei2RFaAzGcduA9WQhqSAQoqXvl/view?usp=sharing

## Engine + Version
- **Unity 6.1** (6000.1.14f1)
- **Aspect ratio display recommended: 4x3.

## WHAT THE GAME IS
A small **2D click-to-move** game where you explore a scene, **collect loot** (e.g., eyeballs, flowers, pots), and track progress through **inventory + EXP/level UI**. Loot can **respawn** after being collected.

## MY ROLE
- **Solo developer** (graphics + code + setup)

## CONTROLS
- **Left Click**: move the player to the clicked X position (player stays on the same Y “floor”)
- **Left Click** on the door: move the player to the other level
- **Left Click (when near loot)**: pick up loot (pickup is blocked when clicking UI)

## SYSTEMS
- **Physics-based click-to-move movement** using Rigidbody2D velocity in `FixedUpdate()` (prevents jitter)
- **Loot pickup system** (trigger range + click to collect) with a global loot event (`Loot.OnItemLooted`)
- **Inventory system + UI slots** (stacking items, empty-slot placement, UI refresh per slot)
- **Gold tracking** (gold is handled separately from inventory slots and updates UI text)
- **EXP + Level system** with slider + level text (supports multiple level-ups in one gain)
- **Loot respawn system** (pickup animation, hide, wait, then restore visuals/collider and enable pickup again)

## KEY SCRIPTS (what each solves)
- PlayerMovement.cs — Click-to-move player controller using Rigidbody2D physics in FixedUpdate() (prevents jitter) + walking animation + footsteps audio.
- Loot.cs — Loot interaction system (trigger range + click to pick up) that fires a global pickup event (Loot.OnItemLooted) and handles respawn logic.
- InventoryManager.cs — Inventory “brain” that listens to loot pickups, stacks items / fills the first empty slot, and updates gold + inventory UI.
- InventorySlot.cs — UI slot controller that displays icon/quantity and processes slot clicks (remove/drop/consume one item depending on current setup).
- ExpManager.cs — EXP + Level progression system that updates the EXP slider and level text and supports multiple level-ups per gain.

## BUGS FIXED
- Player jitter / rapid flipping at destination — moved velocity code to FixedUpdate() and used Time.fixedDeltaTime stop logic.
- Item duplicated into multiple slots — returned after stacking and after placing into the first empty slot.
- EXP slider not updating unless leveling — called UpdateUI() on every EXP change.
- Picking up world loot while clicking UI — blocked pickup with EventSystem.current.IsPointerOverGameObject().
- Respawn stopped after adding audio clips — made audio calls null-safe and ensured an AudioSource + clips are assigned.
