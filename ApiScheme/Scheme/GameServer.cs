using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiScheme.Scheme
{
    public class BannedIdInfo
    {
        public string userId;
    }
    public class GetBlacklistIn : In
    {
        public int page;
    }
    public class GetBlacklistOut : Out
    {
        public List<BannedIdInfo> infos;
    }

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

    public class CharacterItems
    {
        public int WonAsCitizen;
        public int WonAsWerewolf;
        public int WonAsFox;
        public int LostAsCitizen;
        public int LostAsWerewolf;
        public int LostAsFox;

        public CharacterItems AddResult(CharacterItems b)
        {
            b.GetType().GetFields().ToList().ForEach(field =>
            {
                var targetField = this.GetType().GetField(field.Name);

                if (field.FieldType == typeof(int))
                {
                    var aValue = (int)targetField.GetValue(this);
                    var bValue = (int)field.GetValue(b);
                    var resultValue = aValue + bValue;
                    if (resultValue < 0)
                        throw new InvalidOperationException(string.Format("Field must be >= 0. FieldName:{0} Result:{1} + {2} = {3}.", field.Name, aValue, bValue, resultValue));
                    targetField.SetValue(this, resultValue);
                }
            });
            return this;
        }
    }
    public class TransactionInfo
    {
        /// <summary>
        /// An unique, character name.
        /// </summary>
        public string characterName;

        /// <summary>
        /// JSON encoded items like "{foo:2,bar:-1}" means "Add 2 foos and Remove 1 bar.".
        /// Failes if character has no bar.
        /// </summary>
        //public string jsonItems;
        public CharacterItems items;
    }
    /// <summary>
    /// Add or Remove from Characters something like win/lose count, gold, exp, items.
    /// Concurrent, optimistic.
    /// Amounts can be minus. (Fails if character does not have things enough.)
    /// </summary>
    public class TransactionIn : In
    {
        public List<TransactionInfo> infos;
    }
    public class TransactionOut : Out
    {

    }

    public class PlayLogInfo
    {
        public int id;
        public DateTime created;
        public string culture;
        public string timezone;
        public string roomName;
        public string fileName;
    }

    public class AddPlayLogIn : In
    {
        public PlayLogInfo log;
    }
    public class AddPlayLogOut : Out
    {

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

    public class MessageInfo
    {
        public string from;
        public string role;
        public string mode;
        public string body;
        public override string ToString()
        {
            return string.Format("[MessageInfo from:{0} role:{1} mode:{2} body:{3}]", from, role, mode, body);
        }
    }
    public class ReportMessageIn : In
    {
        public string userId;
        public string note;
        public List<MessageInfo> messages;
        public override string ToString()
        {
            var str = string.Format("[ReportMessageIn userId:{0} note:{1} messages:", userId, note);
            messages.ForEach(m => str += m.ToString());
            str += "]";
            return str;
        }
    }
    public class ReportMessageOut : Out
    {

    }
}
