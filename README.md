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
  - Shuriken: high projectile velocity (?)
- Melee
  - Sword: mid attack range and mid recovery
  - Axe: large attack range but very slow recovery
  - Dagger: short attack range but faster recovery

Abilities have a long cooldown and are usually for movement or defensive
purpose (Towerfall, Crawl).
- Dashes pushes you forward (in the joystick direction), ignores gravity, starts
    fast and slows down until stop. During dash any attack is blocked
    (projectiles are catch, melee weapons are desarmed). Cancelling dash enables
    you to keep the current dash velocity (but disables the blocking)
- Double jump enables you to jump mid-air once.

### Level

Multiple interconnected rooms (BoI, Crawl). Contains a boss room that has access
to next level (BoI), an item room (?) and secret rooms (?).

## Roadmap

- [ ] Trello/Jira roadmap (easier to update, not branch related)
- [ ] v0.0: Movement
  - [x] Basic player movement
  - [x] Basic screen wrapping
  - [ ] Basic ledge grab
  - [ ] Basic wall slide
  - [ ] Dash
  - [ ] Joystick input
  - [ ] Bugs
    - [x] Player movement: setting velocity and colliding with wall makes player
        hang in the air
- [ ] v0.1: Enemies, Attack & Abilities
  - [ ] Basic enemy
  - [ ] Health system
  - [ ] Attack items system
  - [ ] Abilites system
- [ ] v0.2: Rooms
  - [ ] Room advancing system

## Ideas

- [ ] Hold down while falling: fall faster (Towerfall)
- [ ] Delayed jump: allow jumping if player just got out of ground (1~3 frames),
    or if player will get ground/wall contact at near future (1~3 frames)
- [ ] Abstract inputs: be able to control every unit in the game

## Bugs

- Jump sometimes is very high
- Jump speed and jump height to calculate gravity seems wrong (using a way
    lower speed, the height is not reached, for example)
