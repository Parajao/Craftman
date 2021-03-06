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
        public void ItShouldCreateAVoidCommand_GivenNoCommand()
        {
            //g
            const string input = "";

            //w
            var actual = _parser.Parse(input);

            //t
            actual.Should().BeAssignableTo<VoidCommand>();
        }

        [Test]
        public void ItShouldCreateAPostCommand_GivenUserNameAndMessage()
        {
            //g
            const string input = "Alice -> I love the weather";

            //w
            var actual = _parser.Parse(input);
            
            //t
            actual.Should().BeAssignableTo<PostCommand>();
            actual.UserName.Should().Be("Alice");
            ((PostCommand)actual).Text.Should().Be("I love the weather");
        }

        [Test]
        public void ItShouldCreateAQuitCommand_GivenQuitUserInput()
        {
            //g
            const string input = "quit";

            //w
            var actual = _parser.Parse(input);

            //t
            actual.Should().BeAssignableTo<QuitCommand>();
        }

        [Test]
        public void ItShouldCreateAReadCommand_GivenUserName()
        {
            //g
            const string input = "Alice";
            
            //w
            var actual = _parser.Parse(input);

            //t
            actual.Should().BeAssignableTo<ReadCommand>();
            actual.UserName.Should().Be("Alice");
        }

        [Test]
        public void ItShouldCreateAFollowCommand_GivenTwoUserNames()
        {
            //g
            const string input = "Charlie follows Alice";

            //w
            var actual = _parser.Parse(input);

            //t
            actual.Should().BeAssignableTo<FollowCommand>();
            actual.UserName.Should().Be("Charlie");
            ((FollowCommand) actual).Followed.Should().Be("Alice");
        }

        [Test]
        public void ItShouldCreateAWallCommand_GivenAUserNames()
        {
            //g
            const string input = "Charlie wall";

            //w
            var actual = _parser.Parse(input);

            //t
            actual.Should().BeAssignableTo<WallCommand>();
            actual.UserName.Should().Be("Charlie");
        }
    }
}
