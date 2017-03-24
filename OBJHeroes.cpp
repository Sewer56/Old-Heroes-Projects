/*
	HeroesOBJ, my second C++ program, utility to convert Sonic Heroes'
	object level layout formats into human editable text files and back.
    Copyright (C) 2017  Sewer56lol

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>
*/

// basic file operations
#include <iostream> //I/O Stream
#include <iomanip> //I/O Manipulation
#include <fstream> //File Stream
#include <cstring> //Strings
#include <string> //String
#include <cmath> //Mathematics library, for powers etc.
#include <sstream> //String Stream
#include <stdio.h>
#include <stdlib.h>

// Speedup: Use this on Linux! Search for #DANKMEME.
//#include "byteswap.h" //Woot, mah endians m8

using namespace std; //Standard Namespace
char VerboseMode;

int ObjectCount = 2048; // The maximum default limit for amount of objects in main section entry.
												// Misc section is unlimited thus read till end of file.


int GetObjectDataMainEntryLength(int NumberOfObjects) { return NumberOfObjects * 48; } // Each object is 48 bytes
int GetDataEntryLength(int FileSize)
{
	int MiscSize;
	MiscSize = FileSize - 98304; // 0x18000, offset of misc
	return MiscSize;
}

// Convert Any Angle to BAMS.
float GetDegreesAngle(unsigned int AngleInBAMS, int SizeOfData)
{
	//cout << AngleInBAMS << " << Angle In BAMS" << endl;
	int DivisionPower0 = (SizeOfData*8) - 1;
	float DivisionPower1; DivisionPower1 = 180.0/pow(2.0, DivisionPower0); //cout << DivisionPower1 << " << Ratio" << endl;
	return ( AngleInBAMS * (DivisionPower1) );
}

// Convery Any Degrees Angle from BAMS
unsigned int GetBAMSAngle(float DegreesAngle, int SizeOfData)
{
	int DivisionPower0 = (SizeOfData*8) - 1;
	float DivisionPower1; DivisionPower1 = 180.0/pow(2.0, DivisionPower0);
	return ( DegreesAngle / (DivisionPower1) );
}

int HexToUINT(string HexValue)
{
	unsigned int x;
	stringstream ss;
	ss << std::hex << HexValue;
	ss >> x;
	return x;
}

float ReverseFloat( const float inFloat )
{
   float retVal;
   char *floatToConvert = ( char* ) & inFloat;
   char *returnFloat = ( char* ) & retVal;

   // swap the bytes into a temporary buffer
   returnFloat[0] = floatToConvert[3];
   returnFloat[1] = floatToConvert[2];
   returnFloat[2] = floatToConvert[1];
   returnFloat[3] = floatToConvert[0];

   return retVal;
}



