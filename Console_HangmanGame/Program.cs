﻿using System;

namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = { "easy", "world", "game", "happy", "code", "fun", "play", "test", "quiz",  "hard" };
            Random random = new Random();
            // random 
            int randomIndex = random.Next(words.Length);
            // word
            string wordToGuess = words[randomIndex];
            //char array გადაყვანა რათა გავიგოთ რამდენი char-ისგან შედგება სიტყვა
            char[] guessedLetters = new char[wordToGuess.Length];
           
            int attempts = 5;

        
            for (int i = 0; i < guessedLetters.Length; i++)
            {
                guessedLetters[i] = '_';
                Console.Write(" _  ");
            }
            Console.WriteLine("  : Guess a Word");

            while (attempts > 0 && !IsWordGuessed(guessedLetters, wordToGuess))
            {
                Console.WriteLine("");
                Console.WriteLine("Guess a letter:");

                //მომხმარებლის მიერ კლავიატურაზე ნებისმიერი ღილაკის წაკითხვა როგორც char
                char guess = Console.ReadKey().KeyChar;
                Console.WriteLine();

                bool found = false;

                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    //შემოწმება მთავარი სიტყვის და ჩაწერილის char
                    if (wordToGuess[i] == guess)
                    {
                        //თუ დაემთხვა _ გადაკეთდება guess char-რაც იქნება
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
                    Console.WriteLine("congrats you are correct and you have : " + attempts + " attempts");
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
                Console.WriteLine("");
                Console.WriteLine("Want to play again ?  Y/N ");
                var response = Console.ReadLine();

                // i need help here
                if (response.ToLower() == "y")
                {
                    return;
                }

            }
        }

        static bool IsWordGuessed(char[] guessedLetters, string wordToGuess)
        {
            //guessedLetters  _ _ _ _ _   char[5], (world)
            //- wordToGuess length

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