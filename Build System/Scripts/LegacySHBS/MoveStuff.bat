@echo off
set WORKING_DIRECTORY=%cd%
rem FIX FOR VARS INSIDE LOOPS
setlocal enabledelayedexpansion

REM ############################################### USER CHOICE 01 BEGIN: DEFAULT CONVERSION

rem #00 Convert Media
echo "[00/21] Convert Media"
call :SortOutAFS
call :SortOutADX
call :DoMusic
call :DoVideo

rem #01 REARRANGE ADVERTISE
echo "[01/21] REARRANGE ADVERTISE"
call :ReArrangeAdvertise

rem #02 ADD OTHER ITEMS TO MAINMENU
echo "[02/21] ADD OTHER ITEMS TO MAINMENU"
call :OtherMenuItems

rem #03 MOVE MAIN MENU CHARS!
echo "[03/21] MOVE MAIN MENU CHARS"
call :OtherMenuChars
call :GenerateMenuCharacters

rem #04 DECOMPRESS PRS FONTS
echo "[04/21] DECOMPRESS PRS FONTS"
call :DecompressPrsFonts

rem #05 MOVE UNUSED MENU FILES
echo "[05/21] MOVE UNUSED MENU FILES"
call :MoveUnusedMenuFiles

rem #06 GROUP MAIN MENU ASSETS
echo "[06/21] GROUP MAIN MENU ASSETS"
call :GroupMainMenuAssets
call :HardcodeGroupAssetsBatch

rem #07 LANGUAGE SPECIFIC ASSETS
echo "[07/21] LANGUAGE SPECIFIC ASSETS"
call :RenameLanguageSpecificMenuAssets

rem #08 MOVE MAIN MENU TEXT
echo "[08/21] MOVE MAIN MENU TEXT"
call :MoveMainMenuText

rem #09 MOVE GAMECUBE GAME CODE
echo "[09/21] MOVE GAMECUBE GAME CODE"
call :GameCubeGameCode

rem #10 SET UP INGAME EVENTS 
echo "[10/21] EXTRACT AND REARRANGE INGAME EVENTS"
call :SonicHeroesEvents

rem #11 SET UP CHARACTER MODELS
echo "[11/21] SET UP CHARACTER MODELS"
call :SetupCharacters

rem #12 SET UP LEVELS
echo "[12/21] SET UP LEVELS"
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
call :LevelAssetsToCategory
call :CategoriseTitleCards
call :CleanupAndMoveRemaining

rem #13 SET UP TEXTURES
echo "[13/21] SET UP TEXTURES"
call :MoveTextures

rem #14 SET UP ENEMIES
echo "[14/21] SET UP ENEMIES"
call :MoveEnemies

rem #15 SET UP SFX
echo "[15/21] SET UP SOUND EFFECTS"
call :MoveSFX

rem #16 SET UP HUD
echo "[16/21] SET UP HUD"
call :MoveHUD

rem #17 ROOTDIR CLEANUP
echo "[17/21] ROOTDIR CLEANUP"
call :RootDirCleanup

rem #18 DO THE FONTS
echo "[18/21] SORT OUT FONTS"
call :SortOutFonts

rem #19 Prototype Specific File Shifting.
echo "[19/21] File Shifting Potential Prototype Files"
call :PrototypeMove

rem #19 HeroesONE HARAMBE EDITION
echo "[20/21] Extracting .one with HeroesONE"
rem I'll probably investigate in the future why this happens, till then.
call :ProtectBrokenONE
call :DecompressONE
rem Restore.
call :RestoreBrokenONE

rem #20 DO CLEANUP
echo "[21/21] DO CLEANUP"
call :DoCleanup

echo "DONE" && pause

REM ############################################### USER CHOICE 01 END: DEFAULT CONVERSION

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
echo "Moving Advertise Folder ==><=="
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\English" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\English"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\French" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\French"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\German" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\German"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Italian" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Italian"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Japanese" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Japanese"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Korean" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Korean"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Spanish" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Spanish"
move /Y %WORKING_DIRECTORY%\ROM\advertise\* "%WORKING_DIRECTORY%\ROM\GameMenus"
move /Y %WORKING_DIRECTORY%\ROM\advertise\E\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\English"
move /Y %WORKING_DIRECTORY%\ROM\advertise\F\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\French"
move /Y %WORKING_DIRECTORY%\ROM\advertise\G\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\German"
move /Y %WORKING_DIRECTORY%\ROM\advertise\I\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Italian"
move /Y %WORKING_DIRECTORY%\ROM\advertise\J\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Japanese"
move /Y %WORKING_DIRECTORY%\ROM\advertise\K\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Korean"
move /Y %WORKING_DIRECTORY%\ROM\advertise\S\* "%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\Spanish"
pause
goto :eof

:OtherMenuItems
echo "Moving Other Main Menu Items ==><=="
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels"
move /Y %WORKING_DIRECTORY%\ROM\adv_ef_warpa.dff "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels"
move /Y %WORKING_DIRECTORY%\ROM\adv_ef_warpb.dff "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels"
move /Y %WORKING_DIRECTORY%\ROM\adv_sonicoutline.dff "%WORKING_DIRECTORY%\ROM\GameMenus\TransitionModels"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\French" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\French"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\German" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\German"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Italian" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Italian"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Spanish" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Spanish"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\English&Japanese" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\English&Japanese"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\English&Japanese\"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_fr.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\French\"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_ge.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\German\"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_it.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Italian\"
move /Y %WORKING_DIRECTORY%\ROM\adv_timeup_sp.dff "%WORKING_DIRECTORY%\ROM\GameMenus\SpecialStageTimeUpModels\Spanish\"
goto :eof

:OtherMenuChars
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\MainMenuCharacters" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\MainMenuCharacters"
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\adv_pl*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\MainMenuCharacters\" && echo "Moving Main Menu Characters ==> %%~nxf")
cd "%WORKING_DIRECTORY%\ROM\GameMenus\MainMenuCharacters\"
goto :eof

:DecompressPrsFonts
cd %WORKING_DIRECTORY%
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\Fonts" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Fonts"
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\*.prs") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\Fonts" && echo "Moving Fonts ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\Fonts\*.prs") do (%WORKING_DIRECTORY%\Tools\PrsDec\PrsDec.exe %%f %%~pnf.bin && echo "Decompressing Fonts ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\Fonts\*.prs") do (DEL /F %%f)
goto :eof

