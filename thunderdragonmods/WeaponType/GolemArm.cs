using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Utility;

namespace thunderdragonmods.WeaponType
{
    internal class GolemArm
    {
        private static readonly string GolemArmName = "GolemArm.Weapon";
        private static readonly string GolemArmGuid = "5A7A2408-1927-4442-A5D0-2D729B6C0C5A";

        private static readonly string GolemArmStdName = "GolemArm.Weapon.Standard";
        private static readonly string GolemArmStdGuid = "EB7AA3AA-2ED1-4E61-B26B-740B369B6E14";

        private static readonly BlueprintWeaponType PunchingDaggerBP = BlueprintTool.Get<BlueprintWeaponType>("fcca8e6b85d19b14786ba1ab553e23ad");

        public static void Configure()
        {
            var GolemArm = WeaponTypeConfigurator.New(GolemArmName, GolemArmGuid)
                .CopyFrom(blueprint: WeaponTypeRefs.PunchingDagger)
                .SetTypeNameText("GolemArm.Name")
                .SetDefaultNameText("GolemArm.Name")
                .SetIsUnarmed(true)
                .SetBaseDamage(baseDamage: new DiceFormula(1, diceType: DiceType.D6))
                .SetAttackType(AttackType.Melee)
                .SetAttackRange(new Feet(5))
                .Configure();

            var icon = AbilityRefs.StoneFist.Reference.Get().Icon;
            
            /* WeaponTypeConfigurator.For(GolemArm)
                .SetTypeNameText("GolemArm.Name")
                .SetDefaultNameText("GolemArm.Name")
                //.SetCategory(Kingmaker.Enums.WeaponCategory.PunchingDagger)
                //.SetVisualParameters(Visual)
                .SetAttackType(AttackType.Melee)
                .SetBaseDamage(baseDamage: new DiceFormula(1, diceType: DiceType.D6))
                //.SetDamageType(damageType: DmgType)
                .SetAttackRange(attackRange: new Feet(5))
                .SetAttackType(AttackType.Melee)
                .SetIcon(icon)
                .SetIsLight(true)
                //.SetIsMonk(true)
                //.SetIsNatural(false)
                //.SetIsUnarmed(true)
                .SetIsOneHanded(true)
                .Configure(); */

            var StandardGolemArm = ItemWeaponConfigurator.New(GolemArmStdName, GolemArmStdGuid)
                .CopyFrom(ItemWeaponRefs.StandardPunchingDagger)
                .SetDisplayNameText("GolemArm.Name")
                .SetType(type: GolemArm)
                .SetIcon(icon)
                .SetEnchantments(enchantments: ["6b38844e2bffbac48b63036b66e735be"]) //Enchantment: Masterwork
                .Configure();
        }
    }
}
