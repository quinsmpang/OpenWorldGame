﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Hash128 : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.Hash128 o;
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			System.UInt32 a3;
			checkType(l,4,out a3);
			System.UInt32 a4;
			checkType(l,5,out a4);
			o=new UnityEngine.Hash128(a1,a2,a3,a4);
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Parse_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=UnityEngine.Hash128.Parse(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Hash128");
		addMember(l,Parse_s);
		createTypeMetatable(l,constructor, typeof(UnityEngine.Hash128),typeof(System.ValueType));
	}
}
