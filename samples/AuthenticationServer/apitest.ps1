#requires -version 3

param(
	[switch]$GetAccessToken,
	[switch]$ConfirmAccessToken,
	[switch]$GetResource,
	[int]$portNumber = 3000
)

$baseUrl = "http://localhost:$portNumber"
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

if($GetAccessToken)
{
# http://stackoverflow.com/questions/27232146/calculating-sha1-hash-algorithm-powershell-v2-0
# http://stackoverflow.com/questions/8051713/convert-a-string-to-a-byte-array-in-powershell-version-2
# https://gallery.technet.microsoft.com/scriptcenter/Get-StringHash-aa843f71
# http://blogs.msdn.com/b/luc/archive/2011/01/21/powershell-getting-the-hash-value-for-a-string.aspx
	write-host "Getting access token..."

	$seal = get-hashcode "${appId}:${appPK}:${instanceHost}"

	$headers = @{}

	$headers[$headerAppInstanceIdHeader] = "$instanceId"
	$headers[$headerSealHeader] = "$seal"
	$headers["Accept"] = "application/json"

	try{
		invoke-webrequest -uri "$baseUrl/session" -method POST  -useragent $userAgent  -headers $headers
	}
	catch [system.net.webexception]
	{
		write-host $_.exception.message
		write-host (new-object system.io.streamreader($_.exception.response.getresponsestream())).readtoend()
	}
}


if($ConfirmAccessToken)
{
	write-host "Confirming access token"

	$headers = @{}

	$headers[$headerAppInstanceIdHeader] = "MyAppID"
	$headers[$headerAccessTokenHeader] = "MyAccessToken"
	$headers[$headerCNonceHeader] = "MyCNonce"
	$headers["Accept"] = "application/json"

	try{
		invoke-webrequest -uri "$baseUrl/session" -method PUT  -useragent $userAgent  -headers $headers
	}
	catch [system.net.webexception]
	{
		write-host $_.exception.message
		write-host (new-object system.io.streamreader($_.exception.response.getresponsestream())).readtoend()
	}
}

if($GetResource)
{
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
}