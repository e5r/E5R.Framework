#requires -version 3

param(
    [string]$hostName = "localhost",
	[int]$portNumber = 3000
)

$baseUrl = "http://${hostName}"

if($portNumber -ne 80){
    $baseUrl = "${baseUrl}:${portNumber}"
}

write-host $baseUrl

$userAgent = "E5R AuthenticationServer API Test/0.1 (Windows)"
$headerAppInstanceIdHeader = "X-E5RAuth-AppInstanceId"
$headerSealHeader = "X-E5RAuth-Seal"
$headerAccessTokenHeader = "X-E5RAuth-AccessToken"
$headerNonceHeader = "X-E5RAuth-Nonce"
$headerCNonceHeader = "X-E5RAuth-CNonce"
$headerSealedAccessTokenHeader = "X-E5RAuth-SealedAccessToken"
$headerOCNonceHeader = "X-E5RAuth-OCNonce"

$appId = "4adf130c5332c69c75fba9284ce1d27e"
$appPK = "194cf821e066ca8708c297691ba15b16fe8c163f0ccabcf26f3eab5fd4c6779d0da6d7aef285255ae45e611c4087e081"
$instanceId = "678c588f461ca61879d2dc689f425e3f"
$instanceHost = "localhost"

$appNonceOrders = @(
	"{AppID}:{AppPrivateKey}:{Nonce}:{AppInstanceHost}:{SealedAccessToken}",
	"{AppID}:{Nonce}:{AppInstanceHost}:{SealedAccessToken}:{AppPrivateKey}",
	"{AppID}:{SealedAccessToken}:{Nonce}:{AppInstanceHost}:{AppPrivateKey}",
	"{AppInstanceHost}:{AppID}:{Nonce}:{AppPrivateKey}:{SealedAccessToken}",
	"{AppInstanceHost}:{SealedAccessToken}:{AppID}:{Nonce}:{AppPrivateKey}",
	"{AppInstanceHost}:{SealedAccessToken}:{Nonce}:{AppID}:{AppPrivateKey}",
	"{AppPrivateKey}:{AppInstanceHost}:{SealedAccessToken}:{AppID}:{Nonce}",
	"{AppPrivateKey}:{AppInstanceHost}:{SealedAccessToken}:{Nonce}:{AppID}",
	"{AppPrivateKey}:{Nonce}:{AppInstanceHost}:{AppID}:{SealedAccessToken}",
	"{AppPrivateKey}:{Nonce}:{SealedAccessToken}:{AppID}:{AppInstanceHost}",
	"{Nonce}:{AppInstanceHost}:{AppID}:{AppPrivateKey}:{SealedAccessToken}",
	"{Nonce}:{AppInstanceHost}:{AppPrivateKey}:{SealedAccessToken}:{AppID}",
	"{SealedAccessToken}:{AppInstanceHost}:{AppID}:{AppPrivateKey}:{Nonce}",
	"{SealedAccessToken}:{AppPrivateKey}:{AppInstanceHost}:{AppID}:{Nonce}",
	"{SealedAccessToken}:{Nonce}:{AppInstanceHost}:{AppPrivateKey}:{AppID}"
)

$accessToken = ""
$sealedAccessToken = ""
$accessNonce = ""
$accessOCNonce = ""

function get-hashcode([string] $value)
{
	$sha1 = new-object system.security.cryptography.SHA1CryptoServiceProvider
	$enc = [system.text.encoding]::Unicode

	$hash = $sha1.ComputeHash($enc.GetBytes($value))

	$result = ""
    foreach ($h in $hash)
    {
        $result += [string]::Format("{0:x2}", $h);
    }

	return $result
}

