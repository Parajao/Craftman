﻿using Craftman.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace Craftman.Tests.Unit
{
    [TestFixture]
    public class DescribeCommandParser
    {
        private readonly CommandParser _parser = new CommandParser();

        [Test]
        public void ItShouldCreateAnVoidCommand_GivenNoCommand()
        {
            //g
            const string input = "";

            //w
            var actual = _parser.Parse(input);

            //t
            actual.Should().BeAssignableTo<VoidCommand>();
        }

        [Test]
        public void ItShouldCreateAPostCommand_GivenUserAndMessage()
        {
            //g
            const string input = "Alice -> I love the weather";

            //w
            var actual = _parser.Parse(input);
            
            //t
            actual.Should().BeAssignableTo<PostCommand>();
            actual.UserName.Should().Be("Alice");
            actual.Message.Should().Be("I love the weather");
        }
    }
}
