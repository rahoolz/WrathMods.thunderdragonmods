using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
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

        public static void Configure()
        {
            var icon = AbilityRefs.StoneFist.Reference.Get().Icon;
            var Visual = new WeaponVisualParameters();
            Visual.m_WeaponAnimationStyle = Kingmaker.View.Animation.WeaponAnimationStyle.Fist;
            Visual.m_WeaponModel = null;
            Visual.m_WeaponBeltModelOverride = null;
            Visual.m_WeaponSheathModelOverride = null;


            var DmgType = new DamageTypeDescription();
            DmgType.Physical.Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Bludgeoning;
            var range = new Feet();
            range.m_Value = 5;


            var GolemArm = WeaponTypeConfigurator.New(GolemArmName, GolemArmGuid)
                .SetTypeNameText("GolemArm.Name")
                .SetDefaultNameText("GolemArm.Name")
                .SetCategory(Kingmaker.Enums.WeaponCategory.PunchingDagger)
                .SetVisualParameters(Visual)
                .SetAttackType(AttackType.Melee)
                .SetBaseDamage(baseDamage: new DiceFormula(1, diceType: DiceType.D6))
                .SetDamageType(DmgType)
                .SetAttackRange(attackRange: range)
                .SetIcon(icon)
                .SetIsLight(true)
                .SetIsMonk(true)
                .SetIsNatural(false)
                .SetIsUnarmed(true)
                .SetIsOneHanded(true)
                .Configure();            

            var StandardGolemArm = ItemWeaponConfigurator.New(GolemArmStdName, GolemArmStdGuid)
                .SetDisplayNameText("GolemArm.Name")
                .SetType(type: GolemArm)
                .SetIcon(icon)
                .Configure();
        }
    }
}
