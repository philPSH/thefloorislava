using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This level generator class generates and returns a list of columns based on the given difficulty and length
public class LevelGenerator : MonoBehaviour
{
    // Reference to the prefabs to instantiate
    public GameObject goodColumn;
    public GameObject badColumn;
    public GameObject emberParticles;
    public GameObject ashParticles;

    // List references
    private List<GameObject> columnList = null;
    private List<GameObject> particleList = null;

    // These variable are used to prevent continual bad columns
    private bool lastColumnIsGood = true;
    private bool secondLastColumnIsGood = true;

    // Column width based on 240 pixels width image
    private float columnWidth = 2.75f;
    private float particleYPosition = -3f;
    private float particleZPosition = -0.5f;
    private float particleFrequency = 3f;

    // This is for testing level generation
    [Header("TESTING ONLY")]
    public int testLength = 5;
    [Range(10, 100)]
    public int testDifficuty = 10;

    // This function generates a level based on the difficulty specified and updates the time/returns the time required
    // to complete the level.
    // length - This is the length of the level in columns
    // time - float to be updated in the function to reflect the new level
    // difficulty - value between 0 and 100 (0% and 100%) for the chance of spawning bad columns
    public void GenerateLevel(int length, int difficulty = 10)
    {
        columnList = new List<GameObject>();
        particleList = new List<GameObject>();
        Transform level = this.transform.GetChild(0);
        float xPosition = 0;

        // the first object in the list must be a good column
        columnList.Add(GameObject.Instantiate(goodColumn, level));

        // add ember particles at the starting column
        particleList.Add(GameObject.Instantiate(emberParticles, level));
        particleList.ElementAt<GameObject>(0).transform.localPosition = new Vector3(0, particleYPosition, particleZPosition);

        // add ash particles at the starting column
        particleList.Add(GameObject.Instantiate(ashParticles, level));
        particleList.ElementAt<GameObject>(1).transform.localPosition = new Vector3(0, particleYPosition, particleZPosition);

        // length - 2 to account for the first and last columns
        for (int i = 1; i < (length - 1); i++)
        {
            xPosition += columnWidth;

            // spawn a particle system every four columns
            if(i % particleFrequency == 0)
            {
                // add ember particles
                particleList.Add(GameObject.Instantiate(emberParticles, level));
                particleList.Last<GameObject>().transform.localPosition = new Vector3(xPosition, particleYPosition, particleZPosition);

                // add ash particles
                particleList.Add(GameObject.Instantiate(ashParticles, level));
                particleList.Last<GameObject>().transform.localPosition = new Vector3(xPosition, particleYPosition, particleZPosition);
            }

            // if the last two columns were bad, add a good column
            if(!lastColumnIsGood && !secondLastColumnIsGood)
            {
                columnList.Add(GameObject.Instantiate(goodColumn, level));
                UpdatePreviousColumns(true);
            }
            else
            {
                // generate random number to determine good/bad column
                int randNumber = Random.Range(0, 100);

                if(randNumber > difficulty)
                {
                    // add a bad column
                    columnList.Add(GameObject.Instantiate(badColumn, level));
                    UpdatePreviousColumns(false);
                }
                else
                {
                    // add a good column
                    columnList.Add(GameObject.Instantiate(goodColumn, level));
                    UpdatePreviousColumns(true);
                }
            }
        }

        // the last column in the list must be a good column
        xPosition += columnWidth;
        columnList.Add(GameObject.Instantiate(goodColumn, level));

        // loop through list and translate each column to correct position
        for(int i = 0; i < length; i++)
        {
            columnList.ElementAt<GameObject>(i).transform.localPosition = new Vector3(columnWidth * i, 0, 0);
        }
    }

    // Updates the good/bad status of the last two columns
    void UpdatePreviousColumns(bool latestColumn)
    {
        secondLastColumnIsGood = lastColumnIsGood;
        lastColumnIsGood = latestColumn;
    }
}
