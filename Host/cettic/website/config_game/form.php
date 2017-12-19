<!doctype html>
<?php include("read.php") ?>
<html>
<head><meta http-equiv="Content-Type" content="text/html; charset=gb18030">

<title>Game Configuration</title>
<link rel="stylesheet" type="text/css" href="hoja.css">
</head>



<body>

<a href="http://cofradinn.com/cettic/">
 <h1>Ir al Inicio</h1><br>
</a>

<h1>Configuracion Actual</h1>

<form name="form1" method="post" action="">
  <table width="25%" border="0" align="center">

    <tr>
      <td></td>
      <td><label for="id"></label>
      <input type="hidden" name="id" id="id"></td>
    </tr>

    <tr>
      <td>Velocidad del Personaje = </td> 

      <td><label for="nom"></label>
        <?php echo $r_speed ?>
</td>
    </tr>

    <tr>
      <td>Tiempo del Nivel = </td>
      <td><label for="ape"></label>
        <?php echo $r_time ?>
      </td>
    </tr>

    <tr>
      <td>Valor del Item = </td>
      <td><label for="dir"></label>
        <?php echo $r_itemValue ?>
      </td>
    </tr>

  </table>
</form>

<h1>Actualizar Configuracion</h1>




<p>&nbsp;</p>
<form  name="form1" id="formConfig" method="get" action="http://cofradinn.com/cettic/website/config_game/update.php?speed=2&time=2&itemValue=3"> 
  <table width="25%" border="0" align="center">

    <tr>
      <td></td>
      <td><label for="id"></label>
      <input type="hidden" name="id" id="id"></td>
    </tr>

    <tr>
      <td>Velocidad del Personaje </td>
      <td><label for=""></label>
      <input type="number" min="1" max="5" steep="1" name="speed" id="field_speed" value=<?php echo$r_speed ?> style = "text-align: right; "></td>
    </tr>

    <tr>
      <td>Tiempo del Nivel</td>
      <td><label for="ape"></label>
      <input type="number" min="30" max="1000" steep="1"  name="time" id="field_time" value=<?php echo$r_time ?> style = "text-align: right; "></td>
    </tr>

    <tr>
      <td>Valor del Item</td>
      <td><label for="dir"></label>
      <input type="number" min="1" max="100" steep="1"  name="itemValue" id="field_itemValue" value=<?php echo$r_itemValue ?> style = "text-align: right; "></td>
    </tr>

    <tr>
      <td colspan="2"><input type="submit" name="bot_actualizar" id="btn_actualizar" value="Enviar"></td>
    </tr>

  </table>
</form>





<p>&nbsp;</p>
</body>





</html>
