<?php

	$servidor  = 'localhost';
	$usuario   = 'andrespr_demo';
	$pass	   = '123456';
	$baseDatos = 'andrespr_cettic_demo';

	$conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);

	$uss 		= mysqli_real_escape_string($conexion, $_GET['uss']);
	$points         = mysqli_real_escape_string($conexion, $_GET['points']);
	$timeSend       = mysqli_real_escape_string($conexion, $_GET['timeS']);
	$ItemValue      = mysqli_real_escape_string($conexion, $_GET['item']);
	
	$_time = "Time";

	if(!$conexion){
		echo "error";
	}else{

		$sql = "SELECT * FROM Users WHERE User like '$uss'";

		
		$resultado = mysqli_query($conexion, $sql);
		if(mysqli_num_rows($resultado)>0)
		{
			$sql = "INSERT INTO Games (ID, User, Points, $_time, ItemValue ) 
					   VALUES (NULL, '$uss', '$points', '$timeSend', '$ItemValue') ";
                $resultado = mysqli_query($conexion, $sql);
			echo "partida guardada";
		}else{

			echo "usuario no existe";
		}
	}
?>