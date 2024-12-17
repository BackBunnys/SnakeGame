# SnakeGame

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/d8a284339ef5487396558a349e97bf83)](https://app.codacy.com/gh/BackBunnys/SnakeGame/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=flat-square&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=flat-square&logo=csharp&logoColor=white)
<img src="https://www.sfml-dev.org/download/goodies/sfml-logo.svg" height="20">
 
Welcome to the Snake Game! This is a classic multiplayer snake game developed in C# using the SFML library. Play in singleplayer or compete against other players or bots in a fun and engaging environment featuring static blocks, walls, and a round system.

## Features
* Multiplayer Mode: Play against another player using designated controls.
* Versus Bots: Challenge computer-controlled snakes.
* Static Blocks and Walls: Navigate through obstacles to avoid crashing.
* Round System: Compete in rounds, with the winner determined by the most victories. Draws are possible!
* Setup and Settings screen

## Table of Contents

* [Dependencies](#dependencies)
* [Building the Project](#building-the-project)
* [Running the Project](#running-the-project)
* [Controls](#controls)
* [Architecture](#architecture)

## Dependencies
* .NET Framework 4.7.2
* SFML.Net

## Building the Project

1. Clone the repository:
```
   git clone https://github.com/BackBunnys/SnakeGame.git
```   

2. Navigate into the project directory:
```cmd
   cd SnakeGame
```

3. Open the solution file (.sln) in Visual Studio.
4. Restore the dependencies (if needed):
```ps
   nuget restore
```

## Running the Project

To start the game, press F5 in Visual Studio to run the project. The game window will open, and you can start playing! \
In some cases you will need to place .dll of csfml placed in packages/CSFML.2.6.1/runtimes/<target>/native/ in your target build. \
Or use binaries present in Releases.

## Controls

### Player 1 Defaults (using WASD)

* W: Move Up
* A: Move Left
* S: Move Down
* D: Move Right

### Player 2 Defaults (using Arrow Keys)
* ↑: Move Up
* ←: Move Left
* ↓: Move Down
* →: Move Right

### Game Controls
* Press opposite direction key to slow the speed of your snake
* Press same direction key to speed up your snake
* Esc: Pause the game

## Architecture
Code of this project have simple structure:
* Core - This namespace is responsible for core snake game objects: snakes, fruits, blocks. And their controllers (like KeyboardController and BotController for snake).
* Engine - This namespace is the base of the game displaying and work (so-called game-loop). This contains StateMachine (for screen/scene managing) and GameEngine structure, that consist of the all required elements for the game (like the window and StateMachine itself)
* Screen - This namespace consist of all game screens/scenes implementation. Like IntroScreen, GameScreen, PauseScreen, etc.
* GUI - This namespace is the base of the graphic interface components.
* Settings - This namespace is repsonsible for settings handling (usign .NET properties and resources mechanism)
* Utils - Utility classes for handling images, resources, settings and sfml extensions.
---

Enjoy playing Snake Game! If you have any questions or feedback, feel free to open an issue on GitHub. Happy gaming!
