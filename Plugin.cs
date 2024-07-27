using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using pentagild.Windows;
using Dalamud.Game.Command;
namespace pentagild;

public class Plugin : IDalamudPlugin {
    public const string pg1 = "/pentagild";
    public const string pg2 = "/pg";
    public const string pgConfig = "/pgcfg";

    public Configuration Configuration;

    public readonly WindowSystem WindowSystem = new("pentagild");
    public MainWindow MainWindow;
    public ConfigWindow ConfigWindow;

    public Plugin(IDalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Services>();

        Configuration = Services.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        MainWindow = new MainWindow();
        ConfigWindow = new ConfigWindow(Configuration);
        WindowSystem.AddWindow(MainWindow);
        WindowSystem.AddWindow(ConfigWindow);

        Services.PluginInterface.UiBuilder.Draw += DrawUi;
        Services.PluginInterface.UiBuilder.OpenMainUi += ToggleMainUi;
        Services.PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUi;

        Services.CommandManager.AddHandler(pg1, new CommandInfo(OnCommand) {
            HelpMessage = "Open the main window"
        });
        Services.CommandManager.AddHandler(pg2, new CommandInfo(OnCommand) {
            HelpMessage = "Open the main window"
        });
        Services.CommandManager.AddHandler(pgConfig, new CommandInfo(OnCommand) {
            HelpMessage = "Open the config window"
        });
    }

    public void Dispose() {
        Services.CommandManager.RemoveHandler(pg1);
        Services.CommandManager.RemoveHandler(pg2);
        Services.CommandManager.RemoveHandler(pgConfig);

        Configuration.Save();

        WindowSystem.RemoveAllWindows();
        MainWindow.Dispose();
        ConfigWindow.Dispose();

        Services.PluginInterface.UiBuilder.Draw -= DrawUi;
        Services.PluginInterface.UiBuilder.OpenMainUi -= ToggleMainUi;
        Services.PluginInterface.UiBuilder.OpenConfigUi -= ToggleConfigUi;
    }

    private void DrawUi() => WindowSystem.Draw();
    private void ToggleMainUi() => MainWindow.Toggle();
    private void ToggleConfigUi() => ConfigWindow.Toggle();

    private unsafe void OnCommand(string command, string args) {
        switch (command) {
            case pg1:
            case pg2:
                if (args is "settings" or "config") {
                    ToggleConfigUi();
                } else {
                    ToggleMainUi();
                }
                break;
            case pgConfig:
                ToggleConfigUi();
                break;
        }
    }
}
