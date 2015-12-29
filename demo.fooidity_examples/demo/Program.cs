using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using deme.fooidity_examples;
using Fooidity;
using Fooidity.Caching;
using Fooidity.CodeSwitches;

namespace demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();


            //custome config module
            cb.RegisterType<CodeFeatureStateCache>()
                .As<ICodeFeatureStateCache>()
                .As<IReloadCache>()
                .As<IUpdateCodeFeatureCache>()
                .SingleInstance();

            cb.RegisterType<Bob>()
               .As<ICodeFeatureStateCacheProvider>();

            ////
            

            cb.RegisterCodeSwitch<RedColor>();
            cb.RegisterSwitchedType<RedColor, ConsoleWriter, RedConsoleWriter, BlueConsoleWriter>();


            var c = cb.Build();

            c.Resolve<ConsoleWriter>().Write("hello");

//            c.Resolve<IToggleSwitchState<RedColor>>().Enabled = false;
//
//            c.Resolve<ConsoleWriter>().Write("hello");

            Console.ReadKey();
        }
    }

    public class Bob : ICodeFeatureStateCacheProvider
    {
        public async Task<bool> GetDefaultState()
        {
            bool defaultState = false;

            //load it up - return true if you found it
//            var configuration = ConfigurationManager.GetSection("fooidity") as FooidityConfiguration;
//            if (configuration != null)
//                defaultState = configuration.DefaultState;

            return true;
        }

        public async Task<IEnumerable<ICachedCodeFeatureState>> Load()
        {
            var codeFeatureStates = new List<ICachedCodeFeatureState>();

            var configuration = new Dictionary<string, FeatureBob>();
            configuration.Add("urn:feature:RedColor", new FeatureBob
            {
                Id = new CodeFeatureId("urn:feature:RedColor:deme.fooidity_examples"),
                Enabled = false
            });
            if (configuration != null)
            {
                if (configuration.Keys.Count > 0)
                {
                    foreach (var key in configuration.Keys)
                    {
                     
                        var feature = configuration[key];

                        var featureId = feature.Id;

                        Type codeFeatureType = featureId.GetType(false);
                        if (codeFeatureType == null)
                            throw new Exception("The feature type is not valid: " + feature.Id);

                        var state = new FeatureState(featureId, feature.Enabled);

                        codeFeatureStates.Add(state);
                    }
                }
            }

            return codeFeatureStates;
        }

        class FeatureState :
            ICachedCodeFeatureState
        {
            readonly bool _enabled;
            readonly CodeFeatureId _id;

            public FeatureState(CodeFeatureId id, bool enabled)
            {
                _enabled = enabled;
                _id = id;
            }

            public CodeFeatureId Id
            {
                get { return _id; }
            }

            public bool Enabled
            {
                get { return _enabled; }
            }
        }

        class FeatureBob
        {
            public CodeFeatureId Id { get; set; }
            public bool Enabled { get; set; }
        }
    }
}
