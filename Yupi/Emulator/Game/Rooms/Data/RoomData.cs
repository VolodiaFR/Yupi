using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Rooms.Chat;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Rooms.Data
{
    /// <summary>
    ///     Class RoomData.
    /// </summary>
    internal class RoomData
    {
        /// <summary>
        ///     The _model
        /// </summary>
        private RoomModel _model;

        /// <summary>
        ///     The allow pets
        /// </summary>
        internal bool AllowPets, AllowPetsEating, AllowWalkThrough, HideWall;

        /// <summary>
        ///     The allow rights override
        /// </summary>
        internal bool AllowRightsOverride;

        /// <summary>
        ///     The category
        /// </summary>
        internal int Category;

        /// <summary>
        ///     The cc ts
        /// </summary>
        internal string CcTs;

        /// <summary>
        ///     The chat balloon
        /// </summary>
        internal uint ChatBalloon, ChatSpeed, ChatMaxDistance, ChatFloodProtection;

        /// <summary>
        ///     The chat type
        /// </summary>
        internal int ChatType;

        internal int CompetitionStatus, CompetitionVotes;

        /// <summary>
        ///     The description
        /// </summary>
        internal string Description;

        internal bool DisablePull = false;

        internal bool DisablePush = false;

        /// <summary>
        ///     The event
        /// </summary>
        internal RoomEvent Event;

        /// <summary>
        ///     The floor
        /// </summary>
        internal string Floor;

        /// <summary>
        ///     The floor thickness
        /// </summary>
        internal int FloorThickness;

        /// <summary>
        ///     The game identifier
        /// </summary>
        internal int GameId;

        /// <summary>
        ///     The group
        /// </summary>
        internal Group Group;

        /// <summary>
        ///     The group identifier
        /// </summary>
        internal uint GroupId;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal uint Id;

        /// <summary>
        ///     The land scape
        /// </summary>
        internal string LandScape;

        public DateTime LastUsed = DateTime.Now;

        /// <summary>
        ///     The model name
        /// </summary>
        internal string ModelName;

        /// <summary>
        ///     The name
        /// </summary>
        internal string Name;

        /// <summary>
        ///     The owner
        /// </summary>
        internal string Owner;

        /// <summary>
        ///     The owner identifier
        /// </summary>
        internal uint OwnerId;

        /// <summary>
        ///     The pass word
        /// </summary>
        internal string PassWord;

        /// <summary>
        ///     The room chat
        /// </summary>
        internal ConcurrentStack<Chatlog> RoomChat;

        /// <summary>
        ///     The score
        /// </summary>
        internal int Score;

        /// <summary>
        ///     The state
        /// </summary>
        internal int State;

        /// <summary>
        ///     The tags
        /// </summary>
        internal List<string> Tags;

        /// <summary>
        ///     The trade state
        /// </summary>
        internal int TradeState;

        /// <summary>
        ///     The type
        /// </summary>
        internal string Type;

        /// <summary>
        ///     The users now
        /// </summary>
        internal uint UsersNow, UsersMax;

        /// <summary>
        ///     The wall height
        /// </summary>
        internal int WallHeight;

        /// <summary>
        ///     The wall paper
        /// </summary>
        internal string WallPaper;

        /// <summary>
        ///     The wall thickness
        /// </summary>
        internal int WallThickness;

        /// <summary>
        ///     The who can ban
        /// </summary>
        internal int WhoCanBan;

        /// <summary>
        ///     The who can kick
        /// </summary>
        internal int WhoCanKick;

        /// <summary>
        ///     The who can mute
        /// </summary>
        internal int WhoCanMute;

        /// <summary>
        ///     The word filter
        /// </summary>
        internal List<string> WordFilter;

        /// <summary>
        ///     Gets the tag count.
        /// </summary>
        /// <value>The tag count.</value>
        internal int TagCount => Tags.Count;

        /// <summary>
        ///     Gets a value indicating whether this instance has event.
        /// </summary>
        /// <value><c>true</c> if this instance has event; otherwise, <c>false</c>.</value>
        internal bool HasEvent => false;

        /// <summary>
        ///     Gets the model.
        /// </summary>
        /// <value>The model.</value>
        internal RoomModel Model => _model ?? (_model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id));

        /// <summary>
        ///     Resets the model.
        /// </summary>
        internal void ResetModel()
        {
            _model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id);
        }

        /// <summary>
        ///     Fills the null.
        /// </summary>
        /// <param name="id">The identifier.</param>
        internal void FillNull(uint id)
        {
            Id = id;
            Name = "Unknown Room";
            Description = "-";
            Type = "private";
            Owner = "-";
            Category = 0;
            UsersNow = 0;
            UsersMax = 0;
            ModelName = "NO_MODEL";
            CcTs = string.Empty;
            Score = 0;
            Tags = new List<string>();
            AllowPets = true;
            AllowPetsEating = false;
            AllowWalkThrough = true;
            HideWall = false;
            PassWord = string.Empty;
            WallPaper = "0.0";
            Floor = "0.0";
            LandScape = "0.0";
            WallThickness = 0;
            FloorThickness = 0;
            Group = null;
            AllowRightsOverride = false;
            Event = null;
            GameId = 0;
            WhoCanBan = 0;
            WhoCanKick = 0;
            WhoCanMute = 0;
            TradeState = 2;
            State = 0;
            RoomChat = new ConcurrentStack<Chatlog>();
            WordFilter = new List<string>();
            WallHeight = -1;
            _model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id);
            CompetitionStatus = 0;
        }

        /// <summary>
        ///     Fills the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
        internal void Fill(DataRow row)
        {
            try
            {
                Id = (uint) row["id"];
                Name = (string) row["caption"];
                PassWord = (string) row["password"];
                Description = (string) row["description"];
                Type = (string) row["roomtype"];
                Owner = string.Empty;
                OwnerId = (uint) row["owner"];
                RoomChat = new ConcurrentStack<Chatlog>();
                WordFilter = new List<string>();

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("SELECT username FROM users WHERE id = @userId");
                    queryReactor.AddParameter("userId", OwnerId);

                    Owner = queryReactor.GetString();

                    queryReactor.SetQuery($"SELECT user_id, message, timestamp FROM users_chatlogs WHERE room_id = '{Id}' ORDER BY timestamp ASC LIMIT 150");

                    DataTable table = queryReactor.GetTable();

                    if (table != null)
                    {
                        foreach (DataRow dataRow in table.Rows)
                            RoomChat.Push(new Chatlog((uint)dataRow[0], (string)dataRow[1], Yupi.UnixToDateTime(int.Parse(dataRow[2].ToString())), false));

                    }

                    queryReactor.SetQuery($"SELECT word FROM rooms_wordfilter WHERE room_id = '{Id}'");

                    DataTable tableFilter = queryReactor.GetTable();

                    if (tableFilter != null)
                    {
                        foreach (DataRow dataRow in tableFilter.Rows)
                            WordFilter.Add(dataRow["word"].ToString());
                    }
                }

                string roomState = row["state"].ToString().ToLower();

                switch (roomState)
                {
                    case "locked":
                        State = 1;
                        break;
                    case "password":
                        State = 2;
                        break;
                    default:
                        State = 0;
                        break;
                }

                ModelName = (string) row["model_name"];
                WallPaper = (string) row["wallpaper"];
                Floor = (string) row["floor"];
                LandScape = (string) row["landscape"];
                CcTs = (string) row["public_ccts"];

                int.TryParse(row["trade_state"].ToString(), out TradeState);
                int.TryParse(row["category"].ToString(), out Category);
                int.TryParse(row["walls_height"].ToString(), out WallHeight);
                int.TryParse(row["score"].ToString(), out Score);
                int.TryParse(row["floorthick"].ToString(), out FloorThickness);
                int.TryParse(row["wallthick"].ToString(), out WallThickness);
                int.TryParse(row["chat_type"].ToString(), out ChatType);
                int.TryParse(row["game_id"].ToString(), out GameId);
                int.TryParse(row["mute_settings"].ToString(), out WhoCanMute);
                int.TryParse(row["kick_settings"].ToString(), out WhoCanKick);
                int.TryParse(row["ban_settings"].ToString(), out WhoCanBan);

                uint.TryParse(row["users_now"].ToString(), out UsersNow);
                uint.TryParse(row["users_max"].ToString(), out UsersMax);
                uint.TryParse(row["group_id"].ToString(), out GroupId);
                uint.TryParse(row["chat_balloon"].ToString(), out ChatBalloon);
                uint.TryParse(row["chat_speed"].ToString(), out ChatSpeed);
                uint.TryParse(row["chat_max_distance"].ToString(), out ChatMaxDistance);
                uint.TryParse(row["chat_flood_protection"].ToString(), out ChatFloodProtection);

                AllowPets = Yupi.EnumToBool(row["allow_pets"].ToString());
                AllowPetsEating = Yupi.EnumToBool(row["allow_pets_eat"].ToString());
                AllowWalkThrough = Yupi.EnumToBool(row["allow_walkthrough"].ToString());
                HideWall = Yupi.EnumToBool(row["hidewall"].ToString());

                AllowRightsOverride = false;

                Group = Yupi.GetGame().GetGroupManager().GetGroup(GroupId);
                Event = Yupi.GetGame().GetRoomEvents().GetEvent(Id);
                _model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id);
                CompetitionStatus = 0;

                Tags = new List<string>();

                if (row.IsNull("tags") || string.IsNullOrEmpty(row["tags"].ToString()))
                    return;

                foreach (string item in row["tags"].ToString().Split(','))
                    Tags.Add(item);
            }
            catch (Exception ex)
            {
                YupiLogManager.LogException(ex, "Registered Room Serialization Exception.", "Yupi.Room");
            }
        }

        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="showEvents">if set to <c>true</c> [show events].</param>
        /// <param name="enterRoom"></param>
        internal void Serialize(SimpleServerMessageBuffer messageBuffer, bool showEvents = false, bool enterRoom = false)
        {
            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendString(Name);
            messageBuffer.AppendInteger(OwnerId);
            messageBuffer.AppendString(Owner);
            messageBuffer.AppendInteger(State);
            messageBuffer.AppendInteger(UsersNow);
            messageBuffer.AppendInteger(UsersMax);
            messageBuffer.AppendString(Description);
            messageBuffer.AppendInteger(TradeState);
            messageBuffer.AppendInteger(Score);
            messageBuffer.AppendInteger(0); // Ranking
            messageBuffer.AppendInteger(Category > 0 ? Category : 0);
            messageBuffer.AppendInteger(TagCount);

            foreach (string current in Tags.Where(current => current != null))
                messageBuffer.AppendString(current);

            string imageData = null;

            int enumType = enterRoom ? 32 : 0;

            PublicItem publicItem = Yupi.GetGame()?.GetNavigator()?.GetPublicItem(Id);

            if (!string.IsNullOrEmpty(publicItem?.Image))
            {
                imageData = publicItem.Image;
                enumType += 1;
            }


            if (Group != null)
                enumType += 2;

            if (showEvents && Event != null)
                enumType += 4;

            if (Type == "private")
                enumType += 8;

            if (AllowPets)
                enumType += 16;

            messageBuffer.AppendInteger(enumType);

            if (imageData != null)
                messageBuffer.AppendString(imageData);

            if (Group != null)
            {
                messageBuffer.AppendInteger(Group.Id);
                messageBuffer.AppendString(Group.Name);
                messageBuffer.AppendString(Group.Badge);
            }

            if (showEvents && Event != null)
            {
                messageBuffer.AppendString(Event.Name);
                messageBuffer.AppendString(Event.Description);
                messageBuffer.AppendInteger((int) Math.Floor((Event.Time - Yupi.GetUnixTimeStamp())/60.0));
            }
        }

        /// <summary>
        ///     Serializes the room data.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="session">The session.</param>
        /// <param name="isNotReload">if set to <c>true</c> [from view].</param>
        /// <param name="sendRoom">if set to <c>true</c> [send room].</param>
        /// <param name="show">if set to <c>true</c> [show].</param>
        internal void SerializeRoomData(SimpleServerMessageBuffer messageBuffer, GameClient session, bool isNotReload, bool? sendRoom = false, bool show = true)
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            messageBuffer.Init(PacketLibraryManager.OutgoingRequest("RoomDataMessageComposer"));
            messageBuffer.AppendBool(show); //flatId
            Serialize(messageBuffer, true, !isNotReload);
            messageBuffer.AppendBool(isNotReload);
            messageBuffer.AppendBool(Yupi.GetGame().GetNavigator() != null && Yupi.GetGame().GetNavigator().GetPublicItem(Id) != null); // staffPick
            messageBuffer.AppendBool(!isNotReload || session.GetHabbo().HasFuse("fuse_mod")); // bypass bell, pass ...
            messageBuffer.AppendBool(room != null && room.RoomMuted); //roomMuted
            messageBuffer.AppendInteger(WhoCanMute);
            messageBuffer.AppendInteger(WhoCanKick);
            messageBuffer.AppendInteger(WhoCanBan);
            messageBuffer.AppendBool(room != null && room.CheckRights(session, true));
            messageBuffer.AppendInteger(ChatType);
            messageBuffer.AppendInteger(ChatBalloon);
            messageBuffer.AppendInteger(ChatSpeed);
            messageBuffer.AppendInteger(ChatMaxDistance);
            messageBuffer.AppendInteger(ChatFloodProtection);
            if (sendRoom == null) return;

            if (sendRoom.Value)
            {
                if (Yupi.GetGame().GetRoomManager().GetRoom(Id) != null)
                    Yupi.GetGame().GetRoomManager().GetRoom(Id).SendMessage(messageBuffer);
            }
            else session.SendMessage(messageBuffer);
        }
    }
}