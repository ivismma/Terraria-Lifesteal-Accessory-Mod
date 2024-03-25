using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using lifestealaccessory.Items.Accessories;
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

        // Função para verificar se pode ou não roubar vida...
        // Nesse caso, Target Dummy (ID 488) é um alvo que não deverá 
        // ser possível roubar vida.
        public bool canLifeSteal(int npcID)
        {
            return (HasLifeStealAccessory && npcID != 488);
        }                                  // Target Dummy
        
        // Sobrescrever OnHitNPC p/ lifesteal...
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (canLifeSteal(target.netID))
            {
                // Calcula o roubo de vida com base no dano e %.
                int lifeStealAmount = (int) (damageDone * VampireClaw.LifeStealPercentage);

                // Se cura >= 1...   (HP do jogo é um inteiro e não há cura com casa decimal)
                if(lifeStealAmount > 0)
                {  
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