using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChordLibrary.DataAccess
{
    class ChordDAO
    {
        private static JObject JsonDAO { get; set; }
        private string FilePath
        {
            get { return FilePath; }
            set => FilePath = "~\\ChordLibrary\\Storage\\ChordStorage.json";
        }

        public ChordDAO()
        {

        }

        public static string FindChord(Chord newChord)
        {
            //TODO figure out how to navigate the JSON file with the appropriate model
            //Storage - NumberOfNotes - Note Differences - RootNote - Chord Name

            return "There was a problem finding this chord.";
        }
    }
}
