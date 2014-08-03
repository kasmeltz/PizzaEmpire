namespace KS.PizzaEmpire.Unity
{	
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	
	/// <summary>
	/// Represents the common GUI elements in the
	///	restaurant screen
	/// </summary>
	public class RestaurantCommonElements
	{
		public static GamePlayer Player { get; set; }
		
		public static void Load(GamePlayer player)
		{
			Player = player;
			
			GamePlayerStateCheck availableCheck;
			GamePlayerStateCheck enabledCheck;
			
			GUIItem phoneIcon = new GUIItem (0.05f, 0.90f, 0.05f, 0.05f);
			phoneIcon.Element = GUIElementEnum.IconPhone;
			phoneIcon.Style = 
				LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
			phoneIcon.Texture = ResourceManager<Texture2D>.Instance.Load (ResourceEnum.TEXTURE_ICON_PHONE);
			phoneIcon.Render = (gi) =>
			{
				if (GUI.Button(gi.Rectangle, gi.Texture, gi.Style))
				{
					GUIItem window = GUIStateManager.Instance
						.GetChildNested(GUIElementEnum.OrderIngredientsWindow);
					
					window.Visible = !window.Visible;
				}
			};
			availableCheck = new GamePlayerStateCheck();
			availableCheck.TutorialStage = 3;
			phoneIcon.AvailableCheck = availableCheck;
			
			GUIStateManager.Instance.AddChild(phoneIcon);			
			GUIStateManager.Instance.UpdateState(player);
		}
	}
}