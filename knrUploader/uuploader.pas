unit uuploader;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, Vcl.Buttons, Data.DB,
  Vcl.Grids, Vcl.DBGrids;

type
  Tfouploader = class(TForm)
    BitBtn1: TBitBtn;
    DBGrid1: TDBGrid;
    dsKnr: TDataSource;
    BitBtn2: TBitBtn;
    procedure BitBtn1Click(Sender: TObject);
  private
    function MontarJson: string;
    { Private declarations }
  public
    { Public declarations }
  end;

var
  fouploader: Tfouploader;

implementation

{$R *.dfm}



uses utabelas,
     System.JSON,
     REST.JSON;

procedure Tfouploader.BitBtn1Click(Sender: TObject);
begin
   dm.SelecionarKnr(1,now);
end;

function tfoUploader.MontarJson : string ;
var jsonObj: TJSONObject;
    jsonArray: TJSONArray;
begin
   jsonObj   := TJSONObject.Create;
   jsonArray := TJSONArray.Create;
   dm.fdKnr.First;
   while (not dm.fdKnr.Eof) do
   begin
      jsonObj   := TJSONObject.Create;
      jsonObj.AddPair('planta','1');
      jsonObj.AddPair('knr',dm.fdKnr.FieldByName('knr').AsString);
      jsonObj.AddPair('status',TJSONNumber.Create(dm.fdKnr.FieldByName('status').asinteger));
      jsonArray.Add(jsonObj);
      dm.fdKnr.Next;
   end;
   result := jsonArray.ToString;
end;

end.
