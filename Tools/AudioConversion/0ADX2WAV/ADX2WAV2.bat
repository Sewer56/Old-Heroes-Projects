rem 1 liner by Sewer56lol
for %%f in (*.adx) do (adx2wav.exe %cd%\%%f %cd%\%%~nf.wav)
for %%f in (*.wav) do (move /Y %%f %WORKING_DIRECTORY%\Tools\AudioConversion\1WAV2AACHCv2)
for %%f in (*.adx) do (del /f %%f)
cd %WORKING_DIRECTORY%\Tools\AudioConversion\1WAV2AACHCv2
call WAV2M4AAFS.bat