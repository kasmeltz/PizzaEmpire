using System;
using UnityEngine;
using System.Collections.Generic;
using KS.PizzaEmpire.Unity;
using KS.PizzaEmpire.Common.BusinessObjects;
using KS.PizzaEmpire.Common.GameLogic;
using KS.PizzaEmpire.Common;
using KS.PizzaEmpire.Common.APITransfer;
using LitJson;

public class GUIGameObject : MonoBehaviour {
	
	protected int currentMessage = 0;
	protected List<string> messages;
	protected GamePlayer player;
	protected GamePlayerStateCheck stateCheck = new GamePlayerStateCheck();
	
	public static Texture2D IconCheckMark { get; protected set; }
	public static Texture2D IconMoreText { get; protected set; }
	
	public bool IsError = false;
	public string ErrorMessage = "";
	
	float checkWorkDone = 0;
	
	protected void OnServerError(ServerCommunication com)
	{
		IsError = true;
		ErrorMessage = com.ErrorMessage;
	}
	
	void Start()
	{
		ServerCommunicator.Instance.Communicate(ServerActionEnum.GetBuildableItems,
	        (ServerCommunication com) => 
	        {
				ItemManager.Instance.Initialize(com.Request.text);				
				Debug.Log(ItemManager.Instance.BuildableItems.Count);
				GamePlayerLogic.Instance.BuildableItems = ItemManager.Instance.BuildableItems;
			}, OnServerError);
		
		ServerCommunicator.Instance.Communicate(ServerActionEnum.GetExperienceLevels,
	        (ServerCommunication com) => 
	        {
				ExperienceLevelManager.Instance.Initialize(com.Request.text);
				Debug.Log(ExperienceLevelManager.Instance.ExperienceLevels.Count);
				GamePlayerLogic.Instance.ExperienceLevels = ExperienceLevelManager.Instance.ExperienceLevels;
			}, OnServerError);
			
		ServerCommunicator.Instance.Communicate(ServerActionEnum.GetPlayer,
			(ServerCommunication com) => 
			{
				GamePlayerAPI playerAPI = ServerCommunicator.Instance.ParseResponse<GamePlayerAPI>(com);
				GamePlayerAPIMorph morph = new GamePlayerAPIMorph();
				player = (GamePlayer)morph.ToBusinessFormat(playerAPI);				
			}, OnServerError);
			
		TutorialManager.Instance.Initialize();
	}
	
	public bool IsReady()
	{
		if (player == null || 
			GamePlayerLogic.Instance.BuildableItems == null ||
			GamePlayerLogic.Instance.ExperienceLevels == null )
		{
			return false;
		}			
		
		return true;
	}
	
	public void DrawError()
	{
		GUI.Box (new Rect(50, 150, Screen.width - 100, Screen.height - 300), 
	        ErrorMessage, GUIGameObject.CurrentStyle);
	}	
	
	void CheckIfWorkDone()
	{
		bool contactServer = false;
		
		foreach(WorkItem workItem in player.WorkItems)
		{
			if (workItem.FinishTime <= DateTime.UtcNow)
			{
				contactServer = true;
				break;
			}
		}
		
		if (contactServer)
		{
			ServerCommunicator.Instance.Communicate(
				ServerActionEnum.FinishWork,
				(ServerCommunication com) => 
				{
					DateTime checkTime = ServerCommunicator.Instance.ParseResponse<DateTime>(com);
					Debug.Log(checkTime);
					GamePlayerLogic.Instance.FinishWork(player, checkTime);
				}, OnServerError);
		}
	}
	
	void Update()
	{
		ServerCommunicator.Instance.Update();
		
		if (!IsReady())
		{
			return;
		}			
				
		checkWorkDone += Time.deltaTime;
		if (checkWorkDone > 1)
		{
			checkWorkDone = 0;
			CheckIfWorkDone();
		}
	}
	
	void OnGUI () 
	{		
		InitStyles();
		
		if (IsError)
		{
			DrawError();
			return;
		}
		
		if (!IsReady())
		{
			return;
		}	
		
		TutorialManager.Instance.OnGUI(player);		
							
		if (player.StateChanged)
		{
			TutorialManager.Instance.Update(player);				
			
			player.StateChanged = false;
		}
		
		ErrorCode ec = GamePlayerLogic.Instance.CanBuildItem(player, BuildableItemEnum.White_Flour);
		if (ec == ErrorCode.ERROR_OK)
		{
			if (GUI.Button(new Rect(Screen.width - 65, 400, 45, 45), GUIGameObject.IconCheckMark))
			{		
				ServerCommunicator.Instance.Communicate(
					ServerActionEnum.StartWork, (int)BuildableItemEnum.White_Flour,
					(ServerCommunication com) => 
		        	{
						ServerCommunicator.Instance.ParseResponse<WorkItem>(com);
						GamePlayerLogic.Instance.StartWork(player, BuildableItemEnum.White_Flour);
					}, OnServerError);
			}
		}		
		
		GUI.TextArea(new Rect(0,0,50, 20), player.Coins.ToString());
		GUI.TextArea(new Rect(50,0,50, 20), player.Coupons.ToString());
		GUI.TextArea(new Rect(100,0,50, 20), player.Level.ToString());
		GUI.TextArea(new Rect(150,0,50, 20), player.Experience.ToString());
		GUI.TextArea(new Rect(200,0,50, 20), player.BuildableItems.Count.ToString());
		GUI.TextArea(new Rect(250,0,50, 20), player.WorkItems.Count.ToString());
		
		if (GUI.Button(new Rect(Screen.width - 65, 450, 45, 45), GUIGameObject.IconCheckMark))
		{		
			GUIEvent gevent = new GUIEvent{ Element = GUIElementEnum.TableCloth, GEvent = GUIEventEnum.Wipe };
						
			if (!TutorialManager.Instance.IsFinished)
			{
				TutorialManager.Instance.TryAdvance(player,gevent);
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
