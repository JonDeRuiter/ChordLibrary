using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChordLibrary.DataAccess
{
    class ChordDAO
    {
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

        public ChordDAO()
        {

        }
        public ChordDAO(string chordSize)
        {
            FilePath = chordSize;
        }

        public static List<Chord> GetAllChordData(string chordSize)
        {
            //read data            
            string chordString;
            ChordDAO dao = new ChordDAO(chordSize);
            chordString = dao.ReadData(dao);

            List<Chord> allChords = new List<Chord>();
                       
            return allChords;
        }

        public static Chord FindAChord(Chord unknownChord)
        {
            int chordLength = unknownChord.NoteDifference.Length;
            List<Chord> allChords = GetAllChordData(chordLength.ToString());
            
            //TODO figure out if linq works better here
            foreach (Chord co in allChords)
            {
                if (co.NoteDifference == unknownChord.NoteDifference && co.RootNote == unknownChord.RootNote)
                {
                    
                    return co;
                }
            }

            return new Chord();
        }

        private string ReadData(ChordDAO dao)
        {
            string fullFile, header, footer;
            string body = "";
            try
            {
                using (StreamReader sr = new StreamReader(dao.FilePath))
                {
                    fullFile = sr.ReadToEnd();
                }

                if (FileEmpty(fullFile))
                {
                    return "The file is empty";
                }

                header = GetHeader(fullFile);
                body = GetBody(fullFile, header.Length);
                footer = GetFooter(fullFile);
            }
            catch (Exception e)
            {
                return e.ToString();
                //TODO: Some sort of logging?
            }

            return body + LineDelim;
        }

        private string GetHeader(string fullFile)
        {
            string header;

            int headerEnd = fullFile.IndexOf("**");
            header = fullFile.Substring(0, headerEnd);

            return header;
        }

        private string GetBody(string fullFile, int endOfHeader)
        {
            string body;

            int bodyEnd = fullFile.LastIndexOf("**");
            body = fullFile.Substring(endOfHeader, bodyEnd);

            return body;
        }

        private string GetFooter(string fullFile)
        {
            string footer;

            int bodyEnd = fullFile.LastIndexOf("**");
            footer = fullFile.Substring(0, bodyEnd);

            return footer;
        }

        private List<Chord> BodyToChordList(string body)
        {
            List<Chord> chords = new List<Chord>();

            string[] splitLines = body.Split(LineDelim);

            foreach (string line in splitLines)
            {
                chords.Add(ChordFromLine(line));
            }


            return chords;
        }

        private Chord ChordFromLine(string line)
        {
            Chord chord = new Chord();
            string[] fieldArray = line.Split(ValDelim);
             
            if (int.TryParse(fieldArray[1], out int rootInt))
            {
                chord.RootNote = (NoteNames)rootInt;
            }
            else
            {
                throw new Exception("Error parsing data file: " + line );
            }

            chord.NoteDifference = GetNoteRelationships(fieldArray[2]);

            chord.ChordName = fieldArray[3];

            return chord;
        }

        private int[] GetNoteRelationships(string noteRelString)
        {
            string[] stringArray = noteRelString.Split(",");
            int[] noteRels = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                int.TryParse(stringArray[i], out noteRels[i]);
            }

            return noteRels;
        }

        private void FileCheck(ChordDAO dao)
        {
            if (!File.Exists(dao.FilePath))
            {
                using (StreamWriter sw = File.CreateText(dao.FilePath))
                {

                }
            }
            //file exists, we don't need to create it
            }

        private bool FileEmpty(string fileContents)
        {
            
            if (fileContents.Count() >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

}
