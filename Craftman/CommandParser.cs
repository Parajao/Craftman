﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Craftman.Commands;

namespace Craftman
{
    public class CommandParser
    {
        private const string QuitPattern = @"[Q|q]uit";
        private static readonly string PostMessagePattern = $"(?<userName>\\b(?!{QuitPattern}\\b)\\w*)\\s->\\s(?<message>.*)";
        private static readonly string FollowsPattern = $"(?<userName>\\b(?!{QuitPattern}\\b)\\w*)\\sfollows\\s(?<followed>.*)";
        private static readonly string ReadPattern = $"(?<userName>\\b^(?!{QuitPattern}\\b)\\w+$)";
        private static readonly string WallPattern = $"(?<userName>\\b(?!{QuitPattern}\\b)\\w*)\\swall$";

        public ICommand Parse(string input)
        {
            var matchToCommand = _patternToCommand
                .Select(kvp =>
                {
                    var pattern = kvp.Key;
                    var commandFactory = kvp.Value;
                    var regex = new Regex(pattern);
                    var match = regex.Match(input);
                    return new KeyValuePair<Match, Func<Match, ICommand>>(
                        match,commandFactory);
                });

            var successfulMatchesToCommand = matchToCommand
                .Where(kvp =>
                {
                    var match = kvp.Key;
                    return match.Success;
                })
                .DefaultIfEmpty(new KeyValuePair<Match, Func<Match, ICommand>>
                    (null, match => new VoidCommand()));

            var command = successfulMatchesToCommand
                            .Select(kvp =>
                            {
                                var match = kvp.Key;
                                var factory = kvp.Value;
                                return factory(match);
                            })
                            .Single();

            return command;
        }

        private readonly Dictionary<string, Func<Match, ICommand>> 
            _patternToCommand =
            new Dictionary<string, Func<Match, ICommand>>
			{
                {QuitPattern,CommandFactory.CreateQuitCommand},
				{PostMessagePattern, CommandFactory.CreatePostCommand},
				{FollowsPattern, CommandFactory.CreateFollowCommnad},
				{ReadPattern, CommandFactory.CreateReadCommand},
				{WallPattern, CommandFactory.CreateWallCommand}
			};
        
    }
}