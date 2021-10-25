using UnityEngine;
using UnboundLib;
using Photon.Pun;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnboundLib.Utils;
using ZomC_Cards.Cards;
using HarmonyLib;
//Borrowed from PCE PacBullets
//A mono to change the Y position of a bullet
//Used by card Flappy Bullets

namespace ZomC_Cards.MonoBehaviours
{
    public class FlappyBulletsAssets
    {
        private static GameObject _flapBullet = null;

        internal static GameObject flapBullet
        {
            get
            {
                if (FlappyBulletsAssets._flapBullet != null) { return FlappyBulletsAssets._flapBullet; }
                else
                {
                    FlappyBulletsAssets._flapBullet = new GameObject("FlappyBullets", typeof(FlappyBulletEffect), typeof(PhotonView));
                    UnityEngine.GameObject.DontDestroyOnLoad(FlappyBulletsAssets._flapBullet);

                    return FlappyBulletsAssets._flapBullet;
                }
            }
            set { }
        }
    }
    public class FlappyMono : MonoBehaviour
    {
        private static bool Initialized = false;



        void Awake()
        {
            if (!Initialized)
            {
                PhotonNetwork.PrefabPool.RegisterPrefab(FlappyBulletsAssets.flapBullet.name, FlappyBulletsAssets.flapBullet);
            }
        }

        void Start()
        {
            if (!Initialized)
            {
                Initialized = true;
                return;
            }

            if (!PhotonNetwork.OfflineMode && !this.gameObject.transform.parent.GetComponent<ProjectileHit>().ownPlayer.data.view.IsMine) return;


            PhotonNetwork.Instantiate(
                FlappyBulletsAssets.flapBullet.name,
                transform.position,
                transform.rotation,
                0,
                new object[] { this.gameObject.transform.parent.GetComponent<PhotonView>().ViewID }
            );
        }
    }
    [RequireComponent(typeof(PhotonView))]
    public class FlappyBulletEffect : MonoBehaviour, IPunInstantiateMagicCallback
    {
        private PhotonView view;
        private Transform parent;
        private Player player;
        private Gun gun;
        private ProjectileHit projectile;
        private Camera mainCam;

        public void OnPhotonInstantiate(Photon.Pun.PhotonMessageInfo info)
        {
            object[] instantiationData = info.photonView.InstantiationData;

            GameObject parent = PhotonView.Find((int)instantiationData[0]).gameObject;

            this.gameObject.transform.SetParent(parent.transform);

            this.player = parent.GetComponent<ProjectileHit>().ownPlayer;
            this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
        }

        void Awake()
        {

        }
        void Start()
        {
            this.parent = this.gameObject.transform.parent;
            if (this.parent == null) { return; }
            this.projectile = this.gameObject.transform.parent.GetComponent<ProjectileHit>();
            this.view = this.gameObject.GetComponent<PhotonView>();

            this.mainCam = MainCam.instance.transform.GetComponent<Camera>();
        }
        void Update()
        {
            if (this.parent == null) { return; }

            Vector3 pos = this.mainCam.WorldToScreenPoint(this.transform.position);
            pos.x /= (float)Screen.width;
            pos.y /= (float)Screen.height;

            gun.ExecuteAfterSeconds((float).5, () =>
            {
                this.parent.transform.position = this.mainCam.ScreenToWorldPoint(new Vector3(pos.x, pos.y +10, pos.z));
            });
        }
    }
}
