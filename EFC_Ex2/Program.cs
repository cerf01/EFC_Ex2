using EFC_Ex2.DAL;
using EFC_Ex2.DAL.Migrations;
using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFC_Ex2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new Service();
            string q = "";
            do
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Commands list:\n -show\n -add\n -upd\n -find\n -exit\n -maxWins\n -maxDef\n -maxDraw\n -maxGoals\n -maxMiss");
                q = Console.ReadLine();
                switch (q.ToLower())
                {
                    case "1":
                    case "-show":
                        {
                            service.ShowData();
                        }
                        break;
                    case "2":
                    case "-add":
                        {
                            service.AddData();

                        }
                        break;
                    case "3":
                    case "-upd":
                        {
                            service.UpdateData();
                            Console.WriteLine("Item updated!");
                        }
                        break;
                    case "4":
                    case "-del":
                        {
                            service.DeleteData();
                        }
                        break;
                    case "5":
                    case "-find":
                        {
                            service.FindInfo(0);
                        }
                        break;
                    case "6":
                    case "-maxWins":
                        {
                            service.ShowMaxThings(0);
                        }
                        break;
                    case "7":
                    case "-maxDef":
                        {
                            service.ShowMaxThings(1);
                        }
                        break;
                    case "8":
                    case "-maxDraw":
                        {
                            service.ShowMaxThings(2);
                        }
                        break;
                    case "9":
                    case "-maxGoals":
                        {
                            service.ShowMaxThings(3);
                        }
                        break;
                    case "10":
                    case "-maxMiss":
                        {
                            service.ShowMaxThings(4);
                        }
                        break;
                    default:
                        Console.WriteLine("wrong input!");
                        break;
                    case "q":
                    case "-exit":
                        {
                            Console.WriteLine("goodbye!");
                            q = "endoftime";
                        }
                        break;
                }
            } while (q != "endoftime");
            Console.ReadKey();
        }
    }
}