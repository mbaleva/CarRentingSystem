cd ..
cd 'tests\unit-tests\CarRentingSystem.Cars\CarRentingSystem.Cars.UnitTests'

dotnet add package coverlet.msbuild
dotnet tool install -g dotnet-reportgenerator-globaltool

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
reportgenerator -reports:coverage.opencover.xml -targetdir:coverage/report

cd ..
cd ..
cd ..
cd ..
