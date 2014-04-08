using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiScheme.Scheme
{
    public class CharacterInfo
    {
        public string userId;
        public string name;
    }

    public class GetCharactersIn : In
    {
        public string userId;
    }
    public class GetCharactersOut : Out
    {
        public List<CharacterInfo> characters;
    }

    public class CreateCharacterIn : In
    {
        public string userId;
        public string name;
    }
    public class CreateCharacterOut : Out
    {
    }

    public class AddPlayLogIn : In
    {
        public string roomName;
        public string fileName;
    }
    public class AddPlayLogOut : Out
    {

    }

    public class PlayLogInfo
    {
        public int id;
        public DateTime created;
        public string roomName;
        public string fileName;
    }
    public class GetPlayLogsIn : In
    {
        /// <summary>
        /// Zero based page to get.
        /// </summary>
        public int page;
    }
    public class GetPlayLogsOut : Out
    {
        public List<PlayLogInfo> playLogs;
    }

    public class GetPlayLogByIdIn : In
    {
        public int id;
    }
    public class GetPlayLogByIdOut : Out
    {
        public PlayLogInfo playLog;
    }
}
