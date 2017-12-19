<?php

// desactivando errores y notificaciones
error_reporting(0);


  $servidor  = 'localhost';
  $usuario   = 'andrespr_demo';
  $pass      = '123456';
  $baseDatos = 'andrespr_cettic_demo';
  $tabla     = 'ConfigGame';

  $conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);

  $speed    = mysqli_real_escape_string($conexion, $_GET['speed']);
  $time     = mysqli_real_escape_string($conexion, $_GET['time']);
  $itemValue= mysqli_real_escape_string($conexion, $_GET['itemValue']);
  

  if(!$conexion)
  {
    //echo "error";
  }
  else
  {    
    $resultado = mysqli_query($conexion, $sql);
    if(mysqli_num_rows($resultado)>0)
    {
      //echo "el usuario ya existe";
    }
    else
    {

      $sql = "UPDATE ConfigGame SET CaharacterSpeed='$speed', LevelTime='$time', ItemValue='$itemValue' WHERE ID='1' ";
      $resultado = mysqli_query($conexion, $sql);
      //echo "Registro Actualizado";

 $url="http://cofradinn.com/cettic/website/config_game/form.php"; 
 echo "<SCRIPT>window.location='$url';</SCRIPT>"; 

    }
  }

?>





