<?php

  $servidor  = 'localhost';
  $usuario   = 'andrespr_demo';
  $pass    = '123456';
  $baseDatos = 'andrespr_cettic_demo';
        $tabla     = 'ConfigGame';

  $conexion = new mysqli($servidor, $usuario, $pass, $baseDatos);

  $uss    = mysqli_real_escape_string($conexion, $_GET['uss']);

  if(!$conexion){
    echo "error";
  }else{
    
    $sql = "SELECT * FROM $tabla";
    $resultado = mysqli_query($conexion, $sql);
    if(mysqli_num_rows($resultado)>0)
    {
      //mostrar los datos obtenidos
      while ($row = mysqli_fetch_assoc($resultado)) {
        //echo $row['CaharacterSpeed']."|".$row['LevelTime'] ."|".$row['ItemValue']. "<br>";

        // guardando datos en varibales distinatas para luego mostrarlas por separado
        $r_speed = $row['CaharacterSpeed'];
        $r_time = $row['LevelTime'];
        $r_itemValue = $row['ItemValue'];

      }
    }else{
      echo "mal";
    }
  }

?>
