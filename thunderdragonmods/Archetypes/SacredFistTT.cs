using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Microsoft.Build.Utilities;
using thunderdragonmods.Utils;

namespace thunderdragonmods.Archetypes
{
    internal class SacredFistTT
    {
        private static readonly string ArchetypeName = "SacredFistTTArchetype";
        private static readonly string ArchetypeGuid = "D623E184-A315-4CF4-A434-94B192ACDA4F";

        private static readonly string Proficiencies = "SacredFist.Proficiencies";
        private static readonly string ProficiencyGuid = "FA6019F9-34CF-46DD-BFB0-1EE4EE539B3D";

        private static readonly string SacredFistAcBonusUnlockName = "ACBonus.SacredFist.Unlock";
        private static readonly string SacredFistAcBonusUnlockGuid = "EE658E7B-564A-4E5E-B2C3-6954E568EB74";
        private static readonly string SacredFistLvlAcBonusName = "ACBonus.SacredFist.Lvl.Buff";
        private static readonly string SacredFistLvlAcBonusGuid = "FAC3E288-735B-4483-8DF3-2656464B8D0A";
        private static readonly string SacredFistAcBonusBuffName = "ACBonus.SacredFist.Buff";
        private static readonly string SacredFistAcBonusBuffGuid = "81F96746-029C-420E-9500-3D16DCC9FBC8";
        private static readonly string AcBonusName = "ACBonus.SacredFist";
        private static readonly string AcBonusGuid = "59DE3E62-1EE4-472D-9430-637DC5AF0F83";

        private static readonly string SacredFistFoBName = "FlurryOfBlows.SacredFist";
        private static readonly string SacredFistFoBGuid = "CF65AE37-A9B1-452B-9B10-416E54692BE0";
        private static readonly string SacredFistFoBUnlockName = "FlurryOfBlows.SacredFist.Unlock";
        private static readonly string SacredFistFoBUnlockGuid = "43CB4D74-4D3F-4ADB-9CA7-F80475E4A8A9";
        private static readonly string SacredFistFoB11Name = "FlurryOfBlows.SacredFist.11";
        private static readonly string SacredFistFoB11Guid = "0F938DE4-8A6C-4991-8E68-E8BEA6FC5F8E";
        private static readonly string SacredFistFoB11UnlockName = "FlurryOfBlows.SacredFist.11.Unlock";
        private static readonly string SacredFistFoB11UnlockGuid = "8909303D-9838-4C37-9CFE-776452659C73";

        private static readonly string SacredFistUnarmedStrikeName = "UnarmedStrike.SacredFist.Feature";
        private static readonly string SacredFistUnarmedStrikeGuid = "EE82648D-EB2A-40C9-863F-FEF2ABD36C2E";

        private static readonly string SacredFistBlessedFortitudeName = "BlessedFortitude.SacredFist";
        private static readonly string SacredFistBlessedFortitudeGuid = "E9B66BF1-F95D-4724-905C-2DCC447BC3D3";
        private static readonly string SacredFistMiraculousFortitudeName = "MiraculousFortitude.SacredFist";
        private static readonly string SacredFistMiraculousFortitudeGuid = "22616C69-1E63-4A39-B765-522C0028799F";

        private static readonly string SacredFistBonusStyleFeatName = "StyleFeat.SacredFist";
        private static readonly string SacredFistBonusStyleFeatGuid = "571FCD45-5866-4187-B4AA-722B9FCC8320";
        private static readonly string SacredFistAsMonkLvlName = "MonkLvl.SacredFist";
        private static readonly string SacredFistAsMonkLvlGuid = "BD040364-83C0-4A88-95BB-32E35DFEED37";

        private static readonly string SacredFistKiPoolFeatureName = "KiPoolFeature.SacredFist";
        private static readonly string SacredFistKiPoolFeatureGuid = "6717C2F1-E646-43CB-9885-57EC2C892319";
        private static readonly string SacredFistKiPoolResourceName = "KiPoolResource.SacredFist";
        private static readonly string SacredFistKiPoolResourceGuid = "52BC98C0-5643-4746-9CAF-527BA8650CE4";
        private static readonly string SacredFistKiPoolDodgeName = "KiPoolDodge.SacredFist";
        private static readonly string SacredFistKiPoolDodgeGuid = "4FE3284F-016B-432A-A5BE-6EF6E5FF7221";
        private static readonly string SacredFistKiPoolDodgeBuffName = "KiPoolDodge.Buff.SacredFist";
        private static readonly string SacredFistKiPoolDodgeBuffGuid = "1810A7A1-3C3C-4511-90BD-2CB8D35645BA";
        private static readonly string SacredFistKiPoolInsightName = "KiPoolInsight.SacredFist";
        private static readonly string SacredFistKiPoolInsightGuid = "32E079AC-50AE-4679-B24B-2C9897194913";
        private static readonly string SacredFistKiPoolInsightBuffName = "KiPoolInsightBuff.SacredFist";
        private static readonly string SacredFistKiPoolInsightBuffGuid = "4F81746D-8CA6-4E68-934A-E6050A68B6A7";

