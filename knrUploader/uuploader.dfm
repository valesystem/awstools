object fouploader: Tfouploader
  Left = 0
  Top = 0
  Caption = 'KNR Uploader'
  ClientHeight = 441
  ClientWidth = 624
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Segoe UI'
  Font.Style = []
  TextHeight = 15
  object BitBtn1: TBitBtn
    Left = 16
    Top = 16
    Width = 153
    Height = 33
    Caption = 'Selecao Inicial'
    TabOrder = 0
    OnClick = BitBtn1Click
  end
  object DBGrid1: TDBGrid
    Left = 16
    Top = 64
    Width = 321
    Height = 120
    TabOrder = 1
    TitleFont.Charset = DEFAULT_CHARSET
    TitleFont.Color = clWindowText
    TitleFont.Height = -12
    TitleFont.Name = 'Segoe UI'
    TitleFont.Style = []
  end
  object BitBtn2: TBitBtn
    Left = 184
    Top = 16
    Width = 153
    Height = 33
    Caption = 'UpLoad'
    TabOrder = 2
    OnClick = BitBtn1Click
  end
  object dsKnr: TDataSource
    DataSet = dm.fdKnr
    Left = 480
    Top = 24
  end
end
