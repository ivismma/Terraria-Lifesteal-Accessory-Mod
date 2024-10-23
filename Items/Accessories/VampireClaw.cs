using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using System;

using lifestealaccessory;

namespace lifestealaccessory.Items.Accessories{
    internal class NPCLOOT : GlobalNPC{
        // ADICIONAR LOOT DROP:
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot){
            int itemID = ModContent.ItemType<Items.Accessories.VampireClaw>();
            int dropChance = LifeStealPlayer.config.dropChance;
            if(npc.netID == 174) // Herpling (Crimson)                        // 0.5% CHANCE (1/200)
                npcLoot.Add(ItemDropRule.Common(itemID,dropChance, 1, 1));
            if(npc.netID == 94) // Corruptor (Corruption)                     // 0.5% CHANCE (1/200)
                npcLoot.Add(ItemDropRule.Common(itemID,dropChance, 1, 1));
        }
    }

    public class VampireClaw : ModItem{
        public readonly int[] blacklisted_weapons = new int[] {1569, 3006}; 
                                            // Vampire Claw e Life Drain

        public override void SetStaticDefaults(){
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults(){
			Item.width = 16;
            Item.height = 16;
            Item.accessory = true;
            Item.rare = 4;
            Item.value = Item.sellPrice(0,5,0,0); // P G S C <-- 5 Gold
            // Preço médio de reforja de buff no NPC Goblin: Apróx 3x o preço do sellPrice.
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual){
            LifeStealPlayer modPlayer = player.GetModPlayer<LifeStealPlayer>();

            if(!Array.Exists(blacklisted_weapons, element => element == player.HeldItem.type))
                modPlayer.HasLifeStealAccessory = true;
            
            player.GetAttackSpeed(DamageClass.Melee) += (!modPlayer.nearDeath())? 0.04f : 0.08f;
        }                                                   // Melee Atk Speed:    4%      8%
    }
}