void OBJToTXT(string InputFile, string OutputFile)
{
	size_t FileSize = 0; //File size
	char* OutputData;

	ifstream FileReader(InputFile,ios::binary); //Input file stream to read BLK file.
	if (! FileReader) { cout << "Error opening file" << endl; } else { if (VerboseMode == 1) { cout << "Successfully opened object file,\n" << endl; } }

	FileReader.seekg(0, ios::end); // Set the pointer to the end
	FileSize = FileReader.tellg(); // Get the length of the file
	if (VerboseMode == 1) { cout << "Size of file: " << FileSize << " Bytes" << endl; } // Print size of file
	FileReader.seekg(0, ios::beg); // Set the pointer to the beginning

	unsigned int DataMiscEntryLength;
	DataMiscEntryLength = GetDataEntryLength(FileSize);
	// 0x18000 offset.
	if (VerboseMode == 1) { cout << "Size of main data: " << FileSize - DataMiscEntryLength << " Bytes" << endl; } // Print size of file
	if (VerboseMode == 1) { cout << "Size of misc data: " << DataMiscEntryLength << " Bytes" << endl; } // Print size of file


	OutputData = new char[FileSize + 1]; // Char array to hold the string.
	FileReader.read( OutputData, FileSize ); // Read the file into the OutputData

	// -----------------------------------------------------------------------
	/*
		Lets define some characteristics for the main section entries, shall we?
	*/
	// -----------------------------------------------------------------------

	int ObjectLoopCount = 1; // Iterates up for each main object entry

	// Gets the length of data within which the program will be searching for the main object entries.
	unsigned int DataMainEntryLength; DataMainEntryLength = GetObjectDataMainEntryLength(ObjectCount);

	// Object XYZ Positions.
	float XPosition;
	float YPosition;
	float ZPosition;

	// Object XYZ Rotations, in BAMS/Binary Angle Measurement System
	unsigned int XRotation;
	unsigned int YRotation;
	unsigned int ZRotation;

	// Object XYZ Rotations, in Degrees.
	float XRotationDEG;
	float YRotationDEG;
	float ZRotationDEG;

	/*--------------
		Looping Region
	*/

	// Usually 00,02 in HEX, or simply 2. Changing this never seems to make effect.
	short int UnknownIgnoredValue; UnknownIgnoredValue = 0;

	// Team affiliated with the object. Probably a carryover idea from early Sonic Heroes development, it doesn't do anything.
	char *ObjectTeam; ObjectTeam = new char[1];

	// Mystery byte, must be odd for the object to load.
	char *MysteryByte; MysteryByte = new char[1];
	unsigned int iUnknownUnusedValue;
	char *UnknownUnusedValue; UnknownUnusedValue = new char[4];
	/*--------------
		Looping Region 1/2
	*/

	/// /// Same as above, you can make certain objects of IDs disappear by tampering here, but the game seems to quite frankly ignore this.
	short int UnknownIgnoredValue2; UnknownIgnoredValue2 = 0;
	char *ObjectTeam2; ObjectTeam2 = new char[1];
	char *MysteryByte2; MysteryByte2 = new char[1];
	unsigned int iUnknownUnusedValue2;
	char *UnknownUnusedValue2; UnknownUnusedValue2 = new char[4];

/*------------------
	Looping Region End
*/

	char *ObjectList; ObjectList = new char[1];
  char *ObjectID; ObjectID = new char[1];
	char *LinkID; LinkID = new char[1];
	char *LoadDistanceScaleFactor; LoadDistanceScaleFactor = new char[1];
	unsigned int MiscallenousEntryID;


	// Used for temporary storage and holding of data to convert and relay.
	char *FourByteStorage; FourByteStorage = new char[4]; // Temporarily will store 4 bytes of data

	// Sets up the output of the new file as it will be given.
	ofstream FileWriter(OutputFile);
	if (FileWriter.is_open()){ if (VerboseMode == 1) { cout << "File successfully created for writing.\n" << endl; } }

	/*

	HERE WE DO THE THINGMAGIQ WHICH RIPS THE MAIN OBJECT PROPERTIES

	*/

	for(unsigned int x = 0; x < DataMainEntryLength;)
	{
		// Get the X, Y and Z Positions
		FourByteStorage[0] = OutputData[x+3]; FourByteStorage[1] = OutputData[x+2]; FourByteStorage[2] = OutputData[x+1]; FourByteStorage[3] = OutputData[x+0];
		XPosition = *(float *)FourByteStorage;

		FourByteStorage[0] = OutputData[x+7]; FourByteStorage[1] = OutputData[x+6]; FourByteStorage[2] = OutputData[x+5]; FourByteStorage[3] = OutputData[x+4];
		YPosition = *(float *)FourByteStorage;

		FourByteStorage[0] = OutputData[x+11]; FourByteStorage[1] = OutputData[x+10]; FourByteStorage[2] = OutputData[x+9]; FourByteStorage[3] = OutputData[x+8];
		ZPosition = *(float *)FourByteStorage;

		FourByteStorage[0] = OutputData[x+15]; FourByteStorage[1] = OutputData[x+14]; FourByteStorage[2] = OutputData[x+13]; FourByteStorage[3] = OutputData[x+12];
		XRotation = *(unsigned int *)FourByteStorage;
		XRotationDEG = GetDegreesAngle(XRotation, sizeof(XRotation));

		FourByteStorage[0] = OutputData[x+19]; FourByteStorage[1] = OutputData[x+18]; FourByteStorage[2] = OutputData[x+17]; FourByteStorage[3] = OutputData[x+16];
		YRotation = *(unsigned int *)FourByteStorage;
		YRotationDEG = GetDegreesAngle(YRotation, sizeof(YRotation));

		FourByteStorage[0] = OutputData[x+23]; FourByteStorage[1] = OutputData[x+22]; FourByteStorage[2] = OutputData[x+21]; FourByteStorage[3] = OutputData[x+20];
		ZRotation = *(unsigned int *)FourByteStorage;
		ZRotationDEG = GetDegreesAngle(ZRotation, sizeof(ZRotation));

		// LOOP REGION

		// Usually 00,02 in HEX, or simply 2. Changing this never seems to make effect.
		FourByteStorage[0] = OutputData[x+25]; FourByteStorage[1] = OutputData[x+24];
		UnknownIgnoredValue = *(short int *)FourByteStorage;

		// Team affiliated with the object. Probably a carryover idea from early Sonic Heroes development, it doesn't do anything.
		// Mystery byte.
		sprintf(ObjectTeam,"0x%02X",(unsigned int)(unsigned char)OutputData[x+26]);
		sprintf(MysteryByte,"0x%02X",(unsigned int)(unsigned char)OutputData[x+27]);

		FourByteStorage[0] = OutputData[x+31]; FourByteStorage[1] = OutputData[x+30]; FourByteStorage[2] = OutputData[x+29]; FourByteStorage[3] = OutputData[x+28];
		iUnknownUnusedValue = *(unsigned int *)FourByteStorage;
		sprintf (UnknownUnusedValue, "0x%08X",iUnknownUnusedValue);


		// Usually 00,02 in HEX, or simply 2. Changing this never seems to make effect.
		FourByteStorage[0] = OutputData[x+33]; FourByteStorage[1] = OutputData[x+32];
		iUnknownUnusedValue2 = *(unsigned int *)FourByteStorage;
		sprintf (UnknownUnusedValue2, "0x%08X",iUnknownUnusedValue2);

		// Team affiliated with the object. Probably a carryover idea from early Sonic Heroes development, it doesn't do anything.
		// Mystery byte.
	  sprintf(ObjectTeam2,"0x%02X",(unsigned int)(unsigned char)OutputData[x+34]);
		sprintf(MysteryByte2,"0x%02X",(unsigned int)(unsigned char)OutputData[x+35]);

		FourByteStorage[0] = OutputData[x+39]; FourByteStorage[1] = OutputData[x+30]; FourByteStorage[2] = OutputData[x+37]; FourByteStorage[3] = OutputData[x+36];
		sprintf(UnknownUnusedValue2,"0x%08X",*(char *)FourByteStorage);

		// END LOOP REGION

		// Finishing properties
		sprintf(ObjectList,"0x%02X",(unsigned int)(unsigned char)OutputData[x+40]);
		sprintf(ObjectID,"0x%02X",(unsigned int)(unsigned char)OutputData[x+41]);
		sprintf(LinkID,"0x%02X",(unsigned int)(unsigned char)OutputData[x+42]);
		sprintf(LoadDistanceScaleFactor,"0x%02X",(unsigned int)(unsigned char)OutputData[x+43]);

		FourByteStorage[0] = OutputData[x+47]; FourByteStorage[1] = OutputData[x+46]; FourByteStorage[2] = OutputData[x+45]; FourByteStorage[3] = OutputData[x+44];
		MiscallenousEntryID = *(unsigned int *)FourByteStorage;

		if (VerboseMode == 1) { cout << "[Object " << dec << ObjectLoopCount << "]" << endl; } FileWriter << "[Object " << dec << ObjectLoopCount << "]" << "\n";

		if (VerboseMode == 1) { cout << "Object X Coordinate: " << XPosition << endl; } FileWriter << "Object X Coordinate: " << XPosition << "\n";
		if (VerboseMode == 1) { cout << "Object Y Coordinate: " << YPosition << endl; } FileWriter << "Object Y Coordinate: " << YPosition << "\n";
		if (VerboseMode == 1) { cout << "Object Z Coordinate: " << ZPosition << endl; } FileWriter << "Object Z Coordinate: " << ZPosition << "\n";
		if (VerboseMode == 1) { cout << "Object X Rotation Degrees: " << XRotationDEG << endl; } FileWriter << "Object X Rotation Degrees: " << XRotationDEG << "\n";
		if (VerboseMode == 1) { cout << "Object Y Rotation Degrees: " << YRotationDEG << endl; } FileWriter << "Object Y Rotation Degrees: " << YRotationDEG << "\n";
		if (VerboseMode == 1) { cout << "Object Z Rotation Degrees: " << ZRotationDEG << endl; } FileWriter << "Object Z Rotation Degrees: " << ZRotationDEG << "\n";
		if (VerboseMode == 1) { cout << "----------------------" << endl; } FileWriter << "----------------------" << "\n";
		if (VerboseMode == 1) { cout << "Unknown Ignored Value: " << UnknownIgnoredValue << endl; } FileWriter << "Unknown Ignored Value: " << UnknownIgnoredValue << "\n";
		if (VerboseMode == 1) { cout << "Object's Associated Team: " << ObjectTeam << endl; } FileWriter << "Object's Associated Team: " << ObjectTeam << "\n";
		if (VerboseMode == 1) { cout << "Mystery Object Byte: " << MysteryByte << endl; } FileWriter << "Mystery Object Byte: " << MysteryByte << "\n";
		if (VerboseMode == 1) { cout << "Four Bytes of Mystery: " << UnknownUnusedValue << endl; } FileWriter << "Four Bytes of Mystery: " << UnknownUnusedValue << "\n";
		if (VerboseMode == 1) { cout << "----------------------" << endl; } FileWriter << "----------------------" << "\n";
		if (VerboseMode == 1) { cout << "Unknown Ignored Value #2: " << UnknownIgnoredValue2 << endl; } FileWriter << "Unknown Ignored Value #2: " << UnknownIgnoredValue2 << "\n";
		if (VerboseMode == 1) { cout << "Object's Associated Team #2: " << ObjectTeam2 << endl; } FileWriter << "Object's Associated Team #2: " << ObjectTeam2 << "\n";
		if (VerboseMode == 1) { cout << "Mystery Object Byte #2: " << MysteryByte2 << endl; } FileWriter << "Mystery Object Byte #2: " << MysteryByte2 << "\n";
		if (VerboseMode == 1) { cout << "Four Bytes of Mystery #2: " << UnknownUnusedValue2 << endl; } FileWriter << "Four Bytes of Mystery #2: " << UnknownUnusedValue2 << "\n";
		if (VerboseMode == 1) { cout << "----------------------" << endl; } FileWriter << "----------------------" << "\n";
		if (VerboseMode == 1) { cout << "Object Grouping/List: " << ObjectList << endl; } FileWriter << "Object Grouping/List: " << ObjectList << "\n";
		if (VerboseMode == 1) { cout << "Object Type/ID: " << ObjectID << endl; } FileWriter << "Object Type/ID: " << ObjectID << "\n";
		if (VerboseMode == 1) { cout << "Object Linking ID: " << LinkID << endl; } FileWriter << "Object Linking ID: " << LinkID << "\n";
		if (VerboseMode == 1) { cout << "Object Load Distance Multiplier: " << LoadDistanceScaleFactor << endl; } FileWriter << "Object Load Distance Multiplier: " << LoadDistanceScaleFactor << "\n";
		if (VerboseMode == 1) { cout << "Object Property Section ID: " << MiscallenousEntryID << endl; } FileWriter << "Object Property Section ID: " << MiscallenousEntryID << "\n";

		if (VerboseMode == 1) { cout << endl; } FileWriter << "\n";


		ObjectLoopCount += 1;
		x += 48; // Length of each object entry in bytes.
	}

	/*
		Misc Data Entry Objects
	*/
	int MiscLoopCount = 1; // Iterates up for each main object entry
	short unsigned int Activator; // Should be 256, in every case.
	short unsigned int EntryID; // Misc entry to match object entry.
	char *FourBStorage; FourBStorage = new char[4]; // Temporarily will store 4 bytes of data

	// These are just for storing data.
	unsigned int iFourByteContainer1;
	unsigned int iFourByteContainer2;
	unsigned int iFourByteContainer3;
	unsigned int iFourByteContainer4;
	unsigned int iFourByteContainer5;
	unsigned int iFourByteContainer6;
	unsigned int iFourByteContainer7;
	unsigned int iFourByteContainer8;

	char *FourByteContainer1; FourByteContainer1 = new char[4];
	char *FourByteContainer2; FourByteContainer2 = new char[4];
	char *FourByteContainer3; FourByteContainer3 = new char[4];
	char *FourByteContainer4; FourByteContainer4 = new char[4];
	char *FourByteContainer5; FourByteContainer5 = new char[4];
	char *FourByteContainer6; FourByteContainer6 = new char[4];
	char *FourByteContainer7; FourByteContainer7 = new char[4];
	char *FourByteContainer8; FourByteContainer8 = new char[4];

	for(unsigned int x = DataMainEntryLength; x < FileSize;)
	{
		FourBStorage[0] = OutputData[x+1]; FourBStorage[1] = OutputData[x];
		Activator = *(short unsigned int *)FourBStorage;

		FourBStorage[0] = OutputData[x+3]; FourBStorage[1] = OutputData[x+2];
		EntryID = *(short unsigned int *)FourBStorage;

		FourBStorage[0] = OutputData[x+7]; FourBStorage[1] = OutputData[x+6]; FourBStorage[2] = OutputData[x+5]; FourBStorage[3] = OutputData[x+4];
		iFourByteContainer1 = *(unsigned int *)FourBStorage;
		sprintf (FourByteContainer1, "0x%08X",iFourByteContainer1);

		FourBStorage[0] = OutputData[x+11]; FourBStorage[1] = OutputData[x+10]; FourBStorage[2] = OutputData[x+9]; FourBStorage[3] = OutputData[x+8];
		iFourByteContainer2 = *(unsigned int *)FourBStorage;
		sprintf (FourByteContainer2, "0x%08X",iFourByteContainer2);

		FourBStorage[0] = OutputData[x+15]; FourBStorage[1] = OutputData[x+14]; FourBStorage[2] = OutputData[x+13]; FourBStorage[3] = OutputData[x+12];
		iFourByteContainer3 = *(unsigned int *)FourBStorage;
		sprintf (FourByteContainer3, "0x%08X",iFourByteContainer3);

		FourBStorage[0] = OutputData[x+19]; FourBStorage[1] = OutputData[x+18]; FourBStorage[2] = OutputData[x+17]; FourBStorage[3] = OutputData[x+16];
		iFourByteContainer4 = *(unsigned int *)FourBStorage;
		sprintf (FourByteContainer4, "0x%08X",iFourByteContainer4);

		FourBStorage[0] = OutputData[x+23]; FourBStorage[1] = OutputData[x+22]; FourBStorage[2] = OutputData[x+21]; FourBStorage[3] = OutputData[x+20];
		iFourByteContainer5 = *(unsigned int *)FourBStorage;
		sprintf (FourByteContainer5, "0x%08X",iFourByteContainer5);

		FourBStorage[0] = OutputData[x+27]; FourBStorage[1] = OutputData[x+26]; FourBStorage[2] = OutputData[x+25]; FourBStorage[3] = OutputData[x+24];
		iFourByteContainer6 = *(unsigned int *)FourBStorage;
		sprintf (FourByteContainer6, "0x%08X",iFourByteContainer6);

		FourBStorage[0] = OutputData[x+31]; FourBStorage[1] = OutputData[x+30]; FourBStorage[2] = OutputData[x+29]; FourBStorage[3] = OutputData[x+28];
		iFourByteContainer7 = *(unsigned int *)FourBStorage;
		sprintf (FourByteContainer7, "0x%08X",iFourByteContainer7);

		FourBStorage[0] = OutputData[x+35]; FourBStorage[1] = OutputData[x+34]; FourBStorage[2] = OutputData[x+33]; FourBStorage[3] = OutputData[x+32];
		iFourByteContainer8 = *(unsigned int *)FourBStorage;
		sprintf (FourByteContainer8, "0x%08X",iFourByteContainer8);

		if (VerboseMode == 0) { cout << "[Misc " << dec << MiscLoopCount << "]" << endl; } FileWriter << "[Misc " << dec << MiscLoopCount << "]" << "\n";
		if (VerboseMode == 1) { cout << "Activator: " << Activator << endl; } FileWriter << "Activator: " << Activator << "\n";
		if (VerboseMode == 1) { cout << "Entry ID: " << EntryID <<  endl; } FileWriter << "Entry ID: " << EntryID << "\n";
		if (VerboseMode == 1) { cout << "Four Byte Container #1: " << FourByteContainer1 <<  endl; } FileWriter << "Four Byte Container #1: " << FourByteContainer1 << "\n";
		if (VerboseMode == 1) { cout << "Four Byte Container #2: " << FourByteContainer2 <<  endl; } FileWriter << "Four Byte Container #2: " << FourByteContainer2 << "\n";
		if (VerboseMode == 1) { cout << "Four Byte Container #3: " << FourByteContainer3 <<  endl; } FileWriter << "Four Byte Container #3: " << FourByteContainer3 << "\n";
		if (VerboseMode == 1) { cout << "Four Byte Container #4: " << FourByteContainer4 <<  endl; } FileWriter << "Four Byte Container #4: " << FourByteContainer4 << "\n";
		if (VerboseMode == 1) { cout << "Four Byte Container #5: " << FourByteContainer5 <<  endl; } FileWriter << "Four Byte Container #5: " << FourByteContainer5 << "\n";
		if (VerboseMode == 1) { cout << "Four Byte Container #6: " << FourByteContainer6 <<  endl; } FileWriter << "Four Byte Container #6: " << FourByteContainer6 << "\n";
		if (VerboseMode == 1) { cout << "Four Byte Container #7: " << FourByteContainer7 <<  endl; } FileWriter << "Four Byte Container #7: " << FourByteContainer7 << "\n";
		if (VerboseMode == 1) { cout << "Four Byte Container #8: " << FourByteContainer8 <<  endl; } FileWriter << "Four Byte Container #8: " << FourByteContainer8 << "\n";

		if (VerboseMode == 1) { cout << endl; } FileWriter << "\n";

		MiscLoopCount += 1;
		x += 36; // Length of each misc entry in bytes.
	}
}

