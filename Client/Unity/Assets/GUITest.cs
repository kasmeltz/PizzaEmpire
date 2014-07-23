using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {

	protected WWW www;

	void Start()
	{
		WWWForm form = new WWWForm ();
		form.AddField ("id", "Kevin");

		www = new WWW ("http://localhost:65023/api/buildableitem");
	}

	void OnGUI () 
	{
		print (www.text);
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), www.text )) {
			Application.LoadLevel(1);
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
			Application.LoadLevel(2);
		}
	}
}
