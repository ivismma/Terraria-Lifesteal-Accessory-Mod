using Terraria;
using Terraria.ModLoader;
using System;
using modconfig;

namespace lifestealaccessory{
    public partial class LifeStealPlayer : ModPlayer{
        public static LifeStealConfig config = ModContent.GetInstance<LifeStealConfig>();
        public static float percentage = config.lifestealPercentage/100f;
        public static float percentage_neardeath = config.lifestealPercentage2/100f;

        public static DateTime lastHeal = DateTime.MinValue;
        
        // FLAGS:
        public bool HasLifeStealAccessory = false;
        public bool NearDeath = false; 

        public bool canLifeSteal(int npcID, DateTime currentTime){  // Função para verificar se pode ou não roubar vida...
            return  HasLifeStealAccessory && 
                    !isOnCooldown(currentTime) && 
                    !npc_BlackList.Contains(npcID);
        }

        public bool isOnCooldown(DateTime currentTime){
            if(NearDeath)
                return (currentTime - lastHeal).TotalMilliseconds < config.healCooldown/2;
            else
                return (currentTime - lastHeal).TotalMilliseconds < config.healCooldown;
        }

        public void ApplyHeal(int amount, DateTime currentTime){
            Main.LocalPlayer.statLife += amount; // adiciona HP.
            Main.LocalPlayer.HealEffect(amount); // efeito visual da cura.
            lastHeal = currentTime;              // atualiza momento do último heal
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone){
            DateTime currentTime = DateTime.Now; // Momento atual
            
            if(spectreSet && hit.DamageType is MagicDamageClass)
                return;

            if(canLifeSteal(target.netID, currentTime)){
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