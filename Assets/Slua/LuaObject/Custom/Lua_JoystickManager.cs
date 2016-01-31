﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_JoystickManager : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			JoystickManager o;
			o=new JoystickManager();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int registerEvent(IntPtr l) {
		try {
			JoystickManager self=(JoystickManager)checkSelf(l);
			JoystickManager.JoystickEventType a1;
			checkEnum(l,2,out a1);
			SLua.LuaFunction a2;
			checkType(l,3,out a2);
			self.registerEvent(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetInstance_s(IntPtr l) {
		try {
			var ret=JoystickManager.GetInstance();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"JoystickManager");
		addMember(l,registerEvent);
		addMember(l,GetInstance_s);
		createTypeMetatable(l,constructor, typeof(JoystickManager),typeof(Singleton<JoystickManager>));
	}
}
