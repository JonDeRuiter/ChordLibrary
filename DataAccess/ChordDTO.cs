using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChordLibrary.DataAccess
{
    class ChordDTO
    {
        //these can be identical between DTO and DAO
        private string Header { get; set; }
        private string Footer { get; set; }
        private string ValDelim
        {
            get { return ValDelim; }
            set => ValDelim = ",";
        }
        private string LineDelim
        {
            get { return LineDelim; }
            set => LineDelim = "*";
        }
        private List<string> Body { get; set; }
        private string FilePath
        {
            get { return FilePath; }
            set => FilePath = "~\\ChordLibrary\\Storage\\" + value.ToString() + ".txt";
        }

        public ChordDTO()
        {

        }
    
        public static string InsertNewChord(Chord newChord)
        {
            //TODO: logic to read and write each file, 1 per chord length
            //- Note Differences - RootNote - Chord Name
            //load former file
            //create Body
            //set header 
            //set footer
            //write to file
            ChordDTO newInsert = new ChordDTO();

            newInsert.FilePath = newChord.NoteList.Count.ToString();
            
            return "There was a problem writting this chord.";
        }
        
        private string BuildHeader(string numLines, string chordLength)
        {
            string returnString;
            DateTime lastMod = new DateTime();
            string lastModDt = lastMod.ToLongDateString();
            returnString = "$Header: File for Chords of length {numLines} contains {chordLength} records. Last updated {lastModDt} /n CSV format with NoteDifference, RootNote, ChordName";
            return  returnString;
        }

    }
}
