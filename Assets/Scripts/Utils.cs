using UnityEngine;
using System.Collections;

public abstract class Utils {

	public static bool ExistStringInArray(string[] array, string s)
	{
		for(int i=0; i < array.Length; i++)
		{
			if(array[i] == s)
				return true;
		}

		return false;
	}

	public static void IgnoreCollisions(GameObject obj1, GameObject obj2)
	{
		foreach(Collider2D collider1 in obj1.GetComponents<Collider2D>())
		{
			foreach(Collider2D collider2 in obj2.GetComponents<Collider2D>())
			{
				if(!collider1.isTrigger && !collider2.isTrigger)
					Physics2D.IgnoreCollision(collider1, collider2);
			}
		}
	}
}
