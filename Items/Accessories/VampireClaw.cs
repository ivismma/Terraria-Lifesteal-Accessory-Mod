using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;

namespace lifestealaccessory.Items.Accessories
{

    internal class NPCLOOT : GlobalNPC
    {
         // ADICIONAR LOOT DROP:
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(npc.netID == 174) // Herpling (Crimson)                                      // 0.5% CHANCE (1/200)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.VampireClaw>(),200, 1, 1));
            if(npc.netID == 94) // Corruptor (Crimson)                                      // 0.5% CHANCE (1/200)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.VampireClaw>(),200, 1, 1));
        }
    }

    public class VampireClaw : ModItem
    {
        public static readonly float LifeStealPercentage = 0.04f; // 4% de roubo de vida

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
			Item.width = 16;
            Item.height = 16;
            Item.accessory = true;
            Item.rare = 4;           // P G  S C         
            Item.value = Item.sellPrice(0,2,50,0); // 2 Gold, 50 Silver
            // Preço médio de reforja de buff no NPC Goblin: Apróx 3x o preço do sellPrice.
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<LifeStealPlayer>().HasLifeStealAccessory = true;
            player.GetAttackSpeed(DamageClass.Melee) += 0.04f; // +4% Atk Speed
        }
    }
}