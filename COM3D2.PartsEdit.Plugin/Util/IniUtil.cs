using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExIni;
using UnityInjector;
using UnityEngine;

internal static class IniUtil {
    public static IniFile preferences;
    public delegate void Delegate();
    public static event Delegate saveMethod = delegate () { };

    public static void Init(PluginBase pBase) {
        preferences = pBase.Preferences;
    }

    public static int GetIntValue(string section, string key, int defaultValue) {
        IniKey iniKey = preferences[section][key];
        int outValue;
        if (iniKey == null || string.IsNullOrEmpty(iniKey.Value) || !int.TryParse(iniKey.Value, out outValue)) {
            return defaultValue;
        }
        return outValue;
    }

    public static float GetFloatValue(string section, string key, float defaultValue) {
        IniKey iniKey = preferences[section][key];
        float outValue;
        if (iniKey == null || string.IsNullOrEmpty(iniKey.Value) || !float.TryParse(iniKey.Value, out outValue)) {
            return defaultValue;
        }
        return outValue;
    }

    public static string GetStringValue(string section, string key, string defaultValue) {
        IniKey iniKey = preferences[section][key];
        if (iniKey == null || iniKey.Value == null) {
            return defaultValue;
        }
        return iniKey.Value;
    }

    public static void Save() {
        saveMethod.Invoke();
    }
}
