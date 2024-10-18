using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using lifestealaccessory.Items.Accessories;
using System;
// using System.Collections.Generic;

namespace lifestealaccessory
{
    public class LifeStealPlayer : ModPlayer
    {
        /*  Não será usada por enquanto...
        private HashSet<int> blacklistedNPCs = new HashSet<int>
        {
            488 // Target Dummy
        }; */

        // FLAG
        public bool HasLifeStealAccessory = false;
        public readonly int healCooldown = 100; // em millisegundos
        // public readonly int healLimit = 10;

        public bool canLifeSteal(int npcID)
        {       // Função para verificar se pode ou não roubar vida...
                return (HasLifeStealAccessory && npcID != 488);
        }                                  // Target Dummy (evitar exploit - não pode)
        
        public bool nearDeath()
        {       // Verifica se a vida atual é <= 15% da vida máxima.
            return (Main.LocalPlayer.statLife <= Main.LocalPlayer.statLifeMax2*0.15f);
        }

        // Sobrescrever OnHitNPC p/ lifesteal...
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            DateTime currentTime = DateTime.Now;
            
            if((currentTime - VampireClaw.lastHeal).TotalMilliseconds < healCooldown)
                return; // Não cura.
            
            VampireClaw.lastHeal = currentTime;

            if(canLifeSteal(target.netID))
            {
                // Calcula o roubo de vida com base no dano e %.
                
                float percentage = VampireClaw.LifeStealPercentage;
                bool passive = nearDeath();
                if(passive) percentage += 0.02f; // Adiciona +2% Life Steal
                
                int lifeStealAmount = (int) (damageDone * percentage);
                if(lifeStealAmount > 0) // HP do jogo é (int)
                {
                    if(!passive) // Vida > 15%...
                    {                              // Limites de heal:
                        if(!hit.Crit) // Normal
                            lifeStealAmount = (lifeStealAmount > 5)? 5 : lifeStealAmount;
                        else          // Crítico
                            lifeStealAmount = (lifeStealAmount > 6)? 6 : lifeStealAmount;
                    }
                    else{ // Vida <= 15%  -  Passiva ativada... 
                        lifeStealAmount = (lifeStealAmount > 8)? 8 : lifeStealAmount;
                    }
                    
                    // Aplica a cura baseada no dano.
                    Main.LocalPlayer.statLife += lifeStealAmount; // Adiciona HP.
                    Main.LocalPlayer.HealEffect(lifeStealAmount); // Efeito visual da quantidade curada.
                }
            }
        }

        public override void ResetEffects()
        {
            HasLifeStealAccessory = false;
        }
    }
}