/*
	HeroesBLK, my first C++ program, utility to convert Sonic Heroes'
	visibility binary file formats into human editable text files and back.
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
#include <sstream> //String Stream

// Speedup: Use this on Linux! Search for #DANKMEME.
//#include "byteswap.h" //Woot, mah endians m8

using namespace std; //Standard Namespace

void BLKToTXT(string InputFile, string OutputFile) {
	size_t FileSize = 0; //File size
	char* OutputData;

	ifstream FileReader(InputFile,ios::binary); //Input file stream to read BLK file.
	if (! FileReader) { cout << "Error opening file" << endl; } else { cout << "Successfully opened visibility file,\n" << endl; }

	FileReader.seekg(0, ios::end); // Set the pointer to the end
	FileSize = FileReader.tellg(); // Get the length of the file
	cout << "Size of file: " << FileSize << " Bytes" << endl;; // Print size of file
	FileReader.seekg(0, ios::beg); // Set the pointer to the beginning

	OutputData = new char[FileSize + 1]; // Char array to hold the string.
	FileReader.read( OutputData, FileSize ); // Read the file into the OutputData

	int ChunkCount = 1; // Iterates up for each chunk

	char* ChunkIDHex;
	ChunkIDHex = new char[1]; // Holds character of 1 byte length

	int MinimumX;
	int MinimumY;
	int MinimumZ;
	int MaximumX;
	int MaximumY;
	int MaximumZ;

	char FourByteStorage [4]; // Temporarily will store 4 bytes of data

	// Sets up the output of the new file as it will be given.
	ofstream FileWriter(OutputFile);
	if (FileWriter.is_open()){ cout << "File successfully created for writing.\n" << endl; }

	for(int x = 0; x < FileSize;)
	{
		sprintf(ChunkIDHex,"%02X",(unsigned int)(unsigned char)OutputData[x+3]); // Put the ID into the Chunk ID Hexadecimal String

		FourByteStorage[0] = OutputData[x+7]; FourByteStorage[1] = OutputData[x+6]; FourByteStorage[2] = OutputData[x+5]; FourByteStorage[3] = OutputData[x+4];
		MinimumX = *(int *)FourByteStorage;

		FourByteStorage[0] = OutputData[x+11]; FourByteStorage[1] = OutputData[x+10]; FourByteStorage[2] = OutputData[x+9]; FourByteStorage[3] = OutputData[x+8];
		MinimumY = *(int *)FourByteStorage;

		FourByteStorage[0] = OutputData[x+15]; FourByteStorage[1] = OutputData[x+14]; FourByteStorage[2] = OutputData[x+13]; FourByteStorage[3] = OutputData[x+12];
		MinimumZ = *(int *)FourByteStorage;

		FourByteStorage[0] = OutputData[x+19]; FourByteStorage[1] = OutputData[x+18]; FourByteStorage[2] = OutputData[x+17]; FourByteStorage[3] = OutputData[x+16];
		MaximumX = *(int *)FourByteStorage;

		FourByteStorage[0] = OutputData[x+23]; FourByteStorage[1] = OutputData[x+22]; FourByteStorage[2] = OutputData[x+21]; FourByteStorage[3] = OutputData[x+20];
		MaximumY = *(int *)FourByteStorage;

		FourByteStorage[0] = OutputData[x+27]; FourByteStorage[1] = OutputData[x+26]; FourByteStorage[2] = OutputData[x+25]; FourByteStorage[3] = OutputData[x+24];
		MaximumZ = *(int *)FourByteStorage;

		cout << "[Chunk " << dec << ChunkCount << "]" << endl; FileWriter << "[Chunk " << dec << ChunkCount << "]" << "\n";
		cout << "Visibility Chunk ID: " << ChunkIDHex << endl; FileWriter << "Visibility Chunk ID: " << ChunkIDHex << "\n";
		cout << "Minimum X Coordinate: " << MinimumX << endl;	FileWriter << "Minimum X Coordinate: " << MinimumX << "\n";
		cout << "Minimum Y Coordinate: " << MinimumY << endl; FileWriter << "Minimum Y Coordinate: " << MinimumY << "\n";
		cout << "Minimum Z Coordinate: " << MinimumZ << endl; FileWriter << "Minimum Z Coordinate: " << MinimumZ << "\n";

		cout << "Maximum X Coordinate: " << MaximumX << endl; FileWriter << "Maximum X Coordinate: " << MaximumX << "\n";
		cout << "Maximum Y Coordinate: " << MaximumY << endl; FileWriter << "Maximum Y Coordinate: " << MaximumY << "\n";
		cout << "Maximum Z Coordinate: " << MaximumZ << endl; FileWriter << "Maximum Z Coordinate: " << MaximumZ << "\n";

		// DEBUG
		//cout << MinimumX << endl;
		//cout << "X1:" << (signed int)OutputData[x+4] << " X2:" << (signed int)OutputData[x+5] << " X3:" << (signed int)OutputData[x+6] << " X4:" << (signed int)OutputData[x+7] << endl;
		cout << endl; FileWriter << "\n";


		ChunkCount += 1;
		x += 28; // Length of each visibility entry;
	}
}


void TXTToBLK(string InputFile, string OutputFile) {
	ifstream FileReader(InputFile); //Input file stream to read BLK file.
	if (! FileReader) { cout << "Error opening input file" << endl; } else { cout << "Successfully opened text file,\n" << endl; }

	// Sets up the output of the new file as it will be given.
	ofstream FileWriter(OutputFile, ios::out | ios::binary);
	if (FileWriter.is_open()){ cout << "File successfully created for writing.\n" << endl; }

	string CurrentLine;
	string CurrentValue;
	size_t ContentMatchIndex;

	// Prepare writer for writing binary
	char WritingData[4];

	while( getline(FileReader,CurrentLine) )
	{
		if (CurrentLine.find("Visibility Chunk ID: ") == 0)
		{
			 ContentMatchIndex = CurrentLine.find_last_of(" ");
			 CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);

			 int32_t Number = strtol(CurrentValue.c_str(), NULL, 16);
			 WritingData[3] = (char)(Number & 0xFF);
			 WritingData[2] = (char)((Number << 8) & 0xFF);
			 WritingData[1] = (char)((Number << 16) & 0xFF);
			 WritingData[0] = (char)((Number << 24) & 0xFF);
			 FileWriter.write(WritingData, 4);
			 cout << "Writing Visibility; " << Number << endl;
		}

		if (CurrentLine.find("Coordinate:") != string::npos)
		{
			 ContentMatchIndex = CurrentLine.find_last_of(" ");
			 CurrentValue = CurrentLine.substr(ContentMatchIndex + 1);

			 signed int IntegerToWrite = stoi(CurrentValue);
			 cout << "Writing Coordinate: " << IntegerToWrite << endl;

			 //Byteswap the integer that is going to be written. It is in the wrong endian.

			 IntegerToWrite = ((IntegerToWrite>>24)&0xff) | // move byte 3 to byte 0
                    ((IntegerToWrite<<8)&0xff0000) | // move byte 1 to byte 2
                    ((IntegerToWrite>>8)&0xff00) | // move byte 2 to byte 1
                    ((IntegerToWrite<<24)&0xff000000); // byte 0 to byte 3

			 FileWriter.write((char*)&IntegerToWrite,sizeof(IntegerToWrite));

			 // #DANKMEME uncomment and inclde library for speedup.
			 //IntegerToWrite = __bswap_32(IntegerToWrite);
			 //FileWriter.write((char*)&IntegerToWrite,sizeof(IntegerToWrite));
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
		if (strcmp (argv[i], "-i") == 0) { InputFile = argv[i+1]; cout << "Input File Found: " << InputFile << endl; }
		if (strcmp (argv[i], "-o") == 0) { OutputFile = argv[i+1]; cout << "Output File: " << OutputFile << endl; }
	}

	if (Action == 1) { BLKToTXT(InputFile, OutputFile); }
	else if (Action == 2) { TXTToBLK(InputFile, OutputFile); }
	else
	{
		cout << "\n\nYou have not specified an action. Try running with parameters:" << endl;
		cout << "--extract -i <BLKFile> -o <TXTFile>" << endl;
		cout << "--compile -i <TXTFile> -o <BLKFile>" << endl;
	}

	return 0;

}
