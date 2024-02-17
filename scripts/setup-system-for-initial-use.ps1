$isDotnetInstalled = $false;
$doesDotnetExistOnMachine = $true;

try {
    $dotnetVersions = dotnet --list-sdks
}
catch {
    $doesDotnetExistOnMachine = $false;
    Write-Host "Dotnet is not installed in the current machine, trying to install..."
}

if ($doesDotnetExistOnMachine) {
    foreach ($item in $dotnetVersions) {
        if ($item.Contains("6")) {
            $isDotnetInstalled = $true;
            Write-Host "Dotnet 6 is already installed. Skipping installation."
            break;
        }
    }
}

if ($isDotnetInstalled -eq $false) {
    winget install Microsoft.DotNet.SDK.6
}



if (-not (Test-Path (Join-Path $env:SystemRoot 'System32\docker.exe'))) {
    Write-Host "Docker is not installed. Installing Docker..."
    Invoke-WebRequest -Uri https://desktop.docker.com/win/stable/Docker%20Desktop%20Installer.exe -OutFile DockerDesktopInstaller.exe
    Start-Process -Wait -FilePath .\DockerDesktopInstaller.exe
    Remove-Item -Path .\DockerDesktopInstaller.exe
    
    Write-Host "Docker installed successfully."
} else {
    Write-Host "Docker is already installed."
}

if (-not (Test-Path (Join-Path $env:ProgramFiles 'Docker\Docker\resources\bin\docker-compose.exe'))) {
    Write-Host "Docker Compose is not installed. Installing Docker Compose..."

    Invoke-WebRequest -Uri https://github.com/docker/compose/releases/download/1.29.2/docker-compose-Windows-x86_64.exe -OutFile docker-compose.exe
    Move-Item -Path .\docker-compose.exe -Destination $env:ProgramFiles\Docker\Docker\resources\bin
    
    Write-Host "Docker Compose installed successfully."
} else {
    Write-Host "Docker Compose is already installed."
}