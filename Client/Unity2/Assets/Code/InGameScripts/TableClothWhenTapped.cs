namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.GameLogic;
	
	public class TableClothWhenTapped : MonoBehaviour 
	{
		protected bool isTapped;

		protected GUIItemImage dishCloth;

		public void Tapped()
		{
			if (isTapped)
			{
				return;
			}

			isTapped = true;

			dishCloth = GUIItemFactory<GUIItemImage>.Instance.Pool.New();
			dishCloth.Content.image = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_UI_DISHCLOTH);
			Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
			dishCloth.SetRectangle (screenPos.x, screenPos.y, 0.3f, 0.3f, false, ScaleMode.ScaleToFit);
			dishCloth.Element = GUIElementEnum.IconWipeTable;
			dishCloth.Visible = true;
			dishCloth.Draggable = DraggableEnum.DISH_CLOTH;
			dishCloth.DuplicateOnDrag = false;

			GUIStateManager.Instance.AddChild(dishCloth);		

			Debug.Log ("Tapped!");

			GUIStateManager.Instance.TapHandled(this.gameObject, GUIElementEnum.None);
		}

		public void UnTapped()
		{
			Debug.Log ("UnTapped!");

			if (dishCloth != null)
			{
				dishCloth.Destroy();							
				dishCloth = null;
				ResourceManager<Texture2D>.Instance.UnLoad(ResourceEnum.TEXTURE_UI_DISHCLOTH);
				GUIStateManager.Instance.RemoveChild(GUIElementEnum.IconWipeTable);
			}

			isTapped = false;
		}
	}
}
