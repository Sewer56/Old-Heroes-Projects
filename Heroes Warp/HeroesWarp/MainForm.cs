using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SlimDX.DirectInput;
using System.Threading;
using Binarysharp.MemoryManagement;
using System.Timers;
using Binarysharp.MemoryManagement.Helpers;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HeroesWarp
{
    public partial class MainWindow : Form
    {
        public const string HEROES_WINDOW_NAME = "SONIC HEROES(TM)";
        static IntPtr HeroesHandle = FindWindow(null, HEROES_WINDOW_NAME);
        static RECT HeroesWindowRectangle;
        static OverlayForm GDIOverlay = new OverlayForm();

        public struct RECT
        {
            public int LeftBorder, TopBorder, RightBorder, BottomBorder;
        }

        public MainWindow()
        {
            InitializeComponent();
            PlayerControllers = GetJoySticks();
            SetupGamePadTick();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        const uint EVENT_OBJECT_LOCATIONCHANGE = 0x800B;
        const uint WINEVENT_OUTOFCONTEXT = 0;


        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);

        static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            // filter out non-HWND namechanges... (eg. items within a listbox)
            if (idObject != 0 || idChild != 0)
            {
                return;
            }

            // Adjust Size Accordingly
            GetWindowRect(HeroesHandle, out HeroesWindowRectangle); // Get rectangle

            int WindowWidth = HeroesWindowRectangle.RightBorder - HeroesWindowRectangle.LeftBorder;
            int WindowHeight = HeroesWindowRectangle.BottomBorder - HeroesWindowRectangle.TopBorder;

            GDIOverlay.Size = new Size(WindowWidth, WindowHeight);
            GDIOverlay.Top = HeroesWindowRectangle.TopBorder;
            GDIOverlay.Left = HeroesWindowRectangle.LeftBorder;
        }

        // Here the stuff is drawn live
        static void DrawStuffToScreen(object sender, EventArgs e)
        {
            GDIOverlay.HookedTextMessageX = "< " + "Warp " + MenuWarpIndex + " >";
            GDIOverlay.Invalidate();
        }

        static void DrawYCoordinate()
        {
            PointerAddress = GameProcess.Read<int>(CharacterPointers.CurrentCharacter);
            if (PointerAddress != 0)
            {
                float XCoordinate = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YPosition), false);
                GDIOverlay.HookedTextMessageX = XCoordinate.ToString("F4");
            }
            GDIOverlay.Invalidate();
        }

        // Variables

        // Declare the gamepad instance for storing the player gamepad
        DirectInput DirectInputAdapter = new DirectInput();

        // This will hold a one singular controller.
        Joystick PlayerController;

        // This will hold multiple controllers, all of the connected controllers
        Joystick[] PlayerControllers;

        // This timer will poll for the joystick
        System.Windows.Forms.Timer JoystickRefreshPoll = new System.Windows.Forms.Timer();
        static System.Timers.Timer GameHookCheck = new System.Timers.Timer();
        static System.Timers.Timer DrawingHook = new System.Timers.Timer();

        // Game Process
        static MemorySharp GameProcess;

        // Menu Warp Index
        static int MenuWarpIndex = 0;

        // Game Controller or Keyboard
        static JoystickState State = new JoystickState();
        static bool[] ControllerButtons;
        static int[] ControllerDPad;

        // Used for the locking feature
        static bool LockToggle = false;
        static List<int> ButtonsIgnoreID = new List<int>(); // Buttons to not check on main thread.
        static List<int> DPADButtonsIgnoreID = new List<int>(); // Buttons to not check on main thread.
        static List<CharacterWarps> CurrentWarps = new List<CharacterWarps>(); // All Warp Points

        // Pointer
        static int PointerAddress;

        /* Button Rebindings */
        int ControllerInputActionID; // What are we doing with the controller right now?

        enum ControllerInputAction
        {
            Null,
            IsAssigningButton,
            LockInActionMode
        }

        Button EditModeButton;          // UI Button to assign to.
        int ButtonActionAssignment;     // What action/function am I assigning?

        enum ButtonActionAssignmentName
        {
            SelectPreviousTeleport,
            TeleportToDestination,
            SelectNextTeleport,
            SaveGameStatus,
            LockYPosition,
            ToggleMode
        }

        enum DPad
        {
            UP = 0,
            RIGHT = 9000,
            DOWN = 18000,
            LEFT = 27000
        };

        enum ControllerButtonType
        {
            Null, // To prevent evaluation of button 0s when no button is assigned.
            Button,
            DPad,
            ControlStick
        };

        enum CharacterPointers
        {
            CurrentCharacter = 0x005CE820,
        }

        enum CharacterPointerOffsets
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

        struct CharacterWarps
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
            public string CharacterWarpName;
        }

        struct ControllerInputBinding
        {
            public int ButtonID;
            public int ButtonType;
            public ButtonActionAssignmentName ActionToPerform;
        };

        enum ControllerInputBindingsName // Name for all of the bindings in the list
        {
           SelectPreviousTeleport,
           TeleportToDestination,
           SelectNextTeleport,
           SaveGameStatus,
           LockYPosition,
           ToggleMode
        };

        /* Button Bindings */
        ControllerInputBinding[] ControllerInputBindings = new ControllerInputBinding[10];

        public void PlayerControllerAssignButton(bool[] ControllerButtons, int[] ControllerDPad)
        {
            // For each button on keypad
            for (int i = 0; i < ControllerButtons.Length; i++)
            {
                if (ControllerButtons[i]) // If pressed
                {
                    EditModeButton.Text = "Button " + i; ControllerInputActionID = (int)ControllerInputAction.Null;
                    AssignButtonToAction(i, ControllerButtonType.Button, ButtonActionAssignment);
                    EditModeButton = null;
                    Thread.Sleep(300); // Give time to release button
                }
            }

            // For each DPAD on keypad
            for (int i = 0; i < ControllerDPad.Length; i++)
            {
                // If pressed
                if (ControllerDPad[i] != -1)
                {
                    EditModeButton.Text = "DPAD " + (DPad)ControllerDPad[i]; ControllerInputActionID = (int)ControllerInputAction.Null;
                    AssignButtonToAction((int)(DPad)ControllerDPad[i], ControllerButtonType.DPad, ButtonActionAssignment);
                    EditModeButton = null;
                    Thread.Sleep(100); // Give time to release button
                }
            }
        }

        public void GetControllerState()
        {
            State = PlayerController.GetCurrentState();
            ControllerButtons = State.GetButtons();
            ControllerDPad = State.GetPointOfViewControllers();
        }

        // Gets controller state for temporary buttons/lockin mode
        public void GetControllerStateTemp()
        {
            State = PlayerController.GetCurrentState();
            ControllerButtons = State.GetButtons();
            ControllerDPad = State.GetPointOfViewControllers();
        }

        private void LockButtonX(ControllerInputBinding ControllerInputX)
        {
            int ToggleButtonID = ControllerInputX.ButtonID;
            bool ToggleLoop = true;

            while (ControllerButtons[ToggleButtonID]) { GetControllerState(); Thread.Sleep(15); } // Wait until user releases button
            while (ToggleLoop)
            {
                ExecuteAction((int)ControllerInputX.ActionToPerform); GetControllerState();
                if (ControllerButtons[ToggleButtonID] == true) { ToggleLoop = false; } // Poll for re-press
                Thread.Sleep(15);
            }
            while (ControllerButtons[ToggleButtonID]) { GetControllerState(); Thread.Sleep(15); } // Wait until user releases button
            ButtonsIgnoreID.RemoveAll(FindToggleButton => FindToggleButton == ToggleButtonID);
        }

        private void LockDPadX(ControllerInputBinding ControllerInputX)
        {
            int ToggleButtonID = ControllerInputX.ButtonID;
            bool ToggleLoop = true;

            while (ControllerDPad[0] == ToggleButtonID) { GetControllerState(); Thread.Sleep(15); } // Wait until user releases button

            while (ToggleLoop)
            {
                ExecuteAction((int)ControllerInputX.ActionToPerform); GetControllerState();
                if (ControllerDPad[0] == ToggleButtonID) { ToggleLoop = false; } // Poll for re-press
                Thread.Sleep(15);
            }
            while (ControllerDPad[0] == ToggleButtonID) { GetControllerState(); Thread.Sleep(15); } // Wait until user releases button
            DPADButtonsIgnoreID.RemoveAll(FindToggleButton => FindToggleButton == ToggleButtonID);
        }

        /* Let's do stuff! */
        public void PlayerControllerExecuteMethod(bool[] ControllerButtonsPassedIn, int[] ControllerDPadPassedIn)
        {
            GetControllerState();

            foreach (ControllerInputBinding ControllerInput in ControllerInputBindings)
            {

                // If the assigned control for the assigned input is a button
                if (ControllerInput.ButtonType == (int)ControllerButtonType.Button)
                {
                    // If the button is pressed.
                    if (ControllerButtonsPassedIn[ControllerInput.ButtonID] && ButtonsIgnoreID.Contains(ControllerInput.ButtonID) == false)
                    {
                        // If locked and will keep repeating
                        if (LockToggle == true && (int)ControllerInput.ActionToPerform != (int)ControllerInputBindingsName.ToggleMode)
                        {
                            Thread GameFunctionLockIn = new Thread(() => LockButtonX(ControllerInput));
                            GameFunctionLockIn.Start();
                            ButtonsIgnoreID.Add(ControllerInput.ButtonID);
                        }
                        else
                        {
                            ButtonsIgnoreID.Add(ControllerInput.ButtonID);
                            ExecuteAction((int)ControllerInput.ActionToPerform);
                            while (ControllerButtons[ControllerInput.ButtonID]) { GetControllerState(); Thread.Sleep(15); } // Poll for re-press
                            ButtonsIgnoreID.RemoveAll(FindToggleButton => FindToggleButton == ControllerInput.ButtonID);
                        }
                    }
                }
                // If the assigned control for the assigned input is a DPAD
                else if (ControllerInput.ButtonType == (int)ControllerButtonType.DPad)
                {
                    // If the DPAD button is pressed
                    if (ControllerDPadPassedIn[0] == ControllerInput.ButtonID && DPADButtonsIgnoreID.Contains(ControllerInput.ButtonID) == false)
                    {
                        // If locked and will keep repeating
                        if (LockToggle == true && (int)ControllerInput.ActionToPerform != (int)ControllerInputBindingsName.ToggleMode)
                        {
                            Thread GameFunctionLockIn = new Thread(() => LockDPadX(ControllerInput));
                            GameFunctionLockIn.Start();
                            DPADButtonsIgnoreID.Add(ControllerInput.ButtonID);
                        }
                        else
                        {
                            DPADButtonsIgnoreID.Add(ControllerInput.ButtonID);
                            ExecuteAction((int)ControllerInput.ActionToPerform);
                            while (ControllerDPad[0] == ControllerInput.ButtonID) { GetControllerState(); Thread.Sleep(15); } // Wait until user releases 
                            DPADButtonsIgnoreID.RemoveAll(FindToggleButton => FindToggleButton == ControllerInput.ButtonID);
                        }
                    }
                }
            }
        }

        /* Controller Stuff Happens Here */
        private void PlayerControllerHandle(Joystick PlayerController, int id)
        {
            JoystickState State = new JoystickState();
            State = PlayerController.GetCurrentState();

            // Currently pressed controller buttons
            ControllerButtons = State.GetButtons();
            ControllerDPad = State.GetPointOfViewControllers();

            // What are we gonna do with this controller?
            switch (ControllerInputActionID)
            {
                case (int)ControllerInputAction.Null: // Default Method Calls
                    PlayerControllerExecuteMethod(ControllerButtons, ControllerDPad);
                    break;
                case (int)ControllerInputAction.IsAssigningButton: // Assigning Buttons
                    PlayerControllerAssignButton(ControllerButtons, ControllerDPad);
                    break;
            }
        }

        private void SetupGamePadTick()
        {
            JoystickRefreshPoll.Interval = 10;
            JoystickRefreshPoll.Tick += new EventHandler(RefreshGamePadState);
            JoystickRefreshPoll.Enabled = true;
        }

        private void RefreshGamePadState(object sender, EventArgs e)
        {
            for (int i = 0; i < PlayerControllers.Length; i++)
            {
                PlayerControllerHandle(PlayerControllers[i], i);
            }
        }

        private void AssignButtonToAction(int ButtonID, ControllerButtonType ButtonType, int Action)
        {
            switch (Action)
            {
                case (int)ButtonActionAssignmentName.SelectPreviousTeleport: // Forward Teleport List
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectPreviousTeleport].ButtonID = ButtonID;
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectPreviousTeleport].ButtonType = (int)ButtonType;
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectPreviousTeleport].ActionToPerform = ButtonActionAssignmentName.SelectPreviousTeleport;
                    break;
                case (int)ButtonActionAssignmentName.TeleportToDestination: // Forward Teleport List
                    ControllerInputBindings[(int)ControllerInputBindingsName.TeleportToDestination].ButtonID = ButtonID;
                    ControllerInputBindings[(int)ControllerInputBindingsName.TeleportToDestination].ButtonType = (int)ButtonType;
                    ControllerInputBindings[(int)ControllerInputBindingsName.TeleportToDestination].ActionToPerform = ButtonActionAssignmentName.TeleportToDestination;
                    break;
                case (int)ButtonActionAssignmentName.SelectNextTeleport: // Forward Teleport List
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectNextTeleport].ButtonID = ButtonID;
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectNextTeleport].ButtonType = (int)ButtonType;
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectNextTeleport].ActionToPerform = ButtonActionAssignmentName.SelectNextTeleport;
                    break;
                case (int)ButtonActionAssignmentName.SaveGameStatus: // Forward Teleport List
                    ControllerInputBindings[(int)ControllerInputBindingsName.SaveGameStatus].ButtonID = ButtonID;
                    ControllerInputBindings[(int)ControllerInputBindingsName.SaveGameStatus].ButtonType = (int)ButtonType;
                    ControllerInputBindings[(int)ControllerInputBindingsName.SaveGameStatus].ActionToPerform = ButtonActionAssignmentName.SaveGameStatus;
                    break;
                case (int)ButtonActionAssignmentName.LockYPosition: // Lock Character in Height
                    ControllerInputBindings[(int)ControllerInputBindingsName.LockYPosition].ButtonID = ButtonID;
                    ControllerInputBindings[(int)ControllerInputBindingsName.LockYPosition].ButtonType = (int)ButtonType;
                    ControllerInputBindings[(int)ControllerInputBindingsName.LockYPosition].ActionToPerform = ButtonActionAssignmentName.LockYPosition;
                    break;
                case (int)ButtonActionAssignmentName.ToggleMode: // Lock Character in Height
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectNextTeleport].ButtonID = ButtonID;
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectNextTeleport].ButtonType = (int)ButtonType;
                    ControllerInputBindings[(int)ControllerInputBindingsName.SelectNextTeleport].ActionToPerform = ButtonActionAssignmentName.ToggleMode;
                    break;
            }
        }

        // Execute the specified actions, run a method depending on action binding on gamepad.
        private void ExecuteAction(int Action)
        {
            switch (Action)
            {
                case (int)ButtonActionAssignmentName.SelectPreviousTeleport:
                    SelectLastTeleport();
                    break;
                case (int)ButtonActionAssignmentName.TeleportToDestination:
                    TeleportToDestination();
                    break;
                case (int)ButtonActionAssignmentName.SelectNextTeleport:
                    SelectNextTeleport();
                    break;
                case (int)ButtonActionAssignmentName.SaveGameStatus:
                    SaveStatus();
                    break;
                case (int)ButtonActionAssignmentName.LockYPosition:
                    LockCharacterHeight();
                    break;
                case (int)ButtonActionAssignmentName.ToggleMode:
                    CheckUIBox(); // We may be running on a different thread, modifying a control, hold up doc!
                    break;
            }

        }

        private void CheckUIBox()
        {
            if (ChkBox_Lock.Checked == false) { ChkBox_Lock.Checked = true; } else { ChkBox_Lock.Checked = false; }
        }

        private void SelectNextTeleport()
        {
            if (GameProcess != null)
            {
                if (GameProcess.IsRunning == true)
                {
                    int Index = MenuWarpIndex + 1;
                    if (Index < CurrentWarps.Count) { MenuWarpIndex = Index; }
                }
            }
        }
        private void TeleportToDestination()
        {
            if (GameProcess != null)
            {
                if (GameProcess.IsRunning == true)
                {
                    PointerAddress = GameProcess.Read<int>(CharacterPointers.CurrentCharacter);
                    if (PointerAddress != 0)
                    {
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.XVelocity), CurrentWarps[MenuWarpIndex].XVelocity ,false);
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YVelocity), CurrentWarps[MenuWarpIndex].YVelocity, false); 
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.ZVelocity), CurrentWarps[MenuWarpIndex].ZVelocity, false); 
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.XPosition), CurrentWarps[MenuWarpIndex].XPosition, false); 
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YPosition), CurrentWarps[MenuWarpIndex].YPosition, false); 
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.ZPosition), CurrentWarps[MenuWarpIndex].ZPosition, false); 
                        GameProcess.Write<ushort>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.XRotation), CurrentWarps[MenuWarpIndex].XRotation, false); 
                        GameProcess.Write<ushort>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YRotation), CurrentWarps[MenuWarpIndex].YRotation, false); 
                        GameProcess.Write<ushort>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.ZRotation), CurrentWarps[MenuWarpIndex].ZRotation, false); 
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.XCharacterThickness), CurrentWarps[MenuWarpIndex].XCharacterThickness, false); 
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YCharacterThickness), CurrentWarps[MenuWarpIndex].YCharacterThickness, false);
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.TeamMatesFollowingSomething), CurrentWarps[MenuWarpIndex].TeamMatesFollowingSomething, false);
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.TeamMatesFollowingSomething2), CurrentWarps[MenuWarpIndex].TeamMatesFollowingSomething2, false);
                        GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.TeamMatesFollowingSomething3), CurrentWarps[MenuWarpIndex].TeamMatesFollowingSomething3, false);
                    }
                }
            }
        }

        private void LockCharacterHeight()
        {
            if (GameProcess != null)
            {
                if (GameProcess.IsRunning == true)
                {
                    PointerAddress = GameProcess.Read<int>(CharacterPointers.CurrentCharacter);

                    // Y Position
                    GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YPosition), 100.0F, false);
                    // Y Speed
                    GameProcess.Write<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YVelocity), 0.0F, false);
                }
            }
        }

        private void SelectLastTeleport()
        {
            if (GameProcess != null)
            {
                if (GameProcess.IsRunning == true)
                {
                    int Index = MenuWarpIndex - 1;
                    if (Index < CurrentWarps.Count && Index > 0) { MenuWarpIndex = Index; }
                }
            }
           
        }
        private void SaveStatus()
        {
            if (GameProcess != null)
            {
                if (GameProcess.IsRunning == true)
                {
                    PointerAddress = GameProcess.Read<int>(CharacterPointers.CurrentCharacter);
                    if (PointerAddress != 0)
                    {
                        CharacterWarps TempCharacterWarpX;
                        TempCharacterWarpX.XVelocity = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.XVelocity), false);
                        TempCharacterWarpX.YVelocity = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YVelocity), false); ;
                        TempCharacterWarpX.ZVelocity = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.ZVelocity), false); ;
                        TempCharacterWarpX.XPosition = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.XPosition), false); ;
                        TempCharacterWarpX.YPosition = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YPosition), false); ;
                        TempCharacterWarpX.ZPosition = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.ZPosition), false); ;
                        TempCharacterWarpX.XRotation = GameProcess.Read<ushort>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.XRotation), false); ;
                        TempCharacterWarpX.YRotation = GameProcess.Read<ushort>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YRotation), false); ;
                        TempCharacterWarpX.ZRotation = GameProcess.Read<ushort>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.ZRotation), false); ;
                        TempCharacterWarpX.XCharacterThickness = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.XCharacterThickness), false); ;
                        TempCharacterWarpX.YCharacterThickness = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.YCharacterThickness), false); ;
                        TempCharacterWarpX.TeamMatesFollowingSomething = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.TeamMatesFollowingSomething), false);
                        TempCharacterWarpX.TeamMatesFollowingSomething2 = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.TeamMatesFollowingSomething2), false);
                        TempCharacterWarpX.TeamMatesFollowingSomething3 = GameProcess.Read<float>((IntPtr)(PointerAddress + (int)CharacterPointerOffsets.TeamMatesFollowingSomething3), false);
                        TempCharacterWarpX.CharacterWarpName = "Warp " + CurrentWarps.Count;
                        GDIOverlay.HookedTextMessageX = "Warp " + CurrentWarps.Count;
                        CurrentWarps.Add(TempCharacterWarpX);
                    }
                }
            }
        }

        // UI Buttons
        private void Btn_Forward_Click(object sender, EventArgs e)
        {
            Btn_TeleportNext.Text = "Press A Button";
            ControllerInputActionID = (int)ControllerInputAction.IsAssigningButton;
            ButtonActionAssignment = (int)ButtonActionAssignmentName.SelectNextTeleport;
            EditModeButton = Btn_TeleportNext;
        }

        private void Btn_Teleport_Click(object sender, EventArgs e)
        {
            Btn_Teleport.Text = "Press A Button";
            ControllerInputActionID = (int)ControllerInputAction.IsAssigningButton;
            ButtonActionAssignment = (int)ButtonActionAssignmentName.TeleportToDestination;
            EditModeButton = Btn_Teleport;
        }

        private void Btn_TeleportLast_Click(object sender, EventArgs e)
        {
            Btn_TeleportLast.Text = "Press A Button";
            ControllerInputActionID = (int)ControllerInputAction.IsAssigningButton;
            ButtonActionAssignment = (int)ButtonActionAssignmentName.SelectPreviousTeleport;
            EditModeButton = Btn_TeleportLast;
        }

        private void Btn_SaveStatus_Click(object sender, EventArgs e)
        {
            Btn_SaveStatus.Text = "Press A Button";
            ControllerInputActionID = (int)ControllerInputAction.IsAssigningButton;
            ButtonActionAssignment = (int)ButtonActionAssignmentName.SaveGameStatus;
            EditModeButton = Btn_SaveStatus;
        }

        private void Btn_LockYPosition_Click(object sender, EventArgs e)
        {
            Btn_LockYPosition.Text = "Press A Button";
            ControllerInputActionID = (int)ControllerInputAction.IsAssigningButton;
            ButtonActionAssignment = (int)ButtonActionAssignmentName.LockYPosition;
            EditModeButton = Btn_LockYPosition;
        }

        private void Btn_ToggleMode_Click(object sender, EventArgs e)
        {
            Btn_ToggleMode.Text = "Press A Button";
            ControllerInputActionID = (int)ControllerInputAction.IsAssigningButton;
            ButtonActionAssignment = (int)ButtonActionAssignmentName.ToggleMode;
            EditModeButton = Btn_ToggleMode;
        }

        private void DrawWindow()
        {
            GDIOverlay.FormBorderStyle = FormBorderStyle.None;
            GDIOverlay.BackColor = Color.White;
            GDIOverlay.TransparencyKey = Color.White;
            GDIOverlay.Show();

            int InitialStyle = GetWindowLong(GDIOverlay.Handle, -20); // Get the window style
            SetWindowLong(GDIOverlay.Handle, -20, InitialStyle | 0x80000 | 0x20); // Set window properties
            GetWindowRect(HeroesHandle, out HeroesWindowRectangle); // Get window rectangle
            GDIOverlay.TopMost = true; // Put to top.

            AdjustOverlayWindow();

            // Listen for sizechange change changes across all processes/threads on current desktop...
            IntPtr HeroesHook = SetWinEventHook(EVENT_OBJECT_LOCATIONCHANGE, EVENT_OBJECT_LOCATIONCHANGE, IntPtr.Zero, procDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        private void AdjustOverlayWindow()
        {
            // Adjust Size Accordingly
            GDIOverlay.Size = new Size(HeroesWindowRectangle.RightBorder - HeroesWindowRectangle.LeftBorder, HeroesWindowRectangle.BottomBorder - HeroesWindowRectangle.TopBorder);
            GDIOverlay.Top = HeroesWindowRectangle.TopBorder;
            GDIOverlay.Left = HeroesWindowRectangle.LeftBorder;
        }

        private void DrawStuffTimer()
        {
            DrawingHook.Interval = 16;
            DrawingHook.Elapsed += new ElapsedEventHandler(DrawStuffToScreen);
            DrawingHook.Enabled = true;
        }

        private void HookTheGame(object sender, EventArgs e)
        {

            if (GameProcess == null)
            {
                try
                {
                    // Game Process
                    GameProcess = new MemorySharp(ApplicationFinder.FromProcessName("Tsonic_win").First());
                    // If the first doesn't successfully happen this will never be hit
                    Invoke(new MethodInvoker(WriteGameIsHookedText));
                    Invoke(new MethodInvoker(DrawWindow));
                    Invoke(new MethodInvoker(DrawStuffTimer));
                }
                catch
                {
                    Invoke(new MethodInvoker(WriteWaitingForGameText));

                }
            }
            else if (GameProcess.IsRunning == false)
            {
                try
                {
                    // Game Process
                    GameProcess = new MemorySharp(ApplicationFinder.FromProcessName("Tsonic_win").First());
                    // If the first doesn't successfully happen this will never be hit
                    Invoke(new MethodInvoker(WriteGameIsHookedText));
                    Invoke(new MethodInvoker(DrawWindow));
                    Invoke(new MethodInvoker(DrawStuffTimer));
                }
                catch
                {
                    Invoke(new MethodInvoker(WriteWaitingForGameText));

                }
            }

        }

        private void WriteWaitingForGameText() { lbl_GameStatus.Text = "Waiting for game..."; }
        private void WriteGameIsHookedText() { lbl_GameStatus.Text = "Game is hooked."; }

        private void CheckTheHook()
        {
            GameHookCheck.Interval = 5000;
            GameHookCheck.Elapsed += new ElapsedEventHandler( HookTheGame );
            GameHookCheck.Enabled = true;
            HookTheGame(null, null);
        }

        /* Returns Controllers on Startup */
        public Joystick[] GetJoySticks()
        {
            // Setup new array of all controllers
            List<Joystick> PlayerControllers = new List<Joystick>();
            // Get all input devices
            List <DeviceInstance> Controllers = new List<DeviceInstance>( DirectInputAdapter.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly) );
            List <DeviceInstance> Keyboards = new List<DeviceInstance>(DirectInputAdapter.GetDevices(DeviceClass.Keyboard, DeviceEnumerationFlags.AttachedOnly));

            List <DeviceInstance> Devices = new List<DeviceInstance>();
            Devices.AddRange(Keyboards);
            Devices.AddRange(Controllers);

            // For each device
            foreach (DeviceInstance device in Devices)
            {
                try
                {
                    // Generate a new PlayerController
                    PlayerController = new SlimDX.DirectInput.Joystick(DirectInputAdapter, device.InstanceGuid);
                    PlayerController.Acquire();
                    foreach (DeviceObjectInstance deviceObject in PlayerController.GetObjects())
                    {
                        if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                        {
                            // Assign PlayerController to the object if found, set range of -100 to 100.
                            PlayerController.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-100, 100);
                        }
                    }
                    // Add the PlayerController to recognized PlayerControllers.
                    PlayerControllers.Add(PlayerController);
                }
                catch (DirectInputException)
                {

                }
            }
            // Return all controllers back to application
            return PlayerControllers.ToArray();
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            Thread GameHookCheckThread = new Thread(CheckTheHook);
            GameHookCheckThread.Start();
        }

        private void ChkBox_Lock_CheckedChanged(object sender, EventArgs e)
        {
            if (LockToggle == false) { LockToggle = true; }
            else if (LockToggle == true) { LockToggle = false; }
        }
    }
}
