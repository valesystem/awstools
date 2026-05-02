program pUploader;

uses
  Vcl.Forms,
  uuploader in 'uuploader.pas' {fouploader},
  utabelas in 'utabelas.pas' {dm: TDataModule};

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(Tfouploader, fouploader);
  Application.CreateForm(Tdm, dm);
  Application.Run;
end.
