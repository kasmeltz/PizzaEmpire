using UnityEngine;
using System.Collections.Generic;
using KS.PizzaEmpire.Unity;

public class GUIGameObject : MonoBehaviour {
	
	protected int currentMessage = 0;
	protected List<string> messages;
	protected GamePlayer player;
	
	void Start()
	{
		player = new GamePlayer();
		TutorialManager.Instance.Initialize ();
	}
	
	void OnGUI () 
	{
		GUIEvent guiEvent = new GUIEvent();
		
		TutorialManager.Instance.OnGUI(player, guiEvent);
	}
	
	/*		
		private GUIStyle currentStyle = null;
		
		private void InitStyles()
		{
			if( currentStyle == null )
			{
				//print ("HELLO");
				currentStyle = new GUIStyle( GUI.skin.box );
				currentStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 1f, 0.7f, 1f ) );
				currentStyle.normal.textColor = new Color(0.3f, 0.1f, 0.1f, 1);	
				currentStyle.font = (Font)Resources.Load("Graphics/Fonts/arvo", typeof(Font));
				print (currentStyle.font);
				//currentStyle.fontSize = 48;
			}
		}
		
		private Texture2D MakeTex( int width, int height, Color col )
		{
			Color[] pix = new Color[width * height];
			for( int i = 0; i < pix.Length; ++i )
			{
				pix[ i ] = col;
			}
			Texture2D result = new Texture2D( width, height );
			result.SetPixels( pix );
			result.Apply();
			return result;
		}
		*/
}
