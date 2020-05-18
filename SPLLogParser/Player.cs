using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SPLLogParser
{
  
  public class Player 
  {
    public string name;
    public string team;
    public int goal;
    public int assist;
    public int shot;
    public int save;
     
    public Player()
    {
      this.name = "";
      this.team = "";
      this.goal = 0;
      this.assist = 0;
      this.shot = 0;
      this.save = 0;
    } 
    
    public Player(string nameValue, string teamValue, int goalValue, int assistValue, int shotValue, int saveValue)
    {
      this.name = nameValue;
      this.team = teamValue;
      this.goal = goalValue;
      this.assist = assistValue;
      this.shot = shotValue;
      this.save = saveValue;
    }

   

    public void addValues(int goalValue, int assistValue, int shotValue, int saveValue)
    {
      this.goal += goalValue;
      this.assist += assistValue;
      this.shot += shotValue;
      this.save += saveValue;
    }
  }
  
}
