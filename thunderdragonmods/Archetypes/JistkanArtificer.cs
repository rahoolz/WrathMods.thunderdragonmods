using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Class.Kineticist.ActivatableAbility;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using Owlcat.QA.Validation;
using thunderdragonmods.WeaponType;
using TabletopTweaks.Core.NewComponents;

namespace thunderdragonmods.Archetypes
{
    internal class JistkanArtificer
    {
        private static readonly string ArtiName = "JistkanArtificerArchetype";
        private static readonly string ArtiGuid = "CDB9F8E7-2031-40BF-A77B-E3CB3ABADDE4";

        private static readonly string ArtiDimSpellName = "DiminishedSpellcasting.Arti";
        private static readonly string ArtiDimSpellGuid = "277C758E-E64A-4DDF-B94E-1E951F99258C";

        private static readonly string GolemArmName = "GolemArm.Arti";
        private static readonly string GolemArmGuid = "92356ADE-ED6D-44C1-925C-5C7527164272";
        private static readonly string GolemArmBuffName = "GolemArm.Buff.Arti";
        private static readonly string GolemArmBuffGuid = "1DA10436-3352-4BE8-B126-FC538F1F7A9A";
        private static readonly string ArmResetName = "GolemArm.Reset.Arti";
        private static readonly string ArmResetGuid = "5B54D0E1-3484-418C-A53A-D372D089FCF2";
        private static readonly string GolemArmCIName = "GolemArm.ColdIron.Arti";
        private static readonly string GolemArmCIGuid = "D27BF1CC-B762-448D-9A94-385A182BE4AF";

        private static readonly string ArtiUnarmedName = "UnarmedStrike.Arti";
        private static readonly string ArtiUnarmedGuid = "AB8EDEB4-FED9-4B3F-A49C-43353B03E785";

        private static readonly string ArtiSpellstrikeName = "SpellStrike.Arti";
        private static readonly string ArtiSpellstrikeGuid = "C1C3611F-6504-4A2F-B80D-9CFFB86C86AA";
        private static readonly string ArtiSpellstrikeFeatureName = "SpellStrike.Feature.Arti";
        private static readonly string ArtiSpellstrikeFeatureGuid = "B37857E6-3FD0-422C-B390-9D81D30E97A9";
        private static readonly string ArtiSpellstrikeBuffName = "SpellStrike.Buff.Arti";
        private static readonly string ArtiSpellstrikeBuffGuid = "9E83F191-5132-42F1-88CC-2F112E639AD9";


        private static readonly string ArtiThunderingName = "ThunderingEnchant.Arti";
        private static readonly string ArtiThunderingGuid = "229DFA8F-DFF9-4391-8E56-579266ED6B08";
        private static readonly string ArtiThunderingBuffName = "ThunderingBuff.Arti";
        private static readonly string ArtiThunderingBuffGuid = "4AF3DA13-DF49-454E-BE1B-E05C56D458EC";

        private static readonly string ArtiCorrosiveName = "CorrosiveEnchant.Arti";
        private static readonly string ArtiCorrosiveGuid = "59A8586F-6AFC-44D7-8A55-D86FDC4084F7";
        private static readonly string ArtiCorrosiveBuffName = "CorrosiveBuff.Arti";
        private static readonly string ArtiCorrosiveBuffGuid = "6D195180-97A7-4D2A-ACB3-EA120AB168E2";

        private static readonly string ArtiCorrosiveBurstName = "CorrosiveBurstEnchant.Arti";
        private static readonly string ArtiCorrosiveBurstGuid = "6A5E9BC4-60DF-4F93-B87E-0D80530495DA";
        private static readonly string ArtiCorrosiveBurstBuffName = "CorrosiveBurstBuff.Arti";
        private static readonly string ArtiCorrosiveBurstBuffGuid = "FB9392A4-FC65-4788-9CFB-6B06289157E6";

        private static readonly string ArtiImpactEnchName = "ImpactWeaponEnchant.Arti";
        private static readonly string ArtiImpactEnchGuid = "DE7B0825-B5C6-4532-8832-4351B1791964";
        private static readonly string ArtiImpactName = "ImpactEnchant.Arti";
        private static readonly string ArtiImpactGuid = "AD1E2D33-B7C9-4944-9A2D-E703A23C1E6C";
        private static readonly string ArtiImpactBuffName = "ImpactBuff.Arti";
        private static readonly string ArtiImpactBuffGuid = "2F78D9F4-C07E-4DEC-9AB7-F547FF99E356";

        private static readonly string ArtiEnchant2Name = "EnchantPlus2.Arti";
        private static readonly string ArtiEnchant2Guid = "88E1E8AC-727D-4456-BAE4-F043AEABE625";

