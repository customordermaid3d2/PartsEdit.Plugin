using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityInjector;

namespace CM3D2.PartsEdit.Plugin {
    class PartsEdit : MonoBehaviour {
        UIWindow window;

        SettingUI settingUI = new SettingUI();
        TargetSelectModeUI targetSelectModeUI = new TargetSelectModeUI();
        ObjectEditUI objectEditUI = new ObjectEditUI();

        ImportUI importUI = new ImportUI();
        ExportUI exportUI = new ExportUI();
        BoneDisplaySettingUI boneDisplaySettingUI = new BoneDisplaySettingUI();
        GizmoSettingUI gizmoSettingUI = new GizmoSettingUI();

        FinishUI finishUI = null;

        BoneEdit boneEdit;

        Mode mode = Mode.Edit;

        void Start() {
            window = gameObject.AddComponent<GearWindowSystem>().GetUIWindow();
            window.SetWindowInfo("PartsEdit", PluginInfo.Version);

            window.AddDrawEvent(Draw);

            finishUI = new FinishUI(window);

            boneEdit = gameObject.AddComponent<BoneEdit>();
        }

        void Draw() {
            if(mode != Setting.mode) {
                mode = Setting.mode;
                if(mode == Mode.Import) {
                    importUI.Reset();
                }
            }
            switch (Setting.mode) {
                case Mode.Edit:
                    settingUI.Draw();

                    GUILayout.Label("");
                    targetSelectModeUI.Draw();

                    //GUILayout.Label("");
                    //objSelectUI.Draw();

                    GUILayout.Label("");
                    objectEditUI.Draw();

                    GUILayout.FlexibleSpace();

                    GUILayout.Label("");
                    finishUI.Draw();
                    break;
                case Mode.Import:
                    importUI.Draw();
                    break;
                case Mode.Export:
                    exportUI.Draw();
                    break;
                case Mode.BoneDisplaySetting:
                    boneDisplaySettingUI.Draw();
                    break;
                case Mode.GizmoSetting:
                    gizmoSettingUI.Draw();
                    break;
            }
        }

    }
}
