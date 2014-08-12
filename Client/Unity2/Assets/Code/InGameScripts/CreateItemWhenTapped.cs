namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.GameLogic;

	public class CreateItemWhenTapped : MonoBehaviour
	{
		public ResourceEnum ItemToCreate;

		protected bool isTapped;
		protected GameObject createdItem;

		public void Tapped()
		{
			if (isTapped)
			{
				UnTapped();
				return;
			}

			isTapped = true;

			createdItem = ResourceManager<GameObject>.Instance.Load(ItemToCreate);			
			createdItem = (GameObject)GameObject.Instantiate(createdItem, transform.position, Quaternion.identity);

			Debug.Log ("Tapped!");

			GUIStateManager.Instance.TapHandled(this.gameObject, GUIElementEnum.None);
		}

		public void UnTapped()
		{
			Debug.Log ("UnTapped!");

			if (createdItem != null) 
			{
				DestroyImmediate(createdItem);
			}

			isTapped = false;
		}
	}
}
