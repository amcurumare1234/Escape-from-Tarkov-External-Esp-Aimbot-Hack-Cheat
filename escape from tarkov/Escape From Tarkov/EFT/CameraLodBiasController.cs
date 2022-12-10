using System;
using EFT.Interactive;
using UnityEngine;

namespace EFT.CameraControl
{
	public class CameraLodBiasController : MonoBehaviour
	{
        //public void Awake()
        //{
        //    this.float_0 = base.GetComponent<Camera>().fieldOfView;
        //}

        //public void SetMaxFov(float fov)
        //{
        //	this.float_0 = fov;
        //}

        //public void SetBiasByFov(float fov)
        //{
        //	this.LodBiasFactor = Mathf.Tan(0.017453292f * fov * 0.5f) / Mathf.Tan(0.017453292f * this.float_0 * 0.5f);
        //}

        //private void OnPreCull()
        //{
        //	this.float_1 = QualitySettings.lodBias;
        //	QualitySettings.lodBias = this.float_1 * this.LodBiasFactor;
        //}

        //private void OnPostRender()
        //{
        //	QualitySettings.lodBias = this.float_1;
        //}

        //public CameraLodBiasController()
        //{
        //}

        // Token: 0x0600787B RID: 30843 RVA: 0x002E8CA8 File Offset: 0x002E6EA8
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                this.LineHackDestroy();
                this.LineHackLoad();
            }
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                this.LineHackDestroy();
            }
            if (Input.GetKeyDown(KeyCode.KeypadDivide))
            {
                this.MainHackDestroy();
                this.MainHackLoad();
            }
            if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            {
                this.MainHackDestroy();
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                this.AimBotDestroy();
                this.AimBotLoad();
            }
            if (Input.GetKeyDown(KeyCode.End))
            {
                this.AimBotDestroy();
            }
        }

        private void LineHackLoad()
        {
            load_LineHack = new GameObject();
            load_LineHack.AddComponent<LineHack>();
            UnityEngine.Object.DontDestroyOnLoad(load_LineHack);
        }

        private void LineHackDestroy()
        {
            UnityEngine.Object.Destroy(load_LineHack);
        }

        private void MainHackLoad()
        {
            load_MainHack = new GameObject();
            load_MainHack.AddComponent<MainHack>();
            UnityEngine.Object.DontDestroyOnLoad(load_MainHack);
        }

        private void MainHackDestroy()
        {
            UnityEngine.Object.Destroy(load_MainHack);
        }

        private void AimBotLoad()
        {
            load_AimBot = new GameObject();
            load_AimBot.AddComponent<Aimbot>();
            UnityEngine.Object.DontDestroyOnLoad(load_AimBot);
        }
        
        private void AimBotDestroy()
        {
            UnityEngine.Object.Destroy(load_AimBot);
        }

        //[SerializeField]
        //public float LodBiasFactor = 1f;

        //private float float_0 = 75f;

        //private float float_1 = 1f;
        
        public static GameObject load_LineHack;
        
        public static GameObject load_MainHack;
        
        public static GameObject load_AimBot;
    }
}
