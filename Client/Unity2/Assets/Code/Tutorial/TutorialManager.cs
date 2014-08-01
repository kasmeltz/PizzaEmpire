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

            moreTextRect.x = Screen.width - 65;
            moreTextRect.y = 165;
            moreTextRect.width = 45;
            moreTextRect.height = 45;
        }

        /// <summary>
        /// Initializes the tutorial manager instance
        /// </summary>
        public void Initialize()
        {
            LoadAssets();

            TutorialStage stage;
            GamePlayerStateCheck stateCheck = null;

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

            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(1);
                DrawLouie(LouieExpression.Surprised);
            };
            stages.Add(stage);

            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(2);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

            stage = new TutorialStage();
            stage.Render = () =>
            {
                RenderTutorialText(3);
                DrawLouie(LouieExpression.Excited_Happy);
            };
            stateCheck = new GamePlayerStateCheck();
            stateCheck.WorkItemsInProgress = new List<ItemQuantity>();
            stateCheck.WorkItemsInProgress.Add (
                new ItemQuantity { ItemCode = BuildableItemEnum.White_Flour, Quantity = 1 });
            stage.PlayerStateCheck = stateCheck;
            stages.Add(stage);

            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(4);
                DrawLouie(LouieExpression.Excited_Happy_Thumbs_Up);
            };
            stages.Add(stage);

            stage = new TutorialStage();
            stage.OnStart = () =>
            {
                AudioSource.PlayClipAtPoint(louieSound, Vector3.zero);
            };
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(5);
                DrawLouie(LouieExpression.Surprised_Eye_Pop);
            };
            stages.Add(stage);

            stage = new TutorialStage();
            stage.Render = () =>
            {
                RenderTutorialText(6);
                DrawLouie(LouieExpression.Surprised_Negative);
            };
            stage.GUIEvent = new GUIEvent { GEvent = GUIEventEnum.Wipe, Element = GUIElementEnum.TableCloth };
            stages.Add(stage);

            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(7);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(8);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(9);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

            stage = new TutorialStage();
            stage.ShowNextButton = true;
            stage.Render = () =>
            {
                RenderTutorialText(10);
                DrawLouie(LouieExpression.Surprised_Arm_Raised);
            };
            stages.Add(stage);

            SetStage(0);
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
            ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_ICON_MORE_TEXT);
            ResourceManager<AudioClip>.Instance.UnLoad(ResourceEnum.AUDIOCLIP_UNCLELOUIE);
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
                SetStage(currentStageIndex + 1);
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
                    SetStage(currentStageIndex + 1);
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