:GenerateMenuCharacters
if not exist "Amy" mkdir "Amy"
if not exist "CharmyBee" mkdir "CharmyBee"
if not exist "BigTheCat" mkdir "BigTheCat"
if not exist "Cream" mkdir "Cream"
if not exist "Espio" mkdir "Espio"
if not exist "Knuckles" mkdir "Knuckles"
if not exist "Omega" mkdir "Omega"
if not exist "Rouge" mkdir "Rouge"
if not exist "Shadow" mkdir "Shadow"
if not exist "Sonic" mkdir "Sonic"
if not exist "Tails" mkdir "Tails"
if not exist "Vector" mkdir "Vector"
if not exist "AllPlayers" mkdir "AllPlayers"
if not exist "Unused~Unidentified" mkdir "Unused~Unidentified"
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
for %%f in (adv_pl*.one) do (move /Y %%f "Unused~Unidentified\" && echo "Moving Unused/Unidentified Menu Players ==> %%~nxf")
goto :eof

:MoveUnusedMenuFiles
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\Unused" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused"
cd "%WORKING_DIRECTORY%\ROM\GameMenus\Unused"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyE3-10.8Title Screen" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyE3-10.8Title Screen"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyPre10.8ProtoAudioMenu" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyPre10.8ProtoAudioMenu"
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\E3BuildMainMenuAssets" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\E3BuildMainMenuAssets"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_title.one" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyE3-10.8Title Screen"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_audio.one" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\EarlyPre10.8ProtoAudioMenu"
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\adv_e3rom.one" "%WORKING_DIRECTORY%\ROM\GameMenus\Unused\E3BuildMainMenuAssets"
goto :eof

:GroupMainMenuAssets
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\Assets" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\Assets"
for %%f in ("%WORKING_DIRECTORY%\ROM\GameMenus\adv_*") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\Assets" && echo "Moving Main Menu Assets ==> %%~nxf")
move /Y "%WORKING_DIRECTORY%\ROM\GameMenus\as_emblem.one" "%WORKING_DIRECTORY%\ROM\GameMenus\Assets"
cd %WORKING_DIRECTORY%\ROM\GameMenus\Assets
goto :eof

:HardcodeGroupAssetsBatch
if not exist "MainMenuBackgroundAssets" mkdir "MainMenuBackgroundAssets"
if not exist "MainMenuOmochao" mkdir "MainMenuOmochao"
if not exist "MainMenuOmochaoHelpIcon" mkdir "MainMenuOmochaoHelpIcon"
if not exist "AutosaveMenu" mkdir "AutosaveMenu"
if not exist "CreditsScreenLogos" mkdir "CreditsScreenLogos"
if not exist "MainMenuWindowTextures" mkdir "MainMenuWindowTextures"
if not exist "EmblemCountSpinningEmblem" mkdir "EmblemCountSpinningEmblem"

move /Y adv_bg.one "MainMenuBackgroundAssets\"
move /Y adv_chao.one "MainMenuOmochao\"
move /Y adv_help.one "MainMenuOmochaoHelpIcon\"
move /Y adv_save.one "AutosaveMenu\"
move /Y adv_staffroll.one "CreditsScreenLogos\"
move /Y adv_window.one "MainMenuWindowTextures\"
move /Y as_emblem.one "EmblemCountSpinningEmblem\"
goto :eof

:RenameLanguageSpecificMenuAssets
echo "Generating Language Specific Menu Asset Directories ==><=="
for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameMenus\LocalizedMenuAssets\*") do (cd %%d & call :RenameLocaleFiles)
goto :eof

:RenameLocaleFiles
if not exist "2PlayerMenuAssets" mkdir "2PlayerMenuAssets"
if not exist "AudioMenuAssets" mkdir "AudioMenuAssets"
if not exist "BottomMenuControlsBarAssets" mkdir "BottomMenuControlsBarAssets"
if not exist "CGCutsceneMenuAssets" mkdir "CGCutsceneMenuAssets"
if not exist "CGCutsceneMenuAssetsUSAVersion" mkdir "CGCutsceneMenuAssetsUSAVersion"
if not exist "ChallengeMenuAssets" mkdir "ChallengeMenuAssets"
if not exist "CongratulationsScreenAssets" mkdir "CongratulationsScreenAssets"
if not exist "DeflickerMenuAssets" mkdir "DeflickerMenuAssets"
if not exist "TryAnotherStoryPopup" mkdir "TryAnotherStoryPopup"
if not exist "Get7ChaosEmeraldsPopup" mkdir "Get7ChaosEmeraldsPopup"
if not exist "DefeatLastEvilPopup" mkdir "DefeatLastEvilPopup"
if not exist "GetAllARankPopup" mkdir "GetAllARankPopup"
if not exist "TrySuperHardModePopup" mkdir "TrySuperHardModePopup"
if not exist "ThankYouForPlayingPopup" mkdir "ThankYouForPlayingPopup"
if not exist "SaveFileSelectionScreenAssets" mkdir "SaveFileSelectionScreenAssets"
if not exist "BrightnessAdjustmentSelectionScreenAssets" mkdir "BrightnessAdjustmentSelectionScreenAssets"
if not exist "MainMenuAssets" mkdir "MainMenuAssets"
if not exist "OptionsMenuAssets" mkdir "OptionsMenuAssets"
if not exist "PALFramerateSelectionScreenAssets" mkdir "PALFramerateSelectionScreenAssets"
if not exist "ProgressiveScanOptionSelectionScreenAssets" mkdir "ProgressiveScanOptionSelectionScreenAssets"
if not exist "StoryModeMenuAssets" mkdir "StoryModeMenuAssets"
if not exist "TitleScreenAssets" mkdir "TitleScreenAssets"
if not exist "TitleScreenAssetsUSAVersion" mkdir "TitleScreenAssetsUSAVersion"

move /Y adv_2p.one "2PlayerMenuAssets\"
move /Y adv_audio.one "AudioMenuAssets\"
move /Y adv_bar.one "BottomMenuControlsBarAssets\"
move /Y adv_cg.one "CGCutsceneMenuAssets\"
move /Y adv_cg_us.one "CGCutsceneMenuAssetsUSAVersion\"
move /Y adv_challenge.one "ChallengeMenuAssets\"
move /Y adv_cong.one "CongratulationsScreenAssets\"
move /Y adv_deflicker.one "DeflickerMenuAssets\"
move /Y adv_ending_a.one "TryAnotherStoryPopup\"
move /Y adv_ending_b.one "Get7ChaosEmeraldsPopup\"
move /Y adv_ending_c.one "DefeatLastEvilPopup\"
move /Y adv_ending_d.one "GetAllARankPopup\"
move /Y adv_ending_e.one "TrySuperHardModePopup\"
move /Y adv_ending_f.one "ThankYouForPlayingPopup\"
move /Y adv_fileselect.one "SaveFileSelectionScreenAssets\"
move /Y adv_ganma.one "BrightnessAdjustmentSelectionScreenAssets\"
move /Y adv_mainmenu.one "MainMenuAssets\"
move /Y adv_option.one "OptionsMenuAssets\"
move /Y adv_pal.one "PALFramerateSelectionScreenAssets\"
move /Y adv_progressive.one "ProgressiveScanOptionSelectionScreenAssets\"
move /Y adv_story.one "StoryModeMenuAssets\"
move /Y adv_title.one "TitleScreenAssets\"
move /Y adv_title_us.one "TitleScreenAssetsUSAVersion\"
goto :eof

:MoveMainMenuText
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\GameMenuText" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\GameMenuText"
for %%f in ("%WORKING_DIRECTORY%\ROM\Text\*.utx") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\GameMenus\GameMenuText" && echo "Moving Main Menu Text ==> %%~nxf")
if not exist "%WORKING_DIRECTORY%\ROM\GameMenus\CreditsScreenText" mkdir "%WORKING_DIRECTORY%\ROM\GameMenus\CreditsScreenText"
move /Y "%WORKING_DIRECTORY%\ROM\Text\staffroll.csv" "%WORKING_DIRECTORY%\ROM\GameMenus\CreditsScreenText"
goto :eof

:CreateLevelFolders
if not exist "Stage 01 - Seaside Hill" mkdir "Stage 01 - Seaside Hill"
if not exist "Stage 02 - Ocean Palace" mkdir "Stage 02 - Ocean Palace"
if not exist "Stage 03 - Grand Metropolis" mkdir "Stage 03 - Grand Metropolis"
if not exist "Stage 04 - Power Plant" mkdir "Stage 04 - Power Plant"
if not exist "Stage 11 - Hang Castle" mkdir "Stage 11 - Hang Castle"
if not exist "Stage 12 - Mystic Mansion" mkdir "Stage 12 - Mystic Mansion"
if not exist "Stage 13 - Egg Fleet" mkdir "Stage 13 - Egg Fleet"
if not exist "Stage 14 - Final Fortress" mkdir "Stage 14 - Final Fortress"
if not exist "Stage 20 - Egg Hawk" mkdir "Stage 20 - Egg Hawk"
if not exist "Stage 21 - Team Battle 1" mkdir "Stage 21 - Team Battle 1"
if not exist "Stage 22 - Robot Carnival" mkdir "Stage 22 - Robot Carnival"
if not exist "Stage 23 - Egg Albatross" mkdir "Stage 23 - Egg Albatross"
if not exist "Stage 25 - Robot Storm" mkdir "Stage 25 - Robot Storm"
if not exist "Stage 27 - Metal Madness" mkdir "Stage 27 - Metal Madness"
if not exist "Stage 28 - Metal Overlord" mkdir "Stage 28 - Metal Overlord"
if not exist "Stage 29 - Sea Gate" mkdir "Stage 29 - Sea Gate"
if not exist "Stage 31 - Seaside Course" mkdir "Stage 31 - Seaside Course"
if not exist "Stage 32 - City Course" mkdir "Stage 32 - City Course"
if not exist "Stage 33 - Casino Course" mkdir "Stage 33 - Casino Course"
if not exist "Stage 60 - Seaside Hill 2P" mkdir "Stage 60 - Seaside Hill 2P"
if not exist "Stage 61 - Grand Metropolis 2P" mkdir "Stage 61 - Grand Metropolis 2P"
if not exist "Stage 63 - City Top 2P" mkdir "Stage 63 - City Top 2P"
if not exist "Stage 65 - Turtle Shell 2P" mkdir "Stage 65 - Turtle Shell 2P"
if not exist "Stage 66 - Egg Treat 2P" mkdir "Stage 66 - Egg Treat 2P"
if not exist "Stage 68 - Hot Elevator 2P" mkdir "Stage 68 - Hot Elevator 2P"
if not exist "Stage 69 - Road Rock 2P" mkdir "Stage 69 - Road Rock 2P"
if not exist "Stage 71 - Terror Hall 2P" mkdir "Stage 71 - Terror Hall 2P"
if not exist "Stage 74 - Egg Fleet 2P" mkdir "Stage 74 - Egg Fleet 2P"

if not exist "Stage 00 - Test Level" mkdir "Stage 00 - Test Level"
if not exist "Stage 05 - Casino Park" mkdir "Stage 05 - Casino Park"
if not exist "Stage 06 - BINGO Highway" mkdir "Stage 06 - BINGO Highway"
if not exist "Stage 07 - Rail Canyon" mkdir "Stage 07 - Rail Canyon"
if not exist "Stage 08 - Bullet Station" mkdir "Stage 08 - Bullet Station"
if not exist "Stage 09 - Frog Forest" mkdir "Stage 09 - Frog Forest"
if not exist "Stage 10 - Lost Jungle" mkdir "Stage 10 - Lost Jungle"
if not exist "Stage 24 - Team Battle 2" mkdir "Stage 24 - Team Battle 2"
if not exist "Stage 26 - Egg Emperor" mkdir "Stage 26 - Egg Emperor"
if not exist "Stage 40 - Bonus Stage 2" mkdir "Stage 40 - Bonus Stage 2"
if not exist "Stage 41 - Bonus Stage 1" mkdir "Stage 41 - Bonus Stage 1"
if not exist "Stage 42 - Bonus Stage 3" mkdir "Stage 42 - Bonus Stage 3"
if not exist "Stage 43 - Bonus Stage 4" mkdir "Stage 43 - Bonus Stage 4"
if not exist "Stage 44 - Bonus Stage 5" mkdir "Stage 44 - Bonus Stage 5"
if not exist "Stage 45 - Bonus Stage 6" mkdir "Stage 45 - Bonus Stage 6"
if not exist "Stage 46 - Bonus Stage 7" mkdir "Stage 46 - Bonus Stage 7"
if not exist "Stage 50 - Team Chaotix Rail Canyon" mkdir "Stage 50 - Team Chaotix Rail Canyon"
if not exist "Stage 62 - BINGO Highway 2P" mkdir "Stage 62 - BINGO Highway 2P"
if not exist "Stage 64 - Casino Ring 2P" mkdir "Stage 64 - Casino Ring 2P"
if not exist "Stage 67 - Pinball Match 2P" mkdir "Stage 67 - Pinball Match 2P"
if not exist "Stage 70 - Mad Express 2P" mkdir "Stage 70 - Mad Express 2P"
if not exist "Stage 72 - Rail Canyon 2P" mkdir "Stage 72 - Rail Canyon 2P"
if not exist "Stage 73 - Frog Forest 2P" mkdir "Stage 73 - Frog Forest 2P"
if not exist "Stage 80 - Emerald Challenge 2" mkdir "Stage 80 - Emerald Challenge 2"
if not exist "Stage 81 - Emerald Challenge 1" mkdir "Stage 81 - Emerald Challenge 1"
if not exist "Stage 82 - Emerald Challenge 3" mkdir "Stage 82 - Emerald Challenge 3"
if not exist "Stage 83 - Emerald Challenge 4" mkdir "Stage 83 - Emerald Challenge 4"
if not exist "Stage 84 - Emerald Challenge 5" mkdir "Stage 84 - Emerald Challenge 5"
if not exist "Stage 85 - Emerald Challenge 6" mkdir "Stage 85 - Emerald Challenge 6"
if not exist "Stage 86 - Emerald Challenge 7" mkdir "Stage 86 - Emerald Challenge 7"
if not exist "Stage 87 - Special Stage 1 2P" mkdir "Stage 87 - Special Stage 1 2P"
if not exist "Stage 88 - Special Stage 2 2P" mkdir "Stage 88 - Special Stage 2 2P"
if not exist "Stage 89 - Special Stage 3 2P" mkdir "Stage 89 - Special Stage 3 2P"

if not exist "Stage 15 - Unused" mkdir "Stage 15 - Unused"
if not exist "Stage 16 - Unused" mkdir "Stage 16 - Unused"
if not exist "Stage 17 - Unused" mkdir "Stage 17 - Unused"
if not exist "Stage 18 - Unused" mkdir "Stage 18 - Unused"
if not exist "Stage 19 - Unused" mkdir "Stage 19 - Unused"
if not exist "Stage 30 - Unused" mkdir "Stage 30 - Unused"
if not exist "Stage 34 - Unused" mkdir "Stage 34 - Unused"
if not exist "Stage 35 - Unused" mkdir "Stage 35 - Unused"
if not exist "Stage 36 - Unused" mkdir "Stage 36 - Unused"
if not exist "Stage 37 - Unused" mkdir "Stage 37 - Unused"
if not exist "Stage 38 - Unused" mkdir "Stage 38 - Unused"
if not exist "Stage 39 - Unused" mkdir "Stage 39 - Unused"
if not exist "Stage 47 - Unused" mkdir "Stage 47 - Unused"
if not exist "Stage 48 - Unused" mkdir "Stage 48 - Unused"
if not exist "Stage 49 - Unused" mkdir "Stage 49 - Unused"
if not exist "Stage 51 - Unused" mkdir "Stage 51 - Unused"
if not exist "Stage 52 - Unused" mkdir "Stage 52 - Unused"
if not exist "Stage 53 - Unused" mkdir "Stage 53 - Unused"
if not exist "Stage 54 - Unused" mkdir "Stage 54 - Unused"
if not exist "Stage 55 - Unused" mkdir "Stage 55 - Unused"
if not exist "Stage 56 - Unused" mkdir "Stage 56 - Unused"
if not exist "Stage 57 - Unused" mkdir "Stage 57 - Unused"
if not exist "Stage 58 - Unused" mkdir "Stage 58 - Unused"
if not exist "Stage 59 - Unused" mkdir "Stage 59 - Unused"
if not exist "Stage 75 - Unused" mkdir "Stage 75 - Unused"
if not exist "Stage 76 - Unused" mkdir "Stage 76 - Unused"
if not exist "Stage 77 - Unused" mkdir "Stage 77 - Unused"
if not exist "Stage 78 - Unused" mkdir "Stage 78 - Unused"
if not exist "Stage 79 - Unused" mkdir "Stage 79 - Unused"
if not exist "Stage 90 - Unused" mkdir "Stage 90 - Unused"
if not exist "Stage 91 - Unused" mkdir "Stage 91 - Unused"
if not exist "Stage 92 - Unused" mkdir "Stage 92 - Unused"
if not exist "Stage 93 - Unused" mkdir "Stage 93 - Unused"
if not exist "Stage 94 - Unused" mkdir "Stage 94 - Unused"
if not exist "Stage 95 - Unused" mkdir "Stage 95 - Unused"
if not exist "Stage 96 - Unused" mkdir "Stage 96 - Unused"
if not exist "Stage 97 - Unused" mkdir "Stage 97 - Unused"
if not exist "Stage 98 - Unused" mkdir "Stage 98 - Unused"
if not exist "Stage 99 - Unused" mkdir "Stage 99 - Unused"

if not exist "Stage XX - Common Assets\GenericStageTitles\English\ExtraMission" mkdir "Stage XX - Common Assets\GenericStageTitles\English\ExtraMission"
if not exist "Stage XX - Common Assets\GenericStageTitles\English\SuperHard" mkdir "Stage XX - Common Assets\GenericStageTitles\English\SuperHard"

if not exist "Stage XX - Common Assets\GenericStageTitles\French\SuperHard" mkdir "Stage XX - Common Assets\GenericStageTitles\French\SuperHard"
if not exist "Stage XX - Common Assets\GenericStageTitles\French\ExtraMission" mkdir "Stage XX - Common Assets\GenericStageTitles\French\ExtraMission"

if not exist "Stage XX - Common Assets\GenericStageTitles\German\ExtraMission" mkdir "Stage XX - Common Assets\GenericStageTitles\German\ExtraMission"
if not exist "Stage XX - Common Assets\GenericStageTitles\German\SuperHard" mkdir "Stage XX - Common Assets\GenericStageTitles\German\SuperHard"

if not exist "Stage XX - Common Assets\GenericStageTitles\Italian\ExtraMission" mkdir "Stage XX - Common Assets\GenericStageTitles\Italian\ExtraMission"
if not exist "Stage XX - Common Assets\GenericStageTitles\Italian\SuperHard" mkdir "Stage XX - Common Assets\GenericStageTitles\Italian\SuperHard"

if not exist "Stage XX - Common Assets\GenericStageTitles\Spanish\ExtraMission" mkdir "Stage XX - Common Assets\GenericStageTitles\Spanish\ExtraMission"
if not exist "Stage XX - Common Assets\GenericStageTitles\Spanish\SuperHard" mkdir "Stage XX - Common Assets\GenericStageTitles\Spanish\SuperHard"

if not exist "Stage XX - Common Assets\TeamBattleTitles\Chaotix\English" mkdir "Stage XX - Common Assets\TeamBattleTitles\Chaotix\English"
if not exist "Stage XX - Common Assets\TeamBattleTitles\Rose\English" mkdir "Stage XX - Common Assets\TeamBattleTitles\Rose\English"
if not exist "Stage XX - Common Assets\TeamBattleTitles\Dark\English" mkdir "Stage XX - Common Assets\TeamBattleTitles\Dark\English"
if not exist "Stage XX - Common Assets\TeamBattleTitles\Sonic\English" mkdir "Stage XX - Common Assets\TeamBattleTitles\Sonic\English"
goto :eof

:GameCubeGameCode
if not exist "%WORKING_DIRECTORY%\ROM\GameCode\StageRelocatableModuleFiles" mkdir "%WORKING_DIRECTORY%\ROM\GameCode\StageRelocatableModuleFiles"
cd "%WORKING_DIRECTORY%\ROM\GameCode\StageRelocatableModuleFiles"
call :CreateLevelFolders
cd %WORKING_DIRECTORY%\ROM\

for /D %%d in ("%WORKING_DIRECTORY%\ROM\GameCode\StageRelocatableModuleFiles\*") do (
	for %%f in (*.rel) do (
		set FileName=%%~nf
		set FolderName=%%~nd
		set First5Letters=!FileName:~0,5!
		set LevelIDRel=!FileName:~5,2!
		set LevelIDFolder=!FolderName:~6,2!
		echo "Moving GC Relocatable Modules ==> %%~nxf"
		if /I !First5Letters!==stage ( if /I !LevelIDRel!==!LevelIDFolder! ( move /Y %%f "%%d\" ) ) else ( move /Y %%f "%WORKING_DIRECTORY%\ROM\GameCode\" )
	)
)
move /Y "%WORKING_DIRECTORY%\ROM\&&systemdata\*.dol" "%WORKING_DIRECTORY%\ROM\GameCode\"
move /Y "%WORKING_DIRECTORY%\ROM\TSonic.str" "%WORKING_DIRECTORY%\ROM\GameCode\"
move /Y "%WORKING_DIRECTORY%\ROM\TSonicD.str" "%WORKING_DIRECTORY%\ROM\GameCode\"
goto :eof

:SonicHeroesEvents
if not exist "%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents" mkdir "%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents"
if not exist "%WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents" mkdir "%WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents"
if not exist "%WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents" mkdir "%WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents"
if not exist "%WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents" mkdir "%WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents"
if not exist "%WORKING_DIRECTORY%\ROM\Events\LastStoryEvents" mkdir "%WORKING_DIRECTORY%\ROM\Events\LastStoryEvents"
if not exist "%WORKING_DIRECTORY%\ROM\Events\Other~UnusedEvents" mkdir "%WORKING_DIRECTORY%\ROM\Events\Other~UnusedEvents"
for %%f in ("%WORKING_DIRECTORY%\ROM\event*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\" && echo "Moving Events ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event00*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents\" && echo "Moving Events ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event01*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamDarkEvents\" && echo "Moving Events ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event02*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamRoseEvents\" && echo "Moving Events ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event03*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamChaotixEvents\" && echo "Moving Events ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event04*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\LastStoryEvents\" && echo "Moving Events ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\Events\event*") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Events\TeamSonicEvents\" && echo "Moving Events ==> %%~nxf")

rem SONIC
rem Auto Gen Folders based on Suffix Discriminant
echo "Generating Folders based on Suffix Discriminant <><><>==> SONIC"
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
		echo "Moving Event File to Subdirectory ==> %%~nxf"
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem DARK
rem Auto Gen Folders based on Suffix Discriminant
echo "Generating Folders based on Suffix Discriminant <><><>==> DARK"
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
		echo "Moving Event File to Subdirectory ==> %%~nxf"
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem ROSE
rem Auto Gen Folders based on Suffix Discriminant
echo "Generating Folders based on Suffix Discriminant <><><>==> ROSE"
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
		echo "Moving Event File to Subdirectory ==> %%~nxf"
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem CHAOTIX
rem Auto Gen Folders based on Suffix Discriminant
echo "Generating Folders based on Suffix Discriminant <><><>==> CHAOTIX"
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
		echo "Moving Event File to Subdirectory ==> %%~nxf"
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem LAST
rem Auto Gen Folders based on Suffix Discriminant
echo "Generating Folders based on Suffix Discriminant <><><>==> LAST"
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
		echo "Moving Event File to Subdirectory ==> %%~nxf"
		if /I !test!==!directoryname! (move /Y %%f %%d\)
	)
)

rem UNUSED
rem Auto Gen Folders based on Suffix Discriminant
echo "Generating Folders based on Suffix Discriminant <><><>==> UNUSED"
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
		echo "Moving Event File to Subdirectory ==> %%~nxf"
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
echo "Sorting out Character Animations ==> :D"
rem For Each Directory Do Sort Out Animations Models And Textures
for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		set filename=%%~nf
		echo "Sorting out Character Animations ==> %%~nxf"
		if /I !filename:~-4!==_anm (mkdir "%%~pfAnimations" & move "%%f" "%%~pfAnimations\")
	)
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		set filename=%%~nf
		echo "Sorting out Character Animations ==> %%~nxf"
		if /I !filename:~-4!==_dff (mkdir "%%~pfModels" & move "%%f" "%%~pfModels\")
	)
)

for /D %%d in ("%WORKING_DIRECTORY%\ROM\CharacterModels\*") do (
	for %%f in ("%%d\*") do (
		set filename=%%~nxf
		echo "Sorting out Character Animations ==> %%~nxf"
		if /I !filename:~-4!==.txd (mkdir "%%~pfTextures" & move "%%f" "%%~pfTextures\")
	)
)
goto :eof

:CreateCharacterFolders
echo "Generating Character Folders ==><=="
if not exist "Amy" mkdir "Amy"
if not exist "Charmy" mkdir "Charmy"
if not exist "Cheese" mkdir "Cheese"
if not exist "Big" mkdir "Big"
if not exist "Cream" mkdir "Cream"
if not exist "Espio" mkdir "Espio"
if not exist "Knuckles" mkdir "Knuckles"
if not exist "Omega" mkdir "Omega"
if not exist "Rouge" mkdir "Rouge"
if not exist "Shadow" mkdir "Shadow"
if not exist "Sonic" mkdir "Sonic"
if not exist "Tails" mkdir "Tails"
if not exist "Vector" mkdir "Vector"

if not exist "SuperSonic" mkdir "SuperSonic"
if not exist "SuperKnuckles" mkdir "SuperKnuckles"
if not exist "SuperTails" mkdir "SuperTails"

if not exist "MetalAmy" mkdir "MetalAmy"
if not exist "MetalCharmy" mkdir "MetalCharmy"
if not exist "MetalCheese" mkdir "MetalCheese"
if not exist "MetalBig" mkdir "MetalBig"
if not exist "MetalCream" mkdir "MetalCream"
if not exist "MetalEspio" mkdir "MetalEspio"
if not exist "MetalKnuckles" mkdir "MetalKnuckles"
if not exist "MetalOmega" mkdir "MetalOmega"
if not exist "MetalRouge" mkdir "MetalRouge"
if not exist "MetalShadow" mkdir "MetalShadow"
if not exist "MetalSonic" mkdir "MetalSonic"
if not exist "MetalTails" mkdir "MetalTails"
if not exist "MetalVector" mkdir "MetalVector"
goto :eof

:MoveEachCharacterToFolder
for %%f in ("am*") do (move /Y %%f "Amy" && echo "Moving Characters ==> %%~nxf")
for %%f in ("be*") do (move /Y %%f "Charmy" && echo "Moving Characters ==> %%~nxf")
for %%f in ("bi*") do (move /Y %%f "Big" && echo "Moving Characters ==> %%~nxf")
for %%f in ("cheese*") do (move /Y %%f "Cheese" && echo "Moving Characters ==> %%~nxf")
for %%f in ("cr*") do (move /Y %%f "Cream" && echo "Moving Characters ==> %%~nxf")
for %%f in ("es*") do (move /Y %%f "Espio" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fam*") do (move /Y %%f "MetalAmy" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fbe*") do (move /Y %%f "MetalCharmy" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fbi*") do (move /Y %%f "MetalBig" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fcheese*") do (move /Y %%f "MetalCheese" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fcr*") do (move /Y %%f "MetalCream" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fes*") do (move /Y %%f "MetalEspio" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fkn*") do (move /Y %%f "MetalKnuckles" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fom*") do (move /Y %%f "MetalOmega" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fro*") do (move /Y %%f "MetalRouge" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fsh*") do (move /Y %%f "MetalShadow" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fso*") do (move /Y %%f "MetalSonic" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fta*") do (move /Y %%f "MetalTails" && echo "Moving Characters ==> %%~nxf")
for %%f in ("fve*") do (move /Y %%f "MetalVector" && echo "Moving Characters ==> %%~nxf")
for %%f in ("kn*") do (move /Y %%f "Knuckles" && echo "Moving Characters ==> %%~nxf")
for %%f in ("om*") do (move /Y %%f "Omega" && echo "Moving Characters ==> %%~nxf")
for %%f in ("ro*") do (move /Y %%f "Rouge" && echo "Moving Characters ==> %%~nxf")
for %%f in ("sh*") do (move /Y %%f "Shadow" && echo "Moving Characters ==> %%~nxf")
for %%f in ("sk*") do (move /Y %%f "SuperKnuckles" && echo "Moving Characters ==> %%~nxf")
for %%f in ("sh*") do (move /Y %%f "Shadow" && echo "Moving Characters ==> %%~nxf")
for %%f in ("so*") do (move /Y %%f "Sonic" && echo "Moving Characters ==> %%~nxf")
for %%f in ("ss*") do (move /Y %%f "SuperSonic" && echo "Moving Characters ==> %%~nxf")
for %%f in ("st*") do (move /Y %%f "SuperTails" && echo "Moving Characters ==> %%~nxf")
for %%f in ("ta*") do (move /Y %%f "Tails" && echo "Moving Characters ==> %%~nxf")
for %%f in ("ve*") do (move /Y %%f "Vector" && echo "Moving Characters ==> %%~nxf")
goto :eof

:SetupLevelsDirectories
if not exist "%WORKING_DIRECTORY%\ROM\Levels\ActionStages\" mkdir "%WORKING_DIRECTORY%\ROM\Levels\ActionStages\"
if not exist "%WORKING_DIRECTORY%\ROM\Levels\Other\SETIDTable" mkdir "%WORKING_DIRECTORY%\ROM\Levels\Other\SETIDTable"
if not exist "%WORKING_DIRECTORY%\ROM\Levels\Other\CommonParticleData" mkdir "%WORKING_DIRECTORY%\ROM\Levels\Other\CommonParticleData"
if not exist "%WORKING_DIRECTORY%\ROM\Levels\Other\" mkdir "%WORKING_DIRECTORY%\ROM\Levels\Other\"
if not exist "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects" mkdir "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
if not exist "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects" mkdir "%WORKING_DIRECTORY%\ROM\Levels\Unused\CommonObjects"
cd "%WORKING_DIRECTORY%\ROM\Levels\ActionStages"
call :CreateLevelFolders
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	if not exist "%%d\Geometry" mkdir "%%d\Geometry"
	if not exist "%%d\CustomFalcoEnemy" mkdir "%%d\CustomFalcoEnemy"
	if not exist "%%d\StageSpecificObjects\GeometryAndEnvironmentModels" mkdir "%%d\StageSpecificObjects\GeometryAndEnvironmentModels"
	if not exist "%%d\TitleCardMissionText" mkdir "%%d\TitleCardMissionText"
	if not exist "%%d\TitleCard" mkdir "%%d\TitleCard"
	if not exist "%%d\TitleCard\ExtraMission" mkdir "%%d\TitleCard\ExtraMission"
	if not exist "%%d\TitleCard\SuperHard" mkdir "%%d\TitleCard\SuperHard"
	if not exist "%%d\Collision" mkdir "%%d\Collision"
	if not exist "%%d\Collision\WaterCollision" mkdir "%%d\Collision\WaterCollision"
	if not exist "%%d\Collision\DeathPlanes" mkdir "%%d\Collision\DeathPlanes"
	if not exist "%%d\ObjectLayouts" mkdir "%%d\ObjectLayouts"
	if not exist "%%d\ObjectLayouts\AllTeams" mkdir "%%d\ObjectLayouts\AllTeams"
	if not exist "%%d\ObjectLayouts\TeamSonic" mkdir "%%d\ObjectLayouts\TeamSonic"
	if not exist "%%d\ObjectLayouts\TeamDark" mkdir "%%d\ObjectLayouts\TeamDark"
	if not exist "%%d\ObjectLayouts\TeamRose" mkdir "%%d\ObjectLayouts\TeamRose"
	if not exist "%%d\ObjectLayouts\TeamChaotix" mkdir "%%d\ObjectLayouts\TeamChaotix"
	if not exist "%%d\ObjectLayouts\SuperHard" mkdir "%%d\ObjectLayouts\SuperHard"
	if not exist "%%d\ObjectLayouts\DecorationLayouts"	 mkdir "%%d\ObjectLayouts\DecorationLayouts"	
	if not exist "%%d\IdleAutoplayDemos" mkdir "%%d\IdleAutoplayDemos"
	if not exist "%%d\LightingData" mkdir "%%d\LightingData"
	if not exist "%%d\CameraData" mkdir "%%d\CameraData"
	if not exist "%%d\GeometryVisibilityData" mkdir "%%d\GeometryVisibilityData"
	if not exist "%%d\Textures" mkdir "%%d\Textures"
	if not exist "%%d\IndirectionalData" mkdir "%%d\IndirectionalData"
	if not exist "%%d\ParticleData" mkdir "%%d\ParticleData"
	if not exist "%%d\ExtraSplineData" mkdir "%%d\ExtraSplineData"
	if not exist "%%d\Unknown" mkdir "%%d\Unknown"
)
goto :eof

:MoveLevelsAssets

for %%f in ("%WORKING_DIRECTORY%\ROM\s**obj.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**MRG.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_flyer.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\stg**.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\collisions\*.cl") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_PB.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P1.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P2.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P3.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P4.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_P5.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_DB.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\mission\*.bmp") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.dmo") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_light.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_cam.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_blk.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_indinfo.dat") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_ptcl.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**_ptclplay.bin") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.txc") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
for %%f in ("%WORKING_DIRECTORY%\ROM\s**.spl") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
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
echo "Moving Common Objects ==><=="
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

