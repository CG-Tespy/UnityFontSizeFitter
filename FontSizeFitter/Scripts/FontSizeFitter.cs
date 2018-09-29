using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Author: CG-Tespy
/// </summary>

namespace CGTespy.UI
{
	public class FontSizeFitter : MonoBehaviour 
	{
		// UI elements.
		[SerializeField] Text textField; 					// To apply new font size to.
		Text label; 										// Helps decide the new font size.
		Canvas fitterCanvas; 								// To avoid messing with canvases other modules set up.
		public RectTransform rectTransform 						{ get { return textField.rectTransform; } }

		[Range(1, 999)]
		[SerializeField] protected int _linesToFit = 		3;

		public int linesToFit
		{
			get { return _linesToFit; }
			set 
			{
				// Make sure the amount asked to size for is reasonable.
				if (value <= 0)
				{
					string format = 					"{0}'s FontSizeFitter cannot fit a size for {1} lines.";
					string errorMessage = 				string.Format(format, this.name, value);
					throw new System.ArgumentException(errorMessage);
				}

				_linesToFit = 							value;
			}
		}

		/// <summary>
		/// Adjusts the text field's font size based on how many lines you want it to fit, and 
		/// returns the chosen size.
		/// </summary>
		public int FitFontSize(int linesToFit)
		{
			this.linesToFit = 								linesToFit;
			CheckBaseComponents();
			PrepareUIElements();
			CalculateFontSize();
			Cleanup();

			return textField.fontSize;
		}

		void CalculateFontSize()
		{
			// Fill the label with as many lines of text as requested...
			label.text = 							"y";

			for (int i = 0; i < linesToFit - 1; i++)
				label.text += 						"\ny";	

			// ... Refresh the text generator, and let the built-in text-resizing 
			// functionality handle the rest. 
			// Credit to LocalDude from Unity Answers for this part of the algorithm.
			TextGenerator textGen = 				label.cachedTextGenerator;
			Vector2 labelSize = 					label.rectTransform.rect.size;
			TextGenerationSettings genSettings = 	label.GetGenerationSettings(labelSize);
			genSettings.scaleFactor = 				1;
			textGen.Populate(label.text, genSettings);

			int sizeChosen = 						textGen.fontSizeUsedForBestFit;
			textField.fontSize = 					sizeChosen;
		}

		#region Helpers

		void CheckBaseComponents()
		{
			// Make sure we have a text field to work with.
			if (textField == null)
			{
				string format = 					"{0}'s FontSizeFitter cannot work without {0} having a Unity UI Text field.";
				string errorMessage = 				string.Format(format, this.name);
				throw new System.MissingFieldException(errorMessage);
			}
		}

		void PrepareUIElements()
		{
			// The canvas...
			fitterCanvas = 						new GameObject().AddComponent<Canvas>();
			fitterCanvas.name = 				"FitterCanvas";

			// ... and the label.
			GameObject labelObj = 				new GameObject();
			labelObj.transform.SetParent(fitterCanvas.transform);
			label = 							labelObj.AddComponent<Text>();
			Vector2 textFieldSize = 			textField.rectTransform.rect.size;
			label.rectTransform.sizeDelta = 	textFieldSize;

			SyncLabelToTextField();

			// Try not to limit the possible chosen sizes too much.
			label.resizeTextMinSize = 			1;
			label.resizeTextMaxSize = 			int.MaxValue - 100;
			// ^ For some reason, glitches out when it's too close to int.MaxValue exactly.
			label.resizeTextForBestFit = 		true;
		}

		void SyncLabelToTextField()
		{
			// Need to make sure the right fields are shared, for accuracy.
			label.font = 						textField.font;
			label.fontStyle = 					textField.fontStyle;
			label.fontSize = 					textField.fontSize;
			label.lineSpacing = 				textField.lineSpacing;
			label.alignment = 					textField.alignment;
			label.color = 						textField.color;
		}

		void Cleanup()
		{
			DestroyImmediate(fitterCanvas.gameObject); // Should indirectly destroy the label, too.
		}

		#endregion

	}

}