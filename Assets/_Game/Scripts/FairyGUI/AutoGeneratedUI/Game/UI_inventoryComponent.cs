/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Game
{
    public partial class UI_inventoryComponent : GComponent
    {
        public GGraph bgShadow;
        public GGraph bg;
        public GGraph slotBg;
        public GList inventoryList;
        public GGraph gearBg;
        public GList gearsList;
        public GLoader characterView;
        public const string URL = "ui://vo8uz0l0sqnx2";

        public static UI_inventoryComponent CreateInstance()
        {
            return (UI_inventoryComponent)UIPackage.CreateObject("Game", "inventoryComponent");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bgShadow = (GGraph)GetChildAt(0);
            bg = (GGraph)GetChildAt(1);
            slotBg = (GGraph)GetChildAt(2);
            inventoryList = (GList)GetChildAt(3);
            gearBg = (GGraph)GetChildAt(4);
            gearsList = (GList)GetChildAt(5);
            characterView = (GLoader)GetChildAt(6);
        }
    }
}