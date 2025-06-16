using System.Collections.Generic;
using UnityEngine;

public class SMParser : MonoBehaviour
{
    public TextAsset smTextFile;
    public float bpm = 120f;
    public List<NoteData> notes = new List<NoteData>();

    public void Parse()
    {
        if (smTextFile == null)
        {
            Debug.LogError("⚠ No se ha asignado el archivo .sm (renombrado a .txt)");
            return;
        }

        notes.Clear();

        string[] lines = smTextFile.text.Split('\n');
        float secondsPerBeat = 60f / bpm;
        bool readingNotes = false;
        float currentBeat = 0f;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (line.StartsWith("#BPMS"))
            {
                int equalIndex = line.IndexOf('=');
                if (equalIndex != -1)
                {
                    string bpmPart = line.Substring(equalIndex + 1).TrimEnd(';');
                    foreach (var pair in bpmPart.Split(','))
                    {
                        string[] values = pair.Split('=');
                        if (values.Length == 2 && float.TryParse(values[1], out float newBpm))
                            bpm = newBpm;
                    }
                    secondsPerBeat = 60f / bpm;
                }
            }


            if (line.StartsWith("#NOTES"))
            {
                readingNotes = true;
                continue;
            }

            if (readingNotes && !string.IsNullOrWhiteSpace(line) && line != ";" && !line.StartsWith(","))
            {
                for (int i = 0; i < Mathf.Min(4, line.Length); i++)
                {
                    if (line[i] == '1')
                    {
                        notes.Add(new NoteData((ArrowType)i, currentBeat * secondsPerBeat));
                    }
                }
                currentBeat += 1f;
            }
        }

        Debug.Log($"✅ Notas cargadas: {notes.Count}");
    }
}
