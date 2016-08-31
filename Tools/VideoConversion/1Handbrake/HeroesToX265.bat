mkdir C:\Temp\x265
for %%f in (C:\Temp\Output\*.avi) do (
HandbrakeCLI -i "%%f" -t 1 --angle 1 -c 1 -o "C:\Temp\x265\1.mp4"  -f mp4 --crop 0:0:0:0 --loose-anamorphic  --modulus 2 -e x265 -q 25 --vfr -a 1 -E av_aac -6 dpl2 -R Auto -B 128 -D 0 --gain 0 --audio-fallback ac3 --markers="C:\Users\sewer56\AppData\Local\Temp\ayymd-1-chapters.csv" --verbose=1
ren C:\Temp\x265\1.mp4 %%~nxf
)
pause