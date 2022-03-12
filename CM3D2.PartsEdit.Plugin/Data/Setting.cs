using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExIni;
using UnityEngine;
using UnityInjector;

namespace CM3D2.PartsEdit.Plugin {
    static class Setting {
        public static Mode mode = Mode.Edit;
        public static BoneDisplay boneDisplay = BoneDisplay.Visible;
        public static BoneGizmoRenderer.COORDINATE coordinateType = BoneGizmoRenderer.COORDINATE.Local;
        public static GizmoType gizmoType = GizmoType.Rotation;
        public static int targetSelectMode = 0;

        public static KeyCode boneSelectKey = KeyCode.LeftAlt;
        public static KeyCode gizmoSmallKey = KeyCode.LeftShift;
        public static KeyCode gizmoBigKey = KeyCode.None;

        public static SettingValue<Color> normalBoneColor = new SettingValue<Color>(Color.white);
        public static SettingValue<Color> selectBoneColor = new SettingValue<Color>(Color.red);

        public static SettingValue<BodyBoneDisplay> bodyBoneDisplay = new SettingValue<BodyBoneDisplay>(BodyBoneDisplay.Visible);

        public class SettingValue<T> {
            T value;
            public delegate void Delegate(T ret);
            public event Delegate mDelegate = delegate (T ret) { };

            public SettingValue(T initValue) {
                value = initValue;
            }

            public T GetValue() {
                return value;
            }

            public void SetValue(T fValue) {
                value = fValue;
                mDelegate(value);
            }
        }

        public static void LoadIni() {
            LoadSetting();
            LoadBoneDisplay();
            LoadGizmoDisplay();
        }

        static void LoadSetting() {
            boneDisplay = (BoneDisplay)IniUtil.GetIntValue("Setting", "BoneDisplay", (int)BoneDisplay.Visible);
            coordinateType = (BoneGizmoRenderer.COORDINATE)IniUtil.GetIntValue("Setting", "CoordinateType", (int)BoneGizmoRenderer.COORDINATE.Local);
            gizmoType = (GizmoType)IniUtil.GetIntValue("Setting", "GizmoType", (int)GizmoType.Rotation);
        }

        static void LoadBoneDisplay() {
            boneSelectKey = (KeyCode)IniUtil.GetIntValue("BoneDisplay", "BoneSelectKey", (int)KeyCode.LeftAlt);
            normalBoneColor = new SettingValue<Color>(ColorUtil.GetColorFromName(IniUtil.GetStringValue("BoneDisplay", "NormalBoneColor", "white")));
            selectBoneColor = new SettingValue<Color>(ColorUtil.GetColorFromName(IniUtil.GetStringValue("BoneDisplay", "SelectBoneColor", "red")));
            bodyBoneDisplay = new SettingValue<BodyBoneDisplay>((BodyBoneDisplay)IniUtil.GetIntValue("BoneDisplay", "BodyBoneDisplay", (int)BodyBoneDisplay.Visible));
        }

        static void LoadGizmoDisplay() {
            gizmoSmallKey = (KeyCode)IniUtil.GetIntValue("GizmoDisplay", "GizmoSmallKey", (int)KeyCode.LeftShift);
            gizmoBigKey = (KeyCode)IniUtil.GetIntValue("GizmoDisplay", "GizmoBigKey", (int)KeyCode.None);
        }

        public static void SaveIni() {
            SaveSetting();
            SaveBoneDisplay();
            SaveGizmoDisplay();
            IniUtil.Save();
        }

        static void SaveSetting() {
            IniUtil.preferences["Setting"]["BoneDisplay"].Value = ((int)boneDisplay).ToString();
            IniUtil.preferences["Setting"]["CoordinateType"].Value = ((int)coordinateType).ToString();
            IniUtil.preferences["Setting"]["GizmoType"].Value = ((int)gizmoType).ToString();
        }

        static void SaveBoneDisplay() {
            IniUtil.preferences["BoneDisplay"]["BoneSelectKey"].Value = ((int)boneSelectKey).ToString();
            IniUtil.preferences["BoneDisplay"]["NormalBoneColor"].Value = ColorUtil.GetNameFromColor(normalBoneColor.GetValue());
            IniUtil.preferences["BoneDisplay"]["SelectBoneColor"].Value = ColorUtil.GetNameFromColor(selectBoneColor.GetValue());
            IniUtil.preferences["BoneDisplay"]["BodyBoneDisplay"].Value = ((int)bodyBoneDisplay.GetValue()).ToString();
        }

        static void SaveGizmoDisplay() {
            IniUtil.preferences["GizmoDisplay"]["GizmoSmallKey"].Value = ((int)gizmoSmallKey).ToString();
            IniUtil.preferences["GizmoDisplay"]["GizmoBigKey"].Value = ((int)gizmoBigKey).ToString();
        }
    }

    enum Mode {
        Edit,
        Import,
        Export,
        BoneDisplaySetting,
        GizmoSetting,
    }

    enum BoneDisplay {
        None,
        Visible,
        Choisable
    }

    enum GizmoType {
        None,
        Position,
        Rotation,
        Scale
    }

    enum TargetType {
        MaidParts,
        MultipleMaids
    }

    enum BodyBoneDisplay {
        Invisible,
        Visible
    }
}
