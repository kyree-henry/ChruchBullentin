. .\BuildFunctions.ps1

$startTime = 
$projectName = "ChruchBullentin"
$base_dir = resolve-path .\
$source_dir = "$base_dir"
$testingFolderPath = "$base_dir\Testing"
$integrationTestProjectPath = "$testingFolderPath\DataAccess.Test"
$projectConfig = $env:BuildConfiguration
$framework = "net6.0"
$version = $env:Version
$verbosity = "m"

$build_dir = "$base_dir\build"
$test_dir = "$build_dir\test"

$aliaSql = "$base_dir\OnionArchitecture\Database\Scripts\AliaSQL.exe"
$databaseAction = $env:DatabaseAction
if ([string]::IsNullOrEmpty($databaseAction)) { $databaseAction = "Rebuild"}
$databaseName = $env:DatabaseName
if ([string]::IsNullOrEmpty($databaseName)) { $databaseName = $projectName}
$script:databaseServer = $env:DatabaseServer
if ([string]::IsNullOrEmpty($script:databaseServer)) { $script:databaseServer = "(LocalDb)\MSSQLLocalDB"}
$databaseScripts = "$base_dir\OnionArchitecture\Database\Scripts"
    
if ([string]::IsNullOrEmpty($version)) { $version = "9.9.9"}
if ([string]::IsNullOrEmpty($projectConfig)) {$projectConfig = "Release"}
 
Function Init {
    rd $build_dir -recurse -force  -ErrorAction Ignore
	md $build_dir > $null

	$solutionPath = Join-Path -Path $base_dir -ChildPath "$projectName.sln"

	if (Test-Path $solutionPath -PathType Leaf) {
        exec {
            & dotnet clean $solutionPath -nologo -v $verbosity
        }

        exec {
            & dotnet restore $solutionPath -nologo --interactive -v $verbosity
        }
    } else {
        throw "Solution file not found at path: $solutionPath"
    }
    
    Write-Host $projectConfig
    Write-Host $version
}


Function Compile{
	exec {
        & dotnet build $base_dir\$projectName.sln -nologo --no-restore -v $verbosity -maxcpucount --configuration $projectConfig --no-incremental /p:Version=$version /p:Authors="Kyree Henry" /p:Product="Church Bulletin"
    }
}

Function RunUnitTests {
    $unitTestProjectPaths = @(
        "$testingFolderPath\Core.Test"
    )

    foreach ($projectPath in $unitTestProjectPaths) {
        Push-Location -Path $projectPath

        try {
            & {
                & dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura -nologo -v $verbosity --logger:trx `
                --results-directory $test_dir --no-build `
                --no-restore --configuration $projectConfig `
                --collect:"Code Coverage"
            }

        } finally {
            Pop-Location
        }
    }
}

Function IntegrationTest{
	Push-Location -Path $integrationTestProjectPath

	try {
		exec {
			& dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura -nologo -v $verbosity --logger:trx `
			--results-directory $test_dir --no-build `
			--no-restore --configuration $projectConfig `
			--collect:"Code Coverage" 
		}
	}
	finally {
		Pop-Location
	}
}

Function MigrateDatabaseLocal {
	exec{
		& $aliaSql $databaseAction $script:databaseServer $databaseName $databaseScripts
	}
}

Function PrivateBuild{
	$sw = [Diagnostics.Stopwatch]::StartNew()
	Init
	Compile
	RunUnitTests
	MigrateDatabaseLocal
    IntegrationTest
	$sw.Stop()
	write-host "Build time: " $sw.Elapsed.ToString()
}

Function CIBuild{
	PrivateBuild
}