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
        //o.genotype = new uint[] { 1 ,2, 7, 6, 8, 0, 3, 4, 5 };
        //Organism o2 = new Organism(c, GenotypeRepresentation.ADJACENCYLIST);
        //o2.SetRandomGenotype(random);
        //o2.genotype = new uint[] { 6, 4, 0, 5, 8, 1, 7, 3, 2 };
        //Console.WriteLine(o.Genotype + o);
        //Console.WriteLine(o2.Genotype + o2);
        //Crossover cr = new CrossoverAdjacencyAlternatingEdges();
        //Organism o3 = cr.Cross(o, o2, random);
        //Console.WriteLine(o3.Genotype + o3);
        //Console.WriteLine(o.IsVaild);
        //Console.WriteLine(o2.IsVaild);
        //Console.WriteLine(o3.IsVaild);

        //Console.ReadKey();
        //Organism o4 = new Organism(c, GenotypeRepresentation.PATH);
        //o4.genotype = new uint[] { 0,1,2,3,4,5,6,7,8 };

        //Test01 ta = new Test01(random, new StopNumEpochs(200),
        //                                new SelectionTournament(),
        //                                new CrossoverPathOX(),
        //                                new MutationPathTwoOpt(0.5),
        //                                new Cities("uy734.tsp"),
        //                                40,
        //                                4,
        //                                "plik.txt"
        //                                );
        //Console.ReadKey();
        //return;
        ////////////////////// fm9  wi29  27603. dj38 6656    uy734   79114     lu980     11340     vm22775    569,288

        //Algorithm a = new Algorithm(random,
        //                                                        new StopNumEpochs(50000),
        //                                                        new SelectionTournament(),
        //                                                        new CrossoverPathPMX(),
        //                                                        new MutationPathTwoOpt(0.5),
        //                                                        "wi29.tsp",
        //                                                        100, true, false
        //                                                        );
        //a.Run("test.txt");

        //Algorithm a = new Algorithm(random,
        //                                                        new StopNumEpochs(50),
        //                                                        new SelectionRouletteRank(),
        //                                                        new CrossoverOrdinalSinglePoint(),
        //                                                        new MutationNone(0.2),
        //                                                        "dj38.tsp",
        //                                                        100, true, false
        //                                                        );
        //a.Run();


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




        //Algorithm a = new Algorithm(random,
        //                                                        new StopTime(60),
        //                                                        new SelectionTournament(),
        //                                                        new CrossoverPathPMX(),
        //                                                        new MutationPathTwoOpt(0.1),
        //                                                        new Cities("uy734.tsp"),
        //                                                        800, true, false
        //                                                        );
        //a.Run();
        //Console.ReadKey();
        //return;
        // ArgStop Stop Select Cross MutationArg Mutation CitiesFile PopulationSize SaveFile
        /// czytanie z parametrów
        /// 
        //      0           1           2           3           4           5               6               7               8
        ///     DATAstopu   StopArg     StopType    selectType  CrossArg    CrossType       MutationArg     MutationType    PopulationSize 
        ///     
        ///     9           10          11
        ///     NumberRuns  TspFile     outFile

        /// wczytanie danych z wytycznych z wykładu
        // data stopu
        DateTime date =Convert.ToDateTime(args[0]);



        StopCondition stopCondition = null;
        double argument = double.Parse(args[1]);

        switch (args[2])
        {
            case "StopNumEpochs":
                stopCondition = new StopNumEpochs(Convert.ToInt32(argument),date);
                break;
            case "StopPopulationDeviation":
                stopCondition = new StopPopulationDeviation(argument);
                    break;
            case "StopTime":
                stopCondition = new StopTime(Convert.ToInt32(argument),date);
                break;
            default:
                break;
        }
        SelectionType selectionType=null;
        switch (args[3])
        {
            case "SelectionRoulette":
                selectionType = new SelectionRoulette();
                break;
            case "SelectionRouletteRank":
                selectionType = new SelectionRouletteRank();
                break;
            case "SelectionTournament":
                selectionType = new SelectionTournament();
                break;
            default:
                Console.WriteLine("brak selection type");
                break;
        }

        Crossover crossover=null;
        argument = double.Parse(args[4]);
        switch (args[5])
        {
            case "CrossoverAdjacencyAlternatingEdges":
                crossover = new CrossoverAdjacencyAlternatingEdges();
                break;
            case "CrossoverAdjacencySubtourChunks":
                crossover = new CrossoverAdjacencySubtourChunks();
                break;
            case "CrossoverOrdinalSinglePoint":
                crossover = new CrossoverOrdinalSinglePoint();
                break;
            case "CrossoverPathCX":
                crossover = new CrossoverPathCX();
                break;
            case "CrossoverPathOX":
                crossover = new CrossoverPathOX();
                break;
            case "CrossoverPathPMX":
                crossover = new CrossoverPathPMX();
                break;
            case "InverOver":
                crossover = new InverOver(argument);
                break;
            default:
                Console.WriteLine("brak crossover type");
                break;
        }

        Mutation mutation=null;

        argument = double.Parse(args[6]);
        switch (args[7])
        {
            case "MutationAdjacencyUsingPathTwoOpt":
                mutation = new MutationAdjacencyUsingPathTwoOpt(argument);
                break;
            case "MutationNone":
                mutation = new MutationNone(argument);
                break;
            case "MutationOridinalOnePoint":
                mutation = new MutationOridinalOnePoint(argument);
                break;
            case "MutationPathSwap":
                mutation = new MutationPathSwap(argument);
                break;
            case "MutationPathTwoOpt":
                mutation = new MutationPathTwoOpt(argument);
                break;
            default:
                Console.WriteLine("brak mutation type");
                mutation = new MutationNone(argument);
                break;
        }

        int populationSize = int.Parse(args[8]);
        int numberRuns = int.Parse(args[9]);

        Cities cities = new Cities(args[10]);


        Test01 t = new Test01(random,   stopCondition,
                                        selectionType,
                                        crossover,
                                        mutation,
                                        cities,
                                        populationSize,
                                        numberRuns,
                                        args[11]);



        //Console.ReadKey();
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