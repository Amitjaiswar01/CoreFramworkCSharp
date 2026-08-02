using System;
using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Contains details about a given product.
    /// NOTE: All properties may not be available at any given time. Properties can be set as needed from information from the database.
    /// </summary>
    public class ProductModel
    {
        public bool IsClearance { get; set; }

        public DateTime SaleEndDate { get; set; }

        public decimal ComparePrice { get; set; }
        public decimal Difference { get; set; }
        public decimal FreightCharge { get; set; }
        public decimal InitialRetailPrice { get; set; }
        public decimal Price { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal RetailPrice58 { get; set; }
        public decimal RetailPriceInternet { get; set; }
        public decimal SpecialDiscount { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SalePrice1Internet { get; set; }
        public decimal SalePrice1 { get; set; }
        public decimal Savings { get; set; }
        public decimal TradePrice { get; set; }
        public decimal YourSavings { get; set; }
        public decimal StrikeThroughPrice { get; set; }

        public int FirstShipDays { get; set; }
        public int LastShipDays { get; set; }
        public int PatternIdTotal { get; set; }
        public int ServiceLevel { get; set; }
        public int EmployeeNumber { get; set; }
        public int Quantity { get; set; }
        public int RelationShipGroupIdString { get; set; }

        public string BaseSku { get; set; }
        public string BuildFullSystemSkus { get; set; }
        public string CallOut { get; set; }
        public string Category { get; set; }
        public string CurrentInventory { get; set; }
        public string Finish { get; set; }
        public string LocationAddress { get; set; } 
        public string LocationCity { get; set; }
        public string LocationNumber { get; set; } 
        public string LocationPhone { get;  set; } 
        public string LocationSms { get; set; }
        public string LocationStoreName { get; set; }
        public string LocationState { get; set; } 
        public string LocationZip { get; set; } 
        public string PrimarySku { get; set; }
        public string ProductName { get; set; }
        public string ShortSku { get; set; }
        public string SortUrl { get; set; }
        public string Style { get; set; }
        public string SkuStatus { get; set; }
        public string Type { get; set; }
        public string Usage { get; set; }
        public string ParentSkuString { get; set; }
        public string WarehouseInventory { get; set; }
        public string Callout { get; set; }
        public DateTime EndSale { get; internal set; }

        /// <summary>
        /// Build Full System products available for this product.
        /// </summary>
        public List<BuildFullSystemProductModel> BuildFullSystemProducts;

        /// <summary>
        /// Contains details about a given product.
        /// NOTE: All properties may not be available at any given time. Properties can be set as needed from information from the database.
        /// </summary>
        public ProductModel()
        {
            BuildFullSystemProducts = new List<BuildFullSystemProductModel>();
            ArProducts = new List<ArProductModel>();
        }

        /// <summary>
        ///AR products.
        /// </summary>
        public List<ArProductModel> ArProducts;
    }
}
