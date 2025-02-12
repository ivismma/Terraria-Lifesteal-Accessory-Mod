using Terraria;
using Terraria.ModLoader;
using System;
using modconfig;

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
        private static float percentage = config.lifestealPercentage/100f;
        private static float percentage_neardeath = config.lifestealPercentage2/100f;

        private static DateTime lastHeal = DateTime.MinValue;
        
        // FLAGS:
        public static bool HasLifeStealAccessory = false;
        public static bool NearDeath = false; 

        // verificar se pode ou não roubar vida:
        public bool canLifeSteal(int npcID, NPC.HitInfo hit, DateTime currentTime){  
            return  !(spectreSet && hit.DamageType is MagicDamageClass) && // usando set de spectre e dano mágico?
                    !isOnCooldown(currentTime) &&                          // está em cooldown?
                    !npc_BlackList.Contains(npcID);                        // npc que está sofrendo dano é blacklisted?
        }

        public bool isOnCooldown(DateTime currentTime){
            double diff = (currentTime - lastHeal).TotalMilliseconds;
            
            return (NearDeath)? diff < config.healCooldown/2 : diff < config.healCooldown;
        }

        public void ApplyHeal(int amount, DateTime currentTime){
            Player player = Main.LocalPlayer;
            if(player.statLife == 0)
                return; // evita efeitos visuais de cura mesmo causando dano após morto.
            
            player.statLife += amount; // adiciona HP.
            player.HealEffect(amount); // efeito visual da cura.
            lastHeal = currentTime;    // atualiza novo momento do último heal
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone){
            if(!HasLifeStealAccessory)
                return; // acessório não está equipado.
            
            DateTime currentTime = DateTime.Now; // momento atual

            if(canLifeSteal(target.netID, hit, currentTime)){
                int lifeStealAmount = (int) (damageDone * percentage);

                if(!NearDeath){
                    if(!hit.Crit) // Normal
                        lifeStealAmount = (lifeStealAmount > config.maxHeal)? config.maxHeal : lifeStealAmount;
                    else          // Crítico
                        lifeStealAmount = (lifeStealAmount > config.maxHealCrit)? config.maxHealCrit : lifeStealAmount;
                }
                else{ 
                    // nearDeath... (cooldown reduzido pela metade e maxHeal aumentado)
                    
                    lifeStealAmount += (int) (damageDone * percentage_neardeath);
                    lifeStealAmount = (lifeStealAmount > config.maxHealPassive)? config.maxHealPassive : lifeStealAmount;
                }
                if(lifeStealAmount > 0)
                    ApplyHeal(lifeStealAmount, currentTime);                  
            }
        }
    
        public override void ResetEffects(){ 
            HasLifeStealAccessory = false;
        } // sem isso o efeito de LifeSteal persiste após desequipar o item
    }
}