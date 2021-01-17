using SharedModConfig;
using System;
using System.Collections.Generic;

namespace WeaponSharpening
{
    public class Settings
    {
        public static string DURABILITY_COST = "Durability_Cost";
        public static string STONE_COST = "Stone_Cost";
        public static string STONE_WEIGHT = "Stone_Weight";

        public ModConfig config;

        public T Get<T>(string setting)
        {
            return (T)Convert.ChangeType(this.config.GetValue(setting), typeof(T));
        }

        public Settings()
        {
            config = new ModConfig
            {
                ModName = "WeaponSharpening",
                SettingsVersion = 1.0,
                Settings = new List<BBSetting>
                {
                    new FloatSetting
                    {
                        Name = DURABILITY_COST,
                        Description = "Durability Cost",
                        DefaultValue = 10.0f,
                        MinValue = 0.0f,
                        MaxValue = 25f,
                        RoundTo = 0,
                        ShowPercent = false
                    },
                    new FloatSetting
                    {
                        Name = STONE_COST,
                        Description = "Sharpening Stone Cost",
                        DefaultValue = 40f,
                        MinValue = 10f,
                        MaxValue = 100f,
                        RoundTo = 0,
                        ShowPercent = false
                    },
                    new FloatSetting
                    {
                        Name = STONE_WEIGHT,
                        Description = "Sharpening Stone Weight",
                        DefaultValue = 3f,
                        MinValue = 1f,
                        MaxValue = 10f,
                        RoundTo = 0,
                        ShowPercent = false
                    }
                }
            };

            config.Register();
        }
    }
}
