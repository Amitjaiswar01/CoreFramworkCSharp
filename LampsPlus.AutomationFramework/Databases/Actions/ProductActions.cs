using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Databases.Queries.PricingBlock;
using LampsPlus.AutomationFramework.Databases.Queries.ProductDetail;
using LampsPlus.AutomationFramework.Databases.Queries.SortCallout;
using LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders;
using LampsPlus.AutomationFramework.Enums;

using ProductModel = LampsPlus.AutomationFramework.Databases.Entities.ProductModel;
using BrandModel = LampsPlus.AutomationFramework.Databases.Entities.BrandModel;

namespace LampsPlus.AutomationFramework.Databases.Actions
{
    /// <summary>
    /// Helper to provide access to product related database queries.
    /// </summary>
    public class ProductActions
    {
        #region SQL Strings
        private const string AtEmailString = "@email";
        private const string AtShortSkuString = "@shortSku";
        private const string AtSourceIdString = "@sourceId";
        private const string AtSubLocationCodeString = "@sublocationcode";
        private const string AtFirstShipDaysString = "@firstshipdays";
        private const string BuildFullSystemSkusString = "BuildFullSystemSKUs";
        private const string CallOutString = "CallOut";
        private const string CopyString = "copy";
        private const string CurrentInventoryString = "CurrentInventory";
        private const string IsDecrementableString = "isdecrementable";
        private const string PrimarySkuString = "PrimarySku";
        private const string ShortSkuString = "ShortSku";
        private const string ParentSkuString = "ParentSku";
        private const string SourceString = "source";
        private const string SourceIdString = "sourceId";
        #endregion

        /// <summary>
        /// CartEasy database connection string.
        /// </summary>
        public string CartEasyConnectionString { get; }
        public string ProductMicroservicesConnectionString { get; }

        /// <summary>
        /// Products database connection string.
        /// </summary>
        public string ProductsConnectionString { get; }

        /// <summary>
        /// Helper to provide access to product related database queries.
        /// </summary>
        public ProductActions(string cartEasyConnectionString, string productsConnectionString, string productMicroservicesConnectionString)
        {
            CartEasyConnectionString = cartEasyConnectionString;
            ProductsConnectionString = productsConnectionString;
            ProductMicroservicesConnectionString = productMicroservicesConnectionString;
        }

        public int GetEmailProductRecipient(string email) => (int)ParameterValue(AtEmailString, email, SourceIdString, Queries.ProductDetail.EmailProductRecipient.Query);

        public string Get16PlusColorsCallOutByShortSku(string shortSku) => (string)ParameterValue(AtShortSkuString, shortSku, CallOutString, Queries.SortCallout.SixteenPlusColorsCalloutByShortSku.Query);

        public string GetAnySkuWithProductDetailPage => ShortSku(AnySkuWithProductPage.Query);

        public string GetSkuWithPriceLessThan30 => ShortSku(Queries.ProductDetail.GetSkuWithPriceLessThan30.Query);

        public string GetSkuWithPriceBetween30And1350 => ShortSku(Queries.ProductDetail.GetSkuWithPriceBetween30And1350.Query);

        public string GetSkuWithPriceMoreThan1500 => ShortSku(Queries.ProductDetail.GetSkuWithPriceMoreThan1500.Query);

        public string GetAnySkuBetweenTenAndTwentyDollars => ShortSku(Queries.ProductDetailSpecificProduct.AnySkuBetweenTenAndTwentyDollars.Query);

        public string GetAnySkuWithRelatedVideos => ShortSku(AnySkuWithRelatedVideos.Query);

        public string GetBopusEligibleSku => ShortSku(FindBopusEligibleSku.Query);

        public string GetCallOutByShortSku(string shortSku) => ShortSkuParameterValue(shortSku, CallOutString, Queries.SortCallout.OneHundredPlusColorsShortSku.Query);

        public string GetCallToOrderSku => ShortSku(CallToOrderSku.Query);

        public string GetColorPlusSku => ShortSku(Queries.ProductDetailSpecificProduct.ColorPlusSku.Query);

        public string GetFanWithEnergyGuideIconShortSku => ShortSku(FanWithEnergyGuideIconShortSku.Query);

        public string GetFreeShippingAndReturnShortSkus => ShortSku(Queries.ProductDetailCallout.FreeShippingAndReturnShortSkus.Query);

        public string GetItemNotOnSale => ShortSku(Queries.ShoppingCart.GetItemNotOnSale.Query);

        public string GetItemWithExpeditedShippingMoreThan3Days => ShortSku(Queries.ShoppingCart.ItemWithExpeditedShippingMoreThan3Days.Query);

        public string GetLessThanTenDollarItem => ShortSku(Queries.ShoppingCartEmployeeCan.LessThanTenDollarItem.Query);

        public string GetLincCompatibleProduct => ShortSku(FindLincCompatibleProduct.Query);

        public string GetMultiProductShortSku => ShortSku(MultiProductShortSku.Query);

        public string GetProductNotAvailableShortSku => ShortSku(ProductNotAvailableSku.Query);

        public string GetProductShortSkuWithZone3Shipping => ShortSku(Queries.Shipping.ProductShortSkuWithZone3Shipping.Query);

