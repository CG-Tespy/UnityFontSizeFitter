using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.UI;
using CGTespy.UI;

/// <summary>
/// Author: CG-Tespy
/// </summary>

[CustomEditor(typeof(FontSizeFitter))]
public class FontSizeFitterEditor : Editor
{
	FontSizeFitter fitter;
	Text textField;
	
	// Keeps track of state.
	TextCharacterState prevTextState;
	RectTransformStretchState prevStretchState;
	int prevLinesToFit = 					0;
	bool prevAutoResize = 					false;
	bool autoResize;
	

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		fitter = 							target as FontSizeFitter;
		textField = 						fitter.GetComponent<Text>();

		// Add a flag field letting users have the font resize happen when certain thins change.
		autoResize = 						EditorGUILayout.Toggle("Auto Fit", prevAutoResize);
		prevAutoResize = 					autoResize;
		HandleAutoResizing();
		CreateFontResizeButton();
	}

	void CreateFontResizeButton()
	{
		// The button should be about half the width of the inspector window.
		float windowWidth = 		EditorGUIUtility.currentViewWidth;
		float width = 				windowWidth / 2;
		float space = 				(windowWidth / 2) - (width / 2);
		
		EditorGUILayout.BeginHorizontal();
		GUILayoutOption widthOp = 	GUILayout.Width(width);
		GUILayout.Space(space);
		
		if (GUILayout.Button("Fit Font Size", widthOp))
			TriggerFontResize();

		EditorGUILayout.EndHorizontal();
	}

	void TriggerFontResize()
	{
		fitter.FitFontSize(fitter.linesToFit);
	}

	void HandleAutoResizing()
	{
		if (autoResize)
		{
			// Resize when some important value has changed.
			TextCharacterState textFieldState = 		new TextCharacterState(textField);
			RectTransformStretchState stretchState = 	new RectTransformStretchState(fitter.rectTransform);

			if (TextFieldChanged(textFieldState) || FitterChanged() || StretchChanged(stretchState))
			{
				TriggerFontResize();
				Debug.Log("Auto-resized font for " + fitter.name);

				// Update the relevant state vars for the next time this func is called.
				prevTextState = 						textFieldState;
				prevStretchState = 						stretchState;
				prevLinesToFit = 						fitter.linesToFit;
			}

		}

	}

	// Checks for state changes.

	/// <summary>
	/// Specfically, whether the relevant parts of the text field's character settings have changed.
	/// </summary>
	bool TextFieldChanged(TextCharacterState newTextState)
	{
		if (prevTextState == null)
		{
			prevTextState = 	newTextState;
			return false;
		}

		bool diffFont = 			newTextState.font != prevTextState.font;
		bool diffFontStyle = 		newTextState.fontStyle != prevTextState.fontStyle;
		bool diffLineSpacing = 		newTextState.lineSpacing != prevTextState.lineSpacing;	

		return diffFont || diffFontStyle || diffLineSpacing;
	}

	bool FitterChanged()
	{
		bool diffLinesToFit = 		prevLinesToFit != fitter.linesToFit;

		return diffLinesToFit;
	}

	/// <summary>
	/// Specifically, the relevant parts of the rect transform's stretch settings.
	/// </summary>
	/// <returns></returns>
	bool StretchChanged(RectTransformStretchState newStretchState)
	{
		if (prevStretchState == null)
		{
			prevStretchState = 		newStretchState;
			return false;
		}

		return !prevStretchState.Equals(newStretchState);
	}
}
