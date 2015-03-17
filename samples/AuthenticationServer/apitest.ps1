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

if($GetAccessToken)
{
	write-host "Getting access token..."

	$headers = @{}

	$headers[$headerAppInstanceIdHeader] = "My Test App Instance"
	$headers[$headerSealHeader] = "Erlimar seal"
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