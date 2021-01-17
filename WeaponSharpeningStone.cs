using SideLoader;
using UnityEngine;

namespace WeaponSharpening
{
    public static class WeaponSharpeningStone
    {
        public static void Initialize(int id, int cost, float weight)
        {
            var item = ResourcesPrefabManager.Instance.GetItemPrefab(id);

            var stats = new SL_ItemStats()
            {
                BaseValue = cost,
                MaxDurability = 100,
                RawWeight = weight,
            };

            stats.ApplyToItem(item.GetComponent<ItemStats>());

            var effects = new GameObject("Effects");
                effects.transform.parent = item.transform;
                effects.AddComponent<SharpeningStoneEffect>();
        }
    }
}
