namespace ppedv.GMV.Model
{
    public class Gerät : Entity
    {
        public string Modell { get; set; }
        public string Hersteller { get; set; }
        public string Typ { get; set; }

        //public int Wichtig { get; set; }

        public virtual ICollection<Messung> Messungen { get; set; } = new HashSet<Messung>();
    }


}