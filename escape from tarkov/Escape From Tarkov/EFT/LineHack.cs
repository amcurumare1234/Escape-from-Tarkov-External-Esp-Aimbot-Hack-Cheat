using System;
using System.Collections.Generic;
using System.Reflection;
using EFT.Interactive;
using UnityEngine;

namespace EFT
{
	public class LineHack : MonoBehaviour
	{
		public void Awake()
		{
			this._cam = Camera.main;
		}

		private void OnGUI()
		{
			//GUI.color = Color.red;
			//GUI.Label(new Rect(458f, 921f, 1400f, 50f), "Мой Y: " + this.myY.ToString());
			//GUI.Label(new Rect(458f, 943f, 1400f, 50f), "Противника Y: " + this.targetY.ToString());
			try
			{
				foreach (GameObject gameObject in LineHack.badguys)
				{
					foreach (Transform transform in gameObject.transform.GetComponentsInChildren<Transform>())
					{
						if (transform.name.Contains(LineHack.childName)) //отрисовываем линии только до тех объектов у которых в качестве чаелда висит объект с название childName
						{
							Vector3 vector = this._cam.WorldToScreenPoint(gameObject.transform.position);
							Vector3 vector2 = this._cam.WorldToScreenPoint(transform.position);
							if (vector.z > -1f)
							{
								float num = Vector3.Distance(this._cam.transform.position, gameObject.transform.position);
								Mathf.Abs(vector.y - vector2.y);
								//if (this.IsVisable(this._cam.gameObject, transform.gameObject))
								{
									//if (LineHack.LineESP)
									{
										LineHack.DrawLine(new Vector2((float)(Screen.width / 2), (float)Screen.height), new Vector2(vector.x, (float)Screen.height - vector.y), Color.green, LineHack.think, false); //линии от меня до врага
									}
									//if (LineHack.DistESP)
									{
										GUI.Label(new Rect((float)((int)vector.x - 5), (float)Screen.height - vector.y - 10f, 40f, 40f), ((int)num).ToString()); //отрисовка дистанции
									}
								}
								//else
								//{
								//	if (LineHack.LineESP)
								//	{
								//		LineHack.DrawLine(new Vector2((float)(Screen.width / 2), (float)Screen.height), new Vector2(vector.x, (float)Screen.height - vector.y), Color.red, LineHack.think, false);
								//	}
								//	if (LineHack.DistESP)
								//	{
								//		GUI.Label(new Rect((float)((int)vector2.x - 5), (float)Screen.height - vector2.y - 14f, 40f, 40f), ((int)num).ToString());
								//	}
								//}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		//private void FixedUpdate()
		//{
		//	if (Input.GetKeyDown(KeyCode.LeftArrow))
		//	{
		//		this.myY -= 0.1f;
		//	}
		//	if (Input.GetKeyDown(KeyCode.RightArrow))
		//	{
		//		this.myY += 0.1f;
		//	}
		//	if (Input.GetKeyDown(KeyCode.UpArrow))
		//	{
		//		this.targetY += 0.1f;
		//	}
		//	if (Input.GetKeyDown(KeyCode.DownArrow))
		//	{
		//		this.targetY -= 0.1f;
		//	}
		//}

		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
		{
			float num = pointB.x - pointA.x;
			float num2 = pointB.y - pointA.y;
			float num3 = Mathf.Sqrt(num * num + num2 * num2);
			if (num3 < 0.001f)
			{
				return;
			}
			Texture2D image;
			if (antiAlias)
			{
				width *= 3f;
				image = LineHack.aaLineTex;
				Material material = LineHack.blendMaterial;
			}
			else
			{
				image = LineHack.lineTex;
				Material material2 = LineHack.blitMaterial;
			}
			float num4 = width * num2 / num3;
			float num5 = width * num / num3;
			Matrix4x4 identity = Matrix4x4.identity;
			identity.m00 = num;
			identity.m01 = -num4;
			identity.m03 = pointA.x + 0.5f * num4;
			identity.m10 = num2;
			identity.m11 = num5;
			identity.m13 = pointA.y - 0.5f * num5;
			GL.PushMatrix();
			GL.MultMatrix(identity);
			GUI.color = color;
			GUI.DrawTexture(LineHack.lineRect, image);
			GL.PopMatrix();
		}

		private void Start()
		{
			if (LineHack.lineTex == null)
			{
				LineHack.lineTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
				LineHack.lineTex.SetPixel(0, 1, Color.white);
				LineHack.lineTex.Apply();
			}
			if (LineHack.aaLineTex == null)
			{
				LineHack.aaLineTex = new Texture2D(1, 3, TextureFormat.ARGB32, false);
				LineHack.aaLineTex.SetPixel(0, 0, new Color(1f, 1f, 1f, 0f));
				LineHack.aaLineTex.SetPixel(0, 1, Color.white);
				LineHack.aaLineTex.SetPixel(0, 2, new Color(1f, 1f, 1f, 0f));
				LineHack.aaLineTex.Apply();
			}
			LineHack.blitMaterial = (Material)typeof(GUI).GetMethod("get_blitMaterial", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
			LineHack.blendMaterial = (Material)typeof(GUI).GetMethod("get_blendMaterial", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
		}

		private void FixedUpdate()
		{ // обновляем список врагов раз в two = 50 (чем меньше число тем чаще обновляется список и тем сильнее нагрузка на ПК)
			try
			{
				if (this.one <= this.two)
				{
					this.one--;
					if (this.one == this.two - 1)
					{
						foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType(typeof(GameObject))) //все объекты на сцене
						{
							if (gameObject.name.Contains(LineHack.nameToAdd))
							{
								Vector3.Distance(this._cam.transform.position, gameObject.transform.position);
								if (!LineHack.badguys.Contains(gameObject) && !gameObject.GetComponent<Corpse>())
								{
									LineHack.badguys.Add(gameObject);
								}
							}
						}
					}
				}
				if (this.one <= 0)
				{
					LineHack.badguys.Clear();
					this.one = this.two;
				}
			}
			catch
			{
			}
		}

		//private bool IsVisable(GameObject origin, GameObject toCheck)
		//{
		//	Vector3 end = new Vector3(toCheck.transform.position.x, toCheck.transform.position.y + this.targetY, toCheck.transform.position.z);
		//	new Vector3(origin.transform.position.x, origin.transform.position.y + this.myY, origin.transform.position.z);
		//	return Physics.Linecast(origin.transform.position, end, out this._hit) && this._hit.transform.name.Contains(LineHack.nameToAdd);
		//}

		private Camera _cam;

		private RaycastHit _hit;

		public int one;

		public int two = 50;

		public static List<GameObject> badguys = new List<GameObject>();

		public static float think = 2f;

		//public static bool LineESP = true;

		//public static bool DistESP = true;

		private static Texture2D aaLineTex = null;

		private static Texture2D lineTex = null;

		private static Material blitMaterial = null;

		private static Material blendMaterial = null;

		private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);

		public static string nameToAdd = "Bot";

		public static string childName = "Base HumanHead"; //часть бота

		//private float myY;

		//private float targetY;
	}
}
