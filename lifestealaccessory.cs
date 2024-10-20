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
        public readonly int healCooldown = 410;    // em millisegundos

        // Sobrescrever OnHitNPC p/ lifesteal...
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone){
            DateTime currentTime = DateTime.Now; // Momento atual

            if(canLifeSteal(target.netID)){
                // Calcula o roubo de vida com base no dano e %.
                float percentage = VampireClaw.LifeStealPercentage;
                bool passive = nearDeath(); // Ver se a passiva será aplicada (vida <= 15%)
                
                if(passive){
                    percentage += 0.04f; // Adiciona +4% Life Steal
                    if((currentTime - VampireClaw.lastHeal).TotalMilliseconds < healCooldown/2)
                        return; // Não cura.
                }
                else
                    if((currentTime - VampireClaw.lastHeal).TotalMilliseconds < healCooldown)
                        return; // Não cura.
                
                int lifeStealAmount = (int) (damageDone * percentage);

                if(lifeStealAmount > 0){ // HP do jogo é (int)
                    if(!passive){

                        if(!hit.Crit) // Normal
                            lifeStealAmount = (lifeStealAmount > 3)? 3 : lifeStealAmount;
                        else          // Crítico
                            lifeStealAmount = (lifeStealAmount > 4)? 4 : lifeStealAmount;
                    }
                    else // passiva ativada 
                        lifeStealAmount = (lifeStealAmount > 7)? 7 : lifeStealAmount;
                    
                    // Aplica a cura baseada no dano.
                    Main.LocalPlayer.statLife += lifeStealAmount; // Adiciona HP.
                    Main.LocalPlayer.HealEffect(lifeStealAmount); // Efeito visual da quantidade curada.
                    VampireClaw.lastHeal = currentTime;           // Atualiza momento do último heal
                }
            }
        }

        public bool canLifeSteal(int npcID){  // Função para verificar se pode ou não roubar vida...
            return (HasLifeStealAccessory && npcID != 488);   // id 488 - Target Dummy -> não roubar vida
        }
        
        public bool nearDeath(){  // Verifica se a vida atual é <= 15% da vida máxima.
            return (Main.LocalPlayer.statLife <= Main.LocalPlayer.statLifeMax2*0.15f);
        }
        
        public override void ResetEffects(){ 
            HasLifeStealAccessory = false;
        } // Função do tmodloader, sem isso os efeitos persistem após desequipar o item
        
    }
}