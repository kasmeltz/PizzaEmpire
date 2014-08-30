namespace KS.PizzaEmpire.Unity
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Common.BusinessObjects;
	using Common.GameLogic;
	using Common.GameLogic.GamePlayerState;

    public class TutorialManager
    {
        private static volatile TutorialManager instance;
        private static object syncRoot = new object();

        private List<TutorialStage> stages;
        private int currentStageIndex;
        private TutorialStage currentStage;
        private string[] sayings;
		        private List<Texture2D> louieTextures;
        
		private GUIImage louieDialogueWindow;
        private GUIBox louieDialogueBox;
		private GUIImage louieImage;
        private GUIButton moreTextButton;
        
        private GamePlayer player;

        public bool IsFinished { get; protected set; }

        public AudioClip louieSound;

        private TutorialManager()
        {
            TextAsset unclelouie = Resources.Load<TextAsset>("Text/unclelouie");
            sayings = unclelouie.text.Split('\n');
            stages = new List<TutorialStage>();
        }

        /// <summary>
		/// Provides the Singleton instance of the TutorialManager
        /// </summary>
        public static TutorialManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new TutorialManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Loads the assets for the tutorial manager
        /// </summary>
        private void LoadAssets()
        {
            IsFinished = false;
            
            louieTextures = new List<Texture2D>();
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_0));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_1));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_2));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_3));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_4));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_5));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_6));

            louieSound = ResourceManager<AudioClip>.Instance.Load(ResourceEnum.AUDIOCLIP_UNCLELOUIE);

			louieDialogueWindow = GUIItemFactory<GUIImage>.Instance.Pool.New();
			louieDialogueWindow.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_WIN_TUTORIAL_DIALOGUE);
			louieDialogueWindow.SetRectangle(0.32f, 0.15f, 0.67f, 0.4f, false, ScaleMode.StretchToFill);			                              			                              
			louieDialogueWindow.Element = GUIElementEnum.TutorialDialogueWindow;
			
			GUIStateManager.Instance.AddChild(louieDialogueWindow);
			
			louieDialogueBox = GUIItemFactory<GUIBox>.Instance.Pool.New();
			louieDialogueBox.SetRectangle(0.05f, 0.05f, 0.58f, 0.3f, false, ScaleMode.ScaleToFit);			                              			                              
      		louieDialogueBox.Element = GUIElementEnum.TutorialDialogueBox;
			louieDialogueBox.Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_NO_BACKGROUND);		
      		
			louieDialogueWindow.AddChild(louieDialogueBox);
			
			moreTextButton = GUIItemFactory<GUIButton>.Instance.Pool.New();
			moreTextButton.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_MORE_TEXT);
			moreTextButton.SetRectangle(0.59f, 0.24f, 0.075f, 0.075f, false, ScaleMode.ScaleToFit);			                              			                              
			moreTextButton.Element = GUIElementEnum.TutorialMoreText;
			moreTextButton.Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_NO_BACKGROUND);		
			
			moreTextButton.OnClick = (gi) =>
			{
				SetStage(currentStageIndex + 1);
			};
			
			louieDialogueWindow.AddChild(moreTextButton);

			louieImage = GUIItemFactory<GUIImage>.Instance.Pool.New();
			louieImage.Element = GUIElementEnum.TutorialLouie;
				
			GUIStateManager.Instance.AddChild(louieImage);
        }

        /// <summary>
        /// Initializes the tutorial manager instance
        /// </summary>
        public void Initialize(GamePlayer player)
        {
            LoadAssets();
		
			this.player = player;
            TutorialStage stage;
            GamePlayerStateCheck stateCheck = null;

			//Hey kiddo, it's your Uncle Louie, how's it going? I see the old man left you in charge of this place. You know he never did manage to make something of this place, but I have faith in you, kid.
            stage = new TutorialStage();
            stage.OnStart = () =>
            {
				AudioSource.PlayClipAtPoint(louieSound, Vector3.zero);
            	SetStageText(0, true);
            	ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Normal, true);
            };           
            stages.Add(stage);

			//Don't worry, I'll help show you the ropes, so you can build the biggest pizza empire in this city!
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(1, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised, true);
			};            
            stages.Add(stage);

			//The first thing we need to do is call the wholesaler and order some flour. You can't make pizza dough without flour, and the dough is the secret to the best tasting pizza!
            stage = new TutorialStage();            
			stage.OnStart = () =>
			{
				SetStageText(2, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
            stages.Add(stage);
            
			//To phone the wholesaler tap on the phone icon. Tap and drag the white flour icon into your shopping cart. Tap the checkmark when you're done.
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(3, true);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, false);
			};
            stateCheck = new GamePlayerStateCheck();
            stateCheck.Rules.Add(
            	new WorkInProgressCompareRule 
            	{
            	  	Item = new ItemQuantity
            	  	{
    	  				ItemCode = BuildableItemEnum.White_Flour,
            	  		UnStoredQuantity = 1            	  		
            	  	},
            	  	ComparisonType = ComparisonEnum.GreaterThan
            	});
            stage.PlayerStateCheck = stateCheck;
            stages.Add(stage);

			//You did good kid. You shouldn't have to wait too long to get your flour, the wholesaler is just around the corner. 
            stage = new TutorialStage();
            stage.OnStart = () =>
            {
				GUIItem window = GUIStateManager.Instance
					.GetChildNested(GUIElementEnum.OrderIngredientsWindow);				
				window.Visible = false;
				SetStageText(4, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Excited_Happy_Thumbs_Up, true);
            };
            stages.Add(stage);
            
			//To check how long it will be before your flour arrives, do something.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(5, true);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
			stage.GUIEvent = new GUIEvent(GUIElementEnum.InGameMotorcycle, GUIEventEnum.Tap);
			stages.Add(stage);

			//Excellent! You see I told you it wouldn't be long. Just let me know when it gets here I'll help you unload it.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(6, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Excited_Happy, true);
			};
			stages.Add(stage);
			
			//In the meantime it looks like we have some cleaning up to do around here. Look at that table it's filthy! No one will want to eat on a filthy table.
            stage = new TutorialStage();
            stage.OnStart = () =>
            {
                AudioSource.PlayClipAtPoint(louieSound, Vector3.zero);
				SetStageText(7, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Eye_Pop, true);
            };
            stages.Add(stage);

			//To clean a table, tap on it and then take the cloth and wipe it.
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(8, true);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Negative, false);
				GameObject dirtyTable = ResourceManager<GameObject>.Instance.Load(ResourceEnum.PREFAB_DIRTY_TABLE);			
				GameObject.Instantiate(dirtyTable, new Vector3(-5f,-0.6f,0), Quaternion.identity);						
			};
			stateCheck = new GamePlayerStateCheck ();
			stateCheck.Rules.Add(
				new StorageItemUnstoredQuantityCompareRule
				{
					 Location = 0,
					 Level = 0,
					 Item = BuildableItemEnum.Dirty_Table,
					 Quantity = 1,
					 ComparisonType = ComparisonEnum.LessThan
				});
			stage.PlayerStateCheck = stateCheck;
            stages.Add(stage);

			//Good work! Tables will get dusty over time on their own and customers will also mess them up. Customers won't sit in your restaurant to be served unless the table is clean!
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(9, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
            stages.Add(stage);

			//I hope you didn't think that that owning a pizza restaurant was going to be all fun and games? It's like my father always said "owning a pizza restaurant is not all fun and games..." 
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(10, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
            stages.Add(stage);

			//...hmm I forgot the rest.
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(11, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
            stages.Add(stage);

			//You need great dough to make great pizza, but the sauce is just as important! If you want to make the best sauce you're going to want fresh tomatoes. Luckily for us, there are local farmers who will deliver right to your restaurant.
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(12, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};         
            stages.Add(stage);

			//To order tomatoes, tap on the phone icon. Tap on the vegetables tab. Now add the tomatoes to your shopping cart like you did with the flour. When you're done tap the checkmark.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(13, true);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, false);
			};
			stateCheck = new GamePlayerStateCheck();
			stateCheck.Rules.Add(
				new WorkInProgressCompareRule 
				{
				Item = new ItemQuantity
				{
					ItemCode = BuildableItemEnum.Tomatoes,
					UnStoredQuantity = 1            	  		
				},
				ComparisonType = ComparisonEnum.GreaterThan
			});
			stage.PlayerStateCheck = stateCheck;
			stages.Add(stage);

			//Way to go kiddo, you're learning how to run a business! It takes a little longer to get stuff from the farm, but those tomatoes will be before you know it.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				GUIItem window = GUIStateManager.Instance
					.GetChildNested(GUIElementEnum.OrderIngredientsWindow);				
				window.Visible = false;
				SetStageText(14, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};         
			stages.Add(stage);

			//It seems you inherited another mess this time its in the kitchen. The dishes need to be cleaned if you want to serve customers. Dishes will accumulate as customers eat your pizza. You'll need to keep the dishes clean in order to keep working!
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(15, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};         
			stages.Add(stage);

			//To wash dishes, tap the dishes that have accumulated in the sink and then take the soap and scrub it over the dishes.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(16, true);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Negative, false);
				GameObject dirtyTable = ResourceManager<GameObject>.Instance.Load(ResourceEnum.PREFAB_DIRTY_DISHES);			
				GameObject.Instantiate(dirtyTable, new Vector3(-5f,-0.6f,0), Quaternion.identity);			
			};
			stateCheck = new GamePlayerStateCheck ();
			stateCheck.Rules.Add(
				new StorageItemUnstoredQuantityCompareRule
				{
				Location = 0,
				Level = 0,
				Item = BuildableItemEnum.Dirty_Dishes,
				Quantity = 1,
				ComparisonType = ComparisonEnum.LessThan
			});
			stage.PlayerStateCheck = stateCheck;
			stages.Add(stage);

			//Gee you're a hard worker. Keep this determination and your pizza restaurants are going to be all over this city in no time.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(17, true);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};         
			stages.Add(stage);

			//Wait for flour to arrive...
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(0, false);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, false);
			};   
			stateCheck = new GamePlayerStateCheck ();
			stateCheck.Rules.Add(
				new StorageItemUnstoredQuantityCompareRule
				{
				Location = 0,
				Level = 0,
				Item = BuildableItemEnum.White_Flour,
				Quantity = 1,
				ComparisonType = ComparisonEnum.GreaterThanOrEqual
			});
			stage.PlayerStateCheck = stateCheck;

			//Well look at that! Your flour is here. I told you it wouldn't take long. To unload the flour, tap the flour bag in the back of the delivery van and drag it into your restaurant.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(18, true);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};         
			stages.Add(stage);

            SetStage(player.TutorialStage);
        }
		
		/// <summary>
		/// Toggles the more text button
		/// </summary>
		public void ToggleMoreTextButton(bool visible)
		{
			GUIButton button = GUIStateManager.Instance
				.GetChildNested(GUIElementEnum.TutorialMoreText) as GUIButton;
			button.Visible = visible;
		}
		
        /// <summary>
        /// Sets the text for this stage
        /// </summary>
        /// <param name="index">Index.</param>
        public void SetStageText(int index, bool visible)
        {
			GUIBox box = GUIStateManager.Instance
				.GetChildNested(GUIElementEnum.TutorialDialogueBox) as GUIBox;
            box.Content.text = sayings[index];
			box.Visible = visible;
        }

        /// <summary>
        /// Sets the texture for the Louie image to the 
        ///	indicated expression
        /// </summary>
        public void SetLouieTexture(LouieExpression expr, bool visible)
        {
			GUIImage image = GUIStateManager.Instance
				.GetChildNested(GUIElementEnum.TutorialLouie) as GUIImage;
				
        	if (!visible)
        	{
				image.Visible = false;
        		return;
        	}
        	
			image.Visible = true;
        	
            Texture2D txr = louieTextures[(int)expr];
			
            float ratio = (float)txr.width / (float)txr.height;
            float height = Screen.height * 0.90f;
            float width = height * ratio;
                                		        	
			image.SetRectangle(-width * 0.1f, Screen.height - height, width, height, false, ScaleMode.ScaleToFit);
            
			image.Content.image = txr;
        }
        
        /// <summary>
        /// Finishes the tutorial
        /// </summary>
        public void FinishTutorial()
        {
            IsFinished = true;
            
            // @TO DO Make sure we destroy all objects and clean everything up!
			ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.AUDIOCLIP_UNCLELOUIE);
			
            ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_ICON_MORE_TEXT);            
			ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_0);
			ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_1);
			ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_2);
			ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_3);
			ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_4);
			ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_5);
			ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_6);
	
			GUIStateManager.Instance.RemoveChild(GUIElementEnum.TutorialLouie);
			GUIStateManager.Instance.RemoveChild(GUIElementEnum.TutorialDialogueWindow);

			louieDialogueWindow.Destroy();
			louieDialogueBox.Destroy();
			louieImage.Destroy();
			moreTextButton.Destroy();
			
			louieDialogueBox = null;
			louieImage = null;
			moreTextButton = null;

			stages.Clear ();
			stages = null;
			currentStage = null;
			sayings = null;
			louieTextures.Clear ();
			louieTextures = null;
		}
		
		/// <summary>
		/// 
		/// </summary>
        /// <param name="index"></param>
        public void SetStage(int index)
        {
            currentStageIndex = index;

			if (player != null)
			{
				if (player.TutorialStage != currentStageIndex)
				{
					player.TutorialStage = currentStageIndex;
					player.StateChanged = true;
					
					ServerCommunicator.Instance.Communicate(
						ServerActionEnum.SetTutorialStage, currentStageIndex,
						(ServerCommunication com) => 
						{						
						}, GUIGameObject.SetGlobalError);
				}
			}

            if (currentStageIndex >= stages.Count)
            {
                FinishTutorial();
                return;
            }

            currentStage = stages[currentStageIndex];
            currentStage = stages[index];                       

            if (currentStage.OnStart != null)
            {
                currentStage.OnStart();
            }            
        }

        /// <summary>
        /// Tries to advance to the next tutorial stage
        /// </summary>
        public void TryAdvance(GUIEvent guiEvent)
        {
        	if (currentStage.PlayerStateCheck == null && 
        		currentStage.GUIEvent == GUIEvent.Empty)
        	{
        		return;
        	}

			if (currentStage.PlayerStateCheck != null)
			{
				Debug.Log(currentStage.PlayerStateCheck);
				Debug.Log(currentStage.PlayerStateCheck.IsValid(player));
			}
			
			if (currentStage.GUIEvent != GUIEvent.Empty)
			{
				Debug.Log(currentStage.GUIEvent);
				Debug.Log(currentStage.GUIEvent == guiEvent);
			}
						
			if ((currentStage.PlayerStateCheck == null || currentStage.PlayerStateCheck.IsValid(player)) &&
                (currentStage.GUIEvent == guiEvent))
            {
                SetStage(currentStageIndex + 1);
            }
        }
	
        /// <summary>
        /// Called when the tutorial manager should update
        /// </summary>
		public void UpdateState()
        {
            if (IsFinished)
            {
                return;
            }

            TryAdvance(GUIEvent.Empty);
        }
    }
}