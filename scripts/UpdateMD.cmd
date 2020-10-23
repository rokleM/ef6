@echo off
path %systemRoot%\Microsoft.NET\Framework\v4.0.30319;%path%
path %cd%\bin;%cd%\sql;%cd%\users;%cd%\projects;%cd%\iis;%cd%;%path%
path %sqlPrefix%\90\%sqlSuffix%;%path%
path %sqlPrefix%\100\%sqlSuffix%;%path%
path %sqlPrefix%\110\%sqlSuffix%;%path%
path %sqlPrefix%\130\%sqlSuffix%;%path%
path %sqlPrefix%\150\%sqlSuffix%;%path%
path %ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin;%path%
path %ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin;%path%
path %windir%\sysnative\config\systemprofile\.dotnet\tools;%path%

pushd ..\custom\EntityFramework.DB
EdmGen /targetversion:4.5 /nologo /mode:FullGeneration  /pluralize /project:Model /entitycontainer:Model /namespace:EntityFramework.TestApp /provider:System.Data.SqlClient /connectionstring:"server=.\sqlexpress;integrated security=true; database=EFTest" /outobjectlayer:..\EntityFramework.TestApp\Views.g.cs /outviews:..\EntityFramework.TestApp\Views.g.cs 
msbuild ..\EntityFramework.DB.Autogen\EntityFramework.DB.Autogen.csproj /nologo /t:Rebuild /v:m /p:Configuration=Release /p:SolutionDir=..\..\;SolutionName=EntityFramework

EdmGen /targetversion:4.5 /nologo /mode:EntityClassGeneration /project:Model /namespace:EntityFramework.TestApp /outobjectlayer:..\EntityFramework.TestApp\Model.g.cs /incsdl:Model.csdl 
EdmGen /targetversion:4.5 /nologo /mode:ViewGeneration        /project:Model /namespace:EntityFramework.TestApp /outviews:..\EntityFramework.TestApp\Views.g.cs       /incsdl:Model.csdl /inmsl:Model.msl /inssdl:Model.ssdl 

msbuild ..\EntityFramework.Core.Autogen\EntityFramework.Core.Autogen.csproj /nologo /t:Rebuild /v:m /p:Configuration=Release /p:SolutionDir=..\..\;SolutionName=EntityFramework
popd