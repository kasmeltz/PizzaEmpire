namespace KS.PizzaEmpire.Unity
{	
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	using System.Collections.Generic;
	
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
			
			IngredientOrderWindow window = new IngredientOrderWindow(player);		
			window.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_WIN_ORDER_INGREDIENT);			
			window.SetRectangle(0.0f, 0.0f, 0.40f, 0.85f, false, ScaleMode.ScaleToFit);
			window.Element = GUIElementEnum.OrderIngredientsWindow;
			window.Visible = false;			
			
			GUIStateManager.Instance.AddChild(window);
			
			GUIBox flourBox = GUIItemFactory<GUIBox>.Instance.Pool.New();
			flourBox.Content.text = "Flour";
			flourBox.SetRectangle(0.02f, 0.12f,  0.15f, 0.15f, false, ScaleMode.ScaleToFit);
			flourBox.Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
			flourBox.Element = GUIElementEnum.BoxFlour;
			enabledCheck = new GamePlayerStateCheck();
			enabledCheck.CanBuildItem = BuildableItemEnum.White_Flour;
			flourBox.EnabledCheck = enabledCheck;
			
			window.AddChild (flourBox);
			
			GUIBox flourOrderBox = GUIItemFactory<GUIBox>.Instance.Pool.New();
			flourOrderBox.Content.text = "";
			flourOrderBox.SetRectangle(0.02f, 0.25f,  0.15f, 0.05f, false, ScaleMode.ScaleToFit);
			flourOrderBox.Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_BASIC_STYLE);
			flourOrderBox.Element = GUIElementEnum.ProgressBar;
			
			window.AddChild (flourOrderBox);			
			
			GUIItemImage flourIngredient = GUIItemFactory<GUIItemImage>.Instance.Pool.New();
			flourIngredient.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_WHITE_FLOUR);			
			flourIngredient.SetRectangle(0.18f, 0.12f, 0.15f, 0.15f, false, ScaleMode.ScaleToFit);
			flourIngredient.Draggable = DraggableEnum.RAW_INGREDIENT;
			flourIngredient.DuplicateOnDrag = true;
			flourIngredient.Element = GUIElementEnum.IconFlour;
			flourIngredient.BuildableItem = BuildableItemEnum.White_Flour;
			enabledCheck = new GamePlayerStateCheck();
			enabledCheck.CanBuildItem = BuildableItemEnum.White_Flour;
			flourIngredient.EnabledCheck = enabledCheck;
			
			window.AddChild (flourIngredient);
			
			GUIItemImage ingredientShoppingCart = GUIItemFactory<GUIItemImage>.Instance.Pool.New();
			ingredientShoppingCart.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_SHOPPING_CART);			
			ingredientShoppingCart.SetRectangle(0.0f, 0.73f, 0.12f, 0.12f, false, ScaleMode.ScaleToFit);
			ingredientShoppingCart.Droppable = DraggableEnum.RAW_INGREDIENT;
			ingredientShoppingCart.Element = GUIElementEnum.IconShoppingCart;
			ingredientShoppingCart.OnDrop = (i1, i2) =>
			{				
				if (i2.BuildableItem != BuildableItemEnum.None)
				{
					/*
					IngredientOrderWindow iow = GUIStateManager.Instance
						.GetChild(GUIElementEnum.OrderIngredientsWindow)
							as IngredientOrderWindow;

					List<ItemQuantity> currentOrder = iow.CurrentOrder;
										
					ItemQuantity selected = null;
					foreach(ItemQuantity iq in currentOrder)
					{
						if (iq.ItemCode == i2.BuildableItem)
						{
							iq.Quantity++;
							selected = iq;
							break;
						}
					}
					
					if (selected == null)
					{
						selected = new ItemQuantity { ItemCode = i2.BuildableItem, Quantity = 1 };
						currentOrder.Add(selected);
					}
					
					GUIBox orderBox = i2.Parent.GetChild(GUIElementEnum.ProgressBar) as GUIBox;
					orderBox.Content.text = selected.Quantity.ToString();					
					*/
					ServerCommunicator.Instance.Communicate(
						ServerActionEnum.StartWork, (int)i2.BuildableItem,
						(ServerCommunication com) => 
						{
							GamePlayerLogic.Instance.StartWork(OrderIngredientsWindow.Player, i2.BuildableItem);
						}, GUIGameObject.SetGlobalError);
				}
			};
			
			window.AddChild (ingredientShoppingCart);		
			
			GUIButton ingredientCheckMark = GUIItemFactory<GUIButton>.Instance.Pool.New();
			ingredientCheckMark.Content.image = 
				ResourceManager<Texture2D>.Instance.Load(ResourceEnum.TEXTURE_ICON_CHECKMARK);			
			ingredientCheckMark.SetRectangle(0.21f, 0.73f, 0.12f, 0.12f, false, ScaleMode.ScaleToFit);
			ingredientCheckMark.Style = LightweightResourceManager<GUIStyle>.Instance.Get(ResourceEnum.GUISTYLE_NO_BACKGROUND);
			
			window.AddChild (ingredientCheckMark);			
		}
	}
}