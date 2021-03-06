﻿using UnityEngine;
using System.Collections;

namespace BSS {
	public static class DrawUtils { 
		private static Texture2D  _whiteTexture ; 
		public static Texture2D WhiteTexture { 
			get { 
				if (_whiteTexture == null) { 
					_whiteTexture = new Texture2D (1, 1);
					_whiteTexture.SetPixel (0, 0, Color.white); 
					_whiteTexture.Apply (); 
				}
				return _whiteTexture; 
			} 
		}
		public static void DrawCircle(int cx, int cy, int r, Color col)
		{
			var tex= new Texture2D (1, 1);
			int x, y, px, nx, py, ny, d;

			for (x = 0; x <= r; x++)
			{
				d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
				for (y = 0; y <= d; y++)
				{
					px = cx + x;
					nx = cx - x;
					py = cy + y;
					ny = cy - y;

					tex.SetPixel(px, py, col);
					tex.SetPixel(nx, py, col);

					tex.SetPixel(px, ny, col);
					tex.SetPixel(nx, ny, col);
				}
			}    
		}
		public static Bounds GetViewportBounds (Camera camera,Vector3 screenPosition1,Vector3 screenPosition2) 
		{ 
			var v1 = Camera . main . ScreenToViewportPoint (screenPosition1); 
			var v2 = Camera . main . ScreenToViewportPoint (screenPosition2); 
			var min = Vector3 . Min (v1 ,v2); 
			var max = Vector3 . Max (v1 ,v2); 
			min.z = camera.nearClipPlane; 
			max.z = camera.farClipPlane;

            var bounds = new Bounds(); 
			bounds.SetMinMax(min,max);
			return bounds ; 
		}
		public static void DrawScreenRect(Rect rect,Color color) { 
			GUI.color = color; 
			GUI.DrawTexture(rect, WhiteTexture) ; 
			GUI.color = Color.white; 
		} 

		public static void DrawScreenRectBorder(Rect rect, float thickness, Color color) { 
			//Top
			DrawScreenRect(new Rect(rect.xMin,rect.yMin,rect.width,thickness),color);
			//Left
			DrawScreenRect(new Rect(rect.xMin,rect.yMin,thickness,rect.height),color);
			//Right
			DrawScreenRect(new Rect(rect.xMax-thickness,rect.yMin,thickness,rect.height),color);
			//Bottom
			DrawScreenRect(new Rect(rect.xMin,rect.yMax-thickness,rect.width,thickness),color);
		}
		static public Rect GetWorldRect (RectTransform rt, Vector2 scale) {
			// Convert the rectangle to world corners and grab the top left
			Vector3[] corners = new Vector3[4];
			rt.GetWorldCorners(corners);
			Vector3 topLeft = corners[0];

			// Rescale the size appropriately based on the current Canvas scale
			Vector2 scaledSize = new Vector2(scale.x * rt.rect.size.x, scale.y * rt.rect.size.y);

			return new Rect(topLeft, scaledSize);
		}
	
		public static Rect GetScreenRect(Vector3 screenPosition1,Vector3 screenPosition2)
		{
			//Moveoriginfrombottomlefttotopleft
			screenPosition1.y=Screen.height-screenPosition1.y;
			screenPosition2.y=Screen.height-screenPosition2.y;
			//Calculatecorners
			var topLeft=Vector3.Min(screenPosition1,screenPosition2);
			var bottomRight=Vector3.Max(screenPosition1,screenPosition2);
			//CreateRect
			return Rect.MinMaxRect(topLeft.x,topLeft.y,bottomRight.x,bottomRight.y);
		}
		public static Rect GetScreenRect(Vector2 screenPosition1,Vector2 screenPosition2)
		{
			//Moveoriginfrombottomlefttotopleft
			screenPosition1.y=Screen.height-screenPosition1.y;
			screenPosition2.y=Screen.height-screenPosition2.y;
			//Calculatecorners
			var topLeft=Vector2.Min(screenPosition1,screenPosition2);
			var bottomRight=Vector2.Max(screenPosition1,screenPosition2);
			//CreateRect
			return Rect.MinMaxRect(topLeft.x,topLeft.y,bottomRight.x,bottomRight.y);
		}
		public static void DrawCustomRect(Vector3 screenPosition1,Vector3 screenPosition2){
			var rect=GetScreenRect(screenPosition1,screenPosition2);
			DrawScreenRect(rect,new Color(0.8f,0.8f,0.95f,0.25f));
			DrawScreenRectBorder(rect,2,new Color(0.8f,0.8f,0.95f));
		}

	}
}

