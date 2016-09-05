param(
    [string]$environment = "Debug"
)

$workingDir = Split-Path ((Get-Variable MyInvocation -Scope 0).Value.MyCommand.Path)
Set-Location $workingDir #otherwise the helpers are not found if we're running the script from a different folder
. .\_Helpers.ps1

#####################################################
#
#  Local helper functions.
#
#####################################################
function GetConnectionDetails($environment)
{
    $connectionString = GetConfigValue -key:'database' -environment:$environment
    $connectionStringBuilder = New-Object System.Data.Common.DbConnectionStringBuilder
    $connectionStringBuilder.set_ConnectionString($connectionString)

    $result = @{ 'server'=$connectionStringBuilder['server']; 'database'=$connectionStringBuilder['database']; 'user'=$connectionStringBuilder['user'];'password'=$connectionStringBuilder['password'];}
    return $result
}

function EnsureDatabaseExists($connection)
{
    $server=$connection.server
    $database=$connection.database
    $createDatabase = "
        IF NOT EXISTS(SELECT * FROM sys.databases WHERE name='$database')
        BEGIN
	        PRINT 'Creating $database database'
	        CREATE DATABASE $database
        END
        GO
    "

    $database = $connection.database
    $connection.database = 'master'
    RunQuery -query $createDatabase -connection:$connection

    $connection.database = $database
}

function UpgradeDatabase($versionFolder, $connection)
{
    $path = $versionFolder.FullName
    foreach($file in Get-ChildItem $path -Recurse -Filter *.sql) { 
        RunQuery -file "$path\$file" -connection:$connection
    }
}

#####################################################
#
#  Main.
#
#####################################################
try {
    ClearPreviousErrors

    $connection = GetConnectionDetails -environment:$environment
    Log -level 'info' -message "Setting up database [$($connection.database)] at [$($connection.server)] server"

    EnsureDatabaseExists -connection:$connection

    $rootFolder = (Resolve-Path "Database").Path
    foreach($versionFolder in Get-ChildItem -Directory $rootFolder) { 
        UpgradeDatabase -versionFolder $versionFolder -connection $connection
    }    
}
finally {
    Set-Location $workingDir
}

