﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lighting
{
	public class LightingRoom : MonoBehaviour
	{

		public LightingTileManager tileManager { get; set; }

		public LightingSourceManager sourceManager { get; set; }

		/// <summary>
		/// x = left
		/// y = top
		/// z = right
		/// w = down
		/// </summary>
		/// <value>The bounds of the LightTile Group in the room.</value>
		public Vector4 bounds {
			get {
				if (tileManager != null) {
					return tileManager.bounds;
				} else {
					return Vector4.zero;
				}
					
			}
		}

		void Awake()
		{
			tileManager = GetComponentInChildren<LightingTileManager>();
			sourceManager = GetComponentInChildren<LightingSourceManager>();
		}

		void Start(){
			Invoke("PrintBounds", 1f);
		}

		void PrintBounds(){
			Debug.Log("LIGHTING: Bounds calc for " + gameObject.name + ": " + bounds);
		}


	}
}
