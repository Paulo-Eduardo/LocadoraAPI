MsBuild.exe .\app\BackEnd\LocadoraAPI\LocadoraAPI\LocadoraAPI.csproj /t:Rebuild /p:Configuration=Release

start app\BackEnd\LocadoraAPI\LocadoraAPI\bin\Release\LocadoraAPI.exe
cd app\FrontEnd\locadora
npm run serve