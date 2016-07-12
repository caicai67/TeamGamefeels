#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;


namespace InControl
{
	using Internal;
	using ReorderableList;


	[CustomEditor( typeof(InControlManager) )]
	public class InControlManagerEditor : Editor
	{
		SerializedProperty logDebugInfo;
		SerializedProperty invertYAxis;
		SerializedProperty useFixedUpdate;
		SerializedProperty dontDestroyOnLoad;
		SerializedProperty suspendInBackground;

		SerializedProperty customProfiles;

		SerializedProperty enableICade;

		SerializedProperty enableXInput;
		SerializedProperty xInputOverrideUpdateRate;
		SerializedProperty xInputUpdateRate;
		SerializedProperty xInputOverrideBufferSize;
		SerializedProperty xInputBufferSize;

		SerializedProperty enableNativeInput;
		SerializedProperty nativeInputEnableXInput;
		//		SerializedProperty nativeInputPreventSleep;
		SerializedProperty nativeInputOverrideUpdateRate;
		SerializedProperty nativeInputUpdateRate;

		Texture headerTexture;


		void OnEnable()
		{
			logDebugInfo = serializedObject.FindProperty( "logDebugInfo" );
			invertYAxis = serializedObject.FindProperty( "invertYAxis" );
			useFixedUpdate = serializedObject.FindProperty( "useFixedUpdate" );
			dontDestroyOnLoad = serializedObject.FindProperty( "dontDestroyOnLoad" );
			suspendInBackground = serializedObject.FindProperty( "suspendInBackground" );

			customProfiles = serializedObject.FindProperty( "customProfiles" );

			enableICade = serializedObject.FindProperty( "enableICade" );

			enableXInput = serializedObject.FindProperty( "enableXInput" );
			xInputOverrideUpdateRate = serializedObject.FindProperty( "xInputOverrideUpdateRate" );
			xInputUpdateRate = serializedObject.FindProperty( "xInputUpdateRate" );
			xInputOverrideBufferSize = serializedObject.FindProperty( "xInputOverrideBufferSize" );
			xInputBufferSize = serializedObject.FindProperty( "xInputBufferSize" );

			enableNativeInput = serializedObject.FindProperty( "enableNativeInput" );
			nativeInputEnableXInput = serializedObject.FindProperty( "nativeInputEnableXInput" );
//			nativeInputPreventSleep = serializedObject.FindProperty( "nativeInputPreventSleep" );
			nativeInputOverrideUpdateRate = serializedObject.FindProperty( "nativeInputOverrideUpdateRate" );
			nativeInputUpdateRate = serializedObject.FindProperty( "nativeInputUpdateRate" );

			headerTexture = Internal.EditorTextures.InControlHeader;
		}


		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			GUILayout.Space( 5.0f );

			var headerRect = GUILayoutUtility.GetRect( 0.0f, 5.0f );
			headerRect.width = headerTexture.width / 2;
			headerRect.height = headerTexture.height / 2;
			GUILayout.Space( headerRect.height );
			GUI.DrawTexture( headerRect, headerTexture );

			var versionTyle = new GUIStyle();
			versionTyle.alignment = TextAnchor.MiddleCenter;
			versionTyle.border = new RectOffset( 2, 2, 2, 2 );
			versionTyle.margin = new RectOffset( 5, 5, 5, 5 );
			versionTyle.padding = new RectOffset( 10, 10, 5, 7 );
			versionTyle.wordWrap = true;
			versionTyle.normal.background = EditorGUIUtility.isProSkin ? Internal.EditorTextures.InspectorWell_Pro : Internal.EditorTextures.InspectorWell;
			versionTyle.richText = true;
			GUILayout.Box( "<color=#007>Version " + InputManager.Version.ToString() + "</color>", versionTyle, GUILayout.ExpandWidth( true ) );


			EditorUtility.BeginGroup( "General Settings" );

