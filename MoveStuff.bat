set WORKING_DIRECTORY=%cd%
rem
rem REARRANGE ADVERTISE
rem
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\French"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\German"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Italian"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Japanese"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Korean"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Spanish"
move /Y %WORKING_DIRECTORY%\ROM\advertise\* "%WORKING_DIRECTORY%\ROM\GameMenus"
move /Y %WORKING_DIRECTORY%\ROM\advertise\E\* "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English"
move /Y %WORKING_DIRECTORY%\ROM\advertise\F\* "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\French"
move /Y %WORKING_DIRECTORY%\ROM\advertise\G\* "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\German"
move /Y %WORKING_DIRECTORY%\ROM\advertise\I\* "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Italian"
move /Y %WORKING_DIRECTORY%\ROM\advertise\J\* "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Japanese"
move /Y %WORKING_DIRECTORY%\ROM\advertise\K\* "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Korean"
move /Y %WORKING_DIRECTORY%\ROM\advertise\S\* "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Spanish"
rmdir /S /Q %WORKING_DIRECTORY%\ROM\advertise
rem
rem ADD OTHER MENU ITEMS INSIDE
rem
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Transition Models"
move /Y %WORKING_DIRECTORY%\ROM\adv_ef_warpa.dff "%WORKING_DIRECTORY%\ROM\GameMenus\Transition Models"
move /Y %WORKING_DIRECTORY%\ROM\adv_ef_warpb.dff "%WORKING_DIRECTORY%\ROM\GameMenus\Transition Models"
move /Y %WORKING_DIRECTORY%\ROM\adv_sonicoutline.dff "%WORKING_DIRECTORY%\ROM\GameMenus\Transition Models"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\French"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\German"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\Italian"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\Spanish"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\English & Japanese"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup.dff "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\English & Japanese"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_fr.dff "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\French"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_ge.dff "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\German"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_it.dff "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\Italian"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_sp.dff "%WORKING_DIRECTORY%\ROM\GameMenus\Special Stage Time Up Models\Spanish"
rem
rem HeroesONE goes Harambe!
rem
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\*.one") do (DEL /F %%f)
rem
rem Move Main Menu Chars!
rem
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Main Menu Characters"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\adv_pl*") do (move /Y %%d "%WORKING_DIRECTORY%\ROM\GameMenus\Main Menu Characters\")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Main Menu Characters\"
call %WORKING_DIRECTORY%\Scripts\RenameMenuCharacters.bat
rem
rem Decompress PRS Fonts
rem
cd %WORKING_DIRECTORY%
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Fonts"
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\*.prs") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\Fonts")
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\Fonts\*.prs") do (%WORKING_DIRECTORY%\Tools\PrsDec\PrsDec.exe %%f %%~pnf.bin)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\Fonts\*.prs") do (DEL /F %%f)
rem
rem Move over Unused Files
rem
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_title" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_audio" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_e3rom" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused"
ren "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\adv_title" "Early E3-10.8 Title Screen"
ren "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\adv_audio" "Early Pre 10.8 Proto Audio Menu"
ren "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\adv_e3rom" "E3 Build Main Menu Assets"
rem
rem Group Assets
rem
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Assets"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\adv_*") do (move /Y %%d "%WORKING_DIRECTORY%\ROM\GameMenus\Assets")
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\as_emblem" "%WORKING_DIRECTORY%\ROM\GameMenus\Assets"
cd %WORKING_DIRECTORY%\ROM\GameMenus\Assets
ren adv_bg "Main Menu Background Assets"
ren adv_chao "Main Menu Omochao"
ren adv_help "Main Menu Omochao Help Icon"
ren adv_save "Autosave Menu"
ren adv_staffroll "Credits Screen Logos"
ren adv_window "Main Menu Window Textures"
ren as_emblem "Emblem Count Spinning Emblem"
rem
rem HeroesONE back for more Harambe. Now taking full languages.
rem
cd %WORKING_DIRECTORY%\ROM\GameMenus\
ren "Localized Menu Assets" "LocalizedMenuAssets"
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\English\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\English\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\French\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\French\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\German\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\German\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Italian\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Italian\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Japanese\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Japanese\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Korean\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Korean\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Spanish\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Spanish\*.one") do (DEL /F %%f)
ren "LocalizedMenuAssets" "Localized Menu Assets"
rem
rem Rename Language Specific Assets
rem
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English") do (call "%WORKING_DIRECTORY%\Scripts\RenameLocaleFiles.bat")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\French"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English") do (call "%WORKING_DIRECTORY%\Scripts\RenameLocaleFiles.bat")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\German"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English") do (call "%WORKING_DIRECTORY%\Scripts\RenameLocaleFiles.bat")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Italian"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English") do (call "%WORKING_DIRECTORY%\Scripts\RenameLocaleFiles.bat")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Japanese"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English") do (call "%WORKING_DIRECTORY%\Scripts\RenameLocaleFiles.bat")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Korean"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English") do (call "%WORKING_DIRECTORY%\Scripts\RenameLocaleFiles.bat")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\Spanish"
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\Localized Menu Assets\English") do (call "%WORKING_DIRECTORY%\Scripts\RenameLocaleFiles.bat")
cd %WORKING_DIRECTORY%\ROM\
ren GameMenus "Game Menus"
cd %WORKING_DIRECTORY%
rem
rem Bundle Game Code Together
rem
cd %WORKING_DIRECTORY%\ROM\
ren "&&systemdata" "Game Code"
move /Y movieD.rel "%WORKING_DIRECTORY%\ROM\Game Code\"
move /Y autosaveD.rel "%WORKING_DIRECTORY%\ROM\Game Code\"
move /Y advertiseD.rel "%WORKING_DIRECTORY%\ROM\Game Code\"
rem
rem Set up Stages
rem
mkdir %WORKING_DIRECTORY%\ROM\Levels
rem Without Spaces to ensure HeroesONE doesn't freak out.
mkdir "%WORKING_DIRECTORY%\ROM\Levels\TitleCards"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\Unused"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\Unused\Title Cards"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\Title Card Mission Text"
for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\mission\*.bmp") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\Title Card Mission Text")
for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\Title Cards")
cd %WORKING_DIRECTORY%\ROM\Levels\
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\TitleCards\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\TitleCards\*.one") do (DEL /F %%f)

rem Add spaces back.
ren "TitleCards" "Title Cards" 

move "%WORKING_DIRECTORY%\ROM\Levels\Title Cards\stg00title_disp" "%WORKING_DIRECTORY%\ROM\Levels\Unused\Title Cards\"
cd "%WORKING_DIRECTORY%\ROM\Levels\Unused\Title Cards"
ren "stg00title_disp" "Stage 00 - Testlevel"

cd "%WORKING_DIRECTORY%\ROM\Levels\Title Cards"
call %WORKING_DIRECTORY%\Scripts\RenameStageTitles.bat
rem
rem TestingCleanup
rem
rmdir /S /Q %WORKING_DIRECTORY%\ROM\Levels\TitleCards
rmdir /S /Q %WORKING_DIRECTORY%\ROM\GameMenus
rmdir /S /Q %WORKING_DIRECTORY%\ROM\stgtitle
pause