Original source downloaded [from Vintage Basic](http://www.vintage-basic.net/games.html)

Conversion to [Oracle Java](https://openjdk.java.net/) by [Taciano Dreckmann Perez](https://github.com/taciano-perez).

Overview of Java classes:
- SuperStarTrekInstructions: displays game instructions
- sstproject.SuperStarTrekGame: main game class
- sstproject.GalaxyMap: map of the galaxy divided in quadrants and sectors, containing stars, bases, klingons, and the sstproject.Enterprise
- sstproject.Enterprise: the starship sstproject.Enterprise
- sstproject.GameCallback: interface allowing other classes to interact with the game class without circular dependencies 
- sstproject.Util: utility methods

[This video](https://www.youtube.com/watch?v=cU3NKOnRNCI) describes the approach and the different steps followed to translate the game.