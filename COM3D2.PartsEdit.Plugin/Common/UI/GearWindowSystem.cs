using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.SceneManagement;

using GearMenu;

internal class GearWindowSystem : MonoBehaviour {
    #region メンバ変数
    bool sceneChangeHide = true;
    bool visible = false;

    GameObject iconGO;
    // アイコン画像データ
    PngData scIcon = new PngData("GearIcon.png");
    bool registered = false;

    UIWindow uiWindow;
    #endregion

    #region イベント関数
    void Awake() {
        uiWindow = gameObject.AddComponent<UIWindow>();

#if UNITY_5_5_OR_NEWER
        SceneManager.sceneLoaded += OnSceneLoaded;
#endif
    }

#if UNITY_5_5_OR_NEWER
    void OnSceneLoaded(Scene loaded, LoadSceneMode mode) {
        if (registered) {
            if (sceneChangeHide) {
                SetVisible(false);
            }
            return;
        }
        if (SceneManager.GetActiveScene().name == "SceneTitle") {
            // 歯車に登録
            string label = PluginInfo.Name + " " + PluginInfo.Version;
            iconGO = Buttons.Add(PluginInfo.Name, label, scIcon.GetData(), ClickGearButton);
            Buttons.SetFrameColor(iconGO, Color.black);
            registered = true;
        }
    }
#else
    void OnLevelWasLoaded(int level) {
        if (registered) {
            if (sceneChangeHide) {
                SetVisible(false);
            }
            return;
        }
        if (SceneManager.GetActiveScene().name == "SceneTitle") {
            // 歯車に登録
            string label = PluginInfo.Name + " " + PluginInfo.Version;
            iconGO = Buttons.Add(PluginInfo.Name, label, scIcon.GetData(), ClickGearButton);
            Buttons.SetFrameColor(iconGO, Color.black);
            registered = true;
        }
    }
#endif

    void Update() {
        // Window側で閉じられた場合
        if (visible != uiWindow.IsVisible) {
            visible = uiWindow.IsVisible;
            if (visible) {
                Buttons.SetFrameColor(iconGO, Color.red);
            } else {
                Buttons.SetFrameColor(iconGO, Color.black);
            }
        }
    }
#endregion

#region public関数
    public UIWindow GetUIWindow() {
        return uiWindow;
    }

    public void SetVisible(bool fVisible) {
        if (visible == fVisible) {
            return;
        }
        visible = fVisible;
        if (visible) {
            Buttons.SetFrameColor(iconGO, Color.red);
        } else {
            Buttons.SetFrameColor(iconGO, Color.black);
        }
        uiWindow.SetVisible(visible);
    }
#endregion

#region private関数
    void ClickGearButton(GameObject goButton) {
        SetVisible(!uiWindow.IsVisible);
    }
#endregion
}
