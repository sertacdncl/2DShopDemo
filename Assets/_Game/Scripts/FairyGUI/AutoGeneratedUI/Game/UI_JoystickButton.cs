/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Game
{
    public partial class UI_JoystickButton : GButton
    {
        public GImage thumb;
        public const string URL = "ui://vo8uz0l0hmmxj";

        public static UI_JoystickButton CreateInstance()
        {
            return (UI_JoystickButton)UIPackage.CreateObject("Game", "JoystickButton");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            thumb = (GImage)GetChildAt(0);
        }
    }
}