#{AppID}:{AppPrivateKey}:{Nonce}:{AppInstanceHost}:{SealedAccessToken}
$cNonceResource = ""
function get-cnonceresource()
{
	if($cNonceResource -eq ""){
		foreach($template in $appNonceOrders){
			$hash = get-hashcode $template

			if($hash -eq $accessOCNonce){				
			    write-host "    -> Using template: ${template}"

				$data = $template
				$data = $data -replace "{AppID}", "${appId}"
				$data = $data -replace "{AppPrivateKey}", "${appPK}"
				$data = $data -replace "{Nonce}", "${accessNonce}"
				$data = $data -replace "{AppInstanceHost}", "${instanceHost}"
				$data = $data -replace "{SealedAccessToken}", "${sealedAccessToken}"

				$cNonceResource = get-hashcode $data
			}
		}
	}
	write-host "    -> Using CNonce: ${cNonceResource}"
	return $cNonceResource
}

# GetAccessToken
# {
	write-host "`nGetting access token..."

	$seal = get-hashcode "${appId}:${appPK}:${instanceHost}"

	$headers = @{}

	$headers[$headerAppInstanceIdHeader] = "${instanceId}"
	$headers[$headerSealHeader] = "${seal}"
	$headers["Accept"] = "application/json"

	try{
		$r = invoke-webrequest -uri "${baseUrl}/session" -method POST  -useragent $userAgent  -headers $headers

		if($r.StatusCode -eq 201){
			write-host "    Access Token created!"

			$accessToken = $r.Headers[$headerAccessTokenHeader]
			$accessNonce = $r.Headers[$headerNonceHeader]

			write-host "    {"
			write-host "        Token: ${accessToken}"
			write-host "        Nonce: ${accessNonce}"
			write-host "    }"
		}
	}
	catch [system.net.webexception]
	{
		write-host $_.exception.message
		write-host (new-object system.io.streamreader($_.exception.response.getresponsestream())).readtoend()
	}
# }


# ConfirmAccessToken
# {
	write-host "`nConfirming access token..."

	$cnonceTemplate = "${appId}:${appPK}:${instanceHost}:${accessNonce}"
	$cnonce = get-hashcode  "${cnonceTemplate}"

	$headers = @{}

	$headers[$headerAppInstanceIdHeader] = "${instanceId}"
	$headers[$headerAccessTokenHeader] = "${accessToken}"
	$headers[$headerCNonceHeader] = "${cnonce}"
	$headers["Accept"] = "application/json"

	try{
		$r = invoke-webrequest -uri "${baseUrl}/session" -method PUT  -useragent $userAgent  -headers $headers

		if($r.StatusCode -eq 202){
			write-host "    Access Token confirmed!"

			$oldAccessNonce = $accessNonce
			$accessToken = $r.Headers[$headerAccessTokenHeader]
			$accessNonce = $r.Headers[$headerNonceHeader]
			$sealedAccessToken = $r.Headers[$headerSealedAccessTokenHeader]
			$accessNonce = $r.Headers[$headerNonceHeader]
			$accessOCNonce = $r.Headers[$headerOCNonceHeader]

			write-host "    {"
			write-host "        SealedToken: ${sealedAccessToken}"
			write-host "        OldNonce: ${oldAccessNonce}"
			write-host "        Nonce: ${accessNonce}"
			write-host "        OCNonce: ${accessOCNonce}"
			write-host "    }"
		}
	}
	catch [system.net.webexception]
	{
		write-host $_.exception.message
		write-host (new-object system.io.streamreader($_.exception.response.getresponsestream())).readtoend()
	}
# }

# GetResource
# {
	write-host "`nGetting resource..."

	$headers = @{}

	$headers[$headerAppInstanceIdHeader] = "${instanceId}"
	$headers[$headerSealedAccessTokenHeader] = "${sealedAccessToken}"
	$headers[$headerCNonceHeader] = get-cnonceresource
	$headers["Accept"] = "application/json"

	try{
		$r = invoke-webrequest -uri "${baseUrl}/any/resource" -method GET  -useragent $userAgent  -headers $headers

		if($r.StatusCode -ne 401){
			write-host "    Feature available!`n"

			foreach($h in $r.Headers){
				$hValue = $r.Headers[$h]
				write-host "    ${h}: ${hValue}"
			}
			
			$r | write-host
		}
	}
	catch [system.net.webexception]
	{
		write-host $_.exception.message
		write-host (new-object system.io.streamreader($_.exception.response.getresponsestream())).readtoend()
	}
# }