        private static readonly BlueprintFeature WarpriestProficiency = BlueprintTool.Get<BlueprintFeature>("ad29d445f1534474db8295a61e42d08b");
        private static readonly BlueprintFeature MonkProficiency = BlueprintTool.Get<BlueprintFeature>("c7d6f5244c617734a8a76b6785a752b4");
        private static readonly BlueprintFeature WarpriestSacredWeapon = BlueprintTool.Get<BlueprintFeature>("8eb5505ae69cc174fb1781134f949e5f");

        private static string SnakeStyle = "fa6ebb8e0d1f4b73bc155b759f244006";
        private static string SnakeSidewind = "d068ddacdb9b4016bfa31260902839e0";
        private static string SnakeFang = "49e036a82c2b4c07b596a4ae3973573e";

        public static void Configure()
        {
            var SacredFistArchetype = ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, CharacterClassRefs.WarpriestClass)
                .SetLocalizedName("SacredFistTT.Name")
                .SetLocalizedDescription("SacredFistTT.Description")
                .AddToClassSkills(classSkills: [StatType.SkillMobility, StatType.SkillPerception])
                .RemoveFromClassSkills(classSkills: [StatType.SkillPersuasion, StatType.SkillKnowledgeWorld])
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

            /* PREREQS */


            /* PREREQS END */

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
                .WithDivStepProgression(divisor:4);
            
            /*var SacredFistLvlAc = ContextRankConfigs
                .ClassLevel(classes: [CharacterClassRefs.WarpriestClass.ToString()], type: AbilityRankType.DamageBonus)
                .WithDivStepProgression(divisor: 1);*/
 
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
            /* var SacredFistFlurryOfBlows = FeatureConfigurator.New(SacredFistFoBName, SacredFistFoBGuid)
                .SetDisplayName("SacredFistFlurryOfBlows.Name")
                .SetDescription("SacredFistFlurryOfBlows.Description")
                .AddBuffExtraAttack(number: 1)
                .SetIsClassFeature(true)
                .Configure();
            */

           // var asd = new FakeFeats() { FakeFact = BlueprintTool.GetRef<BlueprintUnitFactReference>("fd99770e6bd240a4aab70f7af103e56a") };

            var SacredFistFlurryOfBlowsUnlock = FeatureConfigurator.New(SacredFistFoBUnlockName, SacredFistFoBUnlockGuid)
                .SetDisplayName("SacredFistFlurryOfBlows.Name")
                .SetDescription("SacredFistFlurryOfBlows.Description")
                //.AddMonkNoArmorAndMonkWeaponFeatureUnlock(newFact: SacredFistFlurryOfBlows)
                //.AddComponent(component: asd)
                .AddFacts(facts: [FeatureRefs.MonkFlurryOfBlowstUnlock.ToString()])
                .SetIsClassFeature(true)
                .SetIcon(Utils.ImportSprite.CreateSprite("thunderdragonmods.Icons.FlurryOfBlows.png"))
                .Configure();

            /*var SacredFistFlurryOfBlows11 = FeatureConfigurator.New(SacredFistFoB11Name, SacredFistFoB11Guid)
               .SetDisplayName("SacredFistFlurryOfBlows.Name")
               .SetDescription("SacredFistFlurryOfBlows.Description")
               .AddBuffExtraAttack(number: 1)
               .SetIsClassFeature(true)
               .SetHideInUI(true)
               .SetHideInCharacterSheetAndLevelUp(true)
               .SetIsPrerequisiteFor(isPrerequisiteFor: [FeatureRefs.CraneStyleFeat.ToString()])
               .Configure();
            */

