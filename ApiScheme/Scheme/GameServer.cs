using ApiScheme.Utility;
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
        public string ipAddress;
        public string host;
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

    public class GetCharacterItemsIn : In
    {
        public string characterName;
    }
    public class GetCharacterItemsOut : Out
    {
        public CharacterItems items;
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
        public int id;
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

    public class GameServerStatus
    {
        public string host;
        public int port;
        public string name;
        public int players;
        public int maxPlayers;
        public int framesPerInterval;
        public double reportIntervalSeconds;
        public double maxElapsedSeconds;

        public override string ToString()
        {
            return string.Format("[GameServerStatus host:{0} port:{1} name:{2} players:{3} maxPlayers:{4} framesPerInterval:{5} reportIntervalSeconds:{6} maxElapsedSeconds:{7}]",
                host, port, name, players, maxPlayers, framesPerInterval, reportIntervalSeconds, maxElapsedSeconds);
        }
    }
    public class ReportGameServerStatusIn : In
    {
        public GameServerStatus status;
        public override string ToString()
        {
            return string.Format("[ReportGameServerStatusIn status:{0}]", status);
        }
    }
    public class ReportGameServerStatusOut : Out
    {

    }

    public class GetGameServersIn : In
    {

    }
    public class GetGameServersOut : Out
    {
        public List<GameServerStatus> servers;
    }

    public class GetStatisticsIn : In
    {

    }
    public class GetStatisticsOut : Out
    {
        /// <summary>
        /// The number of Users.
        /// </summary>
        public int users;

        /// <summary>
        /// The number of Characters.
        /// </summary>
        public int characters;

        /// <summary>
        /// The number of current playing Users.
        /// </summary>
        public int playings;

        /// <summary>
        /// The number of PlayLogs.
        /// </summary>
        public int playlogs;
    }

    public class JwtSellerData
    {
        public string userId;
        public string sku;
        public double amount = 1;
    }
    public class PurchasableGood
    {
        public string sku;
        public string name;
        public string description;
        public string price;
        
        /// <summary>
        /// JWT, Json Web Token to buy.
        /// </summary>
        public string jwt;

        public static PurchasableGood From(
            string SellerId,
            string SellerSecret,
            string userId,
            string sku,
            double amount,
            string name,
            string description,
            double price,
            double? recurrencePrice,
            string currencyCode,
            string displayPrice)
        {
            return new PurchasableGood()
            {
                sku = sku,
                name = name,
                description = description,
                price = displayPrice,
                jwt = JwtHelper.From(SellerId, SellerSecret, userId, sku, amount, name, description, price, recurrencePrice, currencyCode)
            };
        }
    }
    public class GetPurchasableGoodsIn : In
    {
        /// <summary>
        /// Buyer's UserId.
        /// </summary>
        public string userId;
    }
    public class GetPurchasableGoodsOut : Out
    {
        /// <summary>
        /// Purchasable goods for a specific Player.
        /// </summary>
        public List<PurchasableGood> goods;
    }
}
