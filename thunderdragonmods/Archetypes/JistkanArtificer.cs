using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;

namespace thunderdragonmods.Archetypes
{
    internal class JistkanArtificer
    {
        private static readonly string ArtiName = "JistkanArtificerArchetype";
        private static readonly string ArtiGuid = "CDB9F8E7-2031-40BF-A77B-E3CB3ABADDE4";

        private static readonly string GolemArmName = "GolemArm.Arti";
        private static readonly string GolemArmGuid = "92356ADE-ED6D-44C1-925C-5C7527164272";
        public static void Configure()
        {
            var icon = AbilityRefs.StoneFist.Reference.Get().Icon;
            var JistkanArtificerArchetype = ArchetypeConfigurator.New(ArtiName, ArtiGuid, clazz: CharacterClassRefs.MagusClass)
                .SetLocalizedName("JistkanArti.Name")
                .SetLocalizedDescription("JistkanArti.Description")
                .Configure();

            var GolemArmFeature = FeatureConfigurator.New(GolemArmName, GolemArmGuid)
                .SetDisplayName("GolemArm.Name")
                .SetDescription("GolemArm.Description")
                .SetIcon(icon)
                .Configure();

            ArchetypeConfigurator.For(JistkanArtificerArchetype)
                .AddToAddFeatures(1, GolemArmFeature)
                .Configure();
        }
    }
}
