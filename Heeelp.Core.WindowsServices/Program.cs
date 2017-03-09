// ==============================================================================================================
// Microsoft patterns & practices
// CQRS Journey project
// ==============================================================================================================
// ©2012 Microsoft. All rights reserved. Certain content used with permission from contributors
// http://go.microsoft.com/fwlink/p/?LinkID=258575
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.
// ==============================================================================================================

namespace WorkerRoleCommandProcessor
{
    using System;
    using System.ServiceProcess;
    class Program
    {
        static void Main(string[] args)
        {
            // Cleanup default EF DB initializers.
            DatabaseSetup.Initialize();


            if (System.Diagnostics.Debugger.IsAttached)
            {
#if DEBUG   // debugando como DEBUG
                    CoreProcessor service = new CoreProcessor(false);
                    service.StartDebug(new string[2]);
                    System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else   // debugando como Release 
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new CoreProcessor(false) };
                ServiceBase.Run(ServicesToRun);
#endif
            }
            else   // codigo original
            {
                //ServiceBase[] ServicesToRun;
                //ServicesToRun = new ServiceBase[] { new CoreProcessor(false) };
                //ServiceBase.Run(ServicesToRun);
                CoreProcessor service = new CoreProcessor(false);
                service.StartDebug(new string[2]);
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            }



            //using (var processor = new CoreProcessor(false))
            //{
            //    processor.Start();

            //    Console.WriteLine("Host started");
            //    Console.WriteLine("Press enter to finish");
            //    Console.ReadLine();

            //    processor.Stop();
            //}
        }
    }
}
