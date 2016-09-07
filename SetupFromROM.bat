echo off
rem 
rem MASTER CODE XDDDDDD
rem 
set WORKING_DIRECTORY=%cd%
set HANDBRAKECLI="C:\Program Files\Handbrake\HandBrakeCLI.exe"
rem 
rem AFS2ADX 2 AAC-HC
rem 
for %%f in (ROM\*.afs) do (copy /Y %%f %WORKING_DIRECTORY%\Tools\AudioConversion\99AFS2ADX\)
cd %WORKING_DIRECTORY%\Tools\AudioConversion\99AFS2ADX\
AFSExplorer.exe SH_VOICE_E.afs
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\99AFS2ADX\*.afs) do (del /F %%f)
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\99AFS2ADX\FULL_AFS_FILE_DUMP\*.adx) do (move /Y %%f %WORKING_DIRECTORY%\Tools\AudioConversion\0ADX2WAV\)
cd %WORKING_DIRECTORY%\Tools\AudioConversion\0ADX2WAV
call ADX2WAV2.bat
mkdir "%WORKING_DIRECTORY%\ROM\CharacterSpeech"
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\1WAV2AACHCv2\*.m4a) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\CharacterSpeech")
for %%f in (%WORKING_DIRECTORY%\ROM\*.afs) do (del /F %%f)
rem 
rem SOFDEC VIDEO TO X265
rem 
rem Copy Sofdec videos for processing.
for %%f in (%WORKING_DIRECTORY%\ROM\*.sfd) do (copy /Y %%f %WORKING_DIRECTORY%\Tools\VideoConversion\0SFD2MPG)
cd %WORKING_DIRECTORY%\Tools\VideoConversion\0SFD2MPG\
rem Call ConvertHeroes to demux and convert SFD.
call ConvertHeroes.bat
rem Convert With Handbrake
call %WORKING_DIRECTORY%\Tools\VideoConversion\1Handbrake\HeroesToX265.bat
cd %WORKING_DIRECTORY%
rem Remove all old cutscene files.
for %%f in (%WORKING_DIRECTORY%\ROM\*.sfd) do (del /f %%f)
mkdir %WORKING_DIRECTORY%\ROM\Movies
rem Copy all new cutscene x265 encodings.
for %%f in (C:\Temp\x265\*.avi) do (move /Y %%f ROM\Movies)
rem Cleanup after conversion.
RD /S /Q "C:\Temp"
rem 
rem ADX AUDIO TO AAC-HC V2
rem 
for %%f in (%WORKING_DIRECTORY%\ROM\*.adx) do (copy /Y %%f Tools\AudioConversion\0ADX2WAV\)
cd %WORKING_DIRECTORY%\Tools\AudioConversion\0ADX2WAV\
call ADX2WAV.bat
mkdir "%WORKING_DIRECTORY%\ROM\BackgroundMusic"
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\1WAV2AACHCv2\*.m4a) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\BackgroundMusic")
for %%f in (%WORKING_DIRECTORY%\ROM\*.adx) do (del /f %%f)
rem 
rem CALL MOVESTUFF
rem 
cd %WORKING_DIRECTORY%
call MoveStuff.bat
pause