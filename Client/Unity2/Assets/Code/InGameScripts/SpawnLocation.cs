namespace KS.PizzaEmpire.Unity
{
	using UnityEngine;
	
	public class SpawnLocation : MonoBehaviour 
	{
		public Vector2 Location;

		void Start()
		{
			Vector3 position = transform.position;
			position.x = Location.x;
			position.y = Location.y;
			transform.position = position;
		}
	}
}
