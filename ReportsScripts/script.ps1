$OE = '..\OE\bin\Debug\netcoreapp2.1\publish\OE.exe';
$PATHtoGNUPLOT = '..\..\gnuplot\bin\gnuplot.exe';

$PATHtspFile = '..\tspFiles\uy734.tsp' # uy734  dj38

$PopulationSize = 40;
$Epochs = 10000;
$Runs = 5;

#MutationAdjacencyUsingPathTwoOpt

& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverAdjacencyAlternatingEdges  MutationAdjacencyUsingPathTwoOpt $PATHtspFile $PopulationSize $Runs  AlternatingEdges.csv
& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverAdjacencySubtourChunks MutationAdjacencyUsingPathTwoOpt $PATHtspFile $PopulationSize $Runs SubtourChunks.csv
& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverOrdinalSinglePoint MutationOridinalOnePoint $PATHtspFile $PopulationSize $Runs OrdinalSinglePoint.csv
& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverPathCX MutationPathTwoOpt $PATHtspFile $PopulationSize $Runs CX.csv
& $OE $Epochs StopNumEpochs SelectionTournament 0,5 CrossoverPathOX MutationPathTwoOpt $PATHtspFile $PopulationSize $Runs OX.csv
& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverPathPMX MutationPathTwoOpt $PATHtspFile $PopulationSize $Runs PMX.csv
& $OE $Epochs StopNumEpochs SelectionTournament 0,1 InverOver MutationNone $PATHtspFile $PopulationSize $Runs InverOver.csv


& $PATHtoGNUPLOT "generatechart.gnu"

