function AzDevOpsLogin() {   
    Write-Host "=========================================="
    Write-Host "AzDevOpsLogin"  
    Write-Host "==========================================" 
    az login --allow-no-subscriptions 
    Write-Host  
}

function AzDevOpsInitCli() {
    Write-Host "=========================================="
    Write-Host "AzDevOpsInitCli :  $global:url > $global:projet"  
    Write-Host "==========================================" 
    az extension add --name azure-devops 
    az devops configure -d organization=$global:url project=$global:projet
    az devops configure -l  
    Write-Host 
}

function AzDevOpsPipelinesList() { 
    Write-Host "=========================================="
    Write-Host "AzDevOpsPipelinesList"
    Write-Host "=========================================="     
    az pipelines build definition list -o table  
    Write-Host  
}

function AzDevOpsPipelinesInitQueueId() {
    Write-Host "=========================================="
    Write-Host "AzDevOpsPipelinesInitQueueId : '$global:queue'"  
    Write-Host "==========================================" 
    $response = az pipelines queue list -o json --query "[?name=='$global:queue']" | ConvertFrom-Json
    $global:queueId = $response.id
    Write-Host "AzDevOpsPipelinesInitQueueId : $global:queueId"   
}

function AzDevOpsPipelinesInitServiceConnectionId() {
    Write-Host "=========================================="
    Write-Host "AzDevOpsPipelinesInitServiceConnectionId : '$global:serviceConnection'"  
    Write-Host "==========================================" 
    $response = az devops service-endpoint list -o json --query "[?name=='$global:serviceConnection']" | ConvertFrom-Json
    $global:serviceConnectionId = $response.id
    Write-Host "AzDevOpsPipelinesInitServiceConnectionId : $global:serviceConnectionId"   
}


function AzDevOpsPipelinesCreate(
    [Parameter(Mandatory = $True)][ValidateNotNullOrEmpty()][string ] $name ) {
    Write-Host "=========================================="
    Write-Host "AzDevOpsPipelinesCreate : $name"  
    Write-Host "==========================================" 
    az pipelines create `
        --name "nuget - $name"   `
        --org $global:url   `
        --project $global:projet    `
        --queue-id $global:queueId    `
        --service-connection $global:serviceConnectionId    `
        --repository $global:repository  `
        --repository-type "GitHub" `
        --yaml-path "tools/devops/nuget-$name-pipeline.yml" `
        -o table  

    Write-Host 
} 
function AzDevOpsPipelinesCreateOrUpdate(
    [Parameter(Mandatory = $True)][ValidateNotNullOrEmpty()][string ] $name ) {
    Write-Host "=========================================="
    Write-Host "AzDevOpsPipelinesCreateOrUpdate : $name"  
    Write-Host "==========================================" 


   
    $response = az pipelines show --name "nuget - $name" -o json | ConvertFrom-Json
    $response.id


    Write-Host 
} 




$global:url = "https://krosoft.visualstudio.com" 
$global:projet = "Krosoft.Extensions"
$global:serviceConnection = "krosoft-dev"
$global:repository = "https://github.com/krosoft-dev/Krosoft.Extensions"
$global:queue = "Azure Pipelines"

# AzDevOpsLogin
# AzDevOpsInitCli $global:url $global:projet
AzDevOpsPipelinesList
AzDevOpsPipelinesInitQueueId
AzDevOpsPipelinesInitServiceConnectionId 
 
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Blocking"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Blocking.Abstractions"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Blocking.Memory"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Blocking.Redis"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Cache.Distributed.Redis"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Cache.Distributed.Redis.HealthChecks"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Cache.Memory"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Core"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Cqrs"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Cqrs.Behaviors"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Cqrs.Behaviors.Identity"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Cqrs.Behaviors.Validations"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Data.Abstractions"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Data.EntityFramework"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Data.EntityFramework.InMemory"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Data.EntityFramework.PostgreSql"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Data.EntityFramework.Sqlite"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Data.EntityFramework.SqlServer"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Data.Json"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Events"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Events.Identity"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Identity"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Identity.Abstractions"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Jobs"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Mapping"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Pdf"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Polly"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Reporting.Csv"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Testing"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Testing.Cqrs"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Testing.Data.EntityFramework"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Testing.WebApi"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Validations"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.WebApi"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.WebApi.Blocking"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.WebApi.HealthChecks"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.WebApi.Identity"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.WebApi.Swagger"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.WebApi.Swagger.HealthChecks"
# AzDevOpsPipelinesCreate "Krosoft.Extensions.Zip"
AzDevOpsPipelinesCreateOrUpdate "Krosoft.Extensions.Zip"