using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    public class Game
    {
        private long number;
        private decimal numberLevelFive;
        private int level;
        private int points;
        private int attempts;


        public Game()
        {
            this.points = 0;
        }


        public bool game()
        {
            bool correctLevel;

            do
            {
                Console.WriteLine("> Choose your level from 1 to 5! Make sure to type right or we'll ask again :)");
                correctLevel = int.TryParse(Console.ReadLine(), out level);
                correctLevel = correctLevel && (level >= 1 && level <= 5);
            } while (!correctLevel);

            this.generateNumber(); //attempts here

            if (level == 5)
                Console.WriteLine(">>> The number has 4 decimals. Type it in the following format: xxx,xxxx");



            int op; bool validation;
            bool won = false;

            while (attempts > 0)
            {
                int aux = 0;
                do
                {
                    if (aux > 0)
                        Console.WriteLine("Please type a valid option!\n");
                    getOptions();
                    Console.WriteLine($"You have {attempts} attempts !!");
                    Console.WriteLine("Type your option!");
                    validation = int.TryParse(Console.ReadLine(), out op);
                    Console.WriteLine("------------------------------------------------------------------------\n");
                    validation = validation && (op > 0 && op <= 3);
                    aux++;
                } while (!correctLevel);

                Console.Clear();

                if (op == 3)
                    won = (level == 5) ? takeAGuessLevelFive() : takeAGuess();
                else
                    this.chooseOption(op);
                
                Console.WriteLine("------------------------------------------------------------------------\n\n\n");

                if (won)
                {
                    Console.WriteLine($"You accumulated {points} points!");
                    Console.WriteLine("------------------------------------------------------------------------\n\n\n");

                    return true;
                }
            }

            if (!won)
                Console.WriteLine("Your attempts are over :/");
            Console.WriteLine($"You accumulated {points} points!");
            Console.WriteLine("But... you also lost... Your number was.... ");
            Console.Write((level == 5) ? numberLevelFive.ToString() : number.ToString());
            Console.WriteLine("   ------------------------------\n\n\n");

            return false;
        }

        public void getOptions()
        {
            string option1 = "1 - Check if the number is even\n";
            string option2 = (level == 5) ? ("2 - Find the nearest multiple of 100\n") : ("2 - Know if it is positive or negative\n");
            string option3 = "3 - Guess the number\n";
            Console.WriteLine(option1 + option2 + option3);
        }

        public void chooseOption(int op)
        {
            decimal multiple = Math.Round(numberLevelFive / 100) * 100;

            string option = "";
            if (op == 1)
                option = ((level == 5)? (numberLevelFive % 2 == 0): (number %2 == 0)) ? "It is even" : "It is not even";
            else if (op == 2)
                option = (level == 5) ? $"\nThe nearest 100 multiple is {multiple}\n" : (number >= 0) ? "It is positive" : "It is negative";
           
            
            Console.WriteLine(option);
        }

        private bool takeAGuess()
        {

            Console.WriteLine("Type your guess");
            long guess = 0;
            long.TryParse(Console.ReadLine(), out guess);

            if (guess == number)
            {
                Console.WriteLine($"Congratulations!! You guessed right in the level {level} : {number}\n");
                Console.WriteLine("You get " + level * 5 + " points !!");
                points += level * 5;
                attempts = 0;
                return true;
            }
            else
            {
                Console.WriteLine("You did not guess it right but here's a tip for you:");
                int difference = (int)Math.Abs(number - guess);
                difference = difference / 10;
                Console.WriteLine($"Your guess and the right answer are exactly {difference} dozens of integers away.\n");
                if (difference <= 2)
                {
                    Console.WriteLine("Due to how close you got from the right number, you get 1 points :)");
                    points += 1;
                }
                attempts--;
                return false;
                
            }

        }

        private bool takeAGuessLevelFive()
        {
            Console.WriteLine("Type your guess");
            decimal guess = 0;
            decimal.TryParse(Console.ReadLine(), out guess);



            if (guess == numberLevelFive)
            {
                Console.WriteLine("Congratulations!! You guessed right in the hardest level");
                Console.WriteLine("You get " + 100 + " points !!");
                points += 100;
                attempts = 0;
                return true;
            }
            else
            {
                Console.WriteLine("You did not guess it right but here's a tip for you:");
                long difference = (long) Math.Abs(Math.Abs(numberLevelFive) - Math.Abs(guess));
               
                Console.WriteLine($"Your guess and the right answer are exactly {difference} integers away.\n");
                if (difference == 0)
                {
                    string n1 = numberLevelFive.ToString();
                    string n2 = guess.ToString();

                    int count = n2.Length;
                    for (int i = 0; i < n1.Length; i++)
                    {
                        if ( i < n2.Length)
                        {
                            if (n1[i] == n2[i])
                                count--;
                        }
                    }
                    Console.WriteLine($"To help a bit more, the answer you gave has {(Math.Abs(count))} wrong digits");
                    Console.WriteLine("AND due to how close you got from the right number, you get 7 points :)");
                    Console.WriteLine();
                    points += 7;
                }
                else if(difference <= 1000)
                {
                    Console.WriteLine("Due to how close you got from the right number, you get 5 points :)");
                    points += 5;
                }
                
                attempts--;
                return false;
            }
        }

        private void generateNumber()
        {
            Random random = new Random();

            if (level == 5)
            {
                decimal inferiorLimit = (decimal)-Math.Pow(100.0, double.Parse(level.ToString()));  
                decimal superiorLimit = inferiorLimit * (-1); 

                attempts = 38;

                Console.WriteLine($"Your number is between {inferiorLimit} and {superiorLimit}");
                Console.WriteLine($"You have {attempts} attempts.");


                numberLevelFive = (decimal)(random.NextDouble() * ((double)superiorLimit - (double)inferiorLimit) + (double)inferiorLimit);

                numberLevelFive = Math.Round(numberLevelFive, 4);

            }
            else
            {
                long inferiorLimit = (long)-Math.Pow(100.0, double.Parse(level.ToString()));
                long superiorLimit = inferiorLimit * (-1); 

                attempts = (int)Math.Pow(level, 3) + 5;

                number = (long)(random.NextDouble() * (superiorLimit - inferiorLimit) + inferiorLimit);
                Console.WriteLine($"Your number is between {inferiorLimit} and {superiorLimit}");
                Console.WriteLine($"You have {attempts} attempts.");

            }

        }
    }

}
