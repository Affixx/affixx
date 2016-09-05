namespace Affixx.Core.Database.Generated
{
    public partial class Document
    {
        public bool IsUploadedByMe { get; set; }
        public bool IsPurchasedByMe { get; set; }
        public string UploaderName { get; set; }
        public int Rating { get; set; }

        public decimal Price
        {
            get
            {
                switch(Year)
                {
                    case "BA/BSc Year 1":
                    case "BA/BSc Year 2":
                        return 3.99m;
                    case "BA/BSc Year 3":
                        return 4.99m;
                    case "BA/BSc Year 4":
                    case "BA/BSc Year 5":
                    case "BA/BSc Year 6":
                        return 5.99m;
                    case "MA/MSc":
                        return 6.99m;
                    case "MBA":
                        return 7.99m;
                    default:
                        return 2.99m;
                }                
            }
        }
    }
}
