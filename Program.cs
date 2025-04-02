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
                    player.PlaySync(); // Waits for the file to finish playing
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

            // start chat
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




        // Bot's pre-saved responses
        static void ProcessUserQuery(string userName, string input)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (ContainsKeyword(input, "cybersecurity"))
            {
                TypeEffect("Cybersecurity involves the practices, technologies, and processes designed " +
                    "to protect computers, networks, programs, and data from digital attacks, theft, " +
                    "damage, or unauthorized access. These measures are vital for securing sensitive " +
                    "information, ensuring privacy, and maintaining the integrity of systems in the " +
                    "face of increasing threats from cybercriminals and hackers.");
            }
            else if (ContainsKeyword(input, "password"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeEffect("A passwword is a sequence of letter, numbers and symbols used to confirm identity " +
                    " or grant access. A strong password is essential for security! Use a mix of Uppercase letters (A-Z), " +
                    "lowercase letters(a - z), numbers(0 - 9),symbols(!, @, #, etc.) Consider having 12 + characters" +
                    "for your passwords. Use unique passwords for each account\r\n");
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
            else if (ContainsKeyword(input, "worm"))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                TypeEffect("A computer worm is a type of malware that spreads on its own, usually through networks." +
                    " It can replicate and cause harm without needing to attach to a program or file. Key Features of a" +
                    "worm are: Self - replicating\r\n- Exploits vulnerabilities\r\n- Can cause damage\r\n- May carry " +
                    "other malware" +

                    " You can Remove a Worm by disconnecting from the internet or using an antivirus software");
            }
            else if (ContainsKeyword(input, "ransomware"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeEffect("Ransomware is a serious threat, but proactive measures can reduce the risk:\r\n\r\n" +
                    "- Back up your data\r\n" +
                    "- Keep software up-to-date\r\n" +
                    "- Avoid suspicious links\r\n\r\n" +

                    "What to Do If You're Attacked:\r\n\r\n" +
                    "- Don't pay the ransom\r\n" +
                    "- Use backups and tools to recover files safely");

            }
            else if (ContainsKeyword(input, "firewall"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeEffect("A firewall is like a digital bouncer! It monitors and controls incoming " +
                    "and outgoing network traffic, keeping the bad guys out. A firewall acts as a barrier between" +
                    " trusted networks(like your home network) and untrusted ones(like the internet).It helps" +
                    " prevent unauthorized access, keeping your devices and data safe. A firewall's key functions are:" +
                    "blocking malicious traffic, preventing unauthorized access and protecting yur device and data");
            }
            else if (ContainsKeyword(input, "encryption"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeEffect("Encryption is like sending a secret message! It converts your data into a code," +
                    " so only authorized people can read it. Encryption scrambles your data, making it unreadable " +
                    "to anyone without the decryption key.Even if someone intercepts your data, they won't be" +
                    " able to access it without the key! Benefits of encryption are that it protects sensitive data," +
                    "it prevents unauthorized access and it ensured confidentiality and privacy.");


            }
            else if (ContainsKeyword(input, "social engineering"))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                TypeEffect("Social engineering is when scammers manipulate you into sharing sensitive " +
                    "info by pretending to be someone trustworthy." +

                    "How Does it Work ?" +
                    "Scammers might call, email, or message you, claiming to be from a bank, government agency, or other legit" +
                    " organization.They'll try to trick you into revealing confidential info like passwords, credit card numbers, " +
                    "or personal data." +
                    "Be cautious of unsolicited requests for sensitive data" +
                    "Verify the identity of the person or organization requesting info" +
                    "Never share confidential info via email, text, or phone" +
                    "Keep your personal data private!");
            }
            else if (ContainsKeyword(input, "two-factor authentication"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeEffect("2FA is a security process that requires two pieces of information to " +
                    "verify your identity: 1.Your password" +
                    "2.A second factor, like: a code sent to your phone, a fingerprint scan or face recognition scan" +

                "How Does 2FA Work ? When you try to log in to an account with 2FA enabled:" +

        "1.You enter your password" +
       "2.You receive a code or prompt for the second factor" +
        "3.You enter the code or complete the second factor authentication" +

"The key benefits of 2FA are: adds an extra layer of security; protects your account from unauthorized access;" +
"and it reduces the risk of phishing and password cracking");
            }
            else if (ContainsKeyword(input, "antivirus"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeEffect("Antivirus software is a program that detects, prevents, and removes malware" +
                    " from computers, devices, and networks. It protects against various threats, including " +
                    "" +
                    "viruses, worms, trojans, ransomware, and spyware.\r\n\r\n" +
                    "How Antivirus Software Works:\r\n\r\n" +
                    "- Real-time protection: Scans files and programs for malicious activity and blocks" +
                    " suspicious behavior immediately.\r\n\r\nKey Features to Look for:\r\n\r\n" +
                    "- Real-time protection\r\n" +
                    "- Automatic updates\r\n- Quarantine for suspicious files\r\n" +
                    "- Firewall integration\r\n" +
                    "- Web protection\r\n" +
                    "- Anti-phishing\r\n");
            }
            else if (ContainsKeyword(input, "trojan"))
            {
                TypeEffect("A Trojan is a type of malware that disguises itself as a legitimate program or file, tricking " +
                    "users into downloading and executing it.\r\n\r\nHow Trojans Work:\r\n\r\n" +
                    "- Appear as useful programs, email attachments, or links\r\n" +
                    "- Deceive users into installing them through social engineering\r\n" +
                    "- Carry out malicious activities once activated, such as:\r\n\r\n    " +
                    "• Stealing data\r\n    • Creating backdoors for hackers\r\n   " +
                    "• Damaging systems\r\n\r\n" +
                    "Key Difference:\r\n\r\nUnlike viruses or worms, Trojans don't replicate themselves." +
                    " Instead, they rely on deceiving users to install them.");
            }
            else if (ContainsKeyword(input, "spyware"))
            {
                TypeEffect("Spyware is a type of malware that secretly collects personal info, browsing" +
                    " history, and online activity without your consent.\r\n\r\n" +
                    "How Spyware Works:\r\n\r\n" +
                    "1. Installation: Spyware installs through malicious websites, downloads, email " +
                    "attachments, or infected ads.\r\n2. Data Collection: Spyware tracks:\r\n\r\n   " +
                    "• Keystrokes (passwords, credit card numbers)\r\n    " +
                    "• Browser history\r\n    " +
                    "• Personal files\r\n    " +
                    "• Login credentials\r\n    " +
                    "• System information\r\n\r\nKey Difference:\r\n\r\n" +
                    "Unlike viruses or worms, spyware's main goal is to gather and transmit data without" +
                    " your knowledge or consent.");
            }
            else if (ContainsKeyword(input, "how are you"))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                TypeEffect("I'm doing great! How can I assist you with your cybersecurity questions?");
            }
            else if (ContainsKeyword(input, "purpose"))
            {
                Console.WriteLine("My purpose is to educate and guide you on staying safe online, helping you understand" +
                    " important cybersecurity concepts like phishing, malware, encryption, and more.");
            }
            else if (ContainsKeyword(input, "ask"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeEffect("You can ask me about anything related to cybersecurity, including phishing," +
                    " password safety, firewalls, malware, encryption, social engineering, and more! Ask me how you can stay" +
                    "safe on the inernet");
            }
            else if (ContainsKeyword(input, "pharming"))
            {
                Console.ForegroundColor = ConsoleColor.White;
                TypeEffect("Pharming is a sneaky cyberattack that redirects you from real websites to " +
                    "fake ones, stealing your sensitive data without you even realizing it!\r\n\r\n" +
                    "How Does it Work?\r\n\r\n" +
                    "Pharming manipulates DNS settings or exploits vulnerabilities, all without needing " +
                    "your interaction.\r\n\r\n" +
                    "Stay Safe with These Tips:\r\n\r\n" +
                    "✔ Stick to secure websites (look for HTTPS & the padlock icon)" +
                    "" +
                    "\r\n✔ Enable Multi-Factor Authentication (MFA) for extra security\r\n" +
                    "✔ Keep your software up-to-date\r\n" +
                    "✔ Use trusted DNS services (like Google or Cloudflare)\r\n" +
                    "✔ Install antivirus and anti-malware software\r\n");
            }
            else if (ContainsKeyword(input, "cyber attack"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeEffect("A cyber attack is when hackers try to harm or access your computer, " +
                    "network, or data without permission. This can happen to anyone - individuals, " +
                    "businesses, or governments.\r\n\r\nWhy Do Hackers Launch Cyber Attacks?\r\n\r\n" +
                    "Hackers may attack for various reasons:\r\n\r\n" +
                    "Financial gain (stealing money or sensitive info)\r\n" +
                    "Espionage (spying or stealing secrets)\r\n" +
                    "Sabotage (disrupting or destroying systems)\r\n\r\n");
            }
            else if (ContainsKeyword(input, "virus"))
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                TypeEffect("A computer virus is a type of malware that can harm your device, steal data," +
                    " and slow down performance. Viruses spread through infected emails, downloads, " +
                    "and removable devices.\r\n\r\nPrevention is Key.\r\n\r\nTo keep your device safe:\r\n\r\n" +
                    "✅ Install and update antivirus software\r\n" +
                    "✅ Keep your software up-to-date\r\n" +
                    "✅ Avoid suspicious links and downloads\r\n" +
                    "✅ Enable firewall protection\r\n" +
                    "✅ Use strong passwords and multi-factor authentication\r\n" +
                    "✅ Be cautious with removable media\r\n" +
                    "✅ Backup important data regularly\r\n\r\n" +
                    "Removing a computer virus:\r\n\r\nIf you're infected:\r\n\r\n" +
                    "✅ Run a full system scan with antivirus software\r\n" +
                    "✅ Boot in safe mode\r\n" +
                    "✅ Delete suspicious files or applications\r\n" +
                    "✅ Use system restore\r\n" +
                    "✅ Reinstall your operating system (if necessary)\r\n\r\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                TypeEffect("I don't have the answer for that YET :( but you can ask me something else " +
                    "about cyber security! :)");
            }
            Console.ResetColor();
        }

        // Helper method to check if user input contains any keywords
        static bool ContainsKeyword(string input, string keyword)
        {
            return Regex.IsMatch(input, @"\b" + Regex.Escape(keyword) + @"\b", RegexOptions.IgnoreCase);
        }
        static void TypeEffect(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30); // Simulates typing effect
            }
            Console.WriteLine();
        }

            

    }
        }

