<?php

	$servidor  = 'localhost';
	$usuario   = 'andrespr_demo';
	$pass	   = '123456';
	$baseDatos = 'andrespr_cettic_demo';

	$conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);

	$uss 		= mysqli_real_escape_string($conexion, $_GET['uss']);
	$pass 		= mysqli_real_escape_string($conexion, $_GET['pass']);

	if(!$conexion){
		echo "error";
	}
	else{
		$sql = "SELECT * FROM Users WHERE User LIKE '$uss' AND Pass LIKE '$pass'";
		$resultado = mysqli_query($conexion, $sql);

        if(mysqli_num_rows($resultado)>0)
		{
			echo "bien";
		}else{
			echo "mal";
		}
	}
?>