using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace thunderdragonmods.Feats
{
    static class MartialAptitude
    {
        private static readonly string FeatName = "MartialAptitude";
        private static readonly string FeatGuid = "9D7860D0-0CF1-4367-A014-24873C2B5508";
        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, FeatGuid, Kingmaker.Blueprints.Classes.FeatureGroup.Feat)
                .SetDisplayName("MartialAptitude.Name")
                .SetDescription("MartialAptitude.Description")
                .AddFeatureTagsComponent(FeatureTag.Skills)
                .AddBuffSkillBonus(stat: Kingmaker.EntitySystem.Stats.StatType.SkillAthletics, value: 2, descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable)
                .AddBuffSkillBonus(stat: Kingmaker.EntitySystem.Stats.StatType.SkillMobility, value: 2, descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable)
                .AddBuffSkillBonus(stat: Kingmaker.EntitySystem.Stats.StatType.AdditionalAttackBonus, value: 1, descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable)
                .Configure();
        }
    }
}
