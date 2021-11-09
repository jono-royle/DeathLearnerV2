$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$assetsDir  = Join-Path -Path $scriptDir -ChildPath \Assets\MLConsoleApp
$dataDir  = Join-Path -Path $scriptDir -ChildPath \CompletedGame\DeathLearner_Data\MLConsoleApp
Get-ChildItem "$assetsDir" -Filter *.zip | Expand-Archive -DestinationPath "$assetsDir" -Force
Get-ChildItem "$assetsDir" -Filter *.zip | Expand-Archive -DestinationPath "$dataDir" -Force