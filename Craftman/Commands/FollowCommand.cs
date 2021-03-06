using System.Collections.Generic;

namespace Craftman.Commands
{
    public class FollowCommand : ICommand
    {
        public FollowCommand(string userName, string followed)
        {
            UserName = userName;
            Followed = followed;
        }

        public string UserName { get; }
        public string Followed { get; }

        public void ExecuteUsing(List<Message> messages, Dictionary<string, List<string>> userNameToFollowed)
        {
            if (userNameToFollowed.ContainsKey(UserName))
                userNameToFollowed[UserName].Add(Followed);
            else
                userNameToFollowed.Add(UserName, new List<string> {Followed});
        }
    }
}