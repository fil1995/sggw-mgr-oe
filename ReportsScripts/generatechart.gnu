# set terminal pngcairo  transparent enhanced font "arial,10" fontscale 1.0 size 600, 400 
set term png size 2000, 1000

set output 'out.png'

set title "Porównanie metod krzyżowania" 
set title  font ",20" norotate
set xlabel "Epoki"
set ylabel "Odległość"

set datafile separator ';'

#set colorbox vertical origin screen 0.9, 0.2, 0 size screen 0.05, 0.6, 0 front  noinvert bdefault
#plot [-pi/2:pi] cos(x),-(sin(x) > sin(x+1) ? sin(x) : sin(x+1))
plot 	'PMX.csv' using 3 title 'PMX' with lines linewidth 3,\
		'CX.csv' using 3 title 'CX' with lines linewidth 3,\
		'OX.csv' using 3 title 'OX' with lines linewidth 3,\
		'AlternatingEdges.csv' using 3 title 'AlternatingEdges' with lines linewidth 3,\
		'SubtourChunks.csv' using 3 title 'SubtourChunks' with lines linewidth 3,\
		'InverOver.csv' using 3 title 'InverOver' with lines linewidth 3
		