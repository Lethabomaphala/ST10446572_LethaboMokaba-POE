using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Speech.Synthesis;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Media;


namespace ST10446572_LethaboMokaba_POE
{
    class CyberSecurityChatbot
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Display ASCII Art Logo
            DisplayAsciiArt();
            WelcomeUser();            // Welcome User
            DisplayAsciiLogo();

            // Welcome the user and start chat
            WelcomeUser();
        }

        static void TypeEffect(string text, int delay = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay); // Delay for typing effect
            }
            Console.WriteLine();
        }

        // Method to display an ASCII logo for branding and aesthetics
        static void DisplayAsciiLogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("        .-------.  ");
            Console.WriteLine("       |         |   Welcome to TrustBot!");
            Console.WriteLine("       |  (^ ^)  |   Your Cybersecurity Assistant");
            Console.WriteLine("       |    _    |  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("      /|_________|\\  ");
            Console.WriteLine("     / |_________| \\  ");
            Console.WriteLine("    /  |         |  \\  ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("       |_________|  ");
            Console.WriteLine("          |   |     ");
            Console.WriteLine("          |   |      ");
            Console.WriteLine("          '-' '-'  ");
            Console.ResetColor();





            // Speech synthesizer for voice output
            SpeechSynthesizer synth = new SpeechSynthesizer();

            //Configure the synthesizer
            synth.Volume = 100;
            synth.Rate = 0;

            Console.ForegroundColor = ConsoleColor.Cyan;
            synth.Speak("Hello! Welcome! My name is Trust Bot, Your cyber security guardian");
            Console.ResetColor();
        }

        // Method to ask for user's name and personalize responses
        static void WelcomeUser()
        {
            Console.Write(" Please enter your name: ");
            string userName = Console.ReadLine()?.Trim();

            // If user enters nothing, assign a default name
            if (string.IsNullOrEmpty(userName))
            {
                TypeEffect("I'll call you Safe Searcher!");
                userName = "Safe Searcher";
            }

            // Personalized greeting
            Console.ForegroundColor = ConsoleColor.Yellow;
            TypeEffect($"\nHello, {userName}! I am TrustBot, your cybersecurity assistant.");
            TypeEffect("Ask me anything related to cybersecurity! I am here to practice safe browsing with you!");
            Console.ResetColor();

            // Start interactive chat
            StartChat(userName);
        }

        // Method to handle the chatbot conversation
        static void StartChat(string userName)
        {
            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine()?.Trim().ToLower();

                // Handle empty input
                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeEffect("Please enter something!");
                    Console.ResetColor();
                    continue;
                }

                // Exit condition
                if (input == "exit" || input == "quit" || input == "stop")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    TypeEffect("Goodbye! Remember to browse safely!");
                    Console.ResetColor();
                    break;
                }


            }
        }








                // ASCII Art Display
                static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"\n  ____ Cybersecurity Awareness Bot ____\n");
            Console.ResetColor();
        }

        // Play Pre-recorded Voice Greeting
        public static void PlayAudio(string filePath)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(filePath);
                // Ensure this file exists in the directory
                player.Load();
                player.PlaySync();
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("(Audio playback failed: " + ex.Message + ")");
            }
        }


    }
}
