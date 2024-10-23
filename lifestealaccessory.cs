using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using System;

using lifestealaccessory.Items.Accessories;
using modconfig;

// using System.Collections.Generic;

namespace lifestealaccessory{
    public class LifeStealPlayer : ModPlayer{
        public static LifeStealConfig config = ModContent.GetInstance<LifeStealConfig>();
        public static float percentage = config.lifestealPercentage/100f;
        public static float percentage_neardeath = config.lifestealPercentage2/100f;

        public static DateTime currentTime;
        public static DateTime lastHeal = DateTime.Now;
        public bool HasLifeStealAccessory = false; // FLAG
        public bool spectreSet = false;

        public readonly int[] blacklisted_npcs = new int[] {422, 488, 493, 507, 517}; 
        // Blacklisted: Target Dummy e os pilares pré-moonlord

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone){
            currentTime = DateTime.Now; // Momento atual
            
            // Player pode até curar usando o set, mas não junto com o set bonus (dano mágico):
            if(spectreSet && hit.DamageType is MagicDamageClass)
                return;

            if(canLifeSteal(target.netID)){
                int lifeStealAmount = (int) (damageDone * percentage);

                if(!nearDeath()){
                    if((currentTime - lastHeal).TotalMilliseconds < config.healCooldown)
                        return;
                        
                    if(!hit.Crit) // Normal
                        lifeStealAmount = (lifeStealAmount > config.maxHeal)? config.maxHeal : lifeStealAmount;
                    else          // Crítico
                        lifeStealAmount = (lifeStealAmount > config.maxHealCrit)? config.maxHealCrit : lifeStealAmount;
                }
                else{
                    if((currentTime - lastHeal).TotalMilliseconds < config.healCooldown/2)
                        return;
                    
                    lifeStealAmount += (int) (damageDone * percentage_neardeath);
                    lifeStealAmount = (lifeStealAmount > config.maxHealPassive)? config.maxHealPassive : lifeStealAmount;
                }
                if(lifeStealAmount > 0){
                    Main.LocalPlayer.statLife += lifeStealAmount; // Adiciona HP.
                    Main.LocalPlayer.HealEffect(lifeStealAmount); // Efeito visual da quantidade curada.
                    lastHeal = currentTime;           // Atualiza momento do último heal
                }
            }
        }
    

        public bool canLifeSteal(int npcID){  // Função para verificar se pode ou não roubar vida...
            return (HasLifeStealAccessory && !Array.Exists(blacklisted_npcs, element => element == npcID));
        }
        
        // Verifica se o player está usando o set completo de Spectre
        public bool isUsingSpectreSet(){
            Player player = Main.LocalPlayer; 
            return (player.armor[0].type == 1503 && 
                    player.armor[1].type == 1504 && 
                    player.armor[2].type == 1505);
        }
        
        // Atualizar se está ou não usando o set
        public override void PostUpdateEquips(){
            spectreSet = isUsingSpectreSet();
        }

        public bool nearDeath(){  // Verifica se a vida atual é <= 15% da vida máxima.
            return (Main.LocalPlayer.statLife <= Main.LocalPlayer.statLifeMax2*0.15f);
        }
        
        public override void ResetEffects(){ 
            HasLifeStealAccessory = false;
        } // Função do tmodloader, sem isso os efeitos persistem após desequipar o item
    }
}