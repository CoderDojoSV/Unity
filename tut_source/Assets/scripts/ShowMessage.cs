using UnityEngine;
using System.Collections;

/// <summary>
/// used to show a message using the standard GUI system, 
/// just a little bit below the middle of the screen.
/// </summary>
public class ShowMessage : MonoBehaviour {
	public string message = "You Win!";
	public int messageFontSize = 30;
	public bool showMessage = false;

	void OnGUI()
	{
		if (message != null && showMessage) {
			const float messageVOffset = 64;
			Rect screenSize = new Rect(
				0, messageVOffset, Screen.width, Screen.height-messageVOffset);
			TextAnchor oldAnchor = GUI.skin.label.alignment;
			int oldSize = GUI.skin.label.fontSize;
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.skin.label.fontSize = messageFontSize;
			GUI.Label(screenSize, message);
			GUI.skin.label.alignment = oldAnchor;
			GUI.skin.label.fontSize = oldSize;
		}
	}
}
