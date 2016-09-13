@echo off
set WORKING_DIRECTORY=%cd%
rem FIX FOR VARS INSIDE LOOPS
setlocal enabledelayedexpansion

REM ############################################### USER CHOICE 01 BEGIN: DEFAULT CONVERSION

rem #01 REARRANGE ADVERTISE
echo "[01/30] REARRANGE ADVERTISE"
call :ReArrangeAdvertise

rem #02 ADD OTHER ITEMS TO MAINMENU
echo "[02/30] ADD OTHER ITEMS TO MAINMENU"
call :OtherMenuItems

rem #03 MOVE MAIN MENU CHARS!
echo "[03/30] MOVE MAIN MENU CHARS"
call :OtherMenuChars
call :GenerateMenuCharacters

rem #04 DECOMPRESS PRS FONTS
echo "[04/30] DECOMPRESS PRS FONTS"
call :DecompressPrsFonts

rem #05 MOVE UNUSED MENU FILES
echo "[05/30] MOVE UNUSED MENU FILES"
call :MoveUnusedMenuFiles

rem #06 GROUP MAIN MENU ASSETS
echo "[06/30] GROUP MAIN MENU ASSETS"
call :GroupMainMenuAssets
call :HardcodeGroupAssetsBatch

rem #07 LANGUAGE SPECIFIC ASSETS
echo "[07/30] LANGUAGE SPECIFIC ASSETS"
call :RenameLanguageSpecificMenuAssets

rem #08 MOVE MAIN MENU TEXT
echo "[08/30] MOVE MAIN MENU TEXT"
call :MoveMainMenuText

rem #09 MOVE GAMECUBE GAME CODE
echo "[09/30] MOVE GAMECUBE GAME CODE"
call :GameCubeGameCode

rem #10 SET UP INGAME EVENTS 
echo "[10/30] EXTRACT AND REARRANGE INGAME EVENTS"
call :SonicHeroesEvents

rem #11 SET UP CHARACTER MODELS
echo "[11/30] SET UP CHARACTER MODELS"
call :SetupCharacters

rem #12 SET UP LEVELS
echo "[12/30] SET UP LEVELS"
call :SetupLevelsDirectories
call :MoveLevelsAssets
call :SortOutCommonObjects
call :MoveGenericStageTitles
call :MoveCharacterStageTitles
call :MoveActionStageFilesToStageDir-DAT
call :MoveActionStageFilesToStageDir-DMO
call :MoveActionStageFilesToStageDir-TXC
call :MoveActionStageFilesToStageDir-SPL
call :MoveActionStageFilesToStageDir-TXD
call :MoveActionStageFilesToStageDir-ONE
call :MoveActionStageFilesToStageDir-CL
call :MoveActionStageFilesToStageDir-BIN
call :MoveActionStageFilesToStageDir-BMP


