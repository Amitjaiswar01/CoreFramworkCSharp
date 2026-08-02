namespace Automation.Framework
{
    public interface IBrowserIos
    {
        void SetSafariAddressBarAtTheBottom();

        void EnableGeoLocation(double latitude, double longitude);
    }
}