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
		InitStyles();
		GUIEvent guiEvent = new GUIEvent();
		
		TutorialManager.Instance.OnGUI(player, guiEvent);			
	}
	
	public static GUIStyle CurrentStyle = null;
	
	private void InitStyles()
	{
		if( CurrentStyle == null )
		{
			CurrentStyle = new GUIStyle( GUI.skin.box );
			CurrentStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 1f, 0.7f, 1f ) );
			CurrentStyle.normal.textColor = new Color(0.3f, 0.1f, 0.1f, 1);	
			CurrentStyle.font = (Font)Resources.Load("Graphics/Fonts/arvo", typeof(Font));
			CurrentStyle.wordWrap = true;
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
}
