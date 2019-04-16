using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TouchController : MonoBehaviour
{
	static TouchController instance;

	public static TouchController Instance
	{
		get { return instance; }
	}
	
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	
	
	void Update()
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch(i).phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				RaycastHit hit = new RaycastHit();
				
				if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Cave")) && Time.timeScale == 1)
				{

					if (hit.collider.gameObject.CompareTag("cave1")||hit.collider.gameObject.CompareTag("cave2"))
					{
						hit.collider.gameObject.GetComponent<Cave>()._Touch();
					}
				}
			}
		}
	}
}
