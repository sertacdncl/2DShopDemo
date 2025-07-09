/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Game
{
    public partial class UI_main : GComponent
    {
        public Controller c1;
        public UI_inventoryComponent inventoryComponent;
        public GButton inventoryBtnToggle;
        public GImage joystick_center;
        public UI_JoystickButton joystick;
        public GGraph joystick_touch;
        public const string URL = "ui://vo8uz0l0sqnx1";

        public static UI_main CreateInstance()
        {
            return (UI_main)UIPackage.CreateObject("Game", "main");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            inventoryComponent = (UI_inventoryComponent)GetChildAt(0);
            inventoryBtnToggle = (GButton)GetChildAt(1);
            joystick_center = (GImage)GetChildAt(2);
            joystick = (UI_JoystickButton)GetChildAt(3);
            joystick_touch = (GGraph)GetChildAt(4);
        }
    }
}