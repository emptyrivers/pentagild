using System;
using System.Text;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace pentagild.Windows;

public class MainWindow() : Window("pentagild"), IDisposable {
    public override unsafe void Draw() {
        // GameInventoryItem item =
        // Dalamud.IoC.
        ImGui.Text("Hello, world!");
        // RaptureGearsetModule* module = RaptureGearsetModule.Instance();
        // if (module == null) {
        //     ImGui.Text("GearSet module is null");
        //     return;
        // }

        // var gearSetIndex = module->CurrentGearsetIndex;
        // if (gearSetIndex < 0) {
        //     ImGui.Text("No gearset selected");
        // } else if (module->IsValidGearset(gearSetIndex)) {
        //     ImGui.Text($"Current gearset: {gearSetIndex}");
        // } else {
        //     ImGui.Text("Invalid gearset selected");
        // }
        // if (ImGui.Button("Equip set 60")) {
        //     module->EquipGearset(60);
        // }
        // var gearset = module->GetGearset(60);
        // for (var i = 0; i < gearset->Items.Length; i++) {
        //     var itemID = gearset->Items[i].ItemId;
        //     ImGui.Text($"Slot {i}: {itemID}:");
        // }
        var items = Inventory.Scan();
        if (items is null) return;
        foreach (var item in items) {
            ImGui.Text($"{item.RowId} : {item.Name}, ilvl {item.LevelItem.Value?.RowId ?? 0}, materia slots {item.MateriaSlotCount}{(item.
            IsAdvancedMeldingPermitted ? "(5)" : "")} ");
            var ilvl = item.LevelItem.Value;
            if (ilvl is null) {
                continue;
            }
            ImGui.SameLine();
            StringBuilder sb = new();
            for (var i = 0; i < item.BaseParams.Length; i++) {
                if (item.BaseParams[i].Id == 0) continue;
                var bp = item.BaseParams[i].BaseParam.Value;
                if (bp is null) continue;
                sb.Append($"{bp.Name} = {item.BaseParams[i].Value}, ");
            }
            sb.Append(" | " );
            for (var i = 0; i < item.SpecialParams.Length; i++) {
                if (item.SpecialParams[i].Id == 0) continue;
                var sp = item.SpecialParams[i].BaseParam.Value;
                if (sp is null) continue;
                sb.Append($"{sp.Name} = {item.SpecialParams[i].Value}, ");
            }
            // sb.Append($" Craftsmanship: {ilvl.Craftsmanship}, Control: {ilvl.Control}, CP: {ilvl.CP}");
            ImGui.Text(sb.ToString());
        }
    }

    public void Dispose() { }
}
