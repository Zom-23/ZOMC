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
//A mono to create a new gun that fires along side the original gun
//Used by card Double Vision


namespace ZomC_Cards.MonoBehaviours
{
    
    public class DoubleViAssets
    {
        private static GameObject _doubleVi = null;

        internal static GameObject doubleVi
        {
            get
            {
                if(DoubleViAssets._doubleVi != null) { return DoubleViAssets._doubleVi; }
                else
                {
                    DoubleViAssets._doubleVi = new GameObject("ZOMC_DoubleVision", typeof(DoubleViEffect), typeof(PhotonView));
                    UnityEngine.GameObject.DontDestroyOnLoad(DoubleViAssets._doubleVi);

                    return DoubleViAssets._doubleVi;
                }
            }
            set { }
        }
    }
    public class DoubleViSpawner : MonoBehaviour
    {
        private static bool Initialized = false;



        void Awake()
        {
            if (!Initialized)
            {
                PhotonNetwork.PrefabPool.RegisterPrefab(DoubleViAssets.doubleVi.name, DoubleViAssets.doubleVi);
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
                DoubleViAssets.doubleVi.name,
                transform.position,
                transform.rotation,
                0,
                new object[] { this.gameObject.transform.parent.GetComponent<PhotonView>().ViewID }
            );
        }
    }
    [RequireComponent(typeof(PhotonView))]
    public class DoubleViEffect : MonoBehaviour, IPunInstantiateMagicCallback
    {
        System.Random random = new System.Random();
        private Player player;
        private Gun gun;
        private Gun newGun;
        private ProjectileHit projectile;
        private int layersToAdd = 0;

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
            // get the projectile, player, and gun this is attached to
            this.projectile = this.gameObject.transform.parent.GetComponent<ProjectileHit>();
            this.player = this.projectile.ownPlayer;
            this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();

            this.layersToAdd = (this.player.data.currentCards.Where(card => card.cardName == "Double Vision").Count()) * this.gun.projectiles.Count();

            // create a new gun for the spawnbulletseffect
            this.newGun = this.player.gameObject.AddComponent<DoubleViGun>();

            SpawnBulletsEffect effect = this.player.gameObject.AddComponent<SpawnBulletsEffect>();
            // set the position and direction to fire

            effect.SetDirection(((Quaternion)typeof(Gun).InvokeMember("getShootRotation",
                BindingFlags.Instance | BindingFlags.InvokeMethod |
                BindingFlags.NonPublic, null, this.gun, new object[] { 0, 0, 0f })) * Vector3.forward);

            List<Vector3> positions = new List<Vector3>() { };
            for (int b = 0; b < (this.layersToAdd); b++)
            {
                positions.Add(this.projectile.transform.position + (0.25f) * ((b % 2 == 0) ? (float)b : -((float)b + 1f)) * this.projectile.transform.right);
            }

            effect.SetPositions(positions);
            effect.SetNumBullets(this.layersToAdd);
            effect.SetTimeBetweenShots(0f);
            effect.SetInitialDelay(0f);

            // copy gun stats over
            float sizeM = (float)(random.NextDouble() + 1);
            float sizeM2 = (float)(random.NextDouble() + 1);
            SpawnBulletsEffect.CopyGunStats(this.gun, newGun);
            newGun.objectsToSpawn = newGun.objectsToSpawn.Where(obj => obj.AddToProjectile.GetComponent<DoubleViSpawner>() == null).ToArray();
            this.gun.projectileSize *= sizeM;
            newGun.projectileSize *= sizeM2;

            // set the gun of the spawnbulletseffect
            effect.SetGun(newGun);
            //effect.SetGun(this.gun);
            newGun.objectsToSpawn = new ObjectsToSpawn[] { PreventRecursion.stopRecursionObjectToSpawn };
            //this.gun.objectsToSpawn = new ObjectsToSpawn[] { PreventRecursion.stopRecursionObjectToSpawn };
        }
    }
    class DoubleViGun : Gun
    { };
}
