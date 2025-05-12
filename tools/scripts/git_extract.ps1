[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Configuration
$projectName = "Krosoft.Extensions.WebApi"
$commitMessage = "Extraction du projet $projectName depuis Krosoft.Extensions"
$targetBranch = "feat/init"  
$sourceRepoUrl = "https://github.com/krosoft-dev/Krosoft.Extensions.git"
$targetRepoUrl = "https://github.com/krosoft-dev/$projectName.git"
$targetDir = "C:\Dev\$projectName"
$pathsToKeep = @( 
    # Fichiers communs    
    "src\Directory.Build.props",
    "tests\Directory.Build.props",   
    ".gitattributes", 
    ".gitignore", 
    "CODEOWNERS", 
    "Directory.Build.props", 
    "Krosoft.Extensions.sln" , 
    "LICENSE", 
    "NuGet.Config", 
    "README.md", 

    # Fichiers scripts
    "tools\scripts\dotnet_test.ps1",
    "tools\scripts\git_clean.ps1",
    "tools\scripts\git_pull.ps1",

    # Fichiers sources  
    "src\Krosoft.Extensions.WebApi",
    "src\Krosoft.Extensions.WebApi.Blocking",
    "src\Krosoft.Extensions.WebApi.HealthChecks",
    "src\Krosoft.Extensions.WebApi.Identity",
    "src\Krosoft.Extensions.WebApi.Swagger",
    "src\Krosoft.Extensions.WebApi.Swagger.HealthChecks", 
  
    # Fichiers tests
    "tests\Krosoft.Extensions.WebApi.Identity.Tests",
    "tests\Krosoft.Extensions.WebApi.Tests",

    # Fichiers devops
    "tools\devops\vars\vars.yml",
    "tools\devops\build-pipeline.yml",       
    "tools\devops\nuget-Krosoft.Extensions.WebApi-pipeline.yml",
    "tools\devops\nuget-Krosoft.Extensions.WebApi.Blocking-pipeline.yml",
    "tools\devops\nuget-Krosoft.Extensions.WebApi.HealthChecks-pipeline.yml",
    "tools\devops\nuget-Krosoft.Extensions.WebApi.Identity-pipeline.yml",
    "tools\devops\nuget-Krosoft.Extensions.WebApi.Swagger-pipeline.yml",
    "tools\devops\nuget-Krosoft.Extensions.WebApi.Swagger.HealthChecks-pipeline.yml" 
) 


function Update-GitRepository {
    param (
        [Parameter(Mandatory = $true)][string]$NewRemoteUrl,
        [Parameter(Mandatory = $true)][string]$BranchName,
        [Parameter(Mandatory = $true)][string]$CommitMessage
    )
    
    try {
        # Supprime l'ancien remote
        git remote remove origin
        
        # Ajoute le nouveau remote
        git remote add origin $NewRemoteUrl
        
        # Crée une nouvelle branche et bascule dessus
        git checkout -b $BranchName
        
        # Ajoute tous les fichiers modifiés
        git add .
        
        # Crée un commit
        git commit -m $CommitMessage
        
        Write-Log "Repository configure avec succes" -Color Green
        Write-Log "- Remote : $NewRemoteUrl" -Color Green
        Write-Log "- Branche : $BranchName" -Color Green
    }
    catch {
        Write-Log "Erreur lors de la configuration git : $_" -Color Red
    }
}

function Get-PathParts {
    param ( [string]$path )
    
    $parts = @()
    $currentPath = ""
    
    $path.Split('\') | ForEach-Object {
        if ($currentPath -eq "") {
            $currentPath = $_
        }
        else {
            $currentPath = "$currentPath\$_"
        }
        $parts += $currentPath
    }
    
    return $parts
}

function Write-Log {
    param(
        [Parameter(Mandatory = $true)][string]$Message,
        [Parameter(Mandatory = $false)][ConsoleColor]$Color = 'White'
    )
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Write-Host "[$timestamp] $Message" -ForegroundColor $Color
}

function Rename-SolutionFile {
    param (
        [Parameter(Mandatory = $true)][string]$Path,
        [Parameter(Mandatory = $true)][string]$NewName
    )

    $oldSlnPath = Join-Path $targetDir "Krosoft.Extensions.sln"
    $newSlnPath = Join-Path $targetDir "$NewName.sln"

    if (Test-Path $oldSlnPath) {
        Rename-Item -Path $oldSlnPath -NewName $newSlnPath
        Write-Log "Solution renommée : $projectName.sln" -Color Green
    }
    else {
        Write-Log "Fichier solution non trouvé : $oldSlnPath" -Color Red
    }

}

function Update-ReadmeFile {
    param (
        [Parameter(Mandatory = $true)][string]$Path,
        [Parameter(Mandatory = $true)][string]$NewName
    )
    
    $readmePath = Join-Path $Path "README.md"
    if (Test-Path $readmePath) {
        $content = Get-Content $readmePath -Raw
        $content = $content -replace "# Krosoft.Extensions", "# $NewName"
        Set-Content -Path $readmePath -Value $content
        Write-Log "README.md mis a jour avec : $NewName" -Color Green
    }
    else {
        Write-Log "Fichier README.md non trouve" -Color Red
    }
}

function Copy-FilteredRepository {
    param (
        [Parameter(Mandatory = $true)][string]$SourceUrl,
        [Parameter(Mandatory = $true)][string]$TargetPath,
        [Parameter(Mandatory = $true)][string[]]$PathsToKeep
    )
    
    try {
        # Préparer les dossiers à conserver
        $folderToKeep = @()
        foreach ($path in $PathsToKeep) {
            $parts = Get-PathParts $path
            $folderToKeep += $parts
        }
        $folderToKeep = $folderToKeep | Select-Object -Unique

        # Cloner le dépôt original
        git clone $SourceUrl $TargetPath
        Set-Location $TargetPath
        Write-Log "Repository clone dans : $TargetPath" -Color Green

        # Récupérer tous les dossiers et fichiers
        $allItems = Get-ChildItem -Path $TargetPath -Recurse

        # Parcourir chaque élément en commençant par les plus profonds
        $allItems | Sort-Object -Property FullName -Descending | ForEach-Object {
            # Ignorer le dossier .git
            if ($_.FullName -like "*.git*") {
                return
            }     

            $relativePath = $_.FullName.Substring($TargetPath.Length + 1)
     
            $keepItem = $false
            foreach ($pathToKeep in $PathsToKeep) {       
                # Vérifie si c'est exactement le dossier qu'on veut garder ou un de ses sous-éléments
                if ($relativePath -eq $pathToKeep -or 
                    $relativePath.StartsWith("$pathToKeep\") -or 
                    $folderToKeep -contains $relativePath) {
                    $keepItem = $true
                    Write-Log "Conservation : $relativePath" -Color Yellow
                    break
                }
            }

            # Supprimer si le chemin n'est pas dans la liste à conserver
            if (-not $keepItem) {
                if (Test-Path $_.FullName) {
                    Remove-Item -Path $_.FullName -Force -Recurse -ErrorAction SilentlyContinue
                }
            }
        }
        
        Write-Log "Filtrage des fichiers termine avec succes" -Color Green
    }
    catch {
        Write-Log "Erreur lors du clonage/filtrage : $_" -Color Red
        throw
    }
}


# Nettoyage des anciens répertoires si présents
if (Test-Path $targetDir) {
    Remove-Item -Recurse -Force $targetDir
    Write-Log "Suppression du répertoire de sortie existant..." -Color Yellow
}


Copy-FilteredRepository -SourceUrl $sourceRepoUrl -TargetPath $targetDir -PathsToKeep $pathsToKeep

#Rename sln file with project name
Rename-SolutionFile -Path $targetDir -NewName $projectName

# Update README.md
Update-ReadmeFile -Path $targetDir -NewName $projectName

# git config
Update-GitRepository -NewRemoteUrl $targetRepoUrl -BranchName $targetBranch -CommitMessage $commitMessage

 
Write-Log "Projet extrait : '$targetDir' sur la branche '$targetBranch' de $targetRepoUrl" -Color Green