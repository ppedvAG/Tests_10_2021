namespace ppedv.GMV.Model
{
    public class Messung : Entity
    {
        public DateTime Datum { get; set; }
        public string Beschreibung { get; set; }
        public virtual ICollection<Messwert> Messwerte { get; set; } = new HashSet<Messwert>();
        public virtual Gerät Gerät { get; set; }
    }


}