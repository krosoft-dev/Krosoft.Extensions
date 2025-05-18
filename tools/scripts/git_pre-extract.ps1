[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Configuration

$projectName = "Krosoft.Extensions.Data"



 

function Get-ProjectFiles {
    param (
      
        [Parameter(Mandatory = $true)][string]$ProjectName
    )

    Write-Host "`nRecherche des fichiers pour le projet: $ProjectName`n" -ForegroundColor Cyan

 
    Write-Host "`nDossiers src\$ProjectName* :" -ForegroundColor Yellow
    Get-ChildItem -Path ".\src" -Directory -Filter "$ProjectName*" | ForEach-Object {
        Write-Host "src\$($_.Name)"
    }
 
    Write-Host "`nDossiers tests\$ProjectName* :" -ForegroundColor Yellow
    Get-ChildItem -Path ".\tests" -Directory -Filter "$ProjectName*" | ForEach-Object {
        Write-Host "tests\$($_.Name)"
    }
 
    Write-Host "`nFichiers tools\devops\nuget-$ProjectName* :" -ForegroundColor Yellow
    Get-ChildItem -Path ".\tools\devops" -File -Filter "nuget-$ProjectName*" | ForEach-Object {
        Write-Host "tools\devops\$($_.Name)"
    }
}
 

Write-Host "Recherche des fichiers pour le projet: $projectName" -ForegroundColor Cyan
Get-ProjectFiles -ProjectName $projectName
 
 