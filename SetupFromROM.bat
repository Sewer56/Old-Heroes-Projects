rem 
rem MASTER CODE XDDDDDD
rem 
set WORKING_DIRECTORY=%cd%
rem 
rem SOFDEC VIDEO TO X265
rem 
for %%f in (ROM\*.sfd) do (copy /Y %%f Tools\VideoConversion\0SFD2MPG)
cd Tools\VideoConversion\0SFD2MPG\
call ConvertHeroes.bat
cd Tools\VideoConversion\1Handbrake\
call HeroesToX265.bat
cd %WORKING_DIRECTORY%
for %%f in (ROM\*.sfd) do (del /f %%f)
mkdir ROM\Movies
for %%f in (C:\Temp\x265\*.avi) do (move /Y %%f ROM\Movies)
RD /S /Q "C:\Temp"
rem 
rem ADX AUDIO TO AAC-HC V2
rem 
for %%f in (ROM\*.adx) do (copy /Y %%f Tools\AudioConversion\0ADX2WAV\)
cd Tools\AudioConversion\0ADX2WAV\
call ADX2WAV.bat
pause