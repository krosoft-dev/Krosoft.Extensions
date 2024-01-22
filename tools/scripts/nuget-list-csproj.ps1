$template = Get-Content -Path ./tools/scripts/nuget-template-pipeline.yml



function Testss( $context  ) { 

  
  

  # Write-Host =========== $context.BaseName
 
  


  Get-Content $context.FullName `
| Find "<ProjectReference Include=" `
| ForEach-Object { $_ -replace '<ProjectReference Include=', '' -replace '/>', '' }  `
| Sort-Object -Unique  `
| ForEach-Object {
  
    
   
    $ddd = $PSItem.trim().replace("..\", '').replace('"', '')


    # Write-Host $ddd
    
    $outputPath = Join-Path ".\src\" $ddd  
      
    # Test-Path $outputPath


    $ssd = Get-Item $outputPath 
 

    $global:array += $ssd.BaseName

    Testss (Get-Item $outputPath  )


  }

 
  # Write-Host ===========
  # Write-Host  

   
}


(Get-ChildItem ./src -Filter *.csproj -Recurse) | ForEach-Object {
 
  # if (  $_.BaseName.StartsWith( "Krosoft.Extensions.Data.EntityFramework." )
  # ) {


  $fileName = "./tools/azure-pipelines/nuget-$($_.BaseName)-pipeline.yml"
  New-Item $fileName -Force  


  # Write-Host =========== $_.BaseName
      
  $global:array = @($_.BaseName)

 
  Testss $_  

  # Write-Host $global:array
  # Write-Host $global:array.Count


  $current = $template
  $current = $current.replace('KRO_PACKAGE_NAME', $_.BaseName)
   
  $sb = [System.Text.StringBuilder]::new()
  $array | Sort-Object | Get-Unique | ForEach-Object { [void]$sb.Append( '     - ' )
    [void]$sb.AppendLine( '"' + $PSItem.replace(' ', '') + '"' ) }
  
   
  $current = $current.replace('KRO_PACKAGES', $sb.ToString() )
    
  Set-Content $fileName $current
 
} 
# }