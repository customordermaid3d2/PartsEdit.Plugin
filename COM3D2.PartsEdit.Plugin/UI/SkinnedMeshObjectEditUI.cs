﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class SkinnedMeshObjectEditUI {
        MultipleMaidObjectSelectUI objectSelectUI = new MultipleMaidObjectSelectUI();

        public void Draw() {
            DrawObject();
        }

        void DrawObject() {
            GUILayout.BeginHorizontal();
            {
                string objectName = "未選択";
                if (CommonUIData.obj) {
                    objectName = CommonUIData.obj.name;
                }
                GUILayout.Label("オブジェクト:" /*+ objectName*/, UIParams.Instance.lStyle);

                GUILayout.FlexibleSpace();

                objectSelectUI.DrawCombo();

                if (!CommonUIData.obj || !BackUpData.GetObjectDataExist(CommonUIData.obj)) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestoreObject(CommonUIData.obj);
                    BackUpData.Refresh();
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();

            UIUtil.BeginIndentArea();
            {
                DrawImportExport();
            }
            UIUtil.EndoIndentArea();

            UIUtil.BeginIndentArea();
            {
                DrawBone();
            }
            UIUtil.EndoIndentArea();
        }

        void DrawImportExport() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("プリセット", UIParams.Instance.lStyle);
                if (!CommonUIData.obj) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("Import", UIParams.Instance.bStyle)) {
                    Setting.mode = Mode.Import;
                }
                if (GUILayout.Button("Export", UIParams.Instance.bStyle)) {
                    Setting.mode = Mode.Export;
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }


        void DrawBone() {
            GUILayout.BeginHorizontal();
            {
                string boneName = "未選択";
                if (CommonUIData.bone) {
                    boneName = CommonUIData.bone.name;
                }
                GUILayout.Label("ボーン:" + boneName, UIParams.Instance.lStyle);

                GUILayout.FlexibleSpace();

                if (!CommonUIData.bone || !CommonUIData.obj || !BackUpData.GetBoneDataExist(CommonUIData.obj, CommonUIData.bone)) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestoreBone(CommonUIData.obj, CommonUIData.bone);
                    BackUpData.Refresh();
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();

            UIUtil.BeginIndentArea();
            {
                DrawTransform();
            }
            UIUtil.EndoIndentArea();
        }

        void DrawTransform() {
            BackUpBoneData boneData = null;
            if (CommonUIData.bone) {
                boneData = BackUpData.GetOrNullBoneData(CommonUIData.obj, CommonUIData.bone);
            }

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("位置", UIParams.Instance.lStyle);

                GUILayout.FlexibleSpace();

                if (boneData == null || !boneData.changedPos) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestorePosition(CommonUIData.obj, CommonUIData.bone);
                    BackUpData.Refresh();
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("回転", UIParams.Instance.lStyle);

                GUILayout.FlexibleSpace();

                if (boneData == null || !boneData.changedRot) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestoreRotation(CommonUIData.obj, CommonUIData.bone);
                    BackUpData.Refresh();
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("拡縮", UIParams.Instance.lStyle);

                GUILayout.FlexibleSpace();

                if (boneData == null || !boneData.changedScl) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestoreScale(CommonUIData.obj, CommonUIData.bone);
                    BackUpData.Refresh();
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }

    }
}
