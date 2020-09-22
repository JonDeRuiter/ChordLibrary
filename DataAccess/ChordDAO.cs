using System;
using System.Collections.Generic;
using System.IO;
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

        public static string GetAllChordData(string chordSize)
        {
            //read data
            string chordString;
            ChordDAO dao = new ChordDAO(chordSize);
            chordString = dao.ReadData(dao);

            List<Chord> allChords = new List<Chord>();



            return "found something";
        }

        public static Chord FindAChord()
        {


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

            string line;
            chords.Add(ChordFromLine(line));


            return chords;
        }

        private Chord ChordFromLine(string line)
        {
            Chord chord = new Chord();
            chord.RootNote = line.Substring();
            return chord;
        }

        private string SplitOnField(string line, int startOn)
        {
            string field = line.Substring(startOn, line.Split);
        }
    }
}
