namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	
	public class DropOrSnapBack : Droppable
	{
		public override void Drop(Vector3 screenPosition)
		{
			base.Drop(screenPosition);

			screenPosition = Camera.main.ScreenToWorldPoint(screenPosition);
			if (screenPosition.x > DropArea.x && screenPosition.y > DropArea.y &&
				screenPosition.x < DropArea.x + DropArea.width && screenPosition.y < DropArea.y + DropArea.height) 
			{

			}
			else 
			{
				SpawnLocation spawnLocation = GetComponent<SpawnLocation> ();
				if (spawnLocation != null)
				{
					Vector3 position = transform.position;
					position.x = spawnLocation.Location.x;
					position.y = spawnLocation.Location.y;
					transform.position = position;
				}
			}
		}
	}
}
