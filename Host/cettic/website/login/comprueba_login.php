<?php

	// Login

	//Variables de conexion a la base de datos
	$servidor  = 'localhost';
	$usuario   = 'andrespr_demo';
	$pass	   = '123456';
	$baseDatos = 'andrespr_cettic_demo';

	//Creando una instancia de la base de datos
	$conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);


	//
	$uss 		= mysqli_real_escape_string($conexion, $_GET['uss']);
	$pass 		= mysqli_real_escape_string($conexion, $_GET['pass']);


	

	if(!$conexion){
		echo "error";
	}
	else{
		$sql = "SELECT * FROM Managers WHERE User LIKE '$uss' AND Pass LIKE '$pass'";
		$boolResultado = mysqli_query($conexion, $sql);

        if(mysqli_num_rows($boolResultado)>0)
		{
                        session_start();

			$_SESSION["user"]=$_GET['uss'];

			header("location:http://cofradinn.com/cettic");
			
		}else{
			header("location:http://cofradinn.com/cettic/website/login/login.php");
		}
	}
?>