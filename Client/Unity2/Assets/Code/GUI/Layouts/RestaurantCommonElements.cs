namespace KS.PizzaEmpire.Unity
{	
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	using Common.GameLogic.GamePlayerState;
	
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

			/// COINS
			GUIImage coins = GUIItemFactory<GUIImage>.Instance.Pool.New (); 
			coins.Content.image = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_COINS);			
			coins.SetRectangle(0.90f, 0.025f, 0.075f, 0.075f, false, ScaleMode.ScaleToFit);
			coins.Element = GUIElementEnum.IconCoins;
			GUIStateManager.Instance.AddChild(coins);	

			GUIBox coinBox = GUIItemFactory<GUIBox>.Instance.Pool.New (); 
			coinBox.SetRectangle (0.79f, 0.025f, 0.1f, 0.1f, false, ScaleMode.ScaleToFit);
			coinBox.Style = 
					LightweightResourceManager<GUIStyle>.Instance
						.Get (ResourceEnum.GUISTYLE_BASIC_STYLE);
			coinBox.OnStateUpdate = (gi) =>
			{
				gi.Content.text = GamePlayerManager.Instance.LoggedInPlayer.Coins.ToString();
			};
			coinBox.Element = GUIElementEnum.BoxCoins;
			GUIStateManager.Instance.AddChild (coinBox);


			//GUI.TextArea(new Rect(0,0,50, 20), player.Coins.ToString());

			//GUI.TextArea(new Rect(50,0,50, 20), player.Coupons.ToString());

			/// COUPONS
			GUIImage coupons = GUIItemFactory<GUIImage>.Instance.Pool.New (); 
			coupons.Content.image = ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_COUPONS);			
			coupons.SetRectangle(0.90f, 0.1f, 0.1f, 0.1f, false, ScaleMode.ScaleToFit);
			coupons.Element = GUIElementEnum.IconCoupons;
			GUIStateManager.Instance.AddChild(coupons);

			GUIBox couponBox = GUIItemFactory<GUIBox>.Instance.Pool.New (); 
			couponBox.SetRectangle (0.79f, 0.1f, 0.1f, 0.1f, false, ScaleMode.ScaleToFit);
			couponBox.Style = 
				LightweightResourceManager<GUIStyle>.Instance
					.Get (ResourceEnum.GUISTYLE_BASIC_STYLE);
			couponBox.OnStateUpdate = (gi) =>
			{
				gi.Content.text = GamePlayerManager.Instance.LoggedInPlayer.Coupons.ToString();
			};
			couponBox.Element = GUIElementEnum.BoxCoupons;
			GUIStateManager.Instance.AddChild (couponBox);
			
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
			availableCheck.Rules.Add(
				new TutorialStageCompareRule 
				{
			 		TutorialStage = 3,
				 	ComparisonType = ComparisonEnum.GreaterThanOrEqual
				});
			phoneIcon.AvailableCheck = availableCheck;
			
			GUIStateManager.Instance.AddChild(phoneIcon);	
			
			Debug.Log ("PHONE ICON: " + phoneIcon.Rectangle);		
		}
	}
}