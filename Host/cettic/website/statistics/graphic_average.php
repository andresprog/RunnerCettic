<?php

// Explicacion: http://easy-codigo.blogspot.cl/2012/06/graficas-con-jpgraph.html

require_once ('jpgraph/jpgraph.php');
require_once ('jpgraph/jpgraph_line.php');
 
// Some data
$ydata = array(1,2,3,4,3,2,4,5,9,7);
 
// Create the graph. These two calls are always required
$graph = new Graph(350,250);
$graph->SetScale('textlin');
 
// Create the linear plot
$lineplot = new LinePlot($ydata);
$lineplot->SetColor('red');

// Add the plot to the graph
$graph->Add($lineplot);
$graph->Add($lineplot); // Esta colocado doble para que cambie de color momentaneamente
// Display the graph
$graph->Stroke();



?>
