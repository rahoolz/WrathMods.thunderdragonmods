using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using thunderdragonmods.WeaponType;

namespace thunderdragonmods.Archetypes
{
    internal class JistkanArtificer
    {
        private static readonly string ArtiName = "JistkanArtificerArchetype";
        private static readonly string ArtiGuid = "CDB9F8E7-2031-40BF-A77B-E3CB3ABADDE4";

        private static readonly string GolemArmName = "GolemArm.Arti";
        private static readonly string GolemArmGuid = "92356ADE-ED6D-44C1-925C-5C7527164272";
        private static readonly string GolemArmBuffName = "GolemArm.Buff.Arti";
        private static readonly string GolemArmBuffGuid = "EE6E4B29-F840-4773-8BC8-C73A099327CE";
        private static readonly string ArmResetName = "GolemArm.Reset.Arti";
        private static readonly string ArmResetGuid = "5B54D0E1-3484-418C-A53A-D372D089FCF2";

        private static readonly string ArtiUnarmedName = "UnarmedStrike.Arti";
        private static readonly string ArtiUnarmedGuid = "AB8EDEB4-FED9-4B3F-A49C-43353B03E785";
        public static void Configure()
        {
            /* GOLEM ARM FEATURE */
            var GolemArmWeapon = "GolemArm.Weapon.Standard";
            var icon = AbilityRefs.StoneFist.Reference.Get().Icon;
            var JistkanArtificerArchetype = ArchetypeConfigurator.New(ArtiName, ArtiGuid, clazz: CharacterClassRefs.MagusClass)
                .SetLocalizedName("JistkanArti.Name")
                .SetLocalizedDescription("JistkanArti.Description")
                .Configure();

            var GolemArmBuff = BuffConfigurator.New(GolemArmBuffName, GolemArmBuffGuid)
                .SetDisplayName("GolemArm.Name")
                .SetDescription("GolemArm.Description")
                .SetIcon(icon)
                .AddKineticistBlade(GolemArmWeapon)
                //.AddKineticistBlade(ItemWeaponRefs.AbruptForceItem.ToString())
                .SetStacking(Kingmaker.UnitLogic.Buffs.Blueprints.StackingType.Replace)
                .Configure();

            var GolemArmFeature = FeatureConfigurator.New(GolemArmName, GolemArmGuid)
                .SetDisplayName("GolemArm.Name")
                .SetDescription("GolemArm.Description")
                .AddFacts(facts: [GolemArmBuff])
                .SetIcon(icon)
                .Configure();

            var GolemArmReset = AbilityConfigurator.New(ArmResetName, ArmResetGuid)
                .SetDisplayName("GolemArm.Name")
                .SetDescription("GolemArm.Description")
                .AddFacts(facts: [GolemArmBuff])
                .Configure();

            FeatureConfigurator.For(GolemArmFeature)
                .AddFacts(facts: [GolemArmReset])
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

            ArchetypeConfigurator.For(JistkanArtificerArchetype)
                .AddToAddFeatures(1, GolemArmFeature)
                .AddToAddFeatures(1, ArtiUnarmedStrike)
                .Configure();

        }
    }
}
