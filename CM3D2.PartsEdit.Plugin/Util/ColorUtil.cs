using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

internal static class ColorUtil {
    public static Color GetColorFromName(string name) {
        switch (name.ToLower()) {
            case "white":
                return Color.white;
            case "black":
                return Color.black;
            case "red":
                return Color.red;
            case "green":
                return Color.green;
            case "blue":
                return Color.blue;
            case "yellow":
                return Color.yellow;
            case "gray":
                return Color.gray;
            case "cyan":
                return Color.cyan;
            default:
                return Color.gray;
        }
    } 

    public static string GetNameFromColor(Color color) {
        if (color == Color.white) {
            return "white";
        }else if(color == Color.black) {
            return "black";
        } else if (color == Color.red) {
            return "red";
        } else if (color == Color.green) {
            return "green";
        } else if (color == Color.blue) {
            return "blue";
        } else if (color == Color.yellow) {
            return "yellow";
        } else if (color == Color.gray) {
            return "gray";
        } else if (color == Color.cyan) {
            return "cyan";
        }else {
            return "gray";
        }
    }
}
