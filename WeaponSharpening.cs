using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SideLoader;
using BepInEx;

namespace WeaponSharpening
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("com.sinai.SideLoader", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.sinai.SharedModConfig", BepInDependency.DependencyFlags.HardDependency)]
    public class WeaponSharpening : BaseUnityPlugin
    {
        const string GUID = "com.rexabyte.WeaponSharpening";
        const string NAME = "Weapon Sharpening";
        const string VERSION = "1.1";

        public const int SHARPENING_STONE_ID = 5850759;
        public static float DURABILITY_COST = 10.0f;

        public Settings settings;

        internal void Awake()
        {
            settings = new Settings();
            settings.config.OnSettingsSaved += Config_OnSettingsSaved;
            settings.config.OnSettingsLoaded += Config_OnSettingsLoaded;

            PhysicalImbueEffect.Setup();

            SL.OnPacksLoaded += Loaded;
            SL.OnSceneLoaded += OnSceneChange;
        }

        private void Config_OnSettingsLoaded()
        {
            DURABILITY_COST = settings.Get<float>(Settings.DURABILITY_COST);
        }

        private void Config_OnSettingsSaved()
        {
            DURABILITY_COST = settings.Get<float>(Settings.DURABILITY_COST);

            // TODO: Update prefab with saved values
        }

        private void Loaded()
        {
            WeaponSharpeningStone.Initialize(
               SHARPENING_STONE_ID,
               settings.Get<int>(Settings.STONE_COST),
               settings.Get<float>(Settings.STONE_WEIGHT)
            );

            PhysicalImbueEffect.Initialize();
        }

        private void OnSceneChange()
        {
            SetupBlacksmith();
        }

        private void SetupBlacksmith()
        {
            List<GameObject> list = Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.name == "HumanSNPC_Blacksmith").ToList();

            foreach (GameObject obj in list)
            {
                if (obj.GetComponentInChildren<MerchantPouch>(true) is MerchantPouch pouch
                    && !pouch.ContainsOfSameID(SHARPENING_STONE_ID))
                {
                    Item item = ItemManager.Instance.GenerateItemNetwork(SHARPENING_STONE_ID);
                    item.transform.parent = pouch.transform;
                }
            }
        }
    }
}
