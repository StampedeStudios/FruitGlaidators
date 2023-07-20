# Fruit Gladiators

[![Build and Deploy of Fruit Gladiators](https://github.com/StampedeStudios/FruitGlaidators/actions/workflows/build-and-deploy.yml/badge.svg)](https://github.com/StampedeStudios/FruitGlaidators/actions/workflows/build-and-deploy.yml)


This repository contains the Unity project for GMTK 2023.

## Gameplay
Since the theme was **Roles Reversed**, we built a reversed snake where you control fruits instead of the snake and the mission is to save as much fruit as you can.

### How to play
Use **arrow keys** to move the selected fruit and **space** to change the currently active character. If things get too tough, hold **R** to reset the level.

There are currently two types of levels:

- Ones where you need to make the snake kill himself to win;
- Ones where you can escape or make a partner escape without killing the snake.

At the start of each level, a window will tell you what's the way out of it.

## Getting Started

To run and modify the game locally, follow these steps:

1. **Clone** or **download** this repository to your local machine.
2. **Open** the Unity game project using Unity Editor (version 2021.3.25f1 or later).
3. **Explore** the project files and assets in the Unity Editor.
4. **Play** the game in the Unity Editor by clicking the "Play" button.

## Deployment

The game is configured to be deployed on itch.io using GitHub Actions. The deployment process is automated through the following steps:

1. **Build** - A GitHub Actions workflow is triggered whenever changes are pushed to the `master` branch. The workflow builds the game as WebGL using Unity's build pipeline.
2. **Release** - After a successful build, the workflow creates a new release on GitHub and attaches the game build as an artifact.
3. **Deploy** - Finally, the workflow deploys the game to itch.io by uploading the artifact to a specific game project.

To set up the deployment for your game, you need to configure the necessary secrets and adjust the workflow file located at `.github/workflows/build-and-deploy.yml` to match your project's needs. Please look at the official documentation of GitHub Actions and itch.io for more information.

## Next Steps
There are no current plans to work on this project any further but if ever someone will pick it up from here, there are a few things to correct:

- [ ] Handle the difficulty curve. Apparently, the first level it's very hard to complete and makes many players stop playing on the very first try.
- [ ] Add more levels. 5 playable levels are currently implemented, however, there's the potential, with the currently implemented mechanics, to design a few more.
- [ ] Add music in the background
- [ ] Add a tutorial section, in the game jam version we assumed that everyone knew the famous game: *Snake*. We were wrong.
- [ ] Add sound effects when a character is close to being eaten
