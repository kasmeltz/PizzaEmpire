namespace KS.PizzaEmpire.Unity
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Common.BusinessObjects;

    public class TutorialManager
    {
        private static volatile TutorialManager instance;
        private static object syncRoot = new object();

        private List<TutorialStage> stages;
        private int currentStageIndex;
        private TutorialStage currentStage;
        private string[] sayings;

        private List<Texture2D> louieTextures;
        
        private GUIItemBox louieDialogueBox;
        private GUIItemImage louieImage;
        private GUIItemButton moreTextButton;
        
        private GamePlayer player;

        public bool IsFinished { get; protected set; }

        public GUIStyle Style { get; set; }

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
            
			Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);			

            louieTextures = new List<Texture2D>();
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_0));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_1));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_2));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_3));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_4));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_5));
            louieTextures.Add(ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_6));

            louieSound = ResourceManager<AudioClip>.Instance.Load(ResourceEnum.AUDIOCLIP_UNCLELOUIE);

			louieDialogueBox = GUIItemFactory<GUIItemBox>.Instance.Pool.New();
			louieDialogueBox.SetRectangle(0.3f, 0.05f, 0.68f, 0.35f, false);			                              			                              
      		louieDialogueBox.Element = GUIElementEnum.TutorialDialogueBox;
			louieDialogueBox.Style = Style;
      		
      		GUIStateManager.Instance.AddChild(louieDialogueBox);
			
			moreTextButton = GUIItemFactory<GUIItemButton>.Instance.Pool.New();
			moreTextButton.SetRectangle(0.62f, 0.27f, 0.05f, 0.075f, false);			                              			                              
			moreTextButton.Element = GUIElementEnum.TutorialMoreText;
			moreTextButton.Style = Style;
			moreTextButton.Texture = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_MORE_TEXT);
			moreTextButton.OnClick = (gi) =>
			{
				SetStage(currentStageIndex + 1);
			};
			
			louieDialogueBox.AddChild(moreTextButton);

			louieImage = GUIItemFactory<GUIItemImage>.Instance.Pool.New();
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
            	SetStageText(0);
            	ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Normal, true);
            };           
            stages.Add(stage);

			//Don't worry, I'll help show you the ropes, so you can build the biggest pizza empire in this city!
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(1);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised, true);
			};            
            stages.Add(stage);

			//The first thing we need to do is call the wholesaler and order some flour. You can't make pizza dough without flour, and the dough is the secret to the best tasting pizza!
            stage = new TutorialStage();            
			stage.OnStart = () =>
			{
				SetStageText(2);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
            stages.Add(stage);
            
			//To phone the wholesaler tap on the phone icon. Tap and drag the white flour icon into your shopping cart. Tap the checkmark when you're done.
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(3);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, false);
			};
            stateCheck = new GamePlayerStateCheck();
            stateCheck.WorkItemsInProgress = new List<ItemQuantity>();
            stateCheck.WorkItemsInProgress.Add (
                new ItemQuantity { ItemCode = BuildableItemEnum.White_Flour, Quantity = 1 });
            stage.PlayerStateCheck = stateCheck;
            stages.Add(stage);

			//You did good kid. You shouldn't have to wait too long to get your flour, the wholesaler is just around the corner. 
            stage = new TutorialStage();
            stage.OnStart = () =>
            {
				GUIItem window = GUIStateManager.Instance
					.GetChildNested(GUIElementEnum.OrderIngredientsWindow);				
				window.Visible = false;
				SetStageText(4);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Excited_Happy_Thumbs_Up, true);
            };
            stages.Add(stage);
            
			//To check how long it will be before your flour arrives, do something.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(5);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
			stage.GUIEvent = new GUIEvent(GUIElementEnum.InGameMotorcycle, GUIEventEnum.Tap);
			stages.Add(stage);

			//Excellent! You see I told you it wouldn't be long. Just let me know when it gets here I'll help you unload it.
			stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(6);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Excited_Happy, true);
			};
			stages.Add(stage);
			
			//In the meantime it looks like we have some cleaning up to do around here. Look at that table it's filthy! No one will want to eat on a filthy table.
            stage = new TutorialStage();
            stage.OnStart = () =>
            {
                AudioSource.PlayClipAtPoint(louieSound, Vector3.zero);
				SetStageText(7);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Eye_Pop, true);
            };
            stages.Add(stage);

			//To clean a table, tap on it and then take the cloth and wipe it.
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(8);
				ToggleMoreTextButton(false);
				SetLouieTexture(LouieExpression.Surprised_Negative, false);
			};
            stage.GUIEvent = new GUIEvent { GEvent = GUIEventEnum.Wipe, Element = GUIElementEnum.TableCloth };
            stages.Add(stage);

			//Good work! Tables will get dusty over time on their own and customers will also mess them up. Customers won't sit in your restaurant to be served unless the table is clean!
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(9);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
            stages.Add(stage);

			//I hope you didn't think that that owning a pizza restaurant was going to be all fun and games? It's like my father always said "owning a pizza restaurant is not all fun and games..." 
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(10);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
            stages.Add(stage);

			//...hmm I forgot the rest.
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(11);
				ToggleMoreTextButton(true);
				SetLouieTexture(LouieExpression.Surprised_Arm_Raised, true);
			};
            stages.Add(stage);

			//You need great dough to make great pizza, but the sauce is just as important! If you want to make the best sauce you're going to want fresh tomatoes. Luckily for us, there are local farmers who will deliver right to your restaurant.
            stage = new TutorialStage();
			stage.OnStart = () =>
			{
				SetStageText(12);
				ToggleMoreTextButton(true);
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
			GUIItemButton button = GUIStateManager.Instance
				.GetChildNested(GUIElementEnum.TutorialMoreText) as GUIItemButton; 
			button.Visible = visible;
		}
		
        /// <summary>
        /// Sets the text for this stage
        /// </summary>
        /// <param name="index">Index.</param>
        public void SetStageText(int index)
        {
			GUIItemBox box = GUIStateManager.Instance
				.GetChildNested(GUIElementEnum.TutorialDialogueBox) as GUIItemBox; 
			box.Text = sayings[index];
			box.Visible = true;
        }

        /// <summary>
        /// Sets the texture for the Louie image to the 
        ///	indicated expression
        /// </summary>
        public void SetLouieTexture(LouieExpression expr, bool visible)
        {
			GUIItemImage image = GUIStateManager.Instance
				.GetChildNested(GUIElementEnum.TutorialLouie) as GUIItemImage; 	
        	
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
                                		        	
			image.SetRectangle(-width * 0.1f, Screen.height - height, width, height, false);
            
            image.Texture = txr;
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
			GUIStateManager.Instance.RemoveChild(GUIElementEnum.TutorialDialogueBox);
			
			louieDialogueBox.Destroy();
			louieImage.Destroy();
			moreTextButton.Destroy();
			
			louieDialogueBox = null;
			louieImage = null;
			moreTextButton = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void SetStage(int index)
        {
            currentStageIndex = index;

            if (currentStageIndex >= stages.Count)
            {
                FinishTutorial();
                return;
            }

            currentStage = stages[currentStageIndex];
            currentStage = stages[index];                       

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
				Debug.Log(currentStage.PlayerStateCheck.CheckAll(player));
			}
			
			if (currentStage.GUIEvent != GUIEvent.Empty)
			{
				Debug.Log(currentStage.GUIEvent);
				Debug.Log(currentStage.GUIEvent == guiEvent);
			}
						
            if ((currentStage.PlayerStateCheck == null || currentStage.PlayerStateCheck.CheckAll(player)) &&
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