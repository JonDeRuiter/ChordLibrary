using System;
using System.Collections.Generic;
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

        public static string GetAllChordData()
        {
            //read data
            return "found something";
        }

    }
}
