namespace KS.PizzaEmpire.Unity
{	
	using UnityEngine;
	using Common.BusinessObjects;
	using Common.GameLogic;
	using System.Collections.Generic;

	public class DoWorkAfterColliding : MonoBehaviour 
	{
		public int CollidesRequired;
		public string CollideWith;
		public BuildableItemEnum ItemToBuild;

		protected int collideCount;
		protected GameObject parent;

		void Start () 
		{
			collideCount = 0;
		}
		
		void Update () 
		{
			if (collideCount > CollidesRequired)
			{
				ServerCommunicator.Instance.Communicate(
					ServerActionEnum.StartWork, (int)ItemToBuild,
					(ServerCommunication com) => 
					{
						GamePlayerLogic.Instance.StartWork(
							GamePlayerManager.Instance.LoggedInPlayer, ItemToBuild);
					}, GUIGameObject.SetGlobalError);

				Destroy(gameObject);
				Destroy(parent);

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
			if (other.tag == CollideWith) 
			{
				parent = other.gameObject;
				collideCount++;
			}
		}
	}
}