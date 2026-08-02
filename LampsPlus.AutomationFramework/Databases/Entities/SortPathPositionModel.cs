namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Sort Details for Item
    /// </summary>
    public class SortPathPositionModel
    {
        public int LineNumber { get; set; }
        public int SortPathId { get; set; }
        public int SortPosition { get; set; }

        public string ShortSku { get; set; }
        public string SortPath { get; set; }
    }
}
