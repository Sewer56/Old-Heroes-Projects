echo off
set WORKING_DIRECTORY=%cd%

rem FIX FOR VARS INSIDE LOOPS
setlocal enabledelayedexpansion
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
rem Add Menu Text
rem
mkdir "%WORKING_DIRECTORY%\ROM\Game Menus\Game Menu Text"
for %%f in ("%WORKING_DIRECTORY%\ROM\Text\*.utx") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Game Menus\Game Menu Text")
mkdir "%WORKING_DIRECTORY%\ROM\Game Menus\Credits Screen Text"
move /Y "%WORKING_DIRECTORY%\ROM\Text\staffroll.csv" "%WORKING_DIRECTORY%\ROM\Game Menus\Credits Screen Text"

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
mkdir "%WORKING_DIRECTORY%\ROM\Levels\TitleCardMissionText"
for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\mission\*.bmp") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\TitleCardMissionText")
for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\TitleCards")
cd %WORKING_DIRECTORY%\ROM\Levels\
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\TitleCards\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\TitleCards\*.one") do (DEL /F %%f)

move "%WORKING_DIRECTORY%\ROM\Levels\Title Cards\stg00title_disp" "%WORKING_DIRECTORY%\ROM\Levels\Unused\Title Cards\"
cd "%WORKING_DIRECTORY%\ROM\Levels\Unused\Title Cards"
ren "stg00title_disp" "Stage 00 - Testlevel"

cd "%WORKING_DIRECTORY%\ROM\Levels\Title Cards"
call %WORKING_DIRECTORY%\Scripts\RenameStageTitles.bat

