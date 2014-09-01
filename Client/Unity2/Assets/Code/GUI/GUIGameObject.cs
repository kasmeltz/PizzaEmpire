namespace KS.PizzaEmpire.Unity
{
	using System;
	using UnityEngine;
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.GameLogic;
	using Common;
	using Common.Utility;
	using Common.APITransfer;
	
	public class GUIGameObject : MonoBehaviour 
	{	
		protected GamePlayer player;
		
	    protected FinishedWorkChecker workChecker;
	
	    protected int itemsLoaded = 0;
	    protected int itemsToLoad = 0;
	    protected object itemsLoadedLock = new object();
	
	    public bool IsLoaded { get; set; }
	    
	    private bool stylesInitialized = false;
	    
	    AsyncOperation resourceCleaner = null;
	
		public static ErrorCode CurrentErrorCode = ErrorCode.ERROR_OK;

		public static void OnItemAddedHandler(object sender, GamePlayerLogicEventArgs e)
		{
			if (e.ItemQuantity.ItemCode == BuildableItemEnum.White_Flour) 
			{
				GameObject whiteFlourPrefab = 
					ResourceManager<GameObject>.Instance.Load (ResourceEnum.PREFAB_WHITE_FLOUR);
				GameObject.Instantiate(whiteFlourPrefab);
			}
		}

		public static void SetGlobalError(ServerCommunication com)
		{
			CurrentErrorCode = com.Error;
			
			GUIItem errorWindow = GUIStateManager.Instance
				.GetChild(GUIElementEnum.ErrorWindow);			
			errorWindow.Visible = true;
			
			GUIItem errorText = errorWindow.GetChild(GUIElementEnum.ErrorWindow);
			errorText.Content.text = com.ErrorMessage;
			
			Debug.Log(DateTime.Now + "-------------------------------------------");
			Debug.Log(DateTime.Now + "Error!");
			Debug.Log(DateTime.Now + "" + errorWindow.ToString());
			Debug.Log(DateTime.Now + "" + errorWindow.Visible);
			Debug.Log(DateTime.Now + errorWindow.Content.text);
			Debug.Log(DateTime.Now + "-------------------------------------------");
		}
	
		/// <summary>
		/// Loads a resource list using a coroutine - one resource per frame.
		/// Use ResourceManager<T>.LoadingAsync to see if loading
		/// has been completed
		/// </summary>
		/// <param name="resources">The list of resources to load</param>
		/// <typeparam name="T">The type of resources to load.</typeparam>
		public void LoadResourceList<T>(List<ResourceEnum> resources) 
			where T : UnityEngine.Object
		{
			StartCoroutine(ResourceManager<T>.Instance.LoadList(resources));		
		}
		
	    private void Start()
		{		
			IsLoaded = false;
			
			JsonHelper.Instance.Initialize(null);
	        
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
					GamePlayerManager.Instance.LoggedInPlayer = player;
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

			resourcesList = Resources.Load<TextAsset>("Text/prefabResources");
			ResourceManager<GameObject>.Instance.Initialize(resourcesList.text);
			Resources.UnloadAsset(resourcesList);		
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

			GamePlayerLogic.Instance.ItemAdded += OnItemAddedHandler;
		
			BuildGUI ();
				
			TutorialManager.Instance.Initialize(player);
	          
			workChecker = new FinishedWorkChecker(player, 2);	
	
	        player.StateChanged = true;
	    }                    		
	
		protected void BuildGUI()
		{
			RestaurantCommonElements.Load(player);		
			OrderIngredientsWindow.Load(player);
							
			GUIStateManager.Instance.UpdateState(player);
		}
	
	    private void Update()
		{
			float dt = Time.deltaTime;

			ServerCommunicator.Instance.Update();
	
	        float xAxisValue = Input.GetAxis("Horizontal") * 0.1f;
	        float yAxisValue = Input.GetAxis("Vertical") * 0.1f;
	        if (Camera.main != null)
	        {
				Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0));
	        }

			Camera.main.orthographicSize -= Input.GetAxis ("Mouse ScrollWheel") * 3;
	
	        if (!IsLoaded)
			{
				return;
			}
	
			if (GUIStateManager.Instance.ChildrenModified)
			{
				GUIStateManager.Instance.UpdateState(player);
			}
			
	        if (player.StateChanged)
	        {
	            TutorialManager.Instance.UpdateState();
	            
	            if (!GUIStateManager.Instance.ChildrenModified) 
	            {
					GUIStateManager.Instance.UpdateState(player);	
				}
	        }

			player.StateChanged = false;		
			GUIStateManager.Instance.ChildrenModified = false;		
				        
			GUIStateManager.Instance.Update(dt);
	        workChecker.Update(dt);
	        
			if (resourceCleaner == null || resourceCleaner.isDone)
			{	
				resourceCleaner = Resources.UnloadUnusedAssets();		
			}
		}
	
	    private void OnGUI() 
		{				
			if (!stylesInitialized)
			{
				InitStyles();
			}
			
			float dt = Time.deltaTime;
			
			GUIStateManager.Instance.Draw(dt);
				
	        if (!IsLoaded)
			{
				return;
			}     
	        
			if (CurrentErrorCode != ErrorCode.ERROR_OK)
			{
				// @TO DO WHAT TO DO IF AN ERROR HAS OCCURED?
				return;
			}
					
			//TutorialManager.Instance.OnGUI();		
									/*										       	      
			GUI.TextArea(new Rect(100,0,50, 20), player.Level.ToString());
			GUI.TextArea(new Rect(150,0,50, 20), player.Experience.ToString());
			GUI.TextArea(new Rect(200,0,50, 20), player.BuildableItems.Count.ToString());
			GUI.TextArea(new Rect(250,0,50, 20), player.WorkItems.Count.ToString());
			*/
		}
			
		private void InitStyles()
		{
			GUIStyle style;		

			style = new GUIStyle( GUI.skin.box );
			style.normal.background = MakeTex( 2, 2, new Color( 0.686f, 0.35f, 0.133f, 1f ) );
			style.normal.textColor = new Color(0, 0, 0, 1);
			style.font = ResourceManager<Font>.Instance.Load(ResourceEnum.FONT_ARVO);
			style.alignment = TextAnchor.MiddleCenter;
			style.wordWrap = true;
			
			LightweightResourceManager<GUIStyle>.Instance
				.Set(ResourceEnum.GUISTYLE_DEBUG, style);

			style = new GUIStyle( GUI.skin.box );
			style.normal.background = MakeTex( 2, 2, new Color( 0.686f, 0.85f, 0.933f, 1f ) );
			style.normal.textColor = new Color(0, 0, 0, 1);
			style.font = ResourceManager<Font>.Instance.Load(ResourceEnum.FONT_ARVO);
			style.alignment = TextAnchor.MiddleCenter;
			style.wordWrap = true;
			
			LightweightResourceManager<GUIStyle>.Instance
				.Set(ResourceEnum.GUISTYLE_BASIC_STYLE, style);
				
			style = new GUIStyle( GUI.skin.box );
			style.normal.background = MakeTex( 2, 2, new Color( 0, 0, 0, 0 ) );
			style.normal.textColor = new Color(0, 0, 0, 1);		
			style.font = ResourceManager<Font>.Instance.Load(ResourceEnum.FONT_ARVO);
			style.alignment = TextAnchor.MiddleCenter;
			style.wordWrap = true;

			LightweightResourceManager<GUIStyle>.Instance
				.Set(ResourceEnum.GUISTYLE_NO_BACKGROUND, style);
				
			GUIImage errorWindow = GUIItemFactory<GUIImage>.Instance.Pool.New();
			errorWindow.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_WIN_TUTORIAL_DIALOGUE);
			errorWindow.SetRectangle(0.1f, 0.2f, 0.8f, 0.6f, false, ScaleMode.StretchToFill);
			errorWindow.Element = GUIElementEnum.ErrorWindow;	
			errorWindow.Visible = false;
			
			GUIStateManager.Instance.AddChild(errorWindow);						

			GUIBox errorText = GUIItemFactory<GUIBox>.Instance.Pool.New();
			errorText.SetRectangle(0, 0, 0.8f, 0.6f, false, ScaleMode.StretchToFill);
			errorText.Element = GUIElementEnum.ErrorWindow;
			errorText.Style = style;		
			errorText.Visible = true;
			
			errorWindow.AddChild(errorText);								
			
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
}