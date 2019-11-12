using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Garbage : MonoBehaviour
{

	void Start()
	{
		Invoke("DestroyObject", LifeTime);
	}

	void DestroyObject()
	{
		if (Game.Instance.GameState != GameState.Dead)
			Destroy(gameObject);
	}

	public float LifeTime = 10f;
}
