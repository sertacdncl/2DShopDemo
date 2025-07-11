--- Created by BYTE.
--- DateTime: 10.07.2025 09:48
---

local JoystickModule = {}
JoystickModule.__index = JoystickModule

local Vector2 = CS.UnityEngine.Vector2
local Mathf = CS.UnityEngine.Mathf
local centerJoystickPos = 0

function JoystickModule.new(view)
    local self = setmetatable({}, JoystickModule)

    self._view = view
    self._button = view:GetChild("joystick").asButton
    self._button.changeStateOnClick = false
    self._thumb = self._button:GetChild("thumb")
    self._touchArea = view:GetChild("joystick_touch")
    self._center = view:GetChild("joystick_center")
    
    self.radius = 150
    self.touchId = -1
    self._tweener = nil

    self.onMove = {}
    self.onEnd = {}

    self._touchArea.onTouchBegin:Add(function(context) self:OnTouchBegin(context) end)
    self._touchArea.onTouchMove:Add(function(context) self:OnTouchMove(context) end)
    self._touchArea.onTouchEnd:Add(function(context) self:OnTouchEnd(context) end)
    return self
end

function JoystickModule:OnTouchBegin(context)
    if self.touchId ~= -1 then return end

    local evt = context.data
    self.touchId = evt.touchId

    if self._tweener then
        self._tweener:Kill()
        self._tweener = nil
    end

    local pt = CS.FairyGUI.GRoot.inst:GlobalToLocal(Vector2(evt.x, evt.y))
    local bx = pt.x
    local by = pt.y

    self._InitX = bx
    self._InitY = by

    self._button.selected = true
    self._center.visible = true
    self._center:SetXY(bx - self._center.width / 2, by - self._center.height / 2)
    self._button:SetXY(bx - self._button.width / 2, by - self._button.height / 2)

    local deltaX = 0
    local deltaY = 0
    local degrees = math.deg(math.atan(deltaY, deltaX))
    self._thumb.rotation = degrees + 90

    context:CaptureTouch()
end

function JoystickModule:OnTouchMove(context)
    if self.touchId == -1 then return end

    local evt = context.data
    if evt.touchId ~= self.touchId then return end

    local pt = CS.FairyGUI.GRoot.inst:GlobalToLocal(Vector2(evt.x, evt.y))
    local bx = pt.x
    local by = pt.y

    local deltaX = bx - self._InitX
    local deltaY = by - self._InitY
    local distance = math.sqrt(deltaX * deltaX + deltaY * deltaY)

    if distance > self.radius then
        local angle = math.atan(deltaY, deltaX)
        bx = self._InitX + math.cos(angle) * self.radius
        by = self._InitY + math.sin(angle) * self.radius
        deltaX = bx - self._InitX
        deltaY = by - self._InitY
    end

    self._button:SetXY(bx - self._button.width / 2, by - self._button.height / 2)

    local degrees = math.deg(math.atan(deltaY, deltaX))
    self._thumb.rotation = degrees + 90

    local angle = math.atan(-deltaY, deltaX)
    local degree = math.deg(angle)
    local x = math.cos(angle)
    local y = math.sin(angle)
    
    for _, callback in ipairs(self.onMove) do
        callback(x, y, degree) -- 3 parametreli
    end
    
        context:CaptureTouch()
    end

function JoystickModule:OnTouchEnd(context)
    if self.touchId == -1 then return end

    local evt = context.data
    if evt.touchId ~= self.touchId then return end

    self.touchId = -1

    for _, callback in ipairs(self.onEnd) do
        callback()
    end

    -- Thumb 180 derece döndürülür
    self._thumb.rotation = self._thumb.rotation + 180

    -- Center gizlenir
    self._center.visible = false

    -- Daha önceki tween varsa iptal et
    if self._tweener then
        self._tweener:Kill()
        self._tweener = nil
    end

    -- Joystick (button) merkeze animasyonla döner
    self._tweener = self._button:TweenMove(
        Vector2(self._InitX - self._button.width / 2, self._InitY - self._button.height / 2),
        0.3
    ):OnComplete(function()
        self._tweener = nil
        self._button.selected = false

        -- Thumb rotasyonu sıfırlanır
        self._thumb.rotation = 0

        -- Center tekrar görünür ve konumu sıfırlanır
        self._center.visible = true
        self._center:SetXY(self._InitX - self._center.width / 2, self._InitY - self._center.height / 2)
    end)
end

function JoystickModule:Dispose()
    if self._tweener then
        self._tweener:Kill()
        self._tweener = nil
    end

    if self._touchArea.onTouchBegin.Clear then
        self._touchArea.onTouchBegin:Clear()
        self._touchArea.onTouchMove:Clear()
        self._touchArea.onTouchEnd:Clear()
    end

    self.touchId = -1
    self._view = nil
end


return JoystickModule