            /* var SacredFistFlurryOfBlowsUnlock11 = FeatureConfigurator.New(SacredFistFoB11UnlockName, SacredFistFoB11UnlockGuid)
                .SetDisplayName("SacredFistFlurryOfBlows.Name")
                .SetDescription("SacredFistFlurryOfBlows.Description")
                .AddMonkNoArmorAndMonkWeaponFeatureUnlock(newFact: SacredFistFlurryOfBlows11)
                .SetIcon(Utils.ImportSprite.CreateSprite("thunderdragonmods.Icons.FlurryOfBlows.png"))
                .SetIsClassFeature(true)
                .SetHideInUI(false)
                .Configure();
            */
            /* FLURRY OF BLOWS SECTION END */

            /* UNARMED STRIKE BONUS FEAT DAMAGE */
            var SacredFistUnarmedStrike = FeatureConfigurator.New(SacredFistUnarmedStrikeName, SacredFistUnarmedStrikeGuid)
                .SetDisplayName("SacredFistUnarmedStrike.Name")
                .SetDescription("SacredFistUnarmedStrike.Description")
                .SetIsClassFeature(true)
                .AddFacts([FeatureRefs.ImprovedUnarmedStrike.ToString(), FeatureRefs.MonkUnarmedStrike.ToString()])
                .Configure();
            /* UNARMED STRIKE BONUS FEAT DAMAGE END */

            /* BLESSED MIRACULOUS FORTITUDE SECTION */
            var SacredFistBlessedFortitude = FeatureConfigurator.New(SacredFistBlessedFortitudeName, SacredFistBlessedFortitudeGuid)
                .SetDisplayName("SacredFistBlessedFortitude.Name")
                .SetDescription("SacredFistBlessedFortitude.Description")
                .AddEvasion(savingThrow: SavingThrowType.Fortitude)
                .SetIsClassFeature(true)
                .Configure();

            var SacredFistMiraculousFortitude = FeatureConfigurator.New(SacredFistMiraculousFortitudeName, SacredFistMiraculousFortitudeGuid)
                .SetDisplayName("SacredFistMiraculousFortitude.Name")
                .SetDescription("SacredFistMiraculousFortitude.Description")
                .AddImprovedEvasion(savingThrow: SavingThrowType.Fortitude)
                .SetIsClassFeature(true)
                .Configure();
            /* BLESSED MIRACULOUS FORTITUDE SECTION END */

            /* BONUS STYLE FEAT */
            var SacredFistStyleFeat = FeatureSelectionConfigurator.New(SacredFistBonusStyleFeatName, SacredFistBonusStyleFeatGuid, groups: FeatureGroup.StyleFeat)
                .SetDisplayName("SacredFistStyleFeat.Name")
                .SetDescription("SacredFistStyleFeat.Description")
                .SetIsClassFeature(true)
                .SetIgnorePrerequisites(false)
                .SetObligatory(true)
                .AddToAllFeatures(allFeatures: [
                    FeatureRefs.CraneStyleFeat.ToString(),
                    FeatureRefs.CraneStyleWingFeat.ToString(),
                    FeatureRefs.CraneStyleRiposteFeat.ToString(),
                    FeatureRefs.BoarStyle.ToString(),
                    FeatureRefs.BoarShred.ToString(),
                    FeatureRefs.BoarFerocity.ToString(),
                    FeatureRefs.DivaStyle.ToString(),
                    FeatureRefs.DivaStrike.ToString(),
                    FeatureRefs.DivaAdvance.ToString(),
                    FeatureRefs.DragonStyle.ToString(),
                    FeatureRefs.DragonFerocity.ToString(),
                    FeatureRefs.DragonRoarFeature.ToString(),
                    FeatureRefs.PummelingStyle.ToString(),
                    FeatureRefs.PummelingBully.ToString(),
                    FeatureRefs.PummelingCharge.ToString(),
                    FeatureRefs.ShaitanStyleFeature.ToString(),
                    FeatureRefs.ShaitanSkinFeature.ToString(),
                    FeatureRefs.ShaitanEarthblastFeature.ToString(),
                    ]) 
                .Configure();

            /* FeatureSelectionConfigurator.For(SacredFistStyleFeat)
                .AddToFeatureSelection([
                    SnakeStyle,
                    SnakeSidewind,
                    SnakeFang
                    ])
                .AddToAllFeatures([
                    SnakeStyle,
                    SnakeSidewind, 
                    SnakeFang
                    ])
                .Configure();
            */

