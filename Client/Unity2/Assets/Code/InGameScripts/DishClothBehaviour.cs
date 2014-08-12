namespace KS.PizzaEmpire.Unity
{	
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	using System.Collections.Generic;

	public class DishClothBehaviour : MonoBehaviour 
	{
		protected int wipeCount;
		public int WipesRequired;
		protected GameObject dirtyTable;

		void Start () 
		{
			wipeCount = 0;
		}
		
		void Update () 
		{
			if (wipeCount > WipesRequired)
			{
				ServerCommunicator.Instance.Communicate(
					ServerActionEnum.StartWork, (int)BuildableItemEnum.Dirty_Dishes,
					(ServerCommunication com) => 
					{
						GamePlayerLogic.Instance.StartWork(OrderIngredientsWindow.Player, BuildableItemEnum.Dirty_Dishes);
					}, GUIGameObject.SetGlobalError);

				Destroy(gameObject);
				Destroy(dirtyTable);

				return;
			}

			if (Input.GetMouseButton(0)) 
			{
				Vector3 mousePosition = Input.mousePosition;
				mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
				Vector3 position = transform.position;
				position.x = mousePosition.x;
				position.y = mousePosition.y;
				transform.position = position;
			}
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag == "DirtyTable") 
			{
				dirtyTable = other.gameObject;
				wipeCount++;
			}
		}
	}
}