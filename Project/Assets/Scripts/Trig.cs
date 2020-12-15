using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trig : MonoBehaviour
{

	public string player; 
	void OnTriggerEnter2D(Collider2D col)
	{ 
		if (col.tag == player)
		{ 
			{
				if (SceneManager.GetActiveScene().buildIndex == level.Levels)
				{ 
					level.Levels++; 
				}
				Debug.Log("YPA!!!");
				SceneManager.LoadScene(3);   
			}
		}
		
		if (Input.GetKeyDown(KeyCode.P))
		{ 
			Debug.Log("NO....");           
			SceneManager.LoadScene(5);     
		}

	}
}