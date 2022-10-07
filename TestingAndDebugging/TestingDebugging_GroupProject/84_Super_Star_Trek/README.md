# Super Star Trek Testing and Debugging Project

## Project Overview

For our CPSC 5210 Testing and Debugging class, we are creating units tests for the Java Super Star Trek game, which can be found on [coding-horror's repo](https://github.com/coding-horror/basic-computer-games/tree/main/84_Super_Star_Trek) with full explanation of the game's origin, history and the overall project for converting the game into modern languages. 

Our goal is to create Unit Tests and fully automated testing environment for the java program. During this process, we expect to refactor the java code as we encounter issues with testability or logic errors.

We have configured the gradle to install the required environment onto your IDE, so ensuring the gradle is synced at load is important so you don't run into issues. 

### Project details

- `Java Version: 17`
- `Testing Environment: JUnit5`
- `Mock Environment: Mockito3`

----------------------------------------------------------------------------------------------------

## Game Information

### Super Star Trek† Rules and Notes

1. OBJECTIVE: You are Captain of the starship “sstproject.Enterprise”† with a mission to seek and destroy a fleet of Klingon† warships (usually about 17) which are menacing the United Federation of Planets.† You have a specified number of stardates in which to complete your mission. You also have two or three Federation Starbases† for resupplying your ship.

2. You will be assigned a starting position somewhere in the galaxy. The galaxy is divided into an 8 x 8 quadrant grid. The astronomical name of a quadrant is called out upon entry into a new region. (See “Quadrant Nomenclature.”) Each quadrant is further divided into an 8 x 8 section grid.

3. On a section diagram, the following symbols are used:
    - `<*>` sstproject.Enterprise
    - `†††` Klingon
    - `>!<` Starbase
    - `*`   Star

4. You have eight commands available to you (A detailed description of each command is given in the program instructions.)
    - `NAV` Navigate the Starship by setting course and warp engine speed.
    - `SRS` Short-range sensor scan (one quadrant)
    - `LRS` Long-range sensor scan (9 quadrants)
    - `PHA` Phaser† control (energy gun)
    - `TOR` Photon torpedo control
    - `SHE` Shield control (protects against phaser fire)
    - `DAM` Damage and state-of-repair report
    - `COM` Call library computer

5. Library computer options are as follows (more complete descriptions are in program instructions):
    - `0` Cumulative galactic report
    - `1` Status report
    - `2` Photon torpedo course data
    - `3` Starbase navigation data
    - `4` Direction/distance calculator
    - `5` Quadrant nomenclature map

6. Certain reports on the ship’s status are made by officers of the sstproject.Enterprise who appears on the original TV Show—Spock,† Scott,† Uhura,† Chekov,† etc.

7. Klingons are non-stationary within their quadrants. If you try to maneuver on them, they will move and fire on you.

8. Firing and damage notes:
    - Phaser fire diminishes with increased distance between combatants.
    - If a Klingon zaps you hard enough (relative to your shield strength) he will generally cause damage to some part of your ship with an appropriate “Damage Control” report resulting.
    - If you don’t zap a Klingon hard enough (relative to his shield strength) you won’t damage him at all. Your sensors will tell the story.
    - Damage control will let you know when out-of-commission devices have been completely repaired.

9. Your engines will automatically shit down if you should attempt to leave the galaxy, or if you should try to maneuver through a star, or Starbase, or—heaven help you—a Klingon warship.

10. In a pinch, or if you should miscalculate slightly, some shield control energy will be automatically diverted to warp engine control (if your shield are operational!).

11. While you’re docked at a Starbase, a team of technicians can repair your ship (if you’re willing for them to spend the time required—and the repairmen _always_ underestimate…)

12. If, to same maneuvering time toward the end of the game, you should cold-bloodedly destroy a Starbase, you get a nasty note from Starfleet Command. If you destroy your _last_ Starbase, you lose the game! (For those who think this is too a harsh penalty, delete line 5360-5390, and you’ll just get a “you dumdum!”-type message on all future status reports.)

13. End game logic has been “cleaned up” in several spots, and it is possible to get a new command after successfully completing your mission (or, after resigning your old one).

14. For those of you with certain types of CRT/keyboards setups (e.g. Westinghouse 1600), a “bell” character is inserted at appropriate spots to cause the following items to flash on and off on the screen:
    - The Phrase “\*RED\*” (as in Condition: Red)
    - The character representing your present quadrant in the cumulative galactic record printout.

15. This version of Star Trek was created for a Data General Nova 800 system with 32K or core. So that it would fit, the instructions are separated from the main program via a CHAIN. For conversion to DEC BASIC-PLUS, Statement 160 (Randomize) should be moved after the return from the chained instructions, say to Statement 245. For Altair BASIC, Randomize and the chain instructions should be eliminated.

