using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerLibrary.DataAccess
{
    interface IDataConnection
    {
        void CreatePrize(string model);
        void CreatePerson(int model);
        void CreateTeam(int model);
        void CreateTournament(int model);
        void UpdateMatchup(int model);

        void CompleteTournament(int model);

        List<int> GetPersonAll();
        List<int> GetTeamAll();
        List<int> GetTournamentAll();
    }
}
