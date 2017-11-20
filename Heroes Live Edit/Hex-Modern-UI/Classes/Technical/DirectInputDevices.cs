using SlimDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Hex_Modern_UI
{
    public class DirectInputDevices
    {
        // Variables

        // Declare the gamepad instance for storing the player gamepad
        DirectInput DirectInputAdapter = new DirectInput();

        // This will hold a one singular controller.
        Joystick PlayerController;

        // This will hold multiple controllers, all of the connected controllers
        public Joystick[] PlayerControllers;

        // This timer will poll for the joystick
        static System.Windows.Forms.Timer JoystickRefreshPoll = new System.Windows.Forms.Timer();
        static System.Timers.Timer GameHookCheck = new System.Timers.Timer();
        static System.Timers.Timer DrawingHook = new System.Timers.Timer();

        // Game Controller or Keyboard
        static JoystickState State;
        static bool[] ControllerButtons;
        static int[] ControllerDPad;

        // Used for the locking feature
        static bool LockToggle = false;
        static List<int> ButtonsIgnoreID = new List<int>(); // Buttons to not check on main thread.
        static List<int> DPADButtonsIgnoreID = new List<int>(); // Buttons to not check on main thread.

        /* Button Rebindings */
        public int ControllerInputActionID; // What are we doing with the controller right now? Assigning?

        public enum ControllerInputAction
        {
            Null, // Is not assigning or doing anything?
            IsAssigningButton, // Button yay.
            LockInActionMode // Keep repeating action until pressed again, toggle mode.
        }

        public Button EditModeButton;          // UI Button to assign to.
        public int ButtonActionAssignment;     // What action/function am I assigning?

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

        /* Returns Controllers on Startup */
        public Joystick[] GetJoySticks()
        {
            // Setup new array of all controllers
            List<Joystick> PlayerControllers = new List<Joystick>();
            // Get all input devices
            List<DeviceInstance> Controllers = new List<DeviceInstance>(DirectInputAdapter.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly));
            List<DeviceInstance> Keyboards = new List<DeviceInstance>(DirectInputAdapter.GetDevices(DeviceClass.Keyboard, DeviceEnumerationFlags.AttachedOnly));

            List<DeviceInstance> Devices = new List<DeviceInstance>();
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

        public void SetupGamePadTick()
        {
            JoystickRefreshPoll.Interval = 10;
            JoystickRefreshPoll.Tick += new EventHandler(RefreshGamePadState);
            JoystickRefreshPoll.Enabled = true;
        }

        public void RefreshGamePadState(object sender, EventArgs e)
        {
            for (int i = 0; i < PlayerControllers.Length; i++)
            {
                PlayerControllerHandle(PlayerControllers[i], i);
            }
        }

        /* Controller Stuff Happens Here */
        public void PlayerControllerHandle(Joystick PlayerControllerX, int ControllerID)
        {
            State = new JoystickState();
            State = PlayerControllerX.GetCurrentState();

            // Currently pressed controller buttons
            ControllerButtons = State.GetButtons();
            ControllerDPad = State.GetPointOfViewControllers();

            // What are we gonna do with this controller?
            switch (ControllerInputActionID)
            {
                case (int)ControllerInputAction.Null: // Default Method Calls
                    PlayerControllerExecuteMethod(ControllerButtons, ControllerDPad, ControllerID, PlayerControllerX);
                    break;
                case (int)ControllerInputAction.IsAssigningButton: // Assigning Buttons
                    PlayerControllerAssignButton(ControllerButtons, ControllerDPad, ControllerID, PlayerControllerX);
                    break;
            }
            State = null;
        }

        public void PlayerControllerAssignButton(bool[] ControllerButtonsX, int[] ControllerDPadX, int ControllerID, Joystick PlayerControllerX)
        {
            // For each button on controller
            for (int i = 0; i < ControllerButtonsX.Length; i++)
            {
                if (ControllerButtonsX[i]) // If pressed
                {
                    EditModeButton.Text = "Button " + i; ControllerInputActionID = (int)ControllerInputAction.Null;
                    AssignButtonToAction(i, ControllerButtonType.Button, ButtonActionAssignment, ControllerID);
                    EditModeButton = null;
                    while (ControllerButtons[i])
                    {
                        GetControllerState(PlayerControllerX); Thread.Sleep(15);
                    }
                }
            }

            // For each DPAD on keypad
            for (int i = 0; i < ControllerDPadX.Length; i++)
            {
                // If pressed
                if (ControllerDPadX[i] != -1)
                {
                    EditModeButton.Text = "DPAD " + (DPad)ControllerDPadX[i]; ControllerInputActionID = (int)ControllerInputAction.Null;
                    AssignButtonToAction((int)(DPad)ControllerDPadX[i], ControllerButtonType.DPad, ButtonActionAssignment, ControllerID);
                    EditModeButton = null;
                    while (ControllerDPad[i] != -1)
                    {
                        GetControllerState(PlayerControllerX); Thread.Sleep(15);
                    }
                }
            }
        }

        public void GetControllerState(Joystick PlayerControllerX)
        {
            State = PlayerControllerX.GetCurrentState();
            ControllerButtons = State.GetButtons();
            ControllerDPad = State.GetPointOfViewControllers();
        }

        public void LockButtonX(ControllerInputBinding ControllerInputX, Joystick PlayerControllerX)
        {
            int ToggleButtonID = ControllerInputX.ButtonID;
            bool ToggleLoop = true;

            do { { GetControllerState(PlayerControllerX); Thread.Sleep(15); } } while (ControllerButtons[ToggleButtonID]); // Wait until user releases button

            do
            {
                ExecuteAction((int)ControllerInputX.ActionToPerform); GetControllerState(PlayerControllerX);
                if (ControllerButtons[ToggleButtonID] == true) { ToggleLoop = false; } // Poll for re-press
                Thread.Sleep(15);
            }
            while (ToggleLoop);
            do { GetControllerState(PlayerControllerX); Thread.Sleep(15); } while (ControllerButtons[ToggleButtonID]);  // Wait until user releases button
            ButtonsIgnoreID.RemoveAll(FindToggleButton => FindToggleButton == ToggleButtonID);
        }

        public void LockDPadX(ControllerInputBinding ControllerInputX, Joystick PlayerControllerX)
        {
            int ToggleButtonID = ControllerInputX.ButtonID;
            bool ToggleLoop = true;

            do { GetControllerState(PlayerControllerX); Thread.Sleep(15); } while (ControllerDPad[0] == ToggleButtonID);  // Wait until user releases button

            do
            {
                ExecuteAction((int)ControllerInputX.ActionToPerform); GetControllerState(PlayerControllerX);
                if (ControllerDPad[0] == ToggleButtonID) { ToggleLoop = false; } // Poll for re-press
                Thread.Sleep(15);
            }
            while (ToggleLoop);

            do { GetControllerState(PlayerControllerX); Thread.Sleep(15); } while (ControllerDPad[0] == ToggleButtonID); // Wait until user releases button
            DPADButtonsIgnoreID.RemoveAll(FindToggleButton => FindToggleButton == ToggleButtonID);
        }

        /* Let's do stuff! */
        public void PlayerControllerExecuteMethod(bool[] ControllerButtonsPassedIn, int[] ControllerDPadPassedIn, int ControllerID, Joystick PlayerControllerX)
        {
            GetControllerState(PlayerControllerX);

            foreach (ControllerInputBinding ControllerInput in ControllerInputBindings)
            {

                // If the assigned control for the assigned input is a button
                if (ControllerInput.ButtonType == (int)ControllerButtonType.Button)
                {
                    // If the button is pressed & not ignored.
                    if ( (ControllerButtonsPassedIn[ControllerInput.ButtonID] && ButtonsIgnoreID.Contains(ControllerInput.ButtonID) == false) && ControllerInput.ControllerID == ControllerID )
                    {
                        // If locked and will keep repeating
                        if (LockToggle == true && (int)ControllerInput.ActionToPerform != (int)ControllerInputBindingsName.ToggleMode)
                        {
                            Thread GameFunctionLockIn = new Thread(() => LockButtonX(ControllerInput, PlayerControllerX));
                            GameFunctionLockIn.Start();
                            ButtonsIgnoreID.Add(ControllerInput.ButtonID);
                        }
                        // If not locked!
                        else
                        {
                            ButtonsIgnoreID.Add(ControllerInput.ButtonID);
                            ExecuteAction((int)ControllerInput.ActionToPerform);
                            do { GetControllerState(PlayerControllerX); Thread.Sleep(15); } while (ControllerButtons[ControllerInput.ButtonID]);  // Poll for re-press (release)
                            ButtonsIgnoreID.RemoveAll(FindToggleButton => FindToggleButton == ControllerInput.ButtonID);
                        }
                    }
                }
                // If the assigned control for the assigned input is a DPAD
                else if (ControllerInput.ButtonType == (int)ControllerButtonType.DPad)
                {
                    // If the button is pressed & not ignored.
                    if ( (ControllerDPadPassedIn[0] == ControllerInput.ButtonID && DPADButtonsIgnoreID.Contains(ControllerInput.ButtonID) == false) && ControllerInput.ControllerID == ControllerID)
                    {
                        // If locked and will keep repeating
                        if (LockToggle == true && (int)ControllerInput.ActionToPerform != (int)ControllerInputBindingsName.ToggleMode)
                        {
                            Thread GameFunctionLockIn = new Thread(() => LockDPadX(ControllerInput, PlayerControllerX));
                            GameFunctionLockIn.Start();
                            DPADButtonsIgnoreID.Add(ControllerInput.ButtonID);
                        }
                        else
                        {
                            DPADButtonsIgnoreID.Add(ControllerInput.ButtonID);
                            ExecuteAction((int)ControllerInput.ActionToPerform);
                            do { GetControllerState(PlayerControllerX); Thread.Sleep(15); } while (ControllerDPad[0] == ControllerInput.ButtonID);  // Wait until user releases 
                            DPADButtonsIgnoreID.RemoveAll(FindToggleButton => FindToggleButton == ControllerInput.ButtonID);
                        }
                    }
                }
            }
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////// The above code is the main handling code for controller and keyboard key recognition. Modify below to add keybindings!
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /* Button Bindings */
        ControllerInputBinding[] ControllerInputBindings = new ControllerInputBinding[10];

        public struct ControllerInputBinding
        {
            public int ButtonID;
            public int ButtonType;
            public ControllerInputBindingsName ActionToPerform;
            public int ControllerID; // We can now distinguish between different devices, yay!
        };

        public enum ControllerInputBindingsName // This goes in with ControllerInputBinding, and is a list to the enumerable also present below!
        {
            ToggleFormVisibility,
            DisplayTestMessage,
            SelectLastTeleport,
            SelectNextTeleport,
            WarpToDestination,
            SaveWarpDestination,
            ToggleWarpOverlay,
            ToggleMode
        };
                   
        public void ExecuteAction(int Action) // Execute the specified actions, run a method depending on action binding on gamepad.
        {
            switch (Action)
            {
                case (int)ControllerInputBindingsName.ToggleFormVisibility:
                    Program.xSmallMainWindow.OptionsMenu.ToggleVisibilityHotkey();
                    break;
                case (int)ControllerInputBindingsName.SelectLastTeleport:
                    Program.xSmallMainWindow.WarpUtilitiesMenu.SelectLastTeleport();
                    break;
                case (int)ControllerInputBindingsName.SelectNextTeleport:
                    Program.xSmallMainWindow.WarpUtilitiesMenu.SelectNextTeleport();
                    break;
                case (int)ControllerInputBindingsName.WarpToDestination:
                    Program.xSmallMainWindow.WarpUtilitiesMenu.TeleportToDestination();
                    break;
                case (int)ControllerInputBindingsName.SaveWarpDestination:
                    Program.xSmallMainWindow.WarpUtilitiesMenu.SaveStatus();
                    break;
                case (int)ControllerInputBindingsName.DisplayTestMessage:
                    Program.xSmallMainWindow.OptionsMenu.TestMessage();
                    break;
                case (int)ControllerInputBindingsName.ToggleWarpOverlay:
                    Program.xSmallMainWindow.WarpUtilitiesMenu.ToggleWarpOverlay();
                    break;
                case (int)ControllerInputBindingsName.ToggleMode:
                    //CheckUIBox(); // We may be running on a different thread, modifying a control, hold up doc!
                    break;
            }
        }

        private void AssignButtonToAction(int ButtonID, ControllerButtonType ButtonType, int ControllerInputBindingsNameX, int ControllerID)
        {
            ControllerInputBindings[ControllerInputBindingsNameX].ButtonID = ButtonID;
            ControllerInputBindings[ControllerInputBindingsNameX].ButtonType = (int)ButtonType;
            ControllerInputBindings[ControllerInputBindingsNameX].ActionToPerform = (ControllerInputBindingsName)ControllerInputBindingsNameX;
            ControllerInputBindings[ControllerInputBindingsNameX].ControllerID = ControllerID;
        }
    }
}
