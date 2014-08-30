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

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.tag == CollideWith) 
			{
				parent = other.gameObject;
				collideCount++;
			}

            if (collideCount > CollidesRequired)
            {
                ServerCommunicator.Instance.Communicate(
                    ServerActionEnum.StartWork, (int)ItemToBuild,
                    (ServerCommunication com) =>
                    {
                        GamePlayerLogic.Instance.StartWork(
                            GamePlayerManager.Instance.LoggedInPlayer, 0, 0, ItemToBuild);
                    }, GUIGameObject.SetGlobalError);

                Destroy(gameObject);
                Destroy(parent);

                return;
            }
		}
	}
}