        public string GetProMemberSpecialPriceDiscountCallOutShortSku => ShortSku(Queries.ProductDetailCallout.ProMemberSpecialPriceDiscontCallOutShortSku.Query);

        public string GetRandomComboKitSku => ShortSku(Queries.ProductDetailSpecificProduct.FindRandomComboKitSku.Query);

        public string GetRandomSoldOutItemSku => ShortSku(Queries.SortCallout.RandomSoldOutItemShortSku.Query);

        public string GetShipsFreeOnOrdersOver49CallOutShortSku => ShortSku(Queries.ProductDetailCallout.ShipsFreeOnOrdersOver49CallOutShortSku.Query);

        public string GetShipsFreeWithinStateShortSku => ShortSku(Queries.ProductDetailCallout.ShipsFreeWitinState.Query);

        public string GetShortSkuOnClearance => ShortSku(Queries.ProductDetailCallout.ShortSkuOnClearance.Query);

        public string GetShortSkuThatHasRelatedProducts => ShortSku(ShortSkuThatHasRelatedProducts.Query);

        public string GetShortSkuThatMeetsMinimumOrder => ShortSku(Queries.Shipping.ShortSkuThatMeetsMinimumOrder.Query);

        public string GetShortSkuWithUmrp => ShortSku(Queries.ShoppingCartEmployee.ShortSkuWithUmrp.Query);

        public string GetShortSkuWithPhoneNumberCallToOrderCallout => ShortSku(Queries.ProductDetailCallout.ShortSkuWithPhoneNumberCallToOrderCallout.Query);

        public string GetSingleSkuBathroomLighting => ShortSku(SingleSkuBathroomLighting.Query);

        public string GetSkuBetweenTenAndTwentyDollars => ShortSku(FindSkuBetweenTenAndTwentyDollars.Query);

        public string GetSkuForPricingBlock => ShortSku(PricingBlockSku.Query);

        public string GetSkuGreaterThanTwoHundredDollars => ShortSku(FindSkuGreaterThanTwoHundredDollars.Query);

        public string GetSkuForSaleEndsInCallout => ShortSku(Queries.ShoppingCart.ShortSkuQualifiedForSaleEndsInCallout.Query);

        public string GetSkuForSaleCallout => ShortSku(ProductSalesData.Query);

        public string GetSkuThatHasSpecificationsTables => ShortSku(SkuThatHasSpecificationTable.Query);

        public string GetSkuThatHasGoodToKnowIcons => ShortSku(GetSkuThatHasGoodToKnowIconsLabel.Query);

        public string GetSkuThatHasHousingOptions => ShortSku(SkuThatHasHousingOptions.Query);

        public string GetSkuThatHasArOption => ShortSku(Queries.ProductDetail.GetSkuThatHasArOption.Query);

        public string GetSkufor2DRoom => ShortSku(SkuFor2DRoom.Query);

        public string GetSkuThatHasQuantityGreaterThanTwenty => ShortSku(Queries.AddingToCartAndWishList.SkuThatHasQuantityGreaterThanTwenty.Query);

        public string GetSkuThatIsLessThanTwoHundredDollars => ShortSku(AnySkuLessThanTwoHundredDollars.Query);

        public string GetSkuThatQualifiesForReviews => ShortSku(SkuThatQualifiesForReviews.Query);
        public string GetSkuForFinishAndColorRelationshipWidget => ShortSku(SkuForFinishAndColorRelationshipWidget.Query);

        public string GetSkuWithShipInOption => ShortSku(SkuThatQualifyShipInOption.Query);
        
        public string GetSkuWithViewInRoomOnPdp => ShortSku(FindSkuWithViewInRoomOnPDP.Query);

        public string GetTiffanyColorPlusShortSku => ShortSku(TiffanyColorPlusSku.Query);
        public string GetOpenBoxShortSku => ShortSku(Queries.AddingToCartAndWishList.OpenBoxItemSku.Query);
        public string GetAugmentedReality2DAnd3DSku => ShortSku(AugmentedReality2DAnd3DSku.Query);
        public string GetCollageSku => ShortSku(Queries.ProductDetail.CollageRetailPriceAndProductName.Query);

        public ProductModel GetResidentialSaleProduct => SaleSfpAndPla(ResidentialProductSkuOnSaleSfpAndPla.Query);

