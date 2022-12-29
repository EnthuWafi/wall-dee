# WALL-D
#### Video Demo:  https://youtu.be/oqlbXpMv7kI
#### Description:
This CS50 project was made in Unity, and written in C# programming language.

##### About WALL-D
The name WALL-D is a tribute to a 2008 movie called WALL-E which is a story that features a square-shaped robot as the main character. The original idea for WALL-D was to also have you control a square-shaped robot, but the idea ultimately never came to fruition due to the lack of assets.

#### Game Features
##### Character Movement
- Character's movements are limited to three lane: LEFT, MIDDLE, and RIGHT lane.
- Character can be controlled using A and D to go left and right. 
- Character can jump by pressing W.
##### Character Animation
- Character plays animation when moving, jumping, and dying.
##### Character Shoot
- Character can shoot out a high-speed projectile by pressing "spacebar" on their keyboard once their gauge-meter is full or once they have collected 5 points/diamond.
##### SQLite-based HighScore System
- Implemented a short algorithm to prevent the user from doing an SQL Injection Attack by inputting dangerous characters such as "'" and """ into the name input field.
##### Interactive Buttons and Menu
- Example of button used are: Start Button, Exit Button, Restart Button, Score Button.
##### Score System
- Score increases by 1 each second.
- Score increases by 10 for each collected points
- Score increases by 20 for each destroyed car (by projectile)
##### Unity Particle System
- Particle System is used to display the fog/dust.
- Also used to display the explosion whenever a car gets destroyed (by a projectile)

