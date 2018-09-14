# Codename Rogue of Fall

Game prototyping using concepts from Towerfall, Binding of Isaac, Dead Cells,
Crawl.

## Ideas

- Towerfall-like battle
- Towerfall-like edge wrapping
- BoI-like rooms

## Game Design

- References:
  - Towerfall
  - Binding of Isaac
  - Dead Cells
  - Crawl
  - (Risk of Rain?)
  - (Nucler Throne?)

### Gameplay

Explore multiple levels, each consisting of multiple rooms that can contain
enemies, items, shop or a boss. (BoI)

Items can be a weapon (modify player attack type), ability (modify player usable
ability) or passive (modify anything else). (BoI, Dead Cells)

Rooms have screen wrap and are played as a platformer (Towerfall). On to change
rooms there's a portal on every side of the room that opens when all enemies are
gone (BoI).

Weapons have attack limitations: ranged weapons (Bows/Shurikens) have a finite
number of arrows (Towerfall) and melee weapons (Swords/Axes/Daggers) have a big
recovery time (Crawl).
- Ranged:
  - Bow: mid projectile velocity and 3 quiver
  - Javelin: (charged?)
  - Shuriken/Kunai: high projectile velocity (?)
- Melee:
  - Sword: mid attack range and mid recovery
  - Axe: large attack range but very slow recovery
  - Dagger: short attack range but faster recovery

Abilities have a long cooldown and are usually for movement or defensive
purpose (Towerfall, Crawl).

- Abilities:
  - Dash: pushes you forward (in the joystick direction), ignores gravity, starts
      fast and slows down until stop. During dash any attack is blocked
      (projectiles are catch, melee weapons are desarmed). Cancelling dash enables
      you to keep the current dash velocity (but disables the blocking)
  - Double jump: enables you to jump mid-air once. Can mid-air jump attacks
      (projectiles are pushed down, melee weapons are disarmed)
  - Reflect: reflects projectiles and disarm melee weapons

### Level

Multiple interconnected rooms (BoI, Crawl). Contains a boss room that has access
to next level (BoI), an item room (?) and secret rooms (?).
