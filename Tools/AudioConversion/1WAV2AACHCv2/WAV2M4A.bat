rem 1 liner by Sewer56lol
for %%f in (*.wav) do (fdkaac.exe -p29 -m5 -o %cd%\%%~nf.m4a %cd%\%%f)
for %%f in (*.wav) do (del /f %%f)