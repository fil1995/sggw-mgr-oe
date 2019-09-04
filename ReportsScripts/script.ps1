$OE = '..\OE\bin\Debug\netcoreapp2.1\publish\OE.exe';
$PATHtoGNUPLOT = '..\..\gnuplot\bin\gnuplot.exe';

$PATHtspFile = '..\tspFiles\wi29.tsp' # uy734  dj38

$PopulationSize = 1000;
$Epochs = 1000;
$Runs = 1;
$DATAstopu="2019-07-01 00:00:00";
#MutationAdjacencyUsingPathTwoOpt

& $OE $DATAstopu $Epochs StopNumEpochs SelectionRoulette 0 CrossoverPathPMX 0,5 MutationPathTwoOpt $PopulationSize $Runs $PATHtspFile PMX2.csv
& $OE $DATAstopu $Epochs StopNumEpochs SelectionRoulette 0 CrossoverPathOX 0,5 MutationPathTwoOpt $PopulationSize $Runs $PATHtspFile OX2.csv

#& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverAdjacencyAlternatingEdges  MutationAdjacencyUsingPathTwoOpt $PATHtspFile $PopulationSize $Runs  AlternatingEdges.csv
#& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverAdjacencySubtourChunks MutationAdjacencyUsingPathTwoOpt $PATHtspFile $PopulationSize $Runs SubtourChunks.csv
#& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverOrdinalSinglePoint MutationOridinalOnePoint $PATHtspFile $PopulationSize $Runs OrdinalSinglePoint.csv
#& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverPathCX MutationPathTwoOpt $PATHtspFile $PopulationSize $Runs CX.csv
#& $OE $Epochs StopNumEpochs SelectionTournament 0,5 CrossoverPathOX MutationPathTwoOpt $PATHtspFile $PopulationSize $Runs OX.csv
#& $OE $Epochs StopNumEpochs SelectionTournament 0,1 CrossoverPathPMX MutationPathTwoOpt $PATHtspFile $PopulationSize $Runs PMX.csv
#& $OE $Epochs StopNumEpochs SelectionTournament 0,1 InverOver MutationNone $PATHtspFile $PopulationSize $Runs InverOver.csv


#& $PATHtoGNUPLOT "generatechart.gnu"



#        //      0           1           2           3           4           5               6               7
#        ///     DATAstopu   StopArg     StopType    CrossArg    CrossType   MutationArg     MutationType    PopulationSize 
#        ///     
#        ///     8           9
#        ///     NumberRuns  TspFile