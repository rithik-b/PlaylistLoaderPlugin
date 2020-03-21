using System;
using IPA;
using IPA.Config;
using Logger = IPA.Logging.Logger;
using System.Reflection;
using HarmonyLib;
using PlaylistLoaderLite.UI;

namespace PlaylistLoaderLite
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        #region Properties

        public static Logger Log { get; private set; }

        public static Plugin instance;
        internal static string Name => "PlaylistLoaderLite";
        public const string HarmonyId = "com.github.rithik-b.PlaylistLoaderLite";
        internal static Harmony harmony;

        #endregion

        #region BSIPA Events

        [Init]
        public Plugin(Logger logger, Config conf)
        {
            Log = logger;
            Log.Debug("Logger initialised.");
            harmony = new Harmony(HarmonyId);
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            ApplyHarmonyPatches();
            PluginUI.instance.Setup();
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");
        }

        #endregion

        #region Methods

        public static void ApplyHarmonyPatches()
        {
            try
            {
                Log.Debug("Applying Harmony patches.");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Log.Critical("Error applying Harmony patches: " + ex.Message);
                Log.Debug(ex);
            }
        }

        #endregion

        #region BSIPA Config
        // Uncomment to use BSIPA's config
        //internal static Ref<PluginConfig> config;
        //internal static IConfigProvider configProvider;
        //public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        //{
        //    Logger.log = logger;
        //    Logger.log.Debug("Logger initialised.");

        //    configProvider = cfgProvider;

        //    config = configProvider.MakeLink<PluginConfig>((p, v) =>
        //    {
        //        // Build new config file if it doesn't exist or RegenerateConfig is true
        //        if (v.Value == null || v.Value.RegenerateConfig)
        //        {
        //            Logger.log.Debug("Regenerating PluginConfig");
        //            p.Store(v.Value = new PluginConfig()
        //            {
        //                // Set your default settings here.
        //                RegenerateConfig = false
        //            });
        //        }
        //        config = v;
        //    });
        //}
        #endregion

    }
}
