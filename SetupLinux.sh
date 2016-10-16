#!/bin/bash
if [[ $EUID -ne 0 ]]; then
   echo "This script must be run as root" 
   exit 1
fi


# Declare Initial Variables
WORKING_DIRECTORY=$(pwd)


## Write the short script to allow for easier communication between the host OS and wine.
Write_Script_LinuxWrapper () {
	if [ ! -d "$WORKING_DIRECTORY/Linux/WinepathWrapper/" ]; then 
		mkdir -p "$WORKING_DIRECTORY/Linux/WinepathWrapper/" 
	fi 
	sleep 5m
	echo #!/bin/sh $1 "`wine winepath -u "$2"`" >> WORKING_DIRECTORY/bin/run_linux_program
}

cat << "EOF"
_______________________________________________________________________________________________
 ___           _      _  _                       ___      _ _    _   ___         _            
/ __| ___ _ _ (_)__  | || |___ _ _ ___  ___ ___ | _ )_  _(_) |__| | / __|_  _ __| |_ ___ _ __ 
\__ \/ _ \ ' \| / _| | __ / -_) '_/ _ \/ -_|_-< | _ \ || | | / _` | \__ \ || (_-<  _/ -_) '  \
|___/\___/_||_|_\__| |_||_\___|_| \___/\___/__/ |___/\_,_|_|_\__,_| |___/\_, /__/\__\___|_|_|_|
                                                                         |__/                  
								               =====>Linux Port
_______________________________________________________________________________________________


------------------------------------------------------------------------------
This is an installation/initialization script, you only need to use this once, 
you can later simply launch Linux.sh instead of this.
------------------------------------------------------------------------------

EOF

## Main Menu Linux
echo -e "Working Directory: $WORKING_DIRECTORY \n"
PS3='###=> Please enter your choice: '
options=("Install Linux Program Launching Wrapper for Wine CMD"  "Option 2" "Option 3" "Quit")
select opt in "${options[@]}"
do
	case $opt in
		"Install Linux Program Launching Wrapper for Wine CMD")
			Write_Script_LinuxWrapper
			;;
		"Option 2")
			echo "you chose choice 2"
			;;
		"Option 3")
			echo "you chose choice 3"
			;; 
		"Quit")
			break
			;;
		*) 
			echo "invalid option"
			;;
    	esac
done

