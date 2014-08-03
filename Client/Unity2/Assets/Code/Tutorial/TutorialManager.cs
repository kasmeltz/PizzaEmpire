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
        private Rect louieRect;
        private Rect textRect;
        private Rect moreTextRect;
        private Texture2D moreTextIcon;

        public bool IsFinished { get; protected set; }

        public GUIStyle Style { get; set; }

        public AudioClip louieSound;

        private TutorialManager()
        {
            TextAsset unclelouie = Resources.Load<TextAsset>("Text/unclelouie");
            sayings = unclelouie.text.Split('\n');
            stages = new List<TutorialStage>();
            textRect = new Rect(0, 0, 0, 0);
            louieRect = new Rect(0, 0, 0, 0);
            moreTextRect = new Rect(0, 0, 0, 0);
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

            moreTextIcon = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_MORE_TEXT);

			moreTextRect.width = Screen.width * 0.05f;
			moreTextRect.height = Screen.height * 0.075f;
			moreTextRect.x = Screen.width * 0.92f;
			moreTextRect.y = Screen.height * 0.32f;
        }

        /// <summary>
        /// Initializes the tutorial manager instance
        /// </summary>
        public void Initialize(GamePlayer player)
        {
            LoadAssets();

            TutorialStage stage;
            GamePlayerStateCheck stateCheck = null;

			//Hey kiddo, it's your Uncle Louie, how's it going? I see the old man left you in charge of this place. You know he never did manage to make something of this place, but I have faith in you, kid.
            stage = new TutorialStage();
            stage.OnStart = () =>
            {
                AudioSource.PlayClipAtPoint(louieSound, Vector3.zero);
            };
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(0);
                DrawLouie(LouieExpression.Normal);
            };
            stages.Add(stage);

			//Don't worry, I'll help show you the ropes, so you can build the biggest pizza empire in this city!
            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(1);
                DrawLouie(LouieExpression.Surprised);
            };
            stages.Add(stage);

			//The first thing we need to do is call the wholesaler and order some flour. You can't make pizza dough without flour, and the dough is the secret to the best tasting pizza!
            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(2);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

			//To phone the wholesaler tap on the phone icon. Tap and drag the white flour icon into your shopping cart. Tap the checkmark when you're done.
            stage = new TutorialStage();
            stage.Render = () =>
            {
                RenderTutorialText(3);
                //DrawLouie(LouieExpression.Excited_Happy);
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
            };
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(4);
                DrawLouie(LouieExpression.Excited_Happy_Thumbs_Up);
            };
            stages.Add(stage);
            
			//To check how long it will be before your flour arrives, do something.
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () =>
			{
				RenderTutorialText(5);
				DrawLouie(LouieExpression.Surprised_Arm_Raised);
			};
			stages.Add(stage);

			//Excellent! You see I told you it wouldn't be long. Just let me know when it gets here I'll help you unload it.
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () =>
			{
				RenderTutorialText(6);
				DrawLouie(LouieExpression.Excited_Happy);
			};
			stages.Add(stage);
			
			//In the meantime it looks like we have some cleaning up to do around here. Look at that table it's filthy! No one will want to eat on a filthy table.
            stage = new TutorialStage();
            stage.OnStart = () =>
            {
                AudioSource.PlayClipAtPoint(louieSound, Vector3.zero);
            };
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(7);
                DrawLouie(LouieExpression.Surprised_Eye_Pop);
            };
            stages.Add(stage);

			//To clean a table, tap on it and then take the cloth and wipe it.
            stage = new TutorialStage();
            stage.Render = () =>
            {
                RenderTutorialText(8);
                DrawLouie(LouieExpression.Surprised_Negative);
            };
            stage.GUIEvent = new GUIEvent { GEvent = GUIEventEnum.Wipe, Element = GUIElementEnum.TableCloth };
            stages.Add(stage);

			//Good work! Tables will get dusty over time on their own and customers will also mess them up. Customers won't sit in your restaurant to be served unless the table is clean!
            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(9);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

			//I hope you didn't think that that owning a pizza restaurant was going to be all fun and games? It's like my father always said "owning a pizza restaurant is not all fun and games..." 
            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(10);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

			//...hmm I forgot the rest.
            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(11);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

			//You need great dough to make great pizza, but the sauce is just as important! If you want to make the best sauce you're going to want fresh tomatoes. Luckily for us, there are local farmers who will deliver right to your restaurant.
            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(12);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

            SetStage(player.TutorialStage, player);
        }


        /// <summary>
        /// Renders the tutorial text.
        /// </summary>
        /// <param name="index">Index.</param>
        public void RenderTutorialText(int index)
        {
            textRect.width = Screen.width * 0.68f;
            textRect.height = Screen.height * 0.35f;
            textRect.x = Screen.width * 0.3f;
            textRect.y = Screen.height * 0.05f;

            GUI.Box(textRect, sayings[index], Style);
        }

        /// <summary>
        /// Draws the indicated expressions of Louie
        /// </summary>
        /// <param name="expr">The expression to show</param>
        public void DrawLouie(LouieExpression expr)
        {
            Texture2D txr = louieTextures[(int)expr];
			
            float ratio = (float)txr.width / (float)txr.height;
            float height = Screen.height * 0.90f;
            float width = height * ratio;

            louieRect.x = -width * .1f;
            louieRect.y = Screen.height - height;
            louieRect.width = width;
            louieRect.height = height;

            GUI.DrawTexture(louieRect, txr);
        }
        
        /// <summary>
        /// Finishes the tutorial
        /// </summary>
        public void FinishTutorial()
        {
            IsFinished = true;
            
            // @TO DO Make sure we destroy all objects and clean everything up!
            ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_ICON_MORE_TEXT);
            ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.AUDIOCLIP_UNCLELOUIE);
			ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_0);
			ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_1);
			ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_2);
			ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_3);
			ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_4);
			ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_5);
			ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.TEXTURE_TUTORIAL_LOUIE_6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void SetStage(int index, GamePlayer player)
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
        public void TryAdvance(GamePlayer player, GUIEvent guiEvent)
        {
            if (currentStage.ShowNextButton)
            {
                return;
            }

            if (currentStage.PlayerStateCheck != null)
            {
                Debug.Log(currentStage.PlayerStateCheck);
                Debug.Log(currentStage.PlayerStateCheck.CheckAll(player));
            }
            if (currentStage.GUIEvent != null)
            {
                Debug.Log(currentStage.GUIEvent);
                Debug.Log(currentStage.GUIEvent.IsSame(guiEvent));
            }

            if ((currentStage.PlayerStateCheck == null || currentStage.PlayerStateCheck.CheckAll(player)) &&
                (currentStage.GUIEvent == null || currentStage.GUIEvent.IsSame(guiEvent)))
            {
                SetStage(currentStageIndex + 1, player);
            }
        }

        /// <summary>
        /// Called when the tutorial manager should render
        /// </summary>
        public void OnGUI(GamePlayer player)
        {
            if (IsFinished)
            {
                return;
            }

            if (currentStage.Render != null)
            {
                currentStage.Render();
            }

            if (currentStage.ShowNextButton)
            {
                if (GUI.Button(moreTextRect,moreTextIcon,Style))
                {
                    SetStage(currentStageIndex + 1, player);
                }
            }
        }

        /// <summary>
        /// Called when the tutorial manager should update
        /// </summary>
        public void Update(GamePlayer player)
        {
            if (IsFinished)
            {
                return;
            }

            TryAdvance(player, GUIEvent.Empty);
        }
    }
}