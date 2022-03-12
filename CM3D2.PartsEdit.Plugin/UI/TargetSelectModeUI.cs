using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class TargetSelectModeUI {
        GUIContent[] targetSelectModeContents = new GUIContent[] {
            new GUIContent("メイドパーツ"),
            new GUIContent("複数メイド")
        };

        public void Draw() {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("オブジェクト選択モード", UIParams.Instance.lStyle);
                GUILayout.FlexibleSpace();
                Setting.targetSelectMode = GUILayout.Toolbar(Setting.targetSelectMode, targetSelectModeContents, UIParams.Instance.tStyle);
            }
            GUILayout.EndHorizontal();
        }
    }
}
