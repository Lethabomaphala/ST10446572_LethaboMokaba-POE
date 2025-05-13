using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Speech.Synthesis;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Media;
using System.Collections.Generic;


namespace ST10446572_LethaboMokaba_POE
{
    class CyberSecurityChatbot
    {

        // Arrays for random responses
        private static readonly string[] passwordTips = {
            "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.",
            "Consider using a passphrase instead of a password - something like 'PurpleTurtleJumpedHigh!' is both strong and memorable.",
            "A good password should be at least 12 characters long and include a mix of uppercase, lowercase, numbers, and symbols.",
            "Never reuse passwords across different accounts. If one gets compromised, they all become vulnerable."
        };

        private static readonly string[] scamTips = {
            "Be wary of unsolicited calls or messages asking for personal information. Legitimate organizations won't ask for sensitive data this way.",
            "If an offer seems too good to be true, it probably is. Scammers often use unrealistic promises to lure victims.",
            "Check for poor grammar and spelling in messages - these are often signs of a scam attempt.",
            "Scammers often create a sense of urgency. Take your time to verify any requests for money or information."
        };

        private static readonly string[] privacyTips = {
            "Regularly review privacy settings on your social media accounts and apps to control what information you share.",
            "Be cautious about what personal information you post online - it can be used for identity theft or social engineering attacks.",
            "Consider using a VPN when connecting to public Wi-Fi networks to protect your online privacy.",
            "Use private browsing modes or search engines that don't track your activity if you're concerned about privacy."
        };

        private static readonly string[] phishingTips = {
            "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.",
            "Hover over links before clicking to see the actual URL. Phishing sites often use slight misspellings of legitimate addresses.",
            "Look for poor grammar and formatting in emails - these are common signs of phishing attempts.",
            "Never enter login credentials on a site you reached through an email link. Always navigate directly to the official website."
        };

        private static readonly string[] greetings = {
            "I'm doing great! Ready to help with all your cybersecurity questions!",
            "Feeling secure and ready to protect you from online threats!",
            "My day is going well! How about yours?",
            "I'm functioning at optimal security levels! How can I assist you?",
            "Everything's running smoothly in my digital world! What's on your mind?"
        };



        private static readonly string[] pharmingTips = {
            "Pharming redirects you to fake websites even when you type the correct address. Always check for HTTPS and the padlock icon.",
            "To prevent pharming attacks, keep your router firmware updated and use trusted DNS servers like Google (8.8.8.8) or Cloudflare (1.1.1.1).",
            "Pharming attacks often target financial websites. Bookmark your bank's real website and only use that link to access it.",
            "Install security software that can detect and block pharming attempts before they redirect you to malicious sites."
        };


        // User memory storage
        private static Dictionary<string, string> userMemory = new Dictionary<string, string>();
        private static string currentTopic = "";

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            DisplayAsciiLogo();              // Display ASCII Art Logo
            SpeakWelcomeMessage();
            WelcomeUser();            // Method to Welcome User



        }






        // Method to display an ASCII logo 
        static void DisplayAsciiLogo()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            string asciiArt = @"
                                                                            

 .-') _   _  .-')                .-')    .-') _  .-. .-')                .-') _    
(  OO) ) ( \( -O )              ( OO ). (  OO) ) \  ( OO )              (  OO) )   
/     '._ ,------. ,--. ,--.   (_)---\_)/     '._ ;-----.\  .-'),-----. /     '._  
|'--...__)|   /`. '|  | |  |   /    _ | |'--...__)| .-.  | ( OO'  .-.  '|'--...__) 
'--.  .--'|  /  | ||  | | .-') \  :` `. '--.  .--'| '-' /_)/   |  | |  |'--.  .--' 
   |  |   |  |_.' ||  |_|( OO ) '..`''.)   |  |   | .-. `. \_) |  |\|  |   |  |    
   |  |   |  .  '.'|  | | `-' /.-._)   \   |  |   | |  \  |  \ |  | |  |   |  |    
   |  |   |  |\  \('  '-'(_.-' \       /   |  |   | '--'  /   `'  '-'  '   |  |    
   `--'   `--' '--' `-----'     `-----'    `--'   `------'      `-----'    `--'    

                                (\_/)   
                                (^.^)   I'm here to help<3  
                                (\""\"")("")  

