using UnityEngine;
using System.Collections;

public class TitleSceneScript : MonoBehaviour {
	// Update is called once per frame
	void Update() 
	{
		if ((Input.touchCount == 1) &&
		    (Input.GetTouch(0).phase == TouchPhase.Began) )
		{
			Application.LoadLevel("RestaurantScene");
		}
			
		#if UNITY_STANDALONE_WIN
		
		if (Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel("RestaurantScene");
		}
		
		#endif
	}
}
