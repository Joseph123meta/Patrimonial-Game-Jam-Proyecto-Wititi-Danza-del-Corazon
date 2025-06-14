using System.Collections.Generic;
using UnityEngine;

public class SMParser : MonoBehaviour
{
    public TextAsset smTextFile;
    public float bpm = 120f;
    public List<NoteData> notes = new List<NoteData>();
    

    public void Parse(AudioSource music)
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
        List<string> currentMeasure = new();

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

            if (!readingNotes) continue;

            if (line == ",")
            {
                // Verifica si el compás contiene al menos una flecha real
                bool hasRealNote = false;
                foreach (var measureLine in currentMeasure)
                {
                    if (measureLine.Length >= 4 &&
                        (measureLine[0] == '1' || measureLine[1] == '1' ||
                        measureLine[2] == '1' || measureLine[3] == '1'))
                    {
                        hasRealNote = true;
                        break;
                    }
                }

                if (hasRealNote)
                {
                    int divisions = currentMeasure.Count;
                    float beatIncrement = 1f / divisions;

                    for (int i = 0; i < divisions; i++)
                    {
                        string noteLine = currentMeasure[i];
                        for (int j = 0; j < Mathf.Min(4, noteLine.Length); j++)
                        {
                            if (noteLine[j] == '1')
                            {
                                float beatTime = currentBeat + (i * beatIncrement);
                                float timeInSeconds = beatTime * secondsPerBeat;

                                if (timeInSeconds <= music.clip.length - 2.5f)
                                {
                                    notes.Add(new NoteData((ArrowType)j, timeInSeconds));
                                }
                            }
                        }
                    }

                    currentBeat += 1f;
                }
                else
                {
                    // No se incrementa el currentBeat porque el compás está vacío
                    Debug.Log("⏭️ Compás vacío omitido");
                }

                currentMeasure.Clear();
            }
            else if (!string.IsNullOrWhiteSpace(line) && line != ";")
            {
                currentMeasure.Add(line);
            }
        }

        Debug.Log($"✅ Notas válidas dentro del tiempo ({music.clip.length}s): {notes.Count}");
    }


}