rem
rem Move Stuff Level Stuff To Proper Directory
rem
pause

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	for %%f in ("%%d\*") do ( 
		set FullFileName=%%~nxf
		set FileName=%%~nf
		set Extension=%%~xf
		set Last3Chars=!FileName:~-3!
		set Last5Chars=!FileName:~-5!
		set Last7Chars=!FileName:~-7!
		set Last8Chars=!FileName:~-8!
		set StageTitleTest=!FileName:~5,10!
		set StageTitleTestExtended=!FileName:~5,12!
		REM echo "Currently Processing File: !FileName!"
		echo !Extension!
		REM Move Stage Title Card Mission Text
		if /I !Extension!==.bmp (move /Y "%%f" "%%d\TitleCardMissionText\")
		REM Move Stage Specific Objects
		if /I !Last3Chars!==obj (move /Y "%%f" "%%d\StageSpecificObjects\")
		if /I !Last3Chars!==MRG (move /Y "%%f" "%%d\StageSpecificObjects\GeometryAndEnvironmentModels\")
		REM Move Stage All Team Layout
		if /I !Last3Chars!==_PB (move /Y "%%f" "%%d\ObjectLayouts\AllTeams\")
		REM Move Stage Team Sonic Layout
		if /I !Last3Chars!==_P1 (move /Y "%%f" "%%d\ObjectLayouts\TeamSonic\")
		REM Move Stage Team Dark Layout
		if /I !Last3Chars!==_P2 (move /Y "%%f" "%%d\ObjectLayouts\TeamDark\")
		REM Move Stage Team Rose Layout
		if /I !Last3Chars!==_P3 (move /Y "%%f" "%%d\ObjectLayouts\TeamRose\")
		REM Move Stage Team Chaotix Layout
		if /I !Last3Chars!==_P4 (move /Y "%%f" "%%d\ObjectLayouts\TeamChaotix\")
		REM Move Stage Super Hard Layout
		if /I !Last3Chars!==_P5 (move /Y "%%f" "%%d\ObjectLayouts\SuperHard\")
		REM Move Stage Decoration Layout
		if /I !Last3Chars!==_DB (move /Y "%%f" "%%d\ObjectLayouts\DecorationLayouts\")
		REM Move Stage Collision Data
		if /I !Extension!==.cl (
			if /I !Last3Chars!==_wt (move /Y "%%f" "%%d\Collision\WaterCollision\")
			if /I !Last3Chars!==_WT (move /Y "%%f" "%%d\Collision\WaterCollision\")
			if /I !Last3Chars!==_xx (move /Y "%%f" "%%d\Collision\DeathPlanes\")
			if /I !Last3Chars!==_XX (move /Y "%%f" "%%d\Collision\DeathPlanes\")
			move /Y "%%f" "%%d\Collision\"
		)
		REM Move Stage Title Cards
		if /I !StageTitleTestExtended!==title_dispEX (move /Y "%%f" "%%d\TitleCard\ExtraMission\")
		if /I !StageTitleTestExtended!==title_dispSH (move /Y "%%f" "%%d\TitleCard\SuperHard\")
		if /I !StageTitleTest!==title_disp (move /Y "%%f" "%%d\TitleCard\")
		REM Move Stage Autoplay Demos
		if /I !Extension!==.dmo (move /Y "%%f" "%%d\IdleAutoplayDemos\")
		REM Move Lighting Data
		if /I !Last5Chars!==light (move /Y "%%f" "%%d\LightingData\")
		REM Move FalcoEnemy
		if /I !Last5Chars!==flyer (move /Y "%%f" "%%d\CustomFalcoEnemy")
		REM Move Camera Data
		if /I !Last3Chars!==cam (move /Y "%%f" "%%d\CameraData\")
		REM Move Geometry Visibility Data
		if /I !Last3Chars!==blk (move /Y "%%f" "%%d\GeometryVisibilityData\")
		REM Move Textures
		if /I !Extension!==.txd (move /Y "%%f" "%%d\Textures\")
		if /I !Extension!==.txc (move /Y "%%f" "%%d\Textures\")
		REM Move Indirectional Data
		if /I !Last7Chars!==indinfo (move /Y "%%f" "%%d\IndirectionalData\")
		REM Move Particle Data
		if /I !Last5Chars!==_ptcl (move /Y "%%f" "%%d\ParticleData\")
		if /I !Last8Chars!==ptclplay (move /Y "%%f" "%%d\ParticleData\")
		REM Move Spline Data
		if /I !Extension!==.spl (move /Y "%%f" "%%d\ExtraSplineData\")
		REM Move Geometry
		set GeomFileNameTest1=!FileName:~1,2!
		set GeomFileNameTest2=!FileName:~3,2!
		if /I !FileName!==s!GeomFileNameTest1! (move /Y "%%f" "%%d\Geometry\")
		if /I !FileName!==stg!GeomFileNameTest2! (move /Y "%%f" "%%d\Geometry\")
	)
)

pause

REM
REM SORT OUT THE UNUSUAL LEVEL STUFF (This is pt2 to last method)
REM

REM Move Unknown OBJ to Unknown
move /Y "%WORKING_DIRECTORY%\ROM\stg05.bin" "%WORKING_DIRECTORY%\ROM\Levels\ActionStages\Stage 05 - Casino Park\Unknown\"
move /Y "%WORKING_DIRECTORY%\ROM\startStage.inf" "%WORKING_DIRECTORY%\ROM\Levels\Other\"
move /Y "%WORKING_DIRECTORY%\ROM\setidtbl.bin" "%WORKING_DIRECTORY%\ROM\Levels\Other\SETIDTable\"
move /Y "%WORKING_DIRECTORY%\ROM\cmn_ptcl.bin" "%WORKING_DIRECTORY%\ROM\Levels\Other\CommonParticleData\"

rem
rem Remove all of the remaining stage titles.
rem
for %%i in (%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.bmp) do (echo %%i & mkdir "%WORKING_DIRECTORY%\ROM\Levels\Unused\TitleCardMissionText" & move /Y "%%i" "%WORKING_DIRECTORY%\ROM\Levels\Unused\TitleCardMissionText\")


rem
rem Do all the Textures
rem
cd %WORKING_DIRECTORY%\ROM\
ren "textures" "Textures"
mkdir "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects"
mkdir "%WORKING_DIRECTORY%\ROM\Textures\Common\Loading"
mkdir "%WORKING_DIRECTORY%\ROM\Textures\Common\StartButton"

for %%f in (%WORKING_DIRECTORY%\ROM\Textures\*) do (
	set FileName=%%~nf
	set Extension=%%~xf
	set First3Chars=!FileName:~0,3!
	set First8Chars=!FileName:~0,8!
	
	echo "Extension:!Extension!"
	echo "FileName:!FileName!"
	if /I !First3Chars!==obj (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Common\")
	if /I !First3Chars!==eff (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects\")
	if /I !FileName!==rain_ita (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects\")
	if /I !FileName!==cmn_effect (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects\")
	if /I !First3Chars!==ef_ (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects\")
	if /I !First8Chars!==startbtn (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Common\StartButton\")
	if /I !FileName!==loading (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Common\Loading\")
	if /I !First3Chars!==en_ (mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\Enemies" & move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Unused\Enemies\")
	if /I !FileName!==e3adv (mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\E3" & move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Unused\E3\")
	if /I !FileName!==comsoon (mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\E3" & move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Unused\E3\")

	REM NTSC-U SPECIFIC
	if /I !FileName!==stg40_indinfo (mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Stage 40 - Bonus Stage 2\IndirectionalData" & move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Stage 40 - Bonus Stage 2\IndirectionalData\")
	
	REM PICK UP ANY OTHER SCRAPS LEFT
	if /I not !Extension!==.txd (mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Other" & move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Other\")
)

for %%f in (%WORKING_DIRECTORY%\ROM\Textures\*.txd) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Unused\")

rem
rem Sort out the Enemies
rem
cd %WORKING_DIRECTORY%\ROM
mkdir "Enemies~Bosses\Bosses"
mkdir "Enemies~Bosses\CommonAssets"
mkdir "Enemies~Bosses\Enemies"

for /D %%d in (%WORKING_DIRECTORY%\ROM\Enemies~Bosses\*) do (cd %%d && call %WORKING_DIRECTORY%\Scripts\Current\GenerateEnemyNames.bat)
for %%f in (%WORKING_DIRECTORY%\ROM\*.one) do (
	set FileName=%%~nf
	set First3Chars=!FileName:~0,3!
	set First7Chars=!FileName:~0,7!
	if /I !First7Chars!==chrboss (move /Y %%f "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\Bosses\")
	if /I !FileName!==en_common (move /Y %%f "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\CommonAssets\")
	if /I !FileName!==en_icon (move /Y %%f "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\CommonAssets\")
	if /I !First3Chars!==bs_ (move /Y %%f "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\Bosses\")
	if /I !First3Chars!==en_ (move /Y %%f "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\Enemies\")
)
for /D %%d in (%WORKING_DIRECTORY%\ROM\Enemies~Bosses\*) do (
	cd %%d
	for %%f in (%%d\*.one) do (
		set FileName=%%~nf
		set First12Chars=!FileName:~0,12!
		if /I !FileName!==bs_albatross (move /Y %%f "EggAlbatross\")
		if /I !FileName!==bs_egghawk (move /Y %%f "EggHawk\")
		if /I !FileName!==bs_kingpawn (move /Y %%f "EggEmperor\")
		if /I !FileName!==chrboss_disp (move /Y %%f "TeamBattleIcons\TeamSonic\")
		if /I !FileName!==chrboss_dispC (move /Y %%f "TeamBattleIcons\TeamChaotix\")
		if /I !FileName!==chrboss_dispD (move /Y %%f "TeamBattleIcons\TeamDark\")
		if /I !FileName!==chrboss_dispR (move /Y %%f "TeamBattleIcons\TeamRose\")
		if /I !FileName!==en_common (move /Y %%f "CommonAssets\")
		if /I !FileName!==en_capture (move /Y %%f "Klagen\")
		if /I !FileName!==en_e2000 (move /Y %%f "E2000\")
		if /I !First12Chars!==en_eggmobile (move /Y %%f "EggMobile\")
		if /I !FileName!==en_flyer (move /Y %%f "Falco\")
		if /I !FileName!==en_icon (move /Y %%f "EnemyIcons\")
		if /I !FileName!==en_magician (move /Y %%f "EggMagician\")
		if /I !FileName!==en_metalsonic1st (move /Y %%f "MetalSonic1\")
		if /I !FileName!==en_metalsonic2nd (move /Y %%f "MetalSonic2\")
		if /I !FileName!==en_pawn (move /Y %%f "EggPawn\")
		if /I !FileName!==en_pawn_roulette (move /Y %%f "EggPawnCasino\")
		if /I !FileName!==en_rinoliner (move /Y %%f "Rhinoliner\")
		if /I !FileName!==en_searcher (move /Y %%f "Flapper\")
		if /I !FileName!==en_turtle (move /Y %%f "Cameron\")
		if /I !FileName!==en_wall (move /Y %%f "HeavyEggHammer\")
	)
)

rem
rem Deal with SFX
rem
cd %WORKING_DIRECTORY%\ROM
mkdir "SoundEffects\SoundEffectTables"
mkdir "SoundEffects\Sounds"
mkdir "SoundEffects\SoundEffectSoundLibConfiguration"
@echo Similar stages and zones share one unique .mlt file, e.g. SE_S01ST.mlt is used for Seaside Hill and Ocean Palace.> "%WORKING_DIRECTORY%\ROM\SoundEffects\Sounds\Note.txt"
move /Y %WORKING_DIRECTORY%\ROM\GCAX.conf "%WORKING_DIRECTORY%\ROM\SoundEffects\SoundEffectSoundLibConfiguration\"
for %%f in (%WORKING_DIRECTORY%\ROM\se_*.mlt) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\SoundEffects\Sounds\")
for %%f in (%WORKING_DIRECTORY%\ROM\se_*.bin) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\SoundEffects\SoundEffectTables\")
cd %WORKING_DIRECTORY%\ROM\SoundEffects\Sounds\ && goto CreateLevelFolders
cd %WORKING_DIRECTORY%\ROM\SoundEffects\SoundEffectTables\ && goto CreateLevelFolders
for /D %%d in ("%WORKING_DIRECTORY%\ROM\SoundEffects\*") do (
	mkdir "%%d\Player\TeamSonic"
	mkdir "%%d\Player\TeamDark"
	mkdir "%%d\Player\TeamRose"
	mkdir "%%d\Player\TeamChaotix"
	mkdir "%%d\Enemy"
	mkdir "%%d\Common"
	mkdir "%%d\Vocal"
	mkdir "%%d\Unknown"
	for /D %%0 in ("%%d\*") do (
		for %%f in (%%d\*) do (
			set FileName=%%~nf
			set FolderName=%%~n0
			set LevelIDSFX=!FileName:~4,2!
			
			set LevelIDFolder=!FolderName:~6,2!
			
			set SFXTypeShort=!FileName:~3,2!
			set SFXType=!FileName:~3,5!
			
			echo "FolderName: !FolderName!"
			echo "FileName: !FileName!"
			echo "Level ID SFX CMP: !LevelIDSFX! - !LevelIDFolder!"
			echo "SFXType: !SFXType!"
			
			if /I not !FileName!==GCAX (if /I !LevelIDSFX!==!LevelIDFolder! (move /Y %%f "%%0\"))
			if /I !SFXTypeShort!==pl (move /Y %%f "%%d\Player\")
			if /I !SFXTypeShort!==en (move /Y %%f "%%d\Enemy\")
			if /I !SFXTypeShort!==cn (move /Y %%f "%%d\Common\")
			if /I !SFXTypeShort!==vo (move /Y %%f "%%d\Vocal\")
			if /I !SFXTypeShort!==sy (move /Y %%f "%%d\Unknown\")
			if /I !SFXType!==CMNGK (move /Y %%f "%%d\Common\")
			if /I !SFXType!==VOICE (move /Y %%f "%%d\Vocal\")
			if /I !SFXType!==BASIC (move /Y %%f "%%d\Common\")
			if /I !SFXType!==CH_TS (move /Y %%f "%%d\Player\TeamSonic\")
			if /I !SFXType!==CH_TD (move /Y %%f "%%d\Player\TeamDark\")
			if /I !SFXType!==CH_TR (move /Y %%f "%%d\Player\TeamRose\")
			if /I !SFXType!==CH_TC (move /Y %%f "%%d\Player\TeamChaotix\")
		)
	)
)

rem
rem Deal with the Heads up Display.
rem
cd %WORKING_DIRECTORY%\ROM\
mkdir HUD\ResultsScreen\TimeOnly
mkdir HUD\ResultsScreen\Normal
mkdir HUD\ResultsScreen\BonusStage
mkdir HUD\TeamSonic\
mkdir HUD\TeamDark\
mkdir HUD\TeamChaotix\
mkdir HUD\TeamRose\
mkdir HUD\2Player\

for /D %%d in (%WORKING_DIRECTORY%\ROM\HUD\*) do (
	mkdir "%%d\English"
	mkdir "%%d\French"
	mkdir "%%d\German"
	mkdir "%%d\Italian"
	mkdir "%%d\Korean"
	mkdir "%%d\Spanish"
)

for /D %%1 in (%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\*) do (
		mkdir "%%1\English"
		mkdir "%%1\French"
		mkdir "%%1\German"
		mkdir "%%1\Italian"
		mkdir "%%1\Korean"
		mkdir "%%1\Spanish"
)

for %%f in (%WORKING_DIRECTORY%\ROM\*.one) do (
	set FileName=%%~nf
	set HUDIdentifier=!FileName:~0,9!
	set ResultIdentifier=!FileName:~0,6!
	set HUDType=!FileName:~9,1!
	set HUDLanguage=!FileName:~-1!
	set ResultType=!FileName:~6,1!
	
	if /I !HUDIdentifier!==game_disp (
		REM TYPE SONIC
		if /I !FileName!==game_disp (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\")
		if /I !HUDType!==E (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\English\")
		if /I !HUDType!==F (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\French\")
		if /I !HUDType!==G (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\German\")
		if /I !HUDType!==I (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\Italian\")
		if /I !HUDType!==K (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\Korean\")
		if /I !HUDType!==S (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\Spanish\")
		
		REM TYPE 2PLAYER
		if /I !HUDType!==2 (
			if /I !FileName!==game_disp2 (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\2Player\")
			if /I !HUDLanguage!==E (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\2Player\English\")
			if /I !HUDLanguage!==F (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\2Player\French\")
			if /I !HUDLanguage!==G (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\2Player\German\")
			if /I !HUDLanguage!==I (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\2Player\Italian\")
			if /I !HUDLanguage!==K (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\2Player\Korean\")
			if /I !HUDLanguage!==S (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\2Player\Spanish\")
		)		
		
		REM TYPE CHAOTIX
		if /I !HUDType!==C (
			if /I !FileName!==game_dispC (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\")
			if /I !HUDLanguage!==E (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\English\")
			if /I !HUDLanguage!==F (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\French\")
			if /I !HUDLanguage!==G (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\German\")
			if /I !HUDLanguage!==I (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\Italian\")
			if /I !HUDLanguage!==K (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\Korean\")
			if /I !HUDLanguage!==S (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\Spanish\")
		)		
		
		REM TYPE DARK
		if /I !HUDType!==D (
			if /I !FileName!==game_dispD (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\")
			if /I !HUDLanguage!==E (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\English\")
			if /I !HUDLanguage!==F (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\French\")
			if /I !HUDLanguage!==G (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\German\")
			if /I !HUDLanguage!==I (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\Italian\")
			if /I !HUDLanguage!==K (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\Korean\")
			if /I !HUDLanguage!==S (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\Spanish\")
		)
		
		REM TYPE ROSE
		if /I !HUDType!==R (
			if /I !FileName!==game_dispR (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\")
			if /I !HUDLanguage!==E (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\English\")
			if /I !HUDLanguage!==F (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\French\")
			if /I !HUDLanguage!==G (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\German\")
			if /I !HUDLanguage!==I (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\Italian\")
			if /I !HUDLanguage!==K (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\Korean\")
			if /I !HUDLanguage!==S (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\Spanish\")
		)
	)
	
	if /I !ResultIdentifier!==result (
		REM RESULT TYPE A, NORMAL MODE
		if /I !ResultType!==A (
			if /I !FileName!==resultA_disp (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\")
			if /I !HUDLanguage!==F (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\French\")
			if /I !HUDLanguage!==G (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\German\")
			if /I !HUDLanguage!==I (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\Italian\")
			if /I !HUDLanguage!==S (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\Spanish\")
		)		
		REM RESULT TYPE B, TIME ONLY
		if /I !ResultType!==B (
			if /I !FileName!==resultB_disp (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\")
			if /I !HUDLanguage!==F (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\French\")
			if /I !HUDLanguage!==G (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\German\")
			if /I !HUDLanguage!==I (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\Italian\")
			if /I !HUDLanguage!==S (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\Spanish\")
		)		
		REM RESULT TYPE S, BONUS STAGE
		if /I !ResultType!==S (
			if /I !FileName!==resultS_disp (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\")
			if /I !HUDLanguage!==F (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\French\")
			if /I !HUDLanguage!==G (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\German\")
			if /I !HUDLanguage!==I (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\Italian\")
			if /I !HUDLanguage!==S (move /Y %%f "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\Spanish\")
		)
	)
)


rem
rem Sort out what is left in the ROOTDIR. Misc preprogrammed actions.
rem
mkdir "%WORKING_DIRECTORY%\ROM\Unknown\Group1\"

for %%f in (%WORKING_DIRECTORY%\ROM\*) do (
	set FileName=%%~nf
	set CoverIdentifier=!FileName:~0,9!
	set StartbtnIdentifier=!FileName:~0,8!
	set CoverLanguage=!FileName:~9,2!
	set StartbtnLanguage=!FileName:~9,2!
	
	set 3Chars=!FileName:~0,3!

	if /I !CoverIdentifier!==coverOpen (
		if /I !FileName!==coverOpen (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Unknown\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Unknown\")
		if /I !CoverLanguage!==en (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\English\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\English\")
		if /I !CoverLanguage!==fr (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\French\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\French\")
		if /I !CoverLanguage!==ge (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\German\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\German\")
		if /I !CoverLanguage!==it (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Italian\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Italian\")
		if /I !CoverLanguage!==jp (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Japanese\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Japanese\")
		if /I !CoverLanguage!==ko (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Korean\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Korean\")
		if /I !CoverLanguage!==sp (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Spanish\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Spanish\")
	)	
	
	if /I !StartbtnIdentifier!==startbtn (
		if /I !StartbtnLanguage!==en (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\English\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\English\")
		if /I !StartbtnLanguage!==fr (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\French\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\French\")
		if /I !StartbtnLanguage!==ge (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\German\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\German\")
		if /I !StartbtnLanguage!==it (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Italian\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Italian\")
		if /I !StartbtnLanguage!==jp (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Japanese\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Japanese\")
		if /I !StartbtnLanguage!==ko (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Korean\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Korean\")
		if /I !StartbtnLanguage!==sp (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Spanish\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Spanish\")
	)
	
	if /I !FileName!==opening (mkdir "%WORKING_DIRECTORY%\ROM\Other\GameCubeBanner\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\GameCubeBanner\")
	if /I !FileName!==pk1_par1 (mkdir "%WORKING_DIRECTORY%\ROM\Unused\SeasideHillGiantBirdTexture\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Unused\SeasideHillGiantBirdTexture\")
	if /I !FileName!==mte_gcn (mkdir "%WORKING_DIRECTORY%\ROM\Other\GameCubeMaterialDefinitions\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\GameCubeMaterialDefinitions\")
	if /I !FileName!==startLoad (mkdir "%WORKING_DIRECTORY%\ROM\Other\LoadingLogoOnGameLaunch\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\LoadingLogoOnGameLaunch\")
	if /I !FileName!==loading (mkdir "%WORKING_DIRECTORY%\ROM\Other\TeamBattle~E3ProtoLoadAnimation\" && move /Y %%f "%WORKING_DIRECTORY%\ROM\Other\TeamBattle~E3ProtoLoadAnimation\")
	
	if /I !3Chars!==mc_ (move /Y %%f "%WORKING_DIRECTORY%\ROM\Unknown\Group1\")
)

rem
rem Sort out fonts and ingame text.
rem
cd %WORKING_DIRECTORY%\ROM\
ren "font" "Fonts~Text"
REM No scripting, time waster, not enough dirs to be worth 0.001s performance loss XDDDDD.
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\FontMaps
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\Metrics
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\TextFontMaps

mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\Metrics
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps

mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\FontMaps
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\Metrics
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\TextFontMaps

mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\Metrics
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\TextFontMaps

mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\TextFontMaps
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\Metrics
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps

mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\FontMaps
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\Metrics
mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\TextFontMaps

mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\ErrorMessage\

REM TSONIC
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\TextFontMaps"

mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\TextFontMaps"

REM TDARK
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\TextFontMaps"

mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\TextFontMaps"

REM TROSE
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\TextFontMaps"

mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\TextFontMaps"

REM TCHAOTIX
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\TextFontMaps"

mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\TextFontMaps"

REM TLASTSTORY
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\TextFontMaps"

mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\TextFontMaps"

REM Unused
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\TextFontMaps"

REM Unused
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps"
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps"

REM HINTTEXT
mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\"
cd "%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\"
goto CreateLevelFolders

for /D %%d in (%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*) do (
	mkdir "%%d\English"
	mkdir "%%d\French"
	mkdir "%%d\German"
	mkdir "%%d\Italian"
	mkdir "%%d\Korean"
	mkdir "%%d\Spanish"
	mkdir "%%d\Metrics"
	mkdir "%%d\FontMaps"
	mkdir "%%d\TextFontMaps"
)

for /D %%d in (%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\*) do (
	mkdir "%%d\English"
	mkdir "%%d\French"
	mkdir "%%d\German"
	mkdir "%%d\Italian"
	mkdir "%%d\Korean"
	mkdir "%%d\Spanish"
	mkdir "%%d\Metrics"
	mkdir "%%d\FontMaps"
	mkdir "%%d\TextFontMaps"
)

for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\*) do (
	set FileName=%%~nf
	set Extension=%%~xf
	
	set Ascii=!FileName:~0,9!
	set ScoreNums=!FileName:~0,7!
	set JPAscii=!FileName:~0,3!
	set MainMenu=!FileName:~0,4!
	
	set EventLanguage=!FileName:~10,1!
	set EventTest=!FileName:~0,5!
	set EventTeam=!FileName:~6,1!
	
	set HintIdentifier=!FileName:~0,4!
	set HintLanguage=!FileName:~7,1!

	set MainMenuIdentifier=!FileName:~0,8!
	set MainMenuLanguage=!FileName:~8,1!
	
	REM ASCII EUROPE
	if /I !Ascii!==a_euascii (
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\Metrics\")
	)
	
	REM SCORE NUMBERS ALL
	if /I !ScoreNums!==a_num_s (
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\Metrics\")
	)
	
	REM JAPAN ASCII1
	if /I !JPAscii!==abc (
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\Metrics\")
	)
	
	REM JAPAN ASCII2
	if /I !JPAscii!==def (
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\Metrics\")
	)
	
	REM UNUSED ASCII METRICS ENGLISH1
	if /I !Ascii!==a_enascii (
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\Metrics\")
	)
	
	REM UNUSED ASCII METRICS ENGLISH2
	if /I !FileName!==ascii00 (
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\Metrics\")
	)	
	
	REM MAIN MENU ASCII ALL
	if /I !MainMenu!==adv_ (
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\Metrics\")
	) else (
		if /I !MainMenu!==madv (
			if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps\")
			if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps\")
			if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\TextFontMaps\")
			if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\Metrics\")
		)
	)
	
	REM DUMMY ASCII AYY LMAO
	if /I !FileName!==dummy (
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\Dummy\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\Dummy\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\Dummy\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\Dummy\Metrics\")
	)

	if /I !FileName!==errMessage (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\ErrorMessage\")
	
	REM EVENTS
	if /I !EventTest!==event (
		REM SONIC
		if /I !EventTeam!==0 (
			if /I !EventLanguage!==k (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\Metrics\")
			)
			if /I !EventLanguage!==j (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\Metrics\")
			)
		)
		
		REM DARK
		if /I !EventTeam!==1 (
			if /I !EventLanguage!==k (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\Metrics\")
			)
			if /I !EventLanguage!==j (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\Metrics\")
			)
		)
		
		REM ROSE
		if /I !EventTeam!==2 (
			if /I !EventLanguage!==k (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\Metrics\")
			)
			if /I !EventLanguage!==j (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\Metrics\")
			)
		)
		
		REM CHAOTIX
		if /I !EventTeam!==3 (
			if /I !EventLanguage!==k (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\Metrics\")
			)
			if /I !EventLanguage!==j (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\Metrics\")
			)
		)
		
		REM LAST
		if /I !EventTeam!==4 (
			if /I !EventLanguage!==k (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\Metrics\")
			)
			if /I !EventLanguage!==j (
				if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\FontMaps\")
				if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\FontMaps\")
				if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\TextFontMaps\")
				if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\Metrics\")
			)
		)
		
		REM HINTMENU
		echo "MMI: !MainMenuIdentifier!"
		if /I !MainMenuIdentifier!==hintmenu (
			if /I !HintLanguage!==e (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\English\")
			if /I !HintLanguage!==f (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\French\")
			if /I !HintLanguage!==g (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\German\")
			if /I !HintLanguage!==i (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Italian\")
			if /I !HintLanguage!==k (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Korean\")
			if /I !HintLanguage!==s (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Spanish\") 
			if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\")
			if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\")
			if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps\")
			if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\")
		)
		
		if /I !Extension!==.png (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\FontMaps\")
		if /I !Extension!==.bmp (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\FontMaps\")
		if /I !Extension!==.txt (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\TextFontMaps\")
		if /I !Extension!==.met (move /Y %%f "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\Metrics\")
		
	)
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*") do (
	mkdir "%%d\Metrics\Japanese"
	mkdir "%%d\Metrics\Korean"
	mkdir "%%d\FontMaps\Japanese"
	mkdir "%%d\FontMaps\Korean"
	mkdir "%%d\TextFontMaps\Korean"
	mkdir "%%d\TextFontMaps\Japanese"
	for %%f in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\*") do (
		set FileName=%%~nf
		set Extension=%%~xf
	
		set Directory=%%~nd
		set DirStageID=!Directory:~6,2!
		set StageID=!FileName:~5,2!
		set StageID2=!FileName:~6,2!
	
		set HintIdentifier=!FileName:~0,4!
		set HintMenuIdentifier=!FileName:~0,4!
		set HintLanguage=!FileName:~7,1!
		
		REM HINTS
		if /I !HintIdentifier!==Hint (
			if /I !DirStageID!==!StageID! (
			
				if /I !Extension!==.png (
				if /I !HintLanguage!==k (move /Y %%f "%%d\FontMaps\Korean")
				if /I !HintLanguage!==j (move /Y %%f "%%d\FontMaps\Japanese") )

				if /I !Extension!==.bmp (
				if /I !HintLanguage!==k (move /Y %%f "%%d\FontMaps\Korean")
				if /I !HintLanguage!==j (move /Y %%f "%%d\FontMaps\Japanese") )

				if /I !Extension!==.txt (
				if /I !HintLanguage!==k (move /Y %%f "%%d\TextFontMaps\Korean")
				if /I !HintLanguage!==j (move /Y %%f "%%d\TextFontMaps\Japanese") )
		
				if /I !Extension!==.met (
				if /I !HintLanguage!==k (move /Y %%f "%%d\Metrics\Korean")
				if /I !HintLanguage!==j (move /Y %%f "%%d\Metrics\Japanese") )
			
				if /I !HintLanguage!==e (move /Y %%f "%%d\English")
				if /I !HintLanguage!==f (move /Y %%f "%%d\French")
				if /I !HintLanguage!==g (move /Y %%f "%%d\German")
				if /I !HintLanguage!==i (move /Y %%f "%%d\Italian")
				if /I !HintLanguage!==k (move /Y %%f "%%d\Korean")
				if /I !HintLanguage!==s (move /Y %%f "%%d\Spanish") else (move /Y %%f "%%d\")
			) else (
				if /I !DirStageID!==!StageID2! (
				
					if /I !Extension!==.png (
					if /I !HintLanguage!==k (move /Y %%f "%%d\FontMaps\Korean")
					if /I !HintLanguage!==j (move /Y %%f "%%d\FontMaps\Japanese") )

					if /I !Extension!==.bmp (
					if /I !HintLanguage!==k (move /Y %%f "%%d\FontMaps\Korean")
					if /I !HintLanguage!==j (move /Y %%f "%%d\FontMaps\Japanese") )

					if /I !Extension!==.txt (
					if /I !HintLanguage!==k (move /Y %%f "%%d\TextFontMaps\Korean")
					if /I !HintLanguage!==j (move /Y %%f "%%d\TextFontMaps\Japanese") )
		
					if /I !Extension!==.met (
					if /I !HintLanguage!==k (move /Y %%f "%%d\Metrics\Korean")
					if /I !HintLanguage!==j (move /Y %%f "%%d\Metrics\Japanese") )				

					if /I !HintLanguage!==e (move /Y %%f "%%d\English")
					if /I !HintLanguage!==f (move /Y %%f "%%d\French")
					if /I !HintLanguage!==g (move /Y %%f "%%d\German")
					if /I !HintLanguage!==i (move /Y %%f "%%d\Italian")
					if /I !HintLanguage!==k (move /Y %%f "%%d\Korean")
					if /I !HintLanguage!==s (move /Y %%f "%%d\Spanish") else (move /Y %%f "%%d\")
				)
			)
		)
	)
)


echo "AYY LMAO"
echo "AYY LMAO"
echo "AYY LMAO"
pause
rem
rem HeroesONE goes Harambe! AYAYAYAYAYAYAY!!!
rem
cd %WORKING_DIRECTORY%\ROM\
rem Protect Weird .one files from HeroesONE and Removal
for /D %%d in (%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\*) do (
	for %%f in (%%d\*.one) do (
		cd %%d
		ren "%%~nxf" "%%~xnf.tmp"
	)
)

cd "%WORKING_DIRECTORY%\ROM\"
for /R %%f in ("*.one") do (
	echo "HeroesONE: Extracting %%~nxf"
	%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u "%%f" "%%~pnf.HeroesONE"
)
for /R %%f in ("*.one") do (DEL /F "%%f")

cd %WORKING_DIRECTORY%\ROM\
rem Protect Weird .one files from HeroesONE and Removal 2
for /D %%d in (%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\*) do (
	for %%f in (%%d\*.tmp) do (
		cd %%d
		ren "%%~nxf" "%%~nf"
	)
)

rem
rem Cleanup
rem

rem Remove Useless Microsoft Sourcesafe Leftovers
for /R %%f in ("*") do ( 
	if /I %%~nxf==vssver.scc (DEL /F %%f) 
)

rem Kill Empty Directories
cd %WORKING_DIRECTORY%\ROM\
for /f "usebackq delims=" %%d in (`"dir /ad/b/s | sort /R"`) do rd "%%d"

rem ALL HAIL MARKEYJESTER
rem ALL HAIL MARKEYJESTER
rem ALL HAIL MARKEYJESTER
echo "DONE"

REM METHODS
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM ###########################################################################################################
REM METHODS

:ReArrangeAdvertise
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\English"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\French"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\German"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Italian"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Japanese"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Korean"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Spanish"
move /Y %WORKING_DIRECTORY%\ROM\advertise\* "%WORKING_DIRECTORY%\ROM\GameMenus"
move /Y %WORKING_DIRECTORY%\ROM\advertise\E\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\English"
move /Y %WORKING_DIRECTORY%\ROM\advertise\F\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\French"
move /Y %WORKING_DIRECTORY%\ROM\advertise\G\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\German"
move /Y %WORKING_DIRECTORY%\ROM\advertise\I\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Italian"
move /Y %WORKING_DIRECTORY%\ROM\advertise\J\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Japanese"
move /Y %WORKING_DIRECTORY%\ROM\advertise\K\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Korean"
move /Y %WORKING_DIRECTORY%\ROM\advertise\S\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Spanish"
goto :eof

:OtherMenuItems
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels"
move /Y %WORKING_DIRECTORY%\ROM\adv_ef_warpa.dff "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels"
move /Y %WORKING_DIRECTORY%\ROM\adv_ef_warpb.dff "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels"
move /Y %WORKING_DIRECTORY%\ROM\adv_sonicoutline.dff "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\French"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\German"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Italian"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Spanish"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\English & Japanese"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\English&Japanese"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_fr.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\French"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_ge.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\German"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_it.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Italian"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_sp.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Spanish"
goto :eof

:OtherMenuChars
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\MainMenuCharacters"
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\adv_pl*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\MainMenuCharacters\")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\MainMenuCharacters\"
goto :eof

:DecompressPrsFonts
cd %WORKING_DIRECTORY%
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Fonts"
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\*.prs") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\Fonts")
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\Fonts\*.prs") do (%WORKING_DIRECTORY%\Tools\PrsDec\PrsDec.exe %%f %%~pnf.bin)
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\Fonts\*.prs") do (DEL /F %%f)
goto :eof

:GenerateMenuCharacters
mkdir "Amy"
mkdir "CharmyBee"
mkdir "BigTheCat"
mkdir "Cream"
mkdir "Espio"
mkdir "Knuckles"
mkdir "Omega"
mkdir "Rouge"
mkdir "Shadow"
mkdir "Sonic"
mkdir "Tails"
mkdir "Vector"
mkdir "AllPlayers"
mkdir "Unused~Unidentified"
move /Y adv_pl_amy.one "Amy\"
move /Y adv_pl_bee.one "CharmyBee\"
move /Y adv_pl_big.one "BigTheCat\"
move /Y adv_pl_cream.one "Cream\"
move /Y adv_pl_espio.one "Espio\"
move /Y adv_pl_knuckles.one "Knuckles\"
move /Y adv_pl_omega.one "Omega\"
move /Y adv_pl_rouge.one "Rouge\"
move /Y adv_pl_shadow.one "Shadow\"
move /Y adv_pl_sonic.one "Sonic\"
move /Y adv_pl_tails.one "Tails\"
move /Y adv_pl_vector.one "Vector\"
move /Y adv_player.one "AllPlayers\"
for %%f in (adv_pl*.one) do (move /Y %%f "Unused~Unidentified\")
goto :eof

:MoveUnusedMenuFiles
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused"
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Unused"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyE3-10.8Title Screen"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyPre10.8ProtoAudioMenu"
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\E3BuildMainMenuAssets"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_title.one" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyE3-10.8Title Screen"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_audio.one" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyPre10.8ProtoAudioMenu"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_e3rom.one" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\E3BuildMainMenuAssets"
goto :eof

:GroupMainMenuAssets
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Assets"
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\adv_*") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\Assets")
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\as_emblem.one" "%WORKING_DIRECTORY%\ROM\GameMenus\Assets"
cd %WORKING_DIRECTORY%\ROM\GameMenus\Assets
goto :eof

:HardcodeGroupAssetsBatch
mkdir "MainMenuBackgroundAssets"
mkdir "MainMenuOmochao"
mkdir "MainMenuOmochaoHelpIcon"
mkdir "AutosaveMenu"
mkdir "CreditsScreenLogos"
mkdir "MainMenuWindowTextures"
mkdir "EmblemCountSpinningEmblem"

move /Y adv_bg.one "MainMenuBackgroundAssets\"
move /Y adv_chao.one "MainMenuOmochao\"
move /Y adv_help.one "MainMenuOmochaoHelpIcon\"
move /Y adv_save.one "AutosaveMenu\"
move /Y adv_staffroll.one "CreditsScreenLogos\"
move /Y adv_window.one "MainMenuWindowTextures\"
move /Y as_emblem.one "EmblemCountSpinningEmblem\"
goto :eof

:RenameLanguageSpecificMenuAssets
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\*") do (cd %%d & call "%WORKING_DIRECTORY%\Scripts\Current\RenameLocaleFiles.bat")
goto :eof

:MoveMainMenuText
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\GameMenuText"
for %%f in ("%WORKING_DIRECTORY%\ROM\Text\*.utx") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\GameMenuText")
mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\CreditsScreenText"
move /Y "%WORKING_DIRECTORY%\ROM\Text\staffroll.csv" "%WORKING_DIRECTORY%\ROM\GameMenus\CreditsScreenText"
goto :eof

:CreateLevelFolders
mkdir "Stage 01 - Seaside Hill"
mkdir "Stage 02 - Ocean Palace"
mkdir "Stage 03 - Grand Metropolis"
mkdir "Stage 04 - Power Plant"
mkdir "Stage 11 - Hang Castle"
mkdir "Stage 12 - Mystic Mansion"
mkdir "Stage 13 - Egg Fleet"
mkdir "Stage 14 - Final Fortress"
mkdir "Stage 20 - Egg Hawk"
mkdir "Stage 21 - Team Battle 1"
mkdir "Stage 22 - Robot Carnival"
mkdir "Stage 23 - Egg Albatross"
mkdir "Stage 25 - Final Fortress"
mkdir "Stage 27 - Metal Madness"
mkdir "Stage 28 - Metal Overlord"
mkdir "Stage 29 - Sea Gate"
mkdir "Stage 31 - Seaside Course"
mkdir "Stage 32 - City Course"
mkdir "Stage 33 - Casino Course"
mkdir "Stage 60 - Seaside Hill 2P"
mkdir "Stage 61 - Grand Metropolis 2P"
mkdir "Stage 63 - City Top 2P"
mkdir "Stage 65 - Turtle Shell 2P"
mkdir "Stage 66 - Egg Treat 2P"
mkdir "Stage 68 - Hot Elevator 2P"
mkdir "Stage 69 - Road Rock 2P"
mkdir "Stage 71 - Terror Hall 2P"
mkdir "Stage 74 - Egg Fleet 2P"

mkdir "Stage 00 - Test Level"
mkdir "Stage 05 - Casino Park"
mkdir "Stage 06 - BINGO Highway"
mkdir "Stage 07 - Rail Canyon"
mkdir "Stage 08 - Bullet Station"
mkdir "Stage 09 - Frog Forest"
mkdir "Stage 10 - Lost Jungle"
mkdir "Stage 24 - Team Battle 2"
mkdir "Stage 26 - Egg Emperor"
mkdir "Stage 40 - Bonus Stage 2"
mkdir "Stage 41 - Bonus Stage 1"
mkdir "Stage 42 - Bonus Stage 3"
mkdir "Stage 43 - Bonus Stage 4"
mkdir "Stage 44 - Bonus Stage 5"
mkdir "Stage 45 - Bonus Stage 6"
mkdir "Stage 46 - Bonus Stage 7"
mkdir "Stage 50 - Team Chaotix Rail Canyon"
mkdir "Stage 62 - BINGO Highway 2P"
mkdir "Stage 64 - Casino Ring 2P"
mkdir "Stage 67 - Pinball Match 2P"
mkdir "Stage 70 - Mad Express 2P"
mkdir "Stage 72 - Rail Canyon 2P"
mkdir "Stage 73 - Frog Forest 2P"
mkdir "Stage 80 - Emerald Challenge 2"
mkdir "Stage 81 - Emerald Challenge 1"
mkdir "Stage 82 - Emerald Challenge 3"
mkdir "Stage 83 - Emerald Challenge 4"
mkdir "Stage 84 - Emerald Challenge 5"
mkdir "Stage 85 - Emerald Challenge 6"
mkdir "Stage 86 - Emerald Challenge 7"
mkdir "Stage 87 - Special Stage 1 2P"
mkdir "Stage 88 - Special Stage 2 2P"
mkdir "Stage 89 - Special Stage 3 2P"

mkdir "Stage 15 - Unused"
mkdir "Stage 16 - Unused"
mkdir "Stage 17 - Unused"
mkdir "Stage 18 - Unused"
mkdir "Stage 19 - Unused"
mkdir "Stage 30 - Unused"
mkdir "Stage 34 - Unused"
mkdir "Stage 35 - Unused"
mkdir "Stage 36 - Unused"
mkdir "Stage 37 - Unused"
mkdir "Stage 38 - Unused"
mkdir "Stage 39 - Unused"
mkdir "Stage 47 - Unused"
mkdir "Stage 48 - Unused"
mkdir "Stage 49 - Unused"
mkdir "Stage 51 - Unused"
mkdir "Stage 52 - Unused"
mkdir "Stage 53 - Unused"
mkdir "Stage 54 - Unused"
mkdir "Stage 55 - Unused"
mkdir "Stage 56 - Unused"
mkdir "Stage 57 - Unused"
mkdir "Stage 58 - Unused"
mkdir "Stage 59 - Unused"
mkdir "Stage 75 - Unused"
mkdir "Stage 76 - Unused"
mkdir "Stage 77 - Unused"
mkdir "Stage 78 - Unused"
mkdir "Stage 79 - Unused"
mkdir "Stage 90 - Unused"
mkdir "Stage 91 - Unused"
mkdir "Stage 92 - Unused"
mkdir "Stage 93 - Unused"
mkdir "Stage 94 - Unused"
mkdir "Stage 95 - Unused"
mkdir "Stage 96 - Unused"
mkdir "Stage 97 - Unused"
mkdir "Stage 98 - Unused"
mkdir "Stage 99 - Unused"

mkdir "Stage XX - Common Assets\GenericStageTitles\English\ExtraMission"
mkdir "Stage XX - Common Assets\GenericStageTitles\English\SuperHard"

mkdir "Stage XX - Common Assets\GenericStageTitles\French\SuperHard"
mkdir "Stage XX - Common Assets\GenericStageTitles\French\ExtraMission"

mkdir "Stage XX - Common Assets\GenericStageTitles\German\ExtraMission"
mkdir "Stage XX - Common Assets\GenericStageTitles\German\SuperHard"

mkdir "Stage XX - Common Assets\GenericStageTitles\Italian\ExtraMission"
mkdir "Stage XX - Common Assets\GenericStageTitles\Italian\SuperHard"

mkdir "Stage XX - Common Assets\GenericStageTitles\Spanish\ExtraMission"
mkdir "Stage XX - Common Assets\GenericStageTitles\Spanish\SuperHard"

mkdir "Stage XX - Common Assets\TeamBattleTitles\Chaotix\English"
mkdir "Stage XX - Common Assets\TeamBattleTitles\Rose\English"
mkdir "Stage XX - Common Assets\TeamBattleTitles\Dark\English"
mkdir "Stage XX - Common Assets\TeamBattleTitles\Sonic\English"
goto :eof

:GameCubeGameCode
mkdir "%WORKING_DIRECTORY%\ROM\GameCode\StageRelocatableModuleFiles"
cd "%WORKING_DIRECTORY%\ROM\GameCode\StageRelocatableModuleFiles"
goto CreateLevelFolders
cd %WORKING_DIRECTORY%\ROM\

for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameCode\StageRelocatableModuleFiles\*") do (
	for %%f in (*.rel) do (
		set FileName=%%~nf
		set FolderName=%%~nd
		set First5Letters=!FileName:~0,5!
		set LevelIDRel=!FileName:~5,2!
		set LevelIDFolder=!FolderName:~6,2!
		if /I !First5Letters!==stage ( if /I !LevelIDRel!==!LevelIDFolder! ( move /Y %%f "%%d\" ) ) else ( move /Y %%f "%WORKING_DIRECTORY%\ROM\GameCode\" )
	)
)
move /Y "%WORKING_DIRECTORY%\ROM\&&systemdata\*.dol" "%WORKING_DIRECTORY%\ROM\GameCode\"
move /Y "%WORKING_DIRECTORY%\ROM\TSonic.str" "%WORKING_DIRECTORY%\ROM\GameCode\"
move /Y "%WORKING_DIRECTORY%\ROM\TSonicD.str" "%WORKING_DIRECTORY%\ROM\GameCode\"
goto :eof

:SonicHeroesEvents
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
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents\")

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
goto :eof

:SetupCharacters
cd %WORKING_DIRECTORY%\ROM\
ren playmodel "CharacterModels"
cd %WORKING_DIRECTORY%\ROM\CharacterModels

echo "-- Create Character Folders"
call :CreateCharacterFolders
echo "-- Move Each Character To Folder"
call :MoveEachCharacterToFolder
echo "-- Sort Out Each Character"

rem For Each Directory Do Sort Out Animations Models And Textures
for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		set filename=%%~nf
		if /I !filename:~-4!==_anm (mkdir "%%~pfAnimations" & move "%%f" "%%~pfAnimations\")
	)
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		set filename=%%~nf
		if /I !filename:~-4!==_dff (mkdir "%%~pfModels" & move "%%f" "%%~pfModels\")
	)
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		set filename=%%~nxf
		if /I !filename:~-4!==.txd (mkdir "%%~pfTextures" & move "%%f" "%%~pfTextures\")
	)
)
goto :eof

:CreateCharacterFolders
mkdir "Amy"
mkdir "Charmy"
mkdir "Cheese"
mkdir "Big"
mkdir "Cream"
mkdir "Espio"
mkdir "Knuckles"
mkdir "Omega"
mkdir "Rouge"
mkdir "Shadow"
mkdir "Sonic"
mkdir "Tails"
mkdir "Vector"

mkdir "SuperSonic"
mkdir "SuperKnuckles"
mkdir "SuperTails"

mkdir "MetalAmy"
mkdir "MetalCharmy"
mkdir "MetalCheese"
mkdir "MetalBig"
mkdir "MetalCream"
mkdir "MetalEspio"
mkdir "MetalKnuckles"
mkdir "MetalOmega"
mkdir "MetalRouge"
mkdir "MetalShadow"
mkdir "MetalSonic"
mkdir "MetalTails"
mkdir "MetalVector"
goto :eof

:MoveEachCharacterToFolder
for %%f in ("am*") do (move /Y %%f "Amy")
for %%f in ("be*") do (move /Y %%f "Charmy")
for %%f in ("bi*") do (move /Y %%f "Big")
for %%f in ("cheese*") do (move /Y %%f "Cheese")
for %%f in ("cr*") do (move /Y %%f "Cream")
for %%f in ("es*") do (move /Y %%f "Espio")
for %%f in ("fam*") do (move /Y %%f "MetalAmy")
for %%f in ("fbe*") do (move /Y %%f "MetalCharmy")
for %%f in ("fbi*") do (move /Y %%f "MetalBig")
for %%f in ("fcheese*") do (move /Y %%f "MetalCheese")
for %%f in ("fcr*") do (move /Y %%f "MetalCream")
for %%f in ("fes*") do (move /Y %%f "MetalEspio")
for %%f in ("fkn*") do (move /Y %%f "MetalKnuckles")
for %%f in ("fom*") do (move /Y %%f "MetalOmega")
for %%f in ("fro*") do (move /Y %%f "MetalRouge")
for %%f in ("fsh*") do (move /Y %%f "MetalShadow")
for %%f in ("fso*") do (move /Y %%f "MetalSonic")
for %%f in ("fta*") do (move /Y %%f "MetalTails")
for %%f in ("fve*") do (move /Y %%f "MetalVector")
for %%f in ("kn*") do (move /Y %%f "Knuckles")
for %%f in ("om*") do (move /Y %%f "Omega")
for %%f in ("ro*") do (move /Y %%f "Rouge")
for %%f in ("sh*") do (move /Y %%f "Shadow")
for %%f in ("sk*") do (move /Y %%f "SuperKnuckles")
for %%f in ("sh*") do (move /Y %%f "Shadow")
for %%f in ("so*") do (move /Y %%f "Sonic")
for %%f in ("ss*") do (move /Y %%f "SuperSonic")
for %%f in ("st*") do (move /Y %%f "SuperTails")
for %%f in ("ta*") do (move /Y %%f "Tails")
for %%f in ("ve*") do (move /Y %%f "Vector")
goto :eof

:SetupLevelsDirectories
mkdir "%WORKING_DIRECTORY%\ROM\Levels\ActionStages\"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\Other\SETIDTable"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\Other\CommonParticleData"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\Other\"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
mkdir "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
cd "%WORKING_DIRECTORY%\ROM\Levels\ActionStages"
call :CreateLevelFolders
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	mkdir "%%d\Geometry"
	mkdir "%%d\CustomFalcoEnemy"
	mkdir "%%d\StageSpecificObjects\GeometryAndEnvironmentModels"
	mkdir "%%d\TitleCardMissionText"
	mkdir "%%d\TitleCard"
	mkdir "%%d\TitleCard\ExtraMission"
	mkdir "%%d\TitleCard\SuperHard"
	mkdir "%%d\Collision"
	mkdir "%%d\Collision\WaterCollision"
	mkdir "%%d\Collision\DeathPlanes"
	mkdir "%%d\ObjectLayouts"
	mkdir "%%d\ObjectLayouts\AllTeams"
	mkdir "%%d\ObjectLayouts\TeamSonic"
	mkdir "%%d\ObjectLayouts\TeamDark"
	mkdir "%%d\ObjectLayouts\TeamRose"
	mkdir "%%d\ObjectLayouts\TeamChaotix"
	mkdir "%%d\ObjectLayouts\SuperHard"
	mkdir "%%d\ObjectLayouts\DecorationLayouts"	
	mkdir "%%d\IdleAutoplayDemos"
	mkdir "%%d\LightingData"
	mkdir "%%d\CameraData"
	mkdir "%%d\GeometryVisibilityData"
	mkdir "%%d\Textures"
	mkdir "%%d\IndirectionalData"
	mkdir "%%d\ParticleData"
	mkdir "%%d\ExtraSplineData"
	mkdir "%%d\Unknown"
)
goto :eof

:MoveLevelsAssets
for %%f in ("%WORKING_DIRECTORY%\ROM\s**obj.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**MRG.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_flyer.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\stg**.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\collisions\*.cl") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_PB.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P1.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P2.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P3.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P4.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P5.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_DB.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\mission\*.bmp") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.dmo") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_light.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_cam.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_blk.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_indinfo.dat") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_ptcl.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_ptclplay.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.txc") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.spl") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
for %%f in ("%WORKING_DIRECTORY%\ROM\Textures\s**.txd") do (
	set filename=%%~nf
	set test1=!filename:~-0,3!
	if /I not !test1!==stg (set test2=!filename:~0,8!) else (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
	if /I not !test2!==startbtn (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages")
)
goto :eof

:SortOutCommonObjects
REM COMMON OBJS
REM Fixes for object filter inaccuracies, and move the common objects.
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\stg06_kw_hanabi_**.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects")
move /Y "%WORKING_DIRECTORY%\ROM\Levels\ActionStages\stgmem0910.one" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"

REM Manually move Common Objects Across to the correct directories.
for %%f in ("%WORKING_DIRECTORY%\ROM\obj*_Prop*.dff") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects")
for %%f in ("%WORKING_DIRECTORY%\ROM\bob*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects")
move /Y "%WORKING_DIRECTORY%\ROM\rain_ita.dff" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\obj0708_sparks.dff" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\primModels.one" "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\indirectEditor.dff" "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\null.dff" "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
move /Y "%WORKING_DIRECTORY%\ROM\comobj.one" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"

mkdir  "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Model\"
mkdir  "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Animation\"
move /Y "%WORKING_DIRECTORY%\ROM\ef_bomb.dff" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Model\"
move /Y "%WORKING_DIRECTORY%\ROM\ef_bomb.anm" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Animation\"

cd "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
call :RenameCommonObjects
goto :eof

:RenameCommonObjects
mkdir "Bobsled"
mkdir "French"
mkdir "German"
mkdir "Italian"
mkdir "Spanish"

move /Y "French" "Bobsled\"
move /Y "German" "Bobsled\"
move /Y "Italian" "Bobsled\"
move /Y "Spanish" "Bobsled\"

move /Y bob.one "Bobsled\"
move /Y bobF.one "Bobsled\French"
move /Y bobG.one "Bobsled\German"
move /Y bobI.one "Bobsled\Italian"
move /Y bobS.one "Bobsled\Spanish"

mkdir "Common SET Placeable Objects"
move /Y "comobj.one" "Common SET Placeable Objects\"

mkdir "Stage 09~10 - Frog Forest~Lost Jungle - Level Specific Object Model Copies"
move /Y "stgmem0910.one" "Stage 09~10 - Frog Forest~Lost Jungle - Level Specific Object Model Copies"

mkdir "Stage 06 - BINGO Highway - Lighting Strip Decoration Models"
for %%f in (stg06_kw*) do (move "%%f" "Stage 06 - BINGO Highway - Lighting Strip Decoration Models\")

mkdir "Stage 09 - Frog Forest - Custom Propeller Model"
move /Y obj09* "Stage 09 - Frog Forest - Custom Propeller Model\"

mkdir "Stage 10 - Lost Jungle - Custom Propeller Model"
move /Y obj10* "Stage 10 - Lost Jungle - Custom Propeller Model\"

mkdir "Stage 73 - Frog Forest 2P - Custom Propeller Model"
move /Y obj73* "Stage 73 - Frog Forest 2P - Custom Propeller Model\"

mkdir "Stage 07~08 - Rail Canyon~Bullet Station - Sparks Model"
move /Y obj0708_sparks.dff "Stage 07~08 - Rail Canyon~Bullet Station - Sparks Model\"

mkdir "Generic Propeller Model"
move /Y obj_prop.dff "Generic Propeller Model\"

mkdir "Generic Rain Model"
move /Y rain_ita.dff "Generic Rain Model\"
goto :eof

:MoveGenericStageTitles
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\stgtitle_disp*.one") do ( 
	set FileName=%%~nf
	set GenericStageTitleID=!FileName:~13,3!
	if /I !GenericStageTitleID!==EEX (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\English\ExtraMission\")
	if /I !GenericStageTitleID!==ESH (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\English\SuperHard\")
	if /I !GenericStageTitleID!==E (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\English\")
		
	if /I !GenericStageTitleID!==FSH (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\French\ExtraMission\")
	if /I !GenericStageTitleID!==FEX (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\French\SuperHard\")
	if /I !GenericStageTitleID!==F (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\French\")
			
	if /I !GenericStageTitleID!==GSH (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\German\ExtraMission\")
	if /I !GenericStageTitleID!==GEX (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\German\SuperHard\")
	if /I !GenericStageTitleID!==G (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\German\")
		
	if /I !GenericStageTitleID!==ISH (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\Italian\ExtraMission\")
	if /I !GenericStageTitleID!==IEX (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\Italian\SuperHard\")
	if /I !GenericStageTitleID!==I (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\Italian\")
		
	if /I !GenericStageTitleID!==SSH (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\Spanish\ExtraMission\")
	if /I !GenericStageTitleID!==SEX (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\Spanish\SuperHard\")
	if /I !GenericStageTitleID!==S (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\GenericStageTitles\Spanish\")
)
goto :eof

:MoveCharacterStageTitles
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\stg*title_disp*.one") do ( 
	set FileName=%%~nf
	
	set TeamChaotixTest=!FileName:~-0,15!
	set EnglishTeamTitleTest=!FileName:~-1!
	set Team4CharTest=!FileName:~-0,12!
	set Team5CharTest=!FileName:~-0,13!

	REM TEST FOR TEAMS
		
	if /I !TeamChaotixTest!==stgCHAOTIXtitle (if /I !EnglishTeamTitleTest!==E (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\TeamBattleTitles\Chaotix\English\") else (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\TeamBattleTitles\Chaotix\"))
		
	if /I !Team4CharTest!==stgDARKtitle (if /I !EnglishTeamTitleTest!==E (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\TeamBattleTitles\Dark\English") else (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\TeamBattleTitles\Dark\"))
		
	if /I !Team4CharTest!==stgROSEtitle (if /I !EnglishTeamTitleTest!==E (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\TeamBattleTitles\Rose\English") else (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\TeamBattleTitles\Rose\"))
		
	if /I !Team5CharTest!==stgSONICtitle (if /I !EnglishTeamTitleTest!==E (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\TeamBattleTitles\Sonic\English") else (move /Y "%%~pnxf" "%%~pf\Stage XX - Common Assets\TeamBattleTitles\Sonic\"))
)
goto :eof

:MoveActionStageFilesToStageDir-DAT
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.dat") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory DAT: !directory!"
	)
)
goto :eof

:MoveActionStageFilesToStageDir-DMO
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.dmo") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory DMO: !directory!"
	)
)
goto :eof

:MoveActionStageFilesToStageDir-TXC
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.txc") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory TXC: !directory!"
	)
)
goto :eof

:MoveActionStageFilesToStageDir-SPL
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.spl") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory SPL: !directory!"
	)
)
goto :eof

:MoveActionStageFilesToStageDir-TXD
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.txd") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory TXD: !directory!"
	)
)
goto :eof

:MoveActionStageFilesToStageDir-ONE
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.one") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory ONE: !directory!"
	)
)
goto :eof

:MoveActionStageFilesToStageDir-CL
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.cl") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory Collision: !directory!"
	)
)
goto :eof

:MoveActionStageFilesToStageDir-BIN
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.bin") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory BIN: !directory!"
	)
)
goto :eof

:MoveActionStageFilesToStageDir-BMP
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	set StageDirectory=%%~nd
	for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.bmp") do ( 
		set FileName=%%~nf
		set directory=%%~nxd
		set stagedirshort=!StageDirectory:~6,2!
		set test1=!FileName:~-0,3!
		
		REM TEST FOR DIRECTORY
		if /I not !test1!==stg (set test2=!FileName:~1,2!) else (set test2=!FileName:~3,2!)
		if /I !test2!==!stagedirshort! (move /Y "%%~pnxf" "%%~pf!StageDirectory!\")
		
		echo "Current Directory BMP: !directory!"
	)
)
goto :eof

pause