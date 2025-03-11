namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp.TeamDetails;

    public class TeamDetailsParser : XmlParserBase, IFileParseStrategy
    {
        public async Task<IXmlFileBase> ExecuteFileParseAsync(
            XmlReader xmlReader,
            string fileName,
            decimal version,
            long userId,
            DateTime fetchedDate,
            CancellationToken cancellationToken)
        {
            return new HattrickData(
                fileName,
                version,
                userId,
                fetchedDate,
                await ReadUserNodeAsync(
                    xmlReader),
                await ReadTeamsNodeAsync(
                    xmlReader));
        }

        private static async Task<BotStatus> ReadBotStatusNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var botStatus = new BotStatus(
                (await xmlReader.ReadValueAsync()).AsBool(),
                xmlReader.CheckNode(NodeName.BotSince) ? (await xmlReader.ReadValueAsync()).AsDateTime() : null);

            await xmlReader.ReadAsync();

            return botStatus;
        }

        private static async Task<Cup?> ReadCupNodeAsync(XmlReader xmlReader)
        {
            Cup? cup = null;

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                cup = new Cup(
                    (await xmlReader.ReadValueAsync()).AsBool(),
                    xmlReader.CheckNode(NodeName.CupId) ? (await xmlReader.ReadValueAsync()).AsLong() : null,
                    xmlReader.CheckNode(NodeName.CupName) ? (await xmlReader.ReadValueAsync()).AsString() : null,
                    xmlReader.CheckNode(NodeName.CupLeagueLevel) ? (await xmlReader.ReadValueAsync()).AsInt() : null,
                    xmlReader.CheckNode(NodeName.CupLevel) ? (await xmlReader.ReadValueAsync()).AsInt() : null,
                    xmlReader.CheckNode(NodeName.CupLevelIndex) ? (await xmlReader.ReadValueAsync()).AsInt() : null,
                    xmlReader.CheckNode(NodeName.MatchRound) ? (await xmlReader.ReadValueAsync()).AsInt() : null,
                    xmlReader.CheckNode(NodeName.MatchRoundsLeft) ? (await xmlReader.ReadValueAsync()).AsInt() : null);
            }

            await xmlReader.ReadAsync();

            return cup;
        }

        private static async Task<Fanclub> ReadFanclubNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var fanclub = new Fanclub(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return fanclub;
        }

        private static async Task<Flag[]> ReadFlagListNodeAsync(XmlReader xmlReader)
        {
            var flags = new List<Flag>();

            if (xmlReader.IsEmptyElement)
            {
                return flags.ToArray();
            }

            await xmlReader.ReadAsync();

            while (xmlReader.CheckNode(NodeName.Flag))
            {
                flags.Add(
                    await ReadFlagNodeAsync(
                        xmlReader));
            }

            return flags.ToArray();
        }

        private static async Task<Flag> ReadFlagNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var flag = new Flag(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString());

            await xmlReader.ReadAsync();

            return flag;
        }

        private static async Task<Flags> ReadFlagsNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var flags = new Flags(
                await ReadFlagListNodeAsync(
                    xmlReader),
                await ReadFlagListNodeAsync(
                    xmlReader));

            await xmlReader.ReadAsync();

            return flags;
        }

        private static async Task<Guestbook> ReadGuestbookNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var guestbook = new Guestbook(
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return guestbook;
        }

        private static async Task<LastMatch> ReadLastMatchNodeAsync(XmlReader xmlReader)
        {
            var lastMatch = new LastMatch(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return lastMatch;
        }

        private static async Task<LeagueLevelUnit> ReadLeagueLevelUnitNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var leagueLevelUnit = new LeagueLevelUnit(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return leagueLevelUnit;
        }

        private static async Task<MySupporters> ReadMySupportersNodeAsync(XmlReader xmlReader)
        {
            var totalItems = int.Parse(xmlReader.GetAttribute(NodeName.TotalItems) ?? "0");
            var maxItems = int.Parse(xmlReader.GetAttribute(NodeName.MaxItems) ?? "50");

            var supporterTeamList = new List<SupporterTeam>();

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                while (xmlReader.CheckNode(NodeName.SupporterTeam))
                {
                    supporterTeamList.Add(
                        await ReadSupporterTeamNodeAsync(
                            xmlReader));
                }
            }

            await xmlReader.ReadAsync();

            return new MySupporters(
                totalItems,
                maxItems,
                supporterTeamList);
        }

        private static async Task<NationalTeam> ReadNationalTeamNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var nationalteam = new NationalTeam(
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString());

            await xmlReader.ReadAsync();

            return nationalteam;
        }

        private static async Task<NationalTeam[]> ReadNationalTeamsNodeAsync(XmlReader xmlReader)
        {
            var nationalteams = new List<NationalTeam>();

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                while (xmlReader.CheckNode(NodeName.NationalTeam))
                {
                    nationalteams.Add(
                        await ReadNationalTeamNodeAsync(
                            xmlReader));
                }
            }

            await xmlReader.ReadAsync();

            return nationalteams.ToArray();
        }

        private static async Task<NextMatch> ReadNextMatchNodeAsync(XmlReader xmlReader)
        {
            var nextMatch = new NextMatch(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString());

            await xmlReader.ReadAsync();

            return nextMatch;
        }

        private static async Task<PowerRating> ReadPowerRatingNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var powerRating = new PowerRating(
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return powerRating;
        }

        private static async Task<PressAnnouncement?> ReadPressAnnouncementNodeAsync(XmlReader xmlReader)
        {
            PressAnnouncement? pressAnnouncement = null;

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                string? body = null, subject = null;
                DateTime? sendDate = null;

                // Thank you Hattrick for not maintaining naming nor order between same nodes.
                while (xmlReader.NodeType != XmlNodeType.EndElement)
                {
                    switch (xmlReader.Name)
                    {
                        case NodeName.PressAnnouncementBody:
                        case NodeName.Body:
                            body = (await xmlReader.ReadValueAsync()).AsString();
                            break;

                        case NodeName.PressAnnouncementSubject:
                        case NodeName.Subject:
                            subject = (await xmlReader.ReadValueAsync()).AsString();
                            break;

                        case NodeName.PressAnnouncementSendDate:
                        case NodeName.SendDate:
                            sendDate = (await xmlReader.ReadValueAsync()).AsDateTime();
                            break;
                    }
                }

                ArgumentException.ThrowIfNullOrWhiteSpace(subject);
                ArgumentException.ThrowIfNullOrWhiteSpace(body);
                ArgumentNullException.ThrowIfNull(sendDate);

                pressAnnouncement = new PressAnnouncement(sendDate.Value, subject, body);
            }

            await xmlReader.ReadAsync();

            return pressAnnouncement;
        }

        private static async Task<SupportedTeam> ReadSupportedTeamNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var supportedTeam = new SupportedTeam(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                xmlReader.CheckNode(NodeName.LastMatch) ? await ReadLastMatchNodeAsync(
                    xmlReader) :
                    null,
                xmlReader.CheckNode(NodeName.NextMatch) ? await ReadNextMatchNodeAsync(
                    xmlReader) :
                    null,
                xmlReader.CheckNode(NodeName.PressAnnouncement) ? await ReadPressAnnouncementNodeAsync(
                    xmlReader) :
                    null);

            await xmlReader.ReadAsync();

            return supportedTeam;
        }

        private static async Task<SupportedTeams> ReadSupportedTeamsNodeAsync(XmlReader xmlReader)
        {
            var totalItems = int.Parse(xmlReader.GetAttribute(NodeName.TotalItems) ?? "0");
            var maxItems = int.Parse(xmlReader.GetAttribute(NodeName.MaxItems) ?? "50");

            var supportedTeamList = new List<SupportedTeam>();

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                while (xmlReader.CheckNode(NodeName.SupportedTeam))
                {
                    supportedTeamList.Add(
                        await ReadSupportedTeamNodeAsync(
                            xmlReader));
                }
            }

            await xmlReader.ReadAsync();

            return new SupportedTeams(
                totalItems,
                maxItems,
                supportedTeamList.ToArray());
        }

        private static async Task<SupporterTeam> ReadSupporterTeamNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var supporterTeam = new SupporterTeam(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString());

            await xmlReader.ReadAsync();

            return supporterTeam;
        }

        private static async Task<TeamColors?> ReadTeamColorsNodeAsync(XmlReader xmlReader)
        {
            TeamColors? teamColors = null;

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                teamColors = new TeamColors(
                    (await xmlReader.ReadValueAsync()).AsString(),
                    (await xmlReader.ReadValueAsync()).AsString());
            }

            await xmlReader.ReadAsync();

            return teamColors;
        }

        private static async Task<Team> ReadTeamNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var team = new Team(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsBool(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsBool(),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Arena),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.League),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Country),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Region),
                await ReadTrainerNodeAsync(
                    xmlReader),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                await ReadLeagueLevelUnitNodeAsync(
                    xmlReader),
                await ReadBotStatusNodeAsync(
                    xmlReader),
                await ReadCupNodeAsync(
                    xmlReader),
                await ReadPowerRatingNodeAsync(
                    xmlReader),
                (await xmlReader.ReadValueAsync()).AsNullableLong(),
                (await xmlReader.ReadValueAsync()).AsNullableInt(),
                (await xmlReader.ReadValueAsync()).AsNullableInt(),
                (await xmlReader.ReadValueAsync()).AsNullableInt(),
                await ReadFanclubNodeAsync(
                    xmlReader),
                (await xmlReader.ReadValueAsync()).AsString(),
                xmlReader.CheckNode(NodeName.Guestbook) ? await ReadGuestbookNodeAsync(
                    xmlReader) : null,
                xmlReader.CheckNode(NodeName.PressAnnouncement) ? await ReadPressAnnouncementNodeAsync(
                    xmlReader) : null,
                xmlReader.CheckNode(NodeName.TeamColors) ? await ReadTeamColorsNodeAsync(
                    xmlReader) : null,
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                xmlReader.CheckNode(NodeName.Flags) ? await ReadFlagsNodeAsync(
                    xmlReader) : null,
                await ReadTrophyListNodeAsync(
                    xmlReader),
                xmlReader.CheckNode(NodeName.SupportedTeams) ? await ReadSupportedTeamsNodeAsync(
                    xmlReader) : null,
                xmlReader.CheckNode(NodeName.MySupporters) ? await ReadMySupportersNodeAsync(
                    xmlReader) : null,
                (await xmlReader.ReadValueAsync()).AsBool(),
                (await xmlReader.ReadValueAsync()).AsBool()
            );

            await xmlReader.ReadAsync();

            return team;
        }

        private static async Task<Team[]> ReadTeamsNodeAsync(XmlReader xmlReader)
        {
            var teams = new List<Team>();

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                while (xmlReader.CheckNode(NodeName.Team))
                {
                    teams.Add(
                        await ReadTeamNodeAsync(
                            xmlReader));
                }
            }

            await xmlReader.ReadAsync();

            return teams.ToArray();
        }

        private static async Task<Trainer> ReadTrainerNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var trainer = new Trainer(
                (await xmlReader.ReadValueAsync()).AsLong());

            await xmlReader.ReadAsync();

            return trainer;
        }

        private static async Task<Trophy[]> ReadTrophyListNodeAsync(XmlReader xmlReader)
        {
            var trophies = new List<Trophy>();

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                while (xmlReader.CheckNode(NodeName.Trophy))
                {
                    trophies.Add(
                        await ReadTrophyNodeAsync(
                            xmlReader));
                }
            }

            await xmlReader.ReadAsync();

            return trophies.ToArray();
        }

        private static async Task<Trophy> ReadTrophyNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var trophy = new Trophy(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsString(),
                !xmlReader.IsEmptyElement ? (await xmlReader.ReadValueAsync()).AsInt() : null,
                !xmlReader.IsEmptyElement ? (await xmlReader.ReadValueAsync()).AsInt() : null,
                !xmlReader.IsEmptyElement ? (await xmlReader.ReadValueAsync()).AsInt() : null);

            await xmlReader.ReadAsync();

            return trophy;
        }

        private static async Task<User> ReadUserNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var user = new User(
                (await xmlReader.ReadValueAsync()).AsLong(),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Language),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsBool(),
                await ReadNationalTeamsNodeAsync(
                    xmlReader));

            await xmlReader.ReadAsync();

            return user;
        }
    }
}