using ChordLibrary.DataAccess;
using System;
using System.Collections.Generic;

namespace ChordLibrary
{
    public class Chord
    {
        public string ChordName { get; set; }     
        public NoteNames RootNote { get; set; }
        public List<NoteNames> NoteList { get; set; }
        public int[] NoteDifference { get; set; }
        public string Message { get; set; }


        public Chord()
        {

        }

        public Chord (string chordName, List<NoteNames> noteNames)
        {
            ChordName = chordName;
            NoteList = noteNames;
        }

        public Chord (NoteNames rootNote, int[] noteDifference, string chordName)
        {
            RootNote = rootNote;
            ChordName = chordName;
            NoteDifference = noteDifference;
        }

        //Insert Chord if new
        public string InsertChord(Chord chord)
        {
            if (chord.IsNewChord())
            {
                //insert chord + all variations of it root c through b
                ChordDTO.InsertNewChord(chord);
            }
            else
            {
                //Don't insert chord
                return "Chord already exists.";
            }

            return "Chord saved successfuly";
        }

        public static Chord FindChord(List<NoteNames> notesEntered)
        {
            Chord unknownChord = new Chord();
            unknownChord.NoteList = notesEntered;
            unknownChord.RootNote = notesEntered[0];          

            unknownChord.NoteDifference = FindNoteRelationship(notesEntered);
                        
            if (unknownChord.IsNewChord())
            {
                unknownChord.Message = "Either this is not a formal chord, or we don't have it in our system yet.";
            }
            else
            {
                unknownChord.Message = "We found your chord!";
                //TODO: get the rest of the chord data
            }

            return unknownChord;
            }
        
        private bool IsNewChord()
        {
            //TODO: make a connection to storage to check
            //this.Chord;

            return false;
        }

        //the mathematical relationship between the root note and the rest of the notes in the chord define their chord. This method calculates those relationships
        private static int[] FindNoteRelationship(List<NoteNames> noteNames)
        {
            //Need to check this to make sure it functions as expected - [0] should always hold a value of '0'
            int[] noteRel = new int[(noteNames.Count - 1)];


            int rootNote = (int)noteNames[0];
            int[] enumArray = EnumToIntArray(noteNames);
            for (int i = 1; i < enumArray.Length; i++)
            {
                int safeNote = SafeGuardNotes(rootNote, enumArray[i]);
                noteRel[(i - 1)] = safeNote - rootNote;
            }

            return noteRel;
        }

        //The enumerable list only represents an octaves worth of notes
        //real chords may go beyond those limitations but we can still use simple math to understand how they work
        private static int[] EnumToIntArray(List<NoteNames> notesEntered)
        {
            int[] notesByNum = new int[notesEntered.Count - 1];
            for (int i = 0; i < notesByNum.Length; i++)
            {
                notesByNum[i] = (int)notesEntered[i];
            }
            return notesByNum;

        }

        //Not all chords start near C, and even the ones that do may extend beyond an octave higher 
        //so we need to make sure the number representation can accurately define the relationships between notes
        private static int SafeGuardNotes(int rootNote, int note)
        {
            if (note > rootNote)
            {
                return note;
            }
            else
            {
                return (note + 12);
            }


        }

    }
}
