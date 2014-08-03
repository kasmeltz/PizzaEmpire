namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using System;
	
	public class ProgressBarWhenTapped : MonoBehaviour 
	{
		protected bool isTapped;
		
		public GUIElementEnum MyElement = GUIElementEnum.InGameMotorcycle;
		public GUIElementEnum ProgressBarElement = GUIElementEnum.ProgressBarDryGoodTruck;
		
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
			
			guiItemBox = GUIItemFactory<GUIItemBox>.Instance.Pool.New();
			Vector2 screenCoords = Camera.main.WorldToScreenPoint(this.transform.position);
			guiItemBox.SetRectangle(screenCoords.x, screenCoords.y + 100, 100, 100);
			guiItemBox.Element = ProgressBarElement;
			guiItemBox.Style = 
				LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
			guiItemBox.Visible = true;
			
			GUIStateManager.Instance.AddChild(guiItemBox);			
			
			GUIStateManager.Instance.TapHandled(this.gameObject, MyElement);
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
