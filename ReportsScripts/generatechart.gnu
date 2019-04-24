set encoding utf8
set terminal svg size 600,400 dynamic enhanced font 'arial,10' mousing name "fillcrvs_1" butt dashlength 1.0 
set output 'out.svg'

#set term png size 800, 600
#set output 'out.png'

set title "Porównanie metod krzyżowania" 
set title  font ",20" norotate
set xlabel "Epoki"
set ylabel "Odległość"

set key bottom center outside horizontal

set datafile separator ';'

plot 	'PMX.csv' using 3 title 'PMX' with lines linewidth 2,\
		'CX.csv' using 3 title 'CX' with lines linewidth 2,\
		'OX.csv' using 3 title 'OX' with lines linewidth 2,\
		'OrdinalSinglePoint.csv' using 3 title 'OrdinalSinglePoint' with lines linewidth 2,\
		'AlternatingEdges.csv' using 3 title 'AlternatingEdges' with lines linewidth 2,\
		'SubtourChunks.csv' using 3 title 'SubtourChunks' with lines linewidth 2,\
		'InverOver.csv' using 3 title 'InverOver' with lines linewidth 2

