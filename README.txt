Readme
This project is a very simple 2d platformer that records the players actions, then uses that data to train
an ML.net LightGBM engine. This engine is then used to provide the AI for the final boss of the game.
The platformer had to be kept very simple (e.g simple identical level layouts) to allow for the game state to be
encapsulated into a simple tab delimited txt file to provide an easy input vector.
My original plan was for the machine learning to run in the background of the unity application, however I was 
unable to get the ML dlls to build within the unity framework, so I solved this problem by instead running the ML
process as a separate console app from within the unity project

TO GET THE GAME WORKING:
-Download DeathLearnerV2 repo
-Run the powershell script "DLInstaller.ps1" 
(this script will unzip the ML console app exe required to run the game exe or to run it in unity. 
Will need to set powershell executionpolicy to unrestricted or run powershell as an administrator.
Alternatively, can manually unzip the folder '..\Assets\MLConsoleApp\MLConsoleApp.zip'
to '..\Assets\MLConsoleApp' and '..\CompletedGame\MLConsoleApp')
-Run DeathLearner.exe in ..\CompletedGame

Created in Unity version 2021.1

Music credits:
The Lift Kevin MacLeod (incompetech.com)
Licensed under Creative Commons: By Attribution 3.0
http://creativecommons.org/licenses/by/3.0/

Unholy Knight Kevin MacLeod (incompetech.com)
Licensed under Creative Commons: By Attribution 3.0
http://creativecommons.org/licenses/by/3.0/