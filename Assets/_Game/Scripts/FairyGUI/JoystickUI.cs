using System;
using FairyGUI;
using UnityEngine;

[Serializable]
public class JoystickUI
{
	private GComponent _view;
	JoystickModule _joystick;
	
	public void Initialize(GComponent view)
	{
		_view = view;
		_joystick = new JoystickModule(_view);
		_joystick.onMove.Add(__joystickMove);
		_joystick.onEnd.Add(__joystickEnd);
	}
	
	void __joystickMove(EventContext context)
	{
		float degree = (float)context.data;
		
		float rad = degree * Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad*-1));
		JoystickEvents.OnMove?.Invoke(direction);
	}

	void __joystickEnd()
	{
		JoystickEvents.OnEnd?.Invoke();
	}
}