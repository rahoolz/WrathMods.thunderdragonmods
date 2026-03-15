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
        private static readonly string SacredFistLvlAcBonusName = "SacredFistLvlAcBonus";
        private static readonly string SacredFistLvlAcBonusGuid = "FAC3E288-735B-4483-8DF3-2656464B8D0A";
        private static readonly string SacredFistAcBonusBuffName = "SacredFistAcBonusBuff";
        private static readonly string SacredFistAcBonusBuffGuid = "81F96746-029C-420E-9500-3D16DCC9FBC8";
        private static readonly string AcBonusName = "SacredFistAcBonus";
        private static readonly string AcBonusGuid = "59DE3E62-1EE4-472D-9430-637DC5AF0F83";
 

        private static readonly BlueprintFeature WarpriestProficiency = BlueprintTool.Get<BlueprintFeature>("ad29d445f1534474db8295a61e42d08b");
        private static readonly BlueprintFeature MonkProficiency = BlueprintTool.Get<BlueprintFeature>("c7d6f5244c617734a8a76b6785a752b4");
        private static readonly BlueprintFeature SacredWeapon = BlueprintTool.Get<BlueprintFeature>("a3d148e3817044e4a33cda6c5fd79204");
        private static readonly BlueprintFeature FocusedWeapon = BlueprintTool.Get<BlueprintFeature>("ac384183dbfbbd7499410a21d749bef1");
        private static readonly BlueprintFeature WarpriestFeat = BlueprintTool.Get<BlueprintFeature>("303fd456ddb14437946e344bad9a893b");
        private static readonly BlueprintFeature WarpriestSacredArmor = BlueprintTool.Get<BlueprintFeature>("35e2d9525c240ce4c8ae47dd387b6e53");
       
        public static void Configure()
        {
            var SacredFistArchetype = ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, CharacterClassRefs.WarpriestClass)
                .SetLocalizedName("SacredFistTT.Name")
                .SetLocalizedDescription("SacredFistTT.Description");

            LogWrapper logger = LogWrapper.Get("THTDMods");
            logger.Info(BlueprintTool.Get<BlueprintArchetype>(ArchetypeName).ToString());

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

            var SacredFistUnarmored = new MonkNoArmorFeatureUnlock();

            var SacredFistWisAc = ContextRankConfigs.StatBonus(StatType.Wisdom, type: AbilityRankType.DamageDice);
            var SacredFistLvlAc = ContextRankConfigs
                .SumClassLevelWithArchetype(
                archetypes:[ArchetypeRefs.InstinctualWarriorArchetype.ToString()
                ],
                classes: [CharacterClassRefs.MonkClass.ToString(),
                    CharacterClassRefs.ShifterClass.ToString(),
                    CharacterClassRefs.BarbarianClass.ToString(),
                    CharacterClassRefs.WarpriestClass.ToString()],
                type: AbilityRankType.DamageBonus,
                max:20)
                .WithDivStepProgression(divisor: 4);

            var SacredFistLvlAcBonus = BuffConfigurator.New(SacredFistLvlAcBonusName, SacredFistLvlAcBonusGuid)
                .AddContextRankConfig(SacredFistLvlAc)
                .AddContextStatBonus(stat: StatType.AC, value: ContextValues.Rank(type: AbilityRankType.DamageBonus), descriptor: ModifierDescriptor.UntypedStackable)
                .AddContextStatBonus(stat: StatType.AdditionalCMD, value: ContextValues.Rank(type: AbilityRankType.DamageBonus), descriptor: ModifierDescriptor.UntypedStackable)
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
                .Configure();

            /* var SacredFistAcUnlock = FeatureConfigurator.New(SacredFistAcBonusUnlockName, SacredFistAcBonusUnlockGuid)
                .AddMonkNoArmorFeatureUnlock(SacredFistAcBonus)
                .SetDisplayName("SacredFistAcBonus.Name")
                .SetDescription("SacredFistAcBonus.Description")
                .Configure();
            */

            SacredFistArchetype
                .AddToRemoveFeatures(1, WarpriestProficiency);

            SacredFistArchetype
                .AddToAddFeatures(1, SacredFistProficiencies)
                .AddToAddFeatures(1, SacredFistAcBonus);

            SacredFistArchetype.Configure();


        }

    }
}
