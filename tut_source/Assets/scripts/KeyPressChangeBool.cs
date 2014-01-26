using UnityEngine;
using System.Collections;
using System.Reflection;

/// <summary>
/// Uses C# reflection to change a named bool variable according to a keyboard press.
/// </summary>
public class KeyPressChangeBool : MonoBehaviour {

    public KeyCode keypressToTrigger = KeyCode.U;
    public string componentToAlter = "Light";
    public string variableToAlter = "enabled";
    public enum HowToChange
    {
        setFalse, setTrue, toggle
    }
    public HowToChange howToChange;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown (keypressToTrigger)) {
            Component c = GetComponent(componentToAlter);
			SetBoolean(c, variableToAlter, howToChange);
        }
	}
	public static void SetBoolean(Component c, string name, HowToChange howToChange)
	{
		System.Type t = c.GetType();
		System.Reflection.PropertyInfo prop = t.GetProperty(name);
		object value = null;
		switch(howToChange){
		case HowToChange.setFalse:
			value = (object)false;
			break;
		case HowToChange.setTrue:
			value = (object)true;
			break;
		case HowToChange.toggle:
			bool currentValue = (bool)prop.GetValue(c, null);
			value = (object)!currentValue;
			break;
		}
		prop.SetValue(c, value, null);
	}
}
