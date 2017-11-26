#!/bin/bash

##ROOTMODE
#if [[ $EUID -ne 0 ]]; then
#   echo "This script should be run as root, try using sudo."
#   echo "Root may be required to make install libx265 and libfdk_aac for FFMPEG compilation"
#   exit 1
#fi

######################### #####################
######################### SONIC HEROES
######################### BUILD SYSTEM
######################### ORIGINAL - SEWER56LOL
######################### #####################

# Declare Initial Variables For Working With The ROM
WORKING_DIRECTORY="${0%/*}" ## Work from the program directory

## DEBUG - Disabled by default, used for debugging. No need to make use of this for the end user.
SETTING_ASYNC_MODE="N" ## Enables asynchronous conversion mode.
SETTING_ASYNC_DEBUG="Y" ## Enables asynchronous conversion message.

## Load Level list from Text File
readarray -t SonicHeroesLevels < "$WORKING_DIRECTORY/ResolvingDictionary/Levels"


## DECLARE SONIC HEROES ARRAYS
declare -a Languages=("English" "French" "German" "Italian" "Japanese" "Korean" "Spanish")
declare -a LanguagesShort=("E" "F" "G" "I" "J" "K" "S")
declare -a LanguageSpecificMenuAssets=("adv_2p.one" "adv_audio.one" "adv_bar.one" "adv_cg.one" "adv_cg_us.one" "adv_challenge.one" "adv_cong.one" "adv_deflicker.one" "adv_ending_a.one" "adv_ending_b.one" "adv_ending_c.one" "adv_ending_d.one" "adv_ending_e.one" "adv_ending_f.one" "adv_fileselect.one" "adv_ganma.one" "adv_mainmenu.one" "adv_option.one" "adv_pal.one" "adv_progressive.one" "adv_story.one" "adv_title.one" "adv_title_us.one")
declare -a LanguageSpecificMenuAssetsFolders=("2 Player Menu" "Audio Menu" "Bottom Menu Controls Bar" "CG Cutscene Menu" "CG Cutscene Menu/USA Version" "Challenge Menu" "Congratulations Screen" "Deflicker Menu Option" "Ending Splashes/Try Another Story" "Ending Splashes/Get 7 Chaos Emeralds" "Ending Splashes/Defeat Last Evil" "Ending Splashes/Get All A Rank" "Ending Splashes/Try Super Hard Mode" "Ending Splashes/Thank You For Playing" "Game Save Selection Menu" "Brightness Adjustment Selection Screen" "Main Menu Navigation Assets" "Options Menu" "PAL Framerate Selection" "Progressive Scan Selection Screen" "Story Mode Menu Option" "Title Screen" "Title Screen/USA Version")

declare -a TeamNames=("Chaotix" "Dark" "Rose" "Sonic")
declare -a TeamNamesShort=("C" "D" "R" "S")

## Order Sensitive
declare -a MainMenuCharacters=("Amy" "Charmy Bee" "Big" "Cream" "Espio" "Knuckles" "Omega" "Rouge" "Shadow" "Sonic" "Tails" "Vector" "All Players" "Unused~Unidentified")
declare -a MainMenuCharacterSuffix=("pl_amy.one" "pl_bee.one" "pl_big.one" "pl_cream.one" "pl_espio.one" "pl_knuckles.one" "pl_omega.one" "pl_rouge.one" "pl_shadow.one" "pl_sonic.one" "pl_tails.one" "pl_vector.one" "player.one")

declare -a MainMenuFonts=("sega.prs" "sonic_e.prs")
declare -a UnusedMenuAssets=("adv_title.one" "adv_audio.one" "adv_e3rom.one")
declare -a GeneralMainMenuAssets=("adv_bg.one" "adv_chao.one" "adv_help.one" "adv_save.one" "adv_staffroll.one" "adv_window.one" "as_emblem.one")
declare -a GeneralMainMenuAssetsFolders=("Main Menu Background Assets" "Main Menu Omochao" "Main Menu Omochao Help Icon" "Autosave Menu" "Credits Screen Staffroll" "Main Menu Window Assets" "Emblem Count Spinning Emblem")
declare -a TransitionModels=("adv_ef_warpa.dff" "adv_ef_warpb.dff" "adv_sonicoutline.dff")

## Order Sensitive
declare -a PlayerCharacters=("am" "be" "bi" "cheese" "cr" "es" "kn" "om" "ro" "sh" "so" "ta" "ve" "ss" "sk" "st" "fam" "fbe" "fcheese" "fbi" "fcr" "fes" "fkn" "fom" "fro" "fsh" "fso" "fta" "fve")
declare -a PlayerCharacterFolders=("Amy" "Charmy" "Big" "Cheese" "Cream" "Espio" "Knuckles" "Omega" "Rouge" "Shadow" "Sonic" "Tails" "Vector" "Super Sonic" "Super Knuckles" "Super Tails" "Metal Amy" "Metal Charmy" "Metal Cheese" "Metal Big" "Metal Cream" "Metal Espio" "Metal Knuckles" "Metal Omega" "Metal Rouge" "Metal Shadow" "Metal Sonic" "Metal Tails" "Metal Vector")

declare -a ActionStageFolders=("Geometry" "Custom Falco Enemy" "Stage Specific Objects/Geometry And Environment Models" "Title Card Mission Text" "Title Card" "Title Card/Extra Mission" "Title Card/Super Hard" "Collision" "Collision/Water Collision" "Collision/Death Planes" "Object Layouts" "Object Layouts/All Teams" "Object Layouts/Team Sonic" "Object Layouts/Team Dark" "Object Layouts/Team Rose" "Object Layouts/Team Chaotix" "Object Layouts/Super Hard" "Object Layouts/Decoration Layouts" "Idle Autoplay Demos" "Lighting Data" "Camera Data" "Geometry Visibility Data" "Textures/Indirectional" "Textures/Effects" "Indirectional Data" "Particle Data" "Extra SplineData" "Unknown")
declare -a StageSuffixTypes=("EX" "SH")
declare -a StageSuffixTypesFull=("Extra Mission" "Super Hard")

## Order Sensitive
declare -a EnemyNames=("Egg Albatross" "Egg Hawk" "Egg Emperor" "Klagen" "E2000" "Egg Mobile" "Egg Mobile Stage 22" "Egg Mobile Stage 25" "Falco" "Egg Magician" "Metal Madness" "Metal Overlord" "Egg Pawn" "Egg Pawn Casino Version" "Rhinoliner" "Flapper" "Cameron" "Heavy Egg Hammer")
declare -a EnemyFileNames=("_albatross" "_egghawk" "_kingpawn" "_capture" "_e2000" "_eggmobile" "_eggmobile_stg22" "_eggmobile_stg25" "_flyer" "_magician" "_metalsonic1st" "_metalsonic2nd" "_pawn" "_pawn_roulette" "_rinoliner" "_searcher" "_turtle" "_wall")
declare -a EnemyTeamIcons=("chrboss_dispC" "chrboss_dispD" "chrboss_dispR" "chrboss_disp")
declare -a CommonEnemyAssets=("en_icon" "en_common")
declare -a CommonEnemyAssetsFolders=("Common Assets/Enemy Icons" "Common Assets")

declare -a CommonSFXFiles=("GCAX.conf")
declare -a CommonSFXFolders=("Sound Effect GCAX Sound Library Configuration")
declare -a SoundFXTypesShort=("pl" "en" "cn" "vo" "sy")
declare -a SoundFXTypesShortFolders=("Player" "Enemy" "Common" "Vocal" "Unknown")
declare -a SoundFXTypesLong=("CMNGK" "VOICE" "BASIC" "CH_TS" "CH_TD" "CH_TR" "CH_TC")
declare -a SoundFXTypesLongFolders=("Common" "Vocal" "Common" "Player/Team Sonic" "Player/Team Dark" "Player/Team Rose" "Player/Team Chaotix")

#declare -a KnownTextures=("" "")
#declare -a

declare -a CompilationStrings=("TSonic.str" "TSonicD.str")

