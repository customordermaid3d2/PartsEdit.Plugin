using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

internal class ComboBoxBase {
    protected static bool forceToUnShow = false;
    protected static int useControlID = -1;
    protected bool isClickedComboButton = false;
    protected int selectedItemIndex = 0;
    protected int backSelectedItemIndex = 0;

    protected float itemWidth;
    protected float itemHeight;

    protected GUIContent buttonContent;
    protected GUIContent[] listContent;
    protected GUIStyle buttonStyle;
    protected GUIStyle boxStyle;
    protected GUIStyle listStyle;

    protected ComboBoxBase(GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle)
        : this(buttonContent, listContent, "button", "box", listStyle) {
    }

    protected ComboBoxBase(GUIContent buttonContent, GUIContent[] listContent,
                           GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle) {
        this.buttonContent = buttonContent;
        this.listContent = listContent;
        this.buttonStyle = buttonStyle;
        this.boxStyle = boxStyle;
        this.listStyle = listStyle;
        InitIndex();
        InitSize();
    }

    protected void InitSize() {
        int maxLength = 0;
        foreach (GUIContent c in listContent) {
            if (maxLength < c.text.Length) maxLength = c.text.Length;
        }
        itemWidth = maxLength * 9f;
        itemHeight = listStyle.CalcHeight(listContent[0], 1.0f);

    }
    protected void InitIndex() {
        for (int i = 0; i < listContent.Length; i++) {
            if (buttonContent.text == listContent[i].text) {
                selectedItemIndex = i;
                return;
            }
        }
        selectedItemIndex = -1;
    }
    public int SelectItem(string item) {
        string itemLow = item.ToLower();
        for (int i = 0; i < listContent.Length; i++) {
            if (listContent[i].text.ToLower() == itemLow) {
                selectedItemIndex = i;
                return i;
            }
        }
        return -1;
    }
    public bool IsClickedComboButton {
        get { return isClickedComboButton; }
    }

    public int ItemCount {
        get { return listContent.Length; }
    }

    public int SelectedItemIndex {
        get { return selectedItemIndex; }
        set {
            if (selectedItemIndex != value) {
                if (value < listContent.Length && value >= 0) {
                    selectedItemIndex = value;
                    buttonContent = listContent[selectedItemIndex];
                } else {
                    buttonContent = GUIContent.none;
                    selectedItemIndex = -1;
                }
            }
        }
    }

    protected Vector2 scrollPosition = Vector2.zero;
}

internal class ComboBox : ComboBoxBase {
    public Rect rect;

    public ComboBox(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle)
        : base(buttonContent, listContent, listStyle) {
        this.rect = rect;
    }

    public ComboBox(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
        : base(buttonContent, listContent, buttonStyle, boxStyle, listStyle) {
        this.rect = rect;
    }

    public int Show() {
        if (forceToUnShow) {
            forceToUnShow = false;
            isClickedComboButton = false;
        }

        bool done = false;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        switch (Event.current.GetTypeForControl(controlID)) {
            case EventType.mouseUp:
                done |= isClickedComboButton;
                break;
        }

        if (GUI.Button(rect, buttonContent, buttonStyle)) {
            if (useControlID == -1) {
                useControlID = controlID;
                isClickedComboButton = false;
            }

            if (useControlID != controlID) {
                forceToUnShow = true;
                useControlID = controlID;
            }
            isClickedComboButton = true;
        }

        if (isClickedComboButton) {
            var listRect = new Rect(rect.x, rect.y + itemHeight,
                      rect.width, itemHeight * listContent.Length);

            GUI.Box(listRect, string.Empty, boxStyle);
            int newSelectedItemIndex = GUI.SelectionGrid(listRect, selectedItemIndex, listContent, 1, listStyle);
            if (newSelectedItemIndex != selectedItemIndex) {
                SelectedItemIndex = newSelectedItemIndex;
            }
        }

        isClickedComboButton &= !done;
        return selectedItemIndex;
    }
}

/// <summary>
/// GUILayout版のコンボボックスクラス. 
/// </summary>
internal class ComboBoxLO : ComboBoxBase {
    private readonly bool labelFixed;
    public ComboBoxLO(GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle)
        : base(buttonContent, listContent, listStyle) {
    }

