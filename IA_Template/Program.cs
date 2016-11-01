﻿using System.Collections.Generic;
using IA;
using Discord;
using System.Threading.Tasks;

namespace IA_Template
{
    class Program
    {
        Bot bot;

        /// <summary>
        /// Application startpoint.
        /// </summary>
        /// <param name="args">This is where your application arguments gets stored.</param>
        static void Main(string[] args)
        {
            new Program().StartBot().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Defines the bot, 
        /// </summary>
        /// <returns></returns>
        async Task StartBot()
        {
            // Create all modules inside IA and sets up alot of backend.
            bot = new Bot(x =>
            {
                x.Name = "starter-bot";
                x.Token = "<Token-here>";
                x.Prefix = ">";
            });

            await LoadModules();

            // Connects to discord.
            await bot.ConnectAsync();

            // Locks the window, otherwise the bot will close.
            await Task.Delay(-1);
        }

        /// <summary>
        /// Loads all commands programmed into this starter bot
        /// </summary>
        /// <returns></returns>
        async Task LoadModules()
        {
            // Create single commands
            bot.Events.AddCommandEvent(command =>
            {
                command.name = "say";

                // processCommand gets called whenever someone calls the command. for example ">help" for this one.
                command.processCommand = async (message, arg) =>
                {
                    //SendMessageSafeAsync is a safer solution made for IA.
                    //bot.Events.ListCommands(message) lists all commands that are installed into your bot.
                    await message.Channel.SendMessageSafeAsync(arg);
                };
            });

            // Create a module with Events inside
            await bot.Events.CreateModule(module =>
            {
                module.name = "general";

                // All commands that will be stored here, same way as AddCommandEvent.
                module.events = new List<IA.Events.CommandEvent>()
                {
                    new IA.Events.CommandEvent(command =>
                    {
                        command.name = "ping";
                        command.processCommand = async (message, arg) =>
                        {
                            await message.Channel.SendMessageSafeAsync("Pong!");
                        };
                    })
                };
            }).InstallAsync(bot);
        }
    }
}