# THESE GO BY THE LANGUAGES, E.G ADV_TIMEUP IS SHARED BY ENGLISH, JAPANESE AND KOREAN
declare -a SpecialStageTimeup=("adv_timeup.dff" "adv_timeup_fr.dff" "adv_timeup_ge.dff" "adv_timeup_it.dff" "adv_timeup_sp.dff")

## CHAR_SPEECH_LIBFDK SETTINGS - MUST BE DEFINED BEFORE FORMAT
SETTING_CHAR_SPEECH_LIBFDK_PROFILE="-profile:a aac_he"
SETTING_CHAR_SPEECH_LIBFDK_VBR_LEVEL="-vbr 3"

## MUSIC_MUSIC_TRACK_FORMAT_LIBFDK SETTINGS - MUST BE DEFINED EARLY
SETTING_MUSIC_TRACK_LIBFDK_BITRATE="-b:a 128k"

## VIDEO_FORMAT_X265_SETTINGS
SETTING_VIDEO_FORMAT_X265_PRESET="-preset medium"
SETTING_VIDEO_FORMAT_X265_CRF="-crf 20"

## AUDIO PROFILES
SETTING_CHAR_SPEECH_FORMAT_LIBFDK="-c:v libfdk_aac "$SETTING_CHAR_SPEECH_LIBFDK_PROFILE" "$SETTING_CHAR_SPEECH_LIBFDK_VBR_LEVEL""
SETTING_MUSIC_TRACK_FORMAT_LIBFDK="-c:v libfdk_aac "$SETTING_MUSIC_TRACK_LIBFDK_BITRATE""
SETTING_VIDEO_FORMAT_X265="-c:v libx265 "$SETTING_VIDEO_FORMAT_X265_PRESET" "$SETTING_VIDEO_FORMAT_X265_CRF""

## Shell In-Replacement Colours For Tex
ColourReset=`tput sgr0`
ColourStandout=`tput smso`
ColourNameText=`tput setaf 3`
ColourBold=`tput bold`
ColourWarning=`tput setaf 6`
ColourInfo=`tput setaf 10`

## Shell In-Replacement for All Text Colours!
AllColourReset="tput sgr0"
AllColourBold="tput bold"
AllColourWarning="tput setaf 6"
AllColourInfo="tput setaf 10"

##Do not parse no findings as a filename in for loops
shopt -s nullglob

# Create the Linux Directory on Startup if it does not exist.
if [ ! -d "$WORKING_DIRECTORY/Linux/" ]; then
	mkdir "$WORKING_DIRECTORY/Linux/"
fi

######################### #########################
######################### GAME MODIFICATION METHODS
######################### #########################

AFSDec () {
	## $1 = Utilitydir, $2 = InFile, #3 = FileName (echo)
	wine "$1" "$2" &> /dev/null
	echo "==> $3 (DONE)"
	rm -rf "$2"
}

PRSDec () {
	## $1 = Utilitydir, $2 = InFile, #3 = Outfile #4 = EchoName
	wine "$1" "$2" "$3" &> /dev/null
	echo "==> $4 (DONE)"
	rm -rf "$2"
}

