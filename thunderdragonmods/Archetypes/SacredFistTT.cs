using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Microsoft.Build.Utilities;

namespace thunderdragonmods.Archetypes
{
    internal class SacredFistTT
    {
        private static readonly string ArchetypeName = "SacredFistTTArchetype";
        private static readonly string ArchetypeGuid = "D623E184-A315-4CF4-A434-94B192ACDA4F";

        private static readonly string Proficiencies = "SacredFist.Proficiencies";
        private static readonly string ProficiencyGuid = "FA6019F9-34CF-46DD-BFB0-1EE4EE539B3D";

        private static readonly string SacredFistAcBonusUnlockName = "SacredFistAcBonusUnlock";
        private static readonly string SacredFistAcBonusUnlockGuid = "EE658E7B-564A-4E5E-B2C3-6954E568EB74";
        private static readonly string SacredFistLvlAcBonusName = "SacredFistLvlAcBonusBuff";
        private static readonly string SacredFistLvlAcBonusGuid = "FAC3E288-735B-4483-8DF3-2656464B8D0A";
        private static readonly string SacredFistAcBonusBuffName = "SacredFistAcBonusBuff";
        private static readonly string SacredFistAcBonusBuffGuid = "81F96746-029C-420E-9500-3D16DCC9FBC8";
        private static readonly string AcBonusName = "SacredFistAcBonus";
        private static readonly string AcBonusGuid = "59DE3E62-1EE4-472D-9430-637DC5AF0F83";

        private static readonly string SacredFistFoBName = "SacredFistFoB";
        private static readonly string SacredFistFoBGuid = "CF65AE37-A9B1-452B-9B10-416E54692BE0";
        private static readonly string SacredFistFoBUnlockName = "SacredFistFoBUnlock";
        private static readonly string SacredFistFoBUnlockGuid = "43CB4D74-4D3F-4ADB-9CA7-F80475E4A8A9";
        private static readonly string SacredFistFoB11Name = "SacredFistFoB11";
        private static readonly string SacredFistFoB11Guid = "0F938DE4-8A6C-4991-8E68-E8BEA6FC5F8E";
        private static readonly string SacredFistFoB11UnlockName = "SacredFistFoB11Unlock";
        private static readonly string SacredFistFoB11UnlockGuid = "8909303D-9838-4C37-9CFE-776452659C73";

        private static readonly string SacredFistUnarmedStrikeName = "SacredFistUnarmedStrikeFeature";
        private static readonly string SacredFistUnarmedStrikeGuid = "EE82648D-EB2A-40C9-863F-FEF2ABD36C2E";



        private static readonly BlueprintFeature WarpriestProficiency = BlueprintTool.Get<BlueprintFeature>("ad29d445f1534474db8295a61e42d08b");
        private static readonly BlueprintFeature MonkProficiency = BlueprintTool.Get<BlueprintFeature>("c7d6f5244c617734a8a76b6785a752b4");
        private static readonly BlueprintFeature WarpriestSacredWeapon = BlueprintTool.Get<BlueprintFeature>("8eb5505ae69cc174fb1781134f949e5f");
        private static readonly BlueprintFeature FocusedWeapon = BlueprintTool.Get<BlueprintFeature>("ac384183dbfbbd7499410a21d749bef1");
        private static readonly BlueprintFeature WarpriestFeat = BlueprintTool.Get<BlueprintFeature>("303fd456ddb14437946e344bad9a893b");
        private static readonly BlueprintFeature WarpriestSacredArmor = BlueprintTool.Get<BlueprintFeature>("35e2d9525c240ce4c8ae47dd387b6e53");
        private static readonly BlueprintFeature ImprovedUnarmedStrike = BlueprintTool.Get<BlueprintFeature>("7812ad3672a4b9a4fb894ea402095167");
        private static readonly BlueprintFeature MonkUnarmedDamage = BlueprintTool.Get<BlueprintFeature>("c3fbeb2ffebaaa64aa38ce7a0bb18fb0");

        public static void Configure()
        {
            var SacredFistArchetype = ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, CharacterClassRefs.WarpriestClass)
                .SetLocalizedName("SacredFistTT.Name")
                .SetLocalizedDescription("SacredFistTT.Description")
                .Configure();

