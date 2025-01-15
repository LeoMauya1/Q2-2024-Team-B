using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Hints : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    [TextArea(3, 100)]
    public List<string> sentences;
    void Start()
    {
        List<string> shuffledSentences = sentences.OrderBy(x => Artifact.randyTheRandom.Next()).ToList();
        hintText.text = string.Format($"Hint: {shuffledSentences[0]}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
