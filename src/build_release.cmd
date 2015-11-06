@ECHO OFF
SETLOCAL
SET VERSION=%1
SET NUGET=.nuget\nuget.exe

DEL *.nupkg

"C:\Program Files (x86)\MSBuild\14.0\bin\amd64\msbuild" DWX.PortableApps.SQLite.sln /target:Clean;Build /p:Configuration=Release;OutDir=..\bin

%NUGET% SetApiKey %2

FOR %%G IN (*.nuspec) DO (
  %NUGET% pack %%G -Version %VERSION%
)
REM -Symbols

FOR %%G IN (*.nupkg) DO (
  %NUGET% push %%G
)

