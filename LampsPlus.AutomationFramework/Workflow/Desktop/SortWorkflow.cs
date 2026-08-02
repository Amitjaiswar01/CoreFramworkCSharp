using System;
using System.Collections.Generic;
using System.Linq;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Workflow.Base;

namespace LampsPlus.AutomationFramework.Workflow.Desktop
{
    /// <summary>
    /// Common behavior for sort pages.
    /// </summary>
    public class SortWorkflow : SortWorkflowBase
    {
        public SortWorkflow(TestsBase testsBase) : base(testsBase) { }

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
                TestsBase.Sort.ProductDescriptionLinksElement.Click();
                Browser.Wait.ForClickableElement(GlobalLocators.AddToCartButton);
                if (TestsBase.ProductDetail.IsSingleQuestionAndAnswerElementVisible) { break; }
            }
        }
    }
}
