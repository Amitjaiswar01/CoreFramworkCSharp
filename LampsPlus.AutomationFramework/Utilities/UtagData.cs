using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace LampsPlus.AutomationFramework.Utilities
{
    public class UtagData
    {
        public static string FilterIdLiteral = "filter_id";
        public static string FormulaIdLiteral = "formula_id";
        public static string MmIdLiteral = "mm_id";
        public static string PinIdLiteral = "pin_id";
        public static string TestCompositionIdLiteral = "testcomposition_id";
        public static string TestIdLiteral = "test_id";
        public static string TestStartDateLiteral = "test_start_date";
        public static string ProductCategoryLiteral = "product_category";

        public static UtagDataModel ParseUtagData(string inputText)
        {
            var matches = Regex.Matches(inputText, "var utag_data =({.*?});", RegexOptions.Singleline);
            var utagJson = matches[0].Groups[1].ToString();
            var utagData = JObject.Parse(utagJson);

            var returnData = new UtagDataModel()
            {
                FilterId = utagData.SelectToken(FilterIdLiteral).ToString(),
                ProductCategory = utagData.SelectToken(ProductCategoryLiteral).ToString(),
                FormulaId = utagData.SelectToken(FormulaIdLiteral).ToString(),
                MmId = utagData.SelectToken(MmIdLiteral).ToString(),
                PinId = utagData.SelectToken(PinIdLiteral).ToString(),
                TestCompositionId = utagData.SelectToken(TestCompositionIdLiteral).ToString(),
                TestId = utagData.SelectToken(TestIdLiteral).ToString(),
                TestStartDate = utagData.SelectToken(TestStartDateLiteral).ToString()
            };

            return returnData;
        }
    }
}