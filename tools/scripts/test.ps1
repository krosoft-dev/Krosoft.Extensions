$template = Get-Content -Path ./tools/scripts/nuget-template-pipeline.yml



function Testss( $context ) { 

  
  

  Write-Host =========== $context.BaseName
 
  


  Get-Content $context.FullName `
| Find "<ProjectReference Include=" `
| ForEach-Object { $context -replace '<ProjectReference Include=', '' -replace '/>', '' }  `
| Sort-Object -Unique  `
| ForEach-Object { 
  
    Write-Host $PSItem

  }



  # dotnet list  $context .FullName reference

  Write-Host ===========
  Write-Host  
}


(Get-ChildItem ./src -Filter *.csproj -Recurse) | ForEach-Object {


  if (  $_.BaseName -eq "Krosoft.Extensions.Data.EntityFramework.SqlServer"
  ) {
      
 
    Testss $_ 

   
 
  } 
}