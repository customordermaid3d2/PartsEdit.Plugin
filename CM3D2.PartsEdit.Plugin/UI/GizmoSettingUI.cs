using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class GizmoSettingUI {
        int smallIndex = 0;
        int bigIndex = 0;
        KeyCode[] keyCodeList = null;
        GUIContent[] contentList = null;
        ComboBoxLO smallCombo = null;
        ComboBoxLO bigCombo = null;

        public GizmoSettingUI() {
            keyCodeList = (KeyCode[])Enum.GetValues(typeof(KeyCode));
            smallIndex = Array.IndexOf(keyCodeList, Setting.gizmoSmallKey);
            bigIndex = Array.IndexOf(keyCodeList, Setting.gizmoBigKey);
            contentList = keyCodeList.Select(code => new GUIContent(code.ToString())).ToArray();
            smallCombo = new ComboBoxLO(contentList[smallIndex], contentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
            bigCombo = new ComboBoxLO(contentList[bigIndex], contentList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
        }

        public void Draw() {
            GUILayout.Label("ギズモ表示設定", UIParams.Instance.lStyle);
            UIUtil.BeginIndentArea();
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("移動量減少キー", UIParams.Instance.lStyle);
                    GUILayout.FlexibleSpace();
                    int tempIndex = smallCombo.ShowScroll(GUILayout.ExpandHeight(false));
                    if (smallIndex != tempIndex) {
                        smallIndex = tempIndex;
                        Setting.gizmoSmallKey = keyCodeList[smallIndex];
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("移動量増加キー", UIParams.Instance.lStyle);
                    GUILayout.FlexibleSpace();
                    int tempIndex = bigCombo.ShowScroll(GUILayout.ExpandHeight(false));
                    if (bigIndex != tempIndex) {
                        bigIndex = tempIndex;
                        Setting.gizmoBigKey = keyCodeList[bigIndex];
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();

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
            UIUtil.EndoIndentArea();
        }
    }
}
