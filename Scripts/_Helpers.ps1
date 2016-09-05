#####################################################
#
#  Debug-type output.
#
#####################################################
function Log($level, $message)
{
    if     ($level -eq "info")   { write-host $message }
    elseif ($level -eq "warn")   { write-host $message -foregroundColor Yellow }
    elseif ($level -eq "debug")  { write-host $message -foregroundColor DarkGray }    
    elseif ($level -eq "success"){ write-host $message -foregroundColor Green }    
    elseif ($level -eq "error")  { write-host $message -foregroundColor Red }
    else                         { write-host $message }
}

#####################################################
#
#  Clears the errors.
#  Helps to to avoid the script failing due to the errors from the previous runs.
#
#####################################################
function ClearPreviousErrors()
{
    $Error.Clear()
    $LASTEXITCODE = 0
}

#####################################################
#
#  Terminates the script if there was an error.
#
#####################################################
function ExitIfError()
{
    if (
	($error.Count -gt 0) -or 
	(($LASTEXITCODE -ne 0) -and ($LASTEXITCODE -ne "") -and ($LASTEXITCODE -ne $null))
    ) {
        Log -level:"error" -message:"Script failed with error code $LASTEXITCODE."
        if ($error.Count -gt 0) {
                foreach($e in $error) {
                        Log -level:"debug" -message:$e
                }
        }
        throw 
    }    
}

#####################################################
#
#  Returns configuration value for the given key and environment.
#
#####################################################
function GetConfigValue($key, $environment)
{
    $configPath = (Resolve-Path "..\Affixx.Core\bin\$environment\Configuration\Core.config").Path
    [xml]$config = Get-Content $configPath
    foreach ($setting in $config.configuration.appSettings.add) {
        if ($setting.key -eq $key) {
            return $setting.value
        }
    }
    throw "Setting $key not found in configuraion"

    # doesn't seem to work in some cases - see https://stackoverflow.com/questions/36846049/powershell-5-appsettings-bug
    #[System.AppDomain]::CurrentDomain.SetData("APP_CONFIG_FILE", $configPath)
    #return [System.Configuration.ConfigurationManager]::AppSettings[$key]
}

#####################################################
#
#  Runs the given query.
#  Nicely outputs print statements from the query.
#
#####################################################
function RunQuery($query, $file, $connection, [int]$timeoutSeconds=15, [switch]$continueOnError) 
{
    $server = $connection.server
    $database = $connection.database
    $user = $connection.user
    $password = $connection.password

    $folder = [System.Environment]::GetFolderPath("MyDocuments")
    $messages = [System.IO.Path]::Combine($folder, "query.messages.txt")
    $results = [System.IO.Path]::Combine($folder, "query.results.txt")

    $errorAction = 'Stop'
    if ($continueOnError.isPresent) {
        $errorAction = 'SilentlyContinue'
    }

    if ($file -ne $null) {
        Log -level:'debug' -message:"Running $file"
        $query = [System.IO.File]::ReadAllText("$file")
    }

    try {
        if ($server -eq '.'){
	        Invoke-Sqlcmd -Query $query -Database $database -ErrorAction $errorAction -QueryTimeout:$timeoutSeconds -Verbose 4>$messages 1>$results
	    }
        else {
            Invoke-Sqlcmd -Query $query -ServerInstance $server -Database $database -ErrorAction $errorAction -Username $user -Password $password -QueryTimeout $timeoutSeconds -Verbose 4>$messages 1>$results
        }
    }
    catch {
        Log -level:"error" -message:"Failed to execute query: $query"
        throw
    }
    ExitIfError
    
    foreach ($line in [System.IO.File]::ReadLines($messages)) {
        Log -level:"debug" -message:("  " + $line)
    }
    Remove-Item $messages

    $result = @()
    foreach ($line in [System.IO.File]::ReadLines($results)) {
        if (![String]::IsNullOrEmpty($line)) {
            $result = $result + ([string]$line).Trim()
        }
    }
    Remove-Item $results
    return $result
}