            var SacredFistProficiencies = FeatureConfigurator.New(Proficiencies, ProficiencyGuid)
                .SetDisplayName(displayName: "SacredFistProficiencies.Name")
                .SetDescription(description: "SacredFistProficiencies.Description")
                .SetIsClassFeature(true)
                .AddProficiencies(weaponProficiencies: new WeaponCategory[]
                {
                    WeaponCategory.Club,
                    WeaponCategory.LightCrossbow,
                    WeaponCategory.HeavyCrossbow,
                    WeaponCategory.Dagger,
                    WeaponCategory.Handaxe,
                    WeaponCategory.Javelin,
                    WeaponCategory.Kama,
                    WeaponCategory.Nunchaku,
                    WeaponCategory.Quarterstaff,
                    WeaponCategory.Sai,
                    WeaponCategory.Shortspear,
                    WeaponCategory.Shortsword,
                    WeaponCategory.Dart,
                    WeaponCategory.SlingStaff,
                    WeaponCategory.Spear
                }
                )
                .Configure();

            /* AC BONUS SECTION */
            var SacredFistWisAc = ContextRankConfigs.StatBonus(StatType.Wisdom, type: AbilityRankType.DamageDice);
            var SacredFistLvlAc = ContextRankConfigs
                .SumClassLevelWithArchetype(
                classes: [CharacterClassRefs.MonkClass.ToString(),
                    CharacterClassRefs.ShifterClass.ToString(),
                    CharacterClassRefs.BarbarianClass.ToString(),
                    CharacterClassRefs.WarpriestClass.ToString()],
                archetypes:[ArchetypeRefs.InstinctualWarriorArchetype.ToString()
                ],
                type: AbilityRankType.DamageBonus,
                max:20)
                .WithDivStepProgression(divisor:1);
            
            /*var SacredFistLvlAc = ContextRankConfigs
                .ClassLevel(classes: [CharacterClassRefs.WarpriestClass.ToString()], type: AbilityRankType.DamageBonus)
                .WithDivStepProgression(divisor: 1);
            */

            var SacredFistLvlAcBonus = BuffConfigurator.New(SacredFistLvlAcBonusName, SacredFistLvlAcBonusGuid)
                .AddContextRankConfig(SacredFistLvlAc)
                .AddContextStatBonus(stat: StatType.AC, value: ContextValues.Rank(type: AbilityRankType.DamageBonus), descriptor: ModifierDescriptor.UntypedStackable, multiplier: 1)
                .SetIsClassFeature(true)
                .SetFlags(flags: [Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi, Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.StayOnDeath])
                .SetFrequency(DurationRate.Rounds)
                .SetStacking(Kingmaker.UnitLogic.Buffs.Blueprints.StackingType.Replace)
                .SetDisplayName("SacredFistLvlAcTag.Name")
                .Configure();

            var SacredFistWisAcBonus = BuffConfigurator.New(SacredFistAcBonusBuffName, SacredFistAcBonusBuffGuid)
                .AddContextRankConfig(SacredFistWisAc)
                .AddContextStatBonus(stat: StatType.AC, value: ContextValues.Rank(type: AbilityRankType.DamageDice), descriptor: ModifierDescriptor.UntypedStackable)
                .AddRecalculateOnStatChange(stat: StatType.Wisdom)
                .SetIsClassFeature(true)
                .SetFlags(flags: [Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.HiddenInUi, Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff.Flags.StayOnDeath])
                .SetFrequency(DurationRate.Rounds)
                .SetStacking(Kingmaker.UnitLogic.Buffs.Blueprints.StackingType.Replace)
                .SetDisplayName("SacredFistAcTag.Name")
                .Configure();

            var SacredFistAcBonus = FeatureConfigurator.New(AcBonusName, AcBonusGuid)
                .SetDisplayName("SacredFistAcBonus.Name")
                .SetDescription("SacredFistAcBonus.Description")
                .SetIsClassFeature(true)
                .AddFacts(facts:[SacredFistWisAcBonus, SacredFistLvlAcBonus])
                .SetReapplyOnLevelUp(true)
                .Configure();

            var SacredFistAcUnlock = FeatureConfigurator.New(SacredFistAcBonusUnlockName, SacredFistAcBonusUnlockGuid)
                .SetDisplayName("SacredFistAcBonus.Name")
                .SetDescription("SacredFistAcBonus.Description")
                .AddMonkNoArmorFeatureUnlock(SacredFistAcBonus)
                .SetIsClassFeature(true)
                .Configure();
            /* AC BONUS SECTION END */

