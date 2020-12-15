using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level : MonoBehaviour
{
	public static int Levels = 1; 
	[SerializeField]
	Sprite open; 
	[SerializeField]
	Sprite closed; 

	void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{ 
			if (i < Levels)
			{ 
				transform.GetChild(i).GetComponent<Image>().sprite = open;
				transform.GetChild(i).GetComponent<Button>().interactable = true; 
			}
			else
			{ 
				transform.GetChild(i).GetComponent<Image>().sprite = closed; 
				transform.GetChild(i).GetComponent<Button>().interactable = false; 
			}
		}
	}
}



