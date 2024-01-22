using BepInEx;
using HarmonyLib;
using Showdown3.Commands;
using Showdown3.StateMachine;
using ZeepSDK.ChatCommands;
using ZeepSDK.Messaging;

namespace Showdown3;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("ZeepSDK")]
public class Plugin : BaseUnityPlugin
{
    private Harmony _harmony;
    private MasterStateMachine _masterStateMachine;
    

    private void Awake()
    {
        _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        _harmony.PatchAll();

        ChatCommandApi.RegisterLocalChatCommand<CommandStart>();
        ChatCommandApi.RegisterLocalChatCommand<CommandStop>();
        ChatCommandApi.RegisterLocalChatCommand<CommandContinue>();
        
        ChatCommandApi.RegisterMixedChatCommand<CommandBan>();
        ChatCommandApi.RegisterMixedChatCommand<CommandPick>();

         
        _masterStateMachine = new MasterStateMachine();
        // Plugin startup logic
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void OnDestroy()
    {
        _harmony?.UnpatchSelf();
        _harmony = null;
    }
}