﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_ReflectionProbeClearFlags : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.ReflectionProbeClearFlags");
		addMember(l,1,"Skybox");
		addMember(l,2,"SolidColor");
		LuaDLL.lua_pop(l, 1);
	}
}
