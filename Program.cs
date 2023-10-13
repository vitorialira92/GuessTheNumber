
using GuessTheNumber;
using System;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        introduction();

        string name = Console.ReadLine();
        Console.Clear();
        Console.WriteLine($"Ok, {name}, let's start!");

        play(name);

        Console.WriteLine("See you again soon ;)");

    }

    private static void introduction()
    {
        Console.WriteLine("Oh, hey there! Welcome to guess the number!");
        Console.WriteLine("I will very quickly explain the game.\n");
        Console.WriteLine("Well, pretty much you have to guess the number we have here =D");
        Console.WriteLine("There are five levels from 1 to 5:\n1 - ezpz\n2 - still ezpz\n" +
            "3 - ok...\n4 - hard.\n5 - simply impossible");
        Console.WriteLine("\nDon't worry, we'll help you through it\n\n");
        Console.WriteLine("May you tell us your name?");
    }

    private static void play(string name)
    {
        int op = 1;
        Game gameObject = new();
        bool correctTyping = false;
        bool playing = true;
        int partidas = 0;


        while (playing)
        {
            if(partidas > 0)
            {
                int aux = 0;
                do
                {
                    if (aux > 0)
                        Console.WriteLine("Please type a valid option.\n");
                    Console.WriteLine("Type 1 to keep playing and 0 to finish the game");
                    correctTyping = int.TryParse(Console.ReadLine(), out op);
                    correctTyping = correctTyping ? (op == 0 || op == 1) : false;
                    Console.Clear();
                    aux++;
                } while (!correctTyping);

                if(op == 0)
                {
                    return;
                }
            }

            Console.WriteLine($">> Player {name}");
            Console.WriteLine($"------------->> Starting round number {partidas}");
            gameObject.game();
            partidas++;
        
        
        }


        

        
    }

}
