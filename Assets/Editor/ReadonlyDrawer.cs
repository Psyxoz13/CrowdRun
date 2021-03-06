using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadonlyAttribute))]
public class ReadonlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(
        SerializedProperty property,
         GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        IField readonlyField = new ReadonlyField
        {
            Rectangle = position,
            SerializedProperty = property,
            Content = label
        };
        readonlyField.Draw();
    }
}