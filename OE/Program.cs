﻿using System;

class Program
{

    static void Main(string[] args)
    {
        Random random = new Random();

        // Console.WriteLine(Type.GetType("SelectionRouletteRank"));

        Cities c = new Cities("fm9.tsp");
        Organism o = new Organism(c, GenotypeRepresentation.PATH);
        o.SetRandomGenotype(random);
        Organism o2 = new Organism(c, GenotypeRepresentation.PATH);
        o2.SetRandomGenotype(random);
        Console.WriteLine(o);
        Console.WriteLine(o2);
        Crossover cr = new CrossoverPathCX();
        Organism o3 = cr.Cross(o, o2, random);
        Console.WriteLine(o3);
        

        // Test01 t = new Test01(random);

        //////////////////////  wi29  27603. dj38 6656    uy734   79114     lu980     11340     vm22775    569,288

        //Algorithm a = new Algorithm(random,
        //                                                        new StopNumEpochs(10000),
        //                                                        new SelectionRouletteRank(),
        //                                                        new CrossoverOrdinalSinglePoint(),
        //                                                        new MutationOridinalOnePoint(0.5), 
        //                                                        "dj38.tsp",
        //                                                        1000, true, false
        //                                                        );
        //a.Run(false, "test.txt");


        //Organism o1 = new Organism();
        //o1.SetRandomGenotype(random, new Cities("wi29.tsp"));
        //Console.WriteLine(o1);

        //Organism o2 = new Organism();
        //o2.SetRandomGenotype(random, new Cities("wi29.tsp"));
        //Console.WriteLine(o2);

        //Organism o3 = o1.RecombinationWithMutation(o2, random);
        //Console.WriteLine(o3);

        //Organism o4 = o1.Mutation(random,0.5);
        //Console.WriteLine(o4);

        //new TestAlgorithm<OrganismWithRepair>(
        //                                        new Algorithm(random,
        //                                                        new StopNumEpochs(700),
        //                                                        new SelectionRouletteRank(),
        //                                                        20, 0.2, false
        //                                                        )
        //                                        );

        Console.ReadKey();
    }
}


/// LAB 07
//Do zaimplementowania i przebadania różne sposoby krzyżowania: 

//- krzyżowanie przez naprzemienne wybieranie krawędzi(str. 230 [1]),
//- krzyżowanie przez wymianę podtras(j.w.),
//- PMX(str. 234 [1]),  ok
//- OX(str. 235 [1]),
//- CX(str. 236 [1]),
//- inver-over(str. 257 [1]).




/// <summary>
/// LAB 03
/// Stworzyć trzy wersje algorytmu implementując wybrane strategie radzenia sobie z ograniczeniami.
/// 
/// tu robie 3 różne możliwości, czyli jedno takie, że 
/// Ograniczenie dziedziny, tak, aby nie możliwe były osobniki spoza
/// usuwam te spoza dziedziny i losuję nowe zamiast nich
/// naprawiam te, które są spoza dziedziny - np odbijanie, lub odejmowanie jakiejś wartości - nie obcinamy, bo wtedy możemy dążyć do jakiejś wartości niepotrzebnie
/// 
/// 
//Rozbudować program tak, aby móc stwierdzić, jak szybko program osiąga osobnika o wartości 80%, 90% i 95% najlepszego.
/// czyli odpalamy algorytm mamy po 1000 epokach watość 10, czyli w którym pokoleniu był taki co miał wartość 8? 9 ? itp...

//Rozbudować program tak, aby przebadać działanie programu przy różnym kryterium stopu zależnego od:
//- liczby pokoleń,
//- czasu obliczeń,
//- stopienia poprawy w k ostatnich pokoleń,
//- różnorodności populacji.
/// 
/// </summary>