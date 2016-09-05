param(
    [string]$environment = "Debug"
)

$workingDir = Split-Path ((Get-Variable MyInvocation -Scope 0).Value.MyCommand.Path)
Set-Location $workingDir #otherwise the helpers are not found if we're running the script from a different folder
. .\_Helpers.ps1


#####################################################
#
#  Main.
#
#####################################################
try {
    $programFilesPath = "$env:ProgramFiles"
    if ([System.Environment]::Is64BitOperatingSystem) {
        $programFilesPath = "${env:ProgramFiles(x86)}"
    }

    $nunit = "$programFilesPath\NUnit.org\nunit-console\nunit3-console.exe"
    $tests = "..\Tests.Affixx.Web\bin\$environment\Tests.Affixx.Web.dll "

    #& $nunit $tests 
    & $nunit $tests --where 'method == EmailRegistrationWithConfirmation'
}
finally {
    Set-Location $workingDir
}

