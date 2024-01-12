using libdebug;
using System;

namespace Sender
{
    class Program
    {
        static PS4DBG ps4;

        static void Main(string[] args)
        {
            try
            {
                ps4 = new PS4DBG("192.168.1.100"); //Use your own PS4/PS5 ip
                ps4.Connect();

                ProcessList pl = ps4.GetProcessList();


                Process p = pl.FindProcess("eboot.bin");

                if (p == null)
                {
                    Console.WriteLine("[x] process not found");
                    return;
                }

                ulong stub = ps4.InstallRPC(p.pid);

                string file = "ps4inject.elf";

                ps4.LoadElf(p.pid, file);

                Console.WriteLine("[+] elf loaded!");

                ps4.Disconnect();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }

    }
}