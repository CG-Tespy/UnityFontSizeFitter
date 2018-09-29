using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using CGTespy.Utils;

/// <summary>
/// Author: CG-Tespy
/// </summary>

namespace CGTespy.UI 
{
	/// <summary>
	/// Allows you to save and apply the state of a Unity UI Text's Character fields.
	/// </summary>
	public class TextCharacterState : ISaveState<Text>
	{
		// Character Fields
		public Font font;
		public FontStyle fontStyle;
		public int fontSize;
		public float lineSpacing;
		public bool supportRichText;

		// Methods
		public TextCharacterState(Text textField)
		{
			SetFrom(textField);
		}

		public void SetFrom(Text textField)
		{
			this.font = 						textField.font;
			this.fontStyle = 					textField.fontStyle;
			this.fontSize = 					textField.fontSize;
			this.lineSpacing = 					textField.lineSpacing;
			this.supportRichText = 				textField.supportRichText;
		}

		public void ApplyTo(Text textField)
		{
			textField.font = 					this.font;
			textField.fontStyle = 				this.fontStyle;
			textField.fontSize = 				this.fontSize;
			textField.lineSpacing = 			this.lineSpacing;
			textField.supportRichText = 		this.supportRichText;
		}

		public TextCharacterState CreateFrom(Text textField)
		{
			return new TextCharacterState(textField);
		}

		
	}
}