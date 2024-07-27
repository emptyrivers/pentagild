

using Lumina;
using Lumina.Data;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

[Sheet("Item")]
public class ItemWithParam : Item {

  public struct ItemParam {
    public byte Id;
    public short Value;
    public LazyRow<BaseParam> BaseParam;
  }

  public ItemParam[] BaseParams = new ItemParam[6];
  public int BaseParamCount;
  public ItemParam[] SpecialParams = new ItemParam[6];
  public int SpecialParamCount;

  public override void PopulateData(RowParser parser, GameData gameData, Language language) {
    base.PopulateData(parser, gameData, language);

    for (var i = 0; i < 6; i++) {
      BaseParams[i] = new ItemParam {
        Id = parser.ReadColumn<byte>(59 + i * 2),
        Value = parser.ReadColumn<short>(60 + i * 2),
        BaseParam = new LazyRow<BaseParam>(gameData, parser.ReadColumn<byte>(59 + i * 2), language)
      };
    }

    for (var i = 0; i < 6; i++) {
      SpecialParams[i] = new ItemParam {
        Id = parser.ReadColumn<byte>(73 + i * 2),
        Value = parser.ReadColumn<short>(74 + i * 2),
        BaseParam = new LazyRow<BaseParam>(gameData, parser.ReadColumn<byte>(73 + i * 2), language)
      };
    }


  }

}
