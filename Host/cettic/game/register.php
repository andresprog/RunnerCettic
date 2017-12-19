<?php

	$servidor  = 'localhost';
	$usuario   = 'andrespr_demo';
	$pass	   = '123456';
	$baseDatos = 'andrespr_cettic_demo';

	$conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);

	$uss 		= mysqli_real_escape_string($conexion, $_GET['uss']);
	$pass 		= mysqli_real_escape_string($conexion, $_GET['pass']);

	$nombre         = mysqli_real_escape_string($conexion, $_GET['nombre']);
	

	if(!$conexion){
		echo "error";
	}else{

		$sql = "SELECT * FROM Users WHERE User like '$uss'";

		
		$resultado = mysqli_query($conexion, $sql);
		if(mysqli_num_rows($resultado)>0)
		{
			echo "el usuario ya existe";
		}else{
			$sql = "INSERT INTO Users (id, Name, User, Pass) VALUES (NULL, '$nombre', '$uss', '$pass') ";
			$resultado = mysqli_query($conexion, $sql);
			echo "usuario registrado";
		}
	}
?>