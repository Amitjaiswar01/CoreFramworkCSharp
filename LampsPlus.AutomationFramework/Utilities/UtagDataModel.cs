namespace LampsPlus.AutomationFramework.Utilities
{
    public class UtagDataModel
    {
        public override string ToString()
        {
            return ProductCategory + TestId + MmId + FormulaId + PinId + TestStartDate + TestCompositionId + FilterId;
        }
        public string ProductCategory { get; set; }
        public string TestId { get; set; }
        public string MmId { get; set; }
        public string FormulaId { get; set; }
        public string PinId { get; set; }
        public string TestStartDate { get; set; }
        public string TestCompositionId { get; set; }
        public string FilterId { get; set; }
    }
}