using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using lifestealaccessory.Items.Accessories;
using System;
// using System.Collections.Generic;

namespace lifestealaccessory{
    public class LifeStealPlayer : ModPlayer{
        public bool HasLifeStealAccessory = false; // FLAG
        public bool spectreSet = false;

        float percentage = VampireClaw.LifeStealPercentage;
        public readonly int healCooldown = 340;    // em millisegundos

        public readonly int[] blacklisted_npcs = new int[] {422, 488, 493, 507, 517}; 
        // Blacklisted: Target Dummy e os pilares pré-moonlord

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone){
            DateTime currentTime = DateTime.Now; // Momento atual

            // Player pode até curar usando o set, mas não junto com o set bonus (dano mágico):
            if(spectreSet && hit.DamageType is MagicDamageClass)
                return;

            if(canLifeSteal(target.netID)){
                int lifeStealAmount = (int) (damageDone * percentage);

                if(!nearDeath()){
                    if((currentTime - VampireClaw.lastHeal).TotalMilliseconds < healCooldown)
                        return;
                        
                    if(!hit.Crit) // Normal
                        lifeStealAmount = (lifeStealAmount > 3)? 3 : lifeStealAmount;
                    else          // Crítico
                        lifeStealAmount = (lifeStealAmount > 4)? 4 : lifeStealAmount;
                }
                else{
                    if((currentTime - VampireClaw.lastHeal).TotalMilliseconds < healCooldown/2)
                        return;
                    
                    lifeStealAmount += (int) (damageDone * VampireClaw.nearDeathBonus);
                    lifeStealAmount = (lifeStealAmount > 7)? 7 : lifeStealAmount;
                }
                if(lifeStealAmount > 0){
                    Main.LocalPlayer.statLife += lifeStealAmount; // Adiciona HP.
                    Main.LocalPlayer.HealEffect(lifeStealAmount); // Efeito visual da quantidade curada.
                    VampireClaw.lastHeal = currentTime;           // Atualiza momento do último heal
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