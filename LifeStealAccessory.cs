using Terraria;
using Terraria.ModLoader;
using System;
using modconfig;
using Terraria.ID;

namespace lifestealaccessory{
    public partial class LifeStealPlayer : ModPlayer{
        private static LifeStealConfig config = ModContent.GetInstance<LifeStealConfig>();
        public static LifeStealConfig Config {
            get{
                return config;
            }
            private set {
                config = value;
            }
        }
        public static float percentage = config.lifestealPercentage/100f;
        public static float percentage_neardeath = config.lifestealPercentage2/100f;
        public static int mastermode_bonus = config.masterModeBonus;
        public static int maxheal_normalhit = config.maxHeal;
        public static int maxheal_crit = config.maxHealCrit;
        public static int maxheal_passive = config.maxHealPassive;

        private static DateTime lastHeal = DateTime.MinValue;

        // FLAGS:
        public static bool HasLifeStealEffect = false;
        public static bool NearDeath = false;

        // verificar se pode ou não roubar vida:
        public bool canLifeSteal(NPC target, NPC.HitInfo hit, DateTime currentTime){
            return !isOnCooldown(currentTime) &&                          // está em cooldown?
                   !(spectreSet && hit.DamageType is MagicDamageClass) && // usando set de spectre e dano mágico?
                   !npc_BlackList.Contains(target.netID) &&               // npc alvo é blacklisted?
                   target.aiStyle != NPCAIStyleID.Passive &&              // não é npc passivo (insetos/coelhos/etc)
                   !hasMoonBiteDebuff();                                  // está com debuff do moon lord?
        }

        public bool isOnCooldown(DateTime currentTime){
            double diff = (currentTime - lastHeal).TotalMilliseconds;
            
            return (NearDeath)? diff < config.healCooldown/2 : diff < config.healCooldown;
        }

        public void ApplyHeal(int amount, DateTime currentTime){
            if(Player.statLife == 0)
                return; // evita efeitos visuais de cura mesmo causando dano após morto.
            
            Player.statLife += amount; // adiciona HP.
            Player.HealEffect(amount); // efeito visual da cura.

            lastHeal = currentTime;    // atualiza novo momento do último heal
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone){
            if(!HasLifeStealEffect)
                return; // acessório não está equipado ou está restringido.

            DateTime currentTime = DateTime.Now; // momento atual

            if(canLifeSteal(target, hit, currentTime)){
                int lifeStealAmount;     // qtd. de cura do hit
                int maxHeal;             // HP cap
                float totalPercentage = (hit.DamageType is MeleeDamageClass)? percentage : percentage-0.01f;
                // % total de lifesteal
                
                if (Main.masterMode)
                    totalPercentage += mastermode_bonus/100f;

                if (!NearDeath){
                    lifeStealAmount = (int) (damageDone * totalPercentage);
                    maxHeal = (!hit.Crit)? maxheal_normalhit: maxheal_crit; // normal/crítico
                }
                else { // nearDeath... (cooldown reduzido pela metade e maxHeal aumentado)
                    maxHeal = maxheal_passive;               // HP cap da passiva
                    totalPercentage += percentage_neardeath; // % lifesteal bônus da passiva
                    lifeStealAmount = (int) (damageDone * totalPercentage);
                }
                lifeStealAmount = (lifeStealAmount > maxHeal) ? maxHeal : lifeStealAmount;

                if (lifeStealAmount > 0)
                    ApplyHeal(lifeStealAmount, currentTime);
            }
        }

        public override void ResetEffects(){ 
            HasLifeStealEffect = false;
        } // sem isso o efeito de LifeSteal persiste após desequipar o item
    }
}