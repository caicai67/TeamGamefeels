#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;


namespace InControl.Internal
{
	internal static class EditorUtility
	{
		internal static GUIStyle titleStyle;
		internal static GUIStyle groupStyle;
		internal static GUIStyle labelStyle;
		internal static GUIStyle wellStyle;


		static EditorUtility()
		{
			titleStyle = new GUIStyle();
			titleStyle.border = new RectOffset( 2, 2, 2, 1 );
			titleStyle.margin = new RectOffset( 5, 5, 5, 0 );
			titleStyle.padding = new RectOffset( 5, 5, 5, 5 );
			titleStyle.alignment = TextAnchor.MiddleLeft;
			titleStyle.normal.background = EditorGUIUtility.isProSkin ? Internal.EditorTextures.InspectorTitle_Pro : Internal.EditorTextures.InspectorTitle;

			groupStyle = new GUIStyle();
			groupStyle.border = new RectOffset( 2, 2, 1, 2 );
			groupStyle.margin = new RectOffset( 5, 5, 5, 5 );
			groupStyle.padding = new RectOffset( 10, 10, 10, 10 );
			groupStyle.normal.background = EditorGUIUtility.isProSkin ? Internal.EditorTextures.InspectorGroup_Pro : Internal.EditorTextures.InspectorGroup;

			labelStyle = new GUIStyle();
			labelStyle.richText = true;
			labelStyle.padding.top = 1;
			labelStyle.padding.left = 5;

			wellStyle = new GUIStyle();
			wellStyle.alignment = TextAnchor.UpperLeft;
			wellStyle.border = new RectOffset( 2, 2, 2, 2 );
			wellStyle.margin = new RectOffset( 5, 5, 5, 5 );
			wellStyle.padding = new RectOffset( 10, 10, 5, 7 );
			wellStyle.wordWrap = true;
			wellStyle.normal.background = EditorGUIUtility.isProSkin ? Internal.EditorTextures.InspectorWell_Pro : Internal.EditorTextures.InspectorWell;
			wellStyle.richText = true;
		}


		internal static void GroupTitle( string title, SerializedProperty boolProperty )
		{
			GUILayout.Space( 4.0f );
			GUILayout.BeginVertical( "", titleStyle );
			boolProperty.boolValue = EditorGUILayout.ToggleLeft( "<b>" + title + "</b>", boolProperty.boolValue, labelStyle );
			GUILayout.EndVertical();
		}


		internal static void BeginGroup()
		{
			GUILayout.Space( -6.0f );
			GUILayout.BeginVertical( "", groupStyle );
		}


		internal static void BeginGroup( string title )
		{
			GUILayout.Space( 4.0f );
			GUILayout.BeginVertical( "", titleStyle );
			EditorGUILayout.LabelField( "<b>" + title + "</b>", labelStyle );
			GUILayout.EndVertical();

			BeginGroup();
		}


		internal static void EndGroup()
		{
			GUILayout.EndVertical();
		}


		//		public static T LoadAssetAtPath<T>( string assetPath )
		//			where T: UnityEngine.Object
		//		{
		//			#if UNITY_5 && !(UNITY_5_0_0 || UNITY_5_0_1)
		//			return AssetDatabase.LoadAssetAtPath<T>( assetPath );
		//			#else
		//			return Resources.LoadAssetAtPath<T>( assetPath );
		//			#endif
		//		}
	}
}
#endif