if not exist "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Model\" mkdir "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Model\"
if not exist "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Animation\" mkdir "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Animation\"
move /Y "%WORKING_DIRECTORY%\ROM\ef_bomb.dff" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Model\"
move /Y "%WORKING_DIRECTORY%\ROM\ef_bomb.anm" "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects\Bomb Effect\Animation\"

cd "%WORKING_DIRECTORY%\ROM\Levels\CommonObjects"
call :RenameCommonObjects
goto :eof

:RenameCommonObjects
if not exist "Bobsled" mkdir "Bobsled"
if not exist "French" mkdir "French"
if not exist "German" mkdir "German"
if not exist "Italian" mkdir "Italian"
if not exist "Spanish" mkdir "Spanish"

move /Y "French" "Bobsled\"
move /Y "German" "Bobsled\"
move /Y "Italian" "Bobsled\"
move /Y "Spanish" "Bobsled\"

move /Y bob.one "Bobsled\"
move /Y bobF.one "Bobsled\French"
move /Y bobG.one "Bobsled\German"
move /Y bobI.one "Bobsled\Italian"
move /Y bobS.one "Bobsled\Spanish"

if not exist "Common SET Placeable Objects" mkdir "Common SET Placeable Objects"
move /Y "comobj.one" "Common SET Placeable Objects\"

if not exist "Stage 09~10 - Frog Forest~Lost Jungle - Level Specific Object Model Copies" mkdir "Stage 09~10 - Frog Forest~Lost Jungle - Level Specific Object Model Copies"
move /Y "stgmem0910.one" "Stage 09~10 - Frog Forest~Lost Jungle - Level Specific Object Model Copies"

if not exist "Stage 06 - BINGO Highway - Lighting Strip Decoration Models" mkdir "Stage 06 - BINGO Highway - Lighting Strip Decoration Models"
for %%f in (stg06_kw*) do (move "%%f" "Stage 06 - BINGO Highway - Lighting Strip Decoration Models\")

if not exist "Stage 09 - Frog Forest - Custom Propeller Model" mkdir "Stage 09 - Frog Forest - Custom Propeller Model"
move /Y obj09* "Stage 09 - Frog Forest - Custom Propeller Model\"

if not exist "Stage 10 - Lost Jungle - Custom Propeller Model" mkdir "Stage 10 - Lost Jungle - Custom Propeller Model"
move /Y obj10* "Stage 10 - Lost Jungle - Custom Propeller Model\"

if not exist "Stage 73 - Frog Forest 2P - Custom Propeller Model" mkdir "Stage 73 - Frog Forest 2P - Custom Propeller Model"
move /Y obj73* "Stage 73 - Frog Forest 2P - Custom Propeller Model\"

if not exist "Stage 07~08 - Rail Canyon~Bullet Station - Sparks Model" mkdir "Stage 07~08 - Rail Canyon~Bullet Station - Sparks Model"
move /Y obj0708_sparks.dff "Stage 07~08 - Rail Canyon~Bullet Station - Sparks Model\"

if not exist "Generic Propeller Model" mkdir "Generic Propeller Model"
move /Y obj_prop.dff "Generic Propeller Model\"

if not exist "Generic Rain Model" mkdir "Generic Rain Model"
move /Y rain_ita.dff "Generic Rain Model\"
goto :eof

:MoveGenericStageTitles
for %%f in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\stgtitle_disp*.one") do ( 
	set FileName=%%~nf
	set GenericStageTitleID=!FileName:~13,3!
	echo "Moving Generic StageTitles ==> %%~nxf"
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
	echo "Moving Character StageTitles ==> %%~nxf"

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
		
		echo "ActionStage Files to Folder ==> DAT: !directory!"
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
		
		echo "ActionStage Files to Folder ==> DMO: !directory!"
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
		
		echo "ActionStage Files to Folder ==> TXC: !directory!"
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
		
		echo "ActionStage Files to Folder ==> SPL: !directory!"
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
		
		echo "ActionStage Files to Folder ==> TXD: !directory!"
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
		
		echo "ActionStage Files to Folder ==> ONE: !directory!"
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
		
		echo "ActionStage Files to Folder ==> Collision: !directory!"
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
		
		echo "ActionStage Files to Folder ==> BIN: !directory!"
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
		
		echo "ActionStage Files to Folder ==> BMP: !directory!"
	)
)
goto :eof

