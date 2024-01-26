using BepInEx;
using HarmonyLib;
using Showdown3.Commands;
using Showdown3.StateMachine.PluginState;
using ZeepSDK.ChatCommands;

namespace Showdown3;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("ZeepSDK")]
public class Plugin : BaseUnityPlugin
{
    private Harmony _harmony;
    private ShowdownContext _showdownContext;


    private void Awake()
    {
        _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        _harmony.PatchAll();

        ChatCommandApi.RegisterLocalChatCommand<CommandStart>();
        ChatCommandApi.RegisterLocalChatCommand<CommandStop>();
        ChatCommandApi.RegisterLocalChatCommand<CommandMatchStart>();
        ChatCommandApi.RegisterLocalChatCommand<CommandMatchInitiative>();
        ChatCommandApi.RegisterLocalChatCommand<CommandMatchSet>();
        ChatCommandApi.RegisterLocalChatCommand<CommandMatchStop>();
        ChatCommandApi.RegisterLocalChatCommand<CommandMatchCreate>();

        ChatCommandApi.RegisterMixedChatCommand<CommandBan>();
        ChatCommandApi.RegisterMixedChatCommand<CommandPick>();
        ChatCommandApi.RegisterMixedChatCommand<CommandReady>();


        _showdownContext = new ShowdownContext();
        // Plugin startup logic
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void OnDestroy()
    {
        _harmony?.UnpatchSelf();
        _harmony = null;
    }
}