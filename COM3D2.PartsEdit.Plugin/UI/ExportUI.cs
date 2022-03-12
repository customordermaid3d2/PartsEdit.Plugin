using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class ExportUI {
        string fileName = "";
        ObjectData objectData = null;
        public void Draw() {
            if (objectData == null) {
                objectData = PresetManager.GetObjectDataFromObject();
            }

            DrawFileNameInput();

            GUILayout.FlexibleSpace();
            DrawBackButton();
        }

        void DrawFileNameInput() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("ファイル名", UIParams.Instance.lStyle, GUILayout.ExpandWidth(false));
                fileName = GUILayout.TextField(fileName, UIParams.Instance.textStyle, GUILayout.ExpandWidth(true));
                if (fileName == "") {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("Export", UIParams.Instance.bStyle, GUILayout.ExpandWidth(false))) {
                    PresetManager.SaveObjectData(fileName);
                    Setting.mode = Mode.Edit;
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }

        void DrawObjectData() {
            if (objectData.rootData != null) {
                GUILayout.Label("RootObject");
            }
        }

        void DrawBackButton() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("戻る", UIParams.Instance.bStyle)) {
                    Setting.mode = Mode.Edit;
                    objectData = null;
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