			logDebugInfo.boolValue = EditorGUILayout.ToggleLeft( "Log Debug Info", logDebugInfo.boolValue, EditorUtility.labelStyle );
			invertYAxis.boolValue = EditorGUILayout.ToggleLeft( "Invert Y Axis", invertYAxis.boolValue, EditorUtility.labelStyle );
			useFixedUpdate.boolValue = EditorGUILayout.ToggleLeft( "Use Fixed Update", useFixedUpdate.boolValue, EditorUtility.labelStyle );
			dontDestroyOnLoad.boolValue = EditorGUILayout.ToggleLeft( "Don't Destroy On Load", dontDestroyOnLoad.boolValue, EditorUtility.labelStyle );
			suspendInBackground.boolValue = EditorGUILayout.ToggleLeft( "Suspend In Background", suspendInBackground.boolValue, EditorUtility.labelStyle );

			EditorUtility.EndGroup();


			EditorUtility.GroupTitle( "Enable ICade (iOS only)", enableICade );


			EditorUtility.GroupTitle( "Enable XInput (Windows only)", enableXInput );
			if (enableXInput.boolValue)
			{
				EditorUtility.BeginGroup();

//				var text = "" +
//				           "<b>Warning: <color=#cc0000>Advanced Settings</color></b>\n" +
//				           "Do not modify these unless you perfectly understand what effect they will have. " +
//				           "Set to zero to automatically use sensible defaults.";
//				GUILayout.Box( text, EditorUtility.wellStyle, GUILayout.ExpandWidth( true ) );

				xInputOverrideUpdateRate.boolValue = EditorGUILayout.ToggleLeft( "Override Update Rate <color=#777>(Not Recommended)</color>", xInputOverrideUpdateRate.boolValue, EditorUtility.labelStyle );
				xInputUpdateRate.intValue = xInputOverrideUpdateRate.boolValue ? Mathf.Max( EditorGUILayout.IntField( "Update Rate (Hz)", xInputUpdateRate.intValue ), 0 ) : 0;

				xInputOverrideBufferSize.boolValue = EditorGUILayout.ToggleLeft( "Override Buffer Size <color=#777>(Not Recommended)</color>", xInputOverrideBufferSize.boolValue, EditorUtility.labelStyle );
				xInputBufferSize.intValue = xInputOverrideBufferSize.boolValue ? Mathf.Max( xInputBufferSize.intValue, EditorGUILayout.IntField( "Buffer Size", xInputBufferSize.intValue ), 0 ) : 0;

				EditorUtility.EndGroup();
			}


			EditorUtility.GroupTitle( "Enable Native Input (Mac/Windows only)", enableNativeInput );
			if (enableNativeInput.boolValue)
			{
				EditorUtility.BeginGroup();

				var text1 = "" +
				            "<b>Warning: <color=#cc0000>This feature is in BETA!</color></b>\n" +
				            "Enabling native input will disable using Unity input internally, " +
				            "but should provide more efficient and robust input support.";
				GUILayout.Box( text1, EditorUtility.wellStyle, GUILayout.ExpandWidth( true ) );

				nativeInputEnableXInput.boolValue = EditorGUILayout.ToggleLeft( "Enable XInput Support (Windows only)", nativeInputEnableXInput.boolValue, EditorUtility.labelStyle );
//				nativeInputPreventSleep.boolValue = EditorGUILayout.ToggleLeft( "Prevent Screensaver/Sleep (currently Mac only)", nativeInputPreventSleep.boolValue, EditorUtility.labelStyle );

//				var text2 = "" +
//				            "<b>Warning: <color=#cc0000>Advanced Settings</color></b>\n" +
//				            "Do not modify these unless you perfectly understand what effect they will have. " +
//				            "Set to zero to automatically use sensible defaults.";
//				GUILayout.Box( text2, EditorUtility.wellStyle, GUILayout.ExpandWidth( true ) );

				nativeInputOverrideUpdateRate.boolValue = EditorGUILayout.ToggleLeft( "Override Update Rate <color=#777>(Not Recommended)</color>", nativeInputOverrideUpdateRate.boolValue, EditorUtility.labelStyle );
				nativeInputUpdateRate.intValue = nativeInputOverrideUpdateRate.boolValue ? Mathf.Max( nativeInputUpdateRate.intValue, EditorGUILayout.IntField( "Update Rate (Hz)", nativeInputUpdateRate.intValue ), 0 ) : 0;

				EditorUtility.EndGroup();
			}

			GUILayout.Space( 5.0f );

			ReorderableListGUI.Title( "Custom Profiles <color=#c00>(DEPRECATED)</color>" );
			ReorderableListGUI.ListField( customProfiles );

			GUILayout.Space( 3.0f );
			
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif