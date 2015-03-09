@echo off
cd %~dp0

call e5r help 2>nul

if "%ERRORLEVEL%"=="0" goto BuildCommon_End

:BuildCommon_Install
  echo E5R Environment Bootstrap...
  set E5RREPO=https://github.com/e5r/env/raw/v0.1.0-alpha2
  @powershell -NoProfile -ExecutionPolicy unrestricted -Command ^
    "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest '%E5RREPO%/e5r-install.cmd' -OutFile '%CD%\e5r-install.cmd'"
  call "%CD%\e5r-install.cmd"
  if exist "%CD%\e5r-install.cmd" del /F /Q "%CD%\e5r-install.cmd"
:BuildCommon_End
:Build_EnvCheck
  call e5r 1>nul
  if "%ERRORLEVEL%"=="0" goto Build_Before
  echo.
  echo E5R Environment not installed!
  goto Build_Error

:Build_Before
  call e5r env boot
  call e5r env install
  call e5r env use

:Build_NugetCheck
  call nuget help 1>nul
  if "%ERRORLEVEL%"=="0" goto Build_SakeCheck
  echo.
  echo NuGet tool not installed!
  goto Build_Error

:Build_SakeCheck
  call sake --help 1>nul
  if "%ERRORLEVEL%"=="0" goto Build
  echo.
  echo Sake tool not installed!
  goto Build_Error

:Build
  echo Building...
  call sake -I "build" -f makefile.shade %*
  if "%ERRORLEVEL%"=="0" goto Build_Success
  exit /b %ERRORLEVEL%

:Build_Error
  exit /b 1

:Build_Success
  exit /b 0
