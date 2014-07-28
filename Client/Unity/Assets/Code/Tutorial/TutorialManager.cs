namespace KS.PizzaEmpire.Unity
{	
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using Common.BusinessObjects; 
	
	public enum LouieExpression
	{
		Normal = 0,
		Excited_Happy_Thumbs_Up = 1,
		Excited_Happy = 2,		
		Surprised = 3,
		Surprised_Arm_Raised = 4,
		Surprised_Eye_Pop = 5,
		Surprised_Negative = 6
	}
	
	public class TutorialManager
	{
		private static volatile TutorialManager instance;
		private static object syncRoot = new object();
		
		private List<TutorialStage> stages;
		private int currentStageIndex;
		private TutorialStage currentStage;
		private string[] sayings;
	
		public bool IsFinished { get; protected set; }
	
		private TutorialManager() {
			TextAsset unclelouie = Resources.Load("Text/unclelouie") as TextAsset;
			sayings = unclelouie.text.Split('\n');
			stages = new List<TutorialStage>();
			textRect = new Rect(0,0,0,0);
			louieRect = new Rect(0,0,0,0);	
		}			
				
		private Texture2D[] louieTextures;			
		private Rect louieRect;	
		private Rect textRect;
		
		/// <summary>
		/// Provides the Singleton instance of the RedisCache
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
							instance.IsFinished = false;
							instance.LoadLouie();
						}
					}
				}
				return instance;
			}
		}
		
		private void LoadLouie()
		{
			louieTextures = new Texture2D[7];
			for (int i = 0;i < louieTextures.Length;i++)			
			{
				louieTextures[i] = Resources.Load("Graphics/UI/Characters/louie" + i) as Texture2D;
			}										
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
			
			GUI.Box (textRect,
				sayings[index], GUIGameObject.CurrentStyle);			
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
	
		public void Initialize()
		{
			TutorialStage stage;
			GamePlayerStateCheck stateCheck = null;
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(0);
				DrawLouie(LouieExpression.Normal);
			};
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(1);
				DrawLouie(LouieExpression.Surprised);
			};
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(2);
				DrawLouie(LouieExpression.Surprised_Arm_Raised);
			};
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.Render = () => {
				RenderTutorialText(3);
				DrawLouie(LouieExpression.Excited_Happy);
			};
			stateCheck = new GamePlayerStateCheck();
			stateCheck.WorkItemsInProgress = new ItemQuantity[1];
			stateCheck.WorkItemsInProgress[0] =
				new ItemQuantity { ItemCode = BuildableItemEnum.White_Flour, Quantity = 1 };
			stage.PlayerStateCheck = stateCheck;
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(4);
				DrawLouie(LouieExpression.Excited_Happy_Thumbs_Up);
			};
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(5);
				DrawLouie(LouieExpression.Surprised_Eye_Pop);
			};
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.Render = () => {
				RenderTutorialText(6);
				DrawLouie(LouieExpression.Surprised_Negative);
			};
			stage.GUIEvent = new GUIEvent { GEvent = GUIEventEnum.Wipe, Element = GUIElementEnum.TableCloth };
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(7);
				DrawLouie(LouieExpression.Surprised_Arm_Raised);
			};
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(8);
				DrawLouie(LouieExpression.Surprised_Arm_Raised);
			};
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(9);
				DrawLouie(LouieExpression.Surprised_Arm_Raised);
			};
			stages.Add(stage);
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(10);
				DrawLouie(LouieExpression.Surprised_Arm_Raised);
			};
			stages.Add(stage);
						
			currentStageIndex = 0;
			currentStage = stages[0];			
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
				Advance();
			}
		}
		
		/// <summary>
		/// Advance to the next stage in the tutorial
		/// </summary>
		public void Advance()
		{
			currentStageIndex++;
			
			if (currentStageIndex >= stages.Count) {
				IsFinished = true;
			} else {
				currentStage = stages[currentStageIndex];
			}
		}
	
		/// <summary>
		/// Called when the tutorial manager should render
		/// </summary>
		public void OnGUI(GamePlayer player)
		{
			if (IsFinished) {
				return;
			}			
		
			if (currentStage.Render != null)
			{
				currentStage.Render();
			}
			
			if (currentStage.ShowNextButton)
			{
				if (GUI.Button(new Rect(Screen.width - 65, 165, 45, 45), 
					GUIGameObject.IconMoreText, GUIGameObject.CurrentStyle))
			 	{
					Advance();
			 	}
			}
		}			
		
		/// <summary>
		/// Called when the tutorial manager should update
		/// </summary>
		public void Update(GamePlayer player)
		{
			if (IsFinished) {
				return;
			}			
						
			TryAdvance(player, GUIEvent.Empty);
		}
	}
}