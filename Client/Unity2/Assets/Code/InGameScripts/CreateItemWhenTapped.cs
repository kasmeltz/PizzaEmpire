namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using Common.BusinessObjects;
	using Common.GameLogic;

	public class CreateItemWhenTapped : Tappable
	{
		public ResourceEnum ItemToCreate;

		protected GameObject createdItem;

		public override void Tap()
		{
			base.Tap();

			Debug.Log ("CreateItemWhenTapped Tap!");

			if (isTapped)
			{
				return;
			}

			isTapped = true;

			createdItem = ResourceManager<GameObject>.Instance.Load(ItemToCreate);			
			createdItem = (GameObject)GameObject.Instantiate(createdItem, transform.position, Quaternion.identity);
		}

		public override void UnTap()
		{
			Debug.Log ("CreateItemWhenTapped UnTap!");

			base.UnTap ();

			if (createdItem != null) 
			{
				DestroyImmediate(createdItem);
			}

			isTapped = false;
		}
	}
}
