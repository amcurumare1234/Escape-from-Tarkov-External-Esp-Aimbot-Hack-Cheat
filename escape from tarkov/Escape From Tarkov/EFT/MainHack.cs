using System;
using EFT.Interactive;
using UnityEngine;

namespace EFT
{

	public class MainHack : MonoBehaviour
	{

		public void Awake()
		{
			this._botSpawn.x = 0f;
			this._botSpawn.y = 0f;
			this._botSpawn.z = 0f;
			this._cam = Camera.main;
		}

        private void BotTP() // метод телепорта Ботов
        {
            if (this._botCount > 0)
            {
                this._bots[this._botCount - 1].transform.position = this._botSpawn;
                this._botCount--;
            }
        }

        private void GetBots() //метод поиска Ботов на сцене
        { //На живых ботах висят скрипты - BotOwner, Player и объекты-боты имеют в названии слово "Bot". Скрипт Corpse добавляется ко всем остальным скриптам на боте когда бот "умирает".
            int num = 0;
            this.GetAllObj();
            for (int i = 0; i < this._allObj.Length; i++)
            {
                if (this._allObj[i].transform.GetComponent<BotOwner>() && !this._allObj[i].transform.GetComponent<Corpse>() && this._allObj[i].transform.GetComponent<Player>() && this._allObj[i].transform.name.Contains("Bot"))
                {
                    this._bots[num] = this._allObj[i];
                    num++;
                }
            }
            this._botCount = num;
        }

        private void GetObj() // метод получения компонентов объекта который находится напротив курсора мыши на растоянии 2000
        {
            this._ray = this._cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(this._ray, out this._hit, 2000f);
            if (this._hit.transform != null)
            {
                this._objComp = "Компоненты GameObject'а :";
                Component[] components = this._hit.transform.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    this._objComp = this._objComp + " [ " + components[i].ToString() + " ] ";
                }
                return;
            }
            this._objComp = "";
        }

        private void OnGUI()
        {
            GUI.color = Color.red;
            GUI.Label(new Rect((float)Screen.width / 2f, (float)Screen.height / 2f, 120f, 120f), "+"); //отображение бесполезного плюсика в центре экрана
            GUI.color = Color.yellow;
            GUI.Label(new Rect(458f, 921f, 1400f, 50f), "Координаты персонажа: " + this._playerCoordinates.ToString()); //вывод координат персонажа
            GUI.Label(new Rect(458f, 943f, 1400f, 50f), "Количество Ботов на карте: " + this._botCount.ToString()); //вывод количества ботов на сцене
            GUI.Label(new Rect(458f, 965f, 1400f, 50f), "Координаты телепорта Ботов: " + this._botSpawn.ToString()); //вывод координат куда будут телепортироваться боты
            GUI.Label(new Rect(458f, 987f, 1400f, 150f), this._objComp); //вывод компонентов объекта
        }

        private void GetAllObj() //метод получения всех оюъектов на сцене
        {
            this._allObj = (UnityEngine.Object.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]);
        }

        private void FixedUpdate()
        {
            this._playerCoordinates = this._cam.transform.localPosition;
        }

        private void Update()
		{
			if (Input.GetKeyDown(KeyCode.KeypadPeriod))
			{
				this.GetObj();
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				this.BotTP();
			}
			if (Input.GetKeyDown(KeyCode.Keypad3))
			{
				this.GetBots();
			}
			if (Input.GetKeyDown(KeyCode.Keypad0))
			{
				this._botSpawn = new Vector3(0f, 0f, 0f);
				this._objComp = "";
				this._botCount = 0;
			}
			if (Input.GetKeyDown(KeyCode.Keypad7))
			{
				this._botSpawn.x = this._botSpawn.x + 1f;
			}
			if (Input.GetKeyDown(KeyCode.Keypad4))
			{
				this._botSpawn.x = this._botSpawn.x - 1f;
			}
			if (Input.GetKeyDown(KeyCode.Keypad8))
			{
				this._botSpawn.y = this._botSpawn.y + 1f;
			}
			if (Input.GetKeyDown(KeyCode.Keypad5))
			{
				this._botSpawn.y = this._botSpawn.y - 1f;
			}
			if (Input.GetKeyDown(KeyCode.Keypad9))
			{
				this._botSpawn.z = this._botSpawn.z + 1f;
			}
			if (Input.GetKeyDown(KeyCode.Keypad6))
			{
				this._botSpawn.z = this._botSpawn.z - 1f;
			}
			if (Input.GetKeyDown(KeyCode.KeypadPlus))
			{
				this._botSpawn = this._playerCoordinates;
			}
		}

		private Camera _cam;

		private RaycastHit _hit;

		private string _objComp;

		private Vector3 _playerCoordinates;

		private Vector3 _botSpawn;

		private GameObject[] _bots = new GameObject[100];

		private GameObject[] _allObj;

		public int _botCount;

		private Ray _ray;
	}
}
