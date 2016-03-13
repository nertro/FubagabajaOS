using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class DialogCtrl : MonoBehaviour {

    private List<Confrontation> lowConfrontations;

    void Awake()
    {
        lowConfrontations = new List<Confrontation>();
        LoadConfrontations();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void LoadConfrontations()
    {
        FileStream stream = new FileStream("./Assets/Confrontations.xml", FileMode.Open);
        XmlReader reader = XmlReader.Create(stream);

        while (reader.Read())
        {
            if (reader.ReadToFollowing("Confrontation"))
            {
                Confrontation temp = new Confrontation();

                reader.ReadToFollowing("Question");
                reader.ReadStartElement();
                temp.Question = reader.ReadContentAsString();
                lowConfrontations.Add(temp);

                while (reader.ReadToNextSibling("Answer"))
                {
                    string answer;
                    int cost;
                    reader.ReadStartElement();
                    answer = reader.ReadContentAsString();
                    reader.ReadToFollowing("Cost");
                    reader.ReadStartElement();
                    cost = reader.ReadContentAsInt();

                    temp.Answers.Add(answer, cost);
                }

                Debug.Log(temp.Question);
            }
        }


        stream.Close();
    }
}