";


            Console.WriteLine(asciiArt);
            Console.ResetColor();




        }

        static void SpeakWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.White;
            TypeEffect("TrustBot, Your #1 Trusted Bot For Safe Searching");
            //path to the audio file
            string audioFilePath = @"C:\Users\RC_Student_lab\OneDrive - ADvTECH Ltd\VisualStudio\ST10446572_LethaboMokaba-POE\HumeAI_voice-preview_cyber2.wav";

            try
            {
                // play the .wav welcome audio file
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing audio: " + ex.Message);
            }


        }

        // Method to ask for user's name and personalize responses
        static void WelcomeUser()
        {
            Console.Write(" Please enter your name: ");
            string userName = Console.ReadLine()?.Trim();

            // If user enters nothing, a default name will be assigned 
            if (string.IsNullOrEmpty(userName))
            {
                TypeEffect("I'll call you Safe Searcher!");
                userName = "Safe Searcher";
            }

            // personalized greeting with user's entered name 
            Console.ForegroundColor = ConsoleColor.Yellow;
            TypeEffect($"\nHello, {userName}! I am TrustBot, your cybersecurity assistant.");
            TypeEffect("Ask me anything related to cybersecurity! I am here to help you practice safe browsing with you!");
            Console.ResetColor();

            // start interaction between bot and user
            StartChat(userName);

        }

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

                if (input == "exit" || input == "quit" || input == "stop")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    TypeEffect("Goodbye! Remember to browse safely!");
                    Console.ResetColor();
                    break;
                }
                ProcessUserQuery(userName, input);





            }


        }






        // Method with bot's pre-saved responses
        static void ProcessUserQuery(string userName, string input)
        {

            // Check for general conversation first
            if (ProcessGeneralConversation(input, userName))
                return;

            // Enhanced keyword recognition
            if (ContainsKeyword(input, "password") || ContainsKeyword(input, "passwords"))
            {
                currentTopic = "password";
                var random = new Random();
                string response = passwordTips[random.Next(passwordTips.Length)];
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeEffect(response);
                AskFollowUp("Would you like more password tips?");
            }
            else if (ContainsKeyword(input, "scam") || ContainsKeyword(input, "scams"))
            {
                currentTopic = "scam";
                var random = new Random();
                string response = scamTips[random.Next(scamTips.Length)];
                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeEffect(response);
                AskFollowUp("Should I tell you more about spotting scams?");
            }
            else if (ContainsKeyword(input, "privacy") || ContainsKeyword(input, "private"))
            {
                currentTopic = "privacy";
                var random = new Random();
                string response = privacyTips[random.Next(privacyTips.Length)];
                Console.ForegroundColor = ConsoleColor.Blue;
                TypeEffect(response);
                AskFollowUp("Want to learn more about protecting your privacy online?");
            }
            else if (ContainsKeyword(input, "phishing"))
            {
                currentTopic = "phishing";
                var random = new Random();
                string response = phishingTips[random.Next(phishingTips.Length)];
                Console.ForegroundColor = ConsoleColor.Green;
                TypeEffect(response);
                AskFollowUp("Would you like another phishing prevention tip?");
            }
            else if (ContainsKeyword(input, "cybersecurity"))
            {

                Console.ForegroundColor = ConsoleColor.Magenta;


                TypeEffect("Cybersecurity involves the practices, technologies, and processes designed " +
                    "to protect computers, networks, programs, and data from digital attacks, theft, " +
                    "damage, or unauthorized access. These measures are vital for securing sensitive " +
                    "information, ensuring privacy, and maintaining the integrity of systems in the " +
                    "face of increasing threats from cybercriminals and hackers.");
            }

            else if (ContainsKeyword(input, "malware"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeEffect("Malware, short for malicious software refers to any type of software designed" +
                    " to harm or exploit your computer, device, or data. They usually present themselves as viruses," +
                    "worms,ransomwares, trojans, and/or  spyware. You can prevent these by using antivirus software" +
                    "and keeping it updated; be cautious with downloads and email attachments and avoid suspicious links" +
                    "and websites.");
            }

            else if (ContainsKeyword(input, "pharming") || ContainsKeyword(input, "dns poisoning") || ContainsKeyword(input, "website redirect"))
            {
                currentTopic = "pharming";
                var random = new Random();
                string response = pharmingTips[random.Next(pharmingTips.Length)];
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                TypeEffect(response);
                AskFollowUp("Should I explain more about pharming protection?");
            }


            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                TypeEffect("I don't have the answer for that YET :( but you can ask me something else " +
                    "about cyber security! :)");
                Console.ResetColor();

            }

            RememberUserPreferences(input);
        }

        static bool ProcessGreetings(string input, string userName)
        {
            if (ContainsKeyword(input, "hello") || ContainsKeyword(input, "hi") || ContainsKeyword(input, "hey"))
            {
                var random = new Random();
                string greeting = greetings[random.Next(greetings.Length)];
                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeEffect($"{greeting} What cybersecurity topic can I help with today, {userName}?");
                return true;
            }
            else if (ContainsKeyword(input, "how are you") || ContainsKeyword(input, "how you feeling") || ContainsKeyword(input, "how's your day"))
            {
                var random = new Random();
                string response = greetings[random.Next(greetings.Length)];
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeEffect(response);
                return true;
            }
            return false;
        }


        static bool ProcessGeneralConversation(string input, string userName)
        {
            if (input.Contains("thank") || input.Contains("thanks"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeEffect($"You're welcome, {userName}! Stay safe online!");
                return true;
            }
            else if (input.Contains("hi") || input.Contains("hello") || input.Contains("hey"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeEffect($"Hello again, {userName}! What cybersecurity topic can I help with today?");
                return true;
            }
            else if (input.Contains("yes") && !string.IsNullOrEmpty(currentTopic))
            {
                ContinueTopic(currentTopic);
                return true;
            }
            else if (input.Contains("no") && !string.IsNullOrEmpty(currentTopic))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeEffect("No problem! What else would you like to know about?");
                currentTopic = "";
                return true;
            }
            return false;
        }

        static void ContinueTopic(string topic)
        {
            var random = new Random();
            string response = "";

            switch (topic)
            {
                case "password":
                    response = passwordTips[random.Next(passwordTips.Length)];
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "scam":
                    response = scamTips[random.Next(scamTips.Length)];
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "privacy":
                    response = privacyTips[random.Next(privacyTips.Length)];
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "phishing":
                    response = phishingTips[random.Next(phishingTips.Length)];
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "pharming":
                    response = pharmingTips[random.Next(pharmingTips.Length)];
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
            }

            TypeEffect(response);
            AskFollowUp($"Would you like another {topic} tip?");
        }

        static void AskFollowUp(string question)
        {
            Console.ForegroundColor = ConsoleColor.White;
            TypeEffect(question + " (yes/no)");
            Console.ResetColor();
        }

        static void RememberUserPreferences(string input)
        {
            if (input.Contains("like") || input.Contains("love") || input.Contains("interested"))
            {
                if (input.Contains("password"))
                {
                    userMemory["favoriteTopic"] = "password security";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeEffect("I'll remember you're interested in password security. It's a crucial topic!");
                }
                else if (input.Contains("privacy"))
                {
                    userMemory["favoriteTopic"] = "online privacy";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeEffect("Noted! You care about online privacy - a smart choice in today's digital world!");
                }
                else if (input.Contains("scam") || input.Contains("phishing"))
                {
                    userMemory["favoriteTopic"] = "avoiding scams";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeEffect("I see you're concerned about scams. That's very wise in today's online environment!");
                }
                Console.ResetColor();
            }
        }

        // helper method to ensure that user input contains any keywords
        static bool ContainsKeyword(string input, string keyword)
        {
            return Regex.IsMatch(input, @"\b" + Regex.Escape(keyword) + @"\b", RegexOptions.IgnoreCase);
        }
        static void TypeEffect(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30); // stimulates typing effect
            }
            Console.WriteLine();
        }

    }




}


