using Terraria.ModLoader.Config;
using System;
using System.ComponentModel;
using lifestealaccessory;
using Terraria.ModLoader;
using lifestealaccessory.Items.Accessories;

namespace modconfig{
    public class LifeStealConfig : ModConfig{
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Lifesteal (%)")]
        [Tooltip("Set the amount of Lifesteal % over the damage dealt (default: 3%)")]
        [Range(1, 8)]
        [Increment(1)]
        [Slider]
        [DefaultValue(3)]
        public int lifestealPercentage;

        [Label("Lifesteal (%) bonus on Master Mode")]
        [Tooltip("Set a % bonus amount of lifesteal on Master Mode")]
        [Range(0, 3)]
        [Increment(1)]
        [Slider]
        [DefaultValue(1)]
        public int masterModeBonus;

        [Label("Overall Damage Reduction Percentage")]
        [Tooltip("Set the % of overall damage reduction")]
        [Range(0, 30)]
        [Increment(1)]
        [Slider]
        [DefaultValue(8)]
        public int damageReduction;

        [Label("Heal cooldown (in milliseconds)")]
        [Tooltip("Set the amount of cooldown between heals (default: 260ms)")]
        [Range(0, 1000)]
        [Increment(10)]
        [Slider]
        [DefaultValue(280)]
        public int healCooldown;

        [Label("Max heal (Normal hit)")]
        [Tooltip("Set the max HP heal per hit (default: 3HP)")]
        [Range(1, 8)]
        [Increment(1)]
        [Slider]
        [DefaultValue(3)]
        public int maxHeal;

        [Label("Max heal (Critical hit)")]
        [Tooltip("Set the max HP heal per critical hit (default: 3HP)")]
        [Range(1, 8)]
        [Increment(1)]
        [Slider]
        [DefaultValue(3)]
        public int maxHealCrit;

        [Label("Max heal (Near Death) ")]
        [Tooltip("Set the max HP heal per hit when \"Near Death\" is active (default: 6HP)")]
        [Range(1, 10)]
        [Increment(1)]
        [Slider]
        [DefaultValue(6)]
        public int maxHealPassive;

        [Label("Bonus Lifesteal (%) when Near Death is active")]
        [Tooltip("Set the bonus Lifesteal % when \"Near Death\" is active (default: 3%)")]
        [Range(1, 6)]
        [Increment(1)]
        [Slider]
        [DefaultValue(2)]
        public int lifestealPercentage2;

        [Label("Drop Rate (1 in ...)")]
        [Tooltip("Set the drop chance from Herpling and Corruptor mobs")]
        [Range(100, 1000)]
        [Increment(50)]
        [Slider]
        [DefaultValue(200)]
        [ReloadRequired] // alterar drop rate necessita reload do mod.
        public int dropChance;
        
        // Atualizar alterações do mod configuration que não precisam de reload:
        public override void OnChanged() {
            if(ModContent.GetInstance<LifeStealPlayer>() != null) {
                LifeStealPlayer.percentage = lifestealPercentage/100f;
                LifeStealPlayer.percentage_neardeath = lifestealPercentage2/100f;
                LifeStealPlayer.maxheal_normalhit = maxHeal;
                LifeStealPlayer.maxheal_crit = maxHealCrit;
                LifeStealPlayer.maxheal_passive = maxHealPassive;
                LifeStealPlayer.mastermode_bonus = masterModeBonus;
                VampireClaw.damageReduction = damageReduction/100f;
            }
        }
        
    }
}