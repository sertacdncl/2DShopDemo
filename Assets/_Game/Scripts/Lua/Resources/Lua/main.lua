print("Lua script loaded successfully!")

local UIPackage = CS.FairyGUI.UIPackage
local GRoot = CS.FairyGUI.GRoot.inst
local UnityEngine = CS.UnityEngine

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

-- Create JoystickModule
local JoystickModule = load(CS.UnityEngine.Resources.Load("lua/JoystickModule"):ToString())()

--local JoystickModule = require 'lua.JoystickModule'

local joystick = JoystickModule.new(view)
local JoystickEvents = CS.JoystickEvents

table.insert(joystick.onMove, function(x, y, degree)
    local dir = UnityEngine.Vector2(x, y).normalized
    JoystickEvents.OnMove(dir)
    print(string.format("degree: %.2f → x: %.2f, y: %.2f", degree, x, y))
end)

table.insert(joystick.onEnd, function()
    JoystickEvents.OnEnd()
end)