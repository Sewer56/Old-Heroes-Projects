# *HeroesCollisionModifier*

A simple, quickly written utility in order to test the collision properties for triangles in Sonic Heroes. Overrides all of the triangles flawlessly without fail.

Note:
In source code set the bytes you want to have the triangles replaced to between lines 81 to 91.

Demo:
https://www.youtube.com/watch?v=gX9fwtzPqLg

-----------------------

Here's some notes on the format, straight, pasted from own notes:

The 'flags' as described on the wiki are actually each unsigned bytes for each certain property but it seems that having a value > 0x80 seems to have null effect.

They ship in the format:
XX XX XX XX YY YY YY YY

Where XX XX XX XX is Property Set #1 && YY YY YY YY is Property Set #2 (which is overriden by Property Set #1, where applicable).

Here's the struct of these properties

1 Byte | // 'Pinballization' 
         // 40 min pinball. 80 min bingo. 
         // Balls only in STG05/06  
         // 60 seems to make pinballs go fast
         
1 Byte | // Block X movement on collision cut.  (Gotta NOT go fast)
1 Byte | // Block Y movement on collision cut.  (Gotta NOT go up)
1 Byte | // Block Z movement on collison cut.   (Gotta NOT turn)
__________________
Examples of Block X/Y/Z Logic & Pinball
    
=> Default (as shipped here)
`60 00 00 00`
Allows for fast pinball, especially on Pinball tables, works only on Stage 05 and 06.
	
=> Go Full Pinball Mode Everywhere
`80 00 00 00`
(Works only on Casino Park/Bingo Highway due to .REL (GC)/.exe (PC) restrictions)

=> Floor (Sand)
`00 00 00 10`
Tries to prevent player from turning sideways while running forward.

=> Floor (Sarpet Staircase) 
`00 00 04 00`
Prevents the player from going up, acting as fake gravity, slowing their ascend.

=> Walls (Most)
`00 08 00 00`
Slow the player's X/horizontal speed down on collision.

=> Fall Through Horizontal (but not vertical) Floors
`00 00 80 00`
This works because the game tries to stop you from going vertically while touching the ground, but gravity still will try to pull you down and will succeed at doing so for a brief period until the game checks for collision again, but because Sonic/Player is blocked in the Y axis on collision, he cannot be pushed up properly to stand on the floor again, he will seamlessly fall through the floor (or like quicksand, depending on your value).

These slowdowns are relative to player rotation, i.e. X is always Sonic's/player's forward direction and Z always their side direction etc.

# Old Ideas on how I thought it worked at one point
________________________
Keeping those for the interested of what I initially thought...

For 'Block Y Movement';
    Old Ideas:
    // 00 Completely Flat to 05 Goes Slow on Vertical? Y Collision Offset Power? Block Y?
    // Amount of transfer from vertical to horizontal when hitting horizontal?
    // Changes how sunk in on collision horizontal.
    
For 'Block Z Movement';    
    Old Ideas:
    //00 Completely Flat to 05 Goes Slow on Horizontal? 
    // Amount of transfer from horizontal into vertical when hitting vertical walls/edges. 
    // Z Collision Offset Power
