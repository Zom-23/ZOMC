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


namespace ZomC_Cards
{
    
    //Mods required for this to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]

    [BepInPlugin(ModId, ModName, Version)] //Make it an acutal plugin
    [BepInProcess("Rounds.exe")]

    public class ZomCards : BaseUnityPlugin
    {
        private const string ModId = "com.Zom.rounds.card";
        private const string ModName = "Zom Cards";
        public const string Version = "1.0.0"; //(major.minor.patch)

        //Start up the Cards!!
        void Start()
        {
            UnityEngine.Debug.Log("[ZOMC] Loading Cards");
            CustomCard.BuildCard<GymCard>();
            CustomCard.BuildCard<DoubleVision>();
            CustomCard.BuildCard<FlappyBullets>();
            CustomCard.BuildCard<TrainBullets>();
            CustomCard.BuildCard<MassSilence>();
        }
    }
}
