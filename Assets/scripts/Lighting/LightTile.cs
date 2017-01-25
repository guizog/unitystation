using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lighting
{
	public class LightTile : MonoBehaviour
	{

		public SpriteRenderer thisSprite{get; set;}

		void Awake()
		{
			thisSprite = GetComponentInChildren<SpriteRenderer>();
		}
	}
}