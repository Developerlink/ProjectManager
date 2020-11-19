using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerLibrary.DataAccess
{
    //x TODO: 0) Design and mockup data classes and their relations.
    //x TODO: 1) Create Models folder and DataAccess folder in library.
    //x TODO: 2) In DataAccess: Create IDataConnection interface and SqlConnector class.
    //x TODO: 3) In SqlConnector: Get Dapper from Nuget manager.
    //x TODO: Add using statement to Dapper.
    //x TODO: Add connectionstring info in app.config
    //x TODO: Example: <add name ="TournamentsSql" 
    //x TODO: connectionString="Server=MININT-PGF1701;Database=TournamentsDB;Trusted_Connection=True;" 
    //x TODO: providerName="System.Date.SqlClient"/>
    //x TODO: 4) In Models: Create the designed data classes needed.
    // TODO: 5) Create the database and tables in the sql database. 
    // TODO: 6) Create the form(s) needed in this app. 
    // TODO: 7) Add business and interaction logic to each form, starting with all the create forms first.
    // TODO: 8) Test and debug everything.
    // TODO: 9) Refactor and retest.
    // TODO: 10) Polish aesthetics of GUI. 

    //public class SqlConnector : IDataConnection
    //{
    //    private const string db = "TournamentsSql";
    //    public void CreatePerson(PersonModel model)
    //    {
    //        using (IDbConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
    //        {
    //            var p = new DynamicParameters();
    //            p.Add("@FirstName", model.FirstName);
    //            p.Add("@LastName", model.LastName);
    //            p.Add("@Email", model.Email);
    //            p.Add("@Phonenumber", model.PhoneNumber);
    //            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

    //            conn.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

    //            model.Id = p.Get<int>("@Id");
    //        }
    //    }

    //    /// <summary>
    //    /// Saves a new prize to the database.
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <returns>The prize information, including the unique identifier</returns>
    //    public void CreatePrize(PrizeModel model)
    //    {
    //        using (IDbConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
    //        {
    //            var p = new DynamicParameters();
    //            p.Add("@PlaceNumber", model.PlaceNumber);
    //            p.Add("@PlaceName", model.PlaceName);
    //            p.Add("@PrizeAmount", model.PrizeAmount);
    //            p.Add("@PrizePercentage", model.PrizePercentage);
    //            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

    //            conn.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

    //            model.Id = p.Get<int>("@Id");
    //        }
    //    }

    //    public void CreateTeam(TeamModel model)
    //    {
    //        using (IDbConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
    //        {
    //            var p = new DynamicParameters();
    //            p.Add("@TeamName", model.TeamName);
    //            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

    //            conn.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);

    //            model.Id = p.Get<int>("@Id");

    //            foreach (PersonModel tm in model.TeamMembers)
    //            {
    //                p = new DynamicParameters();
    //                p.Add("@TeamId", model.Id);
    //                p.Add("@PersonId", tm.Id);

    //                conn.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);
    //            }
    //        }
    //    }

    //    public void CreateTournament(TournamentModel model)
    //    {
    //        using (IDbConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
    //        {
    //            SaveTournament(conn, model);

    //            SaveTournamentPrizes(conn, model);

    //            SaveTournamentEntries(conn, model);

    //            SaveTournamentRounds(conn, model);

    //            TournamentLogic.UpdateTournamentResults(model);
    //        }
    //    }

    //    private void SaveTournament(IDbConnection conn, TournamentModel model)
    //    {
    //        var p = new DynamicParameters();
    //        p.Add("@TournamentName", model.TournamentName);
    //        p.Add("@EntryFee", model.EntryFee);
    //        p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

    //        conn.Execute("dbo.spTournaments_Insert", p, commandType: CommandType.StoredProcedure);

    //        model.Id = p.Get<int>("@Id");
    //    }

    //    private void SaveTournamentPrizes(IDbConnection conn, TournamentModel model)
    //    {
    //        foreach (PrizeModel pz in model.Prizes)
    //        {
    //            var p = new DynamicParameters();
    //            p.Add("@TournamentId", model.Id);
    //            p.Add("@PrizeId", pz.Id);
    //            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

    //            conn.Execute("dbo.spTournamentPrizes_Insert", p, commandType: CommandType.StoredProcedure);
    //        }
    //    }

    //    private void SaveTournamentEntries(IDbConnection conn, TournamentModel model)
    //    {
    //        foreach (TeamModel tm in model.EnteredTeams)
    //        {
    //            var p = new DynamicParameters();
    //            p.Add("@TournamentId", model.Id);
    //            p.Add("@TeamId", tm.Id);
    //            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

    //            conn.Execute("dbo.spTournamentEntries_Insert ", p, commandType: CommandType.StoredProcedure);
    //        }
    //    }

    //    private void SaveTournamentRounds(IDbConnection conn, TournamentModel model)
    //    {
    //        // Loop through the rounds
    //        // Loop through the matchups
    //        // Save the individual matchup
    //        // Loop through the entries and save them

    //        // Remember that rounds is a list of List<MatchupModel>
    //        foreach (List<MatchupModel> round in model.Rounds)
    //        {
    //            foreach (MatchupModel matchup in round)
    //            {
    //                var p = new DynamicParameters();
    //                p.Add("@TournamentId", model.Id);
    //                p.Add("@MatchupRound", matchup.MatchupRound);
    //                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

    //                conn.Execute("dbo.spMatchups_Insert ", p, commandType: CommandType.StoredProcedure);

    //                matchup.Id = p.Get<int>("@Id");

    //                foreach (MatchupEntryModel entry in matchup.Entries)
    //                {
    //                    p = new DynamicParameters();
    //                    p.Add("@MatchupId", matchup.Id);
    //                    if (entry.ParentMatchup == null)
    //                    {
    //                        p.Add("@ParentMatchupId", null);
    //                    }
    //                    else
    //                    {
    //                        p.Add("@ParentMatchupId", entry.ParentMatchup.Id);
    //                    }
    //                    if (entry.TeamCompeting == null)
    //                    {
    //                        p.Add("@TeamCompetingId", null);
    //                    }
    //                    else
    //                    {
    //                        p.Add("@TeamCompetingId", entry.TeamCompeting.Id);
    //                    }
    //                    p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

    //                    conn.Execute("dbo.spMatchupEntries_Insert ", p, commandType: CommandType.StoredProcedure);

    //                    entry.Id = p.Get<int>("@Id");
    //                }
    //            }
    //        }

    //    }

    //    public List<PersonModel> GetPersonAll()
    //    {
    //        List<PersonModel> output;

    //        using (IDbConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
    //        {
    //            output = conn.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
    //        }

    //        return output;
    //    }

    //    public List<TeamModel> GetTeamAll()
    //    {
    //        List<TeamModel> output;

    //        using (IDbConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
    //        {
    //            output = conn.Query<TeamModel>("dbo.spTeams_GetAll").ToList();

    //            foreach (TeamModel t in output)
    //            {
    //                var p = new DynamicParameters();
    //                p.Add("@TeamId", t.Id);

    //                t.TeamMembers = conn.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
    //            }
    //        }

    //        return output;
    //    }

    //    public List<TournamentModel> GetTournamentAll()
    //    {
    //        List<TournamentModel> output;
    //        var p = new DynamicParameters();

    //        using (IDbConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
    //        {
    //            output = conn.Query<TournamentModel>("dbo.spTournaments_GetAll").ToList();

    //            foreach (TournamentModel tm in output)
    //            {
    //                p = new DynamicParameters();
    //                p.Add("@TournamentId", tm.Id);

    //                // Populate prizes.
    //                tm.Prizes = conn.Query<PrizeModel>("dbo.spPrizes_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

    //                // Populate teams.
    //                tm.EnteredTeams = conn.Query<TeamModel>("dbo.spTeams_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

    //                foreach (TeamModel t in tm.EnteredTeams)
    //                {
    //                    p = new DynamicParameters();
    //                    p.Add("@TeamId", t.Id);

    //                    t.TeamMembers = conn.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
    //                }

    //                // Populate rounds.
    //                p = new DynamicParameters();
    //                p.Add("@TournamentId", tm.Id);
    //                List<MatchupModel> matchups = conn.Query<MatchupModel>("dbo.spMatchups_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

    //                foreach (MatchupModel m in matchups)
    //                {
    //                    p = new DynamicParameters();
    //                    p.Add("@MatchupId", m.Id);

    //                    m.Entries = conn.Query<MatchupEntryModel>("dbo.spMatchupEntries_GetByMatchup", p, commandType: CommandType.StoredProcedure).ToList();

    //                    // Populate each entry (2 models)
    //                    // Poluate each matchup (1 model)
    //                    List<TeamModel> allTeams = GetTeamAll();

    //                    if (m.WinnerId > 0)
    //                    {
    //                        m.Winner = allTeams.Where(x => x.Id == m.WinnerId).First();
    //                    }

    //                    foreach (var me in m.Entries)
    //                    {
    //                        if (me.TeamCompetingId > 0)
    //                        {
    //                            me.TeamCompeting = allTeams.Where(x => x.Id == me.TeamCompetingId).First();
    //                        }

    //                        if (me.ParentMatchupId > 0)
    //                        {
    //                            me.ParentMatchup = matchups.Where(x => x.Id == me.ParentMatchupId).First();
    //                        }
    //                    }
    //                }

    //                // List<List<MatchupModel>>
    //                List<MatchupModel> currRow = new List<MatchupModel>();
    //                int currRound = 1;

    //                foreach (MatchupModel m in matchups)
    //                {
    //                    if (m.MatchupRound > currRound)
    //                    {
    //                        tm.Rounds.Add(currRow);
    //                        currRow = new List<MatchupModel>();
    //                        currRound += 1;
    //                    }

    //                    currRow.Add(m);
    //                }

    //                tm.Rounds.Add(currRow);
    //            }

    //        } // using

    //        return output;
    //    }

    //    public void UpdateMatchup(MatchupModel model)
    //    {
    //        using (IDbConnection conn = new SqlConnection(GlobalConfig.ConnString(db)))
    //        {
    //            var p = new DynamicParameters();

    //            if (model.Winner != null)
    //            {
    //                p.Add("@Id", model.Id);
    //                p.Add("@WinnerId", model.Winner.Id);

    //                conn.Execute("dbo.spMatchups_Update", p, commandType: CommandType.StoredProcedure);
    //            }

    //            // dbo.spMatchupEntries_Update
    //            foreach (MatchupEntryModel me in model.Entries)
    //            {
    //                if (me.TeamCompeting != null)
    //                {
    //                    p = new DynamicParameters();
    //                    p.Add("@Id", me.Id);
    //                    p.Add("@TeamCompetingId", me.TeamCompeting.Id);
    //                    p.Add("@Score", me.Score);

    //                    conn.Execute("dbo.spMatchupEntries_Update", p, commandType: CommandType.StoredProcedure);
    //                }
    //            }

    //        }
    //    }

    //    public void CompleteTournament(TournamentModel model)
    //    {
    //        // Flag tournament as inactive.

    //    }
    //}
}
