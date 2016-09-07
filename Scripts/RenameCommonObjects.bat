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