using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This level generator class generates and returns a list of tiles based on the
// difficulity and required length
public class LevelGenerator 
{

    // Reference to the Different base tile prefabs 
    public GameObject goodTile;
    public GameObject badTile;

    // These varialbes are for checking the previous objects generated
    // Set to true to handle the edge case at the start of the loop
    private bool good1 = true;
    private bool good2 = true;

    // This function generates a level based on the difficulty specified and updates the time/returns the time required
    // to complete the level.
    // length - This is the length of the level in tiles
    // time - float to be updated in the function to reflect the new level
    // difficulty - value between 0 and 100 (0% and 100%) for the chance of spawning bad when can
    public List<GameObject> GenerateLevel(int length, int difficulty = 10)
    {

        List<GameObject> generatedLevel = new List<GameObject>();

        // This first object in the list must be a good tile to reach the end
        generatedLevel.Add(GameObject.Instantiate(goodTile));

        // Next based on the difficulty randomly calculate the level to generate
        // length - 2 to account for the start and finish must be safe
        for(int i = 1; i < (length - 2); i++)
        {

            // If there are 2 bad for a good tower
            if(!good1 && !good2)
            {
                generatedLevel.Add(GameObject.Instantiate(goodTile));
                UpdatePreviousGoods(true);
            }
            else
            {
                // Get a random number to check if there should be a bad tile 
                int randNumber = Random.Range(0, 100);

                if(randNumber > difficulty)
                {
                    generatedLevel.Add(GameObject.Instantiate(badTile));
                    UpdatePreviousGoods(false);
                }
                else
                {
                    generatedLevel.Add(GameObject.Instantiate(goodTile));
                    UpdatePreviousGoods(true);
                }
            }

        }

        // Add a good for the end
        generatedLevel.Add(GameObject.Instantiate(goodTile));

        // Return the completed level
        return generatedLevel;

    }


    // Simple function to update the good flags based on the latest 
    void UpdatePreviousGoods(bool latestTileType)
    {
        good2 = good1;
        good1 = latestTileType;
    }


}
