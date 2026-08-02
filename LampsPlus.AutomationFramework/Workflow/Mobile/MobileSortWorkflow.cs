using System;
using System.Collections.Generic;
using System.Linq;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Workflow.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Workflow.Mobile
{
    /// <summary>
    /// Common behavior for sort pages.
    /// </summary>
    public class MobileSortWorkflow : SortWorkflowBase
    {
        /// <inheritdoc />
        public MobileSortWorkflow(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public override void VisitMostPopularLampProductThatHasQuestionsAndAnswers()
        {
            var lampSortPages = new List<string>
            {
                Urls.TableLampsSortPageUrl,
                Urls.FloorLampsSortPageUrl,
                Urls.DeskLampsSortPageUrl,
                Urls.LampShadesSortPageUrl
            }.Select(url => $"{url}mp_most-popular/").OrderBy(x => Guid.NewGuid()).ToList();

            for (var x = 0; x < lampSortPages.Count; x++)
            {
                Browser.Navigate(lampSortPages[x]);
                TestsBase.Sort.ListOfProductLinksSortPage[0].Click();
                Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));
                if (TestsBase.ProductDetail.IsSingleQuestionAndAnswerElementVisible) { break; }
            }
        }
    }
}
