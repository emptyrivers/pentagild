using System;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace pentagild.Windows;

public class MainWindow() : Window("pentagild"), IDisposable {
    public override void Draw() {
        ImGui.Text("Hello, world!");
    }

    public void Dispose() { }
}
