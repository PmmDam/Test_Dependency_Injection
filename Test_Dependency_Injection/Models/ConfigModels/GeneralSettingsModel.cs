using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test_Dependency_Injection.Models.ConfigModels
{
    public class GeneralSettingsModel
    {

        public string User { get; set; }
        public string Password { get; set; }
        public bool LogEnabled{ get; set; }
        

        public enum Environments
        {
            None,
            Development,
            Staging,
            Production
        }
        
        private Environments _actualEnvironment { get; set; }
        [JsonIgnore]
        public Environments RawActualEnvironemt
        {
            get
            {
                return this._actualEnvironment;
            }
            set
            {
                this._actualEnvironment = value;    
            }
        }
        public string ActualEnvironment
        {
            get
            {
                return Enum.GetName(this._actualEnvironment);
            }

            set
            {
                Environments result;

                if(Enum.TryParse<Environments>(value, out result))
                {
                    //this._actualEnvironment = (Environments)Enum.Parse(typeof(Environments), value);
                    this._actualEnvironment = result;
                }
                else
                {
                    this._actualEnvironment = Environments.None;
                }
                
            }
        }

    }
}
