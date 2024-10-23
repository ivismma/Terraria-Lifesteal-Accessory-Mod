using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using lifestealaccessory;
using lifestealaccessory.Items.Accessories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
// using System.Collections.Generic;

namespace modconfig{
    public class LifeStealConfig : ModConfig{
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Lifesteal (%)")]
        [Tooltip("Set the amount of Lifesteal % over the damage dealt (default: 5%)")]
        [Range(1, 8)] // Define o intervalo de valores permitido
        [Increment(1)]
        [Slider]
        [DefaultValue(5)]
        public int lifestealPercentage;

        [Label("Heal cooldown (in milliseconds)")]
        [Tooltip("Set the amount of cooldown between heals (default: 260ms)")]
        [Range(0, 1000)] // Define o intervalo de valores permitido
        [Increment(10)]
        [Slider]
        [DefaultValue(260)]
        public int healCooldown;

        [Label("Max heal (Normal hit)")]
        [Tooltip("Set the max HP heal per hit (default: 3HP)")]
        [Range(1, 6)] // Define o intervalo de valores permitido
        [Increment(1)]
        [Slider]
        [DefaultValue(3)]
        public int maxHeal;

        [Label("Max heal (Critical hit)")]
        [Tooltip("Set the max HP heal per critical hit (default: 4HP)")]
        [Range(1, 8)] // Define o intervalo de valores permitido
        [Increment(1)]
        [Slider]
        [DefaultValue(4)]
        public int maxHealCrit;

        [Label("Max heal (Near Death) ")]
        [Tooltip("Set the max HP heal per hit when \"Near Death\" is active (default: 7HP)")]
        [Range(1, 12)] // Define o intervalo de valores permitido
        [Increment(1)]
        [Slider]
        [DefaultValue(7)]
        public int maxHealPassive;

        [Label("Bonus Lifesteal (%) when Near Death is active")]
        [Tooltip("Set the bonus Lifesteal % when \"Near Death\" is active (default: 4%)")]
        [Range(1, 8)] // Define o intervalo de valores permitido
        [Increment(1)]
        [Slider]
        [DefaultValue(4)]
        public int lifestealPercentage2;

        [Label("Drop Rate (1 in ...)")]
        [Tooltip("Set the drop chance from Herpling and Corruptor mobs")]
        [Range(100, 1000)] // Define o intervalo de valores permitido
        [Increment(50)]
        [Slider]
        [DefaultValue(200)]
        public int dropChance;
    }
}