set WORKING_DIRECTORY=%cd%
for %%f in (ROM\*.afs) do (copy /Y %%f %WORKING_DIRECTORY%\Tools\AudioConversion\99AFS2ADX\)
cd %WORKING_DIRECTORY%\Tools\AudioConversion\99AFS2ADX\
AFSExplorer.exe SH_VOICE_E.afs
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\99AFS2ADX\*.afs) do (del /F %%f)
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\99AFS2ADX\FULL_AFS_FILE_DUMP\*.adx) do (move /Y %%f %WORKING_DIRECTORY%\Tools\AudioConversion\98AFSADX2WAV)
cd %WORKING_DIRECTORY%\Tools\AudioConversion\98AFSADX2WAV
call vgmstream.bat
rem m4a happened inbetween.
mkdir "%WORKING_DIRECTORY%\ROM\Character Speech"
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\1WAV2AACHCv2\*.m4a) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Character Speech")
for %%f in (%WORKING_DIRECTORY%\ROM\*.afs) do (del /F %%f)
pause