$PATHtoOE = '..\OE\bin\Debug\netcoreapp2.1\publish\OE.exe';
$PATHtoGNUPLOT = '..\..\gnuplot\bin\gnuplot.exe';

$PopulationSize = 500;
$Epochs = 1000;
$Runs = 10;



& $PATHtoOE $Epochs StopNumEpochs SelectionRoulette 0,5 CrossoverAdjacencyAlternatingEdges  MutationAdjacencyUsingPathTwoOpt "..\tspFiles\dj38.tsp" $PopulationSize $Runs  AlternatingEdges.csv
& $PATHtoOE $Epochs StopNumEpochs SelectionRoulette 0,5 CrossoverAdjacencySubtourChunks MutationAdjacencyUsingPathTwoOpt "..\tspFiles\dj38.tsp" $PopulationSize $Runs SubtourChunks.csv

& $PATHtoOE $Epochs StopNumEpochs SelectionRoulette 0,5 CrossoverPathCX MutationPathTwoOpt "..\tspFiles\dj38.tsp" $PopulationSize $Runs CX.csv
& $PATHtoOE $Epochs StopNumEpochs SelectionRoulette 0,5 CrossoverPathOX MutationPathTwoOpt "..\tspFiles\dj38.tsp" $PopulationSize $Runs OX.csv
& $PATHtoOE $Epochs StopNumEpochs SelectionRoulette 0,5 CrossoverPathPMX MutationPathTwoOpt "..\tspFiles\dj38.tsp" $PopulationSize $Runs PMX.csv
& $PATHtoOE $Epochs StopNumEpochs SelectionRoulette 0,5 InverOver MutationNone "..\tspFiles\dj38.tsp" $PopulationSize $Runs InverOver.csv


& $PATHtoGNUPLOT "generatechart.gnu"
