using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class MaidObjectUI {
        MaidSelectUI maidSelectUI = new MaidSelectUI();
        MaidSlotSelectUI slotSelectUI = new MaidSlotSelectUI();

        public void Draw() {
            DrawMaid();
        }

        void DrawMaid() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("メイド:"/* + maidName*/, UIParams.Instance.lStyle);

                GUILayout.FlexibleSpace();

                maidSelectUI.DrawCombo();

                if (!CommonUIData.maid || !BackUpData.GetMaidDataExist(CommonUIData.maid)) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestoreMaid(CommonUIData.maid);
                    BackUpData.Refresh();
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();

            UIUtil.BeginIndentArea();
            {
                DrawSlot();
            }
            UIUtil.EndoIndentArea();
        }

        void DrawSlot() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("スロット:" /*+ slotName*/, UIParams.Instance.lStyle);

                GUILayout.FlexibleSpace();
                slotSelectUI.DrawCombo();
                if (CommonUIData.slotNo == (int)EXSlot.None || !BackUpData.GetMaidSlotDataExist(CommonUIData.maid, CommonUIData.slotNo)) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestoreSlot(CommonUIData.maid, CommonUIData.slotNo);
                    BackUpData.Refresh();
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();

            UIUtil.BeginIndentArea();
            {
                DrawObject();
            }
            UIUtil.EndoIndentArea();
        }

        void DrawObject() {
            DrawImportExport();
            DrawYure();
            DrawBone();
        }

        void DrawImportExport() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("プリセット", UIParams.Instance.lStyle);
                if (!CommonUIData.obj) {
                    GUI.enabled = false;
                }
                if(GUILayout.Button("Import", UIParams.Instance.bStyle)) {
                    Setting.mode = Mode.Import;
                }
                if(GUILayout.Button("Export", UIParams.Instance.bStyle)) {
                    Setting.mode = Mode.Export;
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }

        void DrawYure() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("揺れボーン", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                if (!YureUtil.GetYureAble(CommonUIData.maid, CommonUIData.slotNo)) {
                    GUI.enabled = false;
                    GUILayout.Toggle(false, "", UIParams.Instance.tStyle);
                    GUI.enabled = true;
                } else {
                    bool bYure = YureUtil.GetYureState(CommonUIData.maid, CommonUIData.slotNo);
                    bool tempYure = GUILayout.Toggle(bYure, "", UIParams.Instance.tStyle);
                    if (tempYure != bYure) {
                        YureUtil.SetYureState(CommonUIData.maid, CommonUIData.slotNo, tempYure);

                        BackUpObjectData objectData = BackUpData.GetOrAddMaidObjectData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj);
                        if (objectData.changedYure && tempYure == objectData.bYure) {
                            // ボーンデータがなければオブジェクトデータ削除
                            if (objectData.boneDataDic.Count == 0) {
                                BackUpSlotData slotData = BackUpData.GetOrNullMaidSlotData(CommonUIData.maid, CommonUIData.slotNo);
                                slotData.objectDataDic.Remove(CommonUIData.obj);
                            } else {
                                objectData.changedYure = false;
                            }
                            BackUpData.Refresh();
                        } else {
                            objectData.changedYure = true;
                            objectData.bYure = bYure;
                        }
                    }
                }
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

                if (!CommonUIData.bone || !BackUpData.GetMaidBoneDataExist(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone)) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestoreBone(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
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
                boneData = BackUpData.GetOrNullMaidBoneData(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
            }

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("位置", UIParams.Instance.lStyle);

                GUILayout.FlexibleSpace();

                if (boneData == null || !boneData.changedPos) {
                    GUI.enabled = false;
                }
                if (GUILayout.Button("R", UIParams.Instance.bStyle)) {
                    BackUpData.RestorePosition(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
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
                    BackUpData.RestoreRotation(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
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
                    BackUpData.RestoreScale(CommonUIData.maid, CommonUIData.slotNo, CommonUIData.obj, CommonUIData.bone);
                    BackUpData.Refresh();
                }
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }
    }
}
