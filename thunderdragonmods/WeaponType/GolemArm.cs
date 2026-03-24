using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;

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
            

            var DmgType = new DamageTypeDescription();
            DmgType.Physical.Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Bludgeoning;

            var GolemArm = WeaponTypeConfigurator.New(GolemArmName, GolemArmGuid)
                .SetTypeNameText("GolemArm")
                .SetDefaultNameText("GolemArm")
                .SetCategory(Kingmaker.Enums.WeaponCategory.HeavyMace)
                .SetVisualParameters(Visual)
                .SetBaseDamage(baseDamage: new DiceFormula(1, diceType: DiceType.D6))
                .SetDamageType(damageType: DmgType)
                .SetIcon(icon)
                .SetIsMonk(true)
                .SetIsUnarmed(true)
                .SetIsOneHanded(true)
                .Configure();


            var StandardGolemFist = ItemWeaponConfigurator.New(GolemArmStdName, GolemArmStdGuid)
                .SetDisplayNameText("GolemArm.Name")
                .SetType(type: GolemArm)
                .SetIcon(icon)
                .Configure();
        }
    }
}
