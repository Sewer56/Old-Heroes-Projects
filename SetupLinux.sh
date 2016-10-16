#!/bin/bash

##ROOTMODE
#if [[ $EUID -ne 0 ]]; then
#   echo "This script should be run as root, try using sudo." 
#   echo "Root is required to make install libx265 and libfdk_aac for FFMPEG compilation" 
#   exit 1
#fi

## Shell In-Replacement Colours For Text
ColourReset=`tput sgr0`
ColourStandout=`tput smso`
ColourBold=`tput bold`
ColourWarning=`tput setaf 15`
ColourInfo=`tput setaf 10`

## Shell In-Replacement for All Colours!
AllColourReset="tput sgr0"
AllColourBold="tput bold"
AllColourWarning="tput setaf 15"
AllColourInfo="tput setaf 10"

# Declare Initial Variables
WORKING_DIRECTORY=$(pwd)

# Create the Linux Directory
if [ ! -d "$WORKING_DIRECTORY/Linux/" ]; then 
	mkdir "$WORKING_DIRECTORY/Linux/"
fi

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

	echo "${ColourWarning}===> Compiling x265 with $((`nproc`+1)) threads ${ColourReset}"
	cd $WORKING_DIRECTORY/Linux/x265/build/linux
	cmake -G "Unix Makefiles" -DCMAKE_INSTALL_PREFIX="$WORKING_DIRECTORY/Linux/ffmpeg-git" -DENABLE_SHARED:bool=off ../../source
	make -j$((`nproc`+1))
	make install
	cd $WORKING_DIRECTORY/

	echo "${ColourWarning}===> Cloning libfdk_aac${ColourReset}"
	git clone https://github.com/mstorsjo/fdk-aac.git $WORKING_DIRECTORY/Linux/libfdk_aac
	cd $WORKING_DIRECTORY/Linux/libfdk_aac

	echo "${ColourWarning}===> Configuring and Compiling libfdk_aac with $((`nproc`+1)) threads ${ColourReset}"
	./autogen.sh
	./configure --enable-shared --enable-static
	make -j$((`nproc`+1))
	make install
	cd $WORKING_DIRECTORY/

	echo "${ColourWarning}===> Configuring and Compiling FFMPEG-Git with $((`nproc`+1)) threads ${ColourReset}"
	cd $WORKING_DIRECTORY/Linux/ffmpeg-git/

	##Configure minimal FFMPEG
	PKG_CONFIG_PATH="$WORKING_DIRECTORY/Linux/ffmpeg-git/lib/pkgconfig/" 
	./configure --extra-cflags="-I$WORKING_DIRECTORY/Linux/ffmpeg-git/include" --extra-ldflags="-L$WORKING_DIRECTORY/Linux/ffmpeg-git/lib" --pkg-config-flags="--static" --prefix="$WORKING_DIRECTORY/Linux/ffmpeg-git/" --enable-libx265 --enable-memalign-hack --disable-encoders --disable-decoders --disable-ffplay --disable-ffprobe --disable-ffserver --disable-debug --disable-filters --disable-devices --disable-outdevs --disable-indevs --disable-protocols --disable-parsers --enable-encoder='libfdk_aac,libx265,adpcm_adx,mpeg1video,sonic_ls' --enable-decoder='libfdk_aac,hevc,adpcm_adx,mpeg1video,aac' --enable-protocol='cache,pipe,concat,md5,file,md5,data,crypto,adx' --enable-parser='aac,mpegvideo,mpeg1video,hevc,adx' --enable-filter=aresample --enable-libfdk-aac --enable-nonfree --enable-libx265 --enable-gpl
	make -j$((`nproc`+1))
	mv "$WORKING_DIRECTORY/Linux/ffmpeg-git/ffmpeg" "$WORKING_DIRECTORY/Linux/ffmpeg-git/ffmpeg" 
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
								               =====>Linux Port
_______________________________________________________________________________________________

EOF
	$AllColourReset
	echo -e "${ColourInfo}Working Directory: $WORKING_DIRECTORY \n${ColourReset}"

	cat << "EOF"
----------------------------------------
Preparation (Only needs to be done once)
----------------------------------------
(1) Prepare Wine CMD Native Linux Application Wrapper
(2) Compile and Build local FFMPEG
(Q) Quit
----------------------------------------

EOF
	read -n1 -s
	echo "${ColourStandout}OUTPUT${ColourReset}"
	case "$REPLY" in
		"1")  Write_Script_LinuxWrapper ;;
		"2")  Compile_Local_FFMPEG ;;
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
