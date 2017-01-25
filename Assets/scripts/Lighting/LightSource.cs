using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lighting
{
	public class LightSource : MonoBehaviour
	{

		private SpriteRenderer thisSprite;

		void Awake()
		{
			thisSprite = GetComponentInChildren<SpriteRenderer>();
		}
	}
}