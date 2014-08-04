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
		
		GUIItemBox guiItemBox;
		
		public void Tapped()
		{
			if (isTapped)
			{
				UnTapped();
				return;
			}
			
			Debug.Log(DateTime.Now + ": Tapped");
			
			isTapped = true;
			
			GamePlayer player = GamePlayerManager.Instance.LoggedInPlayer;
			List<WorkItem> workItems = GamePlayerLogic.Instance.GetCurrentWorkItemsForProductionItem(player, BuildableItem);
			foreach(WorkItem wi in workItems)
			{
				double ratio = GamePlayerLogic.Instance.GetPercentageCompleteForWorkItem(wi);
				Debug.Log("Work item for delivery truck!: " + wi.ItemCode + " : " + wi.FinishTime + " : " + ratio);
			}
			
			guiItemBox = GUIItemFactory<GUIItemBox>.Instance.Pool.New();
			guiItemBox.SetRectangle(transform.position.x, transform.position.y, 0.2f, 0.2f, true);			
			guiItemBox.Element = ProgressBarElement;
			guiItemBox.Style = 
				LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
			guiItemBox.Visible = true;
			
			GUIStateManager.Instance.AddChild(guiItemBox);			
			
			GUIStateManager.Instance.TapHandled(this.gameObject, TappedElement);
		}
		
		public void UnTapped()
		{
			if (!isTapped)
			{
				return;
			}
			
			GUIItemFactory<GUIItemBox>.Instance.Pool.Store(guiItemBox);
			guiItemBox = null;		
			GUIStateManager.Instance.RemoveChild(ProgressBarElement);
			
			isTapped = false;
			Debug.Log(DateTime.Now + ": UnTapped");
		}
	}
}
