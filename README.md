# GMTK-2023

[![Build and Deploy of GMTK-2023](https://github.com/StampedeStudios/GMTK-2023/actions/workflows/build-and-deploy.yml/badge.svg)](https://github.com/StampedeStudios/GMTK-2023/actions/workflows/build-and-deploy.yml)


This repository contains the Unity project for the GMTK 2023.

## Gameplay

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

To set up the deployment for your own game, you need to configure the necessary secrets and adjust the workflow file located at `.github/workflows/build-and-deploy.yml` to match your project's needs. Refer to the official documentation of GitHub Actions and itch.io for more information.

## Next Steps