rem
rem Set up Event Handling
rem
mkdir "%WORKING_DIRECTORY%\ROM\Events"
mkdir "%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents"
mkdir "%WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents"
mkdir "%WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents"
mkdir "%WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents"
mkdir "%WORKING_DIRECTORY%\ROM\Events\LastStoryEvents"
mkdir "%WORKING_DIRECTORY%\ROM\Events\Other~UnusedEvents"
for %%f in ("%WORKING_DIRECTORY%\ROM\event*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event00*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents\")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event01*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents\")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event02*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents\")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event03*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents\")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event04*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\LastStoryEvents\")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\Team SonicEvents\")

rem SONIC
rem Auto Gen Folders based on Suffix Discriminant
cd %WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents\event0*.scr") do (
	set suffix=%%~nxf
	if /I not !suffix:~-6!==_e.scr mkdir "%%~pnf"
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents\*") do (
	for %%f in ("%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents\*") do (
		set directoryname=%%~nxd
		set filename=%%~nf
		set test=!filename:~0,9!
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem DARK
rem Auto Gen Folders based on Suffix Discriminant
cd %WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents\event0*.scr") do (
	set suffix=%%~nxf
	if /I not !suffix:~-6!==_e.scr mkdir "%%~pnf"
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents\*") do (
	for %%f in ("%WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents\*") do (
		set directoryname=%%~nxd
		set filename=%%~nf
		set test=!filename:~0,9!
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem ROSE
rem Auto Gen Folders based on Suffix Discriminant
cd %WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents\event0*.scr") do (
	set suffix=%%~nxf
	if /I not !suffix:~-6!==_e.scr mkdir "%%~pnf"
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents\*") do (
	for %%f in ("%WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents\*") do (
		set directoryname=%%~nxd
		set filename=%%~nf
		set test=!filename:~0,9!
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem CHAOTIX
rem Auto Gen Folders based on Suffix Discriminant
cd %WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents\event0*.scr") do (
	set suffix=%%~nxf
	if /I not !suffix:~-6!==_e.scr mkdir "%%~pnf"
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents\*") do (
	for %%f in ("%WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents\*") do (
		set directoryname=%%~nxd
		set filename=%%~nf
		set test=!filename:~0,9!
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem LAST
rem Auto Gen Folders based on Suffix Discriminant
cd %WORKING_DIRECTORY%\ROM\Events\LastStoryEvents
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\LastStoryEvents\event0*.scr") do (
	set suffix=%%~nxf
	if /I not !suffix:~-6!==_e.scr mkdir "%%~pnf"
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Events\LastStoryEvents\*") do (
	for %%f in ("%WORKING_DIRECTORY%\ROM\Events\LastStoryEvents\*") do (
		set directoryname=%%~nxd
		set filename=%%~nf
		set test=!filename:~0,9!
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem UNUSED
rem Auto Gen Folders based on Suffix Discriminant
cd %WORKING_DIRECTORY%\ROM\Events\Other~UnusedEvents
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\Other~UnusedEvents\event0*.scr") do (
	set suffix=%%~nxf
	if /I not !suffix:~-6!==_e.scr mkdir "%%~pnf"
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Events\Other~UnusedEvents\*") do (
	for %%f in ("%WORKING_DIRECTORY%\ROM\Events\Other~UnusedEvents\*") do (
		set directoryname=%%~nxd
		set filename=%%~nf
		set test=!filename:~0,9!
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

cd %WORKING_DIRECTORY%\ROM\Events\
for /R %%f in ("*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for /R %%f in ("*.one") do (DEL /F %%f)

rem
rem Handle Character Models
rem
cd %WORKING_DIRECTORY%\ROM\
ren playmodel "CharacterModels"
cd %WORKING_DIRECTORY%\ROM\CharacterModels
call %WORKING_DIRECTORY%\Scripts\CreateCharacterFolders.bat

rem For Each Directory Do Sort Out Animations Models And Textures
for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		echo %%d
		set filename=%%~nf
		set directory=%%~nxf
		set test=!filename:~-4!
		if /I !test!==_anm (mkdir "%%~pfAnimations" & move "%%f" "%%~pfAnimations\")
	)
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		echo %%d
		set filename=%%~nf
		set directory=%%~nxf
		set test=!filename:~-4!
		if /I !test!==_dff (mkdir "%%~pfModels" & move "%%f" "%%~pfModels\")
	)
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		echo %%d
		set filename=%%~nxf
		set directory=%%~nxf
		set test=!filename:~-4!
		if /I !test!==.txd (mkdir "%%~pfTextures" & move "%%f" "%%~pfTextures\")
	)
)

cd "%WORKING_DIRECTORY%\ROM\CharacterModels\"
for /R %%f in ("*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for /R %%f in ("*.one") do (DEL /F %%f)

rem
rem
rem
rem
rem Set up Stage Geometry and Objects
rem
rem
rem
rem
mkdir "%WORKING_DIRECTORY%\ROM\Levels\StageGeometry"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\StageSpecificObjects"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
for %%f in ("%WORKING_DIRECTORY%\ROM\s**obj.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\StageSpecificObjects")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**MRG.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\StageSpecificObjects")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_flyer.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\StageSpecificObjects")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\StageGeometry")
for %%f in ("%WORKING_DIRECTORY%\ROM\stg**.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\StageGeometry")

REM Fixes for object filter inaccuracies,
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\StageGeometry\stg06_kw_hanabi_**.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects")
move /Y "%WORKING_DIRECTORY%\ROM\Levels\StageGeometry\stgmem0910.one" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\Levels\StageGeometry\stg80BJ.one" "%WORKING_DIRECTORY%\ROM\Levels\StageSpecificObjects"

REM Move other models to ROOTDIR Models
for %%f in ("%WORKING_DIRECTORY%\ROM\obj*_Prop*.dff") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects")
for %%f in ("%WORKING_DIRECTORY%\ROM\bob*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects")
move /Y "%WORKING_DIRECTORY%\ROM\rain_ita.dff" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\obj0708_sparks.dff" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\primModels.one" "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\indirectEditor.dff" "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\null.dff" "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\comobj.one" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"

for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\StageSpecificObjects\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\StageGeometry\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects\*.one") do (%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u %%f %%~pnf)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\StageSpecificObjects\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\StageGeometry\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\*.one") do (DEL /F %%f)
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects\*.one") do (DEL /F %%f)

cd "%WORKING_DIRECTORY%\ROM\Levels\StageGeometry"
call %WORKING_DIRECTORY%\Scripts\RenameStageGeometry.bat

cd "%WORKING_DIRECTORY%\ROM\Levels\StageSpecificObjects"
call %WORKING_DIRECTORY%\Scripts\RenameStageObjects.bat

cd "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
call %WORKING_DIRECTORY%\Scripts\RenameCommonObjects.bat

cd "%WORKING_DIRECTORY%\ROM\Levels\TitleCardMissionText"
call %WORKING_DIRECTORY%\Scripts\BundleMissionText.bat

for %%i in (%WORKING_DIRECTORY%\ROM\Levels\TitleCardMissionText\*.bmp) do (echo %%i & mkdir "%WORKING_DIRECTORY%\ROM\Levels\Unused\TitleCardMissionText" & move /Y "%%i" "%WORKING_DIRECTORY%\ROM\Levels\Unused\TitleCardMissionText\")

rem
rem Set up Stage Collision Data
rem
mkdir "%WORKING_DIRECTORY%\ROM\Levels\StageCollision"
for %%f in ("%WORKING_DIRECTORY%\ROM\collisions\*.cl") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Levels\StageCollision")
cd "%WORKING_DIRECTORY%\ROM\Levels\StageCollision"
call %WORKING_DIRECTORY%\Scripts\RenameStageCollisions.bat
cd %WORKING_DIRECTORY%

rem
rem Handle Level Layouts
rem
cd %WORKING_DIRECTORY%
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\AllTeams"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\TeamSonic"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\TeamDark"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\TeamRose"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\TeamChaotix"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\SuperHard"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\DecorationLayouts"
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_PB.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\AllTeams")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P1.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\TeamSonic")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P2.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\TeamDark")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P3.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\TeamRose")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P4.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\TeamChaotix")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P5.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\SuperHard")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_DB.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\DecorationLayouts")

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ObjectLayouts\*") do (
	cd %%d
	call %WORKING_DIRECTORY%\Scripts\CreateLevelFolders.bat
	for /D %%1 in ("%%d\*") do (
		set stagedir=%%~n1
		for %%f in ("%%d\*") do ( 
			cd %%~pf
			set filename=%%~nf
			set directory=%%~nxf
			set stagedirshort=!stagedir:~6,2!
			set test1=!filename:~-0,3!
			if /I not !test1!==stg (set test2=!filename:~1,2!) else (set test2=!filename:~3,2!)
			if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!stagedir!\")
		)
	)
)

rem
rem Handle AutoPlay Demos
rem
cd %WORKING_DIRECTORY%
mkdir "%WORKING_DIRECTORY%\ROM\Levels\IdleAutoplayDemos"
cd "%WORKING_DIRECTORY%\ROM\Levels\IdleAutoplayDemos"
call %WORKING_DIRECTORY%\Scripts\CreateLevelFolders.bat
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.dmo") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\IdleAutoplayDemos")
for /D %%1 in ("%WORKING_DIRECTORY%\ROM\Levels\IdleAutoplayDemos\*") do (
	set stagedir=%%~n1
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\IdleAutoplayDemos\*") do ( 
		set filename=%%~nf
		set directory=%%~nxf
		set stagedirshort=!stagedir:~6,2!
		set test1=!filename:~-0,3!
		if /I not !test1!==stg (set test2=!filename:~1,2!) else (set test2=!filename:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!stagedir!\")
	)
)

rem
rem Handle Lighting Data
rem
cd %WORKING_DIRECTORY%
mkdir "%WORKING_DIRECTORY%\ROM\Levels\StageLightingData"
cd "%WORKING_DIRECTORY%\ROM\Levels\StageLightingData"
call %WORKING_DIRECTORY%\Scripts\CreateLevelFolders.bat
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_light.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\StageLightingData")
for /D %%1 in ("%WORKING_DIRECTORY%\ROM\Levels\StageLightingData\*") do (
	set stagedir=%%~n1
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\StageLightingData\*") do ( 
		set filename=%%~nf
		set directory=%%~nxf
		set stagedirshort=!stagedir:~6,2!
		set test1=!filename:~-0,3!
		if /I not !test1!==stg (set test2=!filename:~1,2!) else (set test2=!filename:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!stagedir!\")
	)
)

rem
rem Handle Camera Data
rem
cd %WORKING_DIRECTORY%
mkdir "%WORKING_DIRECTORY%\ROM\Levels\StageCameraData"
cd "%WORKING_DIRECTORY%\ROM\Levels\StageCameraData"
call %WORKING_DIRECTORY%\Scripts\CreateLevelFolders.bat
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_cam.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\StageCameraData")
for /D %%1 in ("%WORKING_DIRECTORY%\ROM\Levels\StageCameraData\*") do (
	set stagedir=%%~n1
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\StageCameraData\*") do ( 
		set filename=%%~nf
		set directory=%%~nxf
		set stagedirshort=!stagedir:~6,2!
		set test1=!filename:~-0,3!
		if /I not !test1!==stg (set test2=!filename:~1,2!) else (set test2=!filename:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!stagedir!\")
	)
)

rem
rem Handle Geometry Visibility Data
rem
cd %WORKING_DIRECTORY%
mkdir "%WORKING_DIRECTORY%\ROM\Levels\StageGeometryVisibilityData"
cd "%WORKING_DIRECTORY%\ROM\Levels\StageGeometryVisibilityData"
call %WORKING_DIRECTORY%\Scripts\CreateLevelFolders.bat
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_blk.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\StageGeometryVisibilityData")
for /D %%1 in ("%WORKING_DIRECTORY%\ROM\Levels\StageGeometryVisibilityData\*") do (
	set stagedir=%%~n1
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\StageGeometryVisibilityData\*") do ( 
		set filename=%%~nf
		set directory=%%~nxf
		set stagedirshort=!stagedir:~6,2!
		set test1=!filename:~-0,3!
		if /I not !test1!==stg (set test2=!filename:~1,2!) else (set test2=!filename:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!stagedir!\")
	)
)

rem
rem
rem
rem
rem Set up Stage Geometry and Objects
rem
rem
rem
rem

echo ayymd
pause

rem
rem Cleanup
rem
cd %WORKING_DIRECTORY%\ROM\
for /R %%f in ("vssver.scc") do (DEL /F %%f)
rmdir /S /Q %WORKING_DIRECTORY%\ROM\Levels\TitleCards
rmdir /S /Q %WORKING_DIRECTORY%\ROM\GameMenus
rmdir /S /Q %WORKING_DIRECTORY%\ROM\stgtitle
rmdir /S /Q %WORKING_DIRECTORY%\ROM\text
rmdir /S /Q %WORKING_DIRECTORY%\ROM\collisions
rem Kill Empty Directories
cd %WORKING_DIRECTORY%\ROM\
for /f "usebackq delims=" %%d in (`"dir /ad/b/s | sort /R"`) do rd "%%d"

rem Add spaces back.
ren "TitleCards" "Title Cards" 
pause