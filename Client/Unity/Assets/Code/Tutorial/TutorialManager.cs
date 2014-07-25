namespace KS.PizzaEmpire.Unity
{	
	using System;
	using UnityEngine;
	
	public class TutorialManager
	{
		private static volatile TutorialManager instance;
		private static object syncRoot = new object();
		
		private TutorialStage[] stages;
		private int currentStageIndex;
		private TutorialStage currentStage;
		private string[] sayings;
	
		private bool allDone = false;
	
		private TutorialManager() {
			TextAsset unclelouie = Resources.Load("Text/unclelouie") as TextAsset;
			sayings = unclelouie.text.Split('\n');
		}
		
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
						}
					}
				}
				return instance;
			}
		}
		
		/// <summary>
		/// Renders the tutorial text.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RenderTutorialText(int index)
		{
			GUI.Box (new Rect (10, 10, Screen.width - 10, 100), sayings[index], GUIGameObject.CurrentStyle);
		}
	
		public void Initialize()
		{
			stages = new TutorialStage[4];
			TutorialStage stage;
			int cs = 0;
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(0);
			};
			stages[cs++] = stage;
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(1);
			};
			stages[cs++] = stage;
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(2);
			};
			stages[cs++] = stage;
			
			stage = new TutorialStage();
			stage.ShowNextButton = true;
			stage.Render = () => {
				RenderTutorialText(3);
			};
			stages[cs++] = stage;
			
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
			
			if ((currentStage.PlayerStateCheck == null || currentStage.PlayerStateCheck.IsTrue(player)) &&
				(currentStage.GUIEventCheck == null || currentStage.GUIEventCheck.IsSame(guiEvent)))
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

			if (currentStageIndex >= stages.Length) {
				allDone = true;
			} else {
				currentStage = stages[currentStageIndex];
			}
		}
	
		/// <summary>
		/// Called when the tutorial manager should update
		/// and render
		/// </summary>
		public void OnGUI(GamePlayer player, GUIEvent guiEvent)
		{
			if (allDone) {
				return;
			}
	
			TryAdvance(player, guiEvent);
	
			if (currentStage.Render != null)
			{
				currentStage.Render();
			}
			
			if (currentStage.ShowNextButton)
			{
				if (GUI.Button(new Rect(Screen.width - 55, 55, 45, 45), GUIGameObject.IconMoreText))
			 	{
					Advance();
			 	}
			}
		}			
	}
}