set WORKING_DIRECTORY=%cd%
for %%f in (ROM\*.adx) do (copy /Y %%f Tools\AudioConversion\0ADX2WAV\)
cd Tools\AudioConversion\0ADX2WAV\
call ADX2WAV.bat