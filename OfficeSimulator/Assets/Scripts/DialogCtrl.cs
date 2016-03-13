using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class DialogCtrl : MonoBehaviour {

    private List<Confrontation> lowConfrontations;
    private List<Confrontation> highConfrontations;

    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Button[] answers;
    [SerializeField]
    private Text[] answerTexts;

    private const float MaxChanceTweak = 20f; //tweak value for low or high confrontation calculation

    public event EventHandler<CustomEventArgs> AnswerEvent;

    void Awake()
    {
        lowConfrontations = new List<Confrontation>();
        LoadConfrontations();
    }

    void Start()
    {
        GameCtrl.DialogueEvent += OnDialogue;
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnDialogue(object sender, CustomEventArgs e)
    {
        Confrontation confrontation = ChooseConfrontation(e.Value);
        string[] answerKeys = confrontation.Answers.Keys.ToArray();

        dialogueText.text = confrontation.Question;

        for (int i = 0; i < confrontation.Answers.Count; i++)
        {
            answers[i].gameObject.SetActive(true);
            answers[i].onClick.AddListener(delegate () { OnAnswer(confrontation.Answers[answerKeys[i]]); });

            answerTexts[i].text = answerKeys[i];
        }
    }

    private void OnAnswer(float cost)
    {
        if (AnswerEvent == null)
            return;

        AnswerEvent(this, new CustomEventArgs(cost));
    }

    private Confrontation ChooseConfrontation(float reputation)
    {
        //100 for 100 percent
        float chance = 100 - reputation + MaxChanceTweak;
        chance /= 100;

        //tweak maxChanceRandom to determine how often there will be highConfrontations
        float rand = UnityEngine.Random.Range(0, 1);

        if (rand > chance)
        {
            int index = UnityEngine.Random.Range(0, lowConfrontations.Count - 1);

            return lowConfrontations[index];
        }
        else
        {
            int index = UnityEngine.Random.Range(0, highConfrontations.Count - 1);

            return lowConfrontations[index];
        }
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
