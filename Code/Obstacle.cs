using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Obstacle : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		//if the player hits one obstacle, it's game over

		// if (col.gameObject.tag == Constants.PlayerTag)
		// {
		// 	Game.Instance.Die();
		// }
	}
}
