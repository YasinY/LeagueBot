﻿using System;
using System.IO;
using System.Reflection;
using System.Threading;
using LeagueBot.Exceptions;
using LeagueBot.Window.Impl;

namespace LeagueBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("    __                                                 ____           __ \n" +
                              "   / /   ___   ____ _   ____ _  __  __  ___           / __ )  ____   / /_\n" +
                              "  / /   / _ \\ / __ `/  / __ `/ / / / / / _ \\         / __  | / __ \\ / __/\n" +
                              " / /___/  __// /_/ /  / /_/ / / /_/ / /  __/        / /_/ / / /_/ // /_  \n" +
                              "/_____/\\___/ \\__,_/   \\__, /  \\__,_/  \\___/        /_____/  \\____/ \\__/  \n" +
                              "                     /____/                                              ");
            var ingameClient = new IngameClient();
            
            var timer = new Timer(
                delegate
                {
                    Console.Write("Tick!");
                    ingameClient.Capture();
                    ingameClient.SaveBitMap();
                },
                null,
                1000,
                1000);


            Console.ReadLine();
        }
    }
}