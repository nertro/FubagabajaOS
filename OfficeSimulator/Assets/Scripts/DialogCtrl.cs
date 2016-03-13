using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class DialogCtrl : MonoBehaviour {

    private List<Confrontation> lowConfrontations;
    private List<Confrontation> highConfrontation;

    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Button[] answers;

    void Awake()
    {
        lowConfrontations = new List<Confrontation>();
        LoadConfrontations();
    }

    void Start()
    {
        GameCtrl.DialogueEvent += OnDialogue;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnDialogue(object sender, CustomEventArgs e)
    {

    }

    private void ChooseConfrontation(float reputation)
    {
        float chance = 100 - reputation;
        chance /= 100;
    }

    private void LoadConfrontations()
    {
        FileStream stream = new FileStream("./Assets/Confrontations.xml", FileMode.Open);
        XmlReader reader = XmlReader.Create(stream);

        while (reader.Read())
        {
            reader.ReadToFollowing("Level1");


            if (reader.ReadToFollowing("Confrontation"))
            {
                GetConfrontation(reader, lowConfrontations);

                while (reader.ReadToNextSibling("Confrontation"))
                {
                    GetConfrontation(reader, lowConfrontations);
                }
            }

            if (reader.ReadToFollowing("Level2"))
            {
                if (reader.ReadToFollowing("Confrontation"))
                {
                    GetConfrontation(reader, lowConfrontations);

                    while (reader.ReadToNextSibling("Confrontation"))
                    {
                        GetConfrontation(reader, lowConfrontations);
                    }
                }
            }
        }


        stream.Close();
    }

    private void GetConfrontation(XmlReader reader, List<Confrontation> list)
    {
        Confrontation temp = new Confrontation();

        reader.ReadToFollowing("Question");
        reader.ReadStartElement();
        temp.Question = reader.ReadContentAsString();

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

        list.Add(temp);
        Debug.Log(temp.Question);
    }
}
