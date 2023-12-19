$path = "C:\Dev\Krosoft.Extensions\tests\Krosoft.Extensions.Validations.Tests\Krosoft.Extensions.Validations.Tests.csproj"
$resultPath = "temp"
$configuration = "Release"

dotnet build $path --configuration $configuration

dotnet test $path  `
    --logger trx `
    --results-directory $resultPath  `
    --no-build  `
    --configuration $configuration  `
    -p:CollectCoverage=true  `
    -p:Platform=AnyCPU  `
    -p:CoverletOutputFormat="cobertura"