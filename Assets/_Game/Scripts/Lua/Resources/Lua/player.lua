---
--- Created by BYTE.
--- DateTime: 10.07.2025 14:27
---

local Player = {}
Player.__index = Player

Player.position = CS.UnityEngine.Vector2(0, 0)
Player.moveSpeed = 5
Player.gameObject = nil
Player.animator = nil
Player._lookDirection = nil

Player._dirXHash = CS.UnityEngine.Animator.StringToHash("DirX")
Player._dirYHash = CS.UnityEngine.Animator.StringToHash("DirY")
Player._speedHash = CS.UnityEngine.Animator.StringToHash("Speed")

function Player.update(dt, input)
    if not Player.rb then return end
    if input == nil then return end

    local move = input * Player.moveSpeed * dt
    local movement = move * Player.moveSpeed
    local movementSpeed = movement.magnitude
    Player.position = Player.position + move
    SetLookDirection(input)
    Player.rb:MovePosition(Player.position)
    Player.animator:SetFloat(Player._dirXHash, Player._lookDirection.x)
    Player.animator:SetFloat(Player._dirYHash, Player._lookDirection.y)
    Player.animator:SetFloat(Player._speedHash, movementSpeed)
end

function SetLookDirection(direction)
    if not Player.animator then return end

    if math.abs(direction.x) > math.abs(direction.y) then
        if direction.x > 0 then
            Player._lookDirection = CS.UnityEngine.Vector2.right
        else
            Player._lookDirection = CS.UnityEngine.Vector2.left
        end
    else
        if direction.y > 0 then
            Player._lookDirection = CS.UnityEngine.Vector2.up
        else
            Player._lookDirection = CS.UnityEngine.Vector2.down
        end
    end
end

return Player