            /* FLURRY OF BLOWS SECTION */
            var SacredFistFlurryOfBlows = FeatureConfigurator.New(SacredFistFoBName, SacredFistFoBGuid)
                .SetDisplayName("SacredFistFlurryOfBlows.Name")
                .SetDescription("SacredFistFlurryOfBlows.Description")
                .AddBuffExtraAttack(number: 1)
                .SetIsClassFeature(true)
                .Configure();

            var SacredFistFlurryOfBlowsUnlock = FeatureConfigurator.New(SacredFistFoBUnlockName, SacredFistFoBUnlockGuid)
                .SetDisplayName("SacredFistFlurryOfBlows.Name")
                .SetDescription("SacredFistFlurryOfBlows.Description")
                .AddMonkNoArmorAndMonkWeaponFeatureUnlock(newFact: SacredFistFlurryOfBlows)
                .SetIsClassFeature(true)
                .Configure();

            var SacredFistFlurryOfBlows11 = FeatureConfigurator.New(SacredFistFoB11Name, SacredFistFoB11Guid)
               .SetDisplayName("SacredFistFlurryOfBlows.Name")
               .SetDescription("SacredFistFlurryOfBlows.Description")
               .AddBuffExtraAttack(number: 1)
               .SetIsClassFeature(true)
               .SetHideInUI(true)
               .SetHideInCharacterSheetAndLevelUp(true)
               .SetIsPrerequisiteFor(isPrerequisiteFor: [FeatureRefs.CraneStyleFeat.ToString()])
               .Configure();

            var SacredFistFlurryOfBlowsUnlock11 = FeatureConfigurator.New(SacredFistFoB11UnlockName, SacredFistFoB11UnlockGuid)
                .SetDisplayName("SacredFistFlurryOfBlows.Name")
                .SetDescription("SacredFistFlurryOfBlows.Description")
                .AddMonkNoArmorAndMonkWeaponFeatureUnlock(newFact: SacredFistFlurryOfBlows11)
                .SetIsClassFeature(true)
                .SetHideInUI(true)
                .Configure();
            /* FLURRY OF BLOWS SECTION END */

            /* UNARMED STRIKE BONUS FEAT DAMAGE */
            var SacredFistUnarmedStrike = FeatureConfigurator.New(SacredFistUnarmedStrikeName, SacredFistUnarmedStrikeGuid)
                .SetDisplayName("SacredFistUnarmedStrike.Name")
                .SetDescription("SacredFistUnarmedStrike.Description")
                .SetIsClassFeature(true)
                .AddFacts([FeatureRefs.ImprovedUnarmedStrike.ToString(), FeatureRefs.MonkUnarmedStrike.ToString()])
                .Configure();

            ArchetypeConfigurator.For(SacredFistArchetype)
                .AddToAddFeatures(1, SacredFistProficiencies)
                .AddToAddFeatures(1, SacredFistAcUnlock)
                .AddToAddFeatures(1, SacredFistFlurryOfBlowsUnlock)
                .AddToAddFeatures(11, SacredFistFlurryOfBlowsUnlock11)
                .AddToAddFeatures(1, SacredFistUnarmedStrike)
                .AddToAddFeatures(4, FeatureRefs.MonkUnarmedStrikeLevel4.ToString())
                .AddToAddFeatures(8, FeatureRefs.MonkUnarmedStrikeLevel8.ToString())
                .AddToAddFeatures(12, FeatureRefs.MonkUnarmedStrikeLevel12.ToString())
                .AddToAddFeatures(16, FeatureRefs.MonkUnarmedStrikeLevel16.ToString())
                .AddToAddFeatures(20, FeatureRefs.MonkUnarmedStrikeLevel20.ToString())

                .AddToRemoveFeatures(1, WarpriestProficiency)
                .AddToRemoveFeatures(1, WarpriestSacredWeapon)
                .AddToRemoveFeatures(1, FeatureSelectionRefs.WarpriestWeaponFocusSelection.ToString())
                .AddToRemoveFeatures(3, FeatureRefs.SacredWeaponEnchantFeature.ToString())
                .AddToRemoveFeatures(3, FeatureRefs.SacredWeaponEnchantPlus2.ToString())
                .AddToRemoveFeatures(3, FeatureRefs.SacredWeaponEnchantPlus3.ToString())
                .AddToRemoveFeatures(3, FeatureRefs.SacredWeaponEnchantPlus4.ToString())
                .AddToRemoveFeatures(3, FeatureRefs.SacredWeaponEnchantPlus5.ToString())
                /*------------------------------------------*/

                .Configure();
        }

    }
}