void TXTToOBJ(string InputFile, string OutputFile)
{
	ifstream FileReader(InputFile); //Input file stream to read BLK file.
	if (! FileReader) { cout << "Error opening input file" << endl; } else { if (VerboseMode == 1) { cout << "Successfully opened text file,\n" << endl; } }

	// Sets up the output of the new file as it will be given.
	ofstream FileWriter(OutputFile, ofstream::out | ofstream::binary);
	if (FileWriter.is_open()){ if (VerboseMode == 1) { cout << "File successfully created for writing.\n" << endl; } }

	string CurrentLine;
	string CurrentValue;
	size_t ContentMatchIndex;

	/////////

	// Prepare writer for writing binary
	//char WritingData4Bytes[4];
	char WritingData2Bytes[2];

	/// MAIN SECTION ///
	while( getline(FileReader,CurrentLine) && CurrentLine != "[Misc 1]")
	{

		// Parse Coordinate Type
		if ( CurrentLine.find("Coordinate: ") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of(" ");
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);

			float FloatToWrite = stof(CurrentValue);
			FloatToWrite = ReverseFloat(FloatToWrite);
			if (VerboseMode == 1) { cout << "Writing Coordinate: " << FloatToWrite << endl; }

			FileWriter.write((char*)&FloatToWrite,sizeof(FloatToWrite));
		}

		// Parse Coordinate Type
		if ( CurrentLine.find("Rotation Degrees") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of(" ");
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);

			float FloatToWrite = stof(CurrentValue);
			unsigned int BAMSAngle = GetBAMSAngle(FloatToWrite, sizeof(FloatToWrite));

			//Byteswap the integer that is going to be written. It is in the wrong endian.
			unsigned int IntegerToWrite = ((BAMSAngle>>24)&0xff) | // move byte 3 to byte 0
									 ((BAMSAngle<<8)&0xff0000) | // move byte 1 to byte 2
									 ((BAMSAngle>>8)&0xff00) | // move byte 2 to byte 1
									 ((BAMSAngle<<24)&0xff000000); // byte 0 to byte 3

			//if (VerboseMode == 0) { cout << "Writing Rotation BAMS: " << BAMSAngle << endl; }
			if (VerboseMode == 1) { cout << "Writing Rotation Degrees: " << GetDegreesAngle(BAMSAngle, sizeof(BAMSAngle)) << endl; }

			FileWriter.write((char*)&IntegerToWrite,sizeof(IntegerToWrite));
		}

		if ( CurrentLine.find("Unknown Ignored Value") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of(" ");
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);

			short unsigned int Number = stoi(CurrentValue);
			short unsigned int SwappedEndian = (Number>>8) | (Number<<8);

			FileWriter.write(reinterpret_cast<char*>(&SwappedEndian), sizeof(Number));
			if (VerboseMode == 1) { cout << "Unknown Ignored Value: " << Number << endl; }
		}

		if ( CurrentLine.find("Object's Associated Team") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of("x"); /// Gets ,e to the index
		 	CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

		 	// Hex bytes to string!
		 	int8_t Number = strtol(CurrentValue.c_str(), NULL, 16);
		 	WritingData2Bytes[0] = (char)((Number << 0) & 0xFF);
		 	FileWriter.write(WritingData2Bytes, 1);

		 	if (VerboseMode == 1) { cout << "Object's Associated Team: " << CurrentValue << endl; }
		}

		if ( CurrentLine.find("Mystery Object Byte") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of("x"); /// Gets ,e to the index
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

			// Hex bytes to string!
			int8_t Number = strtol(CurrentValue.c_str(), NULL, 16);
			WritingData2Bytes[0] = (char)((Number << 0) & 0xFF);
			FileWriter.write(WritingData2Bytes, 1);

			if (VerboseMode == 1) { cout << "Mystery Object Byte: " << CurrentValue << endl; }
		}

		if (CurrentLine.find("Four Bytes of Mystery") == 0)
		{
			ContentMatchIndex = CurrentLine.find_last_of("x"); /// Gets ,e to the index
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

			// Hex bytes to string!
			unsigned int Number = stoi(CurrentValue);
			unsigned int IntegerToWrite = ((Number>>24)&0xff) | // move byte 3 to byte 0
									 ((Number<<8)&0xff0000) | // move byte 1 to byte 2
									 ((Number>>8)&0xff00) | // move byte 2 to byte 1
									 ((Number<<24)&0xff000000); // byte 0 to byte 3

			FileWriter.write((char *)&IntegerToWrite, 4);

			if (VerboseMode == 1) { cout << "Four Bytes of Mystery: " << CurrentValue << endl; }
		}

		if ( CurrentLine.find("Object Grouping/List") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of("x"); /// Gets ,e to the index
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

			// Hex bytes to string!
			int8_t Number = strtol(CurrentValue.c_str(), NULL, 16);
			WritingData2Bytes[0] = (char)((Number << 0) & 0xFF);
			FileWriter.write(WritingData2Bytes, 1);

			if (VerboseMode == 1) { cout << "Object Grouping/List: " << CurrentValue << endl; }
		}

		if ( CurrentLine.find("Object Type/ID") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of("x"); /// Gets ,e to the index
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

			// Hex bytes to string!
			int8_t Number = strtol(CurrentValue.c_str(), NULL, 16);
			WritingData2Bytes[0] = (char)((Number << 0) & 0xFF);
			FileWriter.write(WritingData2Bytes, 1);

			if (VerboseMode == 1) { cout << "Object Type/ID: " << CurrentValue << endl; }
		}

		if ( CurrentLine.find("Object Linking ID") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of("x"); /// Gets ,e to the index
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

			// Hex bytes to string!
			int8_t Number = strtol(CurrentValue.c_str(), NULL, 16);
			WritingData2Bytes[0] = (char)((Number << 0) & 0xFF);
			FileWriter.write(WritingData2Bytes, 1);

			if (VerboseMode == 1) { cout << "Object Linking ID: " << CurrentValue << endl; }
		}

		if ( CurrentLine.find("Object Load Distance Multiplier") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of("x"); /// Gets ,e to the index
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

			// Hex bytes to string!
			int8_t Number = strtol(CurrentValue.c_str(), NULL, 16);
			WritingData2Bytes[0] = (char)((Number << 0) & 0xFF);
			FileWriter.write(WritingData2Bytes, 1);

			if (VerboseMode == 1) { cout << "Object Load Distance Multiplier: " << CurrentValue << endl; }
		}

		if ( CurrentLine.find("Object Property Section ID") != string::npos)
		{
			ContentMatchIndex = CurrentLine.find_last_of(" "); /// Gets ,e to the index
			CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

			// Hex bytes to string!
			unsigned int Number = stoi(CurrentValue);
			unsigned int IntegerToWrite = ((Number>>24)&0xff) | // move byte 3 to byte 0
									 ((Number<<8)&0xff0000) | // move byte 1 to byte 2
									 ((Number>>8)&0xff00) | // move byte 2 to byte 1
									 ((Number<<24)&0xff000000); // byte 0 to byte 3

			FileWriter.write((char *)&IntegerToWrite, 4);

			if (VerboseMode == 1) { cout << "Object Property Section ID: " << CurrentValue << endl; }
		}

		// Debug, confirm code.
		//if (CurrentLine.find("[Object") != string::npos) { cout << x << endl; x = x + 1; }
	}

	//// MISC SECTION ///

	//// Gets me the current line of the text file.
	while( getline(FileReader,CurrentLine) )
	{

		if (CurrentLine.find("Activator: ") == 0)
		{
		 	ContentMatchIndex = CurrentLine.find_last_of(" "); /// Gets ,e to the index
		 	CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

		 	short unsigned int Number = stoi(CurrentValue); // Get as number
		 	short unsigned int SwappedEndian = (Number>>8) | (Number<<8);

		 	FileWriter.write(reinterpret_cast<char*>(&SwappedEndian), sizeof(Number));
		 	if (VerboseMode == 1) { cout << "Writing Activator: " << Number << endl; }
		}

		if (CurrentLine.find("Entry ID: ") == 0)
		{
		 	ContentMatchIndex = CurrentLine.find_last_of(" "); /// Gets ,e to the index
		 	CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

		 	short unsigned int Number = stoi(CurrentValue); // Get as number
		 	short unsigned int SwappedEndian = (Number>>8) | (Number<<8);

		 	FileWriter.write(reinterpret_cast<char*>(&SwappedEndian), sizeof(Number));
		 	if (VerboseMode == 1) { cout << "Writing Entry ID: " << Number << endl; }
		}

		if (CurrentLine.find("Four Byte Container ") == 0)
		{
		 	ContentMatchIndex = CurrentLine.find_last_of("x"); /// Gets ,e to the index
		 	CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);  /// Gets me past the space

			// Hex2String
			string HexString = CurrentValue;
			unsigned int Number = stoul(HexString, nullptr, 16);

			//Byteswap the integer that is going to be written. It is in the wrong endian.
			unsigned int IntegerToWrite = ((Number>>24)&0xff) | // move byte 3 to byte 0
									 ((Number<<8)&0xff0000) | // move byte 1 to byte 2
									 ((Number>>8)&0xff00) | // move byte 2 to byte 1
									 ((Number<<24)&0xff000000); // byte 0 to byte 3

			FileWriter.write((char*)&IntegerToWrite,sizeof(IntegerToWrite));

		 	if (VerboseMode == 1) { cout << "Writing 4 Byte Container: " << CurrentValue << endl; }
		}
	}
}

