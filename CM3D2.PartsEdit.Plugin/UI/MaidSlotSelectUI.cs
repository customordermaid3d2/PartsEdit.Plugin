﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class MaidSlotSelectUI {
        ComboBoxLO combo = null;

        string maidGuid = "";
        Maid maid = null;
        bool maidNotNull = false;

        HashSet<int> activeSlotHash = new HashSet<int>();
        List<int> activeSlotIdList = new List<int>();
        List<Maid> activeMaidList = new List<Maid>();

        int selectSlotId = -2;

        public void Draw() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("スロット選択");
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                {
                    if (combo == null) {
                        ResetSlotList();
                    } else if (maid != CommonUIData.maid || (maidNotNull && !maid)) {
                        ResetSlotList();
                    } else if (CheckActiveSlotChange()) {
                        ResetSlotList();
                    }
                    //selectSlotId = combo.ShowScroll(GUILayout.ExpandWidth(true));
                    int num = combo.ShowScroll(GUILayout.ExpandWidth(false));
                    if (num == 0 || num == 1) {
                        selectSlotId = num - 2;
                    } else if (num > 1) {
                        selectSlotId = activeSlotIdList[num - 2];
                    }
                    CommonUIData.slotNo = selectSlotId;
                    CommonUIData.obj = GetSlotObject();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }

        public void DrawCombo() {
            GUILayout.BeginVertical();
            {
                if (combo == null) {
                    ResetSlotList();
                } else if (maid != CommonUIData.maid || (maidNotNull && !maid)) {
                    selectSlotId = -2;
                    ResetSlotList();
                } else if (CheckActiveSlotChange()) {
                    ResetSlotList();
                }
                //selectSlotId = combo.ShowScroll(GUILayout.ExpandWidth(true));
                int num = combo.ShowScroll(GUILayout.ExpandWidth(false));
                if (num == 0 || num == 1) {
                    selectSlotId = num - 2;
                } else if (num > 1) {
                    selectSlotId = activeSlotIdList[num - 2];
                }
                if (CommonUIData.slotNo != selectSlotId) {
                    CommonUIData.slotNo = selectSlotId;
                    CommonUIData.obj = GetSlotObject();
                    CommonUIData.bone = null;
                }
            }
            GUILayout.EndVertical();
        }

        public void SetMaidGuid(string guid) {
            if (maidGuid == guid) return;
            maidGuid = guid;

            maid = GameMain.Instance.CharacterMgr.GetMaid(guid);
            ResetSlotList();
        }

        void ResetSlotList() {
            maid = CommonUIData.maid;
            maidNotNull = maid != null;
            activeSlotIdList = GetActiveSlotList();
            string selectSlotName;
            if (CommonUIData.maid && selectSlotId == -1) {
                selectSlotName = "ベース";
            } else if (CommonUIData.maid && activeSlotIdList.Contains(selectSlotId)) {
                selectSlotName = maid.body0.goSlot[selectSlotId].Category;
            } else {
                selectSlotId = -2;
                CommonUIData.slotNo = -2;
                CommonUIData.obj = null;
                selectSlotName = "未選択";
            }

            activeSlotHash = new HashSet<int>();
            activeSlotIdList.ForEach(slot => activeSlotHash.Add(slot));

            GUIContent[] slotNameList;
            if (CommonUIData.maid == null) {
                slotNameList = new GUIContent[1];
                slotNameList[0] = new GUIContent("未選択");
            } else {
                slotNameList = new GUIContent[activeSlotIdList.Count + 2];

                slotNameList[0] = new GUIContent("未選択");
                slotNameList[1] = new GUIContent("ベース");
                for (int i = 0; i < activeSlotIdList.Count; i++) {
                    slotNameList[i + 2] = new GUIContent(CommonUIData.maid.body0.goSlot[activeSlotIdList[i]].Category);
                }
            }
            //GUIContent[] slotNameList = new GUIContent[activeSlotIdList.Count];
            //for (int i = 0; i < activeSlotIdList.Count; i++) {
            //    slotNameList[i] = new GUIContent(maid.body0.goSlot[activeSlotIdList[i]].Category);
            //}
            combo = new ComboBoxLO(new GUIContent(selectSlotName), slotNameList, UIParams.Instance.bStyle, UIParams.Instance.winStyle, UIParams.Instance.listStyle, false);
        }

        bool CheckActiveSlotChange() {
            List<int> activeSlotList = GetActiveSlotList();
            if (activeSlotHash.Count != activeSlotList.Count) return true;
            foreach (int slotId in activeSlotList) {
                if (!activeSlotHash.Contains(slotId)) {
                    return true;
                }
            }
            return false;
        }

        List<int> GetActiveSlotList() {
            if (CommonUIData.maid == null) {
                return new List<int>();
            }
#if COM3D2
            return CommonUIData.maid.body0.goSlot.Where(slot => GetSlotActive(slot)).Select(slot => (int)slot.SlotId).ToList();
#else
            return CommonUIData.maid.body0.goSlot.Where(slot => GetSlotActive(slot)).Select(slot => slot.CategoryIdx).ToList();
#endif
        }

        bool GetSlotActive(TBodySkin tbs) {
            if (tbs == null) return false;
            if (!tbs.obj) return false;
            if (!tbs.boVisible) return false;
            return true;
        }

        public GameObject GetSlotObject() {
            if (!CommonUIData.maid) return null;
            if (!CommonUIData.maid.body0) return null;
            if (selectSlotId == -2) return null;
            if (selectSlotId == -1) {
                return CommonUIData.maid.body0.m_Bones.gameObject;
            }
            return CommonUIData.maid.body0.goSlot[selectSlotId].obj;
        }

        public int GetSlotNum() {
            return selectSlotId;
        }
    }
}
