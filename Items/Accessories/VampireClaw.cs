using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using lifestealaccessory;
using Terraria.ID;

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
        public override void SetDefaults(){
			Item.width = 16;
            Item.height = 16;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0,5,0,0); // P G S C <-- 5 Gold
            // Preço médio de reforja de buff no NPC Goblin: Apróx 3x o preço do sellPrice.
        }
        
        public override void SetStaticDefaults(){
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual){
            LifeStealPlayer modPlayer = player.GetModPlayer<LifeStealPlayer>();

            if(player.statLife <= player.statLifeMax2*0.15f){ // passiva
                modPlayer.NearDeath = true;
                player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
            }
            else{
                modPlayer.NearDeath = false;
                player.GetAttackSpeed(DamageClass.Melee) += 0.04f; 
            }
            // verifica se arma não é blacklisted
            if(!modPlayer.item_BlackList.Contains(player.HeldItem.netID))
                modPlayer.HasLifeStealAccessory = true;
        }
    }
}