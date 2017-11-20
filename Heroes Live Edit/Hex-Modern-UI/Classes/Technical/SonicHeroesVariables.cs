using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex_Modern_UI
{
    /// <summary>
    /// All of these pointers are NOT relative to the main module. Thus you may need to apply a 0x400000 base offset.
    /// </summary>
    public static class SonicHeroesVariables
    {
        public enum Characters
        {
            CurrentPlayerControlledCharacter_Pointer = 0x009CE820, // This is a pointer to the information about the current character the player is controlling.
        }
        public enum GameControl
        {
            IsCameraMovementEnabled = 0x00A69880,
            AreShaderEffectsEnabled = 0x008E2050, // 0 to disable FX
            AmICurrentlyInAStage = 0x008E2028, // 1 if in a level, 255 or 0 if in menus
            ScoreRequirementMultiplier = 0x435C45, // Multiplies score requirements by 100
        }

        public enum CharacterPointerOffsets
        {
            XVelocity = 0xDC,
            YVelocity = 0xE0,
            ZVelocity = 0xE4,
            XPosition = 0xE8,
            YPosition = 0xEC,
            ZPosition = 0xF0,
            XRotation = 0xF8,
            YRotation = 0xFC,
            ZRotation = 0x100,
            XCharacterThickness = 0x104,
            YCharacterThickness = 0x108,
            TeamMatesFollowingSomething = 0x10C,
            TeamMatesFollowingSomething2 = 0x110,
            TeamMatesFollowingSomething3 = 0x114
        }

        public struct CharacterWarpInformation
        {
            public float XVelocity;
            public float YVelocity;
            public float ZVelocity;
            public float XPosition;
            public float YPosition;
            public float ZPosition;
            public ushort XRotation;
            public ushort YRotation;
            public ushort ZRotation;
            public float XCharacterThickness;
            public float YCharacterThickness;
            public float TeamMatesFollowingSomething;
            public float TeamMatesFollowingSomething2;
            public float TeamMatesFollowingSomething3;
            /// <summary>
            /// Application specific - used for functionality
            /// </summary>
            public string CharacterWarpName;
        }

        public enum FramerateToggles
        {
            NonFocusedFPSCap = 0x007C9284, // This decides whether to limit the FPS of the game when the game is not the active window.
        }

        public struct FramerateToggleInformation
        {
            byte NonFocusedFPSCap;
        }

        public enum StageChoices
        {
            PlayerStageChoiceMainMenu = 0x008D6720,
            // This is the level the player selects in Challenge Mode or any other Main Menu Screens. 
            // It also dictates which level plate is shown onscreen.

            PlayerStageIDToLoad = 0x008D6710, // This is the Stage that will actually be loaded ingame.
        }

        public struct StageControl
        {
            byte PlayerStageChoiceMainMenu;
            byte PlayerStageIDToLoad;
        }

        public enum StageIDs
        {
            TestLevel = 0x00,
            Unknown = 0x01,
            SeasideHill = 0x2,
            OceanPalace = 0x3,
            GrandMetropolis = 0x4,
            PowerPlant = 0x5,
            CasinoPark = 0x6,
            BingoHighway = 0x7,
            RailCanyon = 0x8,
            BulletStation = 0x9,
            FrogForest = 10,
            LostJungle = 11,
            HangCastle = 12,
            MysticMansion = 13,
            EggFleet = 14,
            FinalFortress = 15,

            //TODO: Verify those below
            EggHawk = 16,
            TeamBattle1 = 17,
            RobotCarnival = 18,
            EggAlbatross = 19,
            TeamBattle2 = 20,
            RobotStorm = 21,
            EggEmperor = 22,
            MetalMadness = 23,
            MetalOverlord = 24,
            SeaGate = 25,
            SeasideCourse = 26,
            CityCourse = 27,
            CasinoCourse = 28,
            BonusStage1 = 29,
            BonusStage2 = 30,
            BonusStage3 = 31,
            BonusStage4 = 32,
            BonusStage5 = 33,
            BonusStage6 = 34,
            BonusStage7 = 35,
            RailCanyonChaotix = 36,
            SeasideHill2PRace = 37,
            GrandMetropolis2PRace = 38,
            BINGOHighway2PRace = 39,
            CityTopBattle = 40,
            CasinoRingBattle = 41,
            TurtleShellBattle = 42,
            EggTreat = 43,
            PinballMatch = 44,
            HotElevator = 45,
            RoadRock = 46,
            MadExpress = 47,
            TerrorHall = 48,
            RailCanyonExpert = 49,
            FrogForestExpert = 50,
            EggFleetExpert = 51,
            EmeraldChallenge1 = 52,
            EmeraldChallenge2 = 53,
            EmeraldChallenge3 = 54,
            EmeraldChallenge4 = 55,
            EmeraldChallenge5 = 56,
            EmeraldChallenge6 = 57,
            EmeraldChallenge7 = 58,
            SpecialStageMultiplayer1 = 59,
            SpecialStageMultiplayer2 = 60,
            SpecialStageMultiplayer3 = 61,
        }

        public enum GameMenus
        {
            // Pointer
            MenuAnimationEntries_BasePointer = 0x00A777B4,

            // Unsigned Byte
            PauseMenu_Selection = 0x008D6930,
            PauseMenu_Options_Selection = 0x008D6934,
            PauseMenu_Options_GamePadConfiguration_Selection = 0x008D6938,
            PauseMenu_Options_CameraMode_Selection = 0x008D693C,
        }

        // Type: Unsigned Byte
        public enum GameMenus_MenuEntries_Offsets // These are offsets for GameMenus enumerable MenuAnimationEntries_BasePointer
        {
            Main_Menu_Animation = 0x124, // Fun fact, freeze this, press an unintended button and next menu will load with theme of menu set!
            Main_Menu_Selection = 0x468,

            CG_Menu_Animation = 0x110, // Shared with 1P Play
            CG_Menu_Selection = 0x198,
            OnePlayer_Play_Animation = 0x110,
            OnePlayer_Play_Selection = 0x1AC,

            Extra_Menu_Animation = 0x118, // Shared with Vibration Setting Menu
            Extra_Menu_Selection = 0x2C4,
            Vibration_Setting_Current_Choice = 0x23C,

            Language_Setting_Animation = 0x11C,
            Language_Setting_Selection = 0x2C8,

            File_Select_Selection_And_Animation = 0x130,
            Options_Menu_Selection_And_Animation = 0x3E0,

            Audio_Menu_Selection = 0x194,
            Audio_Menu_Animation = 0x228,

            Audio_Menu_Subcategory_Selection = 0x220,
            Audio_Menu_Subcategory_Animation = 0x22C,

            TwoPlayer_Menu_Selection = 0x198,
            TwoPlayer_Menu_Animation = 0x2B8,

            TwoPlayer_P1_Team_Menu_Selection = 0x2C4,
            TwoPlayer_P2_Team_Menu_Selection = 0x3DC,

            TwoPlayer_P1_Team_Menu_Animation = 0x350,
            TwoPlayer_P2_Team_Menu_Animation = 0x2C8,

            TwoPlayer_Stage_Selection = 0x2B0,
            TwoPlayer_Stage_Animation = 0x2C0,

            ChallengeMode_Selection = 0x194,
            ChallengeMode_Animation = 0x2B4,

            ChallengeMode_Team_Selection = 0x220,
            ChallengeMode_Team_Animation = 0x2B8,

            ChallengeMode_Mission_Selection = 0x2AC,
            ChallengeMode_Mission_Animation = 0x2BC,

            Story_Select_Menu_Selection = 0x194, // Shared with Challenge Mode
            Story_Select_Menu_Animation = 0x19C,

            Story_Select_Submenu_Selection = 0x234,
            Story_Select_Submenu_Animation = 0x1A0,

            File_Select_Start_Select_Cancel_Selection = 0x518,
            File_Select_Start_Select_Cancel_Selection_Animation = 0x51A,

            Exit_Game_Yes_No_Menu_Selection = 0x678,
            Super_Hard_Mode_Menu_Selection = 0x678,
        }

        // What values the game uses to determine camera correctly
        public enum CameraMathematics
        {
            IsCameraInAutomaticSegment1 = 0x00A606B8, // Can help unlock camera in locked segments
            IsCameraInAutomaticSegment2 = 0x00A606BC, // Can help unlock camera in locked segments
            CameraXPosition_AngleCalculations = 0x00A606C0, // Used for Angle Calculations
            CameraYPosition_AngleCalculations = 0x00A606C4, // Used for Angle Calculations
            CameraZPosition_AngleCalculations = 0x00A606C8, // Used for Angle Calculations
            Unknown_AlmostAlwaysMax = 0x00A606D0,
            TriggerRotationCameraOffset = 0x00A606F0, // When pressing L/R Triggers
            Unknown_GenerallyNull = 0x00A606F4,
            AmIInFreeOrAutocam = 0x00A606F4, // Not the setting in settings menu, this will let you control camera anywhere.
            AmountOfFramesWithOwnCameraInAutocamSegments = 0x00A60700, // Where a camera is defined, this tracks the amount of time since you start to move camera yourself until it returns to game's autocam
        }

        public struct CameraMathematicsStruct
        {
            byte IsCameraInAutomaticSegment1; // Can help unlock camera in locked segments
            byte IsCameraInAutomaticSegment2; // Can help unlock camera in locked segments
            float CameraXPosition_AngleCalculations; // Used for Angle Calculations
            float CameraYPosition_AngleCalculations; // Used for Angle Calculations
            float CameraZPosition_AngleCalculations; // Used for Angle Calculations
            int Unknown_AlmostAlwaysMax;
            float TriggerRotationCameraOffset; // When pressing L/R Triggers
            int Unknown_GenerallyNull;
            float AmIInFreeOrAutocam; // Not the setting in settings menu, this will let you control camera anywhere.
            ushort AmountOfFramesWithOwnCameraInAutocamSegments; // Where a camera is defined, this tracks the amount of time since you start to move camera yourself until it returns to game's autocam
        }

        public enum CurrentCamera
        {
            CameraX_OpposingCharMovement = 0x00A60BF0,
            CameraY_OpposingCharMovement = 0x00A60BF4,
            CameraZ_OpposingCharMovement = 0x00A60BF8,
            Camera_Unknown_X = 0x00A60BFC,
            Camera_Unknown_Y = 0x00A60C00,
            Camera_Unknown_Z = 0x00A60C04,
            Camera_X = 0x00A60C30,
            Camera_Y = 0x00A60C34,
            Camera_Z = 0x00A60C38,
            Camera_Angle_Vertical_BAMS = 0x00A60C3C,
            Camera_Angle_Unknown_BAMS = 0x00A60C3E,
            Camera_Angle_Horizontal_BAMS = 0x00A60C40,
            Camera_Unknown2_X = 0x00A60C48,
            Camera_Unknown2_Y = 0x00A60C4C,
            Camera_Unknown2_Z = 0x00A60C50,
            Camera_ObjectPointedTowards_X = 0x00A60C54,
            Camera_ObjectPointedTowards_Y = 0x00A60C58,
            Camera_ObjectPointedTowards_Z = 0x00A60C5C,
            Camera_Enabled = 0x00A69880,
        }

        public struct CurrentCameraStruct
        {
            float CameraX_OpposingCharMovement; // Freezing makes camera oppose character 2D position on screen
            float CameraY_OpposintCharMovement; // Freezing makes camera oppose character 2D position on screen
            float CameraZ_OpposingCharMovement; // Freezing makes camera oppose character 2D position on screen
            float Camera_Unknown_X;
            float Camera_Unknown_Y;
            float Camera_Unknown_Z;
            float Camera_X; // Physical Camera Location | Functions when Cam Disabled
            float Camera_Y; // Physical Camera Location | Functions when Cam Disabled
            float Camera_Z; // Physical Camera Location | Functions when Cam Disabled
            ushort Camera_Angle_Vertical_BAMS; // Physical Camera Angle | Functions when Cam Disabled
            ushort Camera_Angle_Unknown_BAMS; // Physical Camera Angle | Functions when Cam Disabled
            ushort Camera_Angle_Horizontal_BAMS; // Physical Camera Angle | Functions when Cam Disabled
            float Camera_Unknown2_X;
            float Camera_Unknown2_Y;
            float Camera_Unknown2_Z;
            float Camera_ObjectPointedTowards_X; // Works when camera enabled
            float Camera_ObjectPointedTowards_Y; // Works when camera enabled
            float Camera_ObjectPointedTowards_Z; // Works when camera enabled
            byte Camera_Enabled; // Completely disables/enables camera
        }

        // Maximum 128 Minutes!
        public enum TimeTrialLimits
        {
            SeasideHill = 0xBC6A68,
            OceanPalace = 0xBC6A70,
            GrandMetropolis = 0xBC6A78,
            PowerPlant = 0xBC6A80,
            CasinoPark = 0xBC6A88,
            BingoHighway = 0xBC6A90,
            RailCanyon = 0xBC6A98,
            BulletStation = 0xBC6AA0,
            FrogForest = 0xBC6AA8,
            LostJungle = 0xBC6AB0,
            HangCastle = 0xBC6AB8,
            MysticMansion = 0xBC6AC0,
            EggFleet = 0xBC6AC8,
            FinalFortress = 0xBC6AD0,
            GrandMetropolis_Chaotix = 0xBC6AD8,
            RailCanyon_Chaotix = 0xBC6AE0,
            RailCanyon_ChaotixVersion_Chaotix = 0xBC6AE8,
            FrogForest_Chaotix = 0xBC6AF0,
        }

        public enum TimeTrialLimitOffset
        {
            LevelID = 0x0,
            TeamID = 0x4,
            NumberOfMinutes = 0x5,
        }

        public struct TimeTrialLimitsStruct
        {
            byte LevelID;
            byte TeamID;
            SByte NumberOfMinutes;
        }

        public enum TeamDarkRoseExtraMissions
        {
            NumberOfEnemiesToKill = 0x5A9993,
            NumberOfEnemiesToKillVisual = 0x5A9984,
            RoseRingCount = 0x5A9E0F,
        }

        public struct TeamDarkRobotChallengeStruct
        {
            //TODO: Test, see below
            byte NumberOfEnemiesToKill; // Could be an int, did not test.
            byte NumberOfEnemiesToKillVisual;
            byte RoseRingCountExtraMission; // Could be an int, did not test.
        }

        public enum TeamChaotixExtraMissions
        {
            HermitCrabs = 0x8BF420,
            ChaoRescue = 0x8BF428,
            KillRobots = 0x8BF430, // Nothing Here
            DestroyGoldTurtles = 0x8BF438,

            RingCollection = 0x8BF440, // Unused
            RingCollectionNew = 0x5A9DE5, //Used

            CasinoChipCollection = 0x8BF448,

            ReachTheTerminal = 0x8BF450, // Unused

            DestroyTheCapsules = 0x8BF458,

            FinishUndetected = 0x8BF460, // Unused

            CollectTheChao = 0x8BF468,

            CollectTheCastleKeys = 0x8BF470, // Unused
            LightTheMansionTorches = 0x8BF478, // Unused
            EscapeTheEggmanFleet = 0x8BF480, // Unused

            CollectTheEggmanKeys = 0x8BF488,
        }

        public enum TeamChaotixExtraMissionsOffset
        {
            AmountRequired = 0x0,
            AmountRequiredExtra = 0x4,
        }

        public struct TeamChaotixExtraMissionsValues
        {
            byte AmountRequired;
            byte AmountRequiredExtra;
        }

        public enum TeamChaotixMissionTypeAddresses
        {
            SeasideHill = 0x4020AC,
            OceanPalace = 0x4020AD,
            GrandMetropolis = 0x4020AE,
            PowerPlant = 0x4020AF,
            CasinoPark = 0x4020B0,
            BingoHighway = 0x4020B1,
            RailCanyon = 0x4020B2,
            BulletStation = 0x4020B3,
            FrogForest = 0x4020B4,
            LostJungle = 0x4020B5,
            HangCastle = 0x4020B6,
            MysticMansion = 0x4020B7,
            EggFleet = 0x4020B8,
            FinalFortress = 0x4020B9,
        }

        public enum TeamChaotixMissionTypeParameters
        {
            ItemCollect = 0x00,
            ItemCollectExtraNoDetection = 0x01,
            RobotCleanup = 0x02,
            RingCollect = 0x03,
            RegularMission = 0x04,
            RegularMissionNoFrogs = 0x05,
            RegularMissionNoRobots = 0x06,
        }

        public struct TeamChaotixMissionTypeParametersStruct
        {
            byte MissionType;
        }

        public enum GoalRingStateChaotixAddresses
        {

            TestLevel = 0x7D0C50,
            SeasideHill = 0x7D0C54,
            OceanPalace = 0x7D0C58,
            GrandMetropolis = 0x7D0C5C,
            PowerPlant = 0x7D0C60,
            CasinoPark = 0x7D0C64,
            BingoHighway = 0x7D0C68,
            RailCanyon = 0x7D0C6C,
            BulletStation = 0x7D0C70,
            FrogForest = 0x7D0C74,
            LostJungle = 0x7D0C78,
            HangCastle = 0x7D0C7C,
            MysticMansion = 0x7D0C80,
            EggFleet = 0x7D0C84,
            FinalFortress = 0x7D0C88,

            //TODO: Verify those below
            EggHawk = 0x7D0C8C,
            TeamBattle1 = 0x7D0C90,
            RobotCarnival = 0x7D0C94,
            EggAlbatross = 0x7D0C98,
            TeamBattle2 = 0x7D0C9C,
            RobotStorm = 0x7D0CA0,
            EggEmperor = 0x7D0CA4,
            MetalMadness = 0x7D0CA8,
            MetalOverlord = 0x7D0CAC,
            SeaGate = 0x7D0CB0,
            SeasideCourse = 0x7D0CB4,
            CityCourse = 0x7D0CB8,
            CasinoCourse = 0x7D0CBC,
            BonusStage1 = 0x7D0CC0,
            BonusStage2 = 0x7D0CC4,
            BonusStage3 = 0x7D0CC8,
            BonusStage4 = 0x7D0CCA,
            BonusStage5 = 0x7D0CD0,
            BonusStage6 = 0x7D0CD4,
            BonusStage7 = 0x7D0CD8,
            RailCanyonChaotix = 0x7D0CDC,
            SeasideHill2PRace = 0x7D0CE0,
            GrandMetropolis2PRace = 0x7D0CE4,
            BINGOHighway2PRace = 0x7D0CE8,
            CityTopBattle = 0x7D0CEC,
            CasinoRingBattle = 0x7D0CF0,
            TurtleShellBattle = 0x7D0CF4,
            EggTreat = 0x7D0CF8,
            PinballMatch = 0x7D0CFC,
            HotElevator = 0x7D0D00,
            RoadRock = 0x7D0D04,
            MadExpress = 0x7D0D08,
            TerrorHall = 0x7D0D0C,
            RailCanyonExpert = 0x7D0D10,
            FrogForestExpert = 0x7D0D14,
            EggFleetExpert = 0x7D0D18,
            EmeraldChallenge1 = 0x7D0D1C,
            EmeraldChallenge2 = 0x7D0D20,
            EmeraldChallenge3 = 0x7D0D24,
            EmeraldChallenge4 = 0x7D0D28,
            EmeraldChallenge5 = 0x7D0D2C,
            EmeraldChallenge6 = 0x7D0D30,
            EmeraldChallenge7 = 0x7D0D34,
            SpecialStageMultiplayer1 = 0x7D0D38,
            SpecialStageMultiplayer2 = 0x7D0D3C,
            SpecialStageMultiplayer3 = 0x7D0D40,
        }

        public enum GoalRingStatesChaotix
        {
            GoalRing = 0x00,
            RestartRing = 0x01,
        }

        public struct GoalRingStatesChaotixStruct
        {
            byte GoalRingState;
        }

        public enum TeamBattleStagesOpponentStages
        {
            CityTopLevel = 0x8DD514,
            ForestLevel = 0x8DD5A0,
        }

        public enum TeamBattleStagesOpponentOffsets
        {
            TeamVersusSonic = 0x00,
            TeamVersusDark = 0x04,
            TeamVersusRose = 0x08,
            TeamVersusChaotix = 0x0C,
        }

        public enum TeamBattleStagesTeams
        {
            TeamSonic = 0x00,
            TeamDark = 0x01,
            TeamRose = 0x02,
            TeamChaotix = 0x03,
        }

        public struct TeamBattleStageConfigurations
        {
            byte TeamToFightAgainst;
        }

        // These are pointers to animation files, swap pointers to swap animations!
        public enum TeamBattleStagesBossAnimationHUDPointers
        {
            CityTopTeamRosePointer = 0x8DCD38,
            CityTopTeamChaotixPointer = 0x8DCD3C,
            CityTopTeamSonicPointer = 0x8DCD40,
            CityTopTeamDarkPointer = 0x8DCD44,
            ForestTeamRosePointer = 0x8DCD48,
            ForestTeamChaotixPointer = 0x8DCD4C,
            ForestTeamSonicPointer = 0x8DCD50,
            ForestTeamDarkPointer = 0x8DCD54,
        }

        public enum TeamBattleStagesBossAnimationTextPointers
        {
            CityTopTeamRosePointer = 0x8DCF84,
            CityTopTeamChaotixPointer = 0x8DCF88,
            CityTopTeamSonicPointer = 0x8DCF8C,
            CityTopTeamDarkPointer = 0x8DCF90,
            ForestTeamRosePointer = 0x8DCF94,
            ForestTeamChaotixPointer = 0x8DCF98,
            ForestTeamSonicPointer = 0x8DCF9C,
            ForestTeamDarkPointer = 0x8DCFA0,
        }

        public enum TeamBattleStagesBossMenuTextPointers
        {
            CityTopTeamRosePointer = 0x44BCAE,
            CityTopTeamChaotixPointer = 0x44BCBF,
            CityTopTeamSonicPointer = 0x44BCD1,
            CityTopTeamDarkPointer = 0x44BCE3,
            ForestTeamRosePointer = 0x44BD1D,
            ForestTeamChaotixPointer = 0x44BD2C,
            ForestTeamSonicPointer = 0x44BD3A,
            ForestTeamDarkPointer = 0x44BD49,
        }

        public struct TeamBattleStagesBossHUDPointer
        {
            IntPtr PointerToAnimation;
        }

        public enum SuperSonicASMInjectionAddresses
        {
            SuperSonic1 = 0x5CBEB9,
            SuperSonic2 = 0x5CC0EA,
            SuperSonic3 = 0x5CBFDF,

            SuperTails1 = 0x5B7FDD,
            SuperTails2 = 0x5B7DE9,
            SuperTails3 = 0x5B7ECB,

            SuperKnuckles1 = 0x5C1D6B,
            SuperKnuckles2 = 0x5C1E52,
            SuperKnuckles3 = 0x5C1E7A,
        }

        public class SuperSonicASMInjectionAddressesCode
        {
            byte[] SuperSonic1 = new byte[7] { 0xC6, 0x86, 0xC2, 0x00, 0x00, 0x00, 0x01 };
            byte[] SuperSonic2 = new byte[5] { 0xE8, 0xCA, 0xFD, 0xFF, 0xFF };
            byte[] SuperSonic3 = new byte[7] { 0x80, 0xBE, 0xC2, 0x00, 0x00, 0x00, 0x01 };

            byte[] SuperTails2 = new byte[7] { 0xC6, 0x86, 0xC2, 0x00, 0x00, 0x00, 0x01 };
            byte[] SuperTails1 = new byte[5] { 0xE8, 0x07, 0xFE, 0xFF, 0xFF };
            byte[] SuperTails3 = new byte[7] { 0x80, 0xBE, 0xC2, 0x00, 0x00, 0x00, 0x01 };

            byte[] SuperKnuckles2 = new byte[12] { 0xC6, 0x86, 0xC2, 0x00, 0x00, 0x00, 0x01, 0xE9, 0xB2, 0xFD, 0xFF, 0xFF };
            byte[] SuperKnuckles3 = new byte[5] { 0xE8, 0xCA, 0xFD, 0xFF, 0xFF };
            byte[] SuperKnuckles1 = new byte[7] { 0x80, 0xBE, 0xC2, 0x00, 0x00, 0x00, 0x01 };
        }


        // Amount of entries is until an invalid level is met
        public enum StartPositionAddressesOnePlayer
        {
            SeasideHill = 0x7C2FC8, // 1st stage
            NumberOfTeamEntries = 0x5, // Including Super Hard
            LengthOfStruct = 0x90, // Used in validation Math.
        }

        public enum StartPositionAddressOffsetsTeamEntryOneTwoPlayer
        {
            XPosition = 0x0,
            YPosition = 0x4,
            ZPosition = 0x8,
            Direction = 0xC,
            StartingMode = 0x14,
            HoldTime = 0x18,
        }

        public enum StartPositionStartingModesOneTwoPlayer
        {
            Normal = 0x0,
            Running = 0x1,
            Rail = 0x2
        }

        public struct StartPositionAddressStructOneTwoPlayer
        {
            float XPosition;
            float YPosition;
            float ZPosition;
            ushort Direction;
            byte StartingMode;
            byte HoldTime;
        }

        // Validation of Available Levels/Entries
        // TODO: When actually using this, change this to dump valid levels to a list!
        public static void CheckLevelStartEndEntryForValidity(int StartAddress, int LengthOfStruct)
        {
            bool IsAValidLevelEntry = true;
            int CurrentLevelID;
            while (IsAValidLevelEntry == true)
            {
                IntPtr ReadAddress = (IntPtr)StartAddress;
                CurrentLevelID = GameHook.GameProcess.Read<int>(ReadAddress, false);
                IsAValidLevelEntry = Enum.IsDefined( typeof(StageIDs) , CurrentLevelID);
                StartAddress += LengthOfStruct;
            }
        }

        // These can be validated with the entry above
        public enum EndPositionAddressOffsets
        {
            SeasideHill = 0x7C45B8,
            NumberOfTeamEntries = 0x5, // Including Super Hard
            LengthOfStruct = 0x68, // Used in validation Math.
        }

        public struct EndPositionAddressStruct
        {
            float XPosition;
            float YPosition;
            float ZPosition;
            ushort CameraPitch;
            byte Unknown;
        }

        public enum EndPositionAddressOffsetsTeamEntry
        {
            XPosition = 0x0,
            YPosition = 0x4,
            ZPosition = 0x8,
            CameraPitch = 0xC,
            Unknown = 0xE,
        }

        // Same struct as OnePlayer, use OnePlayer Starting Stuff
        public enum StartPositionAddressesTwoPlayer
        {
            SeasideHill = 0x7C5E18, // 1st stage
            NumberOfTeamEntries = 0x2, // For each player
            LengthOfStruct = 0x3C, // Used in validation Math.
        }

        // Same struct as OnePlayer, use OnePlayer Ending Stuff
        public enum BraggingPositionAddressesTwoPlayer
        {
            SeasideHill = 0x7C6380, // 1st stage
            NumberOfTeamEntries = 0x4, // For each team
            LengthOfStruct = 0x54, // Used in validation Math.
        }

        public enum HighScoresAddresses
        {
            HighScoreEntryStart = 0x7C744C,
            HighScoreLevelEntryLength = 0x24,
        }

        // Note, the two bytes are multiplied by 100
        public class HighScoresStructClass
        {
            IntPtr StageID;
            HighScoreTeamEntry[] HighScores = new HighScoreTeamEntry[4]; // Each array entry is for a team.
        }

        public struct HighScoreTeamEntry
        {
            ushort DRank;
            ushort CRank;
            ushort BRank;
            ushort ARank;
        }

        // These are all 1 byte values
        public enum TwoPlayerUnlockRequirements
        {
            StartingAddressEmblemRequirement = 0x7433C0,
            NumberOfEntriesEmblemRequirements = 0x6,

            MetalSonicLastStoryPercentageRequirementSonic = 0x45642D,
            MetalSonicLastStoryPercentageRequirementDark = 0x456456,
            MetalSonicLastStoryPercentageRequirementRose = 0x45647F,
            MetalSonicLastStoryPercentageRequirementChaotix = 0x4564A8,

            RequiredChaosEmeralds = 0x4564CF,
            ChaosEmeraldMaxComparisonAddress= 0x4564D1, // See Retro Wiki for reference, replace 94 with 9D
        }

        public struct TwoPlayerEmblemRequirements
        {
            byte ActionRace;
            byte Battle;
            byte SpecialStage;
            byte RingRace;
            byte BobsledRace;
            byte QuickRace;
            byte ExpertRace;
        }

        public struct LastStoryPercentageRequirement
        {
            byte Percentage;
        }

        public struct RequiredChaosEmeralds
        {
            byte RequiredChaosEmeraldsX;
        }

        public enum RankIDs
        {
            NotCleared = 0x0,
            ERank = 0x1,
            DRank = 0x2,
            CRank = 0x3,
            BRank = 0x2,
            ARank = 0x1,
        }

        public enum ScreenFadeAddresses
        {
            ScreenTransitionAddress = 0x405766,
            ScreenTransitionOn = 0x74,
            ScreenTransitionOff = 0xEB,
            ScreenTransitionGameCompletionAddress = 0x454943,

        }

        public class ScreenTransitionGameCompletionEnable
        { 
            byte[] DisableScreenTransition = new byte[6] { 0xB0, 0xFF, 0xB2, 0xFF, 0xB1, 0xFF };
        }

        public enum ScreenFadeParameters
        {
            ClearScreemSpeed = 0x454AA9,
            MainMenuSpeed = 0x454AE9,
        }

        public struct ScreenFadeParametersStruct
        {
            Byte ScreenFadeSpeed;
        }

        public enum SplitScreenFramerateToggle
        {
            Address = 0x402D07,
            Enable = 0x40,
            Disable = 0x90
        }


    }
}
