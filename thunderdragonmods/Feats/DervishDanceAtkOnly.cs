using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Components.Replacements;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Kingdom.Buffs;
using Kingmaker.UI.Tooltip;
using Kingmaker.UnitLogic.FactLogic;

namespace thunderdragonmods.Feats
{
    static class DervishDanceAtkOnly
    {
        private static readonly string FeatName = "Dervish Dance";
        private static readonly string FeatGuid = "D1ABF3B5-A82E-4C3D-8F64-99DD9BA27722";
   
        public static void Configure()
        {
            var Scimitar = BlueprintTool.Get<BlueprintWeaponType>("d9fbec4637d71bd4ebc977628de3daf3");
            var WeaponFinesse = BlueprintTool.Get<BlueprintFeature>("90e54424d682d104ab36436bd527af09");

            var statchange = new AttackStatReplacementFixed(
                replacementStat: StatType.Dexterity, 
                weaponTypes: Scimitar
                );
        
            var DervishDance = FeatureConfigurator.New(FeatName, FeatGuid, Kingmaker.Blueprints.Classes.FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName("DervishDanceAtkOnly.Name")
                .SetDescription("DervishDanceAtkOnly.Description")
                .AddPrerequisiteFeature(WeaponFinesse)
                .SetIsClassFeature(true)
                .AddPrerequisiteStatValue(stat: StatType.Dexterity, value: 13)
                .AddPrerequisiteStatValue(stat:StatType.SkillMobility, value: 2)
                .AddComponent(statchange)
                .Configure();
     
        }
    }
}
