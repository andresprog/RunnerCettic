<?php

// Explicacion: http://easy-codigo.blogspot.cl/2012/06/graficas-con-jpgraph.html

require_once ('jpgraph/jpgraph.php');
require_once ('jpgraph/jpgraph_line.php');
 
// Some data
$ydata = array(11,3,8,12,5,1,9,13,5,7);
 
// Create the graph. These two calls are always required
$graph = new Graph(350,250);
$graph->SetScale('textlin');
 
// Create the linear plot
$lineplot = new LinePlot($ydata);
$lineplot->SetColor('blue');

// Add the plot to the graph
$graph->Add($lineplot);
 
// Display the graph
$graph->Stroke();


?>