            var SacredFistAsMonkLvl = FeatureConfigurator.New(SacredFistAsMonkLvlName, SacredFistAsMonkLvlGuid)
                .AddClassLevelsForPrerequisites(
                actualClass: CharacterClassRefs.WarpriestClass.ToString(),
                fakeClass: CharacterClassRefs.MonkClass.ToString(),
                forSelection: SacredFistStyleFeat)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetIsClassFeature(true)
                .Configure();
            /* BONUS STYLE FEAT END */

            /* KI POOL SECTION */
            var SacredFistKiPoolAmount = ResourceAmountBuilder.New(baseValue: 0)
                .IncreaseByStat(stat: StatType.Wisdom)
                .IncreaseByLevelStartPlusDivStep(classes: [CharacterClassRefs.WarpriestClass.ToString()], startingLevel: 4, bonusPerStep: 1, levelsPerStep: 2);

            var SacredFistKiPoolResource = AbilityResourceConfigurator.New(SacredFistKiPoolResourceName, SacredFistKiPoolResourceGuid)
                .SetLocalizedName("SacredFistKiPoolResource.Name")
                .SetMaxAmount(builder: SacredFistKiPoolAmount)
                .Configure();

            var SacredFistKiPoolDodgeBuff = BuffConfigurator.New(SacredFistKiPoolDodgeBuffName, SacredFistKiPoolDodgeBuffGuid)
                .SetDisplayName("SacredFistKiPoolDodge.Name")
                .SetDescription("SacredFistKiPoolDodge.Description")
                .AddStatBonus(stat: StatType.AC, descriptor: ModifierDescriptor.Dodge, value: 4)
                .Configure();

            var SacredFistKiPoolDodge = AbilityConfigurator.New(SacredFistKiPoolDodgeName, SacredFistKiPoolDodgeGuid)
                .SetDisplayName("SacredFistKiPoolDodge.Name")
                .SetDescription("SacredFistKiPoolDodge.Description")
                .SetIcon(icon: Utils.ImportSprite.CreateSprite(image: "thunderdragonmods.Icons.Dodge.png"))
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .SetActionType(actionType: Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(buff: SacredFistKiPoolDodgeBuff, durationValue: ContextDuration.Fixed(1)))
                .AddAbilityResourceLogic(requiredResource: SacredFistKiPoolResource, isSpendResource: true, amount: 1)
                .Configure();

            var SacredFistInsightScaling = ContextRankConfigs
                .ClassLevel(classes: [CharacterClassRefs.WarpriestClass.ToString()])
                .WithStartPlusDivStepProgression(divisor: 3, start: 7, delayStart: true);

            var SacredFistKiPoolInsightBuff = BuffConfigurator.New(SacredFistKiPoolInsightBuffName, SacredFistKiPoolInsightBuffGuid)
                .SetDisplayName("SacredFistKiPoolInsight.Name")
                .SetDescription("SacredFistKiPoolInsight.Description")
                .AddContextRankConfig(SacredFistInsightScaling)
                .AddContextStatBonus(stat:StatType.AC, value: ContextValues.Rank(AbilityRankType.Default), descriptor: ModifierDescriptor.Insight)
                .Configure();

            var SacredFistKiPoolInsight = AbilityConfigurator.New(SacredFistKiPoolInsightName, SacredFistKiPoolInsightGuid)
                .SetDisplayName("SacredFistKiPoolInsight.Name")
                .SetDescription("SacredFistKiPoolInsight.Description")
                .SetType(Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural)
                .SetActionType(actionType: Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .SetIcon(icon: Utils.ImportSprite.CreateSprite(image: "thunderdragonmods.Icons.InsightfulAvoid.png"))
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(buff: SacredFistKiPoolInsightBuff, durationValue: ContextDuration.Fixed(10)))
                .AddAbilityResourceLogic(requiredResource: SacredFistKiPoolResource, isSpendResource: true, amount: 1)
                .Configure();

            var SacredFistKiPool = FeatureConfigurator.New(SacredFistKiPoolFeatureName, SacredFistKiPoolFeatureGuid)
               .SetDisplayName("SacredFistKiPool.Name")
               .SetDescription("SacredFistKiPool.Description")
               .SetIsClassFeature(true)
               .AddAbilityResources(resource: SacredFistKiPoolResource, restoreAmount: true)
               .AddFacts([SacredFistKiPoolDodge, SacredFistKiPoolInsight])
               .Configure();

            /* KI POOL SECTION END */

