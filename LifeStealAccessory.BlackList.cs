using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace lifestealaccessory{
    public partial class LifeStealPlayer : ModPlayer{
        public bool spectreSet;
        // a restrição de cura com o Spectre Set é aplicada somente quando usado junto com dano mágico

        // NPCs blacklisted: Target Dummy e os pilares pré-moonlord:
        public readonly HashSet<int> npc_BlackList = [
            NPCID.TargetDummy,
            NPCID.LunarTowerVortex, 
            NPCID.LunarTowerSolar, 
            NPCID.LunarTowerStardust, 
            NPCID.LunarTowerNebula
        ] ;

        // Armas blacklisted:
        public readonly HashSet<int> item_BlackList = [
            ItemID.VampireKnives,
            ItemID.SoulDrain // (Life Drain)
        ];
    
        // verifica se o player está usando o set completo de Spectre
        public bool isUsingSpectreSet(){
            Player player = Main.LocalPlayer; 
            return  player.armor[0].type == ItemID.SpectreHood && 
                    player.armor[1].type == ItemID.SpectreRobe && 
                    player.armor[2].type == ItemID.SpectreBoots;
        }

        // atualiza se está ou não usando o set
        public override void PostUpdateEquips(){
            spectreSet = isUsingSpectreSet();
        }
    }
}