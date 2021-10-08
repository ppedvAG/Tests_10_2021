namespace ppedv.GMV.Model
{
    public class Messwert : Entity
    {
        public string? MesswertBeschreibung { get; set; }
        public decimal? Wert { get; set; }
        public string? Einheit { get; set; }
        public virtual Messung? Messung { get; set; }
    }


}