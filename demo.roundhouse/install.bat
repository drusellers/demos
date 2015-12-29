#(new-object net.webclient).DownloadFile('http://download-codeplex.sec.s-msft.com/Download/Release?ProjectName=nuget&DownloadId=922467&FileTime=130580564071800000&Build=20941','nuget.exe')
".nuget/NuGet.exe" "Install" "roundhouse" "-OutputDirectory" "packages" "-ExcludeVersion"
