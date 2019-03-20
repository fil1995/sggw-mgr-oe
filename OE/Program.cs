using System;

class Program
{

    static void Main(string[] args)
    {
        Random random = new Random();


        //Algorithm a = new Algorithm(random,
        //                                                        new StopNumEpochs(700),
        //                                                        new SelectionRouletteRank(),
        //                                                        20, 0.4, true
        //                                                        );
        //a.Run<Organism>();


        new TestAlgorithm<OrganismWithRepair>(
                                                new Algorithm(random,
                                                                new StopNumEpochs(700),
                                                                new SelectionRouletteRank(),
                                                                20, 0.2, false
                                                                )
                                                );

        Console.ReadKey();
    }
}


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