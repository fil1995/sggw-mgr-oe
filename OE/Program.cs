using System;

class Program
{

    static void Main(string[] args)
    {
        Random random = new Random();

        // Console.WriteLine(Type.GetType("SelectionRouletteRank"));

        //Cities c = new Cities("fm9.tsp");
        //Organism o = new Organism(c, GenotypeRepresentation.ADJACENCYLIST);
        //o.SetRandomGenotype(random);
        //o.genotype = new uint[] { 5,  8 , 1 , 6 , 2 , 4 , 7 , 0 , 3 };
        //Organism o2 = new Organism(c, GenotypeRepresentation.ADJACENCYLIST);
        //o2.SetRandomGenotype(random);
        //o.genotype = new uint[] { 2 , 7 , 6 , 0 , 1,  4,  8 , 3 , 5 };
        //Console.WriteLine(o);
        //Console.WriteLine(o2);
        //Crossover cr = new CrossoverAdjacencyAlternatingEdges();
        //Organism o3 = cr.Cross(o, o2, random);
        //Console.WriteLine(o3);
        //Console.WriteLine(o.IsVaild);
        //Console.WriteLine(o2.IsVaild);
        //Console.WriteLine(o3.IsVaild);

        //Organism o4 = new Organism(c, GenotypeRepresentation.PATH);
        //o4.genotype = new uint[] { 0,1,2,3,4,5,6,7,8 };

        // Test01 t = new Test01(random);

        ////////////////////// fm9  wi29  27603. dj38 6656    uy734   79114     lu980     11340     vm22775    569,288

        Algorithm a = new Algorithm(random,
                                                                new StopNumEpochs(100),
                                                                new SelectionTournament(),
                                                                new CrossoverPathCX(),
                                                                new MutationPathTwoOpt(0.5),
                                                                "uy734.tsp",
                                                                100, true, false
                                                                );
        a.Run(false, "test.txt");


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

//- krzyżowanie przez naprzemienne wybieranie krawędzi(str. 230 [1]), ok
//- krzyżowanie przez wymianę podtras(j.w.), ok
//- PMX(str. 234 [1]),  ok
//- OX(str. 235 [1]), ok
//- CX(str. 236 [1]), ok
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