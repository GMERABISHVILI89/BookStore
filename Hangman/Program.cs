using System;

namespace HangmanGame
{
    class Program
    {
        static string wordToGuess = String.Empty;
        static char[] guessedLetters;
        static int attempts;

        static void Main(string[] args)
        {
            string[] words = { "easy", "world", "game", "happy", "code", "fun", "play", "test", "quiz", "hard" };

            while (true)
            {
                ResetGameState(words);
                StartNewGame();

                Console.WriteLine("Want to play again? (Y/N): ");
                var response = Console.ReadLine().ToLower();

                if (response != "y")
                {
                    Console.WriteLine("Thanks for playing..");
                    break;
                }
                Console.Clear();
            }
        }


        //static helper methods
        static void ResetGameState(string[] words)
        {
            Random random = new Random();
            int randomIndex = random.Next(words.Length);
            wordToGuess = words[randomIndex];

            guessedLetters = new char[wordToGuess.Length];
            for (int i = 0; i < guessedLetters.Length; i++)
            {
                guessedLetters[i] = '_';
            }

            attempts = 5;
        }

        static void StartNewGame()
        {
            Console.WriteLine("Wellcome to HangMan Game");

            PrintWord(guessedLetters);

            while (attempts > 0 && !IsWordGuessed(guessedLetters, wordToGuess))
            {
                Console.WriteLine("");
                Console.WriteLine("Guess a letter:");

                char guess = Console.ReadKey().KeyChar;
                Console.WriteLine();

                bool found = false;

                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i] == guess)
                    {
                        guessedLetters[i] = guess;
                        found = true;
                    }
                }

                if (!found)
                {
                    attempts--;
                    Console.WriteLine("Incorrect guess. Attempts remaining: " + attempts);
                }
                else
                {
                    Console.WriteLine("Correct guess.");
                }

                PrintWord(guessedLetters);
            }

            if (IsWordGuessed(guessedLetters, wordToGuess))
            {
                Console.WriteLine("Congratulations! You guessed the word: " + wordToGuess);
            }
            else
            {
                Console.WriteLine("You ran out of attempts. The word was: " + wordToGuess);
            }
        }

        static bool IsWordGuessed(char[] guessedLetters, string wordToGuess)
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (guessedLetters[i] != wordToGuess[i])
                {
                    return false;
                }
            }
            return true;
        }

        static void PrintWord(char[] guessedLetters)
        {
            foreach (char letter in guessedLetters)
            {
                Console.Write(letter + " ");
            }
            Console.WriteLine();
        }
    }
}