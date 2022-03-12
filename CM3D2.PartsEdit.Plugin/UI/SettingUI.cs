using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class SettingUI {
        GUIContent[] boneDsiplayContents = new GUIContent[] {
            new GUIContent("非表示"),
            new GUIContent("表示")
        };

        GUIContent[] coordinateTypeContents = new GUIContent[] {
            new GUIContent("Local"),
            new GUIContent("Global"),
            new GUIContent("View")
        };

        GUIContent[] gizmoTypeContents = new GUIContent[] {
            new GUIContent("無し"),
            new GUIContent("位置"),
            new GUIContent("回転")
        };

        GUIContent[] gizmoTypeLocalContents = new GUIContent[] {
            new GUIContent("無し"),
            new GUIContent("位置"),
            new GUIContent("回転"),
            new GUIContent("拡縮")
        };

        public void Draw() {
            GUILayout.Label("設定", UIParams.Instance.lStyle);

            UIUtil.BeginIndentArea();
            {
                DrawBoneDisplay();
                //DrawGizmoEdit();
                DrawCoordinateType();
                DrawGizmoType();
            }
            UIUtil.EndoIndentArea();
        }

        void DrawBoneDisplay() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("ボーン表示", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                Setting.boneDisplay = (BoneDisplay)GUILayout.Toolbar((int)Setting.boneDisplay, boneDsiplayContents, UIParams.Instance.tStyle);
                if (GUILayout.Button("詳細")) {
                    Setting.mode = Mode.BoneDisplaySetting;
                }
            }
            GUILayout.EndHorizontal();
        }

        //void DrawGizmoEdit() {
        //    GUILayout.BeginHorizontal();
        //    {
        //        GUILayout.Label("ギズモ表示", UIParams.Instance.lStyle);
        //        GUILayout.FlexibleSpace();
        //        Setting.gizmoEdit = GUILayout.Toggle(Setting.gizmoEdit, "", UIParams.Instance.tStyle);
        //    }
        //    GUILayout.EndHorizontal();
        //}

        void DrawCoordinateType() {
            GUILayout.BeginHorizontal();
            {
                if (Setting.gizmoType == GizmoType.Scale) {
                    GUI.enabled = false;
                }
                GUILayout.Label("座標タイプ", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                Setting.coordinateType = (BoneGizmoRenderer.COORDINATE) GUILayout.Toolbar((int)Setting.coordinateType, coordinateTypeContents, UIParams.Instance.tStyle);

                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }

        void DrawGizmoType() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("ギズモタイプ", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                if (Setting.coordinateType == BoneGizmoRenderer.COORDINATE.Local) {
                    Setting.gizmoType = (GizmoType)GUILayout.Toolbar((int)Setting.gizmoType, gizmoTypeLocalContents, UIParams.Instance.tStyle);
                } else {
                    Setting.gizmoType = (GizmoType)GUILayout.Toolbar((int)Setting.gizmoType, gizmoTypeContents, UIParams.Instance.tStyle);
                }
                if (GUILayout.Button("詳細")) {
                    Setting.mode = Mode.GizmoSetting;
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
