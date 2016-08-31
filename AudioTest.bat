set WORKING_DIRECTORY=%cd%
for %%f in (ROM\*.adx) do (copy /Y %%f Tools\AudioConversion\0ADX2WAV\)
cd Tools\AudioConversion\0ADX2WAV\
call ADX2WAV.bat
mkdir %WORKING_DIRECTORY%\ROM\BGM
for %%f in (%WORKING_DIRECTORY%\Tools\AudioConversion\1WAV2AACHCv2\*.m4a) do (move /Y %%f %WORKING_DIRECTORY%\ROM\BGM)
for %%f in (%WORKING_DIRECTORY%\ROM\*.adx) do (del /f %%f)
pause