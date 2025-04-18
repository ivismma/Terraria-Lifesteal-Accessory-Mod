using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace lifestealaccessory{
    public partial class LifeStealPlayer : ModPlayer{
        public bool spectreSet {
            get;
            private set;
        }
        // a restrição de cura com o Spectre Set é aplicada somente quando usado junto com dano mágico

        // NPCs blacklisted: Target Dummy e os pilares pré-moonlord:
        public readonly HashSet<int> npc_BlackList = [
            NPCID.TargetDummy,
            NPCID.LunarTowerVortex, 
            NPCID.LunarTowerSolar, 
            NPCID.LunarTowerStardust, 
            NPCID.LunarTowerNebula
        ] ;

        // NPC's (AI) blacklisted (bichos, pássaros, minhocas, etc):
        // com objetivo de seguir o padrão do Vampire Knives
        public readonly HashSet<int> npcAi_BlackList = [
            NPCAIStyleID.Passive,
            NPCAIStyleID.Bird,
            NPCAIStyleID.CritterWorm
        ];

        // Armas blacklisted:
        public readonly HashSet<int> item_BlackList = [
            ItemID.VampireKnives,
            ItemID.SoulDrain // (Life Drain)
        ];

        // verifica se o player está usando o set completo de Spectre
        public bool isUsingSpectreSet(){
            return Player.armor[0].type == ItemID.SpectreHood &&
                   Player.armor[1].type == ItemID.SpectreRobe &&
                   Player.armor[2].type == ItemID.SpectrePants;
        }

        public bool hasMoonBiteDebuff() {
            return Player.HasBuff(BuffID.MoonLeech); // Moon Bite
        }

        // atualiza se está ou não usando o set
        public override void PostUpdateEquips() {
            spectreSet = isUsingSpectreSet();
        }
    }
}