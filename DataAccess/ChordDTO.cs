using System;
using System.Collections.Generic;
using System.Text;
using System.IO.FileStream;

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
            set => ValDelim = "|";
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
            StringBuilder sb = new StringBuilder();
            
            
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
            returnString = "$Header: File for Chords of length {chordLength} contains {numLines} records. Last updated {lastModDt} /n CSV format with RootNote, NoteDifference, ChordName";
            return  returnString;
        }

        private string BuildFooter(string numLines, string chordLength)
        {
            string returnString;
            DateTime lastMod = new DateTime();
            string lastModDt = lastMod.ToLongDateString();
            returnString = "$Footer: File for Chords of length {chordLength} wrote {numLines} records. Last updated {lastModDt}";
            return returnString;
        }

        private string BuildBody(Chord newChord)
        {
            List<Chord> allChords = new List<Chord>();
            //Access data add to list

            //add new data
            allChords.Add(newChord);
            //sort data - do I actually care how?
            allChords.Sort();

            string fileBody = ChordsToBody(allChords);

            return fileBody;
        }

        private string ChordsToBody(List<Chord> chordList)
        {
            string body = "";
            foreach (Chord chord in chordList)
            {
                body += ChordToLine(chord);
            }
            return body;
        }

        private string ChordToLine(Chord chord)
        {
            string line = chord.RootNote + ValDelim;

            foreach (int i in chord.NoteDifference)
            {
                line += chord.NoteDifference[i];
            }

            line += chord.ChordName + LineDelim;
            
            return line;

        }

    }
}
