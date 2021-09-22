using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;
using UnityEngine.UI;
using UnboundLib;

namespace ZomC_Cards.MonoBehaviours
{
    public class DoubleVisionMono : MonoBehaviour
    {
        private CharacterData data;
        private Gun gun;
        private WeaponHandler weaponHandler;
        private Player player;
        private Action<GameObject> shootAction;



        private void Start()
        {
            data = GetComponentInParent<CharacterData>();
        }

        private void Update()
        {
            if (!player)
            {
                if (!(data is null))
                {
                    player = data.player;
                    weaponHandler = data.weaponHandler;
                    gun = weaponHandler.gun;
                    shootAction = new Action<GameObject>(this.OnShootProjectileAction);
                    gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(gun.ShootPojectileAction, shootAction);
                }
            }
        }

        static float NextFloat(double v1, double v2)
        {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (2 - 0) + 0); //double val = (random.NextDouble() * (max - min) + min)
            return (float)val;
        }

        private void OnShootProjectileAction(GameObject obj)
        {
            gun.projectileSize *= NextFloat(0f, 2f);
        }

        private void OnDestroy()
        {
            gun.ShootPojectileAction = (Action<GameObject>)Delegate.Remove(gun.ShootPojectileAction, shootAction);
        }

        public void Destroy()
        {
            UnityEngine.Object.Destroy(this);
        }
    }
}
