print("Lua script loaded successfully!")

local UIPackage = CS.FairyGUI.UIPackage
local GRoot = CS.FairyGUI.GRoot.inst

-- Load the UI package
UIPackage.AddPackage("Assets/_Game/Resources/Inventory/Game")

print("Package loaded : " .. UIPackage.GetByName("Game").name)

-- Create a new ui object
local view = UIPackage.CreateObject("Game", "main").asCom

print("Object created : " .. view.name)
GRoot:AddChild(view)

-- Button reach
local btn = view:GetChild("inventoryBtnToggle")
btn.onClick:Add(function()
    print("Button clicked!")
end)