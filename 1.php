#!/usr/bin/php

<?php
	#===================================================================
	$port 	= 10001;
	$host 	= '127.0.0.1';
	#-------------------------------------------------------------------
	print("Proces 1, Podaj dane: \n");
	while(true)
	{
	$line = fgets(STDIN); 
	$prefix = "yolo_" . $line;
	print($prefix);
	$req = xmlrpc_encode_request("sample.add",$prefix);

	#-------------------------------------------------------------------
	$ctx = stream_context_create(
		array(
			'http' => array(
				'method' 	=> "POST",
				'header' 	=> array( "Content-Type: text/xml" ),
				'content' 	=> $req
			)
		)
	);
	#-------------------------------------------------------------------
	$xml = file_get_contents( "http://$host:$port/RPC2", false, $ctx );
	#-------------------------------------------------------------------
	$res = xmlrpc_decode( $xml );
	}
	#-------------------------------------------------------------------
	//if( $res && xmlrpc_is_fault( $res ) ){
	//	print "xmlrpc: $response[faultString] ($response[faultCode])";
	//	exit( 1 );
	//} 
	#===================================================================
?>