Decompress_Heroes_Sound_Clips () {
	echo "${ColourWarning}Unpacking AFS files in $WORKING_DIRECTORY/ROM/${ColourReset}"
	for AFSSounds in "$WORKING_DIRECTORY/ROM/"*.afs
	do
		local FileNameFull=${AFSSounds##*/}
		local FileName=${FileNameFull%%.*}

		echo "==> $FileName.afs"

		## Make Directory in ROM Folder
		if [ ! -d "$WORKING_DIRECTORY/ROM/Character Speech/$FileName/" ]; then mkdir -p "$WORKING_DIRECTORY/ROM/Character Speech/$FileName/"; fi

		## Move to new directory
		mv "$AFSSounds" "$WORKING_DIRECTORY/ROM/Character Speech/$FileName/"
		cd "$WORKING_DIRECTORY/ROM/Character Speech/$FileName/"

		## Call UNAFS and extract the ADX
		AFSDec "$WORKING_DIRECTORY/Tools/UnAFS/unafs.exe" "$WORKING_DIRECTORY/ROM/Character Speech/$FileName/$FileName.afs" "$FileName.afs" &
	done

	wait
	## NOTE : DOUBLE CONVERSION IS TAKING PLACE, PROCESS MUST FINISH BEFORE CONVERTING TO MP4, USE WAIT BEFORE DOUBLE CONVERSION
	## NOTE : DO NOT REPLACE WITH ASYNC MODE MESSAGE

}

Async_Mode_Message() {
	if [ $SETTING_ASYNC_DEBUG == "Y" ]; then
		if [ $SETTING_ASYNC_MODE == "Y" ]; then
			echo "${ColourStandout}IN ASYNC MODE!${ColourReset}"
		else
			echo "NO ASYNC, WAITING"
			wait
		fi
	fi
}

Heroes_FFMPEG_Convert() {
	## $1 = Conversion Type/Message
	## $2 = Directory to convert
  ## $3 = Conversion Format Video/Music - FFMPEG
	## $4 = File Extension Output
	## $5 = File Extension Input
	echo "${ColourWarning}$1${ColourReset}"
	for HeroesInput in "$2"*"$5"
	do
		local FileNameFull=${HeroesInput##*/}
		local FilePath=${HeroesInput%/*}
		local FileName=${FileNameFull%%.*}

		## I SPENT 3 HOURS HERE TO FIND OUT I FORGOT THE -o PARAM INTO FFMPEG WHEN USING WHITESPACE AND QUOTING IN THE MANNER I AM QUOTING IN

		echo "==> $FileNameFull"
		"$WORKING_DIRECTORY"/Linux/bin/ffmpeg -i "${HeroesInput}" $3 "${FilePath}/${FileName}${4}" &> /dev/null &
	done
}

Convert_Heroes_Sound_Clips () {
	## Convert all adx in each directory with decompressed AFS files.
	for ADXDirectory in "$WORKING_DIRECTORY/ROM/Character Speech/"*
	do
		Heroes_FFMPEG_Convert "Converting Character Speech in $ADXDirectory/" "$ADXDirectory/" "$SETTING_CHAR_SPEECH_FORMAT_LIBFDK" ".mp4" ".adx" &
	done
}

Convert_Heroes_Music () {
	## Create the directory if it does not exist.
	if [ ! -d "$WORKING_DIRECTORY/ROM/Background Music/" ]; then
		mkdir -p "$WORKING_DIRECTORY/ROM/Background Music/"
	fi
	for ADXSong in "$WORKING_DIRECTORY/ROM/"*.adx
	do
		mv "$ADXSong" "$WORKING_DIRECTORY/ROM/Background Music/"
	done
	Heroes_FFMPEG_Convert "Converting Music Files in $WORKING_DIRECTORY/ROM/Background Music/" "$WORKING_DIRECTORY/ROM/Background Music/" "$SETTING_MUSIC_TRACK_FORMAT_LIBFDK" ".mp4" ".adx" &
}

Convert_Heroes_Video () {
	if [ ! -d "$WORKING_DIRECTORY/ROM/Movies/" ]; then
		mkdir -p "$WORKING_DIRECTORY/ROM/Movies/"
	fi
	for SFDMovie in "$WORKING_DIRECTORY/ROM/"*.sfd
	do
		mv "$SFDMovie" "$WORKING_DIRECTORY/ROM/Movies/"
	done
	Heroes_FFMPEG_Convert "Converting Video Files in $WORKING_DIRECTORY/ROM/Movies/" "$WORKING_DIRECTORY/ROM/Movies/" "$SETTING_VIDEO_FORMAT_X265" ".mp4" ".sfd" &
}

Handle_Heroes_Advertise () {
	echo "${ColourWarning}Moving and Parsing All Menu Items in $WORKING_DIRECTORY/ROM/advertise/${ColourReset}"
	## Get Those Main Menu Characters Sorted!
	if [ ! -d "$WORKING_DIRECTORY/ROM/Game Menus/Main Menu Characters/" ]; then
		mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Main Menu Characters/"
	fi

	for ((x=0; x<"${#MainMenuCharacters[@]}"; x++))
	do
		if [ -f "$WORKING_DIRECTORY/ROM/advertise/adv_${MainMenuCharacterSuffix[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Main Menu Characters/${MainMenuCharacters[x]}"
			echo "==> Processing Main Menu Character: ${MainMenuCharacters[x]}"
			mv "$WORKING_DIRECTORY/ROM/advertise/adv_${MainMenuCharacterSuffix[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Main Menu Characters/${MainMenuCharacters[x]}"
		fi
  done

	## Test for any other potential players :)
	for TestPlayer in "$WORKING_DIRECTORY/ROM/advertise/adv_pl_"*".one"
	do
		mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Main Menu Characters/Unused~Unidentified"
		echo "==> Processing Unidentified Menu Character: ${TestPlayer##*/}"
		mv "$TestPlayer" "$WORKING_DIRECTORY/ROM/Game Menus/Main Menu Characters/Unused~Unidentified"
	done

	## Move all localized assets to their respective languages from Array.
	for ((x=0; x<${#LanguagesShort[@]}; x++))
	do
		if [ -d "$WORKING_DIRECTORY/ROM/advertise/${LanguagesShort[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Localized Menu Assets/${Languages[x]}"
			for AdvertiseFile in "$WORKING_DIRECTORY/ROM/advertise/${LanguagesShort[x]}"/*
			do
				mv "$AdvertiseFile" "$WORKING_DIRECTORY/ROM/Game Menus/Localized Menu Assets/${Languages[x]}/${AdvertiseFile##*/}"
			done
		fi
	done

	## Sort out the Main Menu Generic Fonts
	for ((x=0; x<${#MainMenuFonts[@]}; x++))
	do
		if [ -f "$WORKING_DIRECTORY/ROM/advertise/${MainMenuFonts[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Fonts/"
			mv "$WORKING_DIRECTORY/ROM/advertise/${MainMenuFonts[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Fonts/"
			PRSDec "$WORKING_DIRECTORY/Tools/PrsDec/PrsDec.exe" "$WORKING_DIRECTORY/ROM/Game Menus/Fonts/${MainMenuFonts[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Fonts/${MainMenuFonts[x]}.bin" "${MainMenuFonts[x]}.bin" &
		fi
	done

	## Do Unused Menu items - BAD TASTE IN CODE BUT DOES THE JOB - Torvalds pls no kill.
	for ((x=0; x<${#UnusedMenuAssets[@]}; x++))
	do
		if [ ${UnusedMenuAssets[x]} == "adv_title.one" ] && [ -f "$WORKING_DIRECTORY/ROM/advertise/${UnusedMenuAssets[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Unused/Prototypes E3-10.8 TitleScreen"
			mv "$WORKING_DIRECTORY/ROM/advertise/${UnusedMenuAssets[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Unused/Prototypes E3-10.8 TitleScreen"
			continue
		fi
		if [ ${UnusedMenuAssets[x]} == "adv_audio.one" ] && [ -f "$WORKING_DIRECTORY/ROM/advertise/${UnusedMenuAssets[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Unused/Pre 10.8 Proto Audio Menu"
			mv "$WORKING_DIRECTORY/ROM/advertise/${UnusedMenuAssets[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Unused/Pre 10.8 Proto Audio Menu"
			continue
		fi
		if [ ${UnusedMenuAssets[x]} == "adv_e3rom.one" ] && [ -f "$WORKING_DIRECTORY/ROM/advertise/${UnusedMenuAssets[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Unused/E3 Build Main Menu Assets"
			mv "$WORKING_DIRECTORY/ROM/advertise/${UnusedMenuAssets[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Unused/E3 Build Main Menu Assets"
			continue
		fi
	done

	## Do Unused Menu items - MORE BAD TASTE IN CODE BUT DOES THE JOB - This time Readability > Efficiency - Torvalds pls no kill.
	for MenuAsset in $WORKING_DIRECTORY/ROM/advertise/*
	do
		#mv "$MenuAsset" "$WORKING_DIRECTORY/ROM/Game Menus/"
		local FileNameFull=${MenuAsset##*/}
		if [ "$FileNameFull" == "adv_title.one" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Unused/Prototypes E3-10.8 TitleScreen"
			mv "$WORKING_DIRECTORY/ROM/advertise/${UnusedMenuAssets[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Unused/Prototypes E3-10.8 TitleScreen"
			continue
		fi
	done
}

Sort_Heroes_Other_Menu_Items () {
	echo "${ColourWarning}Moving and Parsing All Menu Related Items in $WORKING_DIRECTORY/ROM/${ColourReset}"
	## Move all menu items based on array contents.
	for ((x=0; x<${#SpecialStageTimeup[@]}; x++)); do
		if [ -f "$WORKING_DIRECTORY/ROM/${SpecialStageTimeup[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Special Stage Time Up/${Languages[x]}"
			mv "$WORKING_DIRECTORY/ROM/${SpecialStageTimeup[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Special Stage Time Up/${Languages[x]}"
		fi
	done

	## Move all transition models across based on array contents.
	mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Menu Transition Models"
	for ((x=0; x<${#TransitionModels[@]}; x++)); do
		if [ -f "$WORKING_DIRECTORY/ROM/${TransitionModels[x]}" ]; then
			mv "$WORKING_DIRECTORY/ROM/${TransitionModels[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Menu Transition Models/${TransitionModel##*/}"
		fi
	done

	## Move all general main menu text.
	mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/Menu Text/Credits Text"
	mv "$WORKING_DIRECTORY/ROM/text/"* "$WORKING_DIRECTORY/ROM/Game Menus/Menu Text" &> /dev/null
	mv "$WORKING_DIRECTORY/ROM/Game Menus/Menu Text/staffroll.csv" "$WORKING_DIRECTORY/ROM/Game Menus/Menu Text/Credits Text" &> /dev/null

	for ((x=0; x<${#TransitionModels[@]}; x++)); do
		if [ -f "$WORKING_DIRECTORY/ROM/${TransitionModels[x]}" ]; then
			mv "$WORKING_DIRECTORY/ROM/${TransitionModels[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/Menu Transition Models/${TransitionModel##*/}"
		fi
	done

	## Group General Main Menu Assets
	mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/General Assets"
	for GeneralAsset in "$WORKING_DIRECTORY/ROM/advertise/"adv_*; do
		mv "$GeneralAsset" "$WORKING_DIRECTORY/ROM/Game Menus/General Assets/${GeneralAsset##*/}"
	done
	for GeneralAsset in "$WORKING_DIRECTORY/ROM/advertise/"as_*; do
		mv "$GeneralAsset" "$WORKING_DIRECTORY/ROM/Game Menus/General Assets/${GeneralAsset##*/}"
	done
}

Parse_Advertise_Assets () {
	## Parse the General Main Menu Assets to each own separate folders.
	for ((x=0; x<${#GeneralMainMenuAssets[@]}; x++)); do
		if [ -f "$WORKING_DIRECTORY/ROM/Game Menus/General Assets/${GeneralMainMenuAssets[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Menus/General Assets/${GeneralMainMenuAssetsFolders[x]}"
			mv "$WORKING_DIRECTORY/ROM/Game Menus/General Assets/${GeneralMainMenuAssets[x]}" "$WORKING_DIRECTORY/ROM/Game Menus/General Assets/${GeneralMainMenuAssetsFolders[x]}"
		fi
	done

	## Parse the language dependent assets which vary with each display language
	for LanguageDirectory in "$WORKING_DIRECTORY/ROM//Game Menus/Localized Menu Assets/"*;
	do
		for ((x=0; x<${#LanguageSpecificMenuAssets[@]}; x++)); do
			if [ -f "$LanguageDirectory/${LanguageSpecificMenuAssets[x]}" ]; then
				mkdir -p "$LanguageDirectory/${LanguageSpecificMenuAssetsFolders[x]}"
				mv "$LanguageDirectory/${LanguageSpecificMenuAssets[x]}" "$LanguageDirectory/${LanguageSpecificMenuAssetsFolders[x]}"
			fi
		done
	done
}

Sort_GameCube_Game_Code () {
	## Make directory for storing game code
	if [ ! -d "$WORKING_DIRECTORY/ROM/Game Code/GameCube ISO Stuff" ]; then
		mkdir -p "$WORKING_DIRECTORY/ROM/Game Code/GameCube ISO Stuff"
	fi

	## Move all .dol Game Code
	mv "$WORKING_DIRECTORY/ROM/&&systemdata/"*".dol" "$WORKING_DIRECTORY/ROM/Game Code/" &> /dev/null

	## Move Everything Else Related to Boot
	mv "$WORKING_DIRECTORY/ROM/&&systemdata/"* "$WORKING_DIRECTORY/ROM/Game Code/GameCube ISO Stuff" &> /dev/null

	##
	## Handle GameCube Relocatable Module Files
	##

	if [ ! -d "$WORKING_DIRECTORY/ROM/Game Code/Relocatable Module Files" ]; then
		mkdir -p "$WORKING_DIRECTORY/ROM/Game Code/Relocatable Module Files/Stage Specific Code"
		mkdir -p "$WORKING_DIRECTORY/ROM/Game Code/Relocatable Module Files/General Function Code"
	fi

	## Iterate over every level, sort levels.
	for StageREL in "$WORKING_DIRECTORY/ROM/"stage*".rel"; do
		StageName=${StageREL##*/}
		StageID=$((10#${StageName:5:2}))
		mkdir -p "$WORKING_DIRECTORY/ROM/Game Code/Relocatable Module Files/Stage Specific Code/${SonicHeroesLevels[$StageID]}"
		mv "$StageREL" "$WORKING_DIRECTORY/ROM/Game Code/Relocatable Module Files/Stage Specific Code/${SonicHeroesLevels[$StageID]}"
	done

	## Move the remainment of the code
	for REL in "$WORKING_DIRECTORY/ROM/"*".rel"; do
		mv "$REL" "$WORKING_DIRECTORY/ROM/Game Code/Relocatable Module Files/General Function Code/"
	done
}

Sort_Generic_Game_Code () {
	## Make directory for storing game code
	if [ ! -d "$WORKING_DIRECTORY/ROM/Game Code/" ]; then
		mkdir -p "$WORKING_DIRECTORY/ROM/Game Code/"
	fi

	## Parse Generic Game Code Stuff
	for ((x=0; x<${#CompilationStrings[@]}; x++));
	do
		if [ ${CompilationStrings[x]} == "TSonic.str" ] && [ -f "$WORKING_DIRECTORY/ROM/${CompilationStrings[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Code/Game Compilation Strings/"
			mv "$WORKING_DIRECTORY/ROM/${CompilationStrings[x]}" "$WORKING_DIRECTORY/ROM/Game Code/Game Compilation Strings/${CompilationStrings[x]}"
			continue
		fi
		if [ ${CompilationStrings[x]} == "TSonicD.str" ] && [ -f "$WORKING_DIRECTORY/ROM/${CompilationStrings[x]}" ]; then
			mkdir -p "$WORKING_DIRECTORY/ROM/Game Code/Game Compilation Strings/Debug"
			mv "$WORKING_DIRECTORY/ROM/${CompilationStrings[x]}" "$WORKING_DIRECTORY/ROM/Game Code/Game Compilation Strings/Debug/${CompilationStrings[x]}"
			continue
		fi
	done
}

Handle_Heroes_Events () {
	## Make directory for events to be stored
	if [ ! -d "$WORKING_DIRECTORY/ROM/Events/" ]; then
		mkdir -p "$WORKING_DIRECTORY/ROM/Events/TeamSonicEvents"
		mkdir -p "$WORKING_DIRECTORY/ROM/Events/TeamDarkEvents"
		mkdir -p "$WORKING_DIRECTORY/ROM/Events/TeamRoseEvents"
		mkdir -p "$WORKING_DIRECTORY/ROM/Events/TeamChaotixEvents"
		mkdir -p "$WORKING_DIRECTORY/ROM/Events/LastStoryEvents"
		mkdir -p "$WORKING_DIRECTORY/ROM/Events/Other~UnusedEvents"
	fi

	for HeroesEvent in "$WORKING_DIRECTORY/ROM/event00"*; do mv "$HeroesEvent" "$WORKING_DIRECTORY/ROM/Events/TeamSonicEvents"; done
	for HeroesEvent in "$WORKING_DIRECTORY/ROM/event01"*; do mv "$HeroesEvent" "$WORKING_DIRECTORY/ROM/Events/TeamDarkEvents"; done
	for HeroesEvent in "$WORKING_DIRECTORY/ROM/event02"*; do mv "$HeroesEvent" "$WORKING_DIRECTORY/ROM/Events/TeamRoseEvents"; done
	for HeroesEvent in "$WORKING_DIRECTORY/ROM/event03"*; do mv "$HeroesEvent" "$WORKING_DIRECTORY/ROM/Events/TeamChaotixEvents"; done
	for HeroesEvent in "$WORKING_DIRECTORY/ROM/event04"*; do mv "$HeroesEvent" "$WORKING_DIRECTORY/ROM/Events/LastStoryEvents"; done
	for HeroesEvent in "$WORKING_DIRECTORY/ROM/event"*; do mv "$HeroesEvent" "$WORKING_DIRECTORY/ROM/Events/Other~UnusedEvents"; done

	## Should probably use this: find . -maxdepth 1 ! -type d
	## Fix this later pl0x m9
	for EventDescriptorDirectory in "$WORKING_DIRECTORY/ROM/Events/"*"/"; do
		for Event in "$EventDescriptorDirectory"*; do
			EventFile=${Event##*/}
			FileID=${EventFile:0:9}
			if [ ! -d "$EventDescriptorDirectory""$FileID" ]; then mkdir -p "$EventDescriptorDirectory""$FileID"; fi
			mv "$Event" "$EventDescriptorDirectory""$FileID" &> /dev/null
		done
	done
}

Handle_Heroes_Characters () {
	mv "$WORKING_DIRECTORY/ROM/playmodel/" "$WORKING_DIRECTORY/ROM/Player Models/" &> /dev/null

	## Create appropriate Folders For Players
	for ((x=0; x<${#PlayerCharacterFolders[@]}; x++)); do
		mkdir "$WORKING_DIRECTORY/ROM/Player Models/${PlayerCharacterFolders[x]}" &> /dev/null
	done
	## Move all players to appropriate Folders
	for ((x=0; x<${#PlayerCharacterFolders[@]}; x++)); do
		for CharacterFile in "$WORKING_DIRECTORY/ROM/Player Models/""${PlayerCharacters[x]}"*; do
			mv "$CharacterFile" "$WORKING_DIRECTORY/ROM/Player Models/${PlayerCharacterFolders[x]}"
		done
	done
}

Handle_Heroes_Stages () {
	## Make over 9000 directories for stages.
	if [ ! -d "$WORKING_DIRECTORY/ROM/Action Stages/" ]; then
		mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Levels"
		mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Other/SET ID Table"
		mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Other/Common Particle Data"
		mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Common Stage Objects"
		mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Unused/Common Stage Objects"
	fi

	## Make all action stage folders
	for ((x=0; x<${#SonicHeroesLevels[@]}; x++)) do
		## Make a folder for each stage
		mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Levels/${SonicHeroesLevels[x]}"

		## For every folder in the created stage folder
		for ((y=0; y<${#ActionStageFolders[@]}; y++)) do
			## Create the folder
			mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Levels/${SonicHeroesLevels[x]}/${ActionStageFolders[y]}"
		done

		##
		## Create Folders for Title Card Mission Text
		##

		## For the action stage folder, create the directory structure for the title cards from array (to avoid clutter of previous array)
		for ((z=0; z<"${#Languages[@]}"; z++)); do
				## Gets you to the folder, then makes the directories.
				mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Levels/${SonicHeroesLevels[x]}/Title Card Mission Text/${Languages[z]}/"
		done

		for ((a=0; a<"${#TeamNames[@]}"; a++)); do
			for ((b=0; b<"${#Languages[@]}"; b++)); do
				mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Levels/${SonicHeroesLevels[x]}/Title Card Mission Text/${TeamNames[a]}/${Languages[b]}/"
				mkdir -p "$WORKING_DIRECTORY/ROM/Action Stages/Levels/${SonicHeroesLevels[x]}/Title Card Mission Text/${TeamNames[a]}/Extra Mission/${Languages[b]}/"
			done
		done
	done

	## Resolve Sonic Heroes Stage Data - sXX format

	for ActionStageData in "$WORKING_DIRECTORY/ROM/"[sS][0-9][0-9]*; do
		FullFileName="${ActionStageData##*/}";
		FileName=${FullFileName%.*}
		LevelID=${FullFileName:1:2}

		if [[ $FileName =~ "s"[0-9][0-9]"obj" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Stage Specific Objects/"
	elif [[ $FileName =~ "s"[0-9][0-9]"MRG" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Stage Specific Objects/Geometry And Environment Models/"
elif [[ $FileName =~ "s"[0-9][0-9]"_flyer" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Custom Falco Enemy/"
		elif [[ $FileName =~ "s"[0-9][0-9]"_PB" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/All Teams/"
	elif [[ $FileName =~ "s"[0-9][0-9]"_P1" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Team Sonic/"
elif [[ $FileName =~ "s"[0-9][0-9]"_P2" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Team Dark/"
		elif [[ $FileName =~ "s"[0-9][0-9]"_P3" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Team Rose/"
	elif [[ $FileName =~ "s"[0-9][0-9]"_P4" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Team Chaotix/"
elif [[ $FileName =~ "s"[0-9][0-9]"_P5" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Super Hard/"
		elif [[ $FileName =~ "s"[0-9][0-9]"_DB" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Decoration Layouts/"
	elif [[ $FileName =~ "s"[0-9][0-9]"_light" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Lighting Data/"
elif [[ $FileName =~ "s"[0-9][0-9]"_cam" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Camera Data/"
		elif [[ $FileName =~ "s"[0-9][0-9]"_blk" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Geometry Visibility Data/"
	elif [[ $FileName =~ "s"[0-9][0-9]"_indinfo" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Indirectional Data/"
elif [[ $FileName =~ "s"[0-9][0-9]"_ptcl" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Particle Data/"
		elif [[ $FileName =~ "s"[0-9][0-9]"_ptclplay" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Particle Data/"
	elif [[ $FullFileName = *".txc" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Textures/"
elif [[ $FullFileName = *".spl" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Extra SplineData"
		elif [[ $FullFileName = *".dmo" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Idle Autoplay Demos/"
	elif [[ $FullFileName = *".one" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Geometry/"
else mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Unknown/"
		fi
	done

	## Resolve Sonic Heroes Stage Data - stgXX format
	for ActionStageData in "$WORKING_DIRECTORY/ROM/"[sS][tT][gG][0-9][0-9]*; do
		FullFileName="${ActionStageData##*/}";
		FileName=${FullFileName%.*}
		LevelID=${FullFileName:3:2}

		if [[ $FileName =~ "stg"[0-9][0-9]"obj" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Stage Specific Objects/"
	elif [[ $FileName =~ "stg"[0-9][0-9]"MRG" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Stage Specific Objects/Geometry And Environment Models/"
elif [[ $FileName =~ "stg"[0-9][0-9]"_flyer" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Custom Falco Enemy/"
		elif [[ $FileName =~ "stg"[0-9][0-9]"_PB" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/All Teams/"
	elif [[ $FileName =~ "stg"[0-9][0-9]"_P1" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Team Sonic/"
elif [[ $FileName =~ "stg"[0-9][0-9]"_P2" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Team Dark/"
		elif [[ $FileName =~ "stg"[0-9][0-9]"_P3" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Team Rose/"
	elif [[ $FileName =~ "stg"[0-9][0-9]"_P4" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Team Chaotix/"
elif [[ $FileName =~ "stg"[0-9][0-9]"_P5" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Super Hard/"
		elif [[ $FileName =~ "stg"[0-9][0-9]"_DB" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Object Layouts/Decoration Layouts/"
	elif [[ $FileName =~ "stg"[0-9][0-9]"_light" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Lighting Data/"
elif [[ $FileName =~ "stg"[0-9][0-9]"_cam" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Camera Data/"
		elif [[ $FileName =~ "stg"[0-9][0-9]"_blk" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Geometry Visibility Data/"
	elif [[ $FileName =~ "stg"[0-9][0-9]"_indinfo" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Indirectional Data/"
elif [[ $FileName =~ "stg"[0-9][0-9]"_ptcl" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Particle Data/"
		elif [[ $FileName =~ "stg"[0-9][0-9]"_ptclplay" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Particle Data/"
	elif [[ $FullFileName = *".txc" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Textures/"
elif [[ $FullFileName = *".spl" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Extra SplineData"
		elif [[ $FullFileName = *".dmo" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Idle Autoplay Demos/"
	elif [[ $FullFileName = *".one" ]]; then mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Geometry/"
else mv "$ActionStageData" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Unknown/"
		fi
	done

	## Resolve Sonic Heroes Stage Textures - sXX format
	for ActionStageTextures in "$WORKING_DIRECTORY/ROM/textures/"[sS][0-9][0-9]*; do
		FullFileName="${ActionStageTextures##*/}";
		FileName=${FullFileName%.*}
		LevelID=${FullFileName:1:2}
	if [[ $FileName =~ "s"[0-9][0-9]"_effect" ]]; then mv "$ActionStageTextures" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Textures/Effects/"
	elif [[ $FileName =~ "s"[0-9][0-9]"_indirect" ]]; then mv "$ActionStageTextures" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Textures/Indirectional/"
	elif [[ $FullFileName = *".txd" ]]; then mv "$ActionStageTextures" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Textures/"
	fi
	done

	## Resolve Sonic Heroes Stage Textures - stgXX format
	for ActionStageTextures in "$WORKING_DIRECTORY/ROM/textures/"[sS][tT][gG][0-9][0-9]*; do
		FullFileName="${ActionStageTextures##*/}";
		FileName=${FullFileName%.*}
		LevelID=${FullFileName:3:2}
	if [[ $FileName =~ "stg"[0-9][0-9]"_effect" ]]; then mv "$ActionStageTextures" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Textures/Effects/"
	elif [[ $FileName =~ "stg"[0-9][0-9]"_indirect" ]]; then mv "$ActionStageTextures" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Textures/Indirectional/"
	elif [[ $FullFileName = *".txd" ]]; then mv "$ActionStageTextures" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Textures/"
	fi
	done

	## Resolve Sonic Heroes Stage Collision - sXX format
	for ActionStageCollisions in "$WORKING_DIRECTORY/ROM/collisions/"[sS][0-9][0-9]*; do
		FullFileName="${ActionStageCollisions##*/}";
		FileName=${FullFileName%.*}
		LevelID=${FullFileName:1:2}
	if [[ $FileName =~ "s"[0-9][0-9]"_wt" ]]; then mv "$ActionStageCollisions" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Collision/Water Collision/"
	elif [[ $FileName =~ "s"[0-9][0-9]"_xx" ]]; then mv "$ActionStageCollisions" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Collision/Death Planes/"
elif [[ $FullFileName = *".cl" ]]; then mv "$ActionStageCollisions" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Collision/"
	fi
	done
	## Resolve Sonic Heroes Stage Collision - stgXX format
	for ActionStageCollisions in "$WORKING_DIRECTORY/ROM/collisions/"[sS][tT][gG][0-9][0-9]*; do
		FullFileName="${ActionStageCollisions##*/}";
		FileName=${FullFileName%.*}
		LevelID=${FullFileName:3:2}
	if [[ $FileName =~ "stg"[0-9][0-9]"_wt" ]]; then mv "$ActionStageCollisions" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Collision/Water Collision/"
elif [[ $FileName =~ "stg"[0-9][0-9]"_xx" ]]; then mv "$ActionStageCollisions" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Collision/Death Planes/"
	elif [[ $FullFileName = *".cl" ]]; then mv "$ActionStageCollisions" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Collision/"
	fi
	done

	## Resolve Sonic Heroes Title Card - stgXX format - FOR LEVELS
	for ActionStageTitleCard in "$WORKING_DIRECTORY/ROM/stgtitle/"[sS][tT][gG][0-9][0-9]*; do
		FullFileName="${ActionStageTitleCard##*/}";
		FileName=${FullFileName%.*}
		LevelID=${FullFileName:3:2}

		for ((x=0; x<${#StageSuffixTypes[@]}; x++)); do
			if [[ $FileName =~ "stg"[0-9][0-9]"title_disp${StageSuffixTypes[x]}" ]]; then mv "$ActionStageTitleCard" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Title Card/${StageSuffixTypesFull[x]}/"; fi
		done
		if [[ $FileName =~ "stg"[0-9][0-9]"title_disp"* ]] && [[ -f "$ActionStageTitleCard" ]]; then mv "$ActionStageTitleCard" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Title Card/"; fi
	done

	## Resolve Sonic Heroes Title Card Mission Text - stgXX format
	for ActionStageMissionText in "$WORKING_DIRECTORY/ROM/stgtitle/mission/"[sS][tT][gG][0-9][0-9]*; do
		FullFileName="${ActionStageMissionText##*/}";
		FileName=${FullFileName%.*}
		LevelID=${FullFileName:3:2}

		## Parse for 8 character format, only stage ID and language - used for Generic + Special Stages.
		if [[ ${#FileName} =~ "8" ]]; then FileNameLanguage=${FileName:5:1};
			for ((x=0; x<${#Languages[@]}; x++)); do
				if [[ ${LanguagesShort[x]} == $FileNameLanguage ]]; then mv "$ActionStageMissionText" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Title Card Mission Text/${Languages[x]}/"; fi
			done
		fi

		## Parse for 9 character format, Stage ID, Team, Language - used for Regular Stages.
		if [[ ${#FileName} =~ "9" ]]; then FileNameLanguage=${FileName:6:1}; FileNameTeam=${FileName:5:1};
			for ((t=0; t<${#TeamNamesShort[@]}; t++)); do
				if [[ ${TeamNamesShort[t]} == $FileNameTeam ]]; then
					for ((x=0; x<${#Languages[@]}; x++)); do
						if [[ ${LanguagesShort[x]} == $FileNameLanguage ]]; then mv "$ActionStageMissionText" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Title Card Mission Text/${TeamNames[t]}/${Languages[x]}/"; fi
					done
				fi
			done
		fi

		## Parse for 11 character format, Stage ID, Team, Extra Mission and language - used for Extra Missions only.
		if [[ ${#FileName} =~ "11" ]]; then FileNameLanguage=${FileName:8:1}; FileNameTeam=${FileName:5:1};
			for ((t=0; t<${#TeamNamesShort[@]}; t++)); do
				if [[ ${TeamNamesShort[t]} == $FileNameTeam ]]; then
					for ((x=0; x<${#Languages[@]}; x++)); do
						if [[ ${LanguagesShort[x]} == $FileNameLanguage ]]; then mv "$ActionStageMissionText" "$WORKING_DIRECTORY/ROM/Action Stages/Levels/Stage ${LevelID}"*"/Title Card Mission Text/${TeamNames[t]}/Extra Mission/${Languages[x]}/" ; fi
					done
				fi
			done
		fi

	done

	## Generate directories for all generic title Cards
	for ((x=0; x<${#Languages[@]}; x++)); do
		for ((y=0; y<${#StageSuffixTypes[@]}; y++)); do
			mkdir -p "${WORKING_DIRECTORY}/ROM/Action Stages/Levels/Stage XX - Common Assets/Generic Stage Title Assets/${Languages[x]}/${StageSuffixTypesFull[y]}/"
		done
	done


	## Handle Remaining Title Cards - Generic Title Cards
	for ActionStageGenericTitleCard in "$WORKING_DIRECTORY/ROM/stgtitle/"[sS][tT][gG]*; do
		FullFileName="${ActionStageGenericTitleCard##*/}";
		FileName=${FullFileName%.*}


		STGTitleIdentifier=${FullFileName:3:5}

		if [[ ${STGTitleIdentifier} = "title" ]]; then Language=${FullFileName:13:1}; TitleCardType=${FileName:(-2):22};
			for ((x=0; x<${#LanguagesShort[@]}; x++)); do
				if [[ ${LanguagesShort[x]} == ${Language} ]]; then
					for ((y=0; y<${#StageSuffixTypes[@]}; y++)); do
						if [[ ${StageSuffixTypes[y]} == $TitleCardType ]]; then mv "$ActionStageGenericTitleCard" "${WORKING_DIRECTORY}/ROM/Action Stages/Levels/Stage XX - Common Assets/Generic Stage Title Assets/${Languages[x]}/${StageSuffixTypesFull[y]}/"; fi
					done
					## Bailout in case there is no match for Super Hard or Extra Stage, 1/3 chance.
					if [[ -f "$ActionStageGenericTitleCard" ]]; then mv "$ActionStageGenericTitleCard" "${WORKING_DIRECTORY}/ROM/Action Stages/Levels/Stage XX - Common Assets/Generic Stage Title Assets/${Languages[x]}/${StageSuffixTypesFull[y]}/"; fi
				fi
			done
		fi

		## If not that kind of title then:

		## Handle Boss InGame Title Text, technically not title card but new loop is inefficient
		if [[ ! ${STGTitleIdentifier} = "title" ]]; then Language=${FullFileName:(-1):1}; CutEndExpansion=${FileName%title*}; Team=${CutEndExpansion:3};
			shopt -s nocasematch
			for ((x=0; x<${#TeamNames[@]}; x++)); do
				for ((y=0; y<${#LanguagesShort[@]}; y++)); do
					mkdir -p "${WORKING_DIRECTORY}/ROM/Action Stages/Levels/Stage XX - Common Assets/Boss Stage Title Assets/${Languages[y]}/${TeamNames[x]}/"
					if [[ ${LanguagesShort[y]} = ${Language} ]] && [[ ${TeamNames[x]} = $Team ]]; then mv "$ActionStageGenericTitleCard" "${WORKING_DIRECTORY}/ROM/Action Stages/Levels/Stage XX - Common Assets/Boss Stage Title Assets/${Languages[y]}/${TeamNames[x]}/"; fi
				done
			done
			shopt -u nocasematch
		fi

	done
}

Handle_Heroes_Enemies(){
	mkdir -p "$WORKING_DIRECTORY/ROM/Enemies/Common Assets/Enemy Icons"
	mkdir -p "$WORKING_DIRECTORY/ROM/Enemies/Enemies"

	## Move all regular enemy textures
	for ((x=0; x<${#EnemyNames[@]}; x++)); do
		mkdir -p "$WORKING_DIRECTORY/ROM/Enemies/Enemies/${EnemyNames[x]}/Textures From Folder";
		mkdir -p "$WORKING_DIRECTORY/ROM/Enemies/Enemies/${EnemyNames[x]}/Textures From Root";
		if [[ -f "$WORKING_DIRECTORY/ROM/en${EnemyFileNames[x]}.one" ]]; then mv "$WORKING_DIRECTORY/ROM/en${EnemyFileNames[x]}.one" "$WORKING_DIRECTORY/ROM/Enemies/Enemies/${EnemyNames[x]}/"; fi
		if [[ -f "$WORKING_DIRECTORY/ROM/bs${EnemyFileNames[x]}.one" ]]; then mv "$WORKING_DIRECTORY/ROM/bs${EnemyFileNames[x]}.one" "$WORKING_DIRECTORY/ROM/Enemies/Enemies/${EnemyNames[x]}/"; fi
		if [[ -f "$WORKING_DIRECTORY/ROM/textures/bs${EnemyFileNames[x]}.txd" ]]; then mv "$WORKING_DIRECTORY/ROM/textures/bs${EnemyFileNames[x]}.txd" "$WORKING_DIRECTORY/ROM/Enemies/Enemies/${EnemyNames[x]}/Textures From Folder"; fi
		if [[ -f "$WORKING_DIRECTORY/ROM/textures/en${EnemyFileNames[x]}.txd" ]]; then mv "$WORKING_DIRECTORY/ROM/textures/en${EnemyFileNames[x]}.txd" "$WORKING_DIRECTORY/ROM/Enemies/Enemies/${EnemyNames[x]}/Textures From Folder"; fi
		if [[ -f "$WORKING_DIRECTORY/ROM/en${EnemyFileNames[x]}.txd" ]]; then mv "$WORKING_DIRECTORY/ROM/en${EnemyFileNames[x]}.txd" "$WORKING_DIRECTORY/ROM/Enemies/Enemies/${EnemyNames[x]}/Textures From Root"; fi
		if [[ -f "$WORKING_DIRECTORY/ROM/bs${EnemyFileNames[x]}.txd" ]]; then mv "$WORKING_DIRECTORY/ROM/bs${EnemyFileNames[x]}.txd" "$WORKING_DIRECTORY/ROM/Enemies/Enemies/${EnemyNames[x]}/Textures From Root"; fi
	done

	## Make Team Folders
	for SonicHeroesTeam in ${TeamNames[@]}; do mkdir -p "$WORKING_DIRECTORY/ROM/Enemies/Team Battle Icons/$SonicHeroesTeam/"; done;

	## Move Enemy Team Battle Teams
	for ((x=0; x<${#TeamNames[@]}; x++)); do
		if [[ -f "$WORKING_DIRECTORY/ROM/${EnemyTeamIcons[x]}.one" ]]; then mv "$WORKING_DIRECTORY/ROM/${EnemyTeamIcons[x]}.one" "$WORKING_DIRECTORY/ROM/Enemies/Team Battle Icons/${TeamNames[x]}/"; fi
	done

	## Move Common Assets
	for ((x=0; x<${#CommonEnemyAssets[@]}; x++)); do
		if [[ -f "$WORKING_DIRECTORY/ROM/${CommonEnemyAssets[x]}.one" ]]; then mv "$WORKING_DIRECTORY/ROM/${CommonEnemyAssets[x]}.one" "$WORKING_DIRECTORY/ROM/Enemies/${CommonEnemyAssetsFolders[x]}/"; fi
	done
}

Handle_Heroes_SoundEffects(){
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Action Stage Sounds"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Enemy"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Player"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Common"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Vocal"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Unknown"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Player/Team Sonic"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Player/Team Dark"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Player/Team Rose"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/Player/Team Chaotix"
	mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Sound Effect GCAX Sound Library Configuration"

	## Move Common SFX Stuff
	for ((x=0; x<${#CommonSFXFiles[@]}; x++)); do
		if [[ -f "$WORKING_DIRECTORY/ROM/${CommonSFXFiles[x]}" ]]; then mv "$WORKING_DIRECTORY/ROM/${CommonSFXFiles[x]}" "$WORKING_DIRECTORY/ROM/Sound Effects/${CommonSFXFolders[x]}/"; fi
	done

	## Make folders for each stage soundfx
	for ((x=0; x<${#SonicHeroesLevels[@]}; x++)) do
		mkdir -p "$WORKING_DIRECTORY/ROM/Sound Effects/Action Stage Sounds/${SonicHeroesLevels[x]}/Layout Table"
	done

	## Move soundfx to each stage
	for ActionStageSFX in "$WORKING_DIRECTORY/ROM/SE_S"*".mlt"; do
		FullFileName="${ActionStageSFX##*/}";
		LevelID=${FullFileName:4:2}
		mv "$ActionStageSFX" "$WORKING_DIRECTORY/ROM/Sound Effects/Action Stage Sounds/Stage ${LevelID}"*"/"
	done

	## Move soundfx tables to each stage
	for ActionStageSFX in "$WORKING_DIRECTORY/ROM/se_s"*".bin"; do
		FullFileName="${ActionStageSFX##*/}";
		LevelID=${FullFileName:4:2}
		mv "$ActionStageSFX" "$WORKING_DIRECTORY/ROM/Sound Effects/Action Stage Sounds/Stage ${LevelID}"*"/Layout Table/" &> /dev/null
	done

	shopt -s nocasematch
	## Move common soundfx tables
	for ActionStageSFX in "$WORKING_DIRECTORY/ROM/"[sS][eE]_*; do
		FullFileName="${ActionStageSFX##*/}";
		FileName=${FullFileName%.*}
		SFXTypeShort=${FileName:3:2}
		SFXType=${FileName:3:5}

		for ((x=0; x<${#SoundFXTypesShort[@]}; x++)); do
			if [[ ${SoundFXTypesShort[x]} == "$SFXTypeShort" ]]; then mv "$ActionStageSFX" "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/${SoundFXTypesShortFolders[x]}"; fi
		done

		for ((x=0; x<${#SoundFXTypesLong[@]}; x++)); do
			if [[ ${SoundFXTypesLong[x]} == "$SFXType" ]]; then mv "$ActionStageSFX" "$WORKING_DIRECTORY/ROM/Sound Effects/Sounds By Type/${SoundFXTypesLongFolders[x]}"; fi
		done
	done
	shopt -u nocasematch

	echo "DONE SFX"
	Heroes_Cleanup
	sleep 9999s
}

Handle_Heroes_Textures(){
	mv "$WORKING_DIRECTORY/ROM/textures" "$WORKING_DIRECTORY/ROM/Textures" &> /dev/null

	mkdir -p "$WORKING_DIRECTORY/ROM/Textures/Common/Effects"
	mkdir -p "$WORKING_DIRECTORY/ROM/Textures/Common/Loading"
	mkdir -p "$WORKING_DIRECTORY/ROM/Textures/Common/Start Button"
	mkdir -p "$WORKING_DIRECTORY/ROM/Textures/Unused/Enemies"
	mkdir -p "$WORKING_DIRECTORY/ROM/Textures/Unused/E3"
	mkdir -p "$WORKING_DIRECTORY/ROM/Textures/Unused/OutOfPlace/Other"

	echo "DONE TEXTURES"
	Heroes_Cleanup
	sleep 9999s
}

#for %%f in ("%WORKING_DIRECTORY%\ROM\Textures\s**.txd") do (
#for %%f in ("%WORKING_DIRECTORY%\ROM\collisions\*.cl") do (move /Y "%%f" "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
#for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\mission\*.bmp") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")
#for %%f in ("%WORKING_DIRECTORY%\ROM\stgtitle\*.one") do (move /Y %%f "%WORKING_DIRECTORY%\ROM\Levels\ActionStages" && echo "Moving Level Assets ==> %%~nxf")


Heroes_Cleanup () {
	##Wipe empty directories
	find "$WORKING_DIRECTORY/ROM/" -name "vssver.scc" -type f -delete
	find "$WORKING_DIRECTORY/ROM/" -type d -empty -delete
	find "$WORKING_DIRECTORY/ROM/Background Music/" -name "*.adx" -type f -delete
	find "$WORKING_DIRECTORY/ROM/Character Speech/" -name "*.adx" -type f -delete
	find "$WORKING_DIRECTORY/ROM/Movies/" -name "*.sfd" -type f -delete

	##Wipe ROM Remainments
	#rm -r "$WORKING_DIRECTORY/ROM/advertise/" &> /dev/null
	#rm -r "$WORKING_DIRECTORY/ROM/text/" &> /dev/null
	#rm -r "$WORKING_DIRECTORY/ROM/&&systemdata/" &> /dev/null

}

Wait_For_FFMPEG() {
	## While false do
	while ! [ "${ArbitraryVariable}" ]; do
		sleep 2s

		FFMPEGProcessIDs=`pidof ffmpeg`
		NumberOfProcesses=`echo ${FFMPEGProcessIDs} | tr -cd '[:space:]' | wc -m`
		if [[ $OldNumberOfProcesses = "" ]]; then echo "${ColourInfo}[WAITING] FFMPEG Media Conversion Processes Left:${ColourReset} $NumberOfProcesses";
		elif [[ $NumberOfProcesses < $OldNumberOfProcesses ]]; then echo "${ColourInfo}[WAITING] FFMPEG Media Conversion Processes:${ColourReset} $NumberOfProcesses"; fi
		OldNumberOfProcesses=$NumberOfProcesses

		if [[ `pidof ffmpeg` = "" ]]; then ArbitraryVariable=true; fi
	done
}

Heroes_Decompile() {
	## Async Grouping #1
	Decompress_Heroes_Sound_Clips;
	Convert_Heroes_Sound_Clips;

	## Async Grouping #2
	Convert_Heroes_Music;

	## Async Grouping #3
	Convert_Heroes_Video;

	## Async Grouping #4
	Handle_Heroes_Advertise ##Everything in the /advertise folder
	Sort_Heroes_Other_Menu_Items ##Everything menu related not in the /advertise folder
	Parse_Advertise_Assets
	Sort_Generic_Game_Code
	Sort_GameCube_Game_Code
	Handle_Heroes_Events
	Handle_Heroes_Characters
	Handle_Heroes_Enemies
	Handle_Heroes_SoundEffects
	Handle_Heroes_Textures
	Handle_Heroes_Stages

	#FinishUp
	Wait_For_FFMPEG
	Heroes_Cleanup
	sleep 9999s
}

######################### #################
######################### MAIN MENU METHODS
######################### #################

## Write the short script to allow for easier communication between the host OS and wine.
Write_Script_LinuxWrapper () {
	if [ ! -d "$WORKING_DIRECTORY/Linux/bin/" ]; then
		mkdir -p "$WORKING_DIRECTORY/Linux/bin/"
	else
		echo "${ColourWarning}===> Linux Application Wrapper Directory Already Exists${ColourReset}"
	fi


	if [ ! -f "$WORKING_DIRECTORY/Linux/bin/run_linux_program.sh" ]; then
		cat << "EOF" >> $WORKING_DIRECTORY/Linux/bin/run_linux_program.sh
#!/bin/sh
$1 "`wine winepath -u "$2"`"
EOF

	chmod +x "$WORKING_DIRECTORY/Linux/bin/run_linux_program.sh"
	else
		if [ ! -x "$WORKING_DIRECTORY/Linux/bin/run_linux_program.sh" ]; then
			chmod +x "$WORKING_DIRECTORY/Linux/bin/run_linux_program.sh"
			echo "=${ColourWarning}==> Linux Application Wrapper Has Not Been Marked Executable, This Was Fixed${ColourReset}"
		else
			echo "${ColourWarning}===> Linux Application Wrapper Already Exists${ColourReset}"
		fi
	fi
}

Compile_Local_FFMPEG () {
	echo "${ColourWarning}===> Cloning FFMPEG-Git${ColourReset}"
	git clone https://git.ffmpeg.org/ffmpeg.git $WORKING_DIRECTORY/Linux/ffmpeg-git

	echo "${ColourWarning}===> Cloning x265${ColourReset}"
	hg clone https://bitbucket.org/multicoreware/x265 $WORKING_DIRECTORY/Linux/x265
	cd $WORKING_DIRECTORY/

	echo "${ColourWarning}===> Cloning libfdk_aac${ColourReset}"
	git clone https://github.com/mstorsjo/fdk-aac.git $WORKING_DIRECTORY/Linux/libfdk_aac
	cd $WORKING_DIRECTORY/Linux/libfdk_aac

	echo "${ColourWarning}===> Configuring and Compiling libfdk_aac with $((`nproc`+1)) threads ${ColourReset}"
	PATH="$WORKING_DIRECTORY/Linux/bin:$PATH" ./autogen.sh
	PATH="$WORKING_DIRECTORY/Linux/bin:$PATH" ./configure --enable-shared --enable-static
	make -j$((`nproc`+1))
	make install
	cd $WORKING_DIRECTORY/

	echo "${ColourWarning}===> Compiling x265 with $((`nproc`+1)) threads ${ColourReset}"
	cd $WORKING_DIRECTORY/Linux/x265/build/linux
	PATH="$WORKING_DIRECTORY/Linux/bin:$PATH" cmake -G "Unix Makefiles" -DCMAKE_INSTALL_PREFIX="$WORKING_DIRECTORY/Linux/ffmpeg-git" -DENABLE_SHARED:bool=off ../../source
	PATH="$WORKING_DIRECTORY/Linux/bin:$PATH" make -j$((`nproc`+1))
	make install
	cd $WORKING_DIRECTORY/

	echo "${ColourWarning}===> Configuring and Compiling FFMPEG-Git with $((`nproc`+1)) threads ${ColourReset}"
	cd $WORKING_DIRECTORY/Linux/ffmpeg-git/

	##Configure minimal FFMPEG
	PATH="$WORKING_DIRECTORY/Linux/bin:$PATH" PKG_CONFIG_PATH="$WORKING_DIRECTORY/Linux/ffmpeg-git/lib/pkgconfig/" ./configure --extra-cflags="-I$WORKING_DIRECTORY/Linux/ffmpeg-git/include" --extra-ldflags="-L$WORKING_DIRECTORY/Linux/ffmpeg-git/lib" --pkg-config-flags="--static" --prefix="$WORKING_DIRECTORY/Linux/ffmpeg-git/" --enable-libx265 --enable-memalign-hack --disable-encoders --disable-decoders --disable-ffplay --disable-ffprobe --disable-ffserver --disable-debug --disable-filters --disable-devices --disable-outdevs --disable-indevs --disable-protocols --disable-parsers --enable-encoder='libfdk_aac,libx265,adpcm_adx,mpeg1video,sonic_ls' --enable-decoder='libfdk_aac,hevc,adpcm_adx,mpeg1video,aac' --enable-protocol='cache,pipe,concat,md5,file,md5,data,crypto,adx' --enable-parser='aac,mpegvideo,mpeg1video,hevc,adx' --enable-filter=aresample --enable-libfdk-aac --enable-nonfree --enable-libx265 --enable-gpl
	PATH="$WORKING_DIRECTORY/Linux/bin:$PATH" make -j$((`nproc`+1))
	make install
	mv "$WORKING_DIRECTORY/Linux/ffmpeg-git/ffmpeg" "$WORKING_DIRECTORY/Linux/bin/ffmpeg"
	cd $WORKING_DIRECTORY/
}


## Main Menu V2
while :
do
	clear
	$AllColourWarning
	$AllColourBold
	cat << "EOF"
_______________________________________________________________________________________________
 ___           _      _  _                       ___      _ _    _   ___         _
/ __| ___ _ _ (_)__  | || |___ _ _ ___  ___ ___ | _ )_  _(_) |__| | / __|_  _ __| |_ ___ _ __
\__ \/ _ \ ' \| / _| | __ / -_) '_/ _ \/ -_|_-< | _ \ || | | / _` | \__ \ || (_-<  _/ -_) '  \
|___/\___/_||_|_\__| |_||_\___|_| \___/\___/__/ |___/\_,_|_|_\__,_| |___/\_, /__/\__\___|_|_|_|
                                                                         |__/
WIP                                                                        =====> By Sewer56lol
_______________________________________________________________________________________________

EOF
	$AllColourReset
	echo -e "${ColourInfo}Working Directory: $WORKING_DIRECTORY \n${ColourReset}"

	cat << "EOF"
----------------------------------------
Preparation (Only needs to be done once)
----------------------------------------
(1) Prepare Wine CMD Native Linux Application Wrapper (WSL Support)
(2) Compile and Build Custom FFMPEG

------------------------------------
Build System (For Modders/Tinkerers)
------------------------------------
(3) Decompile & Rearrange Game Assets
(4) Recompile & Prepare (WIP)
(5) Create Full Game Lossless RIP (WIP)
(6) Create Full Game Lossy RIP (WIP)
(Q) Quit
----------------------------------------
EOF
	read -n1 -s
	echo "${ColourStandout}OUTPUT${ColourReset}"
	case "$REPLY" in
		"1")  Write_Script_LinuxWrapper ;;
		"2")  Compile_Local_FFMPEG ;;
		"3") 	Heroes_Decompile ;;
		"Q")  exit                      ;;
		 * )  echo "Invalid Option"; InstantRefresh="Y" ;;
	esac
	echo "${ColourStandout}DONE${ColourReset}"
	if [ "$InstantRefresh" == "Y" ]; then
		echo -e "\nMenu will refresh in 1 second..."
		sleep 1
		InstantRefresh="N"
	else
		echo -e "\nPress Enter key to Continue..."
		read
	fi
done
