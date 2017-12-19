<?php

// Explicacion: http://easy-codigo.blogspot.cl/2012/06/graficas-con-jpgraph.html

require_once ('jpgraph/jpgraph.php');
require_once ('jpgraph/jpgraph_line.php');
 
// Some data
$ydata1 = array(11,3,8,12,5,1,9,13,5,7);
$ydata2 = array(1,2,3,4,3,2,4,5,9,7);
 
// Create the graph. These two calls are always required
$graph = new Graph(350,250);
$graph->SetScale('textlin');
 
// Create the linear plot
$lineplot1 = new LinePlot($ydata1);
$lineplot1->SetColor('blue');

// Create the linear plot
$lineplot2 = new LinePlot($ydata2);
$lineplot2->SetColor('red');
 
// Add the plot to the graph
$graph->Add($lineplot1);
$graph->Add($lineplot2);
 
// Display the graph
$graph->Stroke();



/*
require_once ('jpgraph/jpgraph.php');
require_once ('jpgraph/jpgraph_bar.php');

// Creamos el grafico
$datos=array(6,5,8,6);
$labels=array("pepe","juanita","Maria","Luis");

$grafico = new Graph(500, 400, 'auto');
$grafico->SetScale("textlin");
$grafico->title->Set("Ejemplo de Grafica");
$grafico->xaxis->title->Set("trabajadores");
$grafico->xaxis->SetTickLabels($labels);
$grafico->yaxis->title->Set("Horas Trabajadas");

$barplot1 =new BarPlot($datos);
$barplot1->SetWidth(30); // 30 pixeles de ancho para cada barra

$grafico->Add($barplot1);
$grafico->Stroke();







require_once ('jpgraph/jpgraph.php');
require_once ('jpgraph/jpgraph_line.php');
 
// Some data
$ydata = array(11,3,8,12,5,1,9,13,5,7);
 
// Create the graph. These two calls are always required
$graph = new Graph(350,250);
$graph->SetScale('textlin');
 
// Create the linear plot
$lineplot=new LinePlot($ydata);
$lineplot->SetColor('blue');
 
// Add the plot to the graph
$graph->Add($lineplot);
 
// Display the graph
$graph->Stroke();


 */

?>
