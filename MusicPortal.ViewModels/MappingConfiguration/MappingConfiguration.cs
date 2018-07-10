using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MusicPortal.ViewModels.MappingConfiguration {
    public class MappingConfiguration {
        private static readonly string profilesNamespace = "MusicPortal.ViewModels.MappingProfiles";

        public static void Configure() {
            var profiles = GetProfilesFromAssembly();

            Mapper.Initialize(cfg => {
                cfg.AddProfiles(profiles);
            });
        }

        public static MapperConfiguration GetConfiguration() {
            var profiles = GetProfilesFromAssembly();

            return new MapperConfiguration(cfg => {
                cfg.AddProfiles(profiles);
            });
        }

        private static List<Type> GetProfilesFromAssembly() {
            return Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && x.Namespace == profilesNamespace).ToList();
        }
    }
}
