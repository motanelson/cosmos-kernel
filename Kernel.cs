using Cosmos.HAL.BlockDevice.Registers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Text;
using Sys = Cosmos.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CosmosKernel6
{

    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs;
        String[] bats = null;
        Boolean onBat = false;
        void helps()
        {
            Console.WriteLine("cls clear screen");
            Console.WriteLine("echo ");
            Console.WriteLine("exit shutdown");
            Console.WriteLine("help");
            Console.WriteLine("dir list file sys");
            Console.WriteLine("type file");
            Console.WriteLine("reboot restart");
            Console.WriteLine("help");
            Console.WriteLine("copy file1 newfile");
            Console.WriteLine("edit file1 edit line");
            Console.WriteLine("command my.bat");


        }
        void dir()
        {
            var fs_type = fs.GetFileSystemType(@"0:\");
            Console.WriteLine("File System Type: " + fs_type);
            var directory_list = Directory.GetFiles(@"0:\");
            foreach (var file in directory_list)
            {
                Console.WriteLine(file);
            }
            var available_space = fs.GetAvailableFreeSpace(@"0:\");
            Console.WriteLine("Available Free Space: " + available_space);
        }
        void shells(String[] s)
        {
            string ss="";
            if (s.Length > 0)
            {
                ss = s[0].ToLower().Trim();
                switch (ss)
                {
                    case ("dir"):
                        dir();
                    break;
                    case ("cls"):
                        Console.Clear();
                        break;
                    case ("echo"):

                        if (s.Count() > 1)
                        {
                            int n = 0;
                            String jjj = "";
                            
                            foreach (String d in s)
                            {
                                
                                String o = d;
                                if(n!=0)jjj = jjj + o + " ";
                                n++;
                            }
                            Console.WriteLine(jjj);
                        }
                        break;
                    case ("exit"):
                    Cosmos.System.Power.Shutdown();
                        break;
                    case ("reboot"):
                        Cosmos.System.Power.Reboot();
                        break;
                    case ("type"):
                        if (s.Length > 1)
                        {
                            try
                            {
                                Console.WriteLine(File.ReadAllText(@"0:\" + s[1]));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }
                        break;
                    case ("copy"):
                        if (s.Length > 2)
                        {
                            try
                            {
                                String sss = "";
                                sss=File.ReadAllText(@"0:\" + s[1]);
                                var file_stream = File.Create(@"0:\" + s[2]);
                                File.WriteAllText(@"0:\" + s[2], sss);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }

                        }
                        break;
                    case ("edit"):
                        if (s.Length > 1)
                        {
                            try
                            {
                                Console.WriteLine("enter a empty line to exit");
                                String sss = "";
                                String ssss = ".";
                                while (ssss != "")
                                {
                                    var input = Console.ReadLine();
                                    sss = sss+input + "\r\n";
                                    ssss = input;
                                }
                               
                                
                                var file_stream = File.Create(@"0:\" + s[1]);
                                File.WriteAllText(@"0:\" + s[1], sss);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }

                        }
                        break;
                    case ("color"):
                        if (s.Length > 1)
                        {
                            try
                            {
                                
                                int i=0;
                                i = int.Parse(s[1]);
                                i=i & 7;
                                if (i < 1 || i > 7) Console.ForegroundColor = ConsoleColor.Black;
                                if (i==1) Console.ForegroundColor = ConsoleColor.Blue;
                                if (i == 2) Console.ForegroundColor = ConsoleColor.Green;
                                if (i == 3) Console.ForegroundColor = ConsoleColor.Cyan;
                                if (i == 4) Console.ForegroundColor = ConsoleColor.Red;
                                if (i == 5) Console.ForegroundColor = ConsoleColor.Magenta;
                                if (i == 6) Console.ForegroundColor = ConsoleColor.Yellow;
                                if (i == 7) Console.ForegroundColor = ConsoleColor.White;
                               
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        break;
                    case ("help"):
                        helps();
                        break;
                    case ("command"):
                        if (s.Length > 1)
                        {
                            try
                            {
                                String ssss = "";
                                ssss=File.ReadAllText(@"0:\" + s[1]);
                                bats = ssss.Split("\r\n");
                                onBat = true;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }

                        }
                        break;
                    default:
                        Console.WriteLine("invalid command");
                        break;
                }
            }
                

            
        }
        void shelll(String[] s)
        {
            if (s[0].Trim()!="")shells(s);
        }
        void commands()
        {
            
            
            if (onBat)
            {
                int n = 0;
                foreach (String d in bats)
                {
                    
                    String[] s = d.Split(" ");
                    shelll(s);
                }
                onBat = false;
            }
            else
            {
                Console.Write("0:\\>: ");
                var input = Console.ReadLine();
                String[] s = input.Split(" ");
                shelll(s);
            }
        }
        protected override void BeforeRun()
        {

            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine("command line sample.");
            helps();
        }

        protected override void Run()
        {


            commands();




        }
    }
}
