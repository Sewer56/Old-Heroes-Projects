rem 
rem MASTER CODE XDDDDDD
rem 
set WORKING_DIRECTORY=%cd%
rem 
rem SOFDEC VIDEO TO X265
rem 
rem Copy Sofdec videos for processing.
for %%f in (ROM\*.sfd) do (copy /Y %%f Tools\VideoConversion\0SFD2MPG)
cd Tools\VideoConversion\0SFD2MPG\
rem Call ConvertHeroes to demux and convert SFD.
call ConvertHeroes.bat
cd Tools\VideoConversion\1Handbrake\
rem Convert With Handbrake
call HeroesToX265.bat
cd %WORKING_DIRECTORY%
rem Remove all old cutscene files.
for %%f in (ROM\*.sfd) do (del /f %%f)
mkdir ROM\Movies
rem Copy all new cutscene x265 encodings.
for %%f in (C:\Temp\x265\*.avi) do (move /Y %%f ROM\Movies)
rem Cleanup after conversion.
RD /S /Q "C:\Temp"
rem 
rem ADX AUDIO TO AAC-HC V2
rem 
for %%f in (ROM\*.adx) do (copy /Y %%f Tools\AudioConversion\0ADX2WAV\)
cd Tools\AudioConversion\0ADX2WAV\
call ADX2WAV.bat
mkdir %WORKING_DIRECTORY%\ROM\BGM
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\1WAV2AACHCv2\*.m4a) do (move /Y %%f %WORKING_DIRECTORY%\ROM\BGM)
for %%f in (%WORKING_DIRECTORY%\ROM\*.adx) do (del /f %%f)
pause