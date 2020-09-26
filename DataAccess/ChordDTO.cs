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
        private static string ValDelim
        {
            get { return ValDelim; }
            set => ValDelim = "|";
        }
        private static string LineDelim
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
            //TODO: logic to write each file, 1 per chord length
            //- Note Differences - RootNote - Chord Name
                       
            string fullFile = BuildBody(newChord);

            string[] lines = fullFile.Split(LineDelim);
            int lineCount = lines.Length;
            string chordLength = (newChord.NoteList.Count).ToString();

            fullFile = BuildHeader(lineCount.ToString(), chordLength) + fullFile;
            fullFile = fullFile + BuildFooter(lineCount.ToString(), chordLength);
           
            //write to file
            ChordDTO newInsert = new ChordDTO();

            newInsert.FilePath = chordLength;
            
            return "There was a problem writting this chord.";
        }
        
        private static string BuildHeader(string numLines, string chordLength)
        {
            string returnString;
            DateTime lastMod = new DateTime();
            string lastModDt = lastMod.ToLongDateString();
            returnString = "$Header: File for Chords of length {chordLength} contains {numLines} records. Last updated {lastModDt} /n CSV format with RootNote, NoteDifference, ChordName**";
            return  returnString;
        }

        private static string BuildFooter(string numLines, string chordLength)
        {
            string returnString;
            DateTime lastMod = new DateTime();
            string lastModDt = lastMod.ToLongDateString();
            returnString = "**$Footer: File for Chords of length {chordLength} wrote {numLines} records. Last updated {lastModDt}";
            return returnString;
        }

        private static string BuildBody(Chord newChord)
        {
            List<Chord> allChords = new List<Chord>();
            //Access data add to list

            int chordLength = newChord.NoteDifference.Length;
            allChords = ChordDAO.GetAllChordData(chordLength.ToString());

            //add new data
            allChords.Add(newChord);
            //sort data - do I actually care how?
            allChords.Sort();

            string fileBody = ChordsToBody(allChords);

            return fileBody;
        }        

        private static string ChordsToBody(List<Chord> chordList)
        {
            string body = "";
            foreach (Chord chord in chordList)
            {
                body += ChordToLine(chord);
            }
            return body;
        }

        private static string ChordToLine(Chord chord)
        {
            string line = chord.RootNote + ValDelim;

            foreach (int i in chord.NoteDifference)
            {
                //just combining notes into a string doesn't let extract them
                line += chord.NoteDifference[i] + ',';
            }

            line += chord.ChordName + LineDelim;
            
            return line;

        }

    }
}