        private static readonly string ArtiEmpArmName = "EmpoweredArm.Arti";
        private static readonly string ArtiEmpArmGuid = "BEBC019F-598A-4A66-A304-AD2F5D655FC9";
        private static readonly string ArtiEmpArmBuffName = "EmpoweredArm.Buff.Arti";
        private static readonly string ArtiEmpArmBuffGuid = "2F78BD03-6C62-4273-B272-504AFC05C92A";


        public static void Configure()
        {
            /* SETUP ARCHETYPE */
            var icon = AbilityRefs.StoneFist.Reference.Get().Icon;
            var GolemArmWeapon = "GolemArm.Weapon.Standard";
            var StandardHeavyMace = ItemWeaponRefs.StandardHeavyMace;

            Main.Log.Log("Test");


            var JistkanArtificerArchetype = ArchetypeConfigurator.New(ArtiName, ArtiGuid, clazz: CharacterClassRefs.MagusClass)
                .SetLocalizedName("JistkanArti.Name")
                .SetLocalizedDescription("JistkanArti.Description")
                .SetReplaceSpellbook(SpellbookRefs.SwordSaintSpellbook.ToString())
                .AddToStartingItems(ItemWeaponRefs.Rapier.ToString())
                .AddToStartingItems(ItemWeaponRefs.ColdIronLongspear.ToString())
                .Configure();

            var ArtiDiminishedSpellcasting = FeatureConfigurator.New(ArtiDimSpellName, ArtiDimSpellGuid)
                .SetDisplayName("ArtiDimSpell.Name")
                .SetDescription("ArtiDimSpell.Description")
                .Configure();
            /* SETUP ARCHETYPE END */

            /* GOLEM ARM FEATURE */

            var GolemArmBuff = BuffConfigurator.New(GolemArmBuffName, GolemArmBuffGuid)
                .SetDisplayName("GolemArm.Name")
                .SetDescription("GolemArm.Description")
                .CopyFrom(BuffRefs.LivingGrimoireHolyBookBuffPrimary, componentTypes: [
                    typeof(AddFactContextActions),
                    typeof(ContextRankConfig),
                    typeof(FactsChangeTrigger),
                    typeof(AddAbilityUseTrigger)])
                .SetIcon(icon)
                .AddKineticistBlade(GolemArmWeapon.ToString())
                .AddFactContextActions(activated: ActionsBuilder.New().EnhanceWeaponGreater(
                    durationValue: new ContextDurationValue(),
                    enchantLevel: new ContextValue() { Value = 1},
                    enchantment: [WeaponEnchantmentRefs.Enhancement5.ToString()]))
                .SetStacking(Kingmaker.UnitLogic.Buffs.Blueprints.StackingType.Replace)
                .Configure();

            var GolemArmFeature = FeatureConfigurator.New(GolemArmName, GolemArmGuid)
                .SetDisplayName("GolemArm.Name")
                .SetDescription("GolemArm.Description")
                .AddFacts(facts: [GolemArmBuff])
                .Configure();

            /* GOLEM ARM FEATURE END */

            /* UNARMED STRIKE BONUS FEAT DAMAGE */
            var ArtiUnarmedStrike = FeatureConfigurator.New(ArtiUnarmedName, ArtiUnarmedGuid)
                .SetDisplayName("ArtiUnarmed.Name")
                .SetDescription("ArtiUnarmed.Description")
                .SetIsClassFeature(true)
                .AddFacts([FeatureRefs.ImprovedUnarmedStrike.ToString()])
                .Configure();
            /* UNARMED STRIKE BONUS FEAT DAMAGE END */

            /* WEAPON ENCHANT */

            var thundicon = AbilityRefs.Shout.Reference.Get().Icon;
            var ArtiThunderingBuff = BuffConfigurator.New(ArtiThunderingBuffName, ArtiThunderingBuffGuid)
                .SetDisplayName("ArtiThunderingBuff.Name")
                .AddBondProperty(enchantPool: EnchantPoolType.ArcanePool, enchant: WeaponEnchantmentRefs.Thundering.ToString())
                .SetFlags([BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath])
                .SetStacking(stacking:StackingType.Stack)
                .SetFrequency(Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)
                .Configure();
            var ArtiThunderingChoice = ActivatableAbilityConfigurator.New(ArtiThunderingName, ArtiThunderingGuid)
                .SetDisplayName("ArtiThunderingEnchant.Name")
                .SetDescription("ArtiThunderingEnchant.Description")
                .SetIcon(thundicon)
                .SetGroup(Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.ArcaneWeaponProperty)
                .SetWeightInGroup(1)
                .SetBuff(buff: ArtiThunderingBuff)
                .SetActivationType(activationType: Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(activateWithUnitCommand: Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            var corroicon = AbilityRefs.CorrosiveTouch.Reference.Get().Icon;
            var ArtiCorrosiveBuff = BuffConfigurator.New(ArtiCorrosiveBuffName, ArtiCorrosiveBuffGuid)
                .SetDisplayName("ArtiCorrosiveBuff.Name")
                .AddBondProperty(enchantPool: EnchantPoolType.ArcanePool, enchant: WeaponEnchantmentRefs.Corrosive.ToString())
                .SetFlags([BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath])
                .SetStacking(stacking: StackingType.Stack)
                .SetFrequency(Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)
                .Configure();
            var ArtiCorrosiveChoice = ActivatableAbilityConfigurator.New(ArtiCorrosiveName, ArtiCorrosiveGuid)
                .SetDisplayName("ArtiCorrosiveEnchant.Name")
                .SetDescription("ArtiCorrosiveEnchant.Description")
                .SetIcon(corroicon)
                .SetGroup(Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.ArcaneWeaponProperty)
                .SetWeightInGroup(1)
                .SetBuff(buff: ArtiCorrosiveBuff)
                .SetActivationType(activationType: Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(activateWithUnitCommand: Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            var corrobursticon = AbilityRefs.CausticEruption.Reference.Get().Icon;
            var ArtiCorrosiveBurstBuff = BuffConfigurator.New(ArtiCorrosiveBurstBuffName, ArtiCorrosiveBurstBuffGuid)
                .SetDisplayName("ArtiCorrosiveBurstBuff.Name")
                .AddBondProperty(enchantPool: EnchantPoolType.ArcanePool, enchant: WeaponEnchantmentRefs.CorrosiveBurst.ToString())
                .SetFlags([BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath])
                .SetStacking(stacking: StackingType.Stack)
                .SetFrequency(Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)
                .Configure();
            var ArtiCorrosiveBurstChoice = ActivatableAbilityConfigurator.New(ArtiCorrosiveBurstName, ArtiCorrosiveBurstGuid)
                .SetDisplayName("ArtiCorrosiveBurstEnchant.Name")
                .SetDescription("ArtiCorrosiveBurstEnchant.Description")
                .SetIcon(corrobursticon)
                .SetWeightInGroup(2)
                .SetGroup(Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.ArcaneWeaponProperty)
                .SetBuff(buff: ArtiCorrosiveBurstBuff)
                .SetActivationType(activationType: Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(activateWithUnitCommand: Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            var impacticon = AbilityRefs.LeadBlades.Reference.Get().Icon;
            var ImpactEnchant = WeaponEnchantmentConfigurator.New(ArtiImpactEnchName, ArtiImpactEnchGuid)
                .SetEnchantName("ArtiImpactBuff.Name")
                .SetEnchantmentCost(2)
                .AddComponent(new WeaponSizeChange() { SizeCategoryChange = 5})
                .Configure();
            var ArtiImpactBuff = BuffConfigurator.New(ArtiImpactBuffName, ArtiImpactBuffGuid)
                .SetDisplayName("ArtiImpactBuff.Name")
                .AddBondProperty(enchantPool: EnchantPoolType.ArcanePool, enchant: ImpactEnchant)
                .SetFlags([BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath])
                .SetStacking(stacking: StackingType.Stack)
                .SetFrequency(Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)
                .Configure();
            var ArtiImpactChoice = ActivatableAbilityConfigurator.New(ArtiImpactName, ArtiImpactGuid)
                .SetDisplayName("ArtiImpactEnchant.Name")
                .SetDescription("ArtiImpactEnchant.Description")
                .SetIcon(impacticon)
                .SetGroup(Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.ArcaneWeaponProperty)
                .SetWeightInGroup(2)
                .SetBuff(buff: ArtiImpactBuff)
                .SetActivationType(activationType: Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(activateWithUnitCommand: Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .Configure();

            var ArtiEnchantPlus2 = FeatureConfigurator.New(ArtiEnchant2Name, ArtiEnchant2Guid)
                .AddIncreaseActivatableAbilityGroupSize(group: Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.ArcaneWeaponProperty)
                .AddSavesFixerRecalculate(version: 2)
                .AddFacts(facts: [
                    ActivatableAbilityRefs.ArcaneWeaponFlamingChoice.ToString(),
                    ActivatableAbilityRefs.ArcaneWeaponFlamingBurstChoice.ToString(),
                    ActivatableAbilityRefs.ArcaneWeaponFrostChoice.ToString(),
                    ActivatableAbilityRefs.ArcaneWeaponIcyBurstChoice.ToString(),
                    ActivatableAbilityRefs.ArcaneWeaponShockChoice.ToString(),
                    ActivatableAbilityRefs.ArcaneWeaponShockingBurstChoice.ToString(),
                    ArtiThunderingChoice,
                    ArtiCorrosiveChoice,
                    ArtiCorrosiveBurstChoice,
                    ArtiImpactChoice
                    ])
                .Configure();

            /* WEAPON ENCHANT END */

            /* UNARMED SPELLSTRIKE */

            var USSicon = AbilityRefs.TelekineticFist.Reference.Get().Icon;
            var IsUsingUnarmed = ConditionsBuilder.New().IsWeaponEquipped(category: WeaponCategory.UnarmedStrike, checkOnCaster: true, checkSlot: true, checkWeaponCategory: true, slot: ContextConditionIsWeaponEquipped.CheckedSlot.PrimaryHand, negate: false).Build();
            var ArtiSpellstrikeBuff = BuffConfigurator.New(ArtiSpellstrikeBuffName, ArtiSpellstrikeBuffGuid)
                .SetDisplayName("ArtiUnarmedSpellstrike.Name")
                .SetDescription("ArtiUnarmedSpellstrike.Descriprtion")
                .SetIcon(USSicon)
                .AddComponent(new RecalculateOnEquipmentChange())
                //.SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddFactContextActions(
                    activated:
                        ActionsBuilder.New().Conditional(IsUsingUnarmed,
                        ifTrue: 
                        ActionsBuilder.New().ApplyBuffPermanent(buff: BuffRefs.SpellStrikeBuff.ToString(), isNotDispelable: true).Build(),
                        ifFalse:
                        ActionsBuilder.New().RemoveBuff(buff: BuffRefs.SpellStrikeBuff.ToString(), toCaster: true, onlyFromCaster: true).Build()
                        ),
                    deactivated: ActionsBuilder.New().RemoveBuff(buff: BuffRefs.SpellStrikeBuff.ToString(), toCaster: true, onlyFromCaster: true).Build()
                     )
                .Configure();

            var ArtiUnarmedSpellstrike = ActivatableAbilityConfigurator.New(ArtiSpellstrikeName, ArtiSpellstrikeGuid)
                //.CopyFrom(ActivatableAbilityRefs.SpellStrikeAbility)
                .SetDisplayName("ArtiUnarmedSpellstrike.Name")
                .SetDescription("ArtiUnarmedSpellstrike.Description")
                .SetIcon(USSicon)
                .SetBuff(ArtiSpellstrikeBuff)
                //.SetBuff(BuffRefs.SpellStrikeBuff.ToString())
                .SetActivationType(Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free)
                .SetActivateOnUnitAction(Kingmaker.UnitLogic.ActivatableAbilities.AbilityActivateOnUnitActionType.Attack)
                .SetDeactivateImmediately(true)
                .AddDeactivateTrigger(conditions: ConditionsBuilder.New().IsWeaponEquipped(category: WeaponCategory.Longspear, slot: ContextConditionIsWeaponEquipped.CheckedSlot.PrimaryHand, checkOnCaster: true, checkWeaponCategory: true, checkSlot: true, negate: false).Build())
                .Configure();

            var ArtiUnarmedSpellstrikeFeature = FeatureConfigurator.New(ArtiSpellstrikeFeatureName, ArtiSpellstrikeFeatureGuid)
                .SetDisplayName("ArtiUnarmedSpellstrikeFeature.Name")
                .SetDescription("ArtiUnarmedSpellstrikeFeature.Description")
                .SetIcon(USSicon)
                .AddFacts([ArtiUnarmedSpellstrike])
                .AddComponent(new AddMagusMechanicPart { m_Feature = AddMagusMechanicPart.Feature.Spellstrike})
                .Configure();
            /* UNARMED SPELLSTRIKE END */

            /* EMPOWERED ARM */

            /*var EmpArmBuff = BuffConfigurator.New(ArtiEmpArmBuffName, ArtiEmpArmBuffGuid)
                .AddBuffEnchantSpecificWeaponWorn(enchantmentBlueprint: WeaponEnchantmentRefs.TemporaryEnhancement5.ToString(), weaponBlueprint:GolemArmWeapon)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();
            var EmpArm = FeatureConfigurator.New(ArtiEmpArmName, ArtiEmpArmGuid)
                .SetDisplayName("EmpArms.Name")
                .SetDescription("EmpArms.Description")
                .AddFacts([EmpArmBuff])
                .Configure();*/

            ArchetypeConfigurator.For(JistkanArtificerArchetype)
                .AddToAddFeatures(1, ArtiDiminishedSpellcasting)
                .AddToAddFeatures(1, GolemArmFeature)
                .AddToAddFeatures(1, ArtiUnarmedStrike)
                .AddToAddFeatures(2, ArtiUnarmedSpellstrikeFeature)
                //.AddToAddFeatures(2, EmpArm)
                .AddToAddFeatures(5, ArtiEnchantPlus2)

                .AddToRemoveFeatures(2, FeatureRefs.SpellStrikeFeature.ToString())
                .AddToRemoveFeatures(5, FeatureRefs.ArcaneWeaponPlus2.ToString())
                .Configure();

        }
    }
}
