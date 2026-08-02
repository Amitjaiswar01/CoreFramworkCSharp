using System.Linq;
using System.Web.UI;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Stores
{
    public class StoresPageLocatorDesktopTest : StoresPageLocatorTest
    {
        public StoresPageLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Stores")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateStoresElementsTest(string config) => Locate(config);

        protected override void VerifyStorePageElements()
        {
			var MakeThisMyStoreText = "MAKE THIS MY STORE";
			var MakeThisMyStoreClass = "makeThisMyStore";

			VerifyElementDisplayed(() => Stores.LampsPlusStoreRegionLinks);
            VerifyElementDisplayed(() => Stores.BopusSubmenu);
            VerifyElementDisplayed(() => Stores.NearByZipStores);
            VerifyElementDisplayed(() => Stores.StoreZipCodeInputElement);
            VerifyElementDisplayed(() => Stores.StorePickerSubmitElement);

            //Navigate to region stores result page
            ElementActions.SelectRandom(Stores.LampsPlusStoreRegionLinks).Click();

            // Select another random region except the current one you are on
            ElementActions.SelectRandom(Stores.AllStoresLampsPlusLinks.Where(x => TextActions.NormalizeUrl(x.GetAttribute(HtmlTextWriterAttribute.Href.ToString())) != Browser.PageUrl)).Click();

            VerifyElementDisplayed(() => Stores.AllStoresLampsPlusLinks);
            VerifyElementDisplayed(() => Stores.StoreResults);
            VerifyElementDisplayed(() => Stores.AddressLocalityField);
            VerifyElementDisplayed(() => Stores.AllStoresLampsPlus);
            Stores.SelectedStoreElement = Stores.RandomStoreElement;

            VerifyElementDisplayed(() => Stores.SelectedStoreDetailsName);
            VerifyElementDisplayed(() => Stores.SelectedStoreDetailsLink);
            VerifyElementDisplayed(() => Stores.RandomStoreElement);
            VerifyElementDisplayed(() => Stores.StoreDetailsRegionLinks);
            VerifyElementDisplayed(() => Stores.SelectedStoreElement);
            var buttonText = Stores.SelectedStoreElement.FindElement(By.ClassName(MakeThisMyStoreClass)).Text;
            bool isStoreSelected = buttonText == MakeThisMyStoreText ? false : true;
            while (isStoreSelected)
            {
                if(Stores.StoreDetailsRegionLinks.Count < 2)
                {
                    //If there was no store that was not the current store Select another random region except the current one you are on
                    ElementActions.SelectRandom(Stores.AllStoresLampsPlusLinks.Where(x => TextActions.NormalizeUrl(x.GetAttribute(HtmlTextWriterAttribute.Href.ToString())) != Browser.PageUrl)).Click();
                }
                Stores.SelectedStoreElement = Stores.RandomStoreElement;
                buttonText = Stores.SelectedStoreElement.FindElement(By.ClassName(MakeThisMyStoreClass)).Text;
                isStoreSelected = buttonText == MakeThisMyStoreText ? false : true;
            }

            //Navigate to store detail page
            Stores.SelectedStoreDetailsLink.Click();
            
            Browser.Wait.ForDomReady();
            VerifyElementDisplayed(() => Stores.MakeThisMyStoreContainer);
            VerifyElementDisplayed(() => Stores.MakeThisMyStoreButton);

            Stores.MakeThisMyStoreButton.Click();

            VerifyElementDisplayed(() => Stores.MyStoreButton);
            VerifyElementDisplayed(() => Stores.AddressRegionField);
            VerifyElementDisplayed(() => Stores.PostalCodeField);
            VerifyElementDisplayed(() => Stores.StreetAddressField);
            VerifyElementDisplayed(() => Stores.GetDirectionsButton);
            VerifyElementDisplayed(() => Stores.OpenNow);

            Browser.MouseOverOnElement(Stores.BopusSubmenu, Stores.BopusSubmenu);
            Browser.Wait.ForDisplayedElement(Stores.MyStoreWrapper);

            VerifyElementDisplayed(() => Stores.MyStoreWrapper);

            //VerifyElementNotImplemented
            VerifyElementNotImplemented(() => Stores.StoreDetailBtns);
            VerifyElementNotImplemented(() => Stores.LpIconCallElement);
            VerifyElementNotImplemented(() => Stores.LpIconDirectionsElement);
            VerifyElementNotImplemented(() => Stores.LpIconCalanderElement);
            VerifyElementNotImplemented(() => Stores.LpIconCouponElement);
            VerifyElementNotImplemented(() => Stores.SelectStoreNearMeLinks);
            VerifyElementNotImplemented(() => Stores.StorePhotosImgElement);
            VerifyElementNotImplemented(() => Stores.RandomStoreNearMeElement);
            VerifyElementNotImplemented(() => Stores.StoreNearMeLinks);
        }
    }

    public class StoresPageLocatorMobileTest : StoresPageLocatorTest
    {
        public StoresPageLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Stores")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateStoresElementsTest(string config) => Locate(config);

        protected override void VerifyStorePageElements()
        {
			var MakeThisMyStoreText = "Make this my store";

            Stores.SelectedStoreElement = Stores.RandomStoreNearMeElement;
            
            VerifyElementDisplayed(() => Stores.StoreNearMeLinks);
            VerifyElementDisplayed(() => Stores.RandomStoreNearMeElement);
            VerifyElementDisplayed(() => Stores.SelectStoreNearMeLinks);
            VerifyElementDisplayed(() => Stores.AllStoresLampsPlus);
            VerifyElementDisplayed(() => Stores.MakeThisMyStoreContainer);
            VerifyElementDisplayed(() => Stores.SelectedStoreDetailsLink);
            VerifyElementDisplayed(() => Stores.SelectedStoreElement);
            var buttonText = Stores.SelectedStoreElement.Text;
            bool isStoreSelected = buttonText.Contains(MakeThisMyStoreText) ? false : true;

            while (isStoreSelected)
            {
                if (Stores.StoreNearMeLinks.Count < 2)
                {
                    //If there was no store that was not the current store Select another random region except the current one you are on
                    ElementActions.SelectRandom(Stores.AllStoresLampsPlusLinks.Where(x => TextActions.NormalizeUrl(x.GetAttribute(HtmlTextWriterAttribute.Href.ToString())) != Browser.PageUrl)).Click();
                }
                Stores.SelectedStoreElement = Stores.RandomStoreNearMeElement;
                buttonText = Stores.SelectedStoreElement.Text;
                isStoreSelected = buttonText.Contains(MakeThisMyStoreText) ? false : true;
            }

            //Navigate to store detail page
            Stores.SelectedStoreDetailsLink.Click();
            Browser.Wait.ForDomReady();

            //Navigate to region stores result page
            VerifyElementDisplayed(() => Stores.LampsPlusStoreRegionLinks);
            VerifyElementDisplayed(() => Stores.AllStoresLampsPlusLinks);
            Browser.GoBack();
            Browser.Wait.ForDomReady();

            VerifyElementDisplayed(() => Stores.StoreZipCodeInputElement);
            VerifyElementDisplayed(() => Stores.StorePickerSubmitElement);

            Stores.StoreZipCodeInputElement.SendKeys("91311");
            Browser.Wait.ForClickableElement(Stores.StorePickerSubmitElement).Click();
            VerifyElementDisplayed(() => Stores.StoreDetailBtns);
            VerifyElementDisplayed(() => Stores.StoreResults);
            Browser.Wait.ForClickableElement(Stores.StoreDetailBtns[0]).Click();

            VerifyElementDisplayed(() => Stores.AddressLocalityField);
            VerifyElementDisplayed(() => Stores.StorePhotosImgElement);
            VerifyElementDisplayed(() => Stores.AddressRegionField);
            VerifyElementDisplayed(() => Stores.PostalCodeField);
            VerifyElementDisplayed(() => Stores.StreetAddressField);
            VerifyElementDisplayed(() => Stores.MakeThisMyStoreButton);
            VerifyElementDisplayed(() => Stores.LpIconCallElement);
            VerifyElementDisplayed(() => Stores.LpIconDirectionsElement);
            VerifyElementDisplayed(() => Stores.GetDirectionsButton);
            VerifyElementDisplayed(() => Stores.LpIconCalanderElement);
            VerifyElementDisplayed(() => Stores.LpIconCouponElement);
            VerifyElementDisplayed(() => Stores.OpenNow);

            Stores.MakeThisMyStoreButton.Click();

            VerifyElementDisplayed(() => Stores.MyStoreButton);

            //VerifyElementNotImplemented
            VerifyElementNotImplemented(() => Stores.BopusSubmenu);
            VerifyElementNotImplemented(() => Stores.MyStoreWrapper);
            VerifyElementNotImplemented(() => Stores.SelectedStoreDetailsName);
            VerifyElementNotImplemented(() => Stores.RandomStoreElement);
            VerifyElementNotImplemented(() => Stores.StoreDetailsRegionLinks);
            VerifyElementNotImplemented(() => Stores.NearByZipStores);
        }
    }

    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "Stores")]
    public abstract class StoresPageLocatorTest : PageObjectTestsBase
    {
        protected StoresPageLocatorTest(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            InitializeFramework(config, Urls.StoresPageUrl);
            BuildElementsList(Stores);

            VerifyStorePageElements();
        }

        protected abstract void VerifyStorePageElements();
    }
}