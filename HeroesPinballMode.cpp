/*
	HeroesBLK, my third C++ program, utility to decompress Shadow The Hedgehog's
	Motion Packages .MTP, archives into raw Renderware .anm(s).
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
#include <string> // String
#include <cstring> //Strings
#include <sstream> //String Stream
#include <vector> //Vector the crocodiles

#include <dirent.h> // Directory Entities

// Speedup: Use this on Linux! Search for #DANKMEME.
//#include "byteswap.h" //Woot, mah endians m8

using namespace std; //Standard Namespace

void CLGoSlippy(string InputFile, string OutputFile) {

	// File size
	size_t FileSize = 0;

	// Offset to all them cute triangles.
	unsigned int TriangleSectionOffset;
	unsigned int TriangleEntryConstant = 0x20;
	unsigned int VectorSectionOffset;

	// Just for storing individual bytes, makes my head feel more comfortable.
	char* FourByteBuffer; FourByteBuffer = new char[4];

	// A copy of the file will be stored here.
	char* CLFileData;

	// Load the file.
	ifstream FileReader(InputFile,ios::binary);

	// Load file and get file size ==>
	// Validates if reading of the file has been successful.
	if (! FileReader) { cout << "Error opening file" << endl; } else { cout << "Successfully opened object file\n" << endl; }
	FileReader.seekg(0, ios::end); // Set the pointer to the end
	FileSize = FileReader.tellg(); // Get the pointer location
	FileReader.seekg(0, ios::beg); // Set the pointer to the beginning

	// Allocates enough room for the MTP file.
	CLFileData = new char[FileSize + 1];

	// Read the file into the array.
	FileReader.read(CLFileData, FileSize);

	// This will get us our triangle section entry address
	FourByteBuffer[0] = CLFileData[11]; FourByteBuffer[1] = CLFileData[10]; FourByteBuffer[2] = CLFileData[9]; FourByteBuffer[3] = CLFileData[8];
	TriangleSectionOffset = *(unsigned int *)FourByteBuffer;
	// This will get our triangle section end address
	FourByteBuffer[0] = CLFileData[15]; FourByteBuffer[1] = CLFileData[14]; FourByteBuffer[2] = CLFileData[13]; FourByteBuffer[3] = CLFileData[12];
	VectorSectionOffset = *(unsigned int *)FourByteBuffer;

	cout << TriangleSectionOffset << endl;
	cout << VectorSectionOffset << "\n" << endl;

	for ( unsigned int x = TriangleSectionOffset; x < VectorSectionOffset;)
	{
		// [1] is 3rd from left
		CLFileData[x + 27] = 0x00;
		CLFileData[x + 26] = 0x00;
		CLFileData[x + 25] = 0x00;
		CLFileData[x + 24] = 0x60;

		/* Feeling like it? Override 2nd property for max lolz */

		CLFileData[x + 31] = 0x00;
		CLFileData[x + 30] = 0x00;
		CLFileData[x + 29] = 0x00;
		CLFileData[x + 28] = 0x60;

		// Go to next triangle.
		x += TriangleEntryConstant;
	}

	FileReader.close();

	// Write using ofstream to same file
	ofstream CLFile;
	// Opens the outputstream for binary writing
	CLFile.open(OutputFile,ios::binary);

	for (unsigned int x = 0; x < FileSize;)
	{
		// Push the current byte to file.
		CLFile << CLFileData[x];
		// Add a byte to loop.
		x++;
	}
}

int main(int argc, char ** argv)
{
	string InputFile; //Input file
	string OutputFile; //Output file

	// Take first argument straight as intended.
	for(int i = 1; i < argc; i++)
	{
		InputFile = argv[i]; OutputFile = InputFile;
	}

	CLGoSlippy(InputFile, OutputFile);
	return 0;
}
