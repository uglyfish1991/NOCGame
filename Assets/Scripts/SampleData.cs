using UnityEngine;

//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
//¬|This script is for a scriptable object. Scriptable objects are data containers that can   |
//¬|be reused to hold info as an asset.                                                       |
//¬|By right clicking in the project window, and going to "samples" and "sample data" I can   |
//¬|make a new file called "NewSample" which holds a name, image, and description. I can then |
//¬|attach this to a sample prefab.                                                           |
//¬|This means I can make consistent, referencible information for each sample, and this info |
//¬|can be slotted into the SampleSlide panel easily (based on which sample is collided with) |
//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|

[CreateAssetMenu(fileName = "NewSample", menuName = "Samples/Sample Data")]
public class SampleData : ScriptableObject
{
    public string sampleName;
    public Sprite sampleImage;
    [TextArea]
    public string description;
}
