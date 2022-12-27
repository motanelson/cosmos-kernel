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

namespace CosmosKernel6
{

    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs;
        void helps()
        {
            Console.WriteLine("cls clear screen");
            Console.WriteLine("echo ");
            Console.WriteLine("exit shutdown");
            Console.WriteLine("help");
            Console.WriteLine("dir list file sys");
            Console.WriteLine("type file");
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
                        if (s.Length > 1) Console.WriteLine(s[1]);
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
                    case ("help"):
                        helps();
                        break;
                    default:
                        Console.WriteLine("invalid command");
                        break;
                }
            }
                

            
        }
        void commands()
        {
            
            Console.Write("0:\\>: ");
            var input = Console.ReadLine();
            String[] s=input.Split(" ");
            shells(s);
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
