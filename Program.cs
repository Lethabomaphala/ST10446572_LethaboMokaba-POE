using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Speech.Synthesis;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Media;
using System.Collections.Generic;
using System.Linq;


namespace ST10446572_LethaboMokaba_POE
{
    class CyberSecurityChatbot
    {

        private static bool ContainsKeyword(string input, string keyword)
        {
            try
            {
                // More robust pattern that handles plurals and common variations
                string pattern = $@"\b{Regex.Escape(keyword)}s?\b";
                return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error in keyword matching: {ex.Message}");
                return false;
            }
        }

        // Improved TypeEffect method with additional features
        private static void TypeEffect(string message, int speed = 30)
        {
            if (string.IsNullOrEmpty(message))
                return;

            try
            {
                // Random slight variation in typing speed for more natural effect
                Random rnd = new Random();

                foreach (char c in message)
                {
                    Console.Write(c);

                    // Add small random variation to typing speed (20-40ms)
                    Thread.Sleep(speed + rnd.Next(-10, 11));

                    // Flush the output to ensure immediate display
                    if (Console.KeyAvailable)
                    {
                        // Allow user to interrupt typing by pressing any key
                        Console.ReadKey(true);
                        Console.Write(message.Substring(message.IndexOf(c) + 1));
                        break;
                    }
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                // Fallback to immediate display if something goes wrong
                Console.WriteLine(message);
                Console.WriteLine($"Error in TypeEffect: {ex.Message}");
            }




        }
        // Unified dictionary for all tips
        private static readonly Dictionary<string, string[]> allTips = new Dictionary<string, string[]>
        {
            {"password", new string[] {
                "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.",
                "Consider using a passphrase instead of a password - something like 'PurpleTurtleJumpedHigh!' is both strong and memorable.",
                "A good password should be at least 12 characters long and include a mix of uppercase, lowercase, numbers, and symbols.",
                "Never reuse passwords across different accounts. If one gets compromised, they all become vulnerable."
            }},
            {"scam", new string[] {
                "Be wary of unsolicited calls or messages asking for personal information. Legitimate organizations won't ask for sensitive data this way.",
                "If an offer seems too good to be true, it probably is. Scammers often use unrealistic promises to lure victims.",
                "Check for poor grammar and spelling in messages - these are often signs of a scam attempt.",
                "Scammers often create a sense of urgency. Take your time to verify any requests for money or information."
            }},
            {"privacy", new string[] {
                "Regularly review privacy settings on your social media accounts and apps to control what information you share.",
                "Be cautious about what personal information you post online - it can be used for identity theft or social engineering attacks.",
                "Consider using a VPN when connecting to public Wi-Fi networks to protect your online privacy.",
                "Use private browsing modes or search engines that don't track your activity if you're concerned about privacy."
            }},
            {"phishing", new string[] {
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.",
                "Hover over links before clicking to see the actual URL. Phishing sites often use slight misspellings of legitimate addresses.",
                "Look for poor grammar and formatting in emails - these are common signs of phishing attempts.",
                "Never enter login credentials on a site you reached through an email link. Always navigate directly to the official website."
            }},
            {"malware", new string[] {
                "Malware includes viruses, worms, trojans, and spyware - all designed to harm your devices",
                "Keep your operating system and software updated to patch security vulnerabilities",
                "Never download attachments from unknown senders",
                "Use reputable antivirus software and scan your devices regularly"
            }},
            {"ransomware", new string[] {
                "Ransomware encrypts your files and demands payment for decryption",
                "Regularly backup important files to an external drive or cloud storage",
                "Never pay the ransom - it doesn't guarantee you'll get your files back",
                "Disconnect infected devices from networks immediately to prevent spread"
            }},
            {"firewall", new string[] {
                "A firewall acts as a barrier between your device and untrusted networks",
                "Enable both software and hardware firewalls for maximum protection",
                "Configure firewall rules to block unnecessary incoming connections",
                "Regularly review and update your firewall settings"
            }},
            {"encryption", new string[] {
                "Encryption scrambles data so only authorized parties can read it",
                "Use end-to-end encrypted messaging apps like Signal for private communications",
                "Encrypt sensitive files and full disk encryption for laptops",
                "Look for HTTPS and padlock icons when entering sensitive information online"
            }},
            {"2fa", new string[] {
                "2FA requires both a password and secondary verification method",
                "Use authenticator apps instead of SMS for more secure 2FA",
                "Enable 2FA on all important accounts: email, banking, social media",
                "Keep backup codes in a secure place in case you lose access"
            }},
            {"ddos", new string[] {
                "DDoS attacks overwhelm systems with traffic to make them unavailable",
                "Use cloud-based DDoS protection services for websites",
                "Configure network hardware to handle traffic spikes",
                "Have an incident response plan for DDoS attacks"
            }},
            {"vpn", new string[] {
                "A VPN encrypts your internet traffic and hides your IP address",
                "Choose a no-logs VPN provider with strong encryption standards",
                "Always connect to VPN when using public Wi-Fi networks",
                "VPNs can slow your connection - balance security and performance needs"
            }},
            {"zero-day", new string[] {
                "Zero-day exploits target unknown vulnerabilities with no patch available",
                "Use application whitelisting to prevent unauthorized programs from running",
                "Segment networks to limit potential damage from zero-day attacks",
                "Monitor systems for unusual activity that might indicate an exploit"
            }},
            {"social engineering", new string[] {
                "Social engineering manipulates people into revealing sensitive information",
                "Be skeptical of urgent requests for information or money",
                "Verify identities through separate communication channels",
                "Never share passwords or sensitive data via email or phone"
            }},
            {"brute force", new string[] {
                "Brute force attacks try every possible password combination",
                "Use long, complex passwords (12+ characters with mixed types)",
                "Implement account lockouts after failed login attempts",
                "Consider rate-limiting login attempts on your systems"
            }},
            {"man-in-the-middle", new string[] {
                "Man-in-the-middle attacks intercept communications between two parties",
                "Only connect to websites using HTTPS encryption",
                "Avoid public Wi-Fi for sensitive transactions without a VPN",
                "Check for certificate warnings in your browser"
            }},
            {"iot", new string[] {
                "IoT devices often have weak security - change default credentials",
                "Segment IoT devices onto separate network partitions",
                "Disable unnecessary features and services on IoT devices",
                "Regularly update IoT device firmware"
            }},
            {"dark web", new string[] {
                "The dark web requires special browsers like Tor to access",
                "Never use personal information when accessing dark web sites",
                "Be extremely cautious - many dark web sites are scams or illegal",
                "Consider professional monitoring services if your data appears on dark web markets"
            }},
            {"hacking", new string[] {
                "Ethical hacking helps identify vulnerabilities before criminals do",
                "Use bug bounty programs to responsibly report vulnerabilities",
                "Never attempt to hack systems without explicit permission",
                "Study cybersecurity to understand hacking techniques for defense"
            }},
            {"cybersecurity", new string[] {
                "Cybersecurity requires layers of protection (defense in depth)",
                "Regularly train employees/staff on security best practices",
                "Have an incident response plan for security breaches",
                "Stay informed about emerging threats and vulnerabilities"
            }},
            {"worm", new string[] {
                "Worms self-replicate across networks without user interaction",
                "Disable autorun features that help worms spread via removable drives",
                "Segment networks to contain potential worm outbreaks",
                "Monitor network traffic for unusual patterns indicating worm activity"
            }},
            {"virus", new string[] {
                "Viruses attach to clean files and spread when those files are executed",
                "Don't open email attachments from unknown senders",
                "Scan all downloaded files before opening them",
                "Disable macros in documents from untrusted sources"
            }},
            {"pharming", new string[] {
                "Pharming redirects you to fake websites even when you type the correct address. Always check for HTTPS and the padlock icon.",
                "To prevent pharming attacks, keep your router firmware updated and use trusted DNS servers like Google (8.8.8.8) or Cloudflare (1.1.1.1).",
                "Pharming attacks often target financial websites. Bookmark your bank's real website and only use that link to access it.",
                "Install security software that can detect and block pharming attempts before they redirect you to malicious sites."
            }}
        };

        private static readonly string[] greetings = {
            "I'm doing great! Ready to help with all your cybersecurity questions!",
            "Feeling secure and ready to protect you from online threats!",
            "My day is going well! How about yours?",
            "I'm functioning at optimal security levels! How can I assist you?",
            "Everything's running smoothly in my digital world! What's on your mind?"
        };

        private static readonly Dictionary<string, string[]> sentimentResponses = new Dictionary<string, string[]>
        {
            {"worried", new string[] {
                "It's completely understandable to feel that way. Let me help you navigate this safely.",
                "Your concern is valid. Cybersecurity can be intimidating, but I'm here to help.",
                "Don't worry - we'll tackle this together. Here's what you should know..."
            }},
            {"frustrated", new string[] {
                "I hear your frustration. Cybersecurity can be complex, but we'll break it down.",
                "I understand this might be frustrating. Let me simplify it for you.",
                "Tech issues can be annoying, but I'll do my best to help you through this."
            }},
            {"confused", new string[] {
                "It's okay to feel confused about this. Let me explain it clearly.",
                "This can be confusing at first. Here's a straightforward explanation.",
                "I'll break this down into simpler terms to help you understand."
            }},
            {"scared", new string[] {
                "Feeling scared about online threats is normal. Let's make you feel more secure.",
                "You're not alone in feeling this way. Here's how we can improve your protection.",
                "Your safety is important. Let me share some reassuring security measures."
            }}
        };

        private static readonly string[] positiveResponses = {
            "Great! Let's dive deeper into this topic.",
            "That's a good attitude to have about security!",
            "I'm glad you're approaching this positively. Here's more information..."
        };


        private static readonly Dictionary<string, string> cybersecurityDefinitions = new Dictionary<string, string>
{
    {"phishing", "A cyberattack where criminals impersonate legitimate entities to steal sensitive information through fake emails or websites."},
    {"pharming", "An attack that redirects users from legitimate websites to malicious ones, often through DNS cache poisoning."},
    {"malware", "Malicious software designed to harm computers, including viruses, worms, trojans, and ransomware."},
    {"ransomware", "Malware that encrypts files and demands payment for their release."},
    {"firewall", "A network security system that monitors and controls incoming/outgoing traffic based on security rules."},
    {"encryption", "The process of encoding information to make it unreadable to unauthorized parties."},
    {"2fa", "Two-Factor Authentication: Requires two forms of verification to access an account."},
    {"ddos", "Distributed Denial of Service: Overwhelming a system with traffic to make it unavailable."},
    {"vpn", "Virtual Private Network: Encrypts internet traffic for secure remote access."},
    {"zero-day", "A vulnerability unknown to the vendor, with no available patch."},
    {"social engineering", "Manipulating people into revealing confidential information."},
    {"brute force", "Trying all possible combinations to guess passwords or encryption keys."},
    {"man-in-the-middle", "Secretly intercepting and possibly altering communications."},
    {"iot", "Internet of Things: Network of physical devices connected to the internet."},
    {"dark web", "Encrypted online content not indexed by traditional search engines."},
    {"password", "A password is a secret string of characters (letters, numbers, and symbols) used to verify" +
                " a user's identity and grant" +
                " access to a system, device, or account." },
    {"scam", "A scam is a dishonest scheme or trick used to deceive people, usually to steal money or personal information." },
    {"hacking", "Hacking is the unauthorized access to or control over computer systems, networks, or data, often done to steal information," +
                " cause damage, or disrupt services." },
    {"cybersecurity", "Cybersecurity is the practice of protecting computers, networks, programs, and data from unauthorized access, attacks, or damage." },
    {"worm", "A worm is a type of malicious software (malware) that can copy itself and spread to other computers without needing to attach to a program." },
    {"virus", "A virus is a type of malware (malicious software) that attaches itself to a program or file and spreads when the infected program is run" },
    {"privacy", "Privacy in terms of the internet refers to the right and ability of individuals to control the collection, use," +
                " and sharing of their personal information while using online services. It involves protecting data such as your name, email address," +
                " location, browsing habits, and communications from unauthorized access, surveillance, or misuse by others—whether they are individuals, companies, or governments." }
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
            TypeEffect("Ask me anything related to cybersecurity! I am here to help you practice safe browsing!");
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

                // Handle empty input first
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    TypeEffect("I didn't catch that. Could you please repeat or rephrase your question?");
                    Console.ResetColor();
                    return;
                }

                // Check for definition requests first (modified to catch more variations)
                if (input.StartsWith("what is") || input.StartsWith("what's") || input.StartsWith("define"))
                {
                    if (ProcessDefinitionRequest(input))
                        return;
                }

                // Then check for topic preferences
                if (input.Contains("my favorite topic is"))
                {
                    string topic = input.Replace("my favorite topic is", "").Trim();
                    if (allTips.ContainsKey(topic))
                    {
                        userMemory["favoriteTopic"] = topic;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TypeEffect($"I'll remember you're interested in {topic}! Here's a tip:");
                        ContinueTopic(topic);
                        return;
                    }
                }

                // Then check for greetings
                if (ProcessGreetings(input, userName))
                    return;

                // Then general conversation
                if (ProcessGeneralConversation(input, userName))
                    return;

                // Then detect topic and sentiment
                string sentiment = DetectSentiment(input);
                string detectedTopic = DetectTopic(input);

                if (!string.IsNullOrEmpty(detectedTopic))
                {
                    currentTopic = detectedTopic;
                    RespondWithSentiment(sentiment, detectedTopic);
                    return;
                }

                // Fallback response
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                TypeEffect("I'm not sure I understand. Could you try rephrasing or ask about:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeEffect("- Passwords\n- Phishing\n- Malware\n- Privacy\n- Scams");
                Console.ResetColor();
            
        }



        static bool ProcessDefinitionRequest(string input)
        {
            string processedInput = input.ToLower();
            string[] prefixes = { "what is a ", "what is an ", "what is the ",
                         "what's a ", "what's an ", "what's the ",
                         "define ", "what is ", "what's " };

            foreach (var prefix in prefixes)
            {
                if (processedInput.StartsWith(prefix))
                {
                    processedInput = processedInput.Substring(prefix.Length).Trim();
                    break;
                }
            }

            processedInput = processedInput.TrimEnd('?', ' ');

            if (cybersecurityDefinitions.TryGetValue(processedInput, out string definition))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeEffect($"{processedInput.ToUpper()}: {definition}");

                // Only offer a tip if the topic exists in tips
                if (allTips.TryGetValue(processedInput, out var tips))
                {
                    Console.ForegroundColor = GetTopicColor(processedInput);
                    TypeEffect("\nHere's a tip: " + tips[new Random().Next(tips.Length)]);
                    AskFollowUp($"Would you like another {processedInput} tip?");
                }
                else
                {
                    Console.ResetColor();
                }
                return true;
            }
            return false;
        }