int main(int argc, char ** argv)
{
	string InputFile; //Input file
	string OutputFile; //Output file
	int Action = 0; // Extract or compile?

	// Identify the command line passed arguments.
	for(int i = 1; i < argc; i++)
	{
		if (strcmp (argv[i], "--extract") == 0) { Action = 1; }
		if (strcmp (argv[i], "--compile") == 0) { Action = 2; }
		if (strcmp (argv[i], "-i") == 0) { InputFile = argv[i+1]; }
		if (strcmp (argv[i], "-o") == 0) { OutputFile = argv[i+1]; }
		if (strcmp (argv[i], "--objcount") == 0) { ObjectCount = stoi(argv[i+1]); }
		if (strcmp (argv[i], "--verbose") == 0) { VerboseMode = 1; }
	}

	if (VerboseMode == 1) { cout << "Input File Found: " << InputFile << endl; }
	if (VerboseMode == 1) { cout << "Output File: " << OutputFile << endl; }
	if (VerboseMode == 1) { cout << "User Specified Object Count Override: " << ObjectCount << endl; }

	if (Action == 1) { OBJToTXT(InputFile, OutputFile); }
	else if (Action == 2) { TXTToOBJ(InputFile, OutputFile); }
	else
	{
		cout << "\n\nYou have not specified an action. Try running with parameters:" << endl;
		cout << "--extract -i <ObjectLayout> -o <TXTFile>" << endl;
		cout << "--compile -i <TXTFile> -o <ObjectLayout>" << endl;
		cout << "You can also specify the count of main section entry objects to rip info off with --objcount x" << endl;
		cout << "Verbose mode can be activated with --verbose, it prints out to stdout" << endl;
	}

	return 0;
}
