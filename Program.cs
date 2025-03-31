using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ST10446572_LethaboMokaba_POE
{
    class CyberSecurityChatbot
    {
        static void Main()
        {
            // Display ASCII Art Logo
            DisplayAsciiArt();

            // Play Voice Greeting
            // Welcome User
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Hello! What is your name? ");
            Console.ResetColor();
            string userName = Console.ReadLine();

            Console.WriteLine($"\nWelcome, {userName}! I am your Cybersecurity Awareness Bot.\n");

            // Main Chat Loop
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Ask me about cybersecurity (or type 'exit' to quit): ");
                Console.ResetColor();
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "exit")
                {
                    Console.WriteLine("Goodbye! Stay safe online.");
                    break;
                }

                
            }




            string audioFilePath = @"C:\Users\RC_Student_lab\OneDrive-ADvTECH-Ltd\VisualStudio\ST10446572_LethaboMokaba-POE\HumeAI_voice-preview_cyber1.wav";

            PlayAudio(audioFilePath);
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
