using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordLibrary
{
    public enum NoteNames
    {
        Bsharp = 1,
        C = 1,
        Dflat = 2,
        Csharp = 2,
        D = 3,
        Dsharp = 4,
        Eflat = 4,
        E = 5,
        Fflat = 5,
        Esharp = 6,
        F = 6,
        Fsharp = 7,
        Gflat = 7,
        G = 8,
        Aflat = 9,
        Gsharp = 9,
        A = 10,
        Bb = 11,
        Asharp = 11,
        Cb = 12,
        B = 12,
    }
    public class Notes
    {
        public NoteNames noteNames { get; set; }


        public Notes(NoteNames value)
        {
            noteNames = value;
        }

        public static int FindNoteIncrement(NoteNames note1, NoteNames note2)
        {
            //TODO: Figure out how best to compare the note enums
            return 1;
        }
    }
}