            ArchetypeConfigurator.For(SacredFistArchetype)
                .AddToAddFeatures(1, SacredFistProficiencies)
                .AddToAddFeatures(1, SacredFistAcUnlock)
                .AddToAddFeatures(1, SacredFistFlurryOfBlowsUnlock)
                .AddToAddFeatures(1, SacredFistUnarmedStrike)
                .AddToAddFeatures(3, SacredFistBlessedFortitude)
                .AddToAddFeatures(4, FeatureRefs.MonkUnarmedStrikeLevel4.ToString())
                .AddToAddFeatures(6, SacredFistStyleFeat)
                .AddToAddFeatures(6, SacredFistAsMonkLvl)
                .AddToAddFeatures(7, SacredFistKiPool)
                .AddToAddFeatures(7, FeatureRefs.KiStrikeMagic.ToString())
                .AddToAddFeatures(8, FeatureRefs.MonkUnarmedStrikeLevel8.ToString())
                .AddToAddFeatures(9, SacredFistMiraculousFortitude)
                .AddToAddFeatures(11, FeatureRefs.MonkFlurryOfBlowstLevel11Unlock.ToString())
                .AddToAddFeatures(11, FeatureRefs.KiStrikeColdIronSilver.ToString())
                .AddToAddFeatures(12, SacredFistStyleFeat)
                .AddToAddFeatures(13, FeatureRefs.KiStrikeLawful.ToString())
                .AddToAddFeatures(12, FeatureRefs.MonkUnarmedStrikeLevel12.ToString())
                .AddToAddFeatures(16, FeatureRefs.MonkUnarmedStrikeLevel16.ToString())
                .AddToAddFeatures(18, SacredFistStyleFeat)
                .AddToAddFeatures(19, FeatureRefs.KiStrikeAdamantine.ToString())
                .AddToAddFeatures(20, FeatureRefs.MonkUnarmedStrikeLevel20.ToString())

                .AddToRemoveFeatures(1, WarpriestProficiency)
                .AddToRemoveFeatures(1, WarpriestSacredWeapon)
                .AddToRemoveFeatures(1, FeatureSelectionRefs.WarpriestWeaponFocusSelection.ToString())
                .AddToRemoveFeatures(3, FeatureSelectionRefs.WarpriestFeatSelection.ToString())
                .AddToRemoveFeatures(4, FeatureRefs.SacredWeaponEnchantFeature.ToString())
                .AddToRemoveFeatures(6, FeatureSelectionRefs.WarpriestFeatSelection.ToString())
                .AddToRemoveFeatures(7, FeatureRefs.SacredArmorFeature.ToString())
                .AddToRemoveFeatures(8, FeatureRefs.SacredWeaponEnchantPlus2.ToString())                
                .AddToRemoveFeatures(9, FeatureSelectionRefs.WarpriestFeatSelection.ToString())
                .AddToRemoveFeatures(10, FeatureRefs.SacredArmorEnchantPlus2.ToString())
                .AddToRemoveFeatures(12, FeatureRefs.SacredWeaponEnchantPlus3.ToString())
                .AddToRemoveFeatures(13, FeatureRefs.SacredArmorEnchantPlus3.ToString())
                .AddToRemoveFeatures(12, FeatureSelectionRefs.WarpriestFeatSelection.ToString())
                .AddToRemoveFeatures(16, FeatureRefs.SacredWeaponEnchantPlus4.ToString())
                .AddToRemoveFeatures(16, FeatureRefs.SacredArmorEnchantPlus4.ToString())
                .AddToRemoveFeatures(18, FeatureSelectionRefs.WarpriestFeatSelection.ToString())
                .AddToRemoveFeatures(19, FeatureRefs.SacredArmorEnchantPlus5.ToString())
                .AddToRemoveFeatures(20, FeatureRefs.SacredWeaponEnchantPlus5.ToString())

                /*------------------------------------------*/
                .Configure();
            ProgressionConfigurator.For(ProgressionRefs.WarpriestProgression)
                .AddToUIGroups(new Blueprint<BlueprintFeatureBaseReference>[] {
                    FeatureRefs.KiStrikeMagic.ToString(),
                    FeatureRefs.KiStrikeColdIronSilver.ToString(),
                    FeatureRefs.KiStrikeLawful.ToString(),
                    FeatureRefs.KiStrikeAdamantine.ToString()
                })
                .AddToUIGroups(new Blueprint<BlueprintFeatureBaseReference>[]
                {
                    SacredFistBlessedFortitude,
                    SacredFistMiraculousFortitude
                })
                .Configure();
        }

    }
}
