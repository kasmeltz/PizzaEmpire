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
	
    protected FinishedWorkChecker workChecker;
    protected GUIStateManager guiStateManager;

	public static ErrorCode CurrentErrorCode = ErrorCode.ERROR_OK;

    protected int itemsLoaded = 0;
    protected int itemsToLoad = 0;
    protected object itemsLoadedLock = new object();

    public bool IsLoaded { get; set; }
    
    private bool stylesInitialized = false;
    
	public void SetGlobalError(ServerCommunication com)
	{
		CurrentErrorCode = com.Error;
		GUIItem errorWindow = guiStateManager.GetItem(GUIElementEnum.ErrorWindow);
		errorWindow.State.Visible = true;
		errorWindow.Text = com.ErrorMessage;
		
		Debug.Log(DateTime.Now + "-------------------------------------------");
		Debug.Log(DateTime.Now + "Error!");
		Debug.Log(DateTime.Now + "" + errorWindow.ToString());
		Debug.Log(DateTime.Now + "" + errorWindow.State.Visible);
		Debug.Log(DateTime.Now + errorWindow.Text);
		Debug.Log(DateTime.Now + "-------------------------------------------");
	}

    private void Start()
    {
        IsLoaded = false;
        
		guiStateManager = new GUIStateManager();  

		ServerCommunicator.Instance.Communicate(ServerActionEnum.GetBuildableItems,
	        (ServerCommunication com) => 
	        {
				ItemManager.Instance.Initialize(com.Request.text);				
				GamePlayerLogic.Instance.BuildableItems = ItemManager.Instance.BuildableItems;
                AllItemsLoaded();
			}, SetGlobalError);
        itemsToLoad++;
		
		ServerCommunicator.Instance.Communicate(ServerActionEnum.GetExperienceLevels,
	        (ServerCommunication com) => 
	        {
				ExperienceLevelManager.Instance.Initialize(com.Request.text);
				GamePlayerLogic.Instance.ExperienceLevels = ExperienceLevelManager.Instance.ExperienceLevels;
                AllItemsLoaded();
			}, SetGlobalError);
        itemsToLoad++;
			
		ServerCommunicator.Instance.Communicate(ServerActionEnum.GetPlayer,
			(ServerCommunication com) => 
			{
				GamePlayerAPI playerAPI = ServerCommunicator.Instance.ParseResponse<GamePlayerAPI>(com);
				GamePlayerAPIMorph morph = new GamePlayerAPIMorph();
				player = (GamePlayer)morph.ToBusinessFormat(playerAPI);
                AllItemsLoaded();
			}, SetGlobalError);
        itemsToLoad++;

        // this is for the style
        // that can only be created in the OnGUi method
        itemsToLoad++;

        TextAsset resourcesList = Resources.Load<TextAsset>("Text/textureResources");
        ResourceManager<Texture2D>.Instance.Initialize(resourcesList.text);
        Resources.UnloadAsset(resourcesList);

        resourcesList = Resources.Load<TextAsset>("Text/audioClipResources");
        ResourceManager<AudioClip>.Instance.Initialize(resourcesList.text);
        Resources.UnloadAsset(resourcesList);
        
		resourcesList = Resources.Load<TextAsset>("Text/fontResources");
		ResourceManager<Font>.Instance.Initialize(resourcesList.text);
		Resources.UnloadAsset(resourcesList);

		TutorialManager.Instance.Initialize();
	}

    protected void AllItemsLoaded()
    {
        lock (itemsLoadedLock)
        {
            itemsLoaded++;
        }

        if (itemsLoaded != itemsToLoad)
        {
            return;
        }

        IsLoaded = true;

        TutorialManager.Instance.Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
          
		workChecker = new FinishedWorkChecker(player, 2, SetGlobalError);

		GUIState state;
		
		/*
        GUIItem bigTogglePanel = new GUIItem(250, 200, 400, 200);
        GUIState state = new GUIState();
        state.StateCheck = new GamePlayerStateCheck { RequiredLevel = 2 };
        bigTogglePanel.State = state;
        bigTogglePanel.Element = GUIElementEnum.OrderIngredientsWindow;
        bigTogglePanel.Style = GUIGameObject.CurrentStyle;
        bigTogglePanel.Render = (gi) =>
        {
            GUI.Box(gi.Rectangle, "", gi.Style);
        };

        guiStateManager.AddItem(bigTogglePanel);

		GUIItem togglePanel = new GUIItem(250, 200, 400, 200);
        togglePanel.Element = GUIElementEnum.OrderIngredientsWindow;
        togglePanel.Style = GUIGameObject.CurrentStyle;
        togglePanel.Render = (gi) =>
        {
            GUI.Box(gi.Rectangle, "", gi.Style);
        };

		guiStateManager.AddItem(togglePanel);

        GUIItem innerThing = new GUIItem(50, 50, 35, 35);
        innerThing.Texture = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_CHECKMARK);
        innerThing.Element = GUIElementEnum.IconTomato;
        innerThing.Style = GUIGameObject.CurrentStyle;
        innerThing.Render = (gi) =>
        {
            if (GUI.Button(gi.Rectangle, gi.Texture, gi.Style))
            {
               
            }
        };

        togglePanel.AddChild(innerThing);

        GUIItem innerThing2 = new GUIItem(150, 50, 35, 35);
        innerThing2.Texture = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_CHECKMARK);
        innerThing2.Draggable = true;
        innerThing2.Element = GUIElementEnum.IconFlour;
        innerThing2.Style = GUIGameObject.CurrentStyle;
        innerThing2.Render = (gi) =>
        {
            if (GUI.Button(gi.Rectangle, gi.Texture, gi.Style))
            {

            }
        };

        togglePanel.AddChild(innerThing2);

        GUIItem toggle2Button = new GUIItem(400, 400, 50, 50);
        toggle2Button.Texture = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_CHECKMARK);
        toggle2Button.Element = GUIElementEnum.IconTomato;
        toggle2Button.Style = CurrentStyle;
        toggle2Button.Render = (gi) =>
        {
            if (GUI.Button(gi.Rectangle, gi.Texture, gi.Style))
            {
                togglePanel.State.Visible = !togglePanel.State.Visible;
            }
        };
                       
        guiStateManager.AddItem(toggle2Button);
        */
        
		GUIItem orderWheat = new GUIItem(Screen.width - 65, 400, 45, 45);
		orderWheat.Texture = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_TOMATO_LARGE);
		orderWheat.Element = GUIElementEnum.IconFlour;
		orderWheat.Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
		state = new GUIState();
		state.StateCheck = new GamePlayerStateCheck { CanBuildItem = BuildableItemEnum.White_Flour };
		orderWheat.State = state;
		orderWheat.Render = (gi) =>
		{
			if (GUI.Button(gi.Rectangle, gi.Texture, gi.Style))
			{		
				ServerCommunicator.Instance.Communicate(
					ServerActionEnum.StartWork, (int)BuildableItemEnum.White_Flour,
					(ServerCommunication com) => 
					{
					ServerCommunicator.Instance.ParseResponse<WorkItem>(com);
					GamePlayerLogic.Instance.StartWork(player, BuildableItemEnum.White_Flour);
				}, SetGlobalError);
			}
		};
		
		guiStateManager.AddItem(orderWheat);
		
		GUIItem wipeTable = new GUIItem(Screen.width - 65, 450, 45, 45);
		wipeTable.Texture = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_CHECKMARK);
		wipeTable.Element = GUIElementEnum.IconWipeTable;
		wipeTable.Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
		wipeTable.Render = (gi) =>
		{
			if (GUI.Button(gi.Rectangle, gi.Texture, gi.Style))
			{		
				GUIEvent gevent = new GUIEvent{ Element = GUIElementEnum.TableCloth, GEvent = GUIEventEnum.Wipe };			
				if (!TutorialManager.Instance.IsFinished)
				{
					TutorialManager.Instance.TryAdvance(player,gevent);
				}
			}
		};
		
		guiStateManager.AddItem(wipeTable);			

        player.StateChanged = true;
    }                    		
	
    private void Update()
	{
		ServerCommunicator.Instance.Update();

        float xAxisValue = Input.GetAxis("Horizontal") * 0.1f;
        float yAxisValue = Input.GetAxis("Vertical") * 0.1f;
        if (Camera.current != null)
        {
			Camera.current.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0));
        }

        if (!IsLoaded)
		{
			return;
		}

        if (player.StateChanged)
        {
            TutorialManager.Instance.Update(player);
            guiStateManager.UpdateState(player);

            player.StateChanged = false;
        }
		
        float dt = Time.deltaTime;

        guiStateManager.Update(dt);
        workChecker.Update(dt);
	}

    private void OnGUI() 
	{		
		if (!stylesInitialized)
		{
			InitStyles();
		}
		
		guiStateManager.OnGUI();
			
        if (!IsLoaded)
		{
			return;
		}     
        
		if (CurrentErrorCode != ErrorCode.ERROR_OK)
		{
			// @TO DO WHAT TO DO IF AN ERROR HAS OCCURED?
			return;
		}
				
		TutorialManager.Instance.OnGUI(player);		
									
       
		GUI.TextArea(new Rect(0,0,50, 20), player.Coins.ToString());
		GUI.TextArea(new Rect(50,0,50, 20), player.Coupons.ToString());
		GUI.TextArea(new Rect(100,0,50, 20), player.Level.ToString());
		GUI.TextArea(new Rect(150,0,50, 20), player.Experience.ToString());
		GUI.TextArea(new Rect(200,0,50, 20), player.BuildableItems.Count.ToString());
		GUI.TextArea(new Rect(250,0,50, 20), player.WorkItems.Count.ToString());
	}
		
	private void InitStyles()
	{
		GUIStyle style;		
		style = new GUIStyle( GUI.skin.box );
		style.normal.background = MakeTex( 2, 2, new Color( 1f, 1f, 0.7f, 1f ) );
		style.normal.textColor = new Color(0.3f, 0.1f, 0.1f, 1);		
		style.font = ResourceManager<Font>.Instance.Load(ResourceEnum.FONT_ARVO);
		style.alignment = TextAnchor.MiddleCenter;
		style.wordWrap = true;
		
		LightweightResourceManager<GUIStyle>.Instance
			.Set(ResourceEnum.GUISTYLE_BASIC_STYLE, style);
			
		GUIItem errorWindow = new GUIItem(Screen.width * 0.1f, Screen.height * 0.2f, 
		                                  Screen.width * 0.8f, Screen.height * 0.6f);
		errorWindow.Element = GUIElementEnum.ErrorWindow;
		errorWindow.Style = style;
		errorWindow.Render = (gi) =>
		{
			GUI.Box(gi.Rectangle, gi.Text, gi.Style);
		};
		errorWindow.State.Visible = false;
		
		guiStateManager.AddItem(errorWindow);			
		
		stylesInitialized = true;

        AllItemsLoaded();
	}
		
	protected Texture2D MakeTex( int width, int height, Color col )
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

