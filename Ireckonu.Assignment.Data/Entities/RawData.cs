namespace Ireckonu.Assignment.Data.Entities
{
    public class RawData : BaseEntity
    {
        public string Key { get; set; }
        public string ArtikelCode { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DeliveredIn { get; set; }
        public string Q1 { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }

        public void FromDelimitedString(string delimetedString, char seperator)
        {
            var splittedRow = delimetedString.Split(seperator);

            Key = splittedRow[0];
            ArtikelCode = splittedRow[1];
            ColorCode = splittedRow[2];
            Description = splittedRow[3];
            Price = decimal.Parse(splittedRow[4]);
            DiscountPrice = decimal.Parse(splittedRow[5]);
            DeliveredIn = splittedRow[6];
            Q1 = splittedRow[7];
            Size = splittedRow[8];
            Color = splittedRow[9];
        }
    }
}