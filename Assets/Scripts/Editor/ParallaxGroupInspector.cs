using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;

[CustomEditor(typeof(ParallaxGroup))]
public class ParallaxGroupInspector : Editor {

	private ParallaxGroup _target;
	private ReorderableList list;

	void OnEnable()
	{
		list = new ReorderableList(serializedObject, 
		                           serializedObject.FindProperty("Objects"), 
		                           true, true, true, true);

		list.drawElementCallback =  
		(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.LabelField(new Rect(rect.x, rect.y, 40, EditorGUIUtility.singleLineHeight),"Layer:");
			rect.x += 40;
			EditorGUI.PropertyField(new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("MainObject"), GUIContent.none);
			rect.x += 120;
			EditorGUI.LabelField(new Rect(rect.x, rect.y, 40, EditorGUIUtility.singleLineHeight),"Speed:");
			rect.x += 40;
			EditorGUI.PropertyField(new Rect(rect.x, rect.y, 40, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("ScrollSpeed"), GUIContent.none);
		};
	}

	public override void OnInspectorGUI ()
	{
		_target = target as ParallaxGroup;

		_target.TargetCamera = (Camera)EditorGUILayout.ObjectField("Target camera:",_target.TargetCamera,typeof(Camera),true);
		_target.AutoTile = EditorGUILayout.Toggle("Auto Tile:", _target.AutoTile);

		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();

	}

}