        static string DetectSentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried") || input.Contains("concerned") || input.Contains("anxious"))
                return "worried";
            if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed"))
                return "frustrated";
            if (input.Contains("confused") || input.Contains("unsure") || input.Contains("don't understand"))
                return "confused";
            if (input.Contains("scared") || input.Contains("afraid") || input.Contains("frightened"))
                return "scared";
            if (input.Contains("happy") || input.Contains("excited") || input.Contains("great"))
                return "positive";

            return "neutral";
        }

        static void RespondWithSentiment(string sentiment, string topic)
        {
            var random = new Random();

            if (sentiment != "neutral" && sentiment != "positive")
            {
                string response = sentimentResponses[sentiment][random.Next(sentimentResponses[sentiment].Length)];
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                TypeEffect(response);
            }
            else if (sentiment == "positive")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeEffect(positiveResponses[random.Next(positiveResponses.Length)]);
            }

            // Continue with the topic response
            ContinueTopic(topic);
        }

        static string DetectTopic(string input)
        {
            input = input.ToLower();

            // Create a dictionary mapping keywords to topics
            var topicKeywords = new Dictionary<string, string>
            {
                {"password", "password"},
                {"scam", "scam"},
                {"privacy", "privacy"},
                {"phishing", "phishing"},
                {"malware", "malware"},
                {"ransomware", "ransomware"},
                {"firewall", "firewall"},
                {"encryption", "encryption"},
                {"2fa", "2fa"},
                {"ddos", "ddos"},
                {"vpn", "vpn"},
                {"zero-day", "zero-day"},
                {"social engineering", "social engineering"},
                {"brute force", "brute force"},
                {"man-in-the-middle", "man-in-the-middle"},
                {"iot", "iot"},
                {"dark web", "dark web"},
                {"virus", "virus"},
                {"worm", "worm"},
                {"hacking", "hacking"},
                {"cybersecurity", "cybersecurity"},
                {"pharming", "pharming"}
            };

            foreach (var pair in topicKeywords)
            {
                if (input.Contains(pair.Key))
                {
                    return pair.Value;
                }
            }

            return null;
        }


       
        static bool ProcessGreetings(string input, string userName)
        {
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
        }

        static bool ProcessGeneralConversation(string input, string userName)
        {
            if (input.Contains("thank") || input.Contains("thanks"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeEffect($"You're welcome, {userName}! Stay safe online!");
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
            if (allTips.TryGetValue(topic.ToLower(), out var tips))
            {
                string response = tips[new Random().Next(tips.Length)];
                Console.ForegroundColor = GetTopicColor(topic);
                TypeEffect(response);

                // Only ask follow-up if we didn't already ask in definition response
                if (!currentTopic.Equals(topic, StringComparison.OrdinalIgnoreCase))
                {
                    AskFollowUp($"Would you like another {topic} tip?");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                TypeEffect("I'm not sure about that topic. What else would you like to know?");
                Console.ResetColor();
            }
        }


        static ConsoleColor GetTopicColor(string topic)
        {
            string lowerTopic = topic.ToLower();
            switch (lowerTopic)
            {
                case "password":
                    return ConsoleColor.Magenta;
                case "scam":
                    return ConsoleColor.Yellow;
                case "privacy":
                    return ConsoleColor.Blue;
                case "phishing":
                    return ConsoleColor.Green;
                case "pharming":
                    return ConsoleColor.DarkYellow;
                case "malware":
                    return ConsoleColor.Red;
                case "ransomware":
                    return ConsoleColor.DarkRed;
                case "firewall":
                    return ConsoleColor.DarkCyan;
                case "encryption":
                    return ConsoleColor.Cyan;
                case "2fa":
                    return ConsoleColor.Green;
                case "ddos":
                    return ConsoleColor.DarkMagenta;
                case "vpn":
                    return ConsoleColor.DarkBlue;
                case "zero-day":
                    return ConsoleColor.Red;
                case "social engineering":
                    return ConsoleColor.Yellow;
                case "brute force":
                    return ConsoleColor.DarkRed;
                case "man-in-the-middle":
                    return ConsoleColor.DarkYellow;
                case "iot":
                    return ConsoleColor.DarkGreen;
                case "dark web":
                    return ConsoleColor.DarkGray;
                case "virus":
                    return ConsoleColor.Red;
                case "worm":
                    return ConsoleColor.DarkRed;
                case "hacking":
                    return ConsoleColor.DarkMagenta;
                case "cybersecurity":
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.White;
            }
        }



        static void AskFollowUp(string question)
        {
            Console.ForegroundColor = ConsoleColor.White;
            TypeEffect(question + " (yes/no)");

            while (true)
            {
                Console.Write("\n> ");
                string response = Console.ReadLine()?.Trim().ToLower();

                if (response == "yes")
                {
                    ContinueTopic(currentTopic);
                    break;
                }
                else if (response == "no")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeEffect("Okay! What else would you like to know about?");
                    currentTopic = "";
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    TypeEffect("Please answer with 'yes' or 'no'.");
                }
            }
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
        



       
    }
}



