namespace KS.PizzaEmpire.Unity
{	
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	
	/// <summary>
	/// Represents the window for ordering ingreduents
	/// in the game's GUI
	/// </summary>
	public class OrderIngredientsWindow
	{
		public static GamePlayer Player { get; set; }
		
		public static void Load(GamePlayer player)
		{
			Player = player;
			
			GamePlayerStateCheck availableCheck;
			GamePlayerStateCheck enabledCheck;					
			
			GUIItemFactory<GUIItemImage>.Instance.Pool.New();
			
			GUIItemImage ingredientMenu = GUIItemFactory<GUIItemImage>.Instance.Pool.New();
			ingredientMenu.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_WIN_ORDER_INGREDIENT);			
			ingredientMenu.SetRectangle(0.0f, 0.05f, 0.40f, 0.80f, false, ScaleMode.ScaleToFit);
			ingredientMenu.Element = GUIElementEnum.OrderIngredientsWindow;
			ingredientMenu.Visible = false;			
			
			GUIStateManager.Instance.AddChild(ingredientMenu);
			
			GUIItemImage flourIngredient = GUIItemFactory<GUIItemImage>.Instance.Pool.New();
			flourIngredient.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_WHITE_FLOUR);			
			flourIngredient.SetRectangle(0.05f, 0.05f, 0.05f, 0.075f, false, ScaleMode.ScaleToFit);
			flourIngredient.Draggable = DraggableEnum.RAW_INGREDIENT;
			flourIngredient.DuplicateOnDrag = true;
			flourIngredient.Element = GUIElementEnum.IconFlour;
			flourIngredient.BuildableItem = BuildableItemEnum.White_Flour;
			enabledCheck = new GamePlayerStateCheck();
			enabledCheck.CanBuildItem = BuildableItemEnum.White_Flour;
			flourIngredient.EnabledCheck = enabledCheck;
			
			ingredientMenu.AddChild (flourIngredient);
			
			GUIItemImage ingredientShoppingCart = GUIItemFactory<GUIItemImage>.Instance.Pool.New();
			ingredientShoppingCart.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_SHOPPING_CART);			
			ingredientShoppingCart.SetRectangle(0.21f, 0.68f, 0.12f, 0.12f, false, ScaleMode.ScaleToFit);
			ingredientShoppingCart.Droppable = DraggableEnum.RAW_INGREDIENT;
			ingredientShoppingCart.Element = GUIElementEnum.IconShoppingCart;
			ingredientShoppingCart.OnDrop = (i1, i2) =>
			{
				if (i2.BuildableItem != BuildableItemEnum.None)
				{
					ServerCommunicator.Instance.Communicate(
						ServerActionEnum.StartWork, (int)i2.BuildableItem,
						(ServerCommunication com) => 
						{
							GamePlayerLogic.Instance.StartWork(OrderIngredientsWindow.Player, i2.BuildableItem);
						}, GUIGameObject.SetGlobalError);
				}
			};
			
			ingredientMenu.AddChild (ingredientShoppingCart);			
		}
	}
}