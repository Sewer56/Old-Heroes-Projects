rem 1 liner by Sewer56lol
for %%f in (*.wav) do (fdkaac.exe -p29 -m5 -a1 %%f)
for %%f in (*.wav) do (del /f %%f)