using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using lifestealaccessory.Items.Accessories;
namespace lifestealaccessory
{
    public class LifeStealPlayer : ModPlayer
    {
        // FLAG
        public bool HasLifeStealAccessory = false;

        // Sobrescrever OnHitNPC p/ lifesteal...
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (HasLifeStealAccessory)
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