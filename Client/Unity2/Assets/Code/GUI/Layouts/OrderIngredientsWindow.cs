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
			
			GUIItemBox ingredientMenu = new GUIItemBox (0.05f, 0.05f, 0.25f, 0.80f);
			ingredientMenu.Element = GUIElementEnum.OrderIngredientsWindow;
			ingredientMenu.Style = 
				LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
			ingredientMenu.Visible = false;
			
			GUIStateManager.Instance.AddChild(ingredientMenu);
			
			GUIItemImage flourIngredient = new GUIItemImage (0.05f, 0.05f, 0.05f, 0.075f);
			flourIngredient.Texture = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_WHITE_FLOUR);
			flourIngredient.Draggable = DraggableEnum.RAW_INGREDIENT;
			flourIngredient.DuplicateOnDrag = true;
			flourIngredient.Element = GUIElementEnum.IconFlour;
			flourIngredient.BuildableItem = BuildableItemEnum.White_Flour;
			enabledCheck = new GamePlayerStateCheck();
			enabledCheck.CanBuildItem = BuildableItemEnum.White_Flour;
			flourIngredient.EnabledCheck = enabledCheck;
			
			ingredientMenu.AddChild (flourIngredient);
			
			GUIItemImage ingredientShoppingCart = new GUIItemImage (0.15f, 0.15f, 0.15f, 0.15f);
			ingredientShoppingCart.Texture = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_SHOPPING_CART);
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