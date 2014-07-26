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
	
	public bool IsError = false;
	public string ErrorMessage = "";
	
	protected void OnServerError(ServerCommunication com)
	{
		IsError = true;
		ErrorMessage = com.ErrorMessage;
	}
	
	void Start()
	{
		TutorialManager.Instance.Initialize ();
		
		ServerCommunicator.Instance.Communicate(ServerActionEnum.GetPlayer,
			(ServerCommunication com) => 
			{
				player = ServerCommunicator.Instance.ParseResponse<GamePlayer>(com);			
			}, OnServerError);
	}
	
	public void DrawError()
	{
		GUI.Box (new Rect(50, 150, Screen.width - 100, Screen.height - 300), 
	        ErrorMessage, GUIGameObject.CurrentStyle);
	}	
	
	void Update()
	{
		ServerCommunicator.Instance.Update();
	}
	
	void OnGUI () 
	{		
		InitStyles();
		
		if (IsError)
		{
			DrawError();
			return;
		}
						
		if (player == null)
		{
			return;
		}
		
		TutorialManager.Instance.OnGUI(player);
		
		if (GUI.Button(new Rect(Screen.width - 65, 400, 45, 45), GUIGameObject.IconCheckMark))
		{		
			ServerCommunicator.Instance.Communicate(
				ServerActionEnum.StartWork, (int)BuildableItemEnum.White_Flour,
				(ServerCommunication com) => 
	        	{
					WorkItem workStarted = ServerCommunicator.Instance.ParseResponse<WorkItem>(com);
					player.WorkItems.Add(workStarted);						
				}, OnServerError);
		}		
		
		GUI.TextArea(new Rect(0,0,50, 20), player.Coins.ToString());
		GUI.TextArea(new Rect(50,0,50, 20), player.Coupons.ToString());
		GUI.TextArea(new Rect(100,0,50, 20), player.Level.ToString());
		GUI.TextArea(new Rect(150,0,50, 20), player.Experience.ToString());
		GUI.TextArea(new Rect(200,0,50, 20), player.BuildableItems.Count.ToString());
		GUI.TextArea(new Rect(250,0,50, 20), player.WorkItems.Count.ToString());
		
		
		/*
			GUIEvent gevent = new GUIEvent{ Element = GUIElementEnum.IconCheckMark, GEvent = GUIEventEnum.Tap };
						
			if (!TutorialManager.Instance.IsFinished)
			{
				TutorialManager.Instance.TryAdvance(player,
					new GUIEvent { Element = GUIElementEnum.IconCheckMark, GEvent = GUIEventEnum.Tap });
			}
			*/			
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
		CurrentStyle.alignment = TextAnchor.MiddleCenter;
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
