using System.Collections.Generic;
using System.Linq;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace pentagild;

public unsafe class Inventory {

  private static List<ItemWithParam>? _items;
  
  public static List<ItemWithParam>? Scan() {
    if (_items is not null) return _items;
    var manager = InventoryManager.Instance();
    if (manager is null) return null;
    var inventory = manager->GetInventoryContainer(InventoryType.EquippedItems);

    // var items =  new ReadOnlySpan<GameInventoryItem>(inventory->Items, (int)inventory->Size);

    var itemSheet = Services.DataManager.GetExcelSheet<ItemWithParam>();
    if (itemSheet is null) return null;
    var query = from item in itemSheet
                where item.EquipSlotCategory.Value?.MainHand == 1
                  && (item.ClassJobCategory.Value?.AST ?? false)
                  && item.LevelEquip >= 99
                  && item.LevelItem.Value?.RowId > 600
                select item;
    _items = query.ToList();
    return _items;
  }
}
