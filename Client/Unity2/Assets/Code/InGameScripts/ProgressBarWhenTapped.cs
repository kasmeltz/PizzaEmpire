namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.GameLogic;
	
	public class ProgressBarWhenTapped : MonoBehaviour 
	{
		protected bool isTapped;
		
		public GUIElementEnum TappedElement = GUIElementEnum.InGameMotorcycle;
		public GUIElementEnum ProgressBarElement = GUIElementEnum.ProgressBarDryGoodTruck;
		public BuildableItemEnum BuildableItem = BuildableItemEnum.Dry_Goods_Delivery_Truck_L1;
		public List<WorkItem> WorkItems;
		
		GUIItemBox guiItemBox;
		WorkItemProgressBar progressBar;
		
		public void Tapped()
		{
			if (isTapped)
			{
				UnTapped();
				return;
			}
			
			Debug.Log(DateTime.Now + ": Tapped");
			
			isTapped = true;
			
			double maxRatio = -1;
			WorkItem workItem = null;
			GamePlayer player = GamePlayerManager.Instance.LoggedInPlayer;
			WorkItems = GamePlayerLogic.Instance.GetCurrentWorkItemsForProductionItem(player, BuildableItem);
			foreach(WorkItem wi in WorkItems)
			{
				double ratio = GamePlayerLogic.Instance.GetPercentageCompleteForWorkItem(wi);
				if (ratio > maxRatio)
				{
					workItem = wi;
					maxRatio = ratio;			
				}
			}
			
			guiItemBox = GUIItemFactory<GUIItemBox>.Instance.Pool.New();
			guiItemBox.SetRectangle(transform.position.x - 2f, transform.position.y - 0.1f, 0.3f, 0.3f, true);			
			guiItemBox.Element = ProgressBarElement;
			guiItemBox.Style = 
				LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
			guiItemBox.Visible = true;
			
			GUIStateManager.Instance.AddChild(guiItemBox);							
						
			if (workItem != null)
			{
				progressBar = GUIItemFactory<WorkItemProgressBar>.Instance.Pool.New();
				progressBar.SetRectangle(0.05f, 0.05f, 0.2f, 0.05f, false);
				progressBar.Element = GUIElementEnum.ProgressBar;
				progressBar.Style =  
					LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
				progressBar.Visible = true;
				
				progressBar.Texture = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_BAR_OUTER);
				progressBar.BarInner = 	ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_BAR_INNER);
				
				progressBar.WorkItem = workItem;
				
				guiItemBox.AddChild(progressBar);	
			}
			
			GUIStateManager.Instance.TapHandled(this.gameObject, TappedElement);
		}
		
		public void UnTapped()
		{
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
