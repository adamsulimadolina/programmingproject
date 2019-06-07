using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projekt
{
    /// <summary>
    /// klasa kontrolujaca serializacje i deserializacje danych
    /// </summary>
    [Serializable]
    class Controler
    {
        public static Teams teams = new Teams();
        public static Tournament VolleyballTournament = new Tournament();
        public static Tournament DodgeballTournament = new Tournament();
        public static Tournament TugOfWarTournament = new Tournament();
        public static Referees referees = new Referees();
        public static void Serialization()
        {
            VolleyballTournament.SerializeVolleyballFinals();
            VolleyballTournament.SerializeVolleyballMatchlist();
            VolleyballTournament.SerializeVolleyballScoreboard();
            DodgeballTournament.SerializeDodgeballFinals();
            DodgeballTournament.SerializeDodgeballMatchlist();
            DodgeballTournament.SerializeDodgeballScoreboard();
            TugOfWarTournament.SerializeTugOfWarFinals();
            TugOfWarTournament.SerializeTugOfWarMatchlist();
            TugOfWarTournament.SerializeTugOfWarScoreboard();
            referees.SerializeReferees();
            teams.SerializeDodgeballTeams();
            teams.SerializeTugOfWarTeams();
            teams.SerializeVolleyballTeams();
            VolleyballTournament.SerializeVolleyIsTournamentPlayed();
            VolleyballTournament.SerializeVolleyIsFinalsPlayed();
            DodgeballTournament.SerializeDodgeballIsTournamentPlayed();
            DodgeballTournament.SerializeDodgeballIsFinalsPlayed();
            TugOfWarTournament.SerializeTOWIsTournamentPlayed();
            TugOfWarTournament.SerializeTOWIsFinalsPlayed();
            VolleyballTournament.SerializeVolleyballFinalsMatchList();
            DodgeballTournament.SerializeDodgeballFinalsMatchList();
            TugOfWarTournament.SerializeTOWFinalsMatchList();
        }
        public static void Deserialization()
        {
            VolleyballTournament.DeserializeVolleyballFinals();
            VolleyballTournament.DeserializeVolleyballMatchList();
            VolleyballTournament.DeserializeVolleyballScoreboard();
            DodgeballTournament.DeserializeDodgeballFinals();
            DodgeballTournament.DeserializeDodgeballMatchList();
            DodgeballTournament.DeserializeDodgeballScoreboard();
            TugOfWarTournament.DeserializeTugOfWarFinals();
            TugOfWarTournament.DeserializeTugOfWarMatchList();
            TugOfWarTournament.DeserializeTugOfWarScoreboard();
            referees.DeserializeReferees();
            teams.DeserializeDodgeballTeams();
            teams.DeserializeTugOfWarTeams();
            teams.DeserializeVolleyballTeams();
            VolleyballTournament.DeserializeVolleyisTournamentPlayed();
            VolleyballTournament.DeserializeVolleyisFinalsPlayed();
            DodgeballTournament.DeserializeDodgeballisTournamentPlayed();
            DodgeballTournament.DeserializeDodgeballisFinalsPlayed();
            TugOfWarTournament.DeserializeTOWisTournamentPlayed();
            TugOfWarTournament.DeserializeTOWisFinalsPlayed();
            VolleyballTournament.DeserializeVolleyballFinalsMatchList();
            DodgeballTournament.DeserializeDodgeballFinalsMatchList();
            TugOfWarTournament.DeserializeTOWFinalsMatchList();
        }
    }

}