    public ComboBoxLO(GUIContent buttonContent, GUIContent[] listContent,
                      GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle, bool labelFixed)
        : base(buttonContent, listContent, buttonStyle, boxStyle, listStyle) {
        this.labelFixed = labelFixed;
    }
    public void SetItemWidth(float itemWidth) {
        this.itemWidth = itemWidth;
    }
    public int Show(GUILayoutOption buttonOpt) {
        if (forceToUnShow) {
            forceToUnShow = false;
            isClickedComboButton = false;
        }

        bool done = false;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        switch (Event.current.GetTypeForControl(controlID)) {
            case EventType.mouseUp:
                done |= isClickedComboButton;
                break;
        }

        bool expand = isClickedComboButton;
        if (expand) GUILayout.BeginVertical(boxStyle, GUILayout.Width(itemWidth));
        try {
            if (GUILayout.Button(buttonContent, buttonStyle, buttonOpt, GUILayout.Width(itemWidth))) {
                if (useControlID == -1) {
                    useControlID = controlID;
                    isClickedComboButton = false;
                }

                if (useControlID != controlID) {
                    forceToUnShow = true;
                    useControlID = controlID;
                }
                isClickedComboButton = true;
            }

            if (isClickedComboButton) {
                float height = itemHeight * listContent.Length;
                int newSelectedItemIndex = GUILayout.SelectionGrid(selectedItemIndex, listContent, 1, listStyle,
                                                                   GUILayout.Width(itemWidth), GUILayout.Height(height));
                if (newSelectedItemIndex != selectedItemIndex) {
                    // ラベル指定に応じて
                    if (!labelFixed) {
                        SelectedItemIndex = newSelectedItemIndex;
                    } else {
                        selectedItemIndex = newSelectedItemIndex;
                    }
                }
            }
        } finally {
            if (expand) GUILayout.EndVertical();
        }

        isClickedComboButton &= !done;
        return selectedItemIndex;
    }

    public int ShowScroll(GUILayoutOption buttonOpt) {
        //if (forceToUnShow) {
        //    forceToUnShow = false;
        //    isClickedComboButton = false;
        //}

        bool done = false;
        //int controlID = GUIUtility.GetControlID(FocusType.Passive);

        //switch (Event.current.GetTypeForControl(controlID)) {
        //    case EventType.mouseUp:
        //        done |= isClickedComboButton;
        //        break;
        //}

        bool expand = isClickedComboButton;
        if (expand) {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.BeginVertical(boxStyle, GUILayout.Width(itemWidth));
        }
        try {
            if (GUILayout.Button(buttonContent, buttonStyle, buttonOpt)) {
                //if (useControlID == -1) {
                //    useControlID = controlID;
                //    isClickedComboButton = false;
                //}

                //if (useControlID != controlID) {
                //    forceToUnShow = true;
                //    useControlID = controlID;
                //}
                //isClickedComboButton = true;
                if (IsClickedComboButton) {
                    isClickedComboButton = false;
                    SelectedItemIndex = backSelectedItemIndex;
                } else {
                    isClickedComboButton = true;
                    backSelectedItemIndex = selectedItemIndex;
                    selectedItemIndex = -1;
                }
            }

            if (isClickedComboButton) {
                float height = itemHeight * listContent.Length;
                int newSelectedItemIndex = GUILayout.SelectionGrid(selectedItemIndex, listContent, 1, listStyle, 
                                                                   /*GUILayout.Width(itemWidth),*/ GUILayout.Height(height));
                if (newSelectedItemIndex != selectedItemIndex) {
                    // ラベル指定に応じて
                    SelectedItemIndex = newSelectedItemIndex;
                    isClickedComboButton = false;
                }
            }
        } finally {
            if (expand) {
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
            }
        }

        //isClickedComboButton &= !done;
        if (IsClickedComboButton) {
            return backSelectedItemIndex;
        }
        return selectedItemIndex;
    }
}

