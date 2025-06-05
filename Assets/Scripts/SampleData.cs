using UnityEngine;

[CreateAssetMenu(fileName = "NewSample", menuName = "Samples/Sample Data")]
public class SampleData : ScriptableObject
{
    public string sampleName;
    public Sprite sampleImage;
    [TextArea]
    public string description;
}
