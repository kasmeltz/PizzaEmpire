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
			
			GUIButton phoneIcon = GUIItemFactory<GUIButton>.Instance.Pool.New();
			phoneIcon.Content.image = ResourceManager<Texture2D>.Instance.Load (ResourceEnum.TEXTURE_ICON_PHONE);			
			phoneIcon.SetRectangle(0.0f, 0.85f, 0.15f, 0.15f, false, ScaleMode.ScaleToFit);
			phoneIcon.Element = GUIElementEnum.IconPhone;
			phoneIcon.Style = 
				LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_NO_BACKGROUND);
			phoneIcon.OnClick = (gi) =>
			{
				GUIWindow window = GUIStateManager.Instance
					.GetChildNested(GUIElementEnum.OrderIngredientsWindow)
						as GUIWindow;
				
				window.Toggle();
			};
			availableCheck = new GamePlayerStateCheck();
			availableCheck.TutorialStage = 3;
			phoneIcon.AvailableCheck = availableCheck;
			
			GUIStateManager.Instance.AddChild(phoneIcon);	
			
			Debug.Log ("PHONE ICON: " + phoneIcon.Rectangle);		
		}
	}
}