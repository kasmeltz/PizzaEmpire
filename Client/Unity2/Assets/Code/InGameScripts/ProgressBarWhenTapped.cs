namespace KS.PizzaEmpire.Unity
{
	using System;
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.GameLogic;
	using UnityEngine;
	
	public class ProgressBarWhenTapped : Tappable 
	{
		public GUIElementEnum ProgressBarElement = GUIElementEnum.ProgressBarDryGoodTruck;
		public BuildableItemEnum BuildableItem = BuildableItemEnum.Dry_Goods_Delivery_Truck;
		public List<WorkInProgress> InProgress;
		
		GUIBox guiItemBox;
		WorkItemProgressBar progressBar;
		
		public override void Tap()
		{
			Debug.Log ("ProgressBarWhenTapped Tap!");

			base.Tap();

			if (isTapped)
			{
				UnTap();
				return;
			}
			
			isTapped = true;
			
			double maxRatio = -1;
			WorkInProgress workInProgress = null;
			GamePlayer player = GamePlayerManager.Instance.LoggedInPlayer;
			InProgress = GamePlayerLogic.Instance.GetCurrentWorkItemsForProductionItem(player, BuildableItem);
			foreach(WorkInProgress wip in InProgress)
			{
				double ratio = GamePlayerLogic.Instance.GetPercentageCompleteForWorkItem(wip);
				if (ratio > maxRatio)
				{
					workInProgress = wip;
					maxRatio = ratio;			
				}
			}
			
			guiItemBox = GUIItemFactory<GUIBox>.Instance.Pool.New();
			guiItemBox.SetRectangle(transform.position.x - 2f, transform.position.y - 0.1f, 0.3f, 0.3f, true, ScaleMode.StretchToFill);			
			guiItemBox.Element = ProgressBarElement;
			guiItemBox.Style = 
				LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
			guiItemBox.Visible = true;
			
			GUIStateManager.Instance.AddChild(guiItemBox);							
						
			if (workInProgress != null)
			{
				progressBar = GUIItemFactory<WorkItemProgressBar>.Instance.Pool.New();
				progressBar.Content.image = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_BAR_OUTER);
				progressBar.BarInner = 	ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_BAR_INNER);				
				progressBar.SetRectangle(0.05f, 0.05f, 0.2f, 0.05f, false, ScaleMode.StretchToFill);				
				progressBar.Element = GUIElementEnum.ProgressBar;
				progressBar.Visible = true;								
				
				progressBar.WorkInProgress = workInProgress;
				
				guiItemBox.AddChild(progressBar);	
			}
		}
		
		public override void UnTap()
		{
			Debug.Log ("ProgressBarWhenTapped UnTap!");

			base.UnTap();

			if (!isTapped)
			{
				return;
			}
			
			if (progressBar != null)
			{
				guiItemBox.RemoveChild(GUIElementEnum.ProgressBar);			
				progressBar.Destroy();							
				progressBar = null;
				ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_BAR_OUTER);
				ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_BAR_INNER);				
			}
			
			guiItemBox.Destroy();
			guiItemBox = null;		
						
			GUIStateManager.Instance.RemoveChild(ProgressBarElement);			
			
			isTapped = false;
			Debug.Log(DateTime.Now + ": UnTapped");
		}
	}
}
