using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EFT.Interactive;
using UnityEngine;

namespace EFT
{
    public class Aimbot : MonoBehaviour
    {

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo); //непосредственно для работы аима
        
        public void Awake()
        {
            this._cam = Camera.main;
        }
        
        private void OnGUI()
        {
            GUI.color = Color.yellow;
            GUI.Label(new Rect(458f, 877f, 1400f, 50f), "Fov аимбота: " + this.fov / 3, 7478.ToString());
            GUI.Label(new Rect(458f, 899f, 1400f, 50f), "Гладкость имбота: " + this.smooth.ToString());
        }
        
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                this.fov++;
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                this.fov--;
            }
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                this.smooth++;
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                this.smooth--;
            }
        }
        
        private void FixedUpdate()
        { //поиск врагов на сцене
            try
            {
                if (this.one <= this.two)
                {
                    this.one--;
                    if (this.one == this.two - 1)
                    {
                        foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
                        {
                            if (gameObject.name.Contains(nameToAdd))
                            {
                                Vector3.Distance(this._cam.transform.position, gameObject.transform.position);
                                if (!badguys.Contains(gameObject) && !gameObject.GetComponent<Corpse>())
                                {
                                    badguys.Add(gameObject);
                                }
                            }
                        }
                    }
                }
                if (this.one <= 0)
                {
                    badguys.Clear();
                    this.one = this.two;
                }
            }
            catch
            {
            }
            float num = 99999f;
            Vector2 vector = Vector2.zero;
            try
            {
                foreach (GameObject gameObject2 in badguys)
                {
                    foreach (Transform transform in gameObject2.transform.GetComponentsInChildren<Transform>()) //перебор чаелдов на врагах
                    {
                        if (transform.name.Contains(childName)) //получаем конкретный чаелд отвечающий за голову
                        {
                            Vector3 vector2 = this._cam.WorldToScreenPoint(transform.transform.position);
                            if (vector2.z > -8f)
                            {
                                float num2 = Math.Abs(Vector2.Distance(new Vector2(vector2.x, (float)Screen.height - vector2.y), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2))));
                                if (num2 < (float)this.fov && num2 < num)
                                {
                                    num = num2;
                                    vector = new Vector2(vector2.x, (float)Screen.height - vector2.y);
                                }
                            }
                        }
                    }
                }
                if (vector != Vector2.zero)
                {
                    double num3 = (double)(vector.x - (float)Screen.width / 2f);
                    double num4 = (double)(vector.y - (float)Screen.height / 2f);
                    num3 /= (double)this.smooth;
                    num4 /= (double)this.smooth;
                    if (Input.GetKey(KeyCode.RightArrow)) //кнопка аимбота (можно поменять)
                    {
                        mouse_event(1, (int)num3, (int)num4, 0, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private static string nameToAdd = "Bot";

        private static string childName = "Base HumanHead"; //башка в качестве чаелда

        private static List<GameObject> badguys = new List<GameObject>();

        private int one;

        private int two = 50;

        private int smooth = 16; //плавность перемещения курсора на врагов

        private int fov = 100; //угол в районе которого курсор мыши будет цыплятся за врагов а точнее fov/3.7478 это наш угол

        private Camera _cam;
    }
}
