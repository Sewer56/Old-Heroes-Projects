mkdir C:\Temp
mkdir C:\Temp\Input
mkdir C:\Temp\Output
for %%f in (*.sfd) do (move /Y %%f C:\Temp\Input)
for %%f in (*.grf) do (graphstudionext.exe -run -exitonerror -exitafterrun -noclock -progressview %%f)