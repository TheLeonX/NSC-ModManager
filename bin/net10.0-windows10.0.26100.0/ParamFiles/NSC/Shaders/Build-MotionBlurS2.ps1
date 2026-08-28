param(
    [string]$Fxc = "C:\Program Files (x86)\Windows Kits\10\bin\10.0.26100.0\x86\fxc.exe",
    [string]$Archive = (Join-Path $PSScriptRoot "..\nuccPostEffect_dx11_S2.nsh")
)

$ErrorActionPreference = "Stop"
$shaderSource = Join-Path $PSScriptRoot "MotionBlurS2.hlsl"
$compiledShader = Join-Path ([System.IO.Path]::GetTempPath()) "NSUNSC-MotionBlurS2.dxbc"

& $Fxc /nologo /T ps_4_0 /E main /O3 /Fo $compiledShader $shaderSource
if ($LASTEXITCODE -ne 0) {
    throw "FXC failed with exit code $LASTEXITCODE"
}

$archiveBytes = [System.IO.File]::ReadAllBytes($Archive)
$shaderBytes = [System.IO.File]::ReadAllBytes($compiledShader)
$programCount = [BitConverter]::ToUInt16($archiveBytes, 14)
$cursor = 16
$motionBlurProgramId = [uint32]0x1100000C

for ($index = 0; $index -lt $programCount; ++$index) {
    $programId = [BitConverter]::ToUInt32($archiveBytes, $cursor)
    $cursor += 4
    $vertexSize = [BitConverter]::ToUInt32($archiveBytes, $cursor)
    $cursor += 4 + $vertexSize
    $pixelSizeOffset = $cursor
    $pixelSize = [BitConverter]::ToUInt32($archiveBytes, $cursor)
    $cursor += 4

    if ($programId -eq $motionBlurProgramId) {
        $output = New-Object byte[] ($archiveBytes.Length - $pixelSize + $shaderBytes.Length)
        [Array]::Copy($archiveBytes, 0, $output, 0, $pixelSizeOffset)
        [Array]::Copy([BitConverter]::GetBytes([uint32]$shaderBytes.Length), 0, $output, $pixelSizeOffset, 4)
        [Array]::Copy($shaderBytes, 0, $output, $pixelSizeOffset + 4, $shaderBytes.Length)
        $tailSource = $cursor + $pixelSize
        $tailTarget = $pixelSizeOffset + 4 + $shaderBytes.Length
        [Array]::Copy($archiveBytes, $tailSource, $output, $tailTarget, $archiveBytes.Length - $tailSource)
        [System.IO.File]::WriteAllBytes($Archive, $output)
        Write-Host "Updated program 0x$($programId.ToString('X8')) in $Archive"
        exit 0
    }

    $cursor += $pixelSize
}

throw "Motion-blur program 0x$($motionBlurProgramId.ToString('X8')) was not found"
