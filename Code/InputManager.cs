using UnityEngine;
using System.Collections;





public class InputManager : MonoBehaviour
{


    void Update()
    {
		CheckForClose();
    }



    void CheckForClose()
	{
		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}
}
