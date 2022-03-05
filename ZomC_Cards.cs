using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using BepInEx;
using UnboundLib;
using UnboundLib.GameModes;
using UnboundLib.Cards;
using UnboundLib.Utils;
using ZomC_Cards.Cards;
using Photon.Pun;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ZomC_Cards.MonoBehaviours;
//Mod created by Zom_23 for the game ROUNDS

namespace ZomC_Cards
{
    
    //Mods required for this to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.willuwontu.rounds.RespawnPatch", BepInDependency.DependencyFlags.HardDependency)]

    [BepInPlugin(ModId, ModName, Version)] //Make it an acutal plugin
    [BepInProcess("Rounds.exe")]

    


    public class ZomCards : BaseUnityPlugin
    {
        private const string ModId = "com.Zom.rounds.card";
        private const string ModName = "Zom Cards";
        public const string Version = "2.4.0"; //(major.minor.patch) Now out of Beta!

        private static readonly AssetBundle zomcAssets = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("zomcassets", typeof(ZomCards).Assembly);
        public static GameObject TrainBulletsArt = zomcAssets.LoadAsset<GameObject>("C_TrainBullets");

        //Start up the Cards!!
        public void Start()
        {
            

            UnityEngine.Debug.Log("[ZOMC] Loading Cards");
            //They are alphabetized :D
            CustomCard.BuildCard<ABulletForYou>();
            CustomCard.BuildCard<AdaptiveMags>();
            CustomCard.BuildCard<Barbell>();
            CustomCard.BuildCard<BattleRopes>();
            CustomCard.BuildCard<BEEGGun>();
            CustomCard.BuildCard<Bigger>();
            CustomCard.BuildCard<DoubleVision>();
            CustomCard.BuildCard<Dumbells>();
            CustomCard.BuildCard<Envy>();
            //CustomCard.BuildCard<FlappyBullets>();
            CustomCard.BuildCard<Gluttony>();
            CustomCard.BuildCard<Greed>();
            CustomCard.BuildCard<GymCard>();
            CustomCard.BuildCard<LighterAmmo>();
            CustomCard.BuildCard<Lust>();
            CustomCard.BuildCard<MassSilence>();
            CustomCard.BuildCard<Microscopic>();
            CustomCard.BuildCard<MoreIsBetter>();
            CustomCard.BuildCard<NineLives>();
            CustomCard.BuildCard<PerseverancePristine>();
            CustomCard.BuildCard<Pride>();
            CustomCard.BuildCard<ProteinBar>();
            CustomCard.BuildCard<PumpedUpKicks>();
            CustomCard.BuildCard<PunchingBag>();
            CustomCard.BuildCard<Sloth>();
            CustomCard.BuildCard<SlowReload>();
            CustomCard.BuildCard<SpareMag>();
            CustomCard.BuildCard<StayingAhead>();
            CustomCard.BuildCard<TrainBullets>();
            CustomCard.BuildCard<Treadmill>();
            CustomCard.BuildCard<Wrath>();
        }
    }
}
