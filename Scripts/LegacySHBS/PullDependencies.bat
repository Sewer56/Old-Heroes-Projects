set WORKING_DIRECTORY=%cd%

if not exist %WORKING_DIRECTORY%\Tools\FFMPEG\ffmpeg-latest-win64-static.7z (%WORKING_DIRECTORY%\Tools\Wget\wget.exe --show-progress --directory-prefix=%WORKING_DIRECTORY%\Tools\FFMPEG\ https://ffmpeg.zeranoe.com/builds/win64/static/ffmpeg-latest-win64-static.7z)
%WORKING_DIRECTORY%\Tools\7z\7z.exe x -o%WORKING_DIRECTORY%\Tools\FFMPEG\ %WORKING_DIRECTORY%\Tools\FFMPEG\ffmpeg-latest-win64-static.7z
DEL %WORKING_DIRECTORY%\Tools\FFMPEG\ffmpeg-latest-win64-static.7z
pause