REM PART 1, SEE NEXT (LevelAssetsToCategoryLoop) LABEL
:LevelAssetsToCategory
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	for %%f in ("%%d\*") do ( 
		set FullFileName=%%~nxf
		set FileName=%%~nf
		set File=%%f
		set Directory=%%d
		
		set Extension=%%~xf
		set Last3Chars=!FileName:~-3!
		set Last5Chars=!FileName:~-5!
		set Last7Chars=!FileName:~-7!
		set Last8Chars=!FileName:~-8!
		set StageTitleTest=!FileName:~5,10!
		set StageTitleTestExtended=!FileName:~5,12!
		echo "Level Assets Moving ==> %%~nxf"
		call :LevelAssetsToCategoryLoop
	)
)
goto :eof

:LevelAssetsToCategoryLoop
REM Move Stage Title Card Mission Text
if /I !Extension!==.bmp (move /Y "!File!" "!Directory!\TitleCardMissionText\" && goto :eof)
REM Move Stage Specific Objects
if /I !Last3Chars!==obj (move /Y "!File!" "!Directory!\StageSpecificObjects\" && goto :eof)
if /I !Last3Chars!==MRG (move /Y "!File!" "!Directory!\StageSpecificObjects\GeometryAndEnvironmentModels\" && goto :eof)
REM Move Stage All Team Layout
if /I !Last3Chars!==_PB (move /Y "!File!" "!Directory!\ObjectLayouts\AllTeams\" && goto :eof)
REM Move Stage Team Sonic Layout
if /I !Last3Chars!==_P1 (move /Y "!File!" "!Directory!\ObjectLayouts\TeamSonic\" && goto :eof)
REM Move Stage Team Dark Layout
if /I !Last3Chars!==_P2 (move /Y "!File!" "!Directory!\ObjectLayouts\TeamDark\" && goto :eof)
REM Move Stage Team Rose Layout
if /I !Last3Chars!==_P3 (move /Y "!File!" "!Directory!\ObjectLayouts\TeamRose\" && goto :eof)
REM Move Stage Team Chaotix Layout
if /I !Last3Chars!==_P4 (move /Y "!File!" "!Directory!\ObjectLayouts\TeamChaotix\" && goto :eof)
REM Move Stage Super Hard Layout
if /I !Last3Chars!==_P5 (move /Y "!File!" "!Directory!\ObjectLayouts\SuperHard\" && goto :eof)
REM Move Stage Decoration Layout
if /I !Last3Chars!==_DB (move /Y "!File!" "!Directory!\ObjectLayouts\DecorationLayouts\" && goto :eof)
REM Move Stage Collision Data
if /I !Extension!==.cl (
	if /I !Last3Chars!==_wt (move /Y "!File!" "!Directory!\Collision\WaterCollision\" && goto :eof)
	if /I !Last3Chars!==_WT (move /Y "!File!" "!Directory!\Collision\WaterCollision\" && goto :eof)
	if /I !Last3Chars!==_xx (move /Y "!File!" "!Directory!\Collision\DeathPlanes\" && goto :eof)
	if /I !Last3Chars!==_XX (move /Y "!File!" "!Directory!\Collision\DeathPlanes\" && goto :eof)
	move /Y "!File!" "!Directory!\Collision\" && goto :eof
)
REM Move Stage Title Cards
if /I !StageTitleTestExtended!==title_dispEX (move /Y "!File!" "!Directory!\TitleCard\ExtraMission\" && goto :eof)
if /I !StageTitleTestExtended!==title_dispSH (move /Y "!File!" "!Directory!\TitleCard\SuperHard\" && goto :eof)
if /I !StageTitleTest!==title_disp (move /Y "!File!" "!Directory!\TitleCard\" && goto :eof)
REM Move Stage Autoplay Demos
if /I !Extension!==.dmo (move /Y "!File!" "!Directory!\IdleAutoplayDemos\" && goto :eof)
REM Move Lighting Data
if /I !Last5Chars!==light (move /Y "!File!" "!Directory!\LightingData\" && goto :eof)
REM Move FalcoEnemy
if /I !Last5Chars!==flyer (move /Y "!File!" "!Directory!\CustomFalcoEnemy" && goto :eof)
REM Move Camera Data
if /I !Last3Chars!==cam (move /Y "!File!" "!Directory!\CameraData\" && goto :eof)
REM Move Geometry Visibility Data
if /I !Last3Chars!==blk (move /Y "!File!" "!Directory!\GeometryVisibilityData\" && goto :eof)
REM Move Textures
if /I !Extension!==.txd (move /Y "!File!" "!Directory!\Textures\" && goto :eof)
if /I !Extension!==.txc (move /Y "!File!" "!Directory!\Textures\" && goto :eof)
REM Move Indirectional Data
if /I !Last7Chars!==indinfo (move /Y "!File!" "!Directory!\IndirectionalData\" && goto :eof)
REM Move Particle Data
if /I !Last5Chars!==_ptcl (move /Y "!File!" "!Directory!\ParticleData\" && goto :eof)
if /I !Last8Chars!==ptclplay (move /Y "!File!" "!Directory!\ParticleData\" && goto :eof)
REM Move Spline Data
if /I !Extension!==.spl (move /Y "!File!" "!Directory!\ExtraSplineData\" && goto :eof)
REM Move Geometry
set GeomFileNameTest1=!FileName:~1,2!
set GeomFileNameTest2=!FileName:~3,2!
if /I !FileName!==s!GeomFileNameTest1! (move /Y "!File!" "!Directory!\Geometry\" && goto :eof)
if /I !FileName!==stg!GeomFileNameTest2! (move /Y "!File!" "!Directory!\Geometry\" && goto :eof)
goto :eof

:CleanupAndMoveRemaining
echo "Cleaning Up And Moving Remaining Assets ==><=="
if not exist "%WORKING_DIRECTORY%\ROM\Levels\Unused\TitleCardMissionText" mkdir "%WORKING_DIRECTORY%\ROM\Levels\Unused\TitleCardMissionText"
move /Y "%WORKING_DIRECTORY%\ROM\stg05.bin" "%WORKING_DIRECTORY%\ROM\Levels\ActionStages\Stage 05 - Casino Park\Unknown\"
move /Y "%WORKING_DIRECTORY%\ROM\startStage.inf" "%WORKING_DIRECTORY%\ROM\Levels\Other\"
move /Y "%WORKING_DIRECTORY%\ROM\setidtbl.bin" "%WORKING_DIRECTORY%\ROM\Levels\Other\SETIDTable\"
move /Y "%WORKING_DIRECTORY%\ROM\cmn_ptcl.bin" "%WORKING_DIRECTORY%\ROM\Levels\Other\CommonParticleData\"
rem Move all of the remaining stage titles. (To unused, if unidentified)
for %%i in (%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*.bmp) do (move /Y "%%i" "%WORKING_DIRECTORY%\ROM\Levels\Unused\TitleCardMissionText\" && echo "Moving Remaining Stage Titles ==> %%~nxi")
goto :eof

:MoveTextures
cd %WORKING_DIRECTORY%\ROM\
ren "textures" "Textures"
if not exist "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects" mkdir "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects"
if not exist "%WORKING_DIRECTORY%\ROM\Textures\Common\Loading" mkdir "%WORKING_DIRECTORY%\ROM\Textures\Common\Loading"
if not exist "%WORKING_DIRECTORY%\ROM\Textures\Common\StartButton" mkdir "%WORKING_DIRECTORY%\ROM\Textures\Common\StartButton"
if not exist "%WORKING_DIRECTORY%\ROM\Textures\Unused\Enemies" mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\Enemies"
if not exist "%WORKING_DIRECTORY%\ROM\Textures\Unused\E3" mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\E3"
if not exist "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Other" mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Other"

for %%f in (%WORKING_DIRECTORY%\ROM\Textures\*) do (
	set FileName=%%~nf
	set File=%%f
	set Extension=%%~xf
	set First3Chars=!FileName:~0,3!
	set First8Chars=!FileName:~0,8!
	
	echo "Moving Textures ==> %%~nxf"
	call :MoveTexturesLoop
)
REM UNUSED TEXTURES
for %%f in (%WORKING_DIRECTORY%\ROM\Textures\*.txd) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Textures\Unused\" && echo "Moving Unused Textures ==> %%~nxf")
goto :eof

:MoveTexturesLoop
if /I !First3Chars!==obj (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Common\")
if /I !First3Chars!==eff (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects\")
if /I !FileName!==rain_ita (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects\")
if /I !FileName!==cmn_effect (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects\")
if /I !First3Chars!==ef_ (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Common\Effects\")
if /I !First8Chars!==startbtn (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Common\StartButton\")
if /I !FileName!==loading (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Common\Loading\")
if /I !First3Chars!==en_ (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Unused\Enemies\")
if /I !FileName!==e3adv (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Unused\E3\")
if /I !FileName!==comsoon (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Unused\E3\")

REM NTSC-U SPECIFIC
if /I !FileName!==stg40_indinfo (mkdir "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Stage 40 - Bonus Stage 2\IndirectionalData" & move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Stage 40 - Bonus Stage 2\IndirectionalData\")
	
REM PICK UP ANY OTHER SCRAPS LEFT
if /I not !Extension!==.txd (move /Y !File! "%WORKING_DIRECTORY%\ROM\Textures\Unused\OutOfPlace\Other\" && echo "Moving Remaining Textures ==> %%~nxf")
goto :eof

:MoveEnemies
cd %WORKING_DIRECTORY%\ROM
if not exist "Enemies~Bosses\Bosses" mkdir "Enemies~Bosses\Bosses"
if not exist "Enemies~Bosses\CommonAssets" mkdir "Enemies~Bosses\CommonAssets"
if not exist "Enemies~Bosses\Enemies" mkdir "Enemies~Bosses\Enemies"

REM MOVE ENEMIES TO FOLDER
for /D %%d in (%WORKING_DIRECTORY%\ROM\Enemies~Bosses\*) do (cd %%d && call :GenerateEnemyNames)
for %%f in (%WORKING_DIRECTORY%\ROM\*.one) do (
	set FileName=%%~nf
	set File=%%f
	set First3Chars=!FileName:~0,3!
	set First7Chars=!FileName:~0,7!
	echo "Moving Enemies ==> %%~nxf"
	call :MoveEnemiesLoop1
)

REM MOVE EACH ENEMY TO DESIGNATED FOLDER
for /D %%d in (%WORKING_DIRECTORY%\ROM\Enemies~Bosses\*) do (
	cd %%d
	for %%f in (%%d\*.one) do (
		set FileName=%%~nf
		set File=%%f
		set First12Chars=!FileName:~0,12!
		echo "Moving Enemies ==> %%~nxf"
		call :MoveEnemiesLoop2
	)
)
goto :eof

:MoveEnemiesLoop1
if /I !First7Chars!==chrboss (move /Y !File! "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\Bosses\" && goto :eof)
if /I !FileName!==en_common (move /Y !File! "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\CommonAssets\" && goto :eof)
if /I !FileName!==en_icon (move /Y !File! "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\CommonAssets\" && goto :eof)
if /I !First3Chars!==bs_ (move /Y !File! "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\Bosses\" && goto :eof)
if /I !First3Chars!==en_ (move /Y !File! "%WORKING_DIRECTORY%\ROM\Enemies~Bosses\Enemies\" && goto :eof)
goto :eof

:MoveEnemiesLoop2
if /I !FileName!==bs_albatross (move /Y !File! "EggAlbatross\" && goto :eof)
if /I !FileName!==bs_egghawk (move /Y !File! "EggHawk\" && goto :eof)
if /I !FileName!==bs_kingpawn (move /Y !File! "EggEmperor\" && goto :eof)
if /I !FileName!==chrboss_disp (move /Y !File! "TeamBattleIcons\TeamSonic\ && goto :eof")
if /I !FileName!==chrboss_dispC (move /Y !File! "TeamBattleIcons\TeamChaotix\" && goto :eof)
if /I !FileName!==chrboss_dispD (move /Y !File! "TeamBattleIcons\TeamDark\" && goto :eof)
if /I !FileName!==chrboss_dispR (move /Y !File! "TeamBattleIcons\TeamRose\" && goto :eof)
if /I !FileName!==en_common (move /Y !File! "CommonAssets\ && goto :eof")
if /I !FileName!==en_capture (move /Y !File! "Klagen\" && goto :eof)
if /I !FileName!==en_e2000 (move /Y !File! "E2000\" && goto :eof)
if /I !First12Chars!==en_eggmobile (move /Y !File! "EggMobile\" && goto :eof)
if /I !FileName!==en_flyer (move /Y !File! "Falco\" && goto :eof)
if /I !FileName!==en_icon (move /Y !File! "EnemyIcons\" && goto :eof)
if /I !FileName!==en_magician (move /Y !File! "EggMagician\" && goto :eof)
if /I !FileName!==en_metalsonic1st (move /Y !File! "MetalSonic1\" && goto :eof)
if /I !FileName!==en_metalsonic2nd (move /Y !File! "MetalSonic2\" && goto :eof)
if /I !FileName!==en_pawn (move /Y !File! "EggPawn\" && goto :eof)
if /I !FileName!==en_pawn_roulette (move /Y !File! "EggPawnCasino\" && goto :eof)
if /I !FileName!==en_rinoliner (move /Y !File! "Rhinoliner\" && goto :eof)
if /I !FileName!==en_searcher (move /Y !File! "Flapper\" && goto :eof)
if /I !FileName!==en_turtle (move /Y !File! "Cameron\" && goto :eof)
if /I !FileName!==en_wall (move /Y !File! "HeavyEggHammer\" && goto :eof)
goto :eof

:MoveSFX
cd %WORKING_DIRECTORY%\ROM
if not exist "SoundEffects\SoundEffectTables" mkdir "SoundEffects\SoundEffectTables"
if not exist "SoundEffects\Sounds" mkdir "SoundEffects\Sounds"
if not exist "SoundEffects\SoundEffectSoundLibConfiguration" mkdir "SoundEffects\SoundEffectSoundLibConfiguration"
move /Y %WORKING_DIRECTORY%\ROM\GCAX.conf "%WORKING_DIRECTORY%\ROM\SoundEffects\SoundEffectSoundLibConfiguration\" && echo "Moving SoundFX Lib Configuration ==> GCAX.conf"
for %%f in (%WORKING_DIRECTORY%\ROM\se_*.mlt) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\SoundEffects\Sounds\" && echo "Moving Sound Effect Sounds ==> %%~nxf")
for %%f in (%WORKING_DIRECTORY%\ROM\se_*.bin) do (move /Y %%f "%WORKING_DIRECTORY%\ROM\SoundEffects\SoundEffectTables\" && echo "Moving Sound Effect Tables ==> %%~nxf")
cd %WORKING_DIRECTORY%\ROM\SoundEffects\Sounds\ && call :CreateLevelFolders
cd %WORKING_DIRECTORY%\ROM\SoundEffects\SoundEffectTables\ && call :CreateLevelFolders
for /D %%d in ("%WORKING_DIRECTORY%\ROM\SoundEffects\*") do (
	if not exist "%%d\Player\TeamSonic" mkdir "%%d\Player\TeamSonic"
	if not exist "%%d\Player\TeamDark" mkdir "%%d\Player\TeamDark"
	if not exist "%%d\Player\TeamRose" mkdir "%%d\Player\TeamRose"
	if not exist "%%d\Player\TeamChaotix" mkdir "%%d\Player\TeamChaotix"
	if not exist "%%d\Enemy" mkdir "%%d\Enemy"
	if not exist "%%d\Common" mkdir "%%d\Common"
	if not exist "%%d\Vocal" mkdir "%%d\Vocal"
	if not exist "%%d\Unknown" mkdir "%%d\Unknown"
	for /D %%0 in ("%%d\*") do (
		for %%f in (%%d\*) do (
			set File=%%f
			set BaseDirectory=%%d
			set RootDirSFX=%%0
			
			set FileName=%%~nf
			set FolderName=%%~n0
			set LevelIDSFX=!FileName:~4,2!
			
			set LevelIDFolder=!FolderName:~6,2!
			
			set SFXTypeShort=!FileName:~3,2!
			set SFXType=!FileName:~3,5!
			
			echo "Sorting Sound Effect Sounds To Folder ==> %%~nxf"
			call :MoveSFXLoop
			REM echo "FolderName: !FolderName!"
			REM echo "RootDIRSFX: !File!"
			REM echo "FileName: !FileName!"
			REM echo "Level ID SFX CMP: !LevelIDSFX! - !LevelIDFolder!"
			REM echo "SFXType: !SFXType!"
		)
	)
)
goto :eof

:MoveSFXLoop
if /I not !FileName!==GCAX (if /I !LevelIDSFX!==!LevelIDFolder! (move /Y !File! "!RootDirSFX!\"))
if /I !SFXTypeShort!==pl (move /Y !File! "!BaseDirectory!\Player\" && goto :eof)
if /I !SFXTypeShort!==en (move /Y !File! "!BaseDirectory!\Enemy\" && goto :eof)
if /I !SFXTypeShort!==cn (move /Y !File! "!BaseDirectory!\Common\" && goto :eof)
if /I !SFXTypeShort!==vo (move /Y !File! "!BaseDirectory!\Vocal\" && goto :eof)
if /I !SFXTypeShort!==sy (move /Y !File! "!BaseDirectory!\Unknown\" && goto :eof)
if /I !SFXType!==CMNGK (move /Y !File! "!BaseDirectory!\Common\" && goto :eof)
if /I !SFXType!==VOICE (move /Y !File! "!BaseDirectory!\Vocal\" && goto :eof)
if /I !SFXType!==BASIC (move /Y !File! "!BaseDirectory!\Common\" && goto :eof)
if /I !SFXType!==CH_TS (move /Y !File! "!BaseDirectory!\Player\TeamSonic\" && goto :eof)
if /I !SFXType!==CH_TD (move /Y !File! "!BaseDirectory!\Player\TeamDark\" && goto :eof)
if /I !SFXType!==CH_TR (move /Y !File! "!BaseDirectory!\Player\TeamRose\" && goto :eof)
if /I !SFXType!==CH_TC (move /Y !File! "!BaseDirectory!\Player\TeamChaotix\" && goto :eof)
goto :eof

:CategoriseTitleCards
for /D %%X in ("%WORKING_DIRECTORY%\ROM\Levels\ActionStages\*") do (
	for /D %%d in ("%%X\TitleCardMissionText\") do (
		cd %%d
		call :MakeTitleCardsDirectories
		for %%f in (%%d\*) do (
			set FileName=%%~nf
			set FileNameNoTeamTest=!FileName:~0,8!
			set File=%%f
			set Directory0=%%d
			set Directory=!Directory0:~1,-2!
			
			set TitleCardExtraType=!FileName:~-5,2!
			set TitleCardTeam=!FileName:~5,1!
			set TitleCardLanguage=!FileName:~-3,1!
			echo "Categorizing Title Cards Mission Text To Teams ==> %%~nxf"
			call :TitleCardsSortingLoop
		)
	)
)
goto :eof

:TitleCardsSortingLoop
REM NOTEAM
if /I !FileName!==!FileNameNoTeamTest! ( 
	if /I !TitleCardLanguage!==E (move /Y "!File!" "!Directory!\AllTeams\English\" && goto :eof)
	if /I !TitleCardLanguage!==F (move /Y "!File!" "!Directory!\AllTeams\French\" && goto :eof)
	if /I !TitleCardLanguage!==G (move /Y "!File!" "!Directory!\AllTeams\German\" && goto :eof)
	if /I !TitleCardLanguage!==I (move /Y "!File!" "!Directory!\AllTeams\Italian\" && goto :eof)
	if /I !TitleCardLanguage!==J (move /Y "!File!" "!Directory!\AllTeams\Japanese\" && goto :eof)
	if /I !TitleCardLanguage!==K (move /Y "!File!" "!Directory!\AllTeams\Korean\" && goto :eof)
	if /I !TitleCardLanguage!==S (move /Y "!File!" "!Directory!\AllTeams\Spanish\" && goto :eof)
)
	
REM SONIC
if /I !TitleCardTeam!==S (
	if /I !TitleCardLanguage!==E (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamSonic\English\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamSonic\English\" && goto :eof))
	if /I !TitleCardLanguage!==F (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamSonic\French\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamSonic\French\" && goto :eof))
	if /I !TitleCardLanguage!==G (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamSonic\German\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamSonic\German\" && goto :eof))
	if /I !TitleCardLanguage!==I (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamSonic\Italian\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamSonic\Italian\" && goto :eof))
	if /I !TitleCardLanguage!==J (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamSonic\Japanese\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamSonic\Japanese\" && goto :eof))
	if /I !TitleCardLanguage!==K (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamSonic\Korean\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamSonic\Korean\" && goto :eof))
	if /I !TitleCardLanguage!==S (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamSonic\Spanish\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamSonic\Spanish\" && goto :eof))
)
REM DARK
if /I !TitleCardTeam!==D (
	if /I !TitleCardLanguage!==E (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamDark\English\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamDark\English\" && goto :eof))
	if /I !TitleCardLanguage!==F (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamDark\French\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamDark\French\" && goto :eof))
	if /I !TitleCardLanguage!==G (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamDark\German\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamDark\German\" && goto :eof))
	if /I !TitleCardLanguage!==I (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamDark\Italian\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamDark\Italian\" && goto :eof))
	if /I !TitleCardLanguage!==J (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamDark\Japanese\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamDark\Japanese\" && goto :eof))
	if /I !TitleCardLanguage!==K (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamDark\Korean\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamDark\Korean\" && goto :eof))
	if /I !TitleCardLanguage!==S (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamDark\Spanish\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamDark\Spanish\" && goto :eof))
)

REM ROSE
if /I !TitleCardTeam!==R (
	if /I !TitleCardLanguage!==E (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamRose\English\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamRose\English\" && goto :eof))
	if /I !TitleCardLanguage!==F (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamRose\French\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamRose\French\" && goto :eof))
	if /I !TitleCardLanguage!==G (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamRose\German\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamRose\German\" && goto :eof))
	if /I !TitleCardLanguage!==I (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamRose\Italian\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamRose\Italian\" && goto :eof))
	if /I !TitleCardLanguage!==J (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamRose\Japanese\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamRose\Japanese\" && goto :eof))
	if /I !TitleCardLanguage!==K (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamRose\Korean\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamRose\Korean\" && goto :eof))
	if /I !TitleCardLanguage!==S (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamRose\Spanish\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamRose\Spanish\" && goto :eof))
)
REM CHAOTIX
if /I !TitleCardTeam!==C (
	if /I !TitleCardLanguage!==E (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamChaotix\English\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamChaotix\English\" && goto :eof))
	if /I !TitleCardLanguage!==F (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamChaotix\French\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamChaotix\French\" && goto :eof))
	if /I !TitleCardLanguage!==G (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamChaotix\German\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamChaotix\German\" && goto :eof))
	if /I !TitleCardLanguage!==I (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamChaotix\Italian\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamChaotix\Italian\" && goto :eof))
	if /I !TitleCardLanguage!==J (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamChaotix\Japanese\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamChaotix\Japanese\" && goto :eof))
	if /I !TitleCardLanguage!==K (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamChaotix\Korean\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamChaotix\Korean\" && goto :eof))
	if /I !TitleCardLanguage!==S (if /I !TitleCardExtraType!==Ex (move /Y "!File!" "!Directory!\TeamChaotix\Spanish\ExtraMission" && goto :eof) else (move /Y "!File!" "!Directory!\TeamChaotix\Spanish\" && goto :eof))
)
goto :eof

:MakeTitleCardsDirectories
if not exist TeamSonic\English\ExtraMission mkdir TeamSonic\English\ExtraMission
if not exist TeamSonic\French\ExtraMission mkdir TeamSonic\French\ExtraMission
if not exist TeamSonic\German\ExtraMission mkdir TeamSonic\German\ExtraMission
if not exist TeamSonic\Italian\ExtraMission mkdir TeamSonic\Italian\ExtraMission
if not exist TeamSonic\Japanese\ExtraMission mkdir TeamSonic\Japanese\ExtraMission
if not exist TeamSonic\Korean\ExtraMission mkdir TeamSonic\Korean\ExtraMission
if not exist TeamSonic\Spanish\ExtraMission mkdir TeamSonic\Spanish\ExtraMission

if not exist AllTeams\English\ExtraMission mkdir AllTeams\English\ExtraMission
if not exist AllTeams\French\ExtraMission mkdir AllTeams\French\ExtraMission
if not exist AllTeams\German\ExtraMission mkdir AllTeams\German\ExtraMission
if not exist AllTeams\Italian\ExtraMission mkdir AllTeams\Italian\ExtraMission
if not exist AllTeams\Japanese\ExtraMission mkdir AllTeams\Japanese\ExtraMission
if not exist AllTeams\Korean\ExtraMission mkdir AllTeams\Korean\ExtraMission
if not exist AllTeams\Spanish\ExtraMission mkdir AllTeams\Spanish\ExtraMission

if not exist TeamDark\English\ExtraMission mkdir TeamDark\English\ExtraMission
if not exist TeamDark\French\ExtraMission mkdir TeamDark\French\ExtraMission
if not exist TeamDark\German\ExtraMission mkdir TeamDark\German\ExtraMission
if not exist TeamDark\Italian\ExtraMission mkdir TeamDark\Italian\ExtraMission
if not exist TeamDark\Japanese\ExtraMission mkdir TeamDark\Japanese\ExtraMission
if not exist TeamDark\Korean\ExtraMission mkdir TeamDark\Korean\ExtraMission
if not exist TeamDark\Spanish\ExtraMission mkdir TeamDark\Spanish\ExtraMission

if not exist TeamRose\English\ExtraMission mkdir TeamRose\English\ExtraMission
if not exist TeamRose\French\ExtraMission mkdir TeamRose\French\ExtraMission
if not exist TeamRose\German\ExtraMission mkdir TeamRose\German\ExtraMission
if not exist TeamRose\Italian\ExtraMission mkdir TeamRose\Italian\ExtraMission
if not exist TeamRose\Japanese\ExtraMission mkdir TeamRose\Japanese\ExtraMission
if not exist TeamRose\Korean\ExtraMission mkdir TeamRose\Korean\ExtraMission
if not exist TeamRose\Spanish\ExtraMission mkdir TeamRose\Spanish\ExtraMission

if not exist TeamChaotix\English\ExtraMission mkdir TeamChaotix\English\ExtraMission
if not exist TeamChaotix\French\ExtraMission mkdir TeamChaotix\French\ExtraMission
if not exist TeamChaotix\German\ExtraMission mkdir TeamChaotix\German\ExtraMission
if not exist TeamChaotix\Italian\ExtraMission mkdir TeamChaotix\Italian\ExtraMission
if not exist TeamChaotix\Japanese\ExtraMission mkdir TeamChaotix\Japanese\ExtraMission
if not exist TeamChaotix\Korean\ExtraMission mkdir TeamChaotix\Korean\ExtraMission
if not exist TeamChaotix\Spanish\ExtraMission mkdir TeamChaotix\Spanish\ExtraMission
goto :eof

:MoveHUD
rem
rem Deal with the Heads up Display.
rem
cd %WORKING_DIRECTORY%\ROM\
if not exist HUD\ResultsScreen\TimeOnly mkdir HUD\ResultsScreen\TimeOnly
if not exist HUD\ResultsScreen\Normal mkdir HUD\ResultsScreen\Normal
if not exist HUD\ResultsScreen\BonusStage mkdir HUD\ResultsScreen\BonusStage
if not exist HUD\TeamSonic\ mkdir HUD\TeamSonic\
if not exist HUD\TeamDark\ mkdir HUD\TeamDark\
if not exist HUD\TeamChaotix\ mkdir HUD\TeamChaotix\
if not exist HUD\TeamRose\ mkdir HUD\TeamRose\
if not exist HUD\2Player\ mkdir HUD\2Player\

for /D %%d in (%WORKING_DIRECTORY%\ROM\HUD\*) do (
	if not exist "%%d\English" mkdir "%%d\English"
	if not exist "%%d\French" mkdir "%%d\French"
	if not exist "%%d\German" mkdir "%%d\German"
	if not exist "%%d\Italian" mkdir "%%d\Italian"
	if not exist "%%d\Korean" mkdir "%%d\Korean"
	if not exist "%%d\Spanish" mkdir "%%d\Spanish"
)

for /D %%1 in (%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\*) do (
	if not exist "%%1\English" mkdir "%%1\English"
	if not exist "%%1\French" mkdir "%%1\French"
	if not exist "%%1\German" mkdir "%%1\German"
	if not exist "%%1\Italian" mkdir "%%1\Italian"
	if not exist "%%1\Korean" mkdir "%%1\Korean"
	if not exist "%%1\Spanish" mkdir "%%1\Spanish"
)
for %%f in (%WORKING_DIRECTORY%\ROM\*.one) do (
	set FileName=%%~nf
	set File=%%f
	set HUDIdentifier=!FileName:~0,9!
	set ResultIdentifier=!FileName:~0,6!
	set HUDType=!FileName:~9,1!
	set HUDLanguage=!FileName:~-1!
	set ResultType=!FileName:~6,1!
	echo "Moving HUD Items ==> %%~nxf"
	call :MoveHUDLoop
)
goto :eof

:MoveHUDLoop
if /I !HUDIdentifier!==game_disp (
	REM TYPE SONIC
	if /I !FileName!==game_disp (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\" && goto :eof)
	if /I !HUDType!==E (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\English\" && goto :eof)
	if /I !HUDType!==F (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\French\" && goto :eof)
	if /I !HUDType!==G (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\German\" && goto :eof)
	if /I !HUDType!==I (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\Italian\" && goto :eof)
	if /I !HUDType!==K (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\Korean\" && goto :eof)
	if /I !HUDType!==S (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamSonic\Spanish\" && goto :eof)
		
	REM TYPE 2PLAYER
	if /I !HUDType!==2 (
		if /I !FileName!==game_disp2 (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\2Player\" && goto :eof)
		if /I !HUDLanguage!==E (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\2Player\English\" && goto :eof)
		if /I !HUDLanguage!==F (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\2Player\French\" && goto :eof)
		if /I !HUDLanguage!==G (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\2Player\German\" && goto :eof)
		if /I !HUDLanguage!==I (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\2Player\Italian\" && goto :eof)
		if /I !HUDLanguage!==K (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\2Player\Korean\" && goto :eof)
		if /I !HUDLanguage!==S (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\2Player\Spanish\" && goto :eof)
	)		
	
	REM TYPE CHAOTIX
	if /I !HUDType!==C (
		if /I !FileName!==game_dispC (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\" && goto :eof)
		if /I !HUDLanguage!==E (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\English\" && goto :eof)
		if /I !HUDLanguage!==F (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\French\" && goto :eof)
		if /I !HUDLanguage!==G (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\German\" && goto :eof)
		if /I !HUDLanguage!==I (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\Italian\" && goto :eof)
		if /I !HUDLanguage!==K (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\Korean\" && goto :eof)
		if /I !HUDLanguage!==S (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamChaotix\Spanish\" && goto :eof)
	)		
	
	REM TYPE DARK
	if /I !HUDType!==D (
		if /I !FileName!==game_dispD (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\" && goto :eof)
		if /I !HUDLanguage!==E (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\English\" && goto :eof)
		if /I !HUDLanguage!==F (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\French\" && goto :eof)
		if /I !HUDLanguage!==G (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\German\" && goto :eof)
		if /I !HUDLanguage!==I (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\Italian\" && goto :eof)
		if /I !HUDLanguage!==K (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\Korean\" && goto :eof)
		if /I !HUDLanguage!==S (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamDark\Spanish\" && goto :eof)
	)
	
	REM TYPE ROSE
	if /I !HUDType!==R (
		if /I !FileName!==game_dispR (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\" && goto :eof)
		if /I !HUDLanguage!==E (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\English\" && goto :eof)
		if /I !HUDLanguage!==F (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\French\" && goto :eof)
		if /I !HUDLanguage!==G (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\German\" && goto :eof)
		if /I !HUDLanguage!==I (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\Italian\" && goto :eof)
		if /I !HUDLanguage!==K (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\Korean\" && goto :eof)
		if /I !HUDLanguage!==S (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\TeamRose\Spanish\" && goto :eof)
	)
)

if /I !ResultIdentifier!==result (
	REM RESULT TYPE A, NORMAL MODE
	if /I !ResultType!==A (
		if /I !FileName!==resultA_disp (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\" && goto :eof)
		if /I !HUDLanguage!==F (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\French\" && goto :eof)
		if /I !HUDLanguage!==G (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\German\" && goto :eof)
		if /I !HUDLanguage!==I (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\Italian\" && goto :eof)
		if /I !HUDLanguage!==S (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\Normal\Spanish\" && goto :eof)
	)		
	REM RESULT TYPE B, TIME ONLY
	if /I !ResultType!==B (
		if /I !FileName!==resultB_disp (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\" && goto :eof)
		if /I !HUDLanguage!==F (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\French\" && goto :eof)
		if /I !HUDLanguage!==G (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\German\" && goto :eof)
		if /I !HUDLanguage!==I (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\Italian\" && goto :eof)
		if /I !HUDLanguage!==S (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\TimeOnly\Spanish\" && goto :eof)
	)		
	REM RESULT TYPE S, BONUS STAGE
	if /I !ResultType!==S (
		if /I !FileName!==resultS_disp (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\" && goto :eof)
		if /I !HUDLanguage!==F (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\French\" && goto :eof)
		if /I !HUDLanguage!==G (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\German\" && goto :eof)
		if /I !HUDLanguage!==I (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\Italian\" && goto :eof)
		if /I !HUDLanguage!==S (move /Y !File! "%WORKING_DIRECTORY%\ROM\HUD\ResultsScreen\BonusStage\Spanish\" && goto :eof)
	)
)
goto :eof

:RootDirCleanup
rem
rem Sort out what is left in the ROOTDIR. Misc preprogrammed actions.
rem
if not exist "%WORKING_DIRECTORY%\ROM\Unknown\Group1\" mkdir "%WORKING_DIRECTORY%\ROM\Unknown\Group1\"

for %%f in (%WORKING_DIRECTORY%\ROM\*) do (
	set FileName=%%~nf
	set File=%%f
	set CoverIdentifier=!FileName:~0,9!
	set StartbtnIdentifier=!FileName:~0,8!
	set CoverLanguage=!FileName:~9,2!
	set StartbtnLanguage=!FileName:~9,2!
	
	set 3Chars=!FileName:~0,3!
	echo "Cleaning Up Root Directory ==> %%~nxf"
	call :RootDirCleanupLoop
)
goto :eof

:RootDirCleanupLoop
if /I !CoverIdentifier!==coverOpen (
	if /I !FileName!==coverOpen (echo "SWAG" && if not exist "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Unknown\" mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Unknown\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Unknown\" && goto :eof)
	if /I !CoverLanguage!==en (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\English\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\English\" && goto :eof)
	if /I !CoverLanguage!==fr (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\French\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\French\" && goto :eof)
	if /I !CoverLanguage!==ge (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\German\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\German\" && goto :eof)
	if /I !CoverLanguage!==it (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Italian\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Italian\" && goto :eof)
	if /I !CoverLanguage!==jp (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Japanese\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Japanese\" && goto :eof)
	if /I !CoverLanguage!==ko (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Korean\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Korean\" && goto :eof)
	if /I !CoverLanguage!==sp (mkdir "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Spanish\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\Spanish\" && goto :eof)
)	
	
if /I !StartbtnIdentifier!==startbtn (
	if /I !StartbtnLanguage!==en (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\English\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\English\" && goto :eof)
	if /I !StartbtnLanguage!==fr (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\French\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\French\" && goto :eof)
	if /I !StartbtnLanguage!==ge (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\German\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\German\" && goto :eof)
	if /I !StartbtnLanguage!==it (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Italian\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Italian\" && goto :eof)
	if /I !StartbtnLanguage!==jp (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Japanese\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Japanese\" && goto :eof)
	if /I !StartbtnLanguage!==ko (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Korean\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Korean\" && goto :eof)
	if /I !StartbtnLanguage!==sp (mkdir "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Spanish\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\TitleScreenStartButtonAnimation\Spanish\" && goto :eof)
)
	
if /I !FileName!==opening (mkdir "%WORKING_DIRECTORY%\ROM\Other\GameCubeBanner\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\GameCubeBanner\" && goto :eof)
if /I !FileName!==pk1_par1 (mkdir "%WORKING_DIRECTORY%\ROM\Unused\SeasideHillGiantBirdTexture\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Unused\SeasideHillGiantBirdTexture\" && goto :eof)
if /I !FileName!==mte_gcn (mkdir "%WORKING_DIRECTORY%\ROM\Other\GameCubeMaterialDefinitions\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\GameCubeMaterialDefinitions\" && goto :eof)
if /I !FileName!==startLoad (mkdir "%WORKING_DIRECTORY%\ROM\Other\LoadingLogoOnGameLaunch\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\LoadingLogoOnGameLaunch\" && goto :eof)
if /I !FileName!==loading (mkdir "%WORKING_DIRECTORY%\ROM\Other\TeamBattle~E3ProtoLoadAnimation\" && move /Y !File! "%WORKING_DIRECTORY%\ROM\Other\TeamBattle~E3ProtoLoadAnimation\" && goto :eof)
	
if /I !3Chars!==mc_ (move /Y !File! "%WORKING_DIRECTORY%\ROM\Unknown\Group1\" && goto :eof)
goto :eof

:SortOutFonts
call :DoFontFolders

REM ASCII EUROPE
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\a_euascii*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Main Menu Ascii ==> %%~nxf"
	call :AsciiEurope
)

REM SCORE NUMBERS ALL
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\a_num_s*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Score Numbers Ascii ==> %%~nxf"
	call :ScoreNumbers
)

REM JAPAN ASCII
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\abc*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Main Menu Ascii ==> %%~nxf"
	call :JapanAscii
)

REM JAPAN ASCII2
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\def*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Main Menu Ascii ==> %%~nxf"
	call :JapanAscii
)

REM UNUSED ENGLISH ASCII
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\a_enascii*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Main Menu Ascii ==> %%~nxf"
	call :EnglishAscii
)

REM UNUSED ENGLISH ASCII2
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\ascii00*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Main Menu Ascii ==> %%~nxf"
	call :EnglishAscii
)

REM MAIN MENU ASCII ALL
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\adv_*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Main Menu Ascii ==> %%~nxf"
	call :MainMenuAscii
)

REM MAIN MENU ASCII ALL2
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\madv*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Main Menu Ascii ==> %%~nxf"
	call :MainMenuAscii
)

REM DUMMY ASCII
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\dummy*) do (
	set File=%%f
	set Extension=%%~xf
	echo "Sorting out Dummy Ascii ==> %%~nxf"
	call :DummyAscii
)

REM HINTMENU ASCII
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\hintmenu*) do (
	set File=%%f
	set FileName=%%~nf
	set MainMenuIdentifier=!FileName:~0,8!
	set MainMenuLanguage=!FileName:~8,1!
	set Extension=%%~xf
	echo "Sorting out Hint Menu Fonts ==> %%~nxf"
	call :HintMenuAscii
)

REM EVENTS
for %%f in (%WORKING_DIRECTORY%\ROM\Fonts~Text\event*) do (
	set File=%%f
	set Extension=%%~xf
	set FileName=%%~nf
	set EventLanguage=!FileName:~10,1!
	set EventTest=!FileName:~0,5!
	set EventTeam=!FileName:~6,1!
	echo "Sorting out Event Text ==> %%~nxf"
	call :EventAscii
)

REM SETUP FONT HINT DIRECTORIES
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*") do (
	if not exist "%%d\Metrics\Japanese" mkdir "%%d\Metrics\Japanese"
	if not exist "%%d\Metrics\Korean" mkdir "%%d\Metrics\Korean"
	if not exist "%%d\Metrics\English" mkdir "%%d\Metrics\English"
	if not exist "%%d\Metrics\French" mkdir "%%d\Metrics\French"
	if not exist "%%d\Metrics\German" mkdir "%%d\Metrics\German"
	if not exist "%%d\Metrics\Italian" mkdir "%%d\Metrics\Italian"
	if not exist "%%d\Metrics\Spanish" mkdir "%%d\Metrics\Spanish"
	
	if not exist "%%d\FontMaps\Japanese" mkdir "%%d\FontMaps\Japanese"
	if not exist "%%d\FontMaps\Korean" mkdir "%%d\FontMaps\Korean"
	if not exist "%%d\FontMaps\English" mkdir "%%d\FontMaps\English"
	if not exist "%%d\FontMaps\French" mkdir "%%d\FontMaps\French"
	if not exist "%%d\FontMaps\German" mkdir "%%d\FontMaps\German"
	if not exist "%%d\FontMaps\Italian" mkdir "%%d\FontMaps\Italian"
	if not exist "%%d\FontMaps\Spanish" mkdir "%%d\FontMaps\Spanish"
	
	if not exist "%%d\TextFontMaps\Korean" mkdir "%%d\TextFontMaps\Korean"
	if not exist "%%d\TextFontMaps\Japanese" mkdir "%%d\TextFontMaps\Japanese"
	if not exist "%%d\TextFontMaps\English" mkdir "%%d\TextFontMaps\English"
	if not exist "%%d\TextFontMaps\French" mkdir "%%d\TextFontMaps\French"
	if not exist "%%d\TextFontMaps\German" mkdir "%%d\TextFontMaps\German"
	if not exist "%%d\TextFontMaps\Italian" mkdir "%%d\TextFontMaps\Italian"
	if not exist "%%d\TextFontMaps\Spanish" mkdir "%%d\TextFontMaps\Spanish"
	
	if not exist "%%d\Binaries\English" mkdir "%%d\Binaries\English"
	if not exist "%%d\Binaries\Japanese" mkdir "%%d\Binaries\Japanese"
	if not exist "%%d\Binaries\French" mkdir "%%d\Binaries\French"
	if not exist "%%d\Binaries\German" mkdir "%%d\Binaries\German"
	if not exist "%%d\Binaries\Italian" mkdir "%%d\Binaries\Italian"
	if not exist "%%d\Binaries\Korean" mkdir "%%d\Binaries\Korean"
	if not exist "%%d\Binaries\Spanish" mkdir "%%d\Binaries\Spanish"
	
	if not exist "%%d\Unidentified\" mkdir "%%d\Unidentified\"
)

REM MOVE FONT HINT BMPS
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*") do (
	REM LES BMPS XDDDD AYY LMAO
	for %%f in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\*.bmp") do (
		set FileName=%%~nf
		set File=%%f
		set Directory=%%d
		set DirectoryByName=%%~nd
	
		set DirStageID=!DirectoryByName:~6,2!
		set StageID=!FileName:~5,2!
	
		set HintIdentifier=!FileName:~0,4!
		set HintLanguage=!FileName:~7,1!
		
		echo "Sorting out Hints in Loop - BMP ==> %%~nxf"
		call :FontsLoopBMP
	)		
)

REM MOVE FONT HINT PNGS
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*") do (
	REM LES PNG XDDDD AYY LMAO
	for %%f in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\*.png") do (
		set FileName=%%~nf
		set File=%%f
		set Directory=%%d
		set DirectoryByName=%%~nd
	
		set DirStageID=!DirectoryByName:~6,2!
		set StageID=!FileName:~5,2!
	
		set HintIdentifier=!FileName:~0,4!
		set HintLanguage=!FileName:~7,1!
		
		echo "Sorting out Hints in Loop - PNG ==> %%~nxf"
		call :FontsLoopPNG
	)		
)

REM MOVE FONT HINT TXT
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*") do (
	REM LES TXT XDDDD AYY LMAO
	for %%f in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\*.txt") do (
		set FileName=%%~nf
		set File=%%f
		set Directory=%%d
		set DirectoryByName=%%~nd
	
		set DirStageID=!DirectoryByName:~6,2!
		set StageID=!FileName:~5,2!
	
		set HintIdentifier=!FileName:~0,4!
		set HintLanguage=!FileName:~7,1!
		
		echo "Sorting out Hints in Loop - TXT ==> %%~nxf"
		call :FontsLoopTXT
	)		
)

REM MOVE FONT HINT BIN
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*") do (
	REM LES BIN XDDDD AYY LMAO
	for %%f in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\*.bin") do (
		set FileName=%%~nf
		set File=%%f
		set Directory=%%d
		set DirectoryByName=%%~nd
	
		set DirStageID=!DirectoryByName:~6,2!
		set StageID=!FileName:~5,2!
	
		set HintIdentifier=!FileName:~0,4!
		set HintLanguage=!FileName:~7,1!
		
		echo "Sorting out Hints in Loop - BIN ==> %%~nxf"
		call :FontsLoopBIN
	)		
)

REM MOVE FONT HINT MET
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*") do (
	REM LES MET XDDDD AYY LMAO
	for %%f in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\*.met") do (
		set FileName=%%~nf
		set File=%%f
		set Directory=%%d
		set DirectoryByName=%%~nd
	
		set DirStageID=!DirectoryByName:~6,2!
		set StageID=!FileName:~5,2!
	
		set HintIdentifier=!FileName:~0,4!
		set HintLanguage=!FileName:~7,1!
		
		echo "Sorting out Hints in Loop - MET ==> %%~nxf"
		call :FontsLoopMET
	)		
)

REM MOVE FONT HINT CLEANUP
for /D %%d in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*") do (
	REM LES CLEANUPM8S
	for %%f in ("%WORKING_DIRECTORY%\ROM\Fonts~Text\*") do (
		set FileName=%%~nf
		set File=%%f
		set Directory=%%d
		set DirectoryByName=%%~nd
	
		set DirStageID=!DirectoryByName:~6,2!
		set StageID=!FileName:~5,2!
	
		set HintIdentifier=!FileName:~0,4!
		set HintLanguage=!FileName:~7,1!
		
		echo "Sorting out Hints in Loop - Cleanup ==> %%~nxf"
		call :FontsLoopCleanup
	)	
)
goto :eof

:FontsLoopBMP
if /I !HintIdentifier!==hint (
	if /I !DirStageID!==!StageID! (
		if /I !HintLanguage!==k (move /Y !File! "!Directory!\FontMaps\Korean\" && goto :eof)
		if /I !HintLanguage!==j (move /Y !File! "!Directory!\FontMaps\Japanese\" && goto :eof)
		if /I !HintLanguage!==e (move /Y !File! "!Directory!\FontMaps\English\" && goto :eof)
		if /I !HintLanguage!==f (move /Y !File! "!Directory!\FontMaps\French\" && goto :eof)
		if /I !HintLanguage!==g (move /Y !File! "!Directory!\FontMaps\German\" && goto :eof)
		if /I !HintLanguage!==i (move /Y !File! "!Directory!\FontMaps\Italian\" && goto :eof)
		if /I !HintLanguage!==s (move /Y !File! "!Directory!\FontMaps\Spanish\" && goto :eof)
		move /Y !File! "!Directory!\FontMaps\" && goto :eof
	)
)
goto :eof

REM COPY OF FONTSLOOPBMP, FOR FUTURE USE.
:FontsLoopPNG
if /I !HintIdentifier!==hint (
	if /I !DirStageID!==!StageID! (
		if /I !HintLanguage!==k (move /Y !File! "!Directory!\FontMaps\Korean\" && goto :eof)
		if /I !HintLanguage!==j (move /Y !File! "!Directory!\FontMaps\Japanese\" && goto :eof)
		if /I !HintLanguage!==e (move /Y !File! "!Directory!\FontMaps\English\" && goto :eof)
		if /I !HintLanguage!==f (move /Y !File! "!Directory!\FontMaps\French\" && goto :eof)
		if /I !HintLanguage!==g (move /Y !File! "!Directory!\FontMaps\German\" && goto :eof)
		if /I !HintLanguage!==i (move /Y !File! "!Directory!\FontMaps\Italian\" && goto :eof)
		if /I !HintLanguage!==s (move /Y !File! "!Directory!\FontMaps\Spanish\" && goto :eof)
		move /Y !File! "!Directory!\FontMaps\" && goto :eof
	)
)
goto :eof

REM BINARIES
:FontsLoopBIN
if /I !HintIdentifier!==hint (
	if /I !DirStageID!==!StageID! (
		if /I !HintLanguage!==e (move /Y !File! "!Directory!\Binaries\English\" && goto :eof)
		if /I !HintLanguage!==f (move /Y !File! "!Directory!\Binaries\French\" && goto :eof)
		if /I !HintLanguage!==g (move /Y !File! "!Directory!\Binaries\German\" && goto :eof)
		if /I !HintLanguage!==i (move /Y !File! "!Directory!\Binaries\Italian\" && goto :eof)
		if /I !HintLanguage!==k (move /Y !File! "!Directory!\Binaries\Korean\" && goto :eof)
		if /I !HintLanguage!==s (move /Y !File! "!Directory!\Binaries\Spanish\" && goto :eof)
		if /I !HintLanguage!==j (move /Y !File! "!Directory!\Binaries\Japanese\" && goto :eof)
		move /Y !File! "!Directory!\Binaries\" && goto :eof
	)
)
goto :eof

REM TEXT FONT MAPS
:FontsLoopTXT
if /I !HintIdentifier!==hint (
	if /I !DirStageID!==!StageID! (
		if /I !HintLanguage!==k (move /Y !File! "!Directory!\TextFontMaps\Korean\" && goto :eof)
		if /I !HintLanguage!==j (move /Y !File! "!Directory!\TextFontMaps\Japanese\" && goto :eof)
		if /I !HintLanguage!==e (move /Y !File! "!Directory!\TextFontMaps\English\" && goto :eof)
		if /I !HintLanguage!==f (move /Y !File! "!Directory!\TextFontMaps\French\" && goto :eof)
		if /I !HintLanguage!==g (move /Y !File! "!Directory!\TextFontMaps\German\" && goto :eof)
		if /I !HintLanguage!==i (move /Y !File! "!Directory!\TextFontMaps\Italian\" && goto :eof)
		if /I !HintLanguage!==s (move /Y !File! "!Directory!\TextFontMaps\Spanish\" && goto :eof)
		move /Y !File! "!Directory!\TextFontMaps\" && goto :eof
	)
)
goto :eof

REM METRICS
:FontsLoopMET
if /I !HintIdentifier!==hint (
	if /I !DirStageID!==!StageID! (
		if /I !HintLanguage!==k (move /Y !File! "!Directory!\Metrics\Korean\" && goto :eof)
		if /I !HintLanguage!==j (move /Y !File! "!Directory!\Metrics\Japanese\" && goto :eof)
		if /I !HintLanguage!==e (move /Y !File! "!Directory!\Metrics\English\" && goto :eof)
		if /I !HintLanguage!==f (move /Y !File! "!Directory!\Metrics\French\" && goto :eof)
		if /I !HintLanguage!==g (move /Y !File! "!Directory!\Metrics\German\" && goto :eof)
		if /I !HintLanguage!==i (move /Y !File! "!Directory!\Metrics\Italian\" && goto :eof)
		if /I !HintLanguage!==s (move /Y !File! "!Directory!\Metrics\Spanish\" && goto :eof)
		move /Y !File! "!Directory!\Metrics\" && goto :eof
	)
)
goto :eof


:FontsLoopCleanup
REM CLEANUP
if /I !FileName!==errMessage (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\ErrorMessage\" && goto :eof)

REM HINTS
if /I !HintIdentifier!==hint (
	if /I !DirStageID!==!StageID! (
		if /I !HintLanguage!==e (move /Y !File! "!Directory!\English\" && goto :eof)
		if /I !HintLanguage!==f (move /Y !File! "!Directory!\French\" && goto :eof)
		if /I !HintLanguage!==g (move /Y !File! "!Directory!\German\" && goto :eof)
		if /I !HintLanguage!==i (move /Y !File! "!Directory!\Italian\" && goto :eof)
		if /I !HintLanguage!==k (move /Y !File! "!Directory!\Korean\" && goto :eof)
		if /I !HintLanguage!==s (move /Y !File! "!Directory!\Spanish\" && goto :eof) else (move /Y !File! "!Directory!\Unidentified\" && goto :eof)
	)
)
goto :eof

:DoFontFolders
rem
rem Sort out fonts and ingame text.
rem
cd %WORKING_DIRECTORY%\ROM\
ren "font" "Fonts~Text"
REM No scripting, time waster, not enough dirs to be worth 0.001s performance loss XDDDDD.
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\FontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\FontMaps
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\Metrics mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\Metrics
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\TextFontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\TextFontMaps

if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\Metrics mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\Metrics
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps

if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\FontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\FontMaps
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\Metrics mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\Metrics
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\TextFontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\TextFontMaps

if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\Metrics mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\Metrics
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\TextFontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\TextFontMaps

if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\TextFontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\TextFontMaps
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\Metrics mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\Metrics
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps

if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\FontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\FontMaps
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\Metrics mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\Metrics
if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\TextFontMaps mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\DUMMY\TextFontMaps

if not exist %WORKING_DIRECTORY%\ROM\Fonts~Text\ErrorMessage\ mkdir %WORKING_DIRECTORY%\ROM\Fonts~Text\ErrorMessage\

REM TSONIC
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\TextFontMaps"

if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\TextFontMaps"

REM TDARK
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\TextFontMaps"

if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\TextFontMaps"

REM TROSE
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\TextFontMaps"

if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\TextFontMaps"

REM TCHAOTIX
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\TextFontMaps"

if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\TextFontMaps"

REM TLASTSTORY
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\TextFontMaps"

if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\TextFontMaps"

REM Unused
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\TextFontMaps"

REM Main Menus
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\BinariesWithText" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\BinariesWithText"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps"
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps"

REM HINTTEXT
if not exist "%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\" mkdir "%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\"
cd "%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\"
call :CreateLevelFolders

for /D %%d in (%WORKING_DIRECTORY%\ROM\Fonts~Text\Hints\*) do (
	if not exist "%%d\English" mkdir "%%d\English"
	if not exist "%%d\French" mkdir "%%d\French"
	if not exist "%%d\German" mkdir "%%d\German"
	if not exist "%%d\Italian" mkdir "%%d\Italian"
	if not exist "%%d\Korean" mkdir "%%d\Korean"
	if not exist "%%d\Spanish" mkdir "%%d\Spanish"
	if not exist "%%d\Metrics" mkdir "%%d\Metrics"
	if not exist "%%d\FontMaps" mkdir "%%d\FontMaps"
	if not exist "%%d\TextFontMaps" mkdir "%%d\TextFontMaps"
)

for /D %%d in (%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\*) do (
	if not exist "%%d\English" mkdir "%%d\English"
	if not exist "%%d\French" mkdir "%%d\French"
	if not exist "%%d\German" mkdir "%%d\German"
	if not exist "%%d\Italian" mkdir "%%d\Italian"
	if not exist "%%d\Korean" mkdir "%%d\Korean"
	if not exist "%%d\Spanish" mkdir "%%d\Spanish"
	if not exist "%%d\Metrics" mkdir "%%d\Metrics"
	if not exist "%%d\FontMaps" mkdir "%%d\FontMaps"
	if not exist "%%d\TextFontMaps" mkdir "%%d\TextFontMaps"
)
goto :eof

:AsciiEurope
if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\FontMaps\" && goto :eof)
if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\FontMaps\" && goto :eof)
if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps\" && goto :eof)
if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Ascii\Metrics\" && goto :eof)
goto :eof

:ScoreNumbers
if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\FontMaps\" && goto :eof)
if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\FontMaps\" && goto :eof)
if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps\" && goto :eof)
if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\ScoreNumbers\Metrics\" && goto :eof)
goto :eof

:JapanAscii
if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps\" && goto :eof)
if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\FontMaps\" && goto :eof)
if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\TextFontMaps\" && goto :eof)
if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\JapanFont\Metrics\" && goto :eof)
goto :eof

:EnglishAscii
if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps\" && goto :eof)
if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\FontMaps\" && goto :eof)
if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\TextFontMaps\" && goto :eof)
if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\EnglishAscii\Metrics\" && goto :eof)
goto :eof

:MainMenuAscii
if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps\" && goto :eof)
if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\FontMaps\" && goto :eof)
if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\TextFontMaps\" && goto :eof)
if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenuAscii\Metrics\" && goto :eof)
goto :eof

:DummyAscii
if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\Dummy\FontMaps\" && goto :eof)
if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\Dummy\FontMaps\" && goto :eof)
if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\Dummy\TextFontMaps\" && goto :eof)
if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Unused\Dummy\Metrics\" && goto :eof)
goto :eof

:HintMenuAscii
if /I !Extension!==.png (
	if /I !MainMenuLanguage!==e (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\English\" && goto :eof)
	if /I !MainMenuLanguage!==f (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\French\" && goto :eof)
	if /I !MainMenuLanguage!==g (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\German\" && goto :eof)
	if /I !MainMenuLanguage!==i (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\Italian\" && goto :eof)
	if /I !MainMenuLanguage!==k (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\Korean\" && goto :eof)
	if /I !MainMenuLanguage!==s (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\Spanish\" && goto :eof) 
	move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\" && goto :eof
)
if /I !Extension!==.bmp (
	if /I !MainMenuLanguage!==e (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\English\" && goto :eof)
	if /I !MainMenuLanguage!==f (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\French\" && goto :eof)
	if /I !MainMenuLanguage!==g (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\German\" && goto :eof)
	if /I !MainMenuLanguage!==i (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\Italian\" && goto :eof)
	if /I !MainMenuLanguage!==k (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\Korean\" && goto :eof)
	if /I !MainMenuLanguage!==s (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\Spanish\" && goto :eof) 	
	move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\FontMaps\" && goto :eof
)
if /I !Extension!==.txt (
	if /I !MainMenuLanguage!==e (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps\English\" && goto :eof)
	if /I !MainMenuLanguage!==f (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps\French\" && goto :eof)
	if /I !MainMenuLanguage!==g (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps\German\" && goto :eof)
	if /I !MainMenuLanguage!==i (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps\Italian\" && goto :eof)
	if /I !MainMenuLanguage!==k (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps\Korean\" && goto :eof)
	if /I !MainMenuLanguage!==s (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps\Spanish\" && goto :eof) 	
	move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\TextFontMaps\" && goto :eof
)
if /I !Extension!==.met (
	if /I !MainMenuLanguage!==e (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\English\" && goto :eof)
	if /I !MainMenuLanguage!==f (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\French\" && goto :eof)
	if /I !MainMenuLanguage!==g (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\German\" && goto :eof)
	if /I !MainMenuLanguage!==i (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\Italian\" && goto :eof)
	if /I !MainMenuLanguage!==k (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\Korean\" && goto :eof)
	if /I !MainMenuLanguage!==s (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\Spanish\" && goto :eof) 	
	move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\" && goto :eof
)
if /I !Extension!==.bin (
	if /I !MainMenuLanguage!==e (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\BinariesWithText\English\" && goto :eof)
	if /I !MainMenuLanguage!==f (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\BinariesWithText\French\" && goto :eof)
	if /I !MainMenuLanguage!==g (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\BinariesWithText\German\" && goto :eof)
	if /I !MainMenuLanguage!==i (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\BinariesWithText\Italian\" && goto :eof)
	if /I !MainMenuLanguage!==k (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\BinariesWithText\Korean\" && goto :eof)
	if /I !MainMenuLanguage!==s (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\BinariesWithText\Spanish\" && goto :eof) 	
	move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\MainMenus\Metrics\" && goto :eof
)
goto :eof

:EventAscii
REM SONIC
if /I !EventTeam!==0 (
	if /I !EventLanguage!==k (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Korean\Metrics\" && goto :eof)
	)
	if /I !EventLanguage!==j (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamSonicEvents\Japanese\Metrics\" && goto :eof)
	)
)

REM DARK
if /I !EventTeam!==1 (
	if /I !EventLanguage!==k (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Korean\Metrics\" && goto :eof)
	)
	if /I !EventLanguage!==j (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamDarkEvents\Japanese\Metrics\" && goto :eof)
	)
)

REM ROSE
if /I !EventTeam!==2 (
	if /I !EventLanguage!==k (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Korean\Metrics\" && goto :eof)
	)
	if /I !EventLanguage!==j (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamRoseEvents\Japanese\Metrics\" && goto :eof)
	)
)

REM CHAOTIX
if /I !EventTeam!==3 (
	if /I !EventLanguage!==k (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Korean\Metrics\" && goto :eof)
	)
	if /I !EventLanguage!==j (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\TeamChaotixEvents\Japanese\Metrics\" && goto :eof)
	)
)

REM LAST
if /I !EventTeam!==4 (
	if /I !EventLanguage!==k (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Korean\Metrics\" && goto :eof)
	)
	if /I !EventLanguage!==j (
		if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\FontMaps\" && goto :eof)
		if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\TextFontMaps\" && goto :eof)
		if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\LastStoryEvents\Japanese\Metrics\" && goto :eof)
	)
)

if /I !Extension!==.png (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\FontMaps\" && goto :eof)
if /I !Extension!==.bmp (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\FontMaps\" && goto :eof)
if /I !Extension!==.txt (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\TextFontMaps\" && goto :eof)
if /I !Extension!==.met (move /Y !File! "%WORKING_DIRECTORY%\ROM\Fonts~Text\Events\Other~UnusedEvents\Metrics\" && goto :eof)
goto :eof

:GenerateEnemyNames
if not exist "EggAlbatross" mkdir "EggAlbatross"
if not exist "EggHawk" mkdir "EggHawk"
if not exist "EggEmperor" mkdir "EggEmperor"

if not exist "TeamBattleIcons\TeamSonic" mkdir "TeamBattleIcons\TeamSonic"
if not exist "TeamBattleIcons\TeamDark" mkdir "TeamBattleIcons\TeamDark"
if not exist "TeamBattleIcons\TeamRose" mkdir "TeamBattleIcons\TeamRose"
if not exist "TeamBattleIcons\TeamChaotix" mkdir "TeamBattleIcons\TeamChaotix"
if not exist "EnemyIcons" mkdir "EnemyIcons"
if not exist "CommonAssets" mkdir "CommonAssets"

if not exist "Klagen" mkdir "Klagen"
if not exist "E2000" mkdir "E2000"
if not exist "EggMobile" mkdir "EggMobile"
if not exist "Falco" mkdir "Falco"
if not exist "EggMagician" mkdir "EggMagician"
if not exist "MetalSonic1" mkdir "MetalSonic1"
if not exist "MetalSonic2" mkdir "MetalSonic2"
if not exist "EggPawn" mkdir "EggPawn"
if not exist "EggPawnCasino" mkdir "EggPawnCasino"
if not exist "Rhinoliner" mkdir "Rhinoliner"
if not exist "Flapper" mkdir "Flapper"
if not exist "Cameron" mkdir "Cameron"
if not exist "HeavyEggHammer" mkdir "HeavyEggHammer"
goto :eof

:ProtectBrokenONE
rem
rem HeroesONE goes Harambe! AYAYAYAYAYAYAY!!!
rem
cd %WORKING_DIRECTORY%\ROM\
rem Protect Weird .one files from HeroesONE and Removal
for /D %%d in (%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\*) do (
	for %%f in (%%d\*.one) do (
		cd %%d
		ren "%%~nxf" "%%~xnf.tmp"
		echo "Protecting unextractible .ONE ==> %%~nxf"
	)
)
goto :eof

:DecompressONE
cd "%WORKING_DIRECTORY%\ROM\"
for /R %%f in ("*.one") do (
	echo "HeroesONE Extracting: ==> %%~nxf"
	%WORKING_DIRECTORY%\Tools\HeroesONE\HeroesONE.exe -u "%%f" "%%~pnf.HeroesONE"
)
for /R %%f in ("*.one") do (DEL /F "%%f")
goto :eof

:RestoreBrokenONE
cd %WORKING_DIRECTORY%\ROM\
rem Protect Weird .one files from HeroesONE and Removal 2
for /D %%d in (%WORKING_DIRECTORY%\ROM\Other\OpenDiscTrayMessage\*) do (
	for %%f in (%%d\*.tmp) do (
		cd %%d
		ren "%%~nxf" "%%~nf"
		echo "Restoring unextractible .ONE ==> %%~nxf"
	)
)
goto :eof

:DoCleanup
rem Remove Useless Microsoft Sourcesafe Leftovers
for /R %%f in ("*") do ( 
	if /I %%~nxf==vssver.scc (DEL /F %%f) 
)

rem Kill Empty Directories
cd %WORKING_DIRECTORY%\ROM\
for /f "usebackq delims=" %%d in (`"dir /ad/b/s | sort /R"`) do (
	echo "Removing Directory If Empty ==> %%~nd"
	rd "%%d" 2>NUL 
)

rem ALL HAIL MARKEYJESTER
rem ALL HAIL MARKEYJESTER
rem ALL HAIL MARKEYJESTER
echo "DONE"
goto :eof

:SortOutAFS
for %%f in (ROM\*.afs) do (
	set FileName=%%~nf
	set FileNameFull=%%~nxf
	echo "AFS Unpacking: ==> %%~nxf"
	REM CREATE TEMP DIRECTORY
	if not exist "%WORKING_DIRECTORY%\Temp\!FileName!\ADX\" mkdir "%WORKING_DIRECTORY%\Temp\!FileName!\ADX\"
	REM CREATE DIRECTORY FOR THIS AFS FILE
	if not exist "%WORKING_DIRECTORY%\ROM\CharacterSpeech\!FileName!\" mkdir "%WORKING_DIRECTORY%\ROM\CharacterSpeech\!FileName!\"
	
	REM MOVE THE AFS TO ITS OWN DIRECTORY
	move /Y %%f "%WORKING_DIRECTORY%\Temp\!FileName!\"
	
	cd "%WORKING_DIRECTORY%\Temp\!FileName!\ADX\"
	REM CALL UNAFS AND EXTRACT ADX
	%WORKING_DIRECTORY%\Tools\UnAFS\unafs.exe "%WORKING_DIRECTORY%\Temp\!FileName!\!FileNameFull!"

	echo "Directory WINE Test: %WORKING_DIRECTORY%\Temp\!FileName!\ADX\*.adx" 
	REM MOVE ALL THE ADX FILES TO THEIR DESTINATION DIRECTORY
	for %%z in (%WORKING_DIRECTORY%\Temp\!FileName!\ADX\*.adx) do (
		if not exist "%WORKING_DIRECTORY%\ROM\CharacterSpeech\!FileName!\" mkdir "%WORKING_DIRECTORY%\ROM\CharacterSpeech\!FileName!\"
		move %%z "%WORKING_DIRECTORY%\ROM\CharacterSpeech\!FileName!\"
		echo TEST
	)
)	
pause
REM WIPE THE TEMP FOLDER
RMDIR /S /Q "%WORKING_DIRECTORY%\Temp\"
goto :eof

:SortOutADX
for /D %%d in (%WORKING_DIRECTORY%\ROM\CharacterSpeech\*) do (
	cd %%d
	for %%f in ("%%d\*.adx") do (
		echo "Character Speech Converting: ==> %%~nxf"
		set FileNameFull=%%~nxf
		set NewFileName=%%~nf.mp4
		%WORKING_DIRECTORY%\Tools\FFMPEG\ffmpeg.exe -i !FileNameFull! -c:v libfdk_aac -profile:a aac_he -vbr 3 !NewFileName!
		REM DELETE THE ADX FILE
		del "%%f"
	)
)
goto :eof

:DoMusic
if not exist "%WORKING_DIRECTORY%\ROM\BackgroundMusic\" mkdir "%WORKING_DIRECTORY%\ROM\BackgroundMusic\"

for %%f in ("%WORKING_DIRECTORY%\ROM\*.adx") do (
	move /Y %%f "%WORKING_DIRECTORY%\ROM\BackgroundMusic\"
)

cd "%WORKING_DIRECTORY%\ROM\BackgroundMusic\"

for %%f in ("%WORKING_DIRECTORY%\ROM\BackgroundMusic\*.adx") do (
	echo "Converting Music Tracks: ==> %%~nxf"
	set FileNameFull=%%~nxf
	set NewFileName=%%~nf.mp4
	%WORKING_DIRECTORY%\Tools\FFMPEG\ffmpeg.exe -i !FileNameFull! -c:v libfdk_aac -b:a 128k !NewFileName!
	REM DELETE THE ADX FILE
	del "%%f"
)
cd %WORKING_DIRECTORY%
goto :eof

:DoVideo
if not exist "%WORKING_DIRECTORY%\ROM\Movies\" mkdir "%WORKING_DIRECTORY%\ROM\Movies\"

for %%f in ("%WORKING_DIRECTORY%\ROM\*.sfd") do (
	move /Y %%f "%WORKING_DIRECTORY%\ROM\Movies\"
)
cd "%WORKING_DIRECTORY%\ROM\Movies\"

for %%f in ("%WORKING_DIRECTORY%\ROM\Movies\*.sfd") do (
	echo "Converting Video Tracks: ==> %%~nxf"
	set FileNameFull=%%~nxf
	set NewFileName=%%~nf.mp4
	%WORKING_DIRECTORY%\Tools\FFMPEG\ffmpeg.exe -i !FileNameFull! -c:v libx265 -preset medium -crf 20 !NewFileName!
	REM DELETE THE ADX FILE
	del "%%f"
)
goto :eof

:PrototypeMove
REM E3 GC and XBOX PROTOTYPE SECTION
if not exist "%WORKING_DIRECTORY%\ROM\E3Proto\TitleScreen\" mkdir "%WORKING_DIRECTORY%\ROM\E3Proto\TitleScreen\"
for %%f in ("%WORKING_DIRECTORY%\ROM\*.anm") do (
	set FileName=%%~nf
	echo "Processing Proto RenderWare Maestros (Probably Titles): ==> %%~nxf"
	if /I !FileName!==title (
		if not exist "%WORKING_DIRECTORY%\ROM\E3Proto\UnusedTitleScreen\" mkdir "%WORKING_DIRECTORY%\ROM\E3Proto\UnusedTitleScreen\"
		move /Y %%f "%WORKING_DIRECTORY%\ROM\E3Proto\UnusedTitleScreen\"
		set DNM=1
	)
	if /I !FileName!==test0905 (
		if not exist "%WORKING_DIRECTORY%\ROM\E3Proto\E3XboxRenderWareMaestroTest\" mkdir "%WORKING_DIRECTORY%\ROM\E3Proto\E3XboxRenderWareMaestroTest\"
		move /Y %%f "%WORKING_DIRECTORY%\ROM\E3Proto\E3XboxRenderWareMaestroTest\"
		set DNM=1
	)
	if /I not !DNM!==1 (move %%f "%WORKING_DIRECTORY%\ROM\E3Proto\TitleScreen\")
	REM DO NOT MOVE
	set DNM=0
)
for %%f in ("%WORKING_DIRECTORY%\ROM\*.png") do (
	set Filename=%%~nf
	set UnusedTitle=!FileName:~0,9!
	set Title=!FileName:~0,5!
	echo "Processing Proto RenderWare Maestro Image Assets (Probably Titles): ==> %%~nxf"
	if /I !UnusedTitle!==title0325 (
		if not exist "%WORKING_DIRECTORY%\ROM\E3Proto\UnusedTitleScreen\" mkdir "%WORKING_DIRECTORY%\ROM\E3Proto\UnusedTitleScreen\"
		move /Y %%f "%WORKING_DIRECTORY%\ROM\E3Proto\UnusedTitleScreen\"
		set DNM=1
	)
	if /I !Title!==title (
		if /I not !UnusedTitle!==title0325 (
			if not exist "%WORKING_DIRECTORY%\ROM\E3Proto\TitleScreen\" mkdir "%WORKING_DIRECTORY%\ROM\E3Proto\TitleScreen\"
			move /Y %%f "%WORKING_DIRECTORY%\ROM\E3Proto\TitleScreen\"
			set DNM=1
		)
	)
	if /I not !DNM!==1 (move %%f "%WORKING_DIRECTORY%\ROM\E3Proto\TitleScreen\")
	set DNM=0
)
goto :eof