        public ProductModel GetLampsPlusChoiceSku()
        {
            ProductModel LampsPlusChoiceProduct = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.ProductDetail.GetLampsPlusChoiceSku.Query, conn))
                {
                    {
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            LampsPlusChoiceProduct = new ProductModel
                            {
                                ShortSku = (string)reader["lampspluschoicesku"],
                                ProductName = reader["ProductName"] == DBNull.Value ? (string)null : (string)reader["ProductName"],
                                Category = reader["Category"] == DBNull.Value ? (string)null : (string)reader["Category"],
                                Finish = reader["finish"] == DBNull.Value ? (string)null : (string)reader["finish"],
                                Style = reader["style"] == DBNull.Value ? (string)null : (string)reader["style"],
                                Usage = reader["usage"] == DBNull.Value ? (string)null : (string)reader["usage"],
                                Type = reader["type"] == DBNull.Value ? (string)null : (string)reader["type"],
                            };
                        }
                    }
                }
            }

            return LampsPlusChoiceProduct;
   
        }

        public bool GetDecremantableFlagForShortSku(string shortSku)
        {
            var isDecrementableFlag = false;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.DecrementableFlagForShortSku.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        isDecrementableFlag = (bool)reader[IsDecrementableString];
                    }
                }
            }

            return isDecrementableFlag;
        }

        public List<string> GetManualDiscountableShortSku()
        {
            var skus = new List<string>();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ShoppingCart.GetManualDiscountableSku.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        skus.Add((string)reader["shortsku"]);
                    }
                }
            }

            return skus;
        }

        public ProductModel GetSkuWithSaleWithComparableValue()
        {
            ProductModel salesProduct = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetail.ProductWithSaleAndComparableValue.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        salesProduct = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            RetailPriceInternet = (decimal)reader["retailpriceinternet"],
                            SalePrice1Internet = (decimal)reader["saleprice1internet"],
                            Savings = (decimal)reader["Saving"],
                            EndSale = (DateTime)reader["endsale1"],
                            SalePrice1 = (decimal)reader["SalePrice1"],
                            RetailPrice = (decimal)reader["RetailPrice"]
                        };
                    }
                }
            }
            return salesProduct;
        }

        public ProductModel GetSkuForResidentialProduct()
        {
            ProductModel residentialProduct = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(ResidentialProductSkuOnRegularPrice.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        residentialProduct = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            RetailPriceInternet = (decimal)reader["RetailPriceInternet"],
                            RetailPrice = (decimal)reader["RetailPrice"],
                            ComparePrice = (decimal)reader["ComparePrice"]
                        };
                    }
                }
            }
            return residentialProduct;
        }

        public ProductModel GetSkuForResidentialProductPros()
        {
            ProductModel ResidentialProductPros = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.PricingBlock.ResidentialProductOnRegularPricePros.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ResidentialProductPros = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            RetailPriceInternet = (decimal)reader["retailpriceinternet"],
                            SpecialDiscount = (decimal)reader["specialdiscount"]
                        };
                    }
                }
            }
            return ResidentialProductPros;
        }

        public int GetQuantityLeft(string shortSku)
        {
            var qtyLeft = 0;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.QuantityLeft.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        qtyLeft = (int)reader[CurrentInventoryString];
                    }
                }
            }

            return qtyLeft;
        }

        public string GetEmailProductSource(int sourceId)
        {
            var source = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.ProductDetail.EmailProductSource.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtSourceIdString, sourceId));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        source = (string)reader[SourceString];
                    }
                }
            }

            return source;
        }

        public string GetItemsThatHaveCheckStoreAvailabilityLinkOnProductDetailPage()
        {
            var sku = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.ProductDetail.SkuWithCheckStoreAvailabilityLinkOnPdp.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, sku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sku = (string)reader[ShortSkuString];
                    }
                }
            }

            return sku;
        }


        public string GetShortSkuWithShippingCharge(SubLocationCode sublocationcode)
        {
            var sku = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.ShoppingCart.ShortSkuWithShippingCharge.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtSubLocationCodeString, sublocationcode));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sku = (string)reader[ShortSkuString];
                    }
                }
            }

            return sku;
        }

        public string GetStorePhoneNumberByCity(string city)
        {
            var phoneNumber = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(ReturnStorePhoneNumberByCity.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@city", city));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        phoneNumber = (string)reader["locsms"];
                    }
                }
            }

            return phoneNumber;
        }

        public List<ProductModel> GetReplacementPartDetail(string  Parentsku)
        {
            var GetreplacementpartList = new List<ProductModel>();

            using (var conn = new SqlConnection(ProductMicroservicesConnectionString))
            using (var cmd = new SqlCommand(Queries.ProductDetail.ReplacementPartDetails.Query, conn))
            {
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@parentsku", Parentsku));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GetreplacementpartList.Add(new ProductModel
                    {
                        ShortSku = (string)reader["PartShortSku"]
                    });
                }
            }
            return GetreplacementpartList;
        }

        public List<ProductModel> GetReplacementBulbDetail(string Parentsku)
        {
            var GetReplacementBulbDetail = new List<ProductModel>();

            using (var conn = new SqlConnection(ProductMicroservicesConnectionString))
            using (var cmd = new SqlCommand(Queries.ProductDetail.GetReplacementBulbDetail.Query, conn))
            {
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@parentSku", Parentsku));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GetReplacementBulbDetail.Add(new ProductModel
                    {
                        ShortSku = (string)reader["BulbSKU"]
                    });
                }
            }
            return GetReplacementBulbDetail;
        }


        public Utilities.ProductModel GetShortSkuNameAndPrice(string shortSku)
        {
            var itemName = string.Empty;
            decimal itemPrice = 0;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            using (var cmd = new SqlCommand(Queries.SortCallout.ShortSkuNameAndPrice.Query, conn))
            {
                cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    itemName = (string)reader["ProductName"];
                    itemPrice = (decimal)reader["SalePrice1Internet"];
                }
            }

            return new Utilities.ProductModel(itemName, shortSku, "0", itemPrice.ToString());
        }
        

        public ProductModel GetMoreOptionItem(string shortSku)
        {
            ProductModel moreOptionsEntity = null;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.MoreOptionsByShortSku.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        moreOptionsEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            Callout = (string)reader["Callout"]
                        };
                    }
                }
            }

            return moreOptionsEntity;
        }

        public ProductModel GetOneHundredPlusItem(string shortSku)
        {
            ProductModel oneHundredPlusEntity = null;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.OneHundredPlusColorsShortSku.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        oneHundredPlusEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            Callout = (string)reader["Callout"]
                        };
                    }
                }
            }

            return oneHundredPlusEntity;
        }

        public ProductModel GetClearancePriceByShortsku(string shortsku)
        {
            ProductModel clearancePriceEntity = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.ClearancePriceByShortSku.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortsku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        clearancePriceEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            IsClearance = (bool)reader["clearanceflag"],
                            RetailPriceInternet = (decimal)reader["RetailPriceInternet"]
                        };
                    }
                }
            }

            return clearancePriceEntity;
        }   

        public ProductModel GetFreeShippingFreeReturnsInformation(string shortSku)
        {
            ProductModel productWithFreeShippingFreeReturnCallOutEntity = null;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.FreeShippingFreeReturnsInformation.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        productWithFreeShippingFreeReturnCallOutEntity = new ProductModel
                        {
                            ShortSku = (string) reader[ShortSkuString],
                            Price = (decimal) reader["Price"],
                            ProductName = (string) reader["ProductName"]
                        };
                    }
                }
                return productWithFreeShippingFreeReturnCallOutEntity;
            }
        }

        public ProductModel GetSalePriceByShortSku(string shortSku)
        {
            ProductModel salePriceEntity = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.SalePriceByShortSku.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        salePriceEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            RetailPriceInternet = (decimal)reader["RetailPriceInternet"],
                            SalePrice1Internet = (decimal)reader["SalePrice1Internet"]
                        };
                    }
                }
            }

            return salePriceEntity;
        }


        public ProductModel GetFreeShippingProduct(string shortSku)
        {
            ProductModel productWithFreeShippingCallOutEntity = null;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.FreeShippingProduct.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        productWithFreeShippingCallOutEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            Price = (decimal)reader["Price"],
                            ProductName = (string)reader["ProductName"]
                        };
                    }
                }
            }

            return productWithFreeShippingCallOutEntity;
        }

        public string GetSearchPath()
        {
            var searchPath = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.Sort.SearchPath.Query, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        searchPath = (string)reader["searchpath"];
                    }
                }
            }

            return searchPath.Remove(0, 10);
        }


        public ProductModel GetMpcItemSkus()
        {
            ProductModel mcpItemEntity = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.ProductDetail.McpArtShadeItemSkus.Query, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        mcpItemEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            BaseSku = (string)reader["BaseSKU"],
                            PatternIdTotal = (int)reader["PatternIDTotal"]
                        };
                    }
                }
            }

            return mcpItemEntity;
        }

        public ProductModel GetProductFreightChargeWithZone3(string shortSku)
        {
            ProductModel productFreightCharge = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.Shipping.ProductFreightChargeWithZone3.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        productFreightCharge = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            FreightCharge = (decimal)reader["FreightCharge"]
                        };
                    }
                }
            }

            return productFreightCharge;
        }

        public ProductModel GetSkuPopularProduct()
        {
            ProductModel skuPopularProductEntity = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(SkuPopularProduct.Query, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        skuPopularProductEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            PatternIdTotal = (int)reader["PatternIdTotal"]
                        };
                    }
                }
            }

            return skuPopularProductEntity;
        }

        public ProductModel GetReplacementParentSku => GetReplacementPartSku(ReplacementPartSku.Query);

        private ProductModel GetReplacementPartSku(string sqlQuery)
        {
            ProductModel ReplacementPartSku = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetail.ReplacementPartSku.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ReplacementPartSku = new ProductModel
                        {
                            ParentSkuString = (string)reader["ShortSKU"],
                        };
                    }
                }
            }
            return ReplacementPartSku;
        }

        public string GetSkuThatHasFinishFamily()
        {
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(SkuThatHasFinishFamily.Query, conn))
                {
                    conn.Open();

                    return (string)cmd.ExecuteScalar() ?? string.Empty;
                }
            }
        }

        public string GetSkuThatHasFinishFamilyWithMoreFinishesSlider()
        {
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(SkuThatHasFinishFamilyWithMoreFinishesSlider.Query, conn))
                {
                    conn.Open();

                    return (string)cmd.ExecuteScalar() ?? string.Empty;
                }
            }
        }
        
        public List<string> GetHousingOptionsSkus(string shortSku)
        {
            var housingOptionsSkus = new List<string>();

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetail.HousingOptionsSkus.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        housingOptionsSkus.Add(reader["ShortSku"].ToString().ToLower());
                    }
                    return housingOptionsSkus;
                }
            }
        }

        public List<string> GetMoreOptionSectionSkus(string shortSku)
        {
            var otherOptionSkus = new List<string>();

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetail.OtherOptionSectionSkus.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        otherOptionSkus.Add(reader[ShortSkuString].ToString().ToLower());
                    }

                    return otherOptionSkus;
                }
            }
        }

        public ProductModel GetFreeShippingSkuData(string shortSku)
        {
            ProductModel freeShippingSkuEntity = null;

            if (!string.IsNullOrWhiteSpace(shortSku))
            {
                using (var conn = new SqlConnection(CartEasyConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(Queries.ProductDetail.FreeShippingSkuData.Query, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            freeShippingSkuEntity = new ProductModel
                            {
                                ShortSku = (string)reader[ShortSkuString],
                                Price = (decimal)reader["Price"],
                                ProductName = (string)reader["productname"]
                            };
                        }
                    }
                }
            }

            return freeShippingSkuEntity;
        }

        public ProductModel GetLpProductOnSaleWithComparePrice()
        {
            ProductModel lpProductOnSaleWithComparePrice = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetailCallout.LPProductOnSaleWithComparePrice.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lpProductOnSaleWithComparePrice = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            ComparePrice = (decimal)reader["compareprice"]
                        };
                    }
                }
            }

            return lpProductOnSaleWithComparePrice;
        }

        public CustomerInformationModel GetLastSavedAddressByEmail(string emailAddress)
        {
            CustomerInformationModel savedAddress = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Shipping.LastSavedAddressByEmail.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@emailAddress", emailAddress));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        savedAddress = new CustomerInformationModel
                        {
                            Email = (string)reader["email"],
                            FirstName = (string)reader["firstname"],
                            LastName = (string)reader["lastname"],
                            Address1 = (string)reader["address1"],
                            Address2 = (string)reader["address2"],
                            City = (string)reader["city"],
                            State = (string)reader["state"],
                            Zip = (string)reader["zip"],
                            Country = (string)reader["country"],
                            Phone = (string)reader["phonenumber"],
                            CreatedDate = (DateTime)reader["createddate"]
                        };
                    }
                }
            }

            return savedAddress;
        }

        public ProductModel GetSingleSku()
        {
            ProductModel skuWithFirstAndLastShipDaysEntity = null;
            var shortSku = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(SingleSku.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        skuWithFirstAndLastShipDaysEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            FirstShipDays = (int)reader["FirstShipDays"],
                            LastShipDays = (int)reader["LastShipDays"]
                        };
                    }
                }
            }

            return skuWithFirstAndLastShipDaysEntity;
        }

        /// <summary>
        /// Gets any two skus
        /// </summary>
        public List<string> GetTwoSkusWithNullUmrp()
        {
            var skus = new List<string>();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetail.GetTwoSkusWithNullUmrp.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        skus.Add((string)reader["shortsku"]);
                    }
                }
            }

            return skus;
        }


        /// <summary>
        /// Gets the current discount rate of a selected company
        /// </summary>
        public decimal GetCurrentDiscountRateSelectedCompany(string companyname)
        {
            var currentDiscountRate = 0m;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ShoppingCart.DiscountRateForOrderTotal.Query, conn))
                {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@companyname", companyname));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) { currentDiscountRate = (decimal)reader["currentDiscountRate"]; }
                }
            }

            return currentDiscountRate;
        }



        public ProductModel GetSkuSavePriceFiveAndOver => SkuSavePrice(SkuSavePriceFiveAndOver.Query);


        public ProductModel GetDeliveryDays()
        {
            ProductModel deliveryDaysValues = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.ShoppingCart.GetDeliveryDays.Query, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        deliveryDaysValues = new ProductModel
                        {
                            ShortSku = (string)reader["shortsku"],
                            FirstShipDays = Convert.ToInt32(reader["firstdelvdays"]),
                            LastShipDays = Convert.ToInt32(reader["lastdelvdays"])
                        };
                    
                    }
                }
            }
            return deliveryDaysValues;
        }

        public List<string> GetSortUrlForFds()
        {
            List<string> sortUrlForFds = new List<string>();
            using (var conn = new SqlConnection(ProductMicroservicesConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Sort.GetSortUrlForFds.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string sortUrl = (string)reader["SortUrl"];
                        sortUrlForFds.Add(sortUrl); 
                    }
                }
            }
            return sortUrlForFds;
        }

        public Utilities.ProductModel GetProductWithLimitedInventory()
        {
            var productRecord = new Utilities.ProductModel();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            using (var cmd = new SqlCommand(Queries.ProductDetailCallout.ProductWithLimitedInventory.Query, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productRecord.Sku = (string)reader[ShortSkuString];
                    productRecord.CurrentInventory = (int)reader["inventory"];
                    productRecord.Inventory = (int)reader[CurrentInventoryString];
                }
            }
            return productRecord;
        }

        public ProductModel GetProductWithBuildFullSystemSkus()
        {
            ProductModel byoDimmerSku = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(BuildFullSystemSku.Query, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        byoDimmerSku = new ProductModel
                        {
                            PrimarySku = (string)reader[PrimarySkuString],
                            BuildFullSystemProducts = new List<BuildFullSystemProductModel>
                            {
                                new BuildFullSystemProductModel
                                {
                                    BuildFullSystemSku = (string)reader[BuildFullSystemSkusString]
                                }
                            }
                        };
                    }

                    while (reader.Read())
                    {
                        byoDimmerSku.BuildFullSystemProducts.Add(new BuildFullSystemProductModel
                        {
                            BuildFullSystemSku = (string)reader[BuildFullSystemSkusString]
                        });
                    }
                }
            }
            return byoDimmerSku;
        }

        /// <summary>
        /// Gets Email records added today to database by the "CartOverview" source.
        /// </summary>
        public List<string> GetRecipientsByEmailAddedTodayFromCart(string[] recipientEmails)
        {
            List<string> recipientEmailData = new List<string>();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ShoppingCartPageBase.RecipientsByEmailAddedTodayFromCart.Query, conn))
                {
                    conn.Open();
                    cmd.CommandText = cmd.CommandText.Replace(
                        "@RecipientEmails",
                        $"'{string.Join("','", recipientEmails)}'"
                        );
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        recipientEmailData.Add((string)reader["emailaddress"]);
                    }
                }
            }

            return recipientEmailData;
        }

        public UserPhoneInfo GetUserPhoneInfo(string email)
        {
            var userPhoneInfo = new UserPhoneInfo();

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ManageAccount.UserPhoneInfo.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtEmailString, email));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userPhoneInfo.PhoneNumber = (string)reader["phonenumber"];
                        userPhoneInfo.Fax = (string)reader["fax"];
                        userPhoneInfo.CellPhoneNumber = (string)reader["cellphonenumber"];
                    }
                }
            }
            return userPhoneInfo;
        }

        public ProductModel GetTradePriceInfo()
        {
            ProductModel tradePriceSku = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(TradePriceInfo.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tradePriceSku = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            TradePrice = decimal.Parse(reader["TradePrice"].ToString()),
                            YourSavings = decimal.Parse(reader["YourSavings"].ToString())
                        };
                    }
                }
            }

            return tradePriceSku;
        }

        public ProductModel GetSkusThatHaveArOption()
        {
            ProductModel skusThatHaveArOption = null;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetail.GetSkusThatHaveArOption.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        skusThatHaveArOption = new ProductModel
                        {
                            ArProducts = new List<ArProductModel>
                            {
                                new ArProductModel
                                {
                                    ShortSku = (string)reader[ShortSkuString.ToLower()],
                                    RetailPriceInternet = (decimal)reader["RetailPriceInternet"],
                                    SalePrice1Internet = (decimal)reader["SalePrice1Internet"],
                                    ProductName = (string)reader["productname"]
                                }
                            }
                        };
                    }

                    while (reader.Read())
                    {
                        skusThatHaveArOption.ArProducts.Add(new ArProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString.ToLower()],
                            RetailPriceInternet = (decimal)reader["RetailPriceInternet"],
                            SalePrice1Internet = (decimal)reader["SalePrice1Internet"],
                            ProductName = (string)reader["productname"]
                        });
                    }
                }
            }

            return skusThatHaveArOption;
        }

        private string ShortSku(string sqlQueryString) => GetSku(ShortSkuString, sqlQueryString);
        private string ShortSkuParameterValue(string shortSku, string paramToReturn, string sqlQueryString) => (string)ParameterValue(AtShortSkuString, shortSku, paramToReturn, sqlQueryString);

        private string GetSku(string typeOfSku, string sqlQueryString)
        {
            var sku = string.Empty;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sqlQueryString, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sku = (string)reader[typeOfSku];
                    }
                }
            }

            return sku;
        }

        private object ParameterValue(string paramName, string paramValue, string paramToReturn, string sqlQueryString)
        {
            object databaseParamValue = null;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sqlQueryString, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(paramName, paramValue));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        databaseParamValue = reader[paramToReturn];
                    }
                }
            }

            return databaseParamValue;
        }

        private ProductModel SkuSavePrice(string sqlQuery)
        {
            ProductModel skuSavePrice = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sqlQuery, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        skuSavePrice = new ProductModel
                        {   
                            ShortSku = (string)reader[ShortSkuString],
                            InitialRetailPrice = (decimal)reader["initialretailprice"],
                            RetailPrice = (decimal)reader["retailprice"],
                            RetailPriceInternet = (decimal)reader["retailpriceinternet"],
                            Savings = (decimal)reader["saving"]
                        };
                    }
                }
            }

            return skuSavePrice;
        }
        

        public string GetShortSkuQualifiedFor3rdDayShipping()
        {
            var sku = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ShoppingCart.ShortSkuQualifiedFor3rdDayShipping.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) { sku = (string)reader["ShortSku"]; }
                }
            }
            return sku;
        }


        public string GetListableInStockShortSku()
        {
            var sku = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Certona.ListableInStockShortSku.Query, conn))
                {
                    conn.Open();
                    cmd.CommandText = cmd.CommandText.Replace("@Amount", $"{1}");
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) { sku = (string)reader["shortSku"]; }
                }
            }

            return sku;
        }

        public List<string> GetListableInStockShortSku(int amountToGet)
        {
            var skus = new List<string>();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Certona.ListableInStockShortSku.Query, conn))
                {
                    conn.Open();
                    cmd.CommandText = cmd.CommandText.Replace("@Amount", $"{amountToGet}");
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        skus.Add((string)reader["shortSku"]);
                    }
                }
            }

            return skus;
        }

        public List<string> GetInStockCoordinatingItems(string shortSku)
        {
            var coordinatingItems = new List<string>();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Certona.InStockCoordinatingItems.Query, conn))
                {
                    conn.Open();
                    cmd.CommandText = cmd.CommandText.Replace("@shortsku", shortSku);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        coordinatingItems.Add((string)reader["ShortSku"]);
                    }
                }
            }

            return coordinatingItems;
        }


        public string GetShortSkuThatHasLessThanOrEqualToTenCoordinatingProducts()
        {
            var sku = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Certona.SkuWithLessThanOrEqualToTenCoordPrdcts.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) { sku = (string)reader["ShortSku"]; }
                }
            }

            return sku;
        }

        public ProductModel GetProductWithCurrentInventory()
        {
            ProductModel product = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.SortCallout.ProductWithQuantityCallout.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        product = new ProductModel
                        {
                            ShortSku = (string)reader["ShortSku"],
                            CurrentInventory = reader["CurrentInventory"].ToString()
                        };
                    }
                }
            }

            return product;
        }

        public ProductModel GetProductWithSkuStatus()
        {
            ProductModel product = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(SkuWithStatus.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        product = new ProductModel
                        {
                            ShortSku = (string)reader["ShortSku"],
                            SkuStatus = reader["SkuStatus"].ToString()
                        };
                    }
                }
            }

            return product;
        }

        public ProductModel GetProductWithWarehouseInventory()
        {
            ProductModel product = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(SkuWithInventory.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        product = new ProductModel
                        {
                            ShortSku = (string)reader["ShortSku"],
                            WarehouseInventory = reader["WarehouseInventory"].ToString()
                        };
                    }
                }
            }

            return product;
        }
        
        public string GetShipsInVerbiage(int firstShipDays, SubLocationCode sublocationcode)
        {
            var shipsInVerbiage = string.Empty;
            using (var conn = new SqlConnection(ProductsConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.ProductDetailCallout.ShipsInVerbiage.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtFirstShipDaysString, firstShipDays));
                    cmd.Parameters.Add(new SqlParameter(AtSubLocationCodeString, sublocationcode));

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        shipsInVerbiage = (string)reader[CopyString];
                    }
                }
            }

            return shipsInVerbiage;
        }

        //Store Location
        public ProductModel GetStoreLocation()
        {
            ProductModel location = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Certona.StoreLocation.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        location = new ProductModel
                        {
                            LocationAddress = (string)reader["locaddress"],
                            LocationCity = (string)reader["loccity"],
                            LocationNumber = (string)reader["locnumber"],
                            LocationPhone = (string)reader["locphone"],
                            LocationSms = (string)reader["locsms"],
                            LocationStoreName = (string)reader["StoreName"],
                            LocationState = (string)reader["locstate"],
                            LocationZip = (string)reader["loczip"]
                        };
                    }
                }
            }
            return location;
        }

        public ProductModel GetProductTradeData()
        {
            ProductModel ProsPrice = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(ProsProductTrade.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ProsPrice = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString.ToLower()],
                            RetailPriceInternet = (decimal)reader["retailpriceinternet"],
                            SpecialDiscount = (decimal)reader["specialdiscount"],
                            Savings = decimal.Parse(reader["SAVING"].ToString())
                        };
                    }
                }
            }
            return ProsPrice;
        }

        public string GetFinialSkuWithMultipleShippingOptions()
        {
            var sku = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(FinialWithMultipleShippingOptionsSku.Query, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sku = (string)reader[ShortSkuString];
                    }
                }
            }

            return sku;
        }

        public string GetPlaSkuWithStarsQAndA()
        {
            var possibleSkus = new string[3] { "5Y584", "69794", "8C397" };
            Random random = new Random();
            return possibleSkus[random.Next(0, 3)];
        }

        public List<CustomerInformationModel> GetLastSavedAddressByCartId(string cartId)
        {
            List<CustomerInformationModel> savedAddresses = new List<CustomerInformationModel>();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Shipping.SavedAddressByCartId.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@cartId", cartId));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        savedAddresses.Add( new CustomerInformationModel
                        {
                            ShortSku = (string)reader["shortsku"],
                            FirstName = (string)reader["shiptofirstname"],
                            LastName = (string)reader["shiptolastname"],
                            Address1 = (string)reader["shiptoaddressline1"],
                            Address2 = (string)reader["shiptoaddressline2"],
                            City = (string)reader["shiptocity"],
                            State = (string)reader["shiptostate"],
                            Zip = (string)reader["shiptozipcode"],
                            Country = (string)reader["shiptocountry"],
                            Phone = (string)reader["shiptophonenumber"]
                        });
                    }
                }
            }

            return savedAddresses;
        }


        public BrandModel GetRandomBrandInfo()
        {
            BrandModel model = new BrandModel();

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(AnySkuWithBrand.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        model.ShortSku = (string)reader["ShortSku"];
                        model.Manufacturer = (string)reader["Manufacturer"];
                        model.Url = (string)reader["Url"];
                    }
                }
            }

            return model;
        }

        public string GetRandomStoreZipCode()
        {
            var locZip = string.Empty;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ShoppingCartEmployee.RandomStoreZipCode.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read()) { locZip = (string)reader["LocZip"]; }
                }
            }

            return locZip;
        }

        private ProductModel SaleSfpAndPla(string sqlQuery)
        {
            ProductModel SaleSfpAndPla = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sqlQuery, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        SaleSfpAndPla = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            SalePrice = (decimal)reader["saleprice"],
                            RetailPrice = (decimal)reader["retailprice"],
                            Savings = (decimal)reader["savings"],
                            EndSale = (DateTime)reader["saleenddate"]
                        };
                    }

                    return SaleSfpAndPla;
                }
            }
        }

        public ProductModel GetResidentialClearanceProduct()
        {
            ProductModel pricingBlockValues = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(ResidentialClearanceProduct.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        pricingBlockValues = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString.ToLower()],
                            RetailPriceInternet = (decimal)reader["retailpriceinternet"],
                            RetailPrice = (decimal)reader["retailprice"],
                            InitialRetailPrice = (decimal)reader["initialretailprice"],
                            Savings = (decimal)reader["Saving"],
                        };
                    }
                }
            }

            return pricingBlockValues;
        }

        public ProductModel GetSaleProductWithSkuAndProductName()
        {
            ProductModel saleProductSkuAndName = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(ProductSalesData.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        saleProductSkuAndName = new ProductModel
                        {
                            ShortSku = (string)reader["shortSku"],
                            ProductName = (string)reader["ProductName"]
                        };
                    }
                }
            }
            return saleProductSkuAndName;
        }


        public ProductModel GetOpenBoxItemWithProductName()
        {
            ProductModel OpenBoxProductSkuAndName = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                SqlCommand cmd;
                using (cmd = new SqlCommand(GetOpenBoxItemWithName.Query, conn)) 
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    OpenBoxProductSkuAndName = new ProductModel
                    {
                        ShortSku = (string) reader["shortSku"],
                        ProductName = (string) reader["ProductName"]
                    };
                } 
                return OpenBoxProductSkuAndName;
            }
        }


        public ProductModel GetSixteenPlusColorsCalloutSkuAndProductName()
        {
            ProductModel sixteenPlusColorsSkuAndName = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(SixteenPlusColorsCallout.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sixteenPlusColorsSkuAndName = new ProductModel
                        {
                            ShortSku = (string)reader["shortSku"],
                            ProductName = (string)reader["ProductName"]
                        };
                    }
                }
            }
            return sixteenPlusColorsSkuAndName;
        }

        public ProductModel GetMoreOptionsCalloutSkuAndProductName()
        {
            ProductModel moreOptionsCalloutSkuAndName = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(ProductWithMoreOptionsCallout.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        moreOptionsCalloutSkuAndName = new ProductModel
                        {
                            ShortSku = (string)reader["shortSku"],
                            ProductName = (string)reader["ProductName"]
                        };
                    }
                }
            }
            return moreOptionsCalloutSkuAndName;
        }

        public ProductModel GetColorPlusSkuAndProductName()
        {
            ProductModel colorPlusSkuAndProductName = null;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetailCallout.HundredPlusCallOut.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        colorPlusSkuAndProductName = new ProductModel
                        {
                            ShortSku = (string)reader["shortSku"],
                            ProductName = (string)reader["ProductName"]
                        };
                    }
                }
            }

            return colorPlusSkuAndProductName;
        }

        public ProductModel GetSoldOutShortSkuAndProductName()
        {
            ProductModel getSoldOutCallout = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(SoldOutCalloutShortSku.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        getSoldOutCallout = new ProductModel
                        {
                            ShortSku = (string)reader["shortSku"],
                            ProductName = (string)reader["ProductName"]
                        };
                    }
                }
            }
            return getSoldOutCallout;
        }

        public ProductModel Get16PlusColorItem(string shortSku)
        {
            ProductModel sixteenPlusColorEntity = null;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(Queries.SortCallout.SixteenPlusColorsCalloutByShortSku.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(AtShortSkuString, shortSku));
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sixteenPlusColorEntity = new ProductModel
                        {
                            ShortSku = (string)reader[ShortSkuString],
                            Callout = (string)reader["Callout"]
                        };
                    }
                }
            }

            return sixteenPlusColorEntity;
        }

        public ProductModel GetSkuWithSavingsGreaterThan5Dollar()
        {
            ProductModel OpenBoxProduct = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.ProductDetail.SkuWithSavingsGreaterThan5Dollar.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        OpenBoxProduct = new ProductModel
                        {
                            ShortSku = (string)reader["ShortSKU"],
                            RetailPrice58 = (decimal)reader["RetailPrice58"],
                            StrikeThroughPrice = (decimal)reader["RetailPriceInternet"],
                            Savings = (decimal)reader["Saving"]
                        };
                    }
                }
            }
            return OpenBoxProduct;
        }
    }
}
