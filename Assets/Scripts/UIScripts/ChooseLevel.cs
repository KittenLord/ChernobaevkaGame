using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;







[SerializeField]
public class ChooseLevel : MonoBehaviour
{
    public Text descriptionText;
    public Text rwEvents;
    public Text nameText;
    public int IndexNumber;

    public Image image;
   // public GameObject window;
   
    [System.Serializable]
    public class LevelInfo
    {
        public string LevelDescription;
        public string LevelName;
        public string RealWorldEvents;
        public Image LevelImage;
        

      

        public LevelInfo(string levelDescription, string levelName, string realWorldEvents, Image levelImage )
        {
            LevelDescription = levelDescription;
            LevelImage = levelImage;
            LevelName = levelName;
            RealWorldEvents = realWorldEvents;
            
        }

       

    }
    public List<LevelInfo> levelsInfo = new List<LevelInfo>();

   
    public void LevelPressed()
    {
        
       
        descriptionText.text = levelsInfo[IndexNumber].LevelDescription;
        rwEvents.text = levelsInfo[IndexNumber].RealWorldEvents;
        nameText.text = levelsInfo[IndexNumber].LevelName;

       
        Debug.Log("Button Pressed!");

    }


}



/*public GameObject recallTextObject;

public Transform content;*/



/*public void Start()
{
    string readFromFilePath = Application.streamingAssetsPath + "/UIScripts/" + "test1" + ".txt";

    List<string> FileLines = File.ReadLines(readFromFilePath).ToList();

    foreach (string line in FileLines)
    {
        Instantiate(recallTextObject, content);
        recallTextObject.GetComponent<Text>().text = line;
    }
}*/




