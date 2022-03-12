using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class BoneDisplaySettingUI {
        int keyIndex = 0;
        KeyCode[] keyCodeList = null;
        GUIContent[] keyContentList = null;
        ComboBoxLO keyCombo = null;

        int normalColorIndex = 0;
        int selectColorIndex = 0;
        Color[] colorList = null;
        GUIContent[] colorContentList = null;
        ComboBoxLO normalColorCombo = null;
        ComboBoxLO selectColorCombo = null;

        int bodyBoneDisplayIndex = 0;
        GUIContent[] bodyBoneDsiplayContents = new GUIContent[] {
            new GUIContent("非表示"),
            new GUIContent("表示")
        };

        public BoneDisplaySettingUI() {
            InitColorList();
            InitKeyList();
        }

        public void Draw() {
            GUILayout.Label("ボーン表示設定", UIParams.Instance.lStyle);
            UIUtil.BeginIndentArea();
            {
                DrawNormalBoneColorSetting();
                DrawSelectBoneColorSetting();
                DrawSelectKeySetting();
                DrawBodyBoneDisplaySetting();
                GUILayout.FlexibleSpace();
                DrawReturnButton();
            }
            UIUtil.EndoIndentArea();
        }

        void DrawNormalBoneColorSetting() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("通常ボーン色", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                int tempIndex = normalColorCombo.ShowScroll(GUILayout.ExpandHeight(false));
                if (normalColorIndex != tempIndex) {
                    normalColorIndex = tempIndex;
                    Setting.normalBoneColor.SetValue(colorList[normalColorIndex]);
                }
            }
            GUILayout.EndHorizontal();
        }

        void DrawSelectBoneColorSetting() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("選択ボーン色", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                int tempIndex = selectColorCombo.ShowScroll(GUILayout.ExpandHeight(false));
                if (selectColorIndex != tempIndex) {
                    selectColorIndex = tempIndex;
                    Setting.selectBoneColor.SetValue(colorList[selectColorIndex]);
                }
            }
            GUILayout.EndHorizontal();
        }

        void DrawSelectKeySetting() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("選択キー", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                int tempIndex = keyCombo.ShowScroll(GUILayout.ExpandHeight(false));
                if (keyIndex != tempIndex) {
                    keyIndex = tempIndex;
                    Setting.boneSelectKey = keyCodeList[keyIndex];
                }
            }
            GUILayout.EndHorizontal();
        }

        void DrawBodyBoneDisplaySetting() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("メイド体ボーン表示", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                int tempIndex = GUILayout.Toolbar((int)(Setting.bodyBoneDisplay.GetValue()), bodyBoneDsiplayContents, UIParams.Instance.tStyle);
                if(bodyBoneDisplayIndex != tempIndex) {
                    bodyBoneDisplayIndex = tempIndex;
                    Setting.bodyBoneDisplay.SetValue((BodyBoneDisplay)tempIndex);
                }
            }
            GUILayout.EndHorizontal();
        }

        void DrawReturnButton() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("戻る", UIParams.Instance.bStyle)) {
                    Setting.mode = Mode.Edit;
                    Setting.SaveIni();
                }
            }
            GUILayout.EndHorizontal();
        }

        void InitKeyList() {
            keyCodeList = (KeyCode[])Enum.GetValues(typeof(KeyCode));
            keyIndex = Array.IndexOf(keyCodeList, Setting.boneSelectKey);
            keyContentList = keyCodeList.Select(code => new GUIContent(code.ToString())).ToArray();
            keyCombo = new ComboBoxLO(keyContentList[keyIndex], keyContentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
        }

        void InitColorList() {
            string[] colorStrList = { "white", "black", "gray", "red", "green", "blue", "yellow", "cyan" };
            colorList = colorStrList.Select(str => ColorUtil.GetColorFromName(str)).ToArray();
            normalColorIndex = Array.IndexOf(colorStrList, ColorUtil.GetNameFromColor(Setting.normalBoneColor.GetValue()));
            selectColorIndex = Array.IndexOf(colorStrList, ColorUtil.GetNameFromColor(Setting.selectBoneColor.GetValue()));
            colorContentList = colorStrList.Select(str => new GUIContent(str)).ToArray();
            normalColorCombo = new ComboBoxLO(colorContentList[normalColorIndex], colorContentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
            selectColorCombo = new ComboBoxLO(colorContentList[selectColorIndex], colorContentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
        }
    }
}
