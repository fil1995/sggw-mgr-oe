#!/bin/sh
pliki=("CX.csv" "AlternatingEdges.csv" "SubtourChunks.csv" "OrdinalSinglePoint.csv" "OX.csv" "PMX.csv" "InverOver.csv")

for file in ${pliki[@]}; do
echo $file
cat $file |awk -F';' {'if(NR==2 || NR==100 || NR==200 || NR==500 || NR==1000 || NR==2000 || NR==4000 || NR==8000 || NR==10000 )
			{
				print $3
			}
'}
done
