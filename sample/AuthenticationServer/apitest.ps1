#requires -version 3

param(
    [string]$hostName = "localhost",
	[int]$portNumber = 3000
)

$baseUrl = "http://$hostName"

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

# GetAccessToken
# {
	write-host "`nGetting access token..."

	$seal = get-hashcode "${appId}:${appPK}:${instanceHost}"

	$headers = @{}

	$headers[$headerAppInstanceIdHeader] = "$instanceId"
	$headers[$headerSealHeader] = "$seal"
	$headers["Accept"] = "application/json"

	try{
		$r = invoke-webrequest -uri "$baseUrl/session" -method POST  -useragent $userAgent  -headers $headers

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

	$headers[$headerAppInstanceIdHeader] = "$instanceId"
	$headers[$headerAccessTokenHeader] = "$accessToken"
	$headers[$headerCNonceHeader] = "$cnonce"
	$headers["Accept"] = "application/json"

	try{
		$r = invoke-webrequest -uri "$baseUrl/session" -method PUT  -useragent $userAgent  -headers $headers

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
	write-host "Getting resource..."

	$headers = @{}

	$headers[$headerAppInstanceIdHeader] = "MyAppID"
	$headers[$headerSealedAccessTokenHeader] = "MySealedAccessToken"
	$headers[$headerCNonceHeader] = "MyCNonce"
	$headers["Accept"] = "application/json"

	try{
		invoke-webrequest -uri "$baseUrl/resource" -method GET  -useragent $userAgent  -headers $headers
	}
	catch [system.net.webexception]
	{
		write-host $_.exception.message
		write-host (new-object system.io.streamreader($_.exception.response.getresponsestream())).readtoend()
	}
# }
