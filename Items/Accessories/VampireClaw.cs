using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.GameContent;
using modconfig;
using System.Linq;

namespace lifestealaccessory.Items.Accessories{

    internal class NPCLOOT : GlobalNPC{
        // ADICIONAR LOOT DROP:
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot){
            int itemID = ModContent.ItemType<VampireClaw>();
            int dropChance = LifeStealPlayer.Config.dropChance;

            if(npc.netID == NPCID.Herpling)     // Default -> 0.5% CHANCE (1/200)
                npcLoot.Add(ItemDropRule.Common(itemID,dropChance, 1, 1));
            if(npc.netID == NPCID.Corruptor)    // Default -> 0.5% CHANCE (1/200)
                npcLoot.Add(ItemDropRule.Common(itemID,dropChance, 1, 1));
        }
    }

    public class VampireClaw : ModItem{
        private static readonly float attackSpeedBonus = 0.04f;     // atk speed padrão.
        private static readonly float attackSpeedMultiplier = 2.5f; // multiplicador de atk speed na passiva.
        private static readonly float nearDeath_HP = 0.15f;         // % de HP atual necessário para ativar a passiva.
        public static float damageReduction = 0.08f;                // % de dano geral reduzido.

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

        // Atualizar descrição de acordo com as configs setadas do mod.
        public override void ModifyTooltips(List<TooltipLine> tooltips){
            if(Item.social)
                return; // está equipado em slot visual.

            int percentage = LifeStealPlayer.Config.lifestealPercentage;
            int maxHeal = LifeStealPlayer.Config.maxHeal;
            int maxHealCrit = LifeStealPlayer.Config.maxHealCrit;
            int dr = LifeStealPlayer.Config.damageReduction;

            int t1_index; // indice do 1º tooltip dinâmico do item

            { // escopo só p/ 2ª linha, que possui alguns detalhes diferentes no Master Mode
                t1_index = tooltips.FindIndex(tl => tl.Text == "LifestealInfo");
                if (Main.masterMode) {
                    tooltips[t1_index].OverrideColor = Microsoft.Xna.Framework.Color.Red; // deixa linha em vermelho
                    percentage += LifeStealPlayer.Config.masterModeBonus;
                }
                tooltips[t1_index].Text = percentage + "% " + "increased lifesteal at the cost of " + dr + "% reduced overall damage dealt";
            }
            tooltips[t1_index+1].Text = 100 * attackSpeedBonus + "% increased melee attack speed";
            tooltips[t1_index+2].Text = "Lifesteal amount limit per hit: " + maxHeal + " HP";
            tooltips[t1_index+3].Text = "Non-melee damage is reduced by 1%";
            tooltips[t1_index+4].Text = "Passive: Below 15% HP, all bonuses increase and heal cooldown is halved.";
        }

        private bool isNearDeath(Player player) {
            return player.statLife <= player.statLifeMax2*nearDeath_HP;
        }

        public override void UpdateAccessory(Player player, bool hideVisual){
            LifeStealPlayer modPlayerInstance = player.GetModPlayer<LifeStealPlayer>();

            if (isNearDeath(player)){ // passiva
                modPlayerInstance.NearDeath = true;
                player.GetAttackSpeed(DamageClass.Melee) += attackSpeedBonus*attackSpeedMultiplier;
            }
            else{
                modPlayerInstance.NearDeath = false;
                player.GetAttackSpeed(DamageClass.Melee) += attackSpeedBonus; 
            }
            // verifica se arma não é blacklisted
            if (!modPlayerInstance.item_BlackList.Contains(player.HeldItem.netID)) {
                player.GetDamage(DamageClass.Generic) *= (float)(1 - damageReduction);
                modPlayerInstance.HasLifeStealEffect = true;
            }
        }
    }
}