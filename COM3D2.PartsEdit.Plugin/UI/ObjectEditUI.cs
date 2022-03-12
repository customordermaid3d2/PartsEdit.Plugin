using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace CM3D2.PartsEdit.Plugin {
    class ObjectEditUI {
        MaidObjectUI maidObjectUI = new MaidObjectUI();
        SkinnedMeshObjectEditUI smoEditUI = new SkinnedMeshObjectEditUI();

        public void Draw() {
            GUILayout.Label("操作", UIParams.Instance.lStyle);

            UIUtil.BeginIndentArea();
            {
                switch (Setting.targetSelectMode) {
                    case 0:
                        maidObjectUI.Draw();
                        break;
                    case 1:
                        smoEditUI.Draw();
                        break;
                    default:
                        break;
                }

            }
            UIUtil.EndoIndentArea();
        }
    }
}
