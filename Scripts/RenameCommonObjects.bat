ren bob "Bobsled"
ren bobF "French"
ren bobG "German"
ren bobI "Italian"
ren bobS "Spanish"
move /Y "French" "Bobsled\"
move /Y "German" "Bobsled\"
move /Y "Italian" "Bobsled\"
move /Y "Spanish" "Bobsled\"

ren comobj "Common SET Placeable Objects"
ren stgmem0910 "Stage 09~10 - Frog Forest~Lost Jungle - Level Specific Object Model Copies"

mkdir "Stage 06 - BINGO Highway - Lighting Strip Decoration Models"
for /D %%d in (stg06_kw*) do (move "%%d" "Stage 06 - BINGO Highway - Lighting Strip Decoration Models\")

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