using System;
using System.Data;
using System.IO;
using System.Collections.Generic;


namespace SPLLogParser
{
  class Program
  {
    static DataTable masterTable = new DataTable();
    
    static List<Player> playerArray = new List<Player>();
    static List<int> redPeriodScoreList = new List<int>();
    static List<int> bluePeriodScoreList = new List<int>();
    static int periodIndex = 0;

    static void Main(string[] args)
    {

      Console.SetWindowSize(165, 35);
      Console.Title ="Log Parser 3.0";
      
      string[] filePaths = Directory.GetFiles(@".\Logs\", "*.tsv",
                                         SearchOption.TopDirectoryOnly);
      for (int m = 0; m < filePaths.Length; m++)
      {
        Console.WriteLine(filePaths[m]);
        redPeriodScoreList.Add(0);
        bluePeriodScoreList.Add(0);
        Parse(@filePaths[m]);
        periodIndex += 1;
      }
      Console.WriteLine();
      if(filePaths.Length > 3)
      {
        Console.WriteLine("You have more than 3 logs in the Logs folder\n");
      } else if (filePaths.Length < 3)
      {
        Console.WriteLine("You have less than 3 logs in the Logs folder\n");
      }
      playerArray.Sort(delegate (Player c1, Player c2) { return c1.name.CompareTo(c2.name); });
      SortPlayerList();
      PrintResults();
      CalcScore();
      Console.Write("Press enter to close");
      Console.ReadLine();
    }

    
    static void Parse(string FileName)
    {
      
      DataTable dt = new DataTable();
      using (TextReader tr = File.OpenText(FileName))
      {
        string line;
        while ((line = tr.ReadLine()) != null)
        {
          string[] items = line.Split('\t');
          if (dt.Columns.Count == 0)
          {
            // Create the data columns for the data table based on the number of items
            // on the first line of the file
            for (int i = 0; i < items.Length; i++)
              dt.Columns.Add(new DataColumn("Column" + i, typeof(string)));
          }
          dt.Rows.Add(items);
          
            if (dt.Rows.Count > 1)
            {
              Player playerHolder = new Player();
              playerHolder.name = items[0];
              playerHolder.goal = int.Parse(items[3]);
              playerHolder.assist = int.Parse(items[4]);
              playerHolder.shot = int.Parse(items[5]);
              playerHolder.save = int.Parse(items[6]);
              playerHolder.team = items[7];

            if (playerHolder.team == "red")
            {
              redPeriodScoreList[periodIndex] += playerHolder.goal;
            }
            else
            {
              bluePeriodScoreList[periodIndex] += playerHolder.goal;
            }

            if (PlayerExists(playerHolder))
              {
                //Find correct player index
                for (int p = 0; p < playerArray.Count; p++)
                {
                if (playerArray[p].name == playerHolder.name)
                {
                  playerArray[p].addValues(playerHolder.goal, playerHolder.assist, playerHolder.shot, playerHolder.save);
                  
                }

                

                }

            } else
            {
              playerArray.Add(playerHolder);
              
            }
            }
        }
      }
    }

    

    static void PrintResults()
    {
      for (int j = 0; j < playerArray.Count; j++)
      {
        string tabSpace = "\t\t";
        Console.WriteLine("Name({0}): {1,-25}\t" +  "  Goals: " +  playerArray[j].goal + 
          tabSpace + "Assists: " + playerArray[j].assist + tabSpace + "Shots: " + playerArray[j].shot + tabSpace + "Saves: " + playerArray[j].save, playerArray[j].team, playerArray[j].name);
      }
    }

    static void CalcScore()
    {
      int redScore = 0;
      int blueScore = 0;
      for (int j = 0; j < playerArray.Count; j++)
      {
        if(playerArray[j].team == "red")
        {
          redScore += playerArray[j].goal;
        } else if(playerArray[j].team == "blue")
        {
          blueScore += playerArray[j].goal;
        }
      }

      Console.WriteLine("---------------------------------");
      for(int c = 0; c < periodIndex; c++)
      {
        Console.WriteLine("Period {0} Score: Red - " + redPeriodScoreList[c] + "    Blue - " + bluePeriodScoreList[c], c+1);
      }
      Console.WriteLine("---------------------------------");
      Console.WriteLine("Final Score: Red - " + redScore + "    Blue - " + blueScore);
      Console.WriteLine("Double check with submitted score.  If a player leaves early these scores will not be correct");
    }

    static Boolean PlayerExists(Player passedPlayer)
    {
      Boolean returnValue = false;

      for(int j = 0; j < playerArray.Count; j++)
      {
        if (playerArray[j].name == passedPlayer.name)
        {
          returnValue = true;
          
        }
      }

      return returnValue;
    }

    static void SortPlayerList()
    {
      List<Player> RedPlayerArray = new List<Player>();
      List<Player> BluePlayerArray = new List<Player>();
      
      for ( int j = 0; j < playerArray.Count; j++)
      {
        if (playerArray[j].team == "red")
        {
          RedPlayerArray.Add(playerArray[j]);
        } else if (playerArray[j].team == "blue")
        {
          BluePlayerArray.Add(playerArray[j]);
        }
      }

      playerArray.Clear();
      for (int a = 0; a < RedPlayerArray.Count; a++)
      {
        playerArray.Add(RedPlayerArray[a]);
      }
      for (int a = 0; a < BluePlayerArray.Count; a++)
      {
        playerArray.Add(BluePlayerArray[a]);
      }
    }

  }
}
