using Cosmos.Core;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Sys = Cosmos.System;

namespace CosmosKernel3
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs;
        protected override void BeforeRun()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("hello world....\n.");
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
           var s = VFSManager.GetDisks();




            
           Console.WriteLine("number of logical disks:"+s.Count.ToString());

            

        }

        protected override void Run()
        {

            Console.WriteLine("echo text\ncls\nexit\n");   
            while (1 == 1)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                string[] inputs = null;
                var input2 = input.Split(" ");
                var input3 = input2[0].Trim().ToLower();
                if (input2.Count() > 1)
                {

                    if (input3 == "echo")
                    {
                        var echo = input.Replace(input3, "");
                        Console.WriteLine(echo);
                    }
                }
                else
                {

                }
                if (input3 == "cls" || input3 == "clear")
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                if (input3 == "dir")
                {
                    try
                    {
                        Console.WriteLine(System.IO.Directory.GetCurrentDirectory().ToString());
                        inputs = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory().ToString());

                        var n = 0;
                        if (n > 0)
                        {
                            for (n = 0; n < inputs.Count(); n++)
                            {
                                Console.WriteLine(inputs[n]);
                            }
                        }
                    }
                    catch
                    {

                    }

                }
                if (input3 == "exit") Cosmos.System.Power.Shutdown();

            }
        }
    }
}
