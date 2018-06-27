using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autorotator : MonoBehaviour
{
	Vector2 lastPosition;

	void Awake()
	{
		lastPosition = transform.position;
	}

	void Update()
	{
		transform.up = ((Vector2)transform.position - lastPosition).normalized;
		lastPosition = transform.position;
	}
}
