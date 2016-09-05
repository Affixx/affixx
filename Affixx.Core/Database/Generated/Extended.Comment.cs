namespace Affixx.Core.Database.Generated
{
    public partial class Comment
    {
        public string Type { get; set; } //reuse for different activities - refactor later
        public bool IsCreatedByMe { get; set; } //reuse for different activities - refactor later
        public string UserName { get; set; }
    }
}
