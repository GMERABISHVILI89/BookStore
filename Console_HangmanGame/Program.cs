using System;

namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = { "easy", "world", "game", "happy", "code", "fun", "play", "test", "quiz",  "hard" };
            Random random = new Random();
            // რენდომად ამოღება სიტყვის
            int randomIndex = random.Next(words.Length);
            // ამოსაცნობი სიტყვა
            string wordToGuess = words[randomIndex];
            //char array გადაყვანა რათა გავიგოთ რამდენი char-ისგან შედგება სიტყვა
            char[] guessedLetters = new char[wordToGuess.Length];
           
            int attempts = 5;

            //  _ გადაყვანა სიტყვის
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

                //თუ ვერ მოიძებნა მცდელობები შემცირდება
                if (!found)
                {
                    attempts--;
                    Console.WriteLine("Incorrect guess. Attempts remaining: " + attempts);
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
            //ეს გამოძახებისას guessedLetters არის _ _ _ _ _  მაგალითად char[5], უკვე (world) - wordToGuess length შესაბამისად

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
            //აქ guessedLetters არის char [] დასაწყისში _ _ _ _ და შემდგომ
            //უკვე გამოცნობილით მაგ:  p _ a _ , ყველ ჯერზე დაბეჯდავს თავიდან
            foreach (char letter in guessedLetters)
            {
                Console.Write(letter + " ");
            }
            Console.WriteLine();
        }
    }
}