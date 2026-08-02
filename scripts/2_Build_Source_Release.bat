call 1_NuGet_Restore.bat

"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe" "..\Lamps Plus Web Tests.sln" /target:Clean;Build /p:configuration=Release;platform="Any CPU";BuildInParallel=true /maxcpucount /fl /flp:logfile=2_Build_Source_Release.log

exit
