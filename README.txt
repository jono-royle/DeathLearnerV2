Readme
This project is a very simple 2d platformer that records the players actions, then uses that data to train
an ML.net LightGBM engine. This engine is then used to provide the AI for the final boss of the game.
The platformer had to be kept very simple (e.g simple identical level layouts) to allow for the game state to be
encapsulated into a simple tab delimited txt file.
My original plan was for the machine learning to run in the background of the unity application, however I was 
unable to get the ML dlls to build within the unity framework, so instead I had to settle for firing up the ML
as a separate command line process from within unity

TO GET THE GAME WORKING:
Download DeathLearnerV2 repo
Run the powershell script "DLInstaller.ps1" 
(this will unzip the ML console app exe required to run the game exe or to run it in unity)
Run DeathLearner.exe in DeathLearnerV2\CompletedGame

Created in Unity version 2021.1

Music credits:
The Lift Kevin MacLeod (incompetech.com)
Licensed under Creative Commons: By Attribution 3.0
http://creativecommons.org/licenses/by/3.0/

Unholy Knight Kevin MacLeod (incompetech.com)
Licensed under Creative Commons: By Attribution 3.0
http://creativecommons.org/licenses/by/3.0/