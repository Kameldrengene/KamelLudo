using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerData
{
    public string Id { get; set; }

    private string _name;

    public PlayerData(string name)
    {
        this._name = name;
    }
    public PlayerData() { }

    public string Name
    {
        get { return _name; }
        set { this._name = value; }
    }

    public string password { get; set; }
}

