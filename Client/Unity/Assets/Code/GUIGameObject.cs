using System;
using UnityEngine;
using System.Collections.Generic;
using KS.PizzaEmpire.Unity;
using LitJson;

public class GUIGameObject : MonoBehaviour {
	
	protected int currentMessage = 0;
	protected List<string> messages;
	protected GamePlayer player;
	
	public static Texture2D IconCheckMark { get; protected set; }
	public static Texture2D IconMoreText { get; protected set; }
	
	private WWW www;
	
	void Start()
	{
		player = new GamePlayer();
		TutorialManager.Instance.Initialize ();
	}
	
	void OnGUI () 
	{		
		InitStyles();
		TutorialManager.Instance.OnGUI(player);
		
		if (GUI.Button(new Rect(Screen.width - 65, 400, 45, 45), GUIGameObject.IconCheckMark))
		{
			if (www == null)
			{
				www = new WWW("http://localhost:65023/api/gameplayer/kevin");
			}
			GUIEvent gevent = new GUIEvent{ Element = GUIElementEnum.IconCheckMark, GEvent = GUIEventEnum.Tap };
						
			if (!TutorialManager.Instance.IsFinished)
			{
				TutorialManager.Instance.TryAdvance(player,
					new GUIEvent { Element = GUIElementEnum.IconCheckMark, GEvent = GUIEventEnum.Tap });
			}
		}
		
		if (www != null)
		{
			if (www.isDone)
			{
				print (www.text);
				
				//Result<GamePlayer> result = JsonMapper.ToObject<Result<GamePlayer>>(www.text);
				
				//print (result);
				/*string gString = JsonMapper.ToJson(player);
			
			print (gString);
			
			GamePlayer other = JsonMapper.ToObject<GamePlayer>(gString);
			
			print (other + " " + other.Coins);
			
			*/
			}
		}		
	}
	
	public void DrawButton(GUIEvent guiEvent, Action fn)
	{
		if (GUI.Button(new Rect(Screen.width - 55, 55, 45, 45), GUIGameObject.IconMoreText))
		{
			if (!TutorialManager.Instance.IsFinished)
			{
				TutorialManager.Instance.TryAdvance(player, guiEvent);
			}			
			if (fn != null)
			{
				fn();
			}
		}
	}
	
	public static GUIStyle CurrentStyle = null;
	
	private void InitStyles()
	{
		if (CurrentStyle != null)
		{
			return;
		}
		
		CurrentStyle = new GUIStyle( GUI.skin.box );
		CurrentStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 1f, 0.7f, 1f ) );
		CurrentStyle.normal.textColor = new Color(0.3f, 0.1f, 0.1f, 1);	
		CurrentStyle.font = Resources.Load("Graphics/Fonts/arvo") as Font;
		CurrentStyle.wordWrap = true;		
		
		IconCheckMark = Resources.Load("Graphics/UI/Misc/icon-checkmark") as Texture2D;
		IconMoreText = Resources.Load("Graphics/UI/Misc/icon-moretext") as Texture2D;
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
