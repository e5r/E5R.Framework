for($count = 0; $count -lt $args.length; $count++) {
    $value = $args[$count]
    if("$value".contains(" ")) {
        $value = "$value".replace("`"", "```"")
        $value = "`"$value`""
    }
    $args[$count] = $value
}

$progresspreference = 'SilentlyContinue'

if(!(get-command 'e5r' -erroraction silentlycontinue)){
  write-host "E5R Environment Bootstrap..."
  $e5rrepo = "https://github.com/e5r/env/raw/v0.1.0-alpha2"
  invoke-webrequest "$e5rrepo/e5r-install.ps1" -outfile "$psscriptroot\e5r-install.ps1"
  iex "& `"$psscriptroot\e5r-install.ps1`""
  if(test-path "$psscriptroot\e5r-install.ps1"){
    del "$psscriptroot\e5r-install.ps1" -force | out-null
  }
}

if(!(get-command 'e5r' -erroraction silentlycontinue)){
  write-host "`nE5R Environment not installed!"
  exit 1
}

iex "& e5r env boot"
iex "& e5r env install"
iex "& e5r env use"

if(!(get-command 'nuget' -erroraction silentlycontinue)){
  write-host "`nNuGet tool not installed!"
  exit 1
}

if(!(get-command 'sake' -erroraction silentlycontinue)){
  write-host "`nSake tool not installed!"
  exit 1
}

write-host "Building..."
iex "& sake -I `"build`" -f makefile.shade $args"

exit $